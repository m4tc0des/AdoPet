using AdoPet.Domain.Entities;
using AdoPet.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace AdoPet.Infrastructure.DataAccess.Repositories;

internal sealed class UserRepository: IUserWriteOnlyRepository, IUserReadOnlyRepository
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

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Active && user.Email.Equals(email));
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));
    }
}
