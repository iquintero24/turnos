using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories;
using SistemaTurnos.Web.Repositories.Interfaces;
using SistemaTurnos.Web.Services.Interfaces;
using SistemaTurnos.Web.Utilities.interfaces;

namespace SistemaTurnos.Web.Services;

public class AfiliadoService: IAfiliadoService
{
    // Campos para la inyeccion de dependencias
    private readonly IAfiliadoRepository _afiliadoRepository;
    private readonly BaseCrudRepository<Afiliado> _baseRepository;
    private readonly IQRHelper _qrHelper;
    
    // Constructor para la inyeccion de dependencias
    public AfiliadoService(IAfiliadoRepository afiliadoRepository,  BaseCrudRepository<Afiliado> baseRepository, IQRHelper qrHelper)
    {
        _afiliadoRepository = afiliadoRepository;
        _baseRepository = baseRepository;
        _qrHelper = qrHelper;
    }
    
    // Implementacion del metodo de registrar afiliado
    public async Task<Afiliado> RegistrarAfiliadoAsync(Afiliado afiliado)
    {
        // Validamos si el documento ya existe 
        var existe = await _afiliadoRepository.ObtenerPorDocumentoAsync(afiliado.Documento);
        if (existe != null)
        {
            throw new Exception("Ya existe este documento"); // lanzamos una excepcion si ecuentra el numero de documento
        }
        // Generamos el codigo Qr Unico
        afiliado.CodigoQr = Guid.NewGuid().ToString();
    
        // Guardamos este afiliado en la base de datos 
        await _baseRepository.AgregarAsync(afiliado);
        return afiliado;
    }

    public async Task<Afiliado?> ObtenerAfiliadoPorDocumentoAsync(string documento)
    {
        // delegamos esa consulta directamente al repositorio
        return await _afiliadoRepository.ObtenerPorDocumentoAsync(documento);
    }

    public async Task<Afiliado?> ObtenerDatosPorQrAsync(string codigoQr)
    {
        return await _afiliadoRepository.ObtenerPorQrAsync(codigoQr);
    }
    
 
    public async Task<byte[]?> GenerarImagenQrAsync(string codigoQr, string baseUrl)
    {
       // Validamos el afiliado 
       var afiliado = await _afiliadoRepository.ObtenerPorQrAsync(codigoQr);
       if (afiliado == null) return null;
       
       // Componer la Url Completa
       string rutaRevalativaCarnet = $"/Carnet/Mostrar/{codigoQr}"; // aca es donde se va a generar el carnet
       string qrContent = $"{baseUrl}{rutaRevalativaCarnet}";
       
       // Llamamos a la utilidad para general la imagen:
       return _qrHelper.GenerarPngQrCode(qrContent);

    }
}