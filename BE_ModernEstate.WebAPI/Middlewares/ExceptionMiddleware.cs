using Microsoft.IdentityModel.Tokens;
using ModernEstate.Common.Models.ApiResponse;
using System.Text.Json;

namespace BE_ModernEstate.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
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
                if (context.Response.StatusCode == 401)
                {
                    await HandleUnauthorizedResponse(context);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleUnauthorizedResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse
            {
                Code = StatusCodes.Status401Unauthorized,
                Success = false,
                Message = "Unauthorized access. Please provide valid authentication credentials.",
                Data = null
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            context.Response.ContentType = "application/json";
            var response = new ApiResponse();

            switch (ex)
            {
                case SecurityTokenExpiredException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Code = StatusCodes.Status401Unauthorized;
                    response.Message = "Token has expired. Please login again.";
                    break;

                case SecurityTokenInvalidAudienceException:
                case SecurityTokenInvalidIssuerException:
                case SecurityTokenInvalidSigningKeyException:
                case SecurityTokenValidationException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Code = StatusCodes.Status401Unauthorized;
                    response.Message = "Invalid authentication token.";
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Code = StatusCodes.Status401Unauthorized;
                    response.Message = "You are not authorized to access this resource.";
                    break;

                case ArgumentException:
                case InvalidOperationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Code = StatusCodes.Status400BadRequest;
                    response.Message = ex.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Code = StatusCodes.Status500InternalServerError;
                    response.Message = _env.IsDevelopment()
                        ? $"Internal Server Error: {ex.Message}"
                        : "An unexpected error occurred. Please try again later.";
                    break;
            }

            response.Success = false;
            if (_env.IsDevelopment())
            {
                response.Data = new
                {
                    Exception = ex.GetType().Name,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
