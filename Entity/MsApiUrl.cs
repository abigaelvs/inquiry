namespace InqService.Entity
{
    public class MsApiUrl
    {
        public static string TableName = "MS_API_URL";
        public static string UrlTypeInq = "INQUIRY";
        public static string UrlTypeTrx = "TRANSACTION";

        public string UrlType { get; set; }
        public string UrlPath { get; set; }
    }
}
