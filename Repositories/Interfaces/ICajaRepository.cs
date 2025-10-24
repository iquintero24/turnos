using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Repositories.Interfaces;

public interface ICajaRepository
{
    /// <summary>
    /// Obtiene la caja que tiene asignado un funcionario específico.
    /// </summary>
    Task<Caja?> ObtenerPorFuncionarioIdAsync(int funcionarioId);
}