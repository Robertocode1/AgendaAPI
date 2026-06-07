using AgendaAPI.Core.Common;

namespace AgendaAPI.Core.Entities;

public class ServiceAvailability : EntityBase
{
    // Clave foránea hacia Service
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    // Día de la semana (Lunes, Martes, etc.)
    public DayOfWeek DayOfWeek { get; set; }

    // TimeSpan representa solo la hora, sin fecha ni zona horaria.
    // Es ideal para horarios recurrentes (09:00 a 18:00 cada Lunes)
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; }   

    public bool IsActive { get; set; } = true;
}