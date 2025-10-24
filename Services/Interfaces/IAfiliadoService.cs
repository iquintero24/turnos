using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Services.Interfaces;

public interface IAfiliadoService
{
    /// <summary>
    /// Valida la unicidad y registra un nuevo afiliado, generando un codigo QR
    /// </summary>
    /// <param name="afiliado"></param>
    /// <returns></returns>
    Task<Afiliado> RegistrarAfiliadoAsync(Afiliado afiliado);

    /// <summary>
    /// Obtiene los datos del Afiliado por documento 
    /// </summary>
    /// <param name="documento"></param>
    /// <returns></returns>
    Task<Afiliado?> ObtenerAfiliadoPorDocumentoAsync(String documento);
    
    /// <summary>
    /// Busca un afiliado usando el código QR único.
    /// </summary>
    Task<Afiliado?> ObtenerDatosPorQrAsync(string codigoQr);
    
    /// <summary>
    /// Generar la imagen de Qr
    /// </summary>
    /// <param name="codigoQr"></param>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
    Task<Byte[]?> GenerarImagenQrAsync(string codigoQr, string baseUrl); 
}
