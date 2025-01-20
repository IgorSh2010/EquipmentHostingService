namespace NewWebApplication2.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-Api-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            // Czytamy API Key z konfiguracji
            var apiKey = configuration["ApiSettings:ApiKey"];

            // Sprawdzamy obecność nagłówka X-Api-Key
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }

            // Porównujemy kluczy
            if (!string.Equals(extractedApiKey, apiKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            // Przekazujemy kontrolę kolejnemu oprogramowaniu middleware
            await _next(context);
        }
    }
}
