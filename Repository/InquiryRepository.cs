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

        public SelectUnitResponse SelectUnit(SelectUnitRequest requestData)
        {
            SelectUnitResponse respData = new SelectUnitResponse(ResponseCodeConstant.RcSuccess, ResponseCodeConstant.MsgSuccess);
            PropertyCopier<SelectUnitRequest, SelectUnitResponse>.CopyProperties(requestData, respData);
            
            DBConnection dbconn = null;
            SQLStandard sql = null;
            List<MsPrivilege> privileges = null;
            List<MsGroup> groups = null;
            List<MsGroupPrivilege> groupPrivileges = null;
            List<MsActionPrivilege> actionPrivileges = null;
            List<MsGroupActionPrivilege> groupActionPrivileges = null;

            try
            {
                dbconn = new DBConnection();
                sql = new SQLStandard(dbconn);

                String roleId = requestData.DataUser.RoleId;
                roleId = roleId.Replace(",", "','");
                Dictionary<string, string> criterias = new Dictionary<string, string>
                {
                    { "ums_group_id in ", " ('"+roleId+"')" }
                };
                groups = sql.ExecuteQueryList<MsGroup>(MsGroup.TableName, null, criterias, null);
                String groupId = "";
                if (groups != null && groups.Count > 0) {
                    groups.ForEach(p => groupId += p.GroupId+",");
                    groupId = groupId.Substring(0, groupId.Length-1);
                }
                
                criterias = new Dictionary<string, string>
                {
                    { "group_id in ", " ("+groupId+")" }
                };
                groupPrivileges = sql.ExecuteQueryList<MsGroupPrivilege>(MsGroupPrivilege.TableName, null, criterias, null);
                groupActionPrivileges = sql.ExecuteQueryList<MsGroupActionPrivilege>(MsGroupActionPrivilege.TableName, null, criterias, null);
                String moduleId = "";
                if (groupPrivileges != null && groupPrivileges.Count > 0) {
                    groupPrivileges.ForEach(p => moduleId += p.ModuleId+"','");
                    moduleId = moduleId.Substring(0, moduleId.Length-3);
                }
                String actionId = "";
                if (groupActionPrivileges != null && groupActionPrivileges.Count > 0) {
                    groupActionPrivileges.ForEach(p => actionId += p.ActionId+"','");
                    actionId = actionId.Substring(0, actionId.Length-3);
                }
                
                criterias = new Dictionary<string, string>
                {
                    { "module_id in ", " ('"+moduleId+"')" }
                };
                actionPrivileges = sql.ExecuteQueryList<MsActionPrivilege>(MsActionPrivilege.TableName, null, criterias, null);
                privileges = sql.ExecuteQueryList<MsPrivilege>(MsPrivilege.TableName, null, criterias, null);
                String parentId = "";
                if (privileges != null && privileges.Count > 0) {
                    privileges.ForEach(p => parentId += p.ParentId+"','");
                    parentId = parentId.Substring(0, parentId.Length-3);

                    criterias = new Dictionary<string, string>
                    {
                        { "module_id in ", " ('"+parentId+"')" }
                    };
                    List<MsPrivilege> parentPrivileges = sql.ExecuteQueryList<MsPrivilege>(MsPrivilege.TableName, null, criterias, null);
                    if (parentPrivileges != null && parentPrivileges.Count > 0) privileges.AddRange(parentPrivileges);
                }

                Dictionary<string, List<MsPrivilege>> mapPrivilege = new Dictionary<string, List<MsPrivilege>>();
                Dictionary<string, List<MsActionPrivilege>> mapActionPrivilege = new Dictionary<string, List<MsActionPrivilege>>();
                mapPrivilege = privileges.GroupBy(r => r.ParentId).ToDictionary(g => g.Key, g => g.ToList());
                mapActionPrivilege = actionPrivileges.GroupBy(r => r.ModuleId).ToDictionary(g => g.Key, g => g.ToList());

                Dictionary<string, MsPrivilege> parentPrivilege = new Dictionary<string, MsPrivilege>();
                
                List<PrivilegeAccess> listPrivAccess = new List<PrivilegeAccess>();
                foreach (MsPrivilege c in mapPrivilege[""])
                {
                    Console.WriteLine("KEY1>"+c.ModuleId);
                     
                }

                Console.WriteLine("\n\nMASUK"+mapPrivilege.Count);
                foreach (KeyValuePair<string, List<MsPrivilege>> c in mapPrivilege)
                {
                    Console.WriteLine("KEY>"+c.Key);
                    foreach (MsPrivilege item in c.Value)
                    {
                        Console.WriteLine("Value>"+item.ModuleId);
                    }
                }
                //groups.ForEach(p => Console.WriteLine(p.GroupId));
                //groupPrivileges.ForEach(p => Console.WriteLine(p.ModuleId));
                //Console.WriteLine(groupId);
                Console.WriteLine("KELUAR");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message );
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace );
            }
            finally
            {
                if (dbconn != null) dbconn.Close();
            }
            return respData;
        }
    }
}
