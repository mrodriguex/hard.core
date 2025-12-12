using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    public interface INotificacionB
    {
        Notificacion Obtener(int claveNotificacion);
        List<Notificacion> ObtenerTodos(bool? estatus = null);
        List<Notificacion> ObtenerUsuariosPermitidos(int? claveNotificacion = null);
        List<Notificacion> ObtenerPorUsuario(string claveUsuario);
        bool ExistenPendientesPorLeer(string claveUsuario);
        int Insertar(Notificacion notificacion);
        bool Actualizar(Notificacion notificacion);
        bool MarcarComoVisto(NotificacionDetalle notificacionDetalle);
        WebResult<string> EnviarCorreoReporte(int claveCorreo, string rutaNombreAdjunto);
    }
}
    