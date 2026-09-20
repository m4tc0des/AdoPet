using AdoPet.Domain.Repositories;

namespace AdoPet.Infrastructure.DataAccess;

internal sealed class UnitOfWork: IUnitOfWork
{
    private readonly AdoPetDbContext _dbContext;

    public UnitOfWork(AdoPetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
