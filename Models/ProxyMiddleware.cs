using System.Runtime.CompilerServices;

namespace EntityFrameworkUI.Models
{
    public class ProxyMiddleware
    {
        private readonly RequestDelegate _next;
        

        public ProxyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("from"))
            {
                httpContext.Request.Headers.Add("from", "react");
                
                //if (httpContext.Request.Headers.TryGetValue("token", out var token))
                //{
                //    httpContext.Request.Headers["Authorization"] = $"Bearer {token}";
                //}
            }



            await _next(httpContext);
        }
    }
}
