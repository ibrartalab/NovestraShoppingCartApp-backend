using System.Net;
using System.Text.Json;

namespace NShoppingCart.Api.Middlewars
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string requestBody = null;
            string callerMethod = GetCallerMethod();
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            try
            {

                await _next(context);
            }
            catch (Exception ex)
            {

                var endpoint = context.Request.Path;
                var requestMethod = context.Request.Method;

                // Log error with masked payload and exception details
                var logMessage = new
                {
                    Message = "Unhandled exception occurred.",
                    HttpMethod = requestMethod,
                    Endpoint = endpoint,
                    CallerMethod = callerMethod,
                    MaskedPayload = requestBody,
                    Exception = ex.Message
                };


                await HandleExceptionAsync(context, ex, body, requestMethod, endpoint);
            }
        }

        private static string GetCallerMethod()
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var frames = stackTrace.GetFrames();

            var method = frames?
                .Select(f => f.GetMethod())
                .FirstOrDefault(m => m != null && m.DeclaringType?.Namespace != "Microsoft.AspNetCore");

            return method != null ? $"{method.DeclaringType?.FullName}.{method.Name}" : "Unknown Caller";
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception exception, string payload, string method, string endpoint)
        {
            var response = new
            {
                message = "An unexpected error occurred.",
                details = exception.Message,
                httpMethod = method,
                endpoint = endpoint,
                payload = payload ?? "<none>"
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}