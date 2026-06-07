using AgendaAPI.Core.Common;

namespace AgendaAPI.Core.Entities;

public class Service : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; } // Duración estándar del servicio
    
    // Navegación: Un servicio puede estar disponible en varios horarios
    public ICollection<ServiceAvailability> Availabilities { get; set; } = new List<ServiceAvailability>();
}