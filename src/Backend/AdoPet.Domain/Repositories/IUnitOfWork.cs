namespace AdoPet.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
