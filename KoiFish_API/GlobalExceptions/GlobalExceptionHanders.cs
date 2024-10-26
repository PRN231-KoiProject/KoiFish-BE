using KoiFish_Core.Domain.Error;
using KoiFish_Core;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace KoiFish_API.GlobalExceptions
{
    public class GlobalExceptionHanders : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                httpContext.Response.StatusCode = contextFeature.Error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                //_logger.LogError($"Something went wrong: {exception.Message}");
                var result = new ResultModel
                {
                    Status = httpContext.Response.StatusCode,
                    Message = contextFeature.Error.Message

                };
                var options = new JsonSerializerOptions //conver to CamelCase in response
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result, options));

            }
            return true;
        }
    }
}
