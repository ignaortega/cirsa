using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SocialGames.TechnicalTest.ApiService.MIddleware
{
    public class RequestToResponseTimeLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public RequestToResponseTimeLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<RequestToResponseTimeLoggerMiddleware> logger)
        {
            try
            {
                _logger = logger;

                var originalRequest = httpContext.Request;
                if (originalRequest.Path.StartsWithSegments(new PathString("/api")))
                {
                    var stopWatch = Stopwatch.StartNew();
                    var requestTime = DateTime.UtcNow;

                    var originalRequestBodyContent = await ReadRequestBody(originalRequest);
                    var originalBodyStream = httpContext.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {
                        var response = httpContext.Response;
                        response.Body = responseBody;

                        await _next(httpContext);
                        
                        stopWatch.Stop();

                        string responseBodyContent = string.Empty;

                        responseBodyContent = await ReadResponseBody(response);
                        
                        await responseBody.CopyToAsync(originalBodyStream);

                        _logger.LogInformation("{apiPath} {executionTime}", originalRequest.Path, stopWatch.Elapsed);
                    }
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                await _next(httpContext);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}
