using AdoPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace AdoPet.Infrastructure.DataAccess;

internal class AdoPetDbContext: DbContext
{
    public AdoPetDbContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
}
