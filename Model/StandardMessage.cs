using System;
using System.Text.Json.Serialization;

using CNDS.SqlPaging;

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

        [JsonPropertyName("response_time")]
        public string ResponseTime { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("source_reff_id")]
        public string SourceReffId { get; set; }

        [JsonPropertyName("reff_id")]
        public string ReffId { get; set; }

        [JsonPropertyName("page_no")]
        [JsonIgnore]
        public int PageNo { get; set; }

        public SQLPage Paging { get; set; }

        public StandardMessage() { }
        public StandardMessage(string responseCode, string responseDesc, string requestTime,
            string channelId, string sourceReffId, string reffId)
        {
            ResponseCode = responseCode;
            ResponseDesc = responseDesc;
            RequestTime = requestTime;
            ResponseTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ChannelId = channelId;
            SourceReffId = sourceReffId;
            ReffId = reffId;
        }

        public StandardMessage(string responseCode, string responseDesc, string requestTime,
            string channelId, string sourceReffId, string reffId, SQLPage page)
        {
            ResponseCode = responseCode;
            ResponseDesc = responseDesc;
            RequestTime = requestTime;
            ResponseTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ChannelId = channelId;
            SourceReffId = sourceReffId;
            ReffId = reffId;
            Paging = page;
        }
    }
}
