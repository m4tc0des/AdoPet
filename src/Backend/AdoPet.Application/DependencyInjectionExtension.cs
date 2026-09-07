using AdoPet.Application.UseCases.Login.LoginWithEmailAndPassword;
using AdoPet.Application.UseCases.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace AdoPet.Application;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddApplication()
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserAccountUseCase>();
            services.AddScoped<ILoginWithEmailAndPasswordUseCase, LoginWithEmailAndPasswordUseCase>();
        }
    }
}
