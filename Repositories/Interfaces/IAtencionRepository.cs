using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Repositories.Interfaces;

public interface IAtencionRepository
{
    /// <summary>
    /// Obtiene el historial de atenciones completadas por un funcionario.
    /// </summary>
    Task<IEnumerable<Atencion>> ObtenerHistorialPorFuncionarioAsync(int funcionarioId);
}