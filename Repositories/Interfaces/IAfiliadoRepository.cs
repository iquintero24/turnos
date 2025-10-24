using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Repositories.Interfaces;

// Aca van las consultas especificas
public interface IAfiliadoRepository
{
    /// <summary>
    /// Obtiene un afiliado por su número de documento.
    /// </summary>
    Task<Afiliado?> ObtenerPorDocumentoAsync(string documento);
    
    /// <summary>
    /// Obtiene un afiliado por el código QR de su carnet.
    /// </summary>
    Task<Afiliado?> ObtenerPorQrAsync(string codigo);
}