using BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessLayer.CrossCuttingConcerns.Logging.NLog;
using BusinessLayer.Utilities.Interceptors;
using Castle.DynamicProxy;

namespace BusinessLayer.Aspects.Autofac.Logging;

public class LoggingAspect : MethodInterception
{
    private readonly Type _loggerManagerType;
    public LoggingAspect(Type loggerManagerType)
    {
        if (!typeof(LoggerManager).IsAssignableFrom(loggerManagerType))
        {
            throw new Exception("Bu bir loglama sınıfı değil!");
        }

        _loggerManagerType = loggerManagerType;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var loggerManager = (LoggerManager)Activator.CreateInstance(_loggerManagerType);
        var logMethodParameters = invocation.Method.GetParameters().Select((p, i) => new LogMethodParameter()
        {
            Name = p.Name,
            Type = p.ParameterType.Name,
            Value = invocation.GetArgumentValue(i),
        });
        //loggerManager.LogInfo(logMethodParameters);
        loggerManager.LogInfo(invocation.GetArgumentValue(1));
        //var parameters = invocation.Arguments.Where(t => t.GetType() == entityType);
        //if (loggerManager.IsInfoEnabled)
        //{
        //}
    }

    //protected override void OnException(IInvocation invocation, Exception exception) 
    //{
    //    var loggerManager = (ILoggerManager)Activator.CreateInstance(_loggerManagerType);
    //    loggerManager.LogError(exception.Message);
    //}
}
