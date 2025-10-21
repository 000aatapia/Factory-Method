using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVmProvisioner
    {
        string ProviderName { get; }
        Task<VmProvisionResult> ProvisionAsync(object parameters, CancellationToken ct);
    }
}
