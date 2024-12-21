using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Winecellar.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;  
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
            _logger.LogError(exception, "An unhandled exception occurred while processing the request.");
            HttpStatusCode code = HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(
                  new
                  {
                      error = "Something went wrong. Please try again.",
                  });

            switch (exception)
            {
                case ValidationException:
                    code = HttpStatusCode.BadRequest;

                    result = JsonSerializer.Serialize(
                       new
                       {
                           error = exception.Message
                       });

                    _logger.LogError(exception, "BadRequestException");
                    break;

                default:
                    _logger.LogError(exception, "InternalServerError");
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
