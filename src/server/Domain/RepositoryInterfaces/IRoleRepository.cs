using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<List<Role>> GetRoles(List<Guid> ids);
}