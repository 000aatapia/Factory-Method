using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public class ProviderFactoryResolver : IProvisionerFactoryResolver
    {
        private readonly IEnumerable<ProviderFactoryBase> _factories;

        public ProviderFactoryResolver(IEnumerable<ProviderFactoryBase> factories)
        {
            _factories = factories;
        }

        public ProviderFactoryBase? GetFactoryByName(string providerName)
            => _factories.FirstOrDefault(f =>
                string.Equals(f.ProviderName, providerName, StringComparison.OrdinalIgnoreCase));
    }
}
