namespace AgendaAPI.Core.Common;

// Esta clase es abstracta porque nunca se instancia directamente.
// Todas nuestras entidades (Reservation, Client, etc.) heredarán de ella.
public abstract class EntityBase
{
    // Guid es mejor que int para IDs en sistemas distribuidos.
    // Se genera automáticamente al crear la entidad.
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // DateTimeOffset incluye la zona horaria, evitando problemas con UTC vs hora local.
    // Es crucial en una API de reservas que puede ser usada desde diferentes países.
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    // Nullable porque una entidad recién creada aún no ha sido actualizada.
    public DateTimeOffset? UpdatedAt { get; set; }
}