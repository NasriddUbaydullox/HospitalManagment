using HospitalManagment_V2.classes;

namespace HospitalManagment_V2.Middleware;

public class CorreletionIdMiddleware
{

    private readonly RequestDelegate _next;
    private const string _correletionIdHeader = "X-Correletion-Id";
    public CorreletionIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICorroletionId correletionId)
    {
        var id = GetCorreletion(context, correletionId);
        AddCorreletionId(context, id);

        await _next(context);
    }

    private void AddCorreletionId(HttpContext context, string id)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add(_correletionIdHeader, new[] { id });
            return Task.CompletedTask;
        });
    }

    private string GetCorreletion(HttpContext context, ICorroletionId correletionId)
    {
        if (context.Request.Headers.TryGetValue(_correletionIdHeader, out var headers))
        {
            correletionId.Set(headers);
            return headers;
        }
        else
        {
            return correletionId.Get();
        }
    }
}
