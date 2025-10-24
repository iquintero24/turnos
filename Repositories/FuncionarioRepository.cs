using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories.Interfaces;

namespace SistemaTurnos.Web.Repositories;

public class FuncionarioRepository: BaseCrudRepository<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Buscar funcionario por numero de documento
    /// </summary>
    /// <param name="documento"></param>
    /// <returns></returns>
    public async Task<Funcionario?> ObtenerPorDocumentoAsync(string documento)
    {
        // Obtener FUncionario por numero de documento:
        return await DbSet.FirstOrDefaultAsync(f=>f.Documento == documento);
    }
    
    /// <summary>
    /// Lista los funcionarios que esten en estado activo 
    /// </summary>
    /// <returns></returns>
    public async Task<List<Funcionario>> ObtenerActivosAsync()
    {
        return await DbSet.Where(f=> f.Estado == true).ToListAsync();
    }
}