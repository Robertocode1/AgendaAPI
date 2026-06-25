using AgendaAPI.Core.Common;
using AgendaAPI.Core.Entities;
using AgendaAPI.Core.Enums;
using AgendaAPI.Core.Interfaces;
using FluentValidation;

namespace AgendaAPI.Application.Features.Reservations.CreateReservation;

public class CreateReservationHandler
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IValidator<CreateReservationDto> _validator;

    public CreateReservationHandler(
        IReservationRepository reservationRepository,
        IValidator<CreateReservationDto> validator)
    {
        _reservationRepository = reservationRepository;
        _validator = validator;
    }

    public async Task<Result<Reservation>> HandleAsync(
        CreateReservationDto dto, 
        CancellationToken cancellationToken = default)
    {
        // 1. Validar el DTO con FluentValidation
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Concatenar todos los errores de validación en un solo string
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<Reservation>.Failure(errors);
        }

        // 2. Verificar que no haya solapamiento de horarios
        // 2. Verificar que no haya solapamiento de horarios (usar UTC)
        bool haySolapamiento = await _reservationRepository.ExistsOverlapAsync(
            dto.ServiceId,
            dto.StartDateTime.ToUniversalTime(),  // Convertir a UTC
            dto.EndDateTime.ToUniversalTime(),    // Convertir a UTC
            excludeReservationId: null,
            cancellationToken);

        if (haySolapamiento)
        {
            return Result<Reservation>.Failure("El horario solicitado ya está ocupado.");
        }

        // 3. Crear la entidad de dominio (convertir a UTC)
        var reservation = new Reservation
        {
            ClientId = dto.ClientId,
            ServiceId = dto.ServiceId,
            // Convertir a UTC antes de guardar
            StartDateTime = dto.StartDateTime.ToUniversalTime(),
            EndDateTime = dto.EndDateTime.ToUniversalTime(),
            Status = ReservationStatus.Pending,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(15)
        };

        // 4. Guardar en la base de datos
        await _reservationRepository.AddAsync(reservation, cancellationToken);

        // 5. Devolver éxito con la reserva creada
        return Result<Reservation>.Success(reservation);
    }
}