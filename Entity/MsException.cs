using System;

namespace InqService.Entity
{
    public class MsException
    {
        public static string TableName = "MS_EXCEPTION";
        public string ErrorCode { get; set; }
        public string StackTrace { get; set; }
        public string RecStatus { get; set; }
        public string CreateBy { get; set; }
        public TimeSpan CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public TimeSpan UpdateDate { get; set; }
    }
}
