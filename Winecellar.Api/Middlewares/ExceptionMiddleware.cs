using Microsoft.Extensions.Logging.Console;
using System.Data;
using System.Net;
using System.Text.Json;

namespace Winecellar.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                context.Response.StatusCode,
                exception.Message,
                Details = exception.InnerException?.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
