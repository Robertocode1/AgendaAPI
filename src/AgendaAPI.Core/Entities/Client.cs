using AgendaAPI.Core.Common;

namespace AgendaAPI.Core.Entities;

public class Client : EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    // Propiedad de navegación: Un cliente puede tener muchas reservas.
    // Esto le dice a EF Core que existe una relación uno-a-muchos.
    // La inicializamos para evitar NullReferenceException al acceder a ella.
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}