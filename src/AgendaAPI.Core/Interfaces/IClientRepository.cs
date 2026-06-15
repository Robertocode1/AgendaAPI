using AgendaAPI.Core.Entities;

namespace AgendaAPI.Core.Interfaces;

public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}