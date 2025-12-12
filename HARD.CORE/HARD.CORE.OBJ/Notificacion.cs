using System;
using System.Collections.Generic;

namespace HARD.CORE.OBJ
{
    public class Notificacion
    {
        public int ClaveNotificacion { get; set; }
        public List<NotificacionDetalle> NotificacionDetalle { get; set; }
        public TipoContenido TipoContenido { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Descripcion { get; set; }
        public bool Imagen { get; set; }
        public string NombreImagen { get; set; }
        public byte[] ImagenByte { get; set; }
        public string RutaImagen { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public string ClaveUsuarioActualizacion { get; set; }
        public EnumAccion EnumAccion { get; set; }

        public Notificacion() { }


        public Notificacion(int claveNotificacion, List<NotificacionDetalle> notificacionDetalle, TipoContenido tipoContenido, string titulo, string subtitulo
           , string descripcion, bool imagen, string nombreImagen, string rutaImagen, byte[] imagenByte, bool estatus, DateTime fechaInicioVigencia, DateTime fechaFinVigencia
           , string claveUsuarioActualizacion, EnumAccion enumAccion)
        {
            ClaveNotificacion = claveNotificacion;
            NotificacionDetalle = notificacionDetalle;
            TipoContenido = tipoContenido;
            Titulo = titulo;
            Subtitulo = subtitulo;
            Descripcion = descripcion;
            Imagen = imagen;
            NombreImagen = nombreImagen;
            ImagenByte = imagenByte;
            RutaImagen = rutaImagen;
            Estatus = estatus;
            FechaInicioVigencia = fechaInicioVigencia;
            FechaFinVigencia = fechaFinVigencia;
            ClaveUsuarioActualizacion = claveUsuarioActualizacion;
            EnumAccion = enumAccion;
        }
    }
}