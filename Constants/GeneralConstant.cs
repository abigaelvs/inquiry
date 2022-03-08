namespace InqService.Constants
{
    public class GeneralConstant
    {
		public static string StatusActive = "A";
		public static string StatusInactive = "N";
		public static string StatusYes = "Y";
		public static string StatusNo = "N";

		public static string CreatedBy = "SYSTEM";

		public static string ServiceTypeOutgoing = "O";
		public static string ServiceTypeIncoming = "I";

		//Global Param
		public static readonly string ParameterGlobalDefaultTimeout = "param.global.default.timeout";
	
		//Email notif
		public static readonly string StatusSuccess = "SUCCESS";
		public static readonly string StatusFailed = "FAILED";
		public static readonly string ParameterEmailSubject = "param.email.subject";
		public static readonly string ParameterEmailTo = "param.email.to";
		public static readonly string ParameterEmailCc = "param.email.cc";
		public static readonly string ParameterEmailBcc = "param.email.bcc";
		public static readonly string ParameterEmailFrom = "param.email.from";
	
		//email config
		public static readonly string ParameterEmailConfig = "param.config.email";
		public static readonly string ParameterEmailConfigProperties = "param.config.email.properties";
    }
}
