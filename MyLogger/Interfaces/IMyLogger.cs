using System.Runtime.CompilerServices;

namespace MyLogger.Interfaces
{
    public interface IMyLogger
    {
        void Verbose(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0);
        void Debug(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0);
        void Info(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0);
        void Warn(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0);
        void Error(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0);
        void Fatal(string message, Exception? exception = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0);
    }
}
