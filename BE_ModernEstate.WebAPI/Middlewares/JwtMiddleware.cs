using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppEcommerce_WebApp.WebAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            _logger.LogInformation($"Processing request for path: {path}");

            if (path?.StartsWith("/public") == true)
            {
                _logger.LogInformation("Public endpoint, skipping JWT validation");
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            string token = null;

            if (!string.IsNullOrEmpty(authHeader))
            {
                // Check if it's a Bearer token
                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = authHeader.Substring("Bearer ".Length).Trim();
                }
                else
                {
                    // If not a Bearer token, try to use the raw value
                    token = authHeader.Trim();
                }
            }

            if (token != null)
            {
                _logger.LogInformation("JWT token found in request");
                AttachUserToContext(context, token);
            }
            else
            {
                _logger.LogWarning("No JWT token found in request");
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
                _logger.LogInformation("Attempting to validate JWT token");

                var validationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = false,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken)
                {
                    _logger.LogError("Invalid JWT token format");
                    throw new SecurityTokenException("Invalid JWT token");
                }

                _logger.LogInformation($"JWT token validated successfully. User roles: {string.Join(", ", principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value))}");
                context.User = principal;
            }
            catch (SecurityTokenExpiredException)
            {
                _logger.LogError("JWT token has expired");
                throw;
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError($"JWT token validation failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in JWT Middleware: {ex.Message}");
                throw;
            }
        }
    }
}
