using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniVerseDotNetCore.Middleware
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;
        //private Func _defaultFormatter = (state, exception) => state;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            try
            {
                var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                await _next(context);

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
                _logger.LogDebug($"RESPONSE LOG: {JValue.Parse(responseBody).ToString(Formatting.Indented)}");
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(bodyStream);

            }
            finally
            {
                bodyStream.Dispose();
            }
        }
    }


}