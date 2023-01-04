using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Blog
{
    public class HttpRequestBodyMiddleware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public HttpRequestBodyMiddleware(ILogger<HttpRequestBodyMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //context.Request.EnableBuffering();

            //var reader = new StreamReader(context.Request.Body);

            //string body = await reader.ReadToEndAsync();
            //logger.LogInformation(
            //    $"Request {context.Request?.Method}: {context.Request?.Path.Value}\n{body}");

            //context.Request.Body.Position = 0L;

            //await next(context);

            try
            {
                await next(context);
            }
            finally
            {
                if (!context.Request.Path.Value.Contains("lib") && !context.Request.Path.Value.Contains("js") && !context.Request.Path.Value.Contains("css"))
                logger.LogInformation(
                    "Requestos {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
