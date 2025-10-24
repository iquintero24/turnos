using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data.Entities;

namespace SistemaTurnos.Web.Data;

public class ApplicationDbContext : DbContext
{
    // 1: Dbset : Coleccion de tablas que van para la base de datos
    public DbSet<Afiliado> Afiliados { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Caja> Cajas { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Atencion> Atenciones { get; set; }
    
    // Inicializamos el contructor del DbContext:

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    // 2. OnModelCreating: Configuracion de indices Unicos y Relaciones:
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuracion de unicidad 
        
        // Afiliado : Asegurar unicidad del documento y del QR
        modelBuilder.Entity<Afiliado>()
            .HasIndex(a =>  a.Documento)
            .IsUnique();
        
        modelBuilder.Entity<Afiliado>()
            .HasIndex(a => a.CodigoQr)
            .IsUnique();
        
        // Funcionario: Asegurar que el documento sea unico:
        modelBuilder.Entity<Funcionario>()
            .HasIndex(a => a.Documento)
            .IsUnique();
        
        // Configuracion de Relaciones (Claves Foraneas)
        // Relación Turno -> Afiliado (1 a Muchos)
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Afiliado) // Un turno tiene un Afiliado
            .WithMany() // Un Afiliado tiene muchos Turnos
            .HasForeignKey(t => t.AfiliadoId)
            .IsRequired();

        // Relación Caja -> Funcionario (1 a 1, un funcionario puede asignar a una caja a la vez)
        modelBuilder.Entity<Caja>()
            .HasOne(c => c.Funcionario) // Una Caja tiene un Funcionario
            .WithMany() // Un Funcionario puede estar asignado a muchas Cajas (si se cierran y abren, aunque solo una activa)
            .HasForeignKey(c => c.FuncionarioId)
            .IsRequired(false); // FuncionarioId puede ser nulo si la caja está cerrada

        // Relación Atencion -> Funcionario (1 a Muchos)
        modelBuilder.Entity<Atencion>()
            .HasOne(a => a.Funcionario) // Una Atención tiene un Funcionario
            .WithMany() // Un Funcionario tiene muchas Atenciones
            .HasForeignKey(a => a.FuncionarioId)
            .IsRequired();
            
        // Relación Atencion -> Turno (1 a 1)
        modelBuilder.Entity<Atencion>()
            .HasOne(a => a.Turno) // Una Atención tiene un Turno
            .WithOne() // Un Turno tiene una Atención (el historial de esa atención)
            .HasForeignKey<Atencion>(a => a.TurnoId)
            .IsRequired();
        
        
    }
    
    
}