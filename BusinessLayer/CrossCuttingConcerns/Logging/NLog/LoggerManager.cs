using NLog;

namespace BusinessLayer.CrossCuttingConcerns.Logging.NLog;

public class LoggerManager : ILoggerManager
{
    private static ILogger _logger = LogManager.GetCurrentClassLogger(); 
    public LoggerManager() { }

    public bool IsDebugEnabled => _logger.IsDebugEnabled;
    public bool IsErrorEnabled => _logger.IsErrorEnabled;
    public bool IsInfoEnabled => _logger.IsInfoEnabled;
    public bool IsWarnEnabled => _logger.IsWarnEnabled;
    public bool IsFatalEnabled => _logger.IsFatalEnabled;

    public void LogDebug(object message)
    {
        if (IsDebugEnabled)
            _logger.Debug("{@value}", message);
    }

    public void LogError(object message) 
    {
        if (IsErrorEnabled)
            _logger.Error("{@value}", message);
    }

    public void LogInfo(object message) 
    {
        if (IsInfoEnabled)
            _logger.Info("{@value}", message);
    }

    public void LogWarn(object message) 
    {
        if (IsWarnEnabled)
            _logger.Warn("{@value}", message);
    }

    public void LogFatal(object message)
    {
        if (IsFatalEnabled)
            _logger.Fatal("{@value}", message);
    }
}
