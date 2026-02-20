using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BuildExeServices.Controllers;


namespace BuildExeServices.Library
{
    public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token) && !TokenBlacklistController.IsTokenValid(token))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Token is invalid or expired.");
            return;
        }

        await _next(context);
    }
}

}
