using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly MyDbContext _context;
    public RoleRepository(MyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetRoles(List<Guid> ids)
    {
        return await _context.Roles.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}