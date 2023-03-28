using Microsoft.AspNetCore.Http.HttpResults;
using RideSharing.Entity;

namespace RideSharing.API
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
            catch (CustomException ex)
            {
                context.Response.StatusCode = ex.Status;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync((new { Message = ex.Message, Status = ex.Status}).ToString());
            }
        }
    }
}
