using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Repositories.Interfaces;

public interface IFuncionarioRepository
{
    /// <summary>
    /// Obtiene un funcionario por su identification única de empleado.
    /// </summary>
    Task<Funcionario?> ObtenerPorDocumentoAsync(string documento);
    
    /// <summary>
    /// Obtiene una lista de todos los funcionarios con estado 'Activo'.
    /// </summary>
    Task<List<Funcionario>> ObtenerActivosAsync();
}