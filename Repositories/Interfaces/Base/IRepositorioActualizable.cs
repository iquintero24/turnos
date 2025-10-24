namespace SistemaTurnos.Web.Repositories.Interfaces.Base;

//Define el metodo para actualizar entidades
public interface IRepositorioActualizable<T> where T : class
{
    // Actualizar una entidad existente en la base de datos:
    Task ActualizarAsync(T entidad);
}