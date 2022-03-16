using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class LoginRequest : StandardMessage
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        public string Password { get; set; }
    }
}