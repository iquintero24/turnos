namespace SistemaTurnos.Web.Repositories.Interfaces.Base;

// Define los metodos de lectura (Obtener uno por ID o todos)
public interface IRepositorioLeible<T> where T : class
{
    // metodo para encontrar por id
    Task<T?> GetByIdAsync(int id);
    
    // metodo para traer todos los registros:
    Task<IEnumerable<T>> GetAllAsync();
}