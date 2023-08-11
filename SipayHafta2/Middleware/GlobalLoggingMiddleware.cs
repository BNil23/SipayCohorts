namespace SipayHafta2.Middleware
{
    public class GlobalLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalLoggingMiddleware> _logger;

        public GlobalLoggingMiddleware(RequestDelegate next, ILogger<GlobalLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Middleware'ın çalıştığı yerde loglama yapabilirsiniz
                _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

                await _next(context);
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapabilirsiniz
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
