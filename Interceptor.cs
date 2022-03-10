using Microsoft.AspNetCore.Http;
using Microsoft.IO;

using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

using InqService.Constants;
using InqService.Model;
using InqService.Repository;

namespace InqService
{
    public class Interceptor
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        
        public Interceptor(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            DateTime requestTime = DateTime.Now;

            //Ensure request body can read multiple times
            context.Request.EnableBuffering();

            //Copy request body to stream
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            //Read request body
            string requestMsg = ReadStreamInChunks(requestStream);
            string responseMsg = "";

            //Set stream position back to 0
            context.Request.Body.Position = 0;

            bool isValid = false;
            //Check request signature is valid or ont
            if (requestMsg != null && requestMsg != "")
                isValid = SecurityCheck.VerifyRequest(requestMsg);

            //Get response body
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            if (!isValid)
            {
                //Create response object for return
                StandardResponse resp = new StandardResponse
                {
                    ResponseCode = ResponseCodeConstant.RcException,
                    ResponseDesc = ResponseCodeConstant.MsgInvalidSignature,
                    ResponseDate = DateTime.Now,
                    ReqDate = requestTime,
                };
                //Convert response object to json string
                string respJson = JsonSerializer.Serialize(resp);
                responseMsg = respJson;

                //Create log object
                Dictionary<string, object> log = new Dictionary<string, object>();
                log.Add("requestMsg", requestMsg.Replace("{\u0022}", ""));
                log.Add("requestTime", requestTime);
                log.Add("responseMsg", JsonSerializer.Serialize(resp));
                log.Add("responseTime", DateTime.Now);
                log.Add("serviceType", "I");
                Console.WriteLine($"\n\nLOG>>{JsonSerializer.Serialize(log)}");

                //Set request status to Bad Request
                context.Response.StatusCode = 400;

                //Set response body to error
                var respData = Encoding.UTF8.GetBytes(respJson);
                Stream newBody = new MemoryStream(respData);
                await newBody.CopyToAsync(originalBodyStream);
            } 
            else
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                //Set response body to original
                await responseBody.CopyToAsync(originalBodyStream);

                string serviceName = context.Request.Host.ToString();
                string serviceFrom = "-";
                string traceId = "";

                if (requestMsg.StartsWith("{") && requestMsg.EndsWith("}"))
                {
                    Dictionary<string, object> jObject = JsonSerializer
                    .Deserialize<Dictionary<string, object>>(requestMsg);
                    if (jObject.ContainsKey("serviceFrom"))
                        serviceFrom = jObject["serviceFrom"].ToString();
                    if (jObject.ContainsKey("traceId"))
                        traceId = jObject["traceId"].ToString();
                }

                Dictionary<string, object> log = new Dictionary<string, object>();
                log.Add("requestMsg", requestMsg.Replace("\u0022", ""));
                log.Add("requestTime", requestTime);
                log.Add("responseMsg", responseMsg);
                log.Add("responseTime", DateTime.Now
                    .ToString("yyyy-MM-dd HH:mm:ss.SSS"));
                log.Add("serviceName", serviceName);
                log.Add("serviceType", "I");
                log.Add("serviceFrom", serviceFrom);
                log.Add("traceId", traceId);
                Console.WriteLine($"\n\nLOG>>{JsonSerializer.Serialize(log)}");
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}
