using AgendaAPI.Application.Features.Reservations.GetReservationById;
using AgendaAPI.Core.Common;
using AgendaAPI.Core.Interfaces;

namespace AgendaAPI.Application.Features.Reservations.GetReservationById;

public class GetReservationByIdHandler
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<ReservationResponseDto>> HandleAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id, cancellationToken);

        if (reservation == null)
        {
            return Result<ReservationResponseDto>.Failure("La reserva no existe.");
        }

        // Mapear entidad a DTO de respuesta
        var response = new ReservationResponseDto
        {
            Id = reservation.Id,
            ClientId = reservation.ClientId,
            ServiceId = reservation.ServiceId,
            StartDateTime = reservation.StartDateTime,
            EndDateTime = reservation.EndDateTime,
            Status = reservation.Status,
            PaymentIntentId = reservation.PaymentIntentId,
            AmountPaid = reservation.AmountPaid,
            ExpiresAt = reservation.ExpiresAt,
            CreatedAt = reservation.CreatedAt,
            UpdatedAt = reservation.UpdatedAt
        };

        return Result<ReservationResponseDto>.Success(response);
    }
}