using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace backend.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this._requestDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }catch(ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = new { Message = "validation failed", Errors = ex.Errors };
                await context.Response.WriteAsJsonAsync(response);
            }catch(Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { Message = "An error occured", Details = ex.Message };
                await context.Response.WriteAsJsonAsync(response);

            }

        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
           return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
