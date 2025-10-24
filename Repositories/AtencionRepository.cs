using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories.Interfaces;

namespace SistemaTurnos.Web.Repositories;

public class AtencionRepository: BaseCrudRepository<Atencion>, IAtencionRepository
{
    public AtencionRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Obtiene el historial de atenciones donde este el funcionario
    /// </summary>
    /// <param name="funcionarioId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Atencion>> ObtenerHistorialPorFuncionarioAsync(int funcionarioId)
    {
        return await DbSet.Where(a => a.FuncionarioId == funcionarioId).OrderByDescending(a => a.FinAtencion).ToListAsync();
    }
}