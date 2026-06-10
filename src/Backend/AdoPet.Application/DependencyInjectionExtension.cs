using Microsoft.Extensions.DependencyInjection;

namespace AdoPet.Application;

public class DependencyInjectionExtension
{
    public void AddApplication(IServiceCollection services)
    {
        services.AddScoped<>();
    }
}
