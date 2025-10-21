using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructure.Helpers
{
    public static class MaskingHelper
    {
        private static readonly string[] SensitiveKeys =
            { "password", "token", "secret", "accesskey", "secretkey" };

        public static string MaskSensitive(JsonElement parameters)
        {
            try
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(parameters.GetRawText())!;
                foreach (var key in SensitiveKeys)
                {
                    var match = dict.Keys.FirstOrDefault(k => k.Equals(key, StringComparison.OrdinalIgnoreCase));
                    if (match != null)
                        dict[match] = "***redactado***";
                }
                return JsonSerializer.Serialize(dict);
            }
            catch
            {
                return parameters.ToString();
            }
        }
    }
}
