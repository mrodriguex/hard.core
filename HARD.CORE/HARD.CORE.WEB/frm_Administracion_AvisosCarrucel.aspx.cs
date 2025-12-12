using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public partial class frm_Administracion_AvisosCarrucel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        Generales.GetInstance().LimpiaCache(ref context);

        if (!IsPostBack)
        {
            BindDataToRotator();
            Usuario usuario = (Usuario)Session["Usuario"];
        }
    }

    private void BindDataToRotator()
    {
        List<Aviso> lista = AvisoSER.GetInstance().ObtenerActivosLista();

        if (lista != null && lista.Any())
        {
            int anchoMaximo = 600;
            int altoMaximo = 600;

            var images = lista.Select(a => new
            {
                ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(
                    RedimensionarImagen(a.ImagenByte, anchoMaximo, altoMaximo)
                ),
                ImageAlt = a.Titulo ?? "Imagen"
            }).ToList();

            radCoverFlow.DataSource = images;
            radCoverFlow.DataBind();
        }
        else
        {
            CardVacioCarrusel.Visible = true;
            radCoverFlow.Visible = false;
        }              
}

    public static byte[] RedimensionarImagen(byte[] originalBytes, int anchoMaximo, int altoMaximo)
    {
        using (var inputStream = new MemoryStream(originalBytes))
        using (var imagenOriginal = System.Drawing.Image.FromStream(inputStream))
        {
            int nuevoAncho = anchoMaximo;
            int nuevoAlto = altoMaximo;
            double relacionAspecto = (double)imagenOriginal.Width / imagenOriginal.Height;

            if (imagenOriginal.Width > imagenOriginal.Height)
            {
                nuevoAlto = (int)(nuevoAncho / relacionAspecto);
            }
            else
            {
                nuevoAncho = (int)(nuevoAlto * relacionAspecto);
            }

            // Evitar agrandar la imagen original
            if (nuevoAncho > imagenOriginal.Width || nuevoAlto > imagenOriginal.Height)
            {
                nuevoAncho = imagenOriginal.Width;
                nuevoAlto = imagenOriginal.Height;
            }

            using (var imagenRedimensionada = new Bitmap(nuevoAncho, nuevoAlto, PixelFormat.Format32bppArgb))
            using (var g = Graphics.FromImage(imagenRedimensionada))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(imagenOriginal, 0, 0, nuevoAncho, nuevoAlto);

                using (var outputStream = new MemoryStream())
                {
                    var qualityParam = new EncoderParameter(Encoder.Quality, 100L); // Calidad máxima
                    var jpegCodec = GetEncoder(ImageFormat.Jpeg);
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = qualityParam;

                    imagenRedimensionada.Save(outputStream, jpegCodec, encoderParams);
                    return outputStream.ToArray();
                }
            }
        }
    }

    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
    }
}