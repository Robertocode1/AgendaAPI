using AgendaAPI.Application.Features.Reservations.GetReservationById;
using AgendaAPI.Core.Common;
using AgendaAPI.Core.Interfaces;

namespace AgendaAPI.Application.Features.Reservations.GetReservationsByClient;

public class GetReservationsByClientHandler
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationsByClientHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<IReadOnlyList<ReservationResponseDto>>> HandleAsync(
        Guid clientId, 
        CancellationToken cancellationToken = default)
    {
        var reservations = await _reservationRepository.GetByClientAsync(clientId, cancellationToken);

        if (!reservations.Any())
        {
            return Result<IReadOnlyList<ReservationResponseDto>>.Failure("El cliente no tiene reservas.");
        }

        // Mapear entidades a DTOs
        var response = reservations.Select(r => new ReservationResponseDto
        {
            Id = r.Id,
            ClientId = r.ClientId,
            ServiceId = r.ServiceId,
            StartDateTime = r.StartDateTime,
            EndDateTime = r.EndDateTime,
            Status = r.Status,
            PaymentIntentId = r.PaymentIntentId,
            AmountPaid = r.AmountPaid,
            ExpiresAt = r.ExpiresAt,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        }).ToList();

        return Result<IReadOnlyList<ReservationResponseDto>>.Success(response);
    }
}