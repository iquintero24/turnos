namespace SistemaTurnos.Web.Repositories.Interfaces.Base;

// Define el metodo para añadir nuevas entidades: 
public interface IRepositorioEscribible<T> where T: class
{
    // Añadir una nueva entidad en una base de datos
    Task AgregarAsync(T entidad);
}