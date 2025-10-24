using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories;
using SistemaTurnos.Web.Repositories.Interfaces;
using SistemaTurnos.Web.Services.Interfaces;

namespace SistemaTurnos.Web.Services;

public class TurnoService: IturnoService
{
    // Dependencias:
    private readonly ITurnoRepository _turnoRepository;
    private readonly BaseCrudRepository<Turno> _baseRepository;
    private readonly IAfiliadoRepository _afiliadoRepository; // para validar el afiliado 
    private readonly BaseCrudRepository<Atencion>  _atencionRepository; // Para guardar la Atención

    public TurnoService(ITurnoRepository turnoRepository, BaseCrudRepository<Turno> baseRepository, IAfiliadoRepository afiliadoRepository,  BaseCrudRepository<Atencion> atencionRepository)
    {
        _turnoRepository = turnoRepository;
        _baseRepository = baseRepository;
        _afiliadoRepository = afiliadoRepository;
        _atencionRepository = atencionRepository;
    }
    
    /// <summary>
    /// Solicitamos el siguiente turno
    /// </summary>
    /// <param name="documentoAfiliado"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<Turno> SolicitarTurnoAsync(string documentoAfiliado)
    {
        // Validamos el afiliado 
        var afiliado = await _afiliadoRepository.ObtenerPorDocumentoAsync(documentoAfiliado);
        if (afiliado == null)
        {
            throw new KeyNotFoundException(
                $"No se encontro un afiliado con el documento: {documentoAfiliado}"); // lanzamos un excepcion 
        }
        
        // calculamos el numero consecutivo del dia.
        var fechaActual = DateTime.Now;
        var ultimoNumero = await _turnoRepository.ObtenerUltimoNumeroDiarioAsync(fechaActual);

        var nuevoTurno = new Turno
        {
            AfiliadoId = afiliado.Id,
            Fecha = fechaActual,
            Numero = ultimoNumero + 1,
            Estado = "Pendiente",
            CajaId = null, 
            FuncionarioId = null 
        };

        await _baseRepository.AgregarAsync(nuevoTurno);
        return nuevoTurno;
    }
    
    /// <summary>
    /// Llamamos al siguiente turno que se mostrara en pantalla
    /// </summary>
    /// <param name="cajaId"></param>
    /// <param name="funcionarioId"></param>
    /// <returns></returns>
    public async Task<Turno?> LlamarSiguienteTurnoAsync(int cajaId, int funcionarioId)
    {
        var turno = await _turnoRepository.ObtenerSiguientePendienteAsync();

        if (turno == null)
        {
            return null; // retornamos null si no encontramos ese turno
        }
        
        // Seteamos variables al llamar al siguiente turno (Ejemplo : quien lo llamo y de que caja)
        turno.Estado = "En atencion";
        turno.CajaId = cajaId;
        turno.FuncionarioId = funcionarioId;
        turno.InicioAtencion = DateTime.Now;

        await _baseRepository.ActualizarAsync(turno);
        return turno;
    }
    
    public async Task<Atencion> FinalizarAtencionAsync(int turnoId, int funcionarioId)
    {
        // Obtenemos por id el turno
        var turno = await _baseRepository.GetByIdAsync(turnoId);
        
        // Validamos el turno 
        if (turno == null || turno.Estado != "En atencion" || turno.FuncionarioId != funcionarioId)
        {
            throw new InvalidOperationException("No se puede finalizar el turno. El turno es invalido o no esta 'En atencion' por este funcionario");
        }
        
        // marcamos el turno como terminado 
        turno.Estado = "Finalizado";
        turno.FinAtencion = DateTime.Now;
        await _baseRepository.ActualizarAsync(turno);
        
        // Creamos el registro de atenciones (Historial)

        var atencion = new Atencion
        { 
            TurnoId = turno.Id,
            FuncionarioId = turno.FuncionarioId.Value,
            CajaId = turno.CajaId.Value,
            InicioAtencion = turno.InicioAtencion.Value,
            FinAtencion = turno.FinAtencion.Value,
        };
        // Guardamos el turno
        await _atencionRepository.AgregarAsync(atencion);
        return atencion;
    }
}