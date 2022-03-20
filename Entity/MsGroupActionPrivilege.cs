using System;

namespace InqService.Entity
{
    public class MsGroupActionPrivilege
    {
        public static string TableName = "MS_GROUP_ACTION_PRIVILEGE";
        public long GroupId { get; set; }
		public string ActionId { get; set; }
	}
}
