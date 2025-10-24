using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTurnos.Web.Data.Entities;

public class Atencion
{
    [Key] 
    public int Id { get; set; }
    
    // Turno asociado
    [Required]
    [ForeignKey(nameof(Turno))]
    public int TurnoId { get; set; }
    
    // Funcionario que atendio el turno:
    [Required]
    [ForeignKey(nameof(Funcionario))]
    public int FuncionarioId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Caja))]
    public int CajaId { get; set; }
    
    // Hora de inicio 
    [Required]
    public DateTime InicioAtencion { get; set; }
    
    // Hora finalizacion 
    [Required] 
    public DateTime FinAtencion { get; set; }
    
    
    
    //Relaciones
    public Funcionario? Funcionario { get; set; }
    public Turno? Turno { get; set; }
    public Caja? Caja { get; set; }
    
}