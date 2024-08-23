using log4net;
using System.Net;

namespace SB.PruebaTecnica.API
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetLogger(typeof(ErrorLoggingMiddleware));
        }

        public async Task InvokeAsync(HttpContext context)
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
            _logger.Error("Ha ocurrido un error Inesperado en el servidor", exception);

            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Ha ocurrido un error Inesperado en el servidor",
                exception = exception.Message
            });
            return context.Response.WriteAsync(result);
        }
    }
}
