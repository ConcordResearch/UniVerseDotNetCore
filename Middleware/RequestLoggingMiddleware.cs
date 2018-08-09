using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniVerseDotNetCore.Middleware
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            var injectedRequestStream = new MemoryStream();
            try
            {
                context.Request.EnableRewind();

                var requestLog = $"REQUEST HttpMethod: {context.Request.Method}, Path: {context.Request.Path}";

                using (var bodyReader = new StreamReader(context.Request.Body))
                {
                    var bodyAsText = bodyReader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(bodyAsText) == false)
                    {
                        var converted = "";
                        try
                        {
                            converted = JValue.Parse(bodyAsText).ToString(Formatting.Indented);
                        }
                        catch (Exception e)
                        {
                            converted = $"{e.Message}";
                        }
                        requestLog += $", Body : {converted}";
                    }

                    var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    context.Request.Body = injectedRequestStream;
                }

                _logger.LogDebug(requestLog);

                await _next.Invoke(context);
 

            }
            finally
            {
                injectedRequestStream.Dispose();
                
            }
        }
    }


}