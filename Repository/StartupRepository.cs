using System;
using System.Collections.Generic;
using System.Text.Json;

using InqService.Constants;
using InqService.Entity;

namespace InqService.Repository
{
    public class StartupRepository
    {
        public static Dictionary<string, object> ht = null;
        public static Dictionary<string, object> globalParam = null;
        private static string error = "";

        public static bool Init()
        {
            List<ParameterLevel1> listParam = null;

            try
            {
                if (ht == null) ht = new Dictionary<string, object>();
                else ht.Clear();

                List<string> keyParam = new List<string>
                {
                    GeneralConstant.ParameterEmailSubject,
                    GeneralConstant.ParameterEmailTo,
                    GeneralConstant.ParameterEmailCc,
                    GeneralConstant.ParameterEmailBcc,
                    GeneralConstant.ParameterEmailFrom
                };
                listParam = ParameterRepository.GetMultipleParameterLvl1(keyParam);
                
                if (listParam != null && listParam.Count > 0)
                {
                    foreach (ParameterLevel1 paramLvl1 in listParam)
                    {
                        ht.Add(paramLvl1.KeyParam, paramLvl1.Value1Param);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                string errorCode = GlobalRepository.WriteException(ex, 
                    "refresh-param-email");
                error = ResponseCodeConstant.MsgInternalError.Replace("{errorCode}", 
                    errorCode);
                GlobalRepository.SendEmailNotif(errorCode, ex);
                return false;
            }
            return true;
        }

        public static bool InitGlobalParam()
        {
            List<ParameterLevel1> listParam = null;

            try
            {
                if (globalParam == null) globalParam = new Dictionary<string, object>();
                else globalParam.Clear();

                List<string> keyParam = new List<string>
                {
                    GeneralConstant.ParameterGlobalDefaultTimeout
                };
                listParam = ParameterRepository.GetMultipleParameterLvl1(keyParam);

                if (listParam != null && listParam.Count > 0)
                {
                    foreach (ParameterLevel1 paramLvl1 in listParam)
                    {
                        globalParam.Add(paramLvl1.KeyParam, paramLvl1.Value1Param);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                string errorCode = GlobalRepository.WriteException(ex, 
                    "refresh-param-global");
                error = ResponseCodeConstant.MsgInternalError + errorCode;
                GlobalRepository.SendEmailNotif(errorCode, ex);
                return false;
            }
            return true;
        }

        public static string DoRefreshParam()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();

            if (Init())
            {
                obj.Add("response_code", ResponseCodeConstant.RcSuccess);
                obj.Add("response_desc", ResponseCodeConstant.MsgSuccess);
                obj.Add("listParam", JsonSerializer.Serialize(ht));
            }
            else
            {
                obj.Add("response_code", ResponseCodeConstant.RcInternalEror);
                obj.Add("response_desc", error);
            }
            return JsonSerializer.Serialize(obj);
        }

        public static string DoRefreshParamGlobal()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();

            if (Init())
            {
                obj.Add("response_code", ResponseCodeConstant.RcSuccess);
                obj.Add("response_desc", ResponseCodeConstant.MsgSuccess);
                obj.Add("listParam", JsonSerializer.Serialize(globalParam));
            }
            else
            {
                obj.Add("response_code", ResponseCodeConstant.RcInternalEror);
                obj.Add("response_desc", error);
            }
            return JsonSerializer.Serialize(obj);
        }
    }
}
