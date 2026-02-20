using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BuildExeHR
{
    public class ModelStateMiddleware
    {
        private readonly RequestDelegate _next;

        public ModelStateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log incoming request body
            var requestBodyText = await ReadRequestBody(context);
            Logger.InfoLog("ModelStateMiddleware", "IncomingRequest", $"Payload: {requestBodyText}");

            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context); // Call the next middleware (Controller)

                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseText = await new StreamReader(responseBody, Encoding.UTF8).ReadToEndAsync();

                    if (context.Response.StatusCode == 400)
                    {
                        Logger.InfoLog("ModelStateMiddleware", "400Error", $"Validation Errors: {responseText}");
                    }

                    // Reset the response body stream before sending it to the client
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog("ModelStateMiddleware", "Invoke", ex);
                throw; // Ensure exceptions propagate correctly
            }
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    string body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; // Reset stream position for the controller to read it
                    return body;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog("ModelStateMiddleware", "ReadRequestBody", ex);
                return "Failed to read request body.";
            }
        }
    }
}
