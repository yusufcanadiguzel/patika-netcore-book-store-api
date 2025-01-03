namespace BookStore.WebApi.Services;

public class DatabaseLogger : ILoggerService
{
    public void Log(string message)
    {
        Console.WriteLine($"[DbLogger] {message}");
    }
}