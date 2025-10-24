using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Services.Interfaces;

public interface ICajaService
{
    /// <summary>
    /// Asigna o reasigna un funcionario específico a una caja.
    /// </summary>
    /// <param name="cajaId"></param>
    /// <param name="funcionarioId"></param>
    /// <returns></returns>
    Task<Caja> AsignarFuncionarioACajaAsync(int cajaId, int funcionarioId);
    
    /// <summary>
    /// Libera la caja quitando la asignacion del funcionario 
    /// </summary>
    /// <param name="cajaId"></param>
    /// <returns></returns>
    Task<Caja> LiberarCajaAsync(int cajaId);
}