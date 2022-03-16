using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

using Serilog;

using CNDS.Connection;
using CNDS.SqlPaging;
using CNDS.SqlStandard;
using CNDS.Utils;

using InqService.Entity;
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

        public LoginResponse DoLogin(LoginRequest requestData)
        {
            string response = "";
            List<UserDataFull> userData = null;
            //LoginResponse respData = Utility.CloneObject(requestData) as LoginResponse;
            LoginResponse respData = new LoginResponse(ResponseCodeConstant.RcSuccess, ResponseCodeConstant.MsgSuccess);
            PropertyCopier<LoginRequest, LoginResponse>.CopyProperties(requestData, respData);
            try
            {
                userData = new List<UserDataFull>();
                for (int i = 0; i < 2; i++)
                {
                    UserDataFull data = new UserDataFull();
                    data.OrganizationId = 123;
                    data.OrganizationName = "Kebon Jeruk";
                    data.HopeOrganizationId = 123;
                    data.MobileOrganizationId = "123";
                    data.AxOrganizationId = "123";
                    data.RoleId = (i==0)? "cashier" : "spv_cashier";
                    data.RoleName = (i==0)? "Cashier" : "SPV Cashier";
                    data.UserId = "ihwan";
                    data.UserName = "Ihwan";
                    data.FullName = "Ihwan Daus";
                    data.HopeUserId = 111;
                    data.Email = "daus@gmail.com";
                    data.Birthday = new DateTime();
                    data.Handpone = "085";
                    data.UserRoleId = (i==0)? 12 : 13;

                    userData.Add(data);
                }

                respData.LoginStatus = true;
                respData.UserDataList = userData;
                response = JsonSerializer.Serialize(respData);
                Console.WriteLine("DATA>>>>>"+response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.Error("Error when call service", ex);
                string errorCode = GlobalRepository.WriteException(ex, "SYSTEM");
                respData.ResponseCode = ResponseCodeConstant.RcInternalEror;
                respData.ResponseDesc = ResponseCodeConstant.MsgInternalError.Replace("{errorCode}", errorCode);
                GlobalRepository.SendEmailNotif(errorCode, ex);
                response = JsonSerializer.Serialize(respData);
            }
            return respData;
        }
    }
}
