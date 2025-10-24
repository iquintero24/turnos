using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using SistemaTurnos.Web.Utilities.interfaces;

namespace SistemaTurnos.Web.Utilities;

public class QrHelper:IQRHelper
{
    public byte[]? GenerarPngQrCode(string qrContentUrl)
    {
        using QRCodeGenerator generator = new QRCodeGenerator();
        QRCodeData qrData = generator.CreateQrCode(qrContentUrl, QRCodeGenerator.ECCLevel.Q);

        using QRCode qrCode = new QRCode(qrData);
        using Bitmap qrImage = qrCode.GetGraphic(20);

        using MemoryStream ms = new MemoryStream();
        qrImage.Save(ms, ImageFormat.Png);
        return ms.ToArray();

    }
}