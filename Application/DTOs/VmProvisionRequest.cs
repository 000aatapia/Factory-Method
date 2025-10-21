using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VmProvisionRequest
    {
        public string Provider { get; set; } = default!;
        public JsonElement Parameters { get; set; }  // JSON con parámetros específicos por proveedor
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
    }
}
