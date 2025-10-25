using System.ComponentModel.DataAnnotations;

namespace SistemaTurnos.Web.Models.ViewModels.Afiliados;

public class AfiliadoViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Documento")]
    public string Documento { get; set; } = string.Empty;
    
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;
    
    [Display(Name = "Correo")]
    public string Correo { get; set; } = string.Empty;
    
    [Display(Name = "Estado")]
    public bool Estado { get; set; } = true;

}