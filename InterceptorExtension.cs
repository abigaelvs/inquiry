using Microsoft.AspNetCore.Builder;
using System;

namespace InqService
{
    public static class InterceptorExtension
    {
        public static IApplicationBuilder UseVerifySignature(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Interceptor>();
        }
    }
}
