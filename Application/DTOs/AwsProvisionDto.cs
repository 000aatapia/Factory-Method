using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AwsProvisionDto
    {
        public string InstanceType { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string Vpc { get; set; } = default!;
        public string Ami { get; set; } = default!;
    }
}
