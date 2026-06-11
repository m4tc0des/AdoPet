using AdoPet.Application.UseCases.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace AdoPet.Application;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddApplication()
        {
            services.AddScoped<IRegisterUserUseCase, IRegisterUserUseCase>();
        }
    }
}
