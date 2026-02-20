using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuildExeServices.Models
{
    [Keyless]
    public class UserPermissionResponse
    {
        [JsonPropertyName("Key")]
        public string Key { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }

}
