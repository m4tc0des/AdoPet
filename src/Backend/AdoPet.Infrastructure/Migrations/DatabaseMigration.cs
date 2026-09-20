using AdoPet.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdoPet.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<AdoPetDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
