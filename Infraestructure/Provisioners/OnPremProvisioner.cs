using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.DTOs;
using Application.Interfaces;

namespace Infraestructure.Provisioners
{
    public class OnPremProvisioner : IVmProvisioner
    {
        public string ProviderName => "onpremise";
        private readonly ILogger<OnPremProvisioner> _logger;

        public OnPremProvisioner(ILogger<OnPremProvisioner> logger)
        {
            _logger = logger;
        }

        public async Task<VmProvisionResult> ProvisionAsync(object parameters, CancellationToken ct)
        {
            var p = parameters as OnPremProvisionDto ?? throw new ArgumentException("parametros invalidos para  On-Premise");

            _logger.LogInformation("simulacion On-Premise VM: {Cpu} CPU, {Ram}MB RAM, {Disk}GB", p.Cpus, p.RamMb, p.DiskGb);
            await Task.Delay(500, ct);

            var vmId = $"onprem-{Guid.NewGuid():N}";
            return new VmProvisionResult { Success = true, VmId = vmId };
        }
    }
}
