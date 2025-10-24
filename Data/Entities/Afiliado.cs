using System.ComponentModel.DataAnnotations;

namespace SistemaTurnos.Web.Data.Entities;

public class Afiliado
{ 
    // Utilizamos anotaciones para saber sus validaciones:
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Documento { get; set; } = String.Empty;
    
    [MaxLength(50)]
    public string Correo { get; set; } = String.Empty;
    
    [MaxLength(50)]
    public string Direccion { get; set; } = String.Empty;
    
    [Required]
    public string FotoUrl { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(50)]
    public string CodigoQr { get; set; } = String.Empty;

    public bool Estado { get; set; } = true;
    
}