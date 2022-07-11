namespace Eatagram.Core.Api.Middlewares
{
    public class AccessControlMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            
            context.Response.OnStarting((Func<Task>)(() => {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,PATCH,OPTIONS");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, x-requested-with, x-signalr-user-agent");
                context.Response.StatusCode = 200;
                context.Response.WriteAsJsonAsync($"SomethingNotWorking");
                return Task.CompletedTask;
            }));
            try
            {
                await this._next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
            }
        }
    }
}
