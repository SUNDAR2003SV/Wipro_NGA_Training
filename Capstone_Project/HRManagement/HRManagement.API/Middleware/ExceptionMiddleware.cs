using System.Net;
using System.Text.Json;
using HRManagement.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/problem+json";

            var problem = ex switch
            {
                EmployeeNotFoundException e => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Employee not found",
                    Detail = e.Message
                },
                InvalidLeaveException e => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Invalid leave request",
                    Detail = e.Message
                },
                UnauthorizedAccessException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = "Unauthorized",
                    Detail = "You are not authorized to perform this action"
                },
                _ => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "An unexpected error occurred",
                    // ✅ Stack trace hidden in production
                    Detail = _env.IsDevelopment()
                        ? ex.Message
                        : "An internal error occurred. Please contact support."
                }
            };

            context.Response.StatusCode = problem.Status!.Value;

            var json = JsonSerializer.Serialize(problem,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            await context.Response.WriteAsync(json);
        }
    }
}