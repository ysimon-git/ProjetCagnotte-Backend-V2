namespace ProjetCagnotte.API.Middlewares
{
    using System.Net;
    using System.Text.Json;


    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(
                    context,
                    ex,
                    HttpStatusCode.BadRequest);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(
                    context,
                    ex,
                    HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");

                await HandleExceptionAsync(
                    context,
                    ex,
                    HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = statusCode == HttpStatusCode.InternalServerError
                    ? "Internal error occurs."
                    : exception.Message
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
