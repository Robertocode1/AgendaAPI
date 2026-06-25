using AgendaAPI.Core.Interfaces;
using AgendaAPI.Infrastructure.Persistence;
using AgendaAPI.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            }));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}