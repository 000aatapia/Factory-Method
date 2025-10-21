using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VmProvisionResult
    {
        public bool Success { get; set; }
        public string? VmId { get; set; }
        public string? Error { get; set; }
    }
}
