using Microsoft.Extensions.Logging;

public static class ApplicationLogging
{
    public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory().AddConsole();
    public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
}