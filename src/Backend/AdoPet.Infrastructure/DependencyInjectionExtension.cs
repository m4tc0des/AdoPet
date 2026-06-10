using AdoPet.Domain.Security.PasswordHashing;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Infrastructure.Security.PasswordHashing;

namespace AdoPet.Infrastructure;

public class DependencyInjectionExtension
{
    public void AddInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
    }
}
