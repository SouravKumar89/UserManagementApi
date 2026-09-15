namespace UserManagementApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(
            RequestDelegate next,
            ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            _logger.LogInformation(
                "Request started: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await _next(context);

            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation(
                "Request completed: {StatusCode} in {Duration} ms",
                context.Response.StatusCode,
                duration.TotalMilliseconds);
        }
    }
}