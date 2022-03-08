using System;

namespace InqService.Model
{
    public class StandardResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public DateTime ResponseDate { get; set; }
        public DateTime ReqDate { get; set; }
    }
}
