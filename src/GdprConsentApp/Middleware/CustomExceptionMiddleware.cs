using System.Net;
using GdprConsentApp.Exceptions;
using GdprConsentApp.Models;

namespace GdprConsentApp.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    // private readonly ILoggerManager _logger;
    public CustomExceptionMiddleware(RequestDelegate next)
    // public CustomExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
    {
        // _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BaseException ex)
        {
            // _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, BaseException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsync(new ErrorDetailsModel
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
        }.ToString());
    }
}