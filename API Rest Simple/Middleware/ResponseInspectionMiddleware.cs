using System.IO;
using System.Threading.Tasks;
using API_Rest_Simple.Helpers.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API_Rest_Simple.Middleware
{
    public class ResponseInspectionMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseInspectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            var logService = context.RequestServices.GetRequiredService<Logger>();
            try
            {
                await _next(context);
                context.Response.Body = originalBodyStream;             

                if (context.Response.StatusCode >= 400)
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                    logService.LogError($"Response status code: {context.Response.StatusCode}");
                    logService.LogError($"Response body: {responseBodyText}");

                    // Reset the position of the response body to 0 before copying to the original stream
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
                else
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                    logService = context.RequestServices.GetRequiredService<Logger>();
                    logService.Log($"{context.Response.StatusCode.ToString()}  - good request");
                }
            }
            catch (Exception ex)
            {
                logService.LogError($"Error - {ex.Message}");
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}