using System;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class UserDataFull
    {
        [JsonPropertyName("organization_id")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("organization_name")]
        public string OrganizationName { get; set; }

        [JsonPropertyName("hope_organization_id")]
        public int HopeOrganizationId { get; set; }

        [JsonPropertyName("mobile_organization_id")]
        public string MobileOrganizationId { get; set; }

        [JsonPropertyName("ax_organization_id")]
        public string AxOrganizationId { get; set; }

        [JsonPropertyName("role_id")]
        public string RoleId { get; set; }

        [JsonPropertyName("role_name")]
        public string RoleName { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("hope_user_id")]
        public int HopeUserId { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Handpone { get; set; }

        [JsonPropertyName("user_role_id")]
        public int UserRoleId { get; set; }
    }
}