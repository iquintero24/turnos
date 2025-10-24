using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTurnos.Web.Data.Entities;

public class Caja
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } =  String.Empty;

    [Required] 
    [MaxLength(20)] 
    public string Estado { get; set; } = "Abierta";
    
    [ForeignKey(nameof(Funcionario))]
    public int? FuncionarioId { get; set; }
    
    // Relacion de Funcionarios 
    public Funcionario? Funcionario { get; set; }
}