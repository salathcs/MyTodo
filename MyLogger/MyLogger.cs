using MyLogger.Interfaces;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

namespace MyLogger
{
    public class MyLogger : IMyLogger
    {
        private ILogger logger;

        public MyLogger()
        {
            logger = Log.Logger;
        }

        public void Verbose(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var extendedMessage = CreateExtendedMessage(message, callerFilePath, callerMemberName, callerLineNumber);

            Logging(LogEventLevel.Verbose, extendedMessage, exception);
        }

        public void Debug(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var extendedMessage = CreateExtendedMessage(message, callerFilePath, callerMemberName, callerLineNumber);

            Logging(LogEventLevel.Debug, extendedMessage, exception);
        }

        public void Info(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var extendedMessage = CreateExtendedMessage(message, callerFilePath, callerMemberName, callerLineNumber);

            Logging(LogEventLevel.Information, extendedMessage, exception);
        }

        public void Warn(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var extendedMessage = CreateExtendedMessage(message, callerFilePath, callerMemberName, callerLineNumber);

            Logging(LogEventLevel.Warning, extendedMessage, exception);
        }

        public void Error(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var extendedMessage = CreateExtendedMessage(message, callerFilePath, callerMemberName, callerLineNumber);

            Logging(LogEventLevel.Error, extendedMessage, exception);
        }

        public void Fatal(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var extendedMessage = CreateExtendedMessage(message, callerFilePath, callerMemberName, callerLineNumber);

            Logging(LogEventLevel.Fatal, extendedMessage, exception);
        }

        private void Logging(LogEventLevel level, string message, Exception? exception)
        {
            if (!logger.IsEnabled(level))
            {
                return;
            }

            if (exception is null)
            {
                logger.Write(level, message);
            }
            else
            {
                logger.Write(level, exception, message);
            }
        }

        private string CreateExtendedMessage(string message, string? callerFilePath, string? callerMemberName, int callerLineNumber)
        {
            try
            {
                var className = Path.GetFileNameWithoutExtension(callerFilePath);

                return $"{className}.{callerMemberName} at {callerLineNumber}: {message}";
            }
            catch
            {
                return $"Unknown - {message}";
            }
        }
    }
}
