using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace RideSharing.Common.Middlewares
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
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var responseObj = new { message = ex.Message, status = 500 };
                var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseObj));
                await context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

                //if i call _next() to pass the execution in the pipeline, response body is lost, why??
                //await _next(context);
            }
        }
    }
}