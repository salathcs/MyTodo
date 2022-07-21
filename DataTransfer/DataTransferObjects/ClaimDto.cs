using System.Text.Json.Serialization;

namespace DataTransfer.DataTransferObjects
{
    public class ClaimDto
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
