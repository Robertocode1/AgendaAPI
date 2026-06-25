using AgendaAPI.Core.Entities;
using AgendaAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Persistence.Repositories;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }
}