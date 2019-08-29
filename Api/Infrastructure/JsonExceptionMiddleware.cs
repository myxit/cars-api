using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AntilopaApi.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            { 
                // TODO: add exception logging here
                //  _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (_env.IsDevelopment()) {
                return context.Response.WriteAsync(JsonConvert.SerializeObject(new { StatusCode = context.Response.StatusCode, Message = exception.Message, Stack = exception.StackTrace }));
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { StatusCode = context.Response.StatusCode, Message = exception.Message }));
        }
    }
}