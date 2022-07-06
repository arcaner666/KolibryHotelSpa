using Autofac;
using Autofac.Extras.DynamicProxy;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessLayer.CrossCuttingConcerns.Logging.NLog;
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

        builder.RegisterType<ContactFormBl>().As<IContactFormBl>().SingleInstance();
        builder.RegisterType<DpMsContactFormDal>().As<IContactFormDal>().SingleInstance();

        builder.RegisterType<ClaimBl>().As<IClaimBl>().SingleInstance();
        builder.RegisterType<DpMsClaimDal>().As<IClaimDal>().SingleInstance();

        builder.RegisterType<CurrencyAdvBl>().As<ICurrencyAdvBl>().SingleInstance();
        builder.RegisterType<CurrencyBl>().As<ICurrencyBl>().SingleInstance();
        builder.RegisterType<DpMsCurrencyDal>().As<ICurrencyDal>().SingleInstance();

        builder.RegisterType<DapperContext>().SingleInstance();

        builder.RegisterType<JwtHelper>().As<ITokenService>().SingleInstance();

        builder.RegisterType<KeyService>().As<IKeyService>().SingleInstance();

        builder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();

        builder.RegisterType<PayTrBl>().As<IPayTrBl>().SingleInstance();

        builder.RegisterType<PersonAdvBl>().As<IPersonAdvBl>().SingleInstance();
        builder.RegisterType<PersonBl>().As<IPersonBl>().SingleInstance();
        builder.RegisterType<DpMsPersonDal>().As<IPersonDal>().SingleInstance();

        builder.RegisterType<PersonClaimBl>().As<IPersonClaimBl>().SingleInstance();
        builder.RegisterType<DpMsPersonClaimDal>().As<IPersonClaimDal>().SingleInstance();

        builder.RegisterType<InvoiceBl>().As<IInvoiceBl>().SingleInstance();
        builder.RegisterType<DpMsInvoiceDal>().As<IInvoiceDal>().SingleInstance();

        builder.RegisterType<InvoiceDetailBl>().As<IInvoiceDetailBl>().SingleInstance();
        builder.RegisterType<DpMsInvoiceDetailDal>().As<IInvoiceDetailDal>().SingleInstance();

        builder.RegisterType<SuiteBl>().As<ISuiteBl>().SingleInstance();
        builder.RegisterType<DpMsSuiteDal>().As<ISuiteDal>().SingleInstance();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
