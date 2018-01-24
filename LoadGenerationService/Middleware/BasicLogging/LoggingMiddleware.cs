using System;
using Microsoft.AspNetCore.Http;

namespace LoadGeneratorService.Middleware.BasicLogging
{
    public class LoggingMiddleware : Middleware
    {
        public LoggingMiddleware(RequestDelegate next) : base(next)
        {
        }

        protected override void BeforeRequest(HttpContext context)
        {
            var host = context.Request.Host;
            var path = context.Request.Path;

            Console.WriteLine($"Request : {host + path}");
        }

        protected override void AfterRequest(HttpContext context)
        {
            Console.WriteLine($"Response : {context.Response.StatusCode}");
        }

        protected override void OnError(HttpContext context)
        {
        }
    }
}
