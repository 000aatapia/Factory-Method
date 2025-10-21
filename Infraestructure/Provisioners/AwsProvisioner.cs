using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.DTOs;
using Application.Interfaces;

namespace Infraestructure.Provisioners;
public class AwsProvisioner : IVmProvisioner
{
    public string ProviderName => "aws";
    private readonly ILogger<AwsProvisioner> _logger;

    public AwsProvisioner(ILogger<AwsProvisioner> logger)
    {
        _logger = logger;
    }

    public async Task<VmProvisionResult> ProvisionAsync(object parameters, CancellationToken ct)
    {
        var p = parameters as AwsProvisionDto ?? throw new ArgumentException("parametros invalidos AWS");

        _logger.LogInformation("simulacion  AWS VM creacion : type={Type}, region={Region}", p.InstanceType, p.Region);
        await Task.Delay(500, ct);

        var vmId = $"aws-{Guid.NewGuid():N}";
        return new VmProvisionResult { Success = true, VmId = vmId };
    }
}
