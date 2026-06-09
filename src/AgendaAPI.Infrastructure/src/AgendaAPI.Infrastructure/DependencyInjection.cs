using AgendaAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaAPI.Infrastructure;

// Esta clase es estática porque solo contiene métodos de extensión.
public static class DependencyInjection
{
    // El "this" antes del primer parámetro convierte este método en un "Método de Extensión".
    // Esto nos permite llamarlo en el Program.cs como si fuera parte nativa de IServiceCollection:
    // builder.Services.AddInfrastructure(builder.Configuration);
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // 1. Obtener el string de conexión desde appsettings.json o User Secrets
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // 2. Registrar el DbContext para que pueda ser inyectado en cualquier parte de la aplicación
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // Buena práctica para producción: Reintentos automáticos ante fallos transitorios de red
                // (ej: la BD se reinicia o hay un micro-corte de conexión)
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            }));

        // ==========================================
        // AQUÍ REGISTRAREMOS MÁS ADELANTE:
        // ==========================================
        
        // Background Jobs:
        // services.AddHangfire(config => config.UsePostgreSqlStorage(connectionString));
        // services.AddHangfireServer();

        // Servicios de Email (ej: SendGrid):
        // services.AddTransient<IEmailService, SendGridEmailService>();

        // Servicios de Pago (ej: Stripe):
        // services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

        return services;
    }
}