using AdoPet.Domain.Repositories;
using AdoPet.Domain.Repositories.User;
using AdoPet.Domain.Security.PasswordHashing;
using AdoPet.Infrastructure.DataAccess;
using AdoPet.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Infrastructure.Security.PasswordHashing;

namespace AdoPet.Infrastructure;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

            services.AddScoped<IUserReadOnlyRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AdoPetDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection");

                options.UseMySQL(connectionString!);
            });
                
        }
    }
}
