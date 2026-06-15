using FluentValidation;

namespace AgendaAPI.Application.Features.Reservations.CreateReservation;

public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationValidator()
    {
        // Validar que el ID del cliente no sea vacío
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El ID del cliente es obligatorio.");

        // Validar que el ID del servicio no sea vacío
        RuleFor(x => x.ServiceId)
            .NotEmpty().WithMessage("El ID del servicio es obligatorio.");

        // Validar que la fecha de inicio sea en el futuro
        RuleFor(x => x.StartDateTime)
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("La fecha de inicio debe ser en el futuro.");

        // Validar que la fecha de fin sea posterior a la de inicio
        RuleFor(x => x.EndDateTime)
            .GreaterThan(x => x.StartDateTime).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");
    }
}