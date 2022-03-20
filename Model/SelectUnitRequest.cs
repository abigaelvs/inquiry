using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class SelectUnitRequest : StandardMessage
    {
        [JsonPropertyName("user_data")]
        public UserData DataUser { get; set; }
        
    }
}