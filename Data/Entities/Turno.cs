using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTurnos.Web.Data.Entities;

public class Turno
{
    [Key]
    public int Id { get; set; }
    
    [Required] 
    public int Numero { get; set; }
    
    [Required] 
    public DateTime Fecha { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Estado { get; set; } = "Pendiente";
    
    // Afiliado que esta asignado al turno (Se necesita para crear un turno)
    [Required]
    [ForeignKey(nameof(Afiliado))]
    public int AfiliadoId { get; set; }
    
    // Caja que asigno el turno puede ser null si esta "Pendiente"
    [ForeignKey(nameof(Caja))]
    public int? CajaId { get; set; }
    
    [ForeignKey(nameof(Funcionario))]
    public int? FuncionarioId { get; set; }
    
    // --- Tiempos de Atención (Null hasta que se llama/finaliza) ---
    public DateTime? InicioAtencion { get; set; }
    public DateTime? FinAtencion { get; set; }
    
    public Afiliado? Afiliado { get; set; }
    public Caja? Caja { get; set; }
    public Funcionario? Funcionario { get; set; }
}