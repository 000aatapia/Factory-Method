using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.DTOs;
using Application.Interfaces;

namespace Infraestructure.Provisioners;
public class AzureProvisioner : IVmProvisioner
{
    public string ProviderName => "azure";
    private readonly ILogger<AzureProvisioner> _logger;

    public AzureProvisioner(ILogger<AzureProvisioner> logger)
    {
        _logger = logger;
    }

    public async Task<VmProvisionResult> ProvisionAsync(object parameters, CancellationToken ct)
    {
        var p = parameters as AzureProvisionDto ?? throw new ArgumentException("parametros invalidos Azure");

        _logger.LogInformation("Simulacopm Azure VM creacion: size={Size}, resourceGroup={Group}", p.Size, p.ResourceGroup);
        await Task.Delay(500, ct);

        var vmId = $"az-{Guid.NewGuid():N}";
        return new VmProvisionResult { Success = true, VmId = vmId };
    }
}