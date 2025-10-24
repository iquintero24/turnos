namespace SistemaTurnos.Web.Repositories.Interfaces.Base;

// Define el método para eliminar entidades
public interface IRepositorioEliminable<T> where T : class
{
    // Eliminar una entidad de la base de datos:
    Task EliminarAsync(T entidad);
}