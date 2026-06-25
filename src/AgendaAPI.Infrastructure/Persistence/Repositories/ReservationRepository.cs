using AgendaAPI.Core.Entities;
using AgendaAPI.Core.Enums;
using AgendaAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Persistence.Repositories;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDbContext context) : base(context)
    {
    }

    // Método complejo: Verificar si hay solapamiento de horarios
    public async Task<bool> ExistsOverlapAsync(
        Guid serviceId, 
        DateTimeOffset start, 
        DateTimeOffset end, 
        Guid? excludeReservationId = null,
        CancellationToken cancellationToken = default)
    {
        // Lógica de solapamiento: Dos rangos se solapan si:
        // inicio1 < fin2 Y fin1 > inicio2
        var query = _dbSet
            .Where(r => r.ServiceId == serviceId)
            .Where(r => r.Status != ReservationStatus.Cancelled && r.Status != ReservationStatus.Expired)
            .Where(r => r.StartDateTime < end && r.EndDateTime > start);

        // Si estamos editando una reserva existente, excluirla de la verificación
        if (excludeReservationId.HasValue)
        {
            query = query.Where(r => r.Id != excludeReservationId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetByClientAsync(
        Guid clientId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.ClientId == clientId)
            .OrderByDescending(r => r.StartDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetExpiredPendingAsync(
        DateTimeOffset now, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.Status == ReservationStatus.Pending)
            .Where(r => r.ExpiresAt.HasValue && r.ExpiresAt.Value < now)
            .ToListAsync(cancellationToken);
    }
}