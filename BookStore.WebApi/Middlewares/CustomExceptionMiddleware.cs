using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;
using Newtonsoft.Json;

namespace BookStore.WebApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";

            System.Console.WriteLine(message);

            await _next(context);

            stopwatch.Stop();

            message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {stopwatch.Elapsed.TotalMilliseconds} ms";

            System.Console.WriteLine(message);
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

        Console.WriteLine(message);

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