
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace HospitalManagment_V2.Middleware;

public class ConfigurationMiddleware : IMiddleware
{

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            return next(context);
        }
        catch (OptionsValidationException ex)
        {
            throw new BadHttpRequestException($"Error: {ex.Failures }");
        }
    }
}
