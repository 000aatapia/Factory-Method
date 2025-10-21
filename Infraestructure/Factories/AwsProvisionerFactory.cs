using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public class AwsProvisionerFactory : ProviderFactoryBase
    {
        private readonly IServiceProvider _sp;
        public AwsProvisionerFactory(IServiceProvider sp) => _sp = sp;
        public override string ProviderName => "aws";

        public override IVmProvisioner CreateProvisioner(IServiceProvider sp)
            => _sp.GetRequiredService<Provisioners.AwsProvisioner>();
    }
}
