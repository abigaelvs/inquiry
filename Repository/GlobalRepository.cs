using System;
using System.Text;
using System.Threading.Tasks;

using CNDS.Connection;
using CNDS.SqlStandard;
using CNDS.Http;

using InqService.Constants;
using InqService.Entity;

namespace InqService.Repository
{
    public class GlobalRepository : IGlobalRepository
    {
        private static IEmailRepository _emailRepository;
        private static readonly Request req = new Request();

        public GlobalRepository(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public static string GetStackTrace(Exception ex)
        {
            return ex.ToString();
        }
        public static async Task<string> CallService(string urlParam, string obj)
        {
            try
            {
                return await CallService(urlParam, obj, "POST", "application/json");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} -u {urlParam} -o {obj} {ex}");
            }
        }

        public static async Task<string> CallService(string urlParam, object obj, 
            string method, string contentType)
        {
            return await req.CallService(urlParam, obj, method, contentType);
        }
        public static string WriteException(Exception stackTrace, string actor)
        {
            DBConnection dbconn = null;
            MsException exception = new MsException();
            TimeSpan now = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
            string errorCode = "";
            
            try
            {
                errorCode = CreateRandomCode(8);

                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                exception.ErrorCode = errorCode;
                exception.RecStatus = GeneralConstant.StatusActive;
                exception.StackTrace = GetStackTrace(stackTrace);
                exception.CreateBy = actor != null && actor.Trim().Equals("") ? 
                    actor : "SYSTEM";
                exception.CreateDate = now;

                sql.ExecuteInsert(MsException.TableName, exception);

                dbconn.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (exception != null) exception = null;
                if (dbconn != null) dbconn.Close();
            }
            return errorCode;
        }
        public static string CreateRandomCode(int codeLength)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                .ToCharArray();
            StringBuilder sb = new StringBuilder();
            Random random = new Random((int)(DateTime.UtcNow 
                - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);

            for (int i = 0; i < codeLength; i++)
            {
                char c = chars[random.Next(chars.Length)];
                sb.Append(c);
            }
            string output = sb.ToString();
            return output;
        }
        public static void SendEmailNotif(string errorCode, Exception ex)
        {
            _emailRepository.SendEmailNotif(errorCode, ex);
        }
    }
}
