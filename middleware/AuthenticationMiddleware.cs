namespace UserManagementApi.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow Swagger without authentication
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            // Allow Swagger JSON without authentication
            if (context.Request.Path.StartsWithSegments("/openapi"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(
                    "X-API-Key",
                    out var apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "API key is required"
                });

                return;
            }

            if (apiKey != "user-manage-secret-key")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Invalid API key"
                });

                return;
            }

            await _next(context);
        }
    }
}