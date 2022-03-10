using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class CustomerRequest : StandardMessage
    {
        public string Oid { get; set; }

        [JsonPropertyName("cust_name")]
        public string CustName { get; set; }

        [JsonPropertyName("page_no")]
        public int PageNo { get; set; }
    }
}
