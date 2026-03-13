using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace HARD.CORE.SER
{
    public class ComunesSER
    {

        #region "Singleton"

        private static ComunesSER instance = null;

        private static object mutex = new object();
        private ComunesSER()
        {
        }

        public static ComunesSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new ComunesSER();
                }
            }

            return instance;

        }

        #endregion
        public ExcelPackage ConvertirDataTableExcel(DataTable dtDatos, List<string> nombreColumnas, string nombreArchivo, List<int> columnasTipoFecha = null, List<int> columnasTipoFechaTiempo = null)
        {
            ExcelPackage package = new OfficeOpenXml.ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add(string.Format(nombreArchivo + "_{0}", DateTime.Now.ToString("dd/MM/yyyy")));
            ws.Cells["A1"].LoadFromDataTable(dtDatos, true);

            int filaFinal = ws.Dimension.End.Row;
            int columnaFinal = nombreColumnas.Count;

            //Brindamos estilo a los cabeceros y ajustamos las columnas
            for (int col = 1; col <= columnaFinal; col++)
            {
                var celda = ws.Cells[1, col];

                //Colocamos el nombre correspondiente a la columna.
                celda.Value = nombreColumnas[col - 1];

                //Agregamos estilos a la columna.
                celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00306E"));
                celda.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                celda.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(col).AutoFit();
            }

            //Se implementan estilos a los borden y color de fondo de las celdas.
            for (int row = 2; row <= filaFinal; row++)
            {
                var isEven = (row % 2 == 0);
                var colorFondo = isEven ? "#FFFFFF" : "#FFF1E1"; // blanco y naranja claro

                for (int col = 1; col <= columnaFinal; col++)
                {
                    var celda = ws.Cells[row, col];

                    // Establecer color de fondo alterno
                    celda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    celda.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorFondo));

                    // Agregar bordes superior e inferior
                    var borde = celda.Style.Border;
                    borde.Top.Style = ExcelBorderStyle.Thin;
                    borde.Bottom.Style = ExcelBorderStyle.Thin;
                    borde.Top.Color.SetColor(Color.Gray);
                    borde.Bottom.Color.SetColor(Color.Gray);
                }
            }

            //Cambio de formato a fecha
            if (columnasTipoFecha != null && columnasTipoFecha.Count > 0)
                foreach (var item in columnasTipoFecha)
                    ws.Column(item).Style.Numberformat.Format = "dd-mm-yyyy";

            //Cambio de formato a fecha tiempo
            if (columnasTipoFechaTiempo != null && columnasTipoFechaTiempo.Count > 0)
                foreach (var item in columnasTipoFechaTiempo)
                    ws.Column(item).Style.Numberformat.Format = "dd-mm-yyyy HH:mm";

            return package;
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
}