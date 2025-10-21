using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public class GcpProvisionerFactory : ProviderFactoryBase
    {
        private readonly IServiceProvider _sp;
        public GcpProvisionerFactory(IServiceProvider sp) => _sp = sp;
        public override string ProviderName => "gcp";

        public override IVmProvisioner CreateProvisioner(IServiceProvider sp)
            => _sp.GetRequiredService<Provisioners.GcpProvisioner>();
    }
}
