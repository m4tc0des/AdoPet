using AdoPet.Domain.Repositories;
using AdoPet.Domain.Repositories.User;
using AdoPet.Domain.Security.PasswordHashing;
using AdoPet.Domain.Security.Tokens;
using AdoPet.Infrastructure.DataAccess;
using AdoPet.Infrastructure.DataAccess.Repositories;
using AdoPet.Infrastructure.Security.Tokens.AccessToken;
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
            services.AddRepositories();
            services.AddSecurity(configuration);
            services.AddDbContext(configuration);
        }

        private void AddRepositories()
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

            services.AddScoped<IUserReadOnlyRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private void AddSecurity(IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

            services.AddScoped<IAccessTokenGenerator>(provider =>
            {
                var expireTimeMinutes = configuration.GetValue<uint>("JsonWebToken:ExpireTimeMinutes");
                var signingKey = configuration.GetValue<string>("JsonWebToken:SigningKey")!;

                return new JwtTokenHandler(expireTimeMinutes, signingKey);
            });
        }

        private void AddDbContext(IConfiguration configuration)
        {
            services.AddDbContext<AdoPetDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection");

                options.UseMySQL(connectionString!);
            });
        }
    }
}
