using System;

using static System.Net.Mime.MediaTypeNames;

namespace HARD.CORE.OBJ
{
    public class Aviso
    {
        private int v1;
        private string v2;
        private string v3;
        private string v4;
        private byte[] bytes;
        private string v5;
        private bool v6;

        public int ClaveAviso { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Visible { get; set; }
        public string ClaveUsuarioActualizacion { get; set; }
        public string NombreImagen { get; set; }
        public string RutaImagen { get; set; }
        public byte[] ImagenByte { get; set; }
        public bool Estatus { get; set; }

        public Aviso() { }

        public Aviso(int claveAviso, string titulo, string descripcion, DateTime fechaInicial, DateTime fechaFinal,
            bool visible, string nombreImagen, string rutaImagen, byte[] imagenByte, string claveUsuarioActualizacion)
        {
            ClaveAviso = claveAviso;
            Titulo = titulo;
            Descripcion = descripcion;
            FechaInicial = fechaInicial;
            FechaFinal = fechaFinal;
            Visible = visible;
            NombreImagen = nombreImagen;
            RutaImagen = rutaImagen;
            ImagenByte = imagenByte;
            ClaveUsuarioActualizacion = claveUsuarioActualizacion;

        }
    }
}