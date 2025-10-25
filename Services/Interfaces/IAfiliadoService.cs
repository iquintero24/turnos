using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Models.shared;
using SistemaTurnos.Web.Models.ViewModels.Afiliados;

namespace SistemaTurnos.Web.Services.Interfaces;

public interface IAfiliadoService
{
    // =================================================================
    // MÉTODOS DE REGISTRO
    // =================================================================
    
    /// <summary>
    /// Valida la unicidad y registra un nuevo afiliado, generando un código QR.
    /// Acepta el ViewModel para la entrada de datos.
    /// </summary>
    /// <param name="model">Datos de registro del afiliado.</param>
    /// <returns>La entidad Afiliado creada o null si falla.</returns>
    Task<Afiliado?> RegistrarAfiliadoAsync(AfiliadoRegistroViewModel model);
    
    // =================================================================
    // MÉTODOS DE GESTIÓN (CRUD)
    // =================================================================

    /// <summary>
    /// Obtiene los datos del Afiliado por ID.
    /// </summary>
    Task<Afiliado?> GetAfiliadoByIdAsync(int id);
    
    /// <summary>
    /// Actualiza los datos de un afiliado existente.
    /// </summary>
    Task<bool> UpdateAfiliadoAsync(int id, AfiliadoRegistroViewModel model);

    /// <summary>
    /// Elimina (lógicamente o físicamente) un afiliado por ID.
    /// </summary>
    Task<bool> DeleteAfiliadoAsync(int id);
    
    /// <summary>
    /// Obtiene una lista paginada de todos los afiliados.
    /// </summary>
    Task<PaginacionResponse<AfiliadoViewModel>> GetAfiliadosPaginadosAsync(int page, int pageSize);


    // =================================================================
    // MÉTODOS DE CONSULTA Y UTILIDAD
    // =================================================================

    /// <summary>
    /// Obtiene los datos del Afiliado por documento.
    /// </summary>
    /// <param name="documento"></param>
    /// <returns></returns>
    Task<Afiliado?> ObtenerAfiliadoPorDocumentoAsync(string documento);
    
    /// <summary>
    /// Busca un afiliado usando el código QR único.
    /// </summary>
    Task<Afiliado?> ObtenerDatosPorQrAsync(string codigoQr);
    
    /// <summary>
    /// Generar la imagen de Qr en formato byte[]
    /// </summary>
    /// <param name="codigoQr">El código que contiene la URL a incrustar.</param>
    /// <param name="baseUrl">La URL base de la aplicación (e.g., http://localhost:5284).</param>
    /// <returns>Array de bytes representando la imagen PNG del QR.</returns>
    Task<byte[]?> GenerarImagenQrAsync(string codigoQr, string baseUrl); 
}
