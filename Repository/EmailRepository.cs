using Microsoft.Extensions.Configuration;

using System;
using System.Net;
using System.Net.Mail;
using System.Text;

using CNDS.Connection;
using CNDS.SqlStandard;

using InqService.Config;
using InqService.Constants;
using InqService.Entity;

namespace InqService.Repository
{
    public class EmailRepository : IEmailRepository
    {
        public EmailConfig MailSender;

        private string EmailNotifContent { get; set; }

        public EmailRepository(IConfiguration conf, EmailConfig mailSender)
        {
            EmailNotifContent = conf.GetSection("EmailNotifContent").Value;
            MailSender = mailSender;
        }

        public void SendEmailNotif(string errorCode, Exception ex)
        {
            if (StartupRepository.ht == null || StartupRepository.ht.Count == 0)
            {
                StartupRepository.Init();
                Console.WriteLine("\n\n\nKOSONG");
            }

            string emailSubject = StartupRepository.ht[
                GeneralConstant.ParameterEmailSubject].ToString() + " - " + errorCode;
            string listEmailTo = StartupRepository.ht[
                GeneralConstant.ParameterEmailTo].ToString();
            string listEmailCc = StartupRepository.ht[
                GeneralConstant.ParameterEmailCc].ToString();
            string listEmailBcc = StartupRepository.ht[
                GeneralConstant.ParameterEmailBcc].ToString();

            string[] emailTo = listEmailTo != null && !listEmailTo.Trim().Equals("")
                ? listEmailTo.Split(";") : null;
            string[] emailCc = listEmailCc != null && !listEmailCc.Trim().Equals("")
                ? listEmailCc.Split(";") : null;
            string[] emailBcc = listEmailBcc != null && !listEmailBcc.Trim().Equals("")
                ? listEmailBcc.Split(";") : null;
            string error = GlobalRepository.GetStackTrace(ex);
            string content = EmailNotifContent.Replace("{errorCode}", errorCode)
                .Replace("{errorMessage}", error.Substring(0, error.IndexOf("at ")));

            LgEmailNotif emailNotif = new LgEmailNotif
            {
                EmailSubject = emailSubject,
                EmailTo = listEmailTo,
                EmailCc = listEmailCc,
                EmailBcc = listEmailBcc,
                Content = content
            };

            try
            {
                DoSend(emailTo, emailCc, emailBcc, emailSubject, content);
                emailNotif.Status = GeneralConstant.StatusSuccess;
                emailNotif.Description = GeneralConstant.StatusSuccess;
                DoSaveNotif(emailNotif);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                emailNotif.Status = GeneralConstant.StatusFailed;
                emailNotif.Description = GlobalRepository.GetStackTrace(e);
                DoSaveNotif(emailNotif);
            }
        }

        //TODO: Add email attachment feature later (if needed)
        private void DoSend(string[] sendTo, string[] cc, string[] bcc, string subject,
            string body)
        {
            MailAddress emailFrom = new MailAddress(
                StartupRepository.ht[GeneralConstant.ParameterEmailFrom].ToString());

            SmtpClient client = new SmtpClient("smtp.mailtrap.io", 2525);
            NetworkCredential basicCredential1 = new NetworkCredential(
                "bf0cb9c54318f7", "83fec05318a404");
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.Credentials = basicCredential1;
            try
            {
                foreach (string to in sendTo)
                {
                    MailAddress emailTo = new MailAddress(to);
                    MailMessage message = new MailMessage(emailTo, emailFrom);
                    message.Subject = subject;
                    message.Body = body;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;

                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DoSaveNotif(LgEmailNotif emailNotif)
        {
            DBConnection dbconn = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);

                long logId = long.MinValue;
                logId = (long)(DateTime.UtcNow 
                    - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                    .TotalMilliseconds;
                emailNotif.LogId = logId;
                emailNotif.LogBy = "SYSTEM";
                emailNotif.LogDate = new DateTime(logId);

                sql.ExecuteInsert(LgEmailNotif.TableName, emailNotif);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (dbconn != null) dbconn.Close();
            }
        }
    }
}
