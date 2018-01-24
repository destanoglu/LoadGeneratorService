using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LoadGeneratorService.Middleware
{
    public abstract class Middleware
    {
        protected readonly RequestDelegate _next;

        protected Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            BeforeRequest(context);
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                OnError(context);
            }
            AfterRequest(context);
        }

        protected abstract void BeforeRequest(HttpContext context);
        protected abstract void AfterRequest(HttpContext context);
        protected abstract void OnError(HttpContext context);
    }
}
