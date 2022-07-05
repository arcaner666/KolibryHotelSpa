using BusinessLayer.Constants;
using BusinessLayer.Extensions;
using BusinessLayer.Utilities.Interceptors;
using BusinessLayer.Utilities.IoC;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Aspects.Autofac.Authorization;

public class AuthorizationAspect : MethodInterception
{
    private readonly string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;

    public AuthorizationAspect(string roles)
    {
        _roles = roles.Split(',');
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
            {
                return;
            }
        }
        throw new Exception(Messages.AuthorizationDenied);
    }
}
