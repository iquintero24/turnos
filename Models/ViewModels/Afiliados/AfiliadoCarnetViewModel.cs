namespace SistemaTurnos.Web.Models.ViewModels.Afiliados;

public class AfiliadoCarnetViewModel
{
    public int Id { get; set; }
    
    public string Documento { get; set; } = string.Empty;
    
    public string Nombre { get; set; } = string.Empty;
    
    public string Correo { get; set; } = string.Empty;
    
    public string Direccion { get; set; } = string.Empty;
    
    /// <summary>
    /// URL de la foto del afiliado.
    /// </summary>
    public string FotoUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// El código QR codificado en Base64 para incrustar directamente en la etiqueta IMG.
    /// </summary>
    public string QrCodePngBase64 { get; set; } = string.Empty;
}