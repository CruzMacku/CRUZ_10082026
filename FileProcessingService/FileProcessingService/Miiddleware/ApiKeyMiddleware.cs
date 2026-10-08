using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace FileProcessingService.Miiddleware
{
    public class ApiKeyMiddleware
    {
        IConfiguration _configuration;

        RequestDelegate _next;
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-API-Key"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API key is required.");
                return;
            }

            var _config = _configuration["ApiKey"];
            var key = context.Request.Headers["X-API-Key"];

            if (key != _config)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }

    }
}
