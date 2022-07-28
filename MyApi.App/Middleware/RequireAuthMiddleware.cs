namespace MyApi.Middleware
{
    public class RequireAuthMiddleware
    {
        // Field
        private readonly RequestDelegate _next;

        // Constructor
        public RequireAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Methods
        public async Task InvokeAsync(HttpContext context)

        {
            if (context.Request.Query["authenticated"] == "true")
            {
                // this middleware is done, let the next process take over and do it's thing"
                await _next(context);
            }
            else
            {
                // don't trigger the next, this middleware will "short-circuit" the pipeline
                // so we need to set up the response here.

                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("error, not authenticated");
            }
        }
    }
}
