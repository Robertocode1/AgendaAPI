using AgendaAPI.Core.Entities;

namespace AgendaAPI.Core.Interfaces;

public interface IServiceRepository : IRepository<Service>
{
    // Obtener un servicio con sus disponibilidades cargadas
    Task<Service?> GetWithAvailabilitiesAsync(
        Guid id, 
        CancellationToken cancellationToken = default);
}