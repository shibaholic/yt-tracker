using System.Reflection;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ServiceExtensions
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IPasswordService, PasswordService>();
    }
}