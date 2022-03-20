using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class PrivilegeAccess
    {
        [JsonPropertyName("module_id")]
        public string ModuleId { get; set; }

        [JsonPropertyName("module_name")]
        public string ModuleName { get; set; }
        
        [JsonPropertyName("module_type")]
        public string ModuleType { get; set; }

        [JsonPropertyName("parent_module_id")]
        public string ParentModuleId { get; set; }

        [JsonPropertyName("module_url")]
        public string ModuleUrl { get; set; }

        [JsonPropertyName("sub_module")]
        public List<PrivilegeAccess> ListSubModule { get; set; }

        [JsonPropertyName("action_list")]
        public List<ActionAccess> ListAction { get; set; }
    }
}