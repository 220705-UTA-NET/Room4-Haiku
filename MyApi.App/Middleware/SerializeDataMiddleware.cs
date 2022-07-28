using System.Text.Json;

namespace MyApi.Middleware
{
    public class SerializeDataMiddleware
    {
      //  private static readonly JsonSerializer serialize;
        private readonly RequestDelegate _next;

        public SerializeDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response.ContentType == "text/plain")
            {
                context.Response.ContentType = "application/json";
      //          context.Response.WriteAsync(serialize.Serialize(context.Response.Body));
            }
            else
            {
                await _next(context);
            }
        }
    }
}
