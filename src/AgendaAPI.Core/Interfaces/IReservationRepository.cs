using AgendaAPI.Core.Entities;

namespace AgendaAPI.Core.Interfaces;

// Hereda del IRepository genérico, así ya tiene GetById, Add, etc.
// Solo agregamos métodos específicos de reservas.
public interface IReservationRepository : IRepository<Reservation>
{
    // Verificar si un horario está ocupado (crucial para evitar dobles reservas)
    Task<bool> ExistsOverlapAsync(
        Guid serviceId, 
        DateTimeOffset start, 
        DateTimeOffset end, 
        Guid? excludeReservationId = null,
        CancellationToken cancellationToken = default);
    
    // Obtener reservas de un cliente específico
    Task<IReadOnlyList<Reservation>> GetByClientAsync(
        Guid clientId, 
        CancellationToken cancellationToken = default);
    
    // Para el Job de Hangfire: obtener reservas pendientes expiradas
    Task<IReadOnlyList<Reservation>> GetExpiredPendingAsync(
        DateTimeOffset now, 
        CancellationToken cancellationToken = default);
}