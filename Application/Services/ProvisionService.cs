using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Application.Services
{
    public class ProvisionService
    {
        private readonly IProvisionerFactoryResolver _resolver;
        private readonly ILogger<ProvisionService> _logger;

        public ProvisionService(IProvisionerFactoryResolver resolver, ILogger<ProvisionService> logger)
        {
            _resolver = resolver;
            _logger = logger;
        }

        public async Task<VmProvisionResult> ProvisionAsync(VmProvisionRequest request, CancellationToken ct)
        {
            var providerName = request.Provider?.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(providerName))
                return new VmProvisionResult { Success = false, Error = "provider is required" };

            var factory = _resolver.GetFactoryByName(providerName);
            if (factory is null)
            {
                _logger.LogWarning("Provider '{Provider}' not found", providerName);
                return new VmProvisionResult { Success = false, Error = $"unknown provider '{providerName}'" };
            }

            var provisioner = factory.CreateProvisioner(null!);

            object typedParams;
            try
            {
                typedParams = providerName switch
                {
                    "aws" => request.Parameters.Deserialize<AwsProvisionDto>()!,
                    "azure" => request.Parameters.Deserialize<AzureProvisionDto>()!,
                    "gcp" => request.Parameters.Deserialize<GcpProvisionDto>()!,
                    "onpremise" => request.Parameters.Deserialize<OnPremProvisionDto>()!,
                    _ => throw new InvalidOperationException("unsupported provider")
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Invalid parameter format for provider {Provider}", providerName);
                return new VmProvisionResult { Success = false, Error = "invalid parameters" };
            }

            _logger.LogInformation("Starting provisioning: provider={Provider}, requestId={RequestId}", providerName, request.RequestId);

            try
            {
                var result = await provisioner.ProvisionAsync(typedParams, ct);

                if (result.Success)
                    _logger.LogInformation("Provisioned successfully: {Provider} -> {VmId}", providerName, result.VmId);
                else
                    _logger.LogWarning("Provision failed: {Provider} -> {Error}", providerName, result.Error);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error provisioning {Provider}", providerName);
                return new VmProvisionResult { Success = false, Error = "unexpected error" };
            }
        }
    }
}
