using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories.Interfaces;

namespace SistemaTurnos.Web.Repositories;

public class TurnoRepository: BaseCrudRepository<Turno>, ITurnoRepository
{
    public TurnoRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Obtenemos el siguiente turno pendiente: 
    /// </summary>
    /// <returns></returns>
    public async Task<Turno?> ObtenerSiguientePendienteAsync()
    {
        return await DbSet.Where(t => t.Estado == "Pendiente").OrderBy(t => t.Fecha).FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// buscar el ultimo turno generado
    /// </summary>
    /// <param name="fecha"></param>
    /// <returns></returns>
    public async Task<int> ObtenerUltimoNumeroDiarioAsync(DateTime fecha)
    {
        var inicioDia = fecha.Date;
        var finDia = fecha.Date.AddDays(1);
        
        return await DbSet.Where(t=> t.Fecha >= inicioDia && t.Fecha <= finDia)
            .Select(t=> t.Numero)
            .DefaultIfEmpty(0).MaxAsync(); // devuelve 0 si no hya turnos todavia en el dia
    }
}