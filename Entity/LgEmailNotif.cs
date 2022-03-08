using System;

namespace InqService.Entity
{
    public class LgEmailNotif
    {
        public static string TableName = "LG_EMAIL_NOTIF";
        public long LogId { get; set; }
		public string EmailSubject { get; set; }
		public string EmailTo { get; set; }
		public string EmailCc { get; set; }
		public string EmailBcc { get; set; }
		public string Content { get; set; }
		public string Status { get; set; }
		public string Description { get; set; }
		public string LogBy { get; set; }
		public DateTime LogDate { get; set; }
	}
}
