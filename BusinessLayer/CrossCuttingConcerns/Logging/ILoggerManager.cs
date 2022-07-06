namespace BusinessLayer.CrossCuttingConcerns.Logging;

public interface ILoggerManager
{
    bool IsDebugEnabled { get; }
    bool IsErrorEnabled { get; }
    bool IsInfoEnabled { get; }
    bool IsWarnEnabled { get; }
    bool IsFatalEnabled { get; }
    void LogDebug(object message);
    void LogError(object message); 
    void LogInfo(object message); 
    void LogWarn(object message);
    void LogFatal(object message);
}
