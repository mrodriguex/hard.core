using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class NotificacionDetalle
    {
        public int ClaveNotificacionDetalle { get; set; }
        public int ClaveNotificacion { get; set; }
        public string ClaveUsuario { get; set; }
        public bool Visto { get; set; }
        public string ClaveUsuarioActualizacion { get; set; }
        public EnumAccion EnumAccion { get; set; }

        public NotificacionDetalle() { }

        public NotificacionDetalle(int claveNotificacionDetalle, int claveNotificacion, string claveUsuario, bool visto, EnumAccion enumAccion)
        {
            ClaveNotificacionDetalle = claveNotificacionDetalle;
            ClaveNotificacion = claveNotificacion;
            ClaveUsuario = claveUsuario;
            Visto = visto;
            EnumAccion = enumAccion;
        }

    }
}
