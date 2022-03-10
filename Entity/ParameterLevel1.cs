using System;
using System.Text.Json.Serialization;

namespace InqService.Entity
{
    public class ParameterLevel1
    {
        public static readonly string TableName = "MS_PARAMETER_LEVEL1";

		[JsonPropertyName("key_param")]
		public string KeyParam { get; set; }

        [JsonPropertyName("value1_param")]
        public string Value1Param { get; set; }

		[JsonPropertyName("create_by")]
		public string CreateBy { get; set; }

		[JsonPropertyName("create_date")]
		public DateTime CreateDate { get; set; }

		[JsonPropertyName("update_by")]
		public string UpdateBy { get; set; }

		[JsonPropertyName("update_date")]
		public DateTime UpdateDate { get; set; }

		[JsonPropertyName("rec_status")]
		public string RecStatus { get; set; }

		[JsonPropertyName("restart_scheduler")]
		public string RestartScheduler { get; set; }
	}
}
