using AdoPet.Domain.Entities;
using AdoPet.Domain.Repositories.User;

namespace AdoPet.Infrastructure.DataAccess.Repositories;

internal sealed class UserRepository: IUserWriteOnlyRepository
{
    private readonly AdoPetDbContext _dbContext;
    public UserRepository(AdoPetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}
