using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories.Interfaces;
using SistemaTurnos.Web.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using System.Threading.Tasks;
using System;
using SistemaTurnos.Web.Models.shared;
using SistemaTurnos.Web.Models.ViewModels.Afiliados;
using SistemaTurnos.Web.Utilities.interfaces; // Agregado para Guid y Math

namespace SistemaTurnos.Web.Services; // NAMESPACE CORREGIDO

/// <summary>
/// Implementación del servicio de lógica de negocio para la gestión de Afiliados.
/// </summary>
public class AfiliadoService : IAfiliadoService
{
    // Dependencias inyectadas
    private readonly IAfiliadoRepository _afiliadoRepository;
    private readonly ApplicationDbContext _context;
    private readonly IQRHelper _qrHelper;
    
    // El constructor recibe las dependencias necesarias.
    public AfiliadoService(IAfiliadoRepository afiliadoRepository, ApplicationDbContext context, IQRHelper qrHelper)
    {
        _afiliadoRepository = afiliadoRepository;
        _context = context;
        _qrHelper = qrHelper;
    }
    
    // =================================================================================
    // MÉTODOS DE REGISTRO
    // =================================================================================

    /// <summary>
    /// Registra un nuevo afiliado después de validar la unicidad del documento.
    /// Realiza el mapeo de ViewModel a Entidad.
    /// </summary>
    public async Task<Afiliado?> RegistrarAfiliadoAsync(AfiliadoRegistroViewModel model)
    {
        // 1. Validar unicidad del documento
        var existe = await _afiliadoRepository.ObtenerPorDocumentoAsync(model.Documento);
        if (existe != null)
        {
            // El documento ya existe
            return null; 
        }

        // 2. Mapear ViewModel a la Entidad Afiliado
        var nuevoAfiliado = new Afiliado
        {
            Nombre = model.Nombre,
            Documento = model.Documento,
            Correo = model.Correo ?? string.Empty,
            Direccion = model.Direccion ?? string.Empty,
            FotoUrl = "default-url", 
            Estado = true,
            // Generar un Código QR único para la URL del carnet
            CodigoQr = Guid.NewGuid().ToString()
        };
    
        // 3. Guardar en la base de datos
        await _context.Afiliados.AddAsync(nuevoAfiliado);
        await _context.SaveChangesAsync();
        
        return nuevoAfiliado;
    }

    // =================================================================================
    // MÉTODOS DE GESTIÓN (CRUD)
    // =================================================================================
    
    public async Task<bool> UpdateAfiliadoAsync(int id, AfiliadoRegistroViewModel model)
    {
        var afiliado = await GetAfiliadoByIdAsync(id);
        if (afiliado == null) return false;

        // 1. Validar unicidad del documento si se cambió (debe ser único, excepto para él mismo)
        var existeConOtroId = await _afiliadoRepository.ObtenerPorDocumentoAsync(model.Documento);
        if (existeConOtroId != null && existeConOtroId.Id != id)
        {
            return false;
        }

        // 2. Mapear los campos del ViewModel a la Entidad
        afiliado.Nombre = model.Nombre;
        afiliado.Documento = model.Documento;
        afiliado.Correo = model.Correo ?? string.Empty;
        afiliado.Direccion = model.Direccion ?? string.Empty;
        
        // 3. Actualizar en DB
        _context.Afiliados.Update(afiliado);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAfiliadoAsync(int id)
    {
        var afiliado = await GetAfiliadoByIdAsync(id);
        if (afiliado == null) return false;
        
        // Eliminación física (se puede cambiar a eliminación lógica si se prefiere)
        _context.Afiliados.Remove(afiliado);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Afiliado?> GetAfiliadoByIdAsync(int id)
    {
        return await _context.Afiliados.FirstOrDefaultAsync(a => a.Id == id);
    }
    
    /// <summary>
    /// Obtiene la lista de afiliados aplicando paginación y mapeando a AfiliadoViewModel.
    /// </summary>
    public async Task<PaginacionResponse<AfiliadoViewModel>> GetAfiliadosPaginadosAsync(int page, int pageSize)
    {
        var query = _context.Afiliados.AsQueryable();
        var totalItems = await query.CountAsync();
        
        // Aplicar la lógica de paginación (Skip y Take)
        var paginatedQuery = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        // Aplicar la proyección y ejecutar la consulta (ToListAsync)
        var afiliados = await paginatedQuery
            .Select(a => new AfiliadoViewModel
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Documento = a.Documento,
                Correo = a.Correo,
                Estado = a.Estado
            })
            .ToListAsync();
        
        // Construir el modelo de respuesta paginada
        return new PaginacionResponse<AfiliadoViewModel>
        {
            PaginaActual = page,
            ElementosPorPagina = pageSize,
            TotalElementos = totalItems,
            TotalPaginas = (int)Math.Ceiling((double)totalItems / pageSize),
            Elementos = afiliados
        };
    }

    // =================================================================================
    // MÉTODOS DE CONSULTA Y UTILIDAD
    // =================================================================================

    public async Task<Afiliado?> ObtenerAfiliadoPorDocumentoAsync(string documento)
    {
        // Se delega al Repositorio (método optimizado para esta búsqueda)
        return await _afiliadoRepository.ObtenerPorDocumentoAsync(documento);
    }

    public async Task<Afiliado?> ObtenerDatosPorQrAsync(string codigoQr)
    {
        // Se delega al Repositorio (método optimizado para esta búsqueda)
        return await _afiliadoRepository.ObtenerPorQrAsync(codigoQr);
    }
    
    /// <summary>
    /// Genera la imagen del QR para el carnet del afiliado.
    /// </summary>
    public async Task<byte[]?> GenerarImagenQrAsync(string codigoQr, string baseUrl)
    {
       // 1. Verificamos que el afiliado exista
       var afiliado = await ObtenerDatosPorQrAsync(codigoQr);
       if (afiliado == null) return null;
       
       // 2. Componer la Url Completa (ej: http://localhost:5284/Afiliado/Carnet/123)
       // NOTA: Usamos el ID del afiliado, no el CodigoQr, para la URL amigable.
       string rutaRevalativaCarnet = $"/Afiliado/Carnet/{afiliado.Id}"; 
       string qrContent = $"{baseUrl}{rutaRevalativaCarnet}";
       
       // 3. Llamar al helper para generar la imagen PNG (byte[])
       return _qrHelper.GenerarPngQrCode(qrContent);
    }
}