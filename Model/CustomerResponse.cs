using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using CNDS.SqlPaging;

using InqService.Entity;
using InqService.Model;

namespace InqService.Model
{
    public class CustomerResponse : StandardMessage
    {
        [JsonPropertyName("customer_list")]
        public List<MsCustomer> CustomerList { get; set; }

        public CustomerResponse() { }

        public CustomerResponse(string responseCode, string responseDesc,
            List<MsCustomer> customers, SQLPage page)
            : base (responseCode, responseDesc, page)
        {
            CustomerList = customers;
        }
    }
}
