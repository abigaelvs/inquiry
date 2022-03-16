using System;
using System.Text.Json.Serialization;

using CNDS.Utils;
using CNDS.SqlPaging;

namespace InqService.Model
{
    public class StandardMessage
    {
        [JsonPropertyName("response_code")]
        [NoCopyProperty]
        public string ResponseCode { get; set; }

        [JsonPropertyName("response_desc")]
        [NoCopyProperty]
        public string ResponseDesc { get; set; }

        [JsonPropertyName("request_time")]
        public string RequestTime { get; set; }

        [JsonPropertyName("response_time")]
        [NoCopyProperty]
        public string ResponseTime { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("source_ref_id")]
        public string SourceRefId { get; set; }

        [JsonPropertyName("ref_id")]
        public string RefId { get; set; }

        [JsonPropertyName("page_no")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [NoCopyProperty]
        public int PageNo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [NoCopyProperty]
        public SQLPage Paging { get; set; }

        public StandardMessage() { }

        public StandardMessage(string responseCode, string responseDesc, SQLPage page)
        {
            ResponseCode = responseCode;
            ResponseDesc = responseDesc;
            ResponseTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            Paging = page;
        }

        public StandardMessage(string responseCode, string responseDesc)
        {
            ResponseCode = responseCode;
            ResponseDesc = responseDesc;
            ResponseTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
