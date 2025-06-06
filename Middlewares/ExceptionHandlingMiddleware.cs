using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context); // Proceed with the next middleware in the pipeline
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                ValidationException or ArgumentNullException or ArgumentException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                DbUpdateException => HttpStatusCode.InternalServerError,
                NotImplementedException => HttpStatusCode.NotImplemented,
                InvalidOperationException => HttpStatusCode.TooManyRequests,
                _ => HttpStatusCode.InternalServerError
            };

            var responseMessage = exception switch
            {
                KeyNotFoundException => exception.Message,
                ValidationException or ArgumentNullException or ArgumentException => exception.Message,
                UnauthorizedAccessException => "You are not authorized to perform this action.",
                DbUpdateException => "A database error occurred while processing your request.",
                NotImplementedException => "This feature is not yet implemented.",
                InvalidOperationException => exception.Message,
                _ => "An unexpected error occurred. Please try again later."
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                message = responseMessage
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
