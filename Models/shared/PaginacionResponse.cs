namespace SistemaTurnos.Web.Models.shared;

public class PaginacionResponse<T> where T : class
{
    /// <summary>
    /// Lista de elementos de la página actual.
    /// </summary>
    public List<T> Elementos { get; set; } = new List<T>();

    /// <summary>
    /// Número de la página actual (basado en 1).
    /// </summary>
    public int PaginaActual { get; set; }

    /// <summary>
    /// El número máximo de elementos que se muestran por página.
    /// </summary>
    public int ElementosPorPagina { get; set; }

    /// <summary>
    /// El número total de elementos en toda la colección (no solo en esta página).
    /// </summary>
    public int TotalElementos { get; set; }

    /// <summary>
    /// El número total de páginas disponibles.
    /// </summary>
    public int TotalPaginas { get; set; }

    /// <summary>
    /// Indica si hay una página anterior disponible.
    /// </summary>
    public bool TienePaginaAnterior => PaginaActual > 1;

    /// <summary>
    /// Indica si hay una página siguiente disponible.
    /// </summary>
    public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
}