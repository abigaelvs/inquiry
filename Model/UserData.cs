using System;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class UserData
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("role_id")]
        public string RoleId { get; set; }
        
        [JsonPropertyName("organization_id")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("hope_organization_id")]
        public int HopeOrganizationId { get; set; }

        [JsonPropertyName("mobile_organization_id")]
        public string MobileOrganizationId { get; set; }

        [JsonPropertyName("ax_organization_id")]
        public string AxOrganizationId { get; set; }

        [JsonPropertyName("hope_user_id")]
        public int HopeUserId { get; set; }

        [JsonPropertyName("group_id")]
        public string GroupId { get; set; }
    }
}