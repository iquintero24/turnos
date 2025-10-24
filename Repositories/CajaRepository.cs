using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories.Interfaces;

namespace SistemaTurnos.Web.Repositories;

public class CajaRepository: BaseCrudRepository<Caja>, ICajaRepository
{
    public CajaRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    /// <summary>
    ///  Busca la caja que tiene asignado el ID del funcionario.
    /// </summary>
    /// <param name="funcionarioId"></param>
    /// <returns></returns>
    public async Task<Caja?> ObtenerPorFuncionarioIdAsync(int funcionarioId)
    {
        return await DbSet.FirstOrDefaultAsync(c=> c.FuncionarioId == funcionarioId);
    }
}