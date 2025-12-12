using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface INotificacionDA
    {
        List<Notificacion> ObtenerTodos(bool? estatus = null);
        Notificacion Obtener(int claveNotificacion);
        List<Notificacion> ObtenerUsuariosPermitidos(int? claveNotificacion = null);
        List<Notificacion> ObtenerPorUsuario(string claveUsuario);
         public int Insertar(Notificacion notificacion);
        bool Actualizar(Notificacion notificacion);

    }
}
