namespace Domain.RepositoryInterfaces;

public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken);
}