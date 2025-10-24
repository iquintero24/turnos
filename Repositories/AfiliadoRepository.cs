using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories.Interfaces;

namespace SistemaTurnos.Web.Repositories;

public class AfiliadoRepository : BaseCrudRepository<Afiliado>, IAfiliadoRepository
{
    public AfiliadoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Afiliado?> ObtenerPorDocumentoAsync(string documento)
    {
        // Buscar el afiliado por su documento unico:
        return await  DbSet.FirstOrDefaultAsync(a => a.Documento == documento);
    }

    public async Task<Afiliado?> ObtenerPorQrAsync(string codigo)
    {
        // Buscar afiliado por el codigo Qr
        return await DbSet.FirstOrDefaultAsync(a => a.CodigoQr == codigo);
    }
}