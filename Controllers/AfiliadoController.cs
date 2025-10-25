using Microsoft.AspNetCore.Mvc;
using SistemaTurnos.Web.Models.ViewModels.Afiliados;
using SistemaTurnos.Web.Services.Interfaces;

namespace SistemaTurnos.Web.Controllers;

public class AfiliadoController: Controller
{
    private readonly IAfiliadoService _afiliadoService;
    private readonly string _BaseUrl;
    
    public AfiliadoController(IAfiliadoService afiliadoService, IConfiguration configuration)
    {
        _afiliadoService = afiliadoService;
        _BaseUrl = configuration.GetValue<string>("BaseUrl") ?? "http://localhost:5284";
    }

  // Accion Index (LISTADO CON PAGINACION)
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
       // validamos que la pagina sea menor a uno 
       if (pageNumber < 1) pageNumber = 1;
       
       var response = await _afiliadoService.GetAfiliadosPaginadosAsync(pageNumber, pageSize);
       
       // El view model ya es paginacionResponse<AfiliadoViewModel>
         return View(response);
    }
    
    // Mandamos la vista de registramos view model
    [HttpGet]
    public IActionResult Register()
    {
        return View(new AfiliadoRegistroViewModel());
    }
    
    // Post: /Afiliado/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AfiliadoRegistroViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var afiliado = await _afiliadoService.RegistrarAfiliadoAsync(model);

        if(afiliado == null)
        {
            ModelState.AddModelError(string.Empty, "El documento ya está registrado.");
            return View(model);
        }
        
        TempData["SuccessMessage"] = "Afiliado registrado exitosamente.";
        return RedirectToAction(nameof(Index));
    }
    
    //  Get: /Afiliado/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var afiliado = await _afiliadoService.GetAfiliadoByIdAsync(id);
        if (afiliado == null)return NotFound();
        
        // Mapear entidad a ViewModel para mostrar los datos en el formulario
        var model = new AfiliadoRegistroViewModel
        {
            Id = afiliado.Id,
            Nombre = afiliado.Nombre,
            Documento = afiliado.Documento,
            Correo = afiliado.Correo,
            Direccion = afiliado.Direccion
        };
        
        return View(model);
    }
    
    // Post: /Afiliado/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AfiliadoRegistroViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);
        
        var success = await _afiliadoService.UpdateAfiliadoAsync(id, model);
        if (!success)
        {
            // Podría ser un error de documento duplicado o ID no encontrado
            ModelState.AddModelError("Documento", "Error al actualizar. El documento podría estar duplicado.");
            return View(model);
        }
        
        TempData["SuccessMessage"] = "Afiliado actualizado exitosamente.";
        return RedirectToAction(nameof(Index));
    }
    
    //[POST]:/Afiliado/Delete/5
    //delete afiliado 
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _afiliadoService.DeleteAfiliadoAsync(id);
        if (!success)
        {
            TempData["ErrorMessage"] = "Error al eliminar el afiliado.";
        }
        else
        {
            TempData["SuccessMessage"] = "Afiliado eliminado exitosamente.";
        }
        return RedirectToAction(nameof(Index));
    }
    
    // =================================================================================
    // ACCIÓN: Carnet (Visualización y Descarga del QR)
    // GET: /Afiliado/Carnet/5
    // =================================================================================
    
    [HttpGet]
    public async Task<IActionResult> Carnet(int id)
    {
        var afiliado = await _afiliadoService.GetAfiliadoByIdAsync(id);
        if (afiliado == null)
        {
            return NotFound();
        }

        // Generar la imagen del QR (byte[])
        var qrImageBytes = await _afiliadoService.GenerarImagenQrAsync(afiliado.CodigoQr, _BaseUrl);
        
        if (qrImageBytes == null)
        {
            TempData["ErrorMessage"] = "Error al generar el Código QR.";
            return RedirectToAction(nameof(Index));
        }

        var model = new AfiliadoCarnetViewModel
        {
            Id = afiliado.Id,
            FotoUrl = afiliado.FotoUrl,
            Nombre = afiliado.Nombre,
            Documento = afiliado.Documento,
            QrCodePngBase64 = System.Convert.ToBase64String(qrImageBytes)
        };
        
        return View(model);
    }

    // GET: /Afiliado/QrImage/5
    // Acción para servir la imagen del QR directamente
    [HttpGet]
    public async Task<IActionResult> QrImage(int id)
    {
        var afiliado = await _afiliadoService.GetAfiliadoByIdAsync(id);
        if (afiliado == null)
        {
            return NotFound();
        }
        
        var qrImageBytes = await _afiliadoService.GenerarImagenQrAsync(afiliado.CodigoQr, _BaseUrl);

        if (qrImageBytes == null)
        {
            return NotFound(); // No se pudo generar la imagen
        }
        
        // Devolver el archivo como una imagen PNG
        return File(qrImageBytes, "image/png");
    }
    
}
  
    
    
    
