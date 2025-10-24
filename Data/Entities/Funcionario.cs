using System.ComponentModel.DataAnnotations;

namespace SistemaTurnos.Web.Data.Entities;

public class Funcionario
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Documento { get; set; } = String.Empty;

    [Required] 
    [MaxLength(20)] 
    public string Rol { get; set; } = "Funcionario";
    
    public bool Estado { get; set; } = true;

    



}