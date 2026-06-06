namespace HRManagement.API.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Prevent MIME sniffing
            context.Response.Headers.Append(
                "X-Content-Type-Options", "nosniff");

            // Prevent clickjacking
            context.Response.Headers.Append(
                "X-Frame-Options", "DENY");

            // Disable legacy XSS filter (CSP handles this now)
            context.Response.Headers.Append(
                "X-XSS-Protection", "0");

            // Control referrer information
            context.Response.Headers.Append(
                "Referrer-Policy", "strict-origin-when-cross-origin");

            await _next(context);
        }
    }
}