using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Repositories.Interfaces.Base;

namespace SistemaTurnos.Web.Repositories;

public class BaseCrudRepository<T>: 
    IRepositorioLeible<T>,
    IRepositorioEscribible<T>,
    IRepositorioActualizable<T>, 
    IRepositorioEliminable<T> where T : class

{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    public BaseCrudRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    // Implementacion De CRUD (funcionalidad generica)
    
    public async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task AgregarAsync(T entidad)
    {
        await DbSet.AddAsync(entidad);
        await Context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(T entidad)
    {
        DbSet.Update(entidad); 
        await Context.SaveChangesAsync();
    }

    public async Task EliminarAsync(T entidad)
    {
        DbSet.Remove(entidad);
        await Context.SaveChangesAsync();
    }
}