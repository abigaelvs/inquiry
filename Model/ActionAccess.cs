using System;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class ActionAccess
    {
        [JsonPropertyName("action_id")]
        public string ActionId { get; set; }

        [JsonPropertyName("action_name")]
        public string ActionName { get; set; }
    }
}