using Newtonsoft.Json;
using Serilog;

namespace SunService.EndPoints.Mvc.Task.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async global::System.Threading.Tasks.Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred: {ex.Message}";

                Log.Error(ex, message);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = message,
                }));
            }
        }
    }

    public static class Extensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
            return app;
        }
    }
}

