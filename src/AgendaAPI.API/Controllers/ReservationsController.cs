using AgendaAPI.Application.Features.Reservations.CreateReservation;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly CreateReservationHandler _handler;

    public ReservationsController(CreateReservationHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateReservationDto dto, 
        CancellationToken cancellationToken)
    {
        var result = await _handler.HandleAsync(dto, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        // Devolver la reserva creada con su ID
        return CreatedAtAction(
            nameof(GetById), 
            new { id = result.Value.Id }, 
            result.Value);
    }

    // Endpoint placeholder para que CreatedAtAction funcione
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Lo implementaremos después
        return NotFound();
    }
}