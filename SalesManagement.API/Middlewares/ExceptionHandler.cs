using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SalesManagement.API.Models;

namespace SalesManagement.API.Middlewares;

public class ExceptionHandler
{
    public static async Task Handle(HttpContext httpContext)
    {
        var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature!.Error;

        var response = httpContext.Response;
        response.ContentType = "application/json";

        if (exception.GetType() == typeof(ConflictException))
            response.StatusCode = StatusCodes.Status409Conflict;

        var result = JsonConvert.SerializeObject(new ErrorResponse(exception.Message, exception.InnerException?.Message), new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });

        await response.WriteAsync(result);
    }
}
