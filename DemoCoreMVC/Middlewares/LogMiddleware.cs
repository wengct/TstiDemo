using System.Net;

namespace DemoCoreMVC.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next; // 必要
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;  // 必要
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)  // 必要
        {
            try
            {
                _logger.LogInformation("Begin Request");
                int i = 0;
                var j = 1 / i;
                await _next(context);
                _logger.LogInformation("End Request");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
