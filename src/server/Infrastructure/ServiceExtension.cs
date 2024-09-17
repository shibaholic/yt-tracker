using Domain.RepositoryInterfaces;

using Infrastructure.Database.Context;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");
        IServiceCollection serviceCollection = services.AddDbContext<MyDbContext>(opt => 
            opt.UseSqlServer(connectionString, x => x.MigrationsAssembly("Project.Infrastructure")), 
            ServiceLifetime.Scoped
        );

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}