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

            await _next(context);
        }

    }
}
