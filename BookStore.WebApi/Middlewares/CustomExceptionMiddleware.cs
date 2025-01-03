using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;
using BookStore.WebApi.Services;
using Newtonsoft.Json;

namespace BookStore.WebApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";

            _logger.Log(message);

            await _next(context);

            stopwatch.Stop();

            message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {stopwatch.Elapsed.TotalMilliseconds} ms";

            _logger.Log(message);
        }
        catch (Exception exception)
        {
            stopwatch.Stop();

            await HandleException(context, exception, stopwatch);
        }
    }

    private Task HandleException(HttpContext context, Exception exception, Stopwatch stopwatch)
    {
        var message = $"[Error] HTTP {context.Request.Method} - {context.Response.StatusCode} Error Message: {exception.Message} in  {stopwatch.Elapsed.TotalMilliseconds} ms.";

        _logger.Log(message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new {error = exception.Message}, Formatting.None);

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}