using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Services.Interfaces;

public interface IFuncionarioService
{
    /// <summary>
    /// Intenta autenticar a un funcionario por su número de documento 
    /// </summary>
    /// <param name="documento"></param>
    /// <returns></returns>
    /// todo: refactorizar esta implementacion para que sea por identity []
    Task<Funcionario?> AutenticarFuncionarioAsync(string documento);
    
    /// <summary>
    /// Obtiene la lista de Funcionarios que estan activos actualmente 
    /// </summary>
    /// <returns></returns>
    Task<List<Funcionario>> ObtenerFuncionariosActivosAsync();
}