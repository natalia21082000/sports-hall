using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SportsHall.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var env = context.RequestServices.GetRequiredService<IHostEnvironment>();

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string title = "Произошла внутренняя ошибка сервера.";
            string detail = exception.ToString();

            switch (exception)
            {
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    title = "Доступ запрещён.";
                    break;
                case ArgumentException _:
                case InvalidOperationException _:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Ошибка запроса.";
                    detail = exception.Message;
                    break;

                case KeyNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    title = "Ресурс не найден.";
                    break;

                default:
                    // Для остальных случаев остаётся 500 Internal Server Error
                    break;
            }
            _logger.LogError(exception, "Необработанное исключение: {Message}", exception.Message);

            context.Response.StatusCode = (int)statusCode;

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = (int)statusCode,
                Detail = env.IsDevelopment() ? detail : null,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = (int)statusCode;
            // Возвращаем клиенту JSON с ошибкой
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}