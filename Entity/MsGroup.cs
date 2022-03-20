using System;

namespace InqService.Entity
{
    public class MsGroup
    {
        public static string TableName = "MS_GROUP";
        public long GroupId { get; set; }
		public string GroupName { get; set; }
		public string Description { get; set; }
		public string UmsGroupId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
	}
}
