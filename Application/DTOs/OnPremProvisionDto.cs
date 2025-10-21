using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OnPremProvisionDto
    {
        public int Cpus { get; set; }
        public int RamMb { get; set; }
        public int DiskGb { get; set; }
        public string Network { get; set; } = default!;
    }
}
