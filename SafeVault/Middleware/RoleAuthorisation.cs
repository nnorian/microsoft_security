public class RoleAuthorisation{
    private readonly RequestDelegate _next;
    public RoleAuthorisation(RequestDelegate next) => _next=next;
    
    public async Task InvokeAsync(Httpcontext context){
        var role = context.Session.GetString("role");
        if (context.Request.Path.StartsWithSegments("/admin")){
            if (role != "admin"){
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await contrxt.Response.WriteAsync("Forbidden");
                return;
            }
        }
        await _next(context);
    }
}