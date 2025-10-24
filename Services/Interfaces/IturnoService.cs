using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Services.Interfaces;

public interface IturnoService
{
    /// <summary>
    /// Procesa la solicitud de un turno, calcula el número diario y asigna el estado 'Pendiente'.
    /// </summary>
    /// <param name="documentoAfiliado"></param>
    /// <returns></returns>
    Task<Turno> SolicitarTurnoAsync(string documentoAfiliado);
    
    /// <summary>
    /// Llamar al siguiente turno en lista (el mas antiguo que este en estado 'Pendiente')
    /// </summary>
    /// <param name="cajaId"></param>
    /// <param name="funcionarioId"></param>
    /// <returns></returns>
    Task<Turno?> LlamarSiguienteTurnoAsync(int cajaId, int funcionarioId);
    
    /// <summary>
    /// Finaliza la atención del turno y registra el historial.
    /// </summary>
    /// <param name="turnoId"></param>
    /// <param name="funcionarioId"></param>
    /// <returns></returns>
    Task<Atencion> FinalizarAtencionAsync(int turnoId, int funcionarioId);
}