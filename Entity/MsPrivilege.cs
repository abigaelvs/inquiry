using System;

namespace InqService.Entity
{
    public class MsPrivilege
    {
        public static string TableName = "MS_PRIVILEGE";
        public string ModuleId { get; set; }
		public string ModuleName { get; set; }
		public string ParentId { get; set; }
		public string Description { get; set; }
		public string Level { get; set; }
		public string Type { get; set; }
		public string Url { get; set; }
		public string AccessLevel { get; set; }
		public string FaIcon { get; set; }
	}
}
