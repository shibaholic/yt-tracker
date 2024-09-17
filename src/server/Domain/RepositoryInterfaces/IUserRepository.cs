using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> AnyAsync(string username, CancellationToken cancelationToken);
    public Task<User?> GetUserByRefreshCode(Guid refreshToken, CancellationToken cancellationToken);
}