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

        public CustomerResponse(string responseCode, string responseDesc, 
            CustomerRequest customer, SQLPage page, List<MsCustomer> customers)
            : base(responseCode, responseDesc, customer.RequestTime, customer.ChannelId,
                customer.SourceReffId, customer.ReffId, page)
        {
            CustomerList = customers;
        }

        public CustomerResponse(string responseCode, string responseDesc,
            StandardMessage customer, SQLPage page, List<MsCustomer> customers)
            : base(responseCode, responseDesc, customer.RequestTime, customer.ChannelId,
                customer.SourceReffId, customer.ReffId, page)
        {
            CustomerList = customers;
        }
    }
}
