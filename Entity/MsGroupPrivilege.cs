using System;

namespace InqService.Entity
{
    public class MsGroupPrivilege
    {
        public static string TableName = "MS_GROUP_PRIVILEGE";
        public long GroupId { get; set; }
		public string ModuleId { get; set; }
	}
}
