using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GcpProvisionDto
    {
        public string MachineType { get; set; } = default!;
        public string Zone { get; set; } = default!;
        public string Disk { get; set; } = default!;
        public string Project { get; set; } = default!;
    }
}
