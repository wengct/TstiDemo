namespace DemoCoreMVC.Middlewares
{
    public class LogMiddleware2
    {
        private readonly RequestDelegate _next; // 必要
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware2(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;  // 必要
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)  // 必要
        {
            _logger.LogInformation("Begin Request 2");
            await _next(context);
            _logger.LogInformation("End Request 2");
        }
    }
}
