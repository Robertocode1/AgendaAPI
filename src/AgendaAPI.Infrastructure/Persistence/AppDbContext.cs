using System.Reflection;
using AgendaAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    // El constructor recibe las opciones de configuración inyectadas desde la API
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets: Representan las tablas en la base de datos
    public DbSet<Client> Clients { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceAvailability> ServiceAvailabilities { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // MAGIA: Aplica automáticamente TODAS las configuraciones (IEntityTypeConfiguration) 
        // que existan en este ensamblado (Infrastructure).
        // Así no tienes que llamar a builder.ApplyConfiguration(new ReservationConfiguration()) manualmente.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}