using AgendaAPI.Core.Entities;
using AgendaAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Persistence.Repositories;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Service?> GetWithAvailabilitiesAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Availabilities) // Cargar las disponibilidades relacionadas
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}