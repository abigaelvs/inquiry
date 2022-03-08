using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

using InqService.Constants;
using InqService.Entity;
using InqService.Repository;

namespace InqService.Config
{
    public class EmailConfig
    {
        public SmtpClient MailSender { get; set; }
        public EmailConfig()
        {
            InitEmailConfig();
        }

        public Dictionary<string, object> UpdateEmailConfig()
        {
            return InitEmailConfig();
        }
        private Dictionary<string, object> InitEmailConfig()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();

            try
            {
                MailSender = new SmtpClient();
                NetworkCredential credential = new NetworkCredential(
                "bf0cb9c54318f7", "83fec05318a404");

                List<ParameterLevel2> listEmailConfig = ParameterRepository
                    .GetListParameterLvl2(GeneralConstant.ParameterEmailConfig);

                foreach (ParameterLevel2 parameterLevel2 in listEmailConfig)
                {
                    obj.Add(parameterLevel2.Value1Param, parameterLevel2.Value2Param);

                    if (parameterLevel2.Value1Param.Equals("mail.host", 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        MailSender.Host = parameterLevel2.Value2Param;
                    }

                    if (parameterLevel2.Value1Param.Equals("mail.port",
                        StringComparison.OrdinalIgnoreCase)) 
                    {
                        string portStr = parameterLevel2.Value2Param;
                        int port = portStr != null && !portStr.Trim().Equals("") ?
                            Int16.Parse(portStr) : 0;
                        if (port != 0) MailSender.Port = port;
                    }

                    if (parameterLevel2.Value1Param.Equals("mail.username",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        if (parameterLevel2.Value2Param != null
                            && !parameterLevel2.Value2Param.Trim().Equals(""))
                        {
                            credential.UserName = parameterLevel2.Value2Param;
                        }
                    }

                    if (parameterLevel2.Value1Param.Equals("mail.password",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        if (parameterLevel2.Value2Param != null
                            && !parameterLevel2.Value2Param.Trim().Equals(""))
                        {
                            credential.Password = parameterLevel2.Value2Param;
                        }
                    }
                }
                obj.Add("response_code", ResponseCodeConstant.RcSuccess);
                obj.Add("response_desc", ResponseCodeConstant.MsgSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                string errorCode = GlobalRepository.WriteException(ex,
                    "refresh-param-email-config");
                string error = ResponseCodeConstant.MsgInternalError.Replace(
                    "{errorCode}", errorCode);
                obj.Add("response_code", ResponseCodeConstant.RcInternalEror);
                obj.Add("response_desc", error);
                GlobalRepository.SendEmailNotif(errorCode, ex);
            }
            return obj;
        }
    }
}
