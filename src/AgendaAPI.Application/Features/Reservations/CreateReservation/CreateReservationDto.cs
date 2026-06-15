namespace AgendaAPI.Application.Features.Reservations.CreateReservation;

public class CreateReservationDto
{
    public Guid ClientId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    // Opcional: podríamos calcular el EndDateTime en base a la duración del servicio, 
    // o recibirlo directamente. Por ahora lo recibimos para simplificar.
    public DateTimeOffset EndDateTime { get; set; } 
}

