using System;

namespace InqService.Entity
{
    public class ParameterLevel2
    {
        public static readonly string TableName = "MS_PARAMETER_LEVEL2";

		public string KeyParam { get; set; }
		public string Value1Param { get; set; }
		public string Value2Param { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
		public string RecStatus { get; set; }
	}
}
