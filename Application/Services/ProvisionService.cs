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
                return new VmProvisionResult { Success = false, Error = "el provedor es obligatorio" };

            var factory = _resolver.GetFactoryByName(providerName);
            if (factory is null)
            {
                _logger.LogWarning("provedor '{Provider}'", providerName);
                return new VmProvisionResult { Success = false, Error = $"provedor desconocido '{providerName}'" };
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
                    _ => throw new InvalidOperationException("proveedor no soportado")
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "parametros invalos para el proveedor {Provider}", providerName);
                return new VmProvisionResult { Success = false, Error = "Parametros invalidos" };
            }

            _logger.LogInformation("Iniciando aprovisionamiento: provider={Provider}, requestId={RequestId}", providerName, request.RequestId);

            try
            {
                var result = await provisioner.ProvisionAsync(typedParams, ct);

                if (result.Success)
                    _logger.LogInformation("Aprovisionamiento exitoso: {Provider} -> {VmId}", providerName, result.VmId);
                else
                    _logger.LogWarning("Aprovisionamiento fallido: {Provider} -> {Error}", providerName, result.Error);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error de  aprovisionamiento {Provider}", providerName);
                return new VmProvisionResult { Success = false, Error = "Error" };
            }
        }
    }
}
