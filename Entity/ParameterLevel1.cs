using System;

namespace InqService.Entity
{
    public class ParameterLevel1
    {
        public static readonly string TableName = "MS_PARAMETER_LEVEL1";

		public string KeyParam { get; set; }
		public string Value1Param { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
		public string RecStatus { get; set; }
		public string RestartScheduler { get; set; }
	}
}
