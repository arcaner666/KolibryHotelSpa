using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Utilities.IoC;

public interface ICoreModule
{
    void Load(IServiceCollection serviceCollection);
}
