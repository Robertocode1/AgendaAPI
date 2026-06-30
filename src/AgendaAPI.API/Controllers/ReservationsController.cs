using AgendaAPI.Application.Features.Reservations.CreateReservation;
using AgendaAPI.Application.Features.Reservations.GetReservationById;
using AgendaAPI.Application.Features.Reservations.GetReservationsByClient;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly CreateReservationHandler _createHandler;
    private readonly GetReservationByIdHandler _getByIdHandler;
    private readonly GetReservationsByClientHandler _getByClientHandler;

    public ReservationsController(
        CreateReservationHandler createHandler,
        GetReservationByIdHandler getByIdHandler,
        GetReservationsByClientHandler getByClientHandler)
    {
        _createHandler = createHandler;
        _getByIdHandler = getByIdHandler;
        _getByClientHandler = getByClientHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReservationResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateReservationDto dto, 
        CancellationToken cancellationToken)
    {
        var result = await _createHandler.HandleAsync(dto, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return CreatedAtAction(
            nameof(GetById), 
            new { id = result.Value.Id }, 
            result.Value);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReservationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getByIdHandler.HandleAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpGet("client/{clientId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<ReservationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByClient(Guid clientId, CancellationToken cancellationToken)
    {
        var result = await _getByClientHandler.HandleAsync(clientId, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }
}