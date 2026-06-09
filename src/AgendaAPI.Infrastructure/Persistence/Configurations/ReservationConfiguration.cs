using AgendaAPI.Core.Entities;
using AgendaAPI.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaAPI.Infrastructure.Persistence.Configurations;

// Implementamos la interfaz de EF Core para configurar la entidad Reservation
public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        // 1. Nombre de la tabla en la BD (opcional, pero buena práctica)
        builder.ToTable("Reservations");

        // 2. Configuración de la clave primaria (ya la detecta por convención, pero es bueno ser explícito)
        builder.HasKey(r => r.Id);

        // 3. Restricciones de columnas (evita que la BD acepte datos basura)
        builder.Property(r => r.PaymentIntentId)
               .HasMaxLength(255)
               .IsRequired(false); // Puede ser null si aún no se paga

        builder.Property(r => r.AmountPaid)
               .HasColumnType("decimal(18,2)"); // Formato estándar para dinero en PostgreSQL

        // 4. Mapeo del Enum: Guardarlo como string en la BD es más legible al hacer consultas manuales
        // Si prefieres rendimiento puro, quita esta línea y se guardará como int.
        builder.Property(r => r.Status)
               .HasConversion<string>(); 

        // 5. Relaciones (Claves foráneas y comportamiento en cascada)
        builder.HasOne(r => r.Client)
               .WithMany(c => c.Reservations)
               .HasForeignKey(r => r.ClientId)
               .OnDelete(DeleteBehavior.Restrict); // Evita borrar un cliente si tiene reservas

        builder.HasOne(r => r.Service)
               .WithMany() // No pusimos ICollection<Reservation> en Service para simplificar, pero la relación existe
               .HasForeignKey(r => r.ServiceId)
               .OnDelete(DeleteBehavior.Restrict);

        // 6. Índices para optimizar consultas frecuentes
        // Ejemplo: Buscar reservas de un cliente específico, o reservas pendientes que expiran pronto
        builder.HasIndex(r => r.ClientId);
        builder.HasIndex(r => new { r.Status, r.ExpiresAt });
    }
}