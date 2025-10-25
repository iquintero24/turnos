using System.ComponentModel.DataAnnotations;

namespace SistemaTurnos.Web.Models.ViewModels.Afiliados;

/// <summary>
/// View Model utilizado para registrar y editar nuevos afiliados
/// </summary>
public class AfiliadoRegistroViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre completo debe tener entre 5 y 50 caracteres")]
    [Display(Name = "Nombre Completo")]
    public string Nombre { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "El número de documento es obligatorio.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "El documento debe tener entre 5 y 20 caracteres.")]
    [Display(Name = "Documento")]
    public string Documento { get; set; } = string.Empty;
    
    [EmailAddress(ErrorMessage = "Ingrese una dirección de correo válida.")]
    [Display(Name = "Correo Electrónico")]
    public string Correo { get; set; } = string.Empty;
    
    [StringLength(50, ErrorMessage = "La dirección no puede exceder los 50 caracteres.")]
    [Display(Name = "Dirección de Residencia")]
    public string Direccion { get; set; } = string.Empty;
    
    [Display(Name = "Estado")]
    public bool Estado { get; set; } = true;
    
}