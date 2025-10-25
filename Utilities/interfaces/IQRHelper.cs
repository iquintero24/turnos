using System.Drawing;

namespace SistemaTurnos.Web.Utilities.interfaces;

public interface IQRHelper
{
    /// <summary>
    /// Genera un código QR como un array de bytes PNG.
    /// </summary>
    /// <param name="qrContentUrl">La URL completa que debe ser codificada.</param>
    /// <returns>Array de bytes de la imagen PNG.</returns>
    byte[]? GenerarPngQrCode(string qrContentUrl);
}