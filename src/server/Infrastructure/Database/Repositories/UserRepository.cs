using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> AnyAsync(string username, CancellationToken cancelationToken)
    {
        return _context.Users
            .AnyAsync(x => x.Username == username, cancelationToken);
    }

    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return _context.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }

    public Task<User?> GetUserByRefreshCode(Guid refreshToken, CancellationToken cancellationToken)
    {
        return _context.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken: cancellationToken);
    }
}