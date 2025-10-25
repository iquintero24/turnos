using QRCoder;
using SistemaTurnos.Web.Utilities.interfaces;
using System.Drawing; // Se utiliza la clase Color

namespace SistemaTurnos.Web.Utilities
{
    // Esta implementación utiliza la librería QRCoder, que es la solución moderna y compatible 
    // con múltiples plataformas para generar imágenes sin dependencias nativas problemáticas (como libgdiplus).
    public class QrHelper : IQRHelper
    {
        // Colores por defecto para el QR (negro y blanco)
        private static readonly Color DarkColor = Color.FromArgb(255, 0, 0, 0); // Negro
        private static readonly Color LightColor = Color.FromArgb(255, 255, 255, 255); // Blanco
        private const int PixelsPerModule = 10; // Tamaño de celda por defecto

        public byte[]? GenerarPngQrCode(string qrContentUrl)
        {
            if (string.IsNullOrEmpty(qrContentUrl))
            {
                // Devolver null si la URL a codificar está vacía.
                return null;
            }

            try
            {
                // Paso 1: Crear la data del QR
                using var qrGenerator = new QRCodeGenerator();
                // Nivel de corrección de error: Q (25% - un buen balance entre tamaño y robustez de lectura)
                using var qrCodeData = qrGenerator.CreateQrCode(qrContentUrl, QRCodeGenerator.ECCLevel.Q);
                
                // Paso 2: Generar la imagen como bytes PNG
                // PngByteQRCode permite obtener directamente un array de bytes PNG.
                using var qrCode = new PngByteQRCode(qrCodeData);
                
                // Genera la imagen como un array de bytes PNG
                byte[] qrBytes = qrCode.GetGraphic(
                    pixelsPerModule: PixelsPerModule, 
                    darkColor: DarkColor, 
                    lightColor: LightColor
                );

                return qrBytes;
            }
            catch (Exception ex)
            {
                // Capturar y registrar el error.
                // En un entorno de producción real, se usaría un logger (ej. ILogger).
                Console.WriteLine($"Error al generar el código QR: {ex.Message}");
                return null;
            }
        }
    }
}