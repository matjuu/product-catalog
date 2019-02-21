using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Views;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Catalog.API.Host.Middlewares
{
    public class ErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException exception)
            {
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;
                    httpContext.Response.Headers.Add("Content-Type", "application/json");
                    return Task.CompletedTask;
                }, context);


                var errorResponse = new Error
                {
                    Message = exception.Message,
                    Code = exception.Code,
                    Properties = exception.Properties
                };

                context.Response.StatusCode = (int) exception.StatusCode;

                await WriteResponseToBodyAsync(context, errorResponse);
            }
            catch (Exception)
            {
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;
                    httpContext.Response.Headers.Add("Content-Type", "application/json");
                    return Task.CompletedTask;
                }, context);


                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await WriteResponseToBodyAsync(context, new Error{Code = "InternalServerError", Message = "Internal server error. Please contact a developer."});
            }

        }

        private async Task WriteResponseToBodyAsync(HttpContext context, Error errorResponse)
        {
            await context.Response.Body.FlushAsync();
            using (var writer = new StreamWriter(context.Response.Body))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
