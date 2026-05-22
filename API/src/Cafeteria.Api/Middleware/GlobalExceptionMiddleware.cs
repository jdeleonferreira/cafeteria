using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Cafeteria.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problem = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                title = "An unexpected error occurred.",
                status = context.Response.StatusCode,
                detail = ex.Message
            };

            var payload = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(payload);
        }
    }
}
