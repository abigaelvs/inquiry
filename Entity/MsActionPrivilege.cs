using System;

namespace InqService.Entity
{
    public class MsActionPrivilege
    {
        public static string TableName = "MS_ACTION_PRIVILEGE";
        public string ModuleId { get; set; }
		public string ActionId { get; set; }
        public string ActionName { get; set; }
	}
}
