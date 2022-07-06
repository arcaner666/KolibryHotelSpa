using BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessLayer.Utilities.Interceptors;
using Castle.DynamicProxy;

namespace BusinessLayer.Aspects.Autofac.Logging;

public class LoggingAspect : MethodInterception
{
    private readonly Type _loggerManagerType;
    public LoggingAspect(Type loggerManagerType)
    {
        if (!typeof(ILoggerManager).IsAssignableFrom(loggerManagerType))
        {
            throw new Exception("Bu bir loglama sınıfı değil!");
        }

        _loggerManagerType = loggerManagerType;
    }

    public override void OnBefore(IInvocation invocation)
    {
        var loggerManager = (ILoggerManager)Activator.CreateInstance(_loggerManagerType);
        var methodParameterTypes = invocation.Method.GetGenericArguments()
        var parameters = invocation.Arguments.Where(t => t.GetType() == entityType);
        if (loggerManager.IsInfoEnabled)
        {
            loggerManager.LogError();
        }
    }
    public override void OnException(IInvocation invocation, Exception exception) 
    {
        var _loggerManager = (ILoggerManager)Activator.CreateInstance(_loggerManagerType);
        _loggerManager.LogError(exception.Message);
    }
}
