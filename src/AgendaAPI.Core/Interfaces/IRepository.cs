namespace AgendaAPI.Core.Interfaces;

// Interfaz genérica base. Sirve para cualquier entidad.
// T debe ser una clase (referencia) y heredar de EntityBase.
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}