namespace AgendaAPI.Core.Enums;

// Los enums definen un conjunto cerrado de valores posibles.
// Esto hace que el código sea más legible y seguro que usar strings o ints.
public enum ReservationStatus
{
    // La reserva se creó, pero está esperando confirmación de pago o email.
    Pending,
    
    // El pago se confirmó y la reserva está activa.
    Confirmed,
    
    // El cliente o admin canceló la reserva.
    Cancelled,
    
    // La reserva expiró por no ser confirmada a tiempo (lo manejará Hangfire).
    Expired,
    
    // El servicio ya se prestó.
    Completed
}