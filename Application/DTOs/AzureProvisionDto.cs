using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AzureProvisionDto
    {
        public string Size { get; set; } = default!;
        public string ResourceGroup { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string VirtualNetwork { get; set; } = default!;
    }
}
