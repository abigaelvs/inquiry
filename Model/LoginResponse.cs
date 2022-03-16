using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class LoginResponse : StandardMessage
    {
        [JsonPropertyName("login_status")]
        public bool LoginStatus { get; set; }

        [JsonPropertyName("user_data_list")]
        public List<UserDataFull> UserDataList { get; set; }

        public LoginResponse(){}

        public LoginResponse(string responseCode, string responseDesc)
            : base (responseCode, responseDesc){}
        
    }
}