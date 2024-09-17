using Domain.RepositoryInterfaces;
using Infrastructure.Database.Context;

namespace Infrastructure.Database.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _context;

    public UnitOfWork(MyDbContext context)
    {
        _context = context;
    }

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}