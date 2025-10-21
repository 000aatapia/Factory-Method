using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public abstract class ProviderFactoryBase
    {
        public abstract string ProviderName { get; }

        public abstract IVmProvisioner CreateProvisioner(IServiceProvider sp);
    }
}
