using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Numerics;
using System.Security.Cryptography;

namespace InqService.Repository
{
    public class SecurityCheck
    {
        public static string GenerateRequest(string jsonRequest)
        {
            string date = DateTime.Now.ToString("ddMMyyyy");

            Dictionary<string, object> unsortMap = GetAllStringJson(jsonRequest);
            unsortMap.Add("request_time", DateTime.Now.ToString());

            Dictionary<string, object> obj = JsonSerializer
                .Deserialize<Dictionary<string, object>>(jsonRequest);
            obj.Add("request_time", DateTime.Now.ToString());

            string value = "";
            SortedDictionary<string, object> treeMap = new 
                SortedDictionary<string, object>(unsortMap);

            //Cretate signature from all json value
            foreach (KeyValuePair<string, object> t in treeMap)
            {
                string key = t.Key;
                value += treeMap[key].ToString();
            }

            obj.Add("signature", EncryptThisString(value + date));
            return JsonSerializer.Serialize(obj);
        }

        public static bool VerifyRequest(string jsonRequest)
        {
            try
            {
                Dictionary<string, object> unsortMap = new Dictionary<string, object>();
                Dictionary<string, object> obj = JsonSerializer
                    .Deserialize<Dictionary<string, object>>(jsonRequest);
                string signature = obj["signature"].ToString();
                obj.Remove("signature");

                foreach (var o in obj)
                {
                    unsortMap.Add(o.Key, o.Value.ToString());
                }

                string value = "";
                SortedDictionary<string, object> treeMap = 
                    new SortedDictionary<string, object>(unsortMap);
                foreach (KeyValuePair <string, object> t in treeMap)
                {
                    string key = t.Key;
                    value += treeMap[key];
                }
                string date = DateTime.Now.ToString("ddMMyyyy");
                return signature.Equals(EncryptThisString(value + date));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static Dictionary<string, object> GetAllStringJson(string jsonRequest)
        {
            if (jsonRequest.StartsWith("{") && jsonRequest.EndsWith("}"))
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(
                    jsonRequest);
            } 
            else if (jsonRequest.StartsWith("[")  && jsonRequest.EndsWith("]"))
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                List<object> data = JsonSerializer.Deserialize<List<object>>(
                    jsonRequest);
                result.Add("data", data);
                return result;
            }
            return new Dictionary<string, object>();
        }

        public static string EncryptThisString(string input)
        {
            try
            {
                SHA256 md = SHA256.Create();
                MD5 md1 = MD5.Create();
                SHA256 md2 = SHA256.Create();

                byte[] messageDigest = md.ComputeHash(md1.ComputeHash(md2.ComputeHash(
                    Encoding.ASCII.GetBytes(input))));

                BigInteger no = new BigInteger(messageDigest);

                string hashtext = no.ToString("X");

                while (hashtext.Length < 32) hashtext = "0" + hashtext;

                return hashtext;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
