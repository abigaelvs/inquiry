using System.Collections.Generic;
using System.Text.Json.Serialization;

using InqService.Entity;

namespace InqService.Model
{
    public class CustomerResponse : StandardMessage
    {
        [JsonPropertyName("customer_list")]
        public List<MsCustomer> CustomerList { get; set; }
    }
}
