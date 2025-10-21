using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public class AzureProvisionerFactory : ProviderFactoryBase
    {
        private readonly IServiceProvider _sp;
        public AzureProvisionerFactory(IServiceProvider sp) => _sp = sp;
        public override string ProviderName => "azure";

        public override IVmProvisioner CreateProvisioner(IServiceProvider sp)
            => _sp.GetRequiredService<Provisioners.AzureProvisioner>();
    }
}
