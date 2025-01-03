namespace BookStore.WebApi.Services;

public class ConsoleLogger : ILoggerService
{
    public void Log(string message)
    {
        System.Console.WriteLine($"[ConsoleLogger] {message}");
    }
}