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
    public class GcpProvisioner : IVmProvisioner
    {
        public string ProviderName => "gcp";
        private readonly ILogger<GcpProvisioner> _logger;

        public GcpProvisioner(ILogger<GcpProvisioner> logger)
        {
            _logger = logger;
        }

        public async Task<VmProvisionResult> ProvisionAsync(object parameters, CancellationToken ct)
        {
            var p = parameters as GcpProvisionDto ?? throw new ArgumentException("parametros invalidos GCP");

            _logger.LogInformation("simulacion GCP VM creacion: type={Type}, zone={Zone}", p.MachineType, p.Zone);
            await Task.Delay(500, ct);

            var vmId = $"gcp-{Guid.NewGuid():N}";
            return new VmProvisionResult { Success = true, VmId = vmId };
        }
    }
}
