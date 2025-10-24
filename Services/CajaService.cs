using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories;
using SistemaTurnos.Web.Repositories.Interfaces;
using SistemaTurnos.Web.Services.Interfaces;

namespace SistemaTurnos.Web.Services;

public class CajaService: ICajaService
{
    //variables para la inyeccion 
    private readonly ICajaRepository _cajaRepository;
    private readonly BaseCrudRepository<Caja> _baseCrudRepository;

    public CajaService(ICajaRepository cajaRepository,BaseCrudRepository<Caja> baseCrudRepository )
    {
        // inyeccion de dependencias (Dependencias que necesitamos para poder hacer consultas)
        _cajaRepository = cajaRepository;
        _baseCrudRepository = baseCrudRepository;
    }
    
    public async Task<Caja> AsignarFuncionarioACajaAsync(int cajaId, int funcionarioId)
    {
        //Buscamos la caja que vamos actualizar
        var caja = await _baseCrudRepository.GetByIdAsync(cajaId);
        if (caja == null)
        {
            // Lanzamos una excepcion en caso de no encontrar la caja:
            throw new Exception($"No se la encontro la caja con el id {cajaId}");
        }
        
        // asignamos el funcionario
        caja.FuncionarioId = funcionarioId;
        caja.Estado = "Ocupada";
        
        // Actualizamos la entidad
        await _baseCrudRepository.ActualizarAsync(caja);
        return caja;

    }

    public async Task<Caja> LiberarCajaAsync(int cajaId)
    {
        // Buscamos la caja que vamos a liberar 
        var caja = await _baseCrudRepository.GetByIdAsync(cajaId);
        if (caja == null)
        {
            throw new KeyNotFoundException($"Caja con el id  {cajaId} No encontrada.");
        }
        
        // Logica para liberar la caja 
        caja.FuncionarioId = null;
        caja.Estado = "Abierta";
        
        // Actualizamos la entidad
        await _baseCrudRepository.ActualizarAsync(caja);
        return caja;
    }
}