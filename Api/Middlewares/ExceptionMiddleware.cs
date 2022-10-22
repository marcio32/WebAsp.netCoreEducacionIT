using Api.Dtos;
using Commons.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await GenerateLogHelper.LogError(ex, "ExceptionMiddleware", ex.TargetSite.Name);
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;


            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetailsDto
            {
                StackTrace = ex.StackTrace,
                Message = ex.Message,
                StatusCodes = StatusCodes.Status406NotAcceptable
            }));
        }
    }
}
