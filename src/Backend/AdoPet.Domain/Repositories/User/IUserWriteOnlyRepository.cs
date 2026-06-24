using AdoPet.Domain.Entities;

namespace AdoPet.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    Task Add(Domain.Entities.User user);
}
