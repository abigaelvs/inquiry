using System;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class StandardMessage
    {
        [JsonPropertyName("response_code")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("response_desc")]
        public string ResponseDesc { get; set; }

        [JsonPropertyName("request_time")]
        public string RequestTime { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("source_reff_id")]
        public string SourceReffId { get; set; }

        [JsonPropertyName("page_no")]
        public int PageNo { get; set; }
    }
}
