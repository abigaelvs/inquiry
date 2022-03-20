using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class SelectUnitResponse : StandardMessage
    {
        [JsonPropertyName("privileged_access")]
        public List<PrivilegeAccess> ListPrivilege { get; set; }
        
        public SelectUnitResponse(){}

        public SelectUnitResponse(string responseCode, string responseDesc)
            : base (responseCode, responseDesc){}
    }
}