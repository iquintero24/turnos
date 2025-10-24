using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Repositories.Interfaces;

public interface ITurnoRepository
{
    /// <summary>
    /// Obtiene el turno más antiguo con estado 'Pendiente'.
    /// </summary>
    Task<Turno?> ObtenerSiguientePendienteAsync();
    
    /// <summary>
    /// Obtiene el último número de turno asignado para el día de hoy (ej. 15).
    /// </summary>
    Task<int> ObtenerUltimoNumeroDiarioAsync(DateTime fecha);
}