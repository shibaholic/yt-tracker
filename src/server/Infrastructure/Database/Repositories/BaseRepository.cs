using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Database.Context;

namespace Infrastructure.Database.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
{
    private readonly MyDbContext _context;

    public BaseRepository(MyDbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        entity.DateCreated = DateTimeOffset.UtcNow;
        _context.Add(entity);
    }

    public void Delete(T entity)
    {
        entity.DateDeleted = DateTimeOffset.UtcNow;
        _context.Remove(entity);
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T GetById(Guid id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public void Update(T entity)
    {
        entity.DateUpdated = DateTimeOffset.UtcNow;
        _context.Update(entity);
    }
}