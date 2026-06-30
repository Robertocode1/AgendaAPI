using AgendaAPI.Core.Enums;

namespace AgendaAPI.Application.Features.Reservations.GetReservationById;

public class ReservationResponseDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }
    public ReservationStatus Status { get; set; }
    public string? PaymentIntentId { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}