using HARD.CORE.SER;
using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarRadRotator();
        }
    }
    private void CargarRadRotator()
    {

        var rutasImagenes = AvisoSER.GetInstance().ObtenerActivosLista().Select(a =>
        {
            var imagenRedimensionada = RedimensionarImagenCalidad(a.ImagenByte, 650, 400);
            return "data:image/png;base64," + Convert.ToBase64String(imagenRedimensionada);
        }).ToList();

        if (rutasImagenes != null && rutasImagenes.Count > 0)
        {
            CImagenes.DataSource = rutasImagenes;
            CImagenes.DataBind();
            cardMensaje.Visible = false;
        }
        else
        {
            cardMensaje.Visible = true;
            CImagenes.Visible = false;
        }
    }
    // Método para redimensionar
    private byte[] RedimensionarImagenCalidad(byte[] imagenBytes, int nuevoAncho, int nuevoAlto)
    {
        if (imagenBytes == null || imagenBytes.Length == 0)
            return null;

        using (var ms = new MemoryStream(imagenBytes))
        using (var imagenOriginal = System.Drawing.Image.FromStream(ms))
        using (var bitmap = new System.Drawing.Bitmap(nuevoAncho, nuevoAlto, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
        using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
        {
            // Configuración de máxima calidad
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            // Fondo transparente
            graphics.Clear(System.Drawing.Color.Transparent);

            // Mantener relación de aspecto
            var ratioX = (double)nuevoAncho / imagenOriginal.Width;
            var ratioY = (double)nuevoAlto / imagenOriginal.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var nuevoWidth = (int)(imagenOriginal.Width * ratio);
            var nuevoHeight = (int)(imagenOriginal.Height * ratio);

            var posX = (nuevoAncho - nuevoWidth) / 2;
            var posY = (nuevoAlto - nuevoHeight) / 2;
            var destRect = new System.Drawing.Rectangle(posX, posY, nuevoWidth, nuevoHeight);

            // 🔸 Crear un rectángulo con esquinas redondeadas de 10px
            int cornerRadius = 10;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int r = Math.Min(cornerRadius, Math.Min(nuevoWidth, nuevoHeight) / 2);
            int x = posX, y = posY, w = nuevoWidth, h = nuevoHeight;

            path.AddArc(x, y, r * 2, r * 2, 180, 90);                 // esquina sup. izq
            path.AddArc(x + w - r * 2, y, r * 2, r * 2, 270, 90);     // sup. der
            path.AddArc(x + w - r * 2, y + h - r * 2, r * 2, r * 2, 0, 90); // inf. der
            path.AddArc(x, y + h - r * 2, r * 2, r * 2, 90, 90);      // inf. izq
            path.CloseFigure();

            // 🔸 Aplicar recorte (clip) y dibujar imagen dentro del área redondeada
            graphics.SetClip(path);
            graphics.DrawImage(imagenOriginal, destRect);
            graphics.ResetClip();

            // Guardar como PNG (para conservar transparencia)
            using (var outputMs = new MemoryStream())
            {
                bitmap.Save(outputMs, System.Drawing.Imaging.ImageFormat.Png);
                return outputMs.ToArray();
            }
        }
    }


    private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        var codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        return codecs.FirstOrDefault(codec => codec.MimeType == mimeType);
    }
}