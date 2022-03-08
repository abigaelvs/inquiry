namespace InqService.Constants
{
    public class ResponseCodeConstant
    {
        // RESPONSE_CODE
        public static readonly string RcSuccess = "00";
        public static readonly string MsgSuccess = "SUCCESS";

        public static readonly string RcValidate = "02";
        public static readonly string MsgValidateInvalid = "INVALID";

        public static readonly string RcException = "01";
        public static readonly string MsgException = "EXCEPTION";
        public static readonly string MsgInvalidSignature = "Invalid Signature";
    
        public static readonly string RcInternalEror = "99";
        public static readonly string MsgInternalError = "Error System with Error Code : {errorCode}. Please contact Administration";
    
        public static readonly string RcInvalidUrl = "94";
        public static readonly string MsgInvalidUrl = "URL Not Found";
    }
}
