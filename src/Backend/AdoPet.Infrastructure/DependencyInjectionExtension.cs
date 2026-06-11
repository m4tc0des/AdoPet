using AdoPet.Domain.Security.PasswordHashing;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Infrastructure.Security.PasswordHashing;

namespace AdoPet.Infrastructure;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure()
        {
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
        }
    }
}
