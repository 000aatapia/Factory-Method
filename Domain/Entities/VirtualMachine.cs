using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class VirtualMachine
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public ProviderType Provider { get; private set; }
        public VmConfiguration Configuration { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private VirtualMachine(string id, string name, ProviderType provider, VmConfiguration config)
        {
            Id = id;
            Name = name;
            Provider = provider;
            Configuration = config;
            CreatedAt = DateTime.UtcNow;
        }

        public static VirtualMachine Create(string name, ProviderType provider, VmConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("VM name cannot be empty");

            return new VirtualMachine(Guid.NewGuid().ToString(), name, provider, config);
        }
    }
}
