using Microsoft.AspNetCore.Http;

public class RoleAuthorisation
{
    private readonly RequestDelegate _next;
    public RoleAuthorisation(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var role = context.Session.GetString("role");
        if (context.Request.Path.StartsWithSegments("/admin"))
        {
            if (role != "admin")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden");
                return;
            }
        }
        await _next(context);
    }
}

// Extension method to support GetString on ISession
public static class SessionExtensions
{
    public static string? GetString(this ISession session, string key)
    {
        if (!session.TryGetValue(key, out byte[]? value) || value == null)
            return null;
        return System.Text.Encoding.UTF8.GetString(value);
    }
}
