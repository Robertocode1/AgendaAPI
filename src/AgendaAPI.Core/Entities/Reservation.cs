using AgendaAPI.Core.Common;
using AgendaAPI.Core.Enums;

namespace AgendaAPI.Core.Entities;

public class Reservation : EntityBase
{
    // Clave foránea: Guarda el Guid del cliente en la BD
    public Guid ClientId { get; set; }
    
    // Propiedad de navegación. El "null!" es intencional (explicado abajo)
    public Client Client { get; set; } = null!; 

    // Clave foránea del servicio
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    
    // Para integración con Stripe
    public string? PaymentIntentId { get; set; }
    public decimal AmountPaid { get; set; }

    // Para el Job de limpieza automática
    public DateTimeOffset? ExpiresAt { get; set; } 

    // Métodos de dominio: La entidad se protege a sí misma
    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException("Solo se pueden confirmar reservas pendientes.");
        
        Status = ReservationStatus.Confirmed;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Cancel()
    {
        if (Status == ReservationStatus.Cancelled || Status == ReservationStatus.Completed)
            throw new InvalidOperationException("No se puede cancelar esta reserva.");

        Status = ReservationStatus.Cancelled;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}