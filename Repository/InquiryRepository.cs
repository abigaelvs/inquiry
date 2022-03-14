using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;
using System.Text.Json;

using Serilog;

using InqService.Constants;
using InqService.Exceptions;
using InqService.Model;

namespace InqService.Repository
{
    public class InquiryRepository : IInquiryRepository
    {
        private string WrapperHostName { get; set; }

        public InquiryRepository(IConfiguration conf)
        {
            WrapperHostName = conf.GetSection("WrapperHostName").Value;
        }

        public async Task<string> SendRedirect(string requestBody, string url, 
            HttpRequest request)
        {
            string response = "";

            try
            {
                response = await GlobalRepository.CallService(
                    WrapperHostName + url + request.QueryString, requestBody);
            }
            catch (InvalidUrlException ex)
            {
                throw new InvalidUrlException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.Error("Error when call service", ex);
                string errorCode = GlobalRepository.WriteException(ex, "SYSTEM");
                StandardResponse resp = new StandardResponse
                {
                    ResponseCode = ResponseCodeConstant.RcInternalEror,
                    ResponseDesc = ResponseCodeConstant.MsgInternalError.Replace("{errorCode}",
                    errorCode)
                };
                GlobalRepository.SendEmailNotif(errorCode, ex);
                response = JsonSerializer.Serialize(resp);
            }
            return response;
        }
    }
}
