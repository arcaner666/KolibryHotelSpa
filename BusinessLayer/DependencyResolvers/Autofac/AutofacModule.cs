using Autofac;
using Autofac.Extras.DynamicProxy;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessLayer.Utilities.Interceptors;
using BusinessLayer.Utilities.Security.Cryptography;
using BusinessLayer.Utilities.Security.JWT;
using Castle.DynamicProxy;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

namespace BusinessLayer.DependencyResolvers.Autofac;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<ContactBl>().As<IContactBl>().SingleInstance();
        builder.RegisterType<DpMsContactDal>().As<IContactDal>().SingleInstance();

        builder.RegisterType<ClaimBl>().As<IClaimBl>().SingleInstance();
        builder.RegisterType<DpMsClaimDal>().As<IClaimDal>().SingleInstance();

        builder.RegisterType<CurrencyBl>().As<ICurrencyBl>().SingleInstance();
        builder.RegisterType<DpMsCurrencyDal>().As<ICurrencyDal>().SingleInstance();

        builder.RegisterType<DapperContext>().SingleInstance();

        builder.RegisterType<JwtHelper>().As<ITokenService>().SingleInstance();

        builder.RegisterType<KeyService>().As<IKeyService>().SingleInstance();

        builder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();

        builder.RegisterType<PersonAdvBl>().As<IPersonAdvBl>().SingleInstance();
        builder.RegisterType<PersonBl>().As<IPersonBl>().SingleInstance();
        builder.RegisterType<DpMsPersonDal>().As<IPersonDal>().SingleInstance();

        builder.RegisterType<PersonClaimBl>().As<IPersonClaimBl>().SingleInstance();
        builder.RegisterType<DpMsPersonClaimDal>().As<IPersonClaimDal>().SingleInstance();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
