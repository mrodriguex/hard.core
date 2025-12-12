using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class NotificacionB : INotificacionB
    {
        private readonly IConfiguration _configuration;
        private readonly INotificacionDA _notificacionDA;

        public NotificacionB(IConfiguration configuration, INotificacionDA notificacionDA)
        {
            _configuration = configuration;
            _notificacionDA = notificacionDA;
        }


        public Notificacion Obtener(int claveNotificacion)
        {
            return _notificacionDA.Obtener(claveNotificacion);
        }
        public List<Notificacion> ObtenerTodos(bool? estatus = null)
        {
            return _notificacionDA.ObtenerTodos(estatus: estatus);
        }
        public List<Notificacion> ObtenerUsuariosPermitidos(int? claveNotificacion = null)
        {
            return _notificacionDA.ObtenerUsuariosPermitidos(claveNotificacion);
        }
        public List<Notificacion> ObtenerPorUsuario(string claveUsuario)
        {
            return _notificacionDA.ObtenerPorUsuario(claveUsuario);
        }
        public bool ExistenPendientesPorLeer(string claveUsuario)
        {
            return ObtenerPendientesPorLeer(claveUsuario, false, false).Count > 0;
        }
        private List<Notificacion> ObtenerPendientesPorLeer(string claveUsuario, bool leido, bool todos)
        {
            List<Notificacion> listaNotificaciones = ObtenerPorUsuario(claveUsuario);
            List<Notificacion> listaFiltrada = new List<Notificacion>();

            foreach (Notificacion notificacion in listaNotificaciones)
            {
                foreach (NotificacionDetalle detalle in notificacion.NotificacionDetalle.Where(x => x.Visto == leido))
                {
                    listaFiltrada.Add(notificacion);
                    if (!todos) //Con que encuentre el primero
                        break;
                }
            }
            return listaFiltrada;
        }
        public bool ConClientesEspecificos(int claveNotificacion)
        {
            var usuarios = _notificacionDA.ObtenerUsuariosPermitidos(claveNotificacion);
            // foreach (var usuario in usuarios)
            // {
            //     // Assuming Notificacion has a property 'Asignado'
            //     if (!usuario.Asignado)
            //         return true;
            // }
            return false;
        }
        public int Insertar(Notificacion notificacion)
        {
            if (notificacion.NotificacionDetalle == null)
            {
                List<Notificacion> usuariosPermitidos = _notificacionDA.ObtenerUsuariosPermitidos();
                List<NotificacionDetalle> listaUsuarios = new List<NotificacionDetalle>();
                foreach (var usuario in usuariosPermitidos)
                {
                    NotificacionDetalle detalle = new NotificacionDetalle(0,
                                                                           notificacion.ClaveNotificacion,
                                                                           usuario.ClaveUsuarioActualizacion,
                                                                           false,
                                                                           EnumAccion.Insertar
                                                                        );
                    detalle.ClaveUsuarioActualizacion = notificacion.ClaveUsuarioActualizacion;
                    listaUsuarios.Add(detalle);
                }

                notificacion.NotificacionDetalle = listaUsuarios;
            }

            return _notificacionDA.Insertar(notificacion);
        }
        public bool Actualizar(Notificacion notificacion)
        {
            if (notificacion.NotificacionDetalle == null) //Se indica que se asigne a todos los usuarios a esa notificación
            {
                List<Notificacion> usuariosPermitidos = _notificacionDA.ObtenerUsuariosPermitidos(notificacion.ClaveNotificacion);
                List<NotificacionDetalle> listaUsuarios = new List<NotificacionDetalle>();
                foreach (var usuario in usuariosPermitidos)
                {
                    NotificacionDetalle detalle = new NotificacionDetalle(0,
                                                                          notificacion.ClaveNotificacion,
                                                                          usuario.ClaveUsuarioActualizacion,
                                                                          false,
                                                                          EnumAccion.Insertar
                                                                       );

                    detalle.ClaveUsuarioActualizacion = notificacion.ClaveUsuarioActualizacion;
                    listaUsuarios.Add(detalle);
                }

                notificacion.NotificacionDetalle = listaUsuarios;
            }

            return _notificacionDA.Actualizar(notificacion);
        }

        public bool MarcarComoVisto(NotificacionDetalle notificacionDetalle)
        {
            return NotificacionDetalleDA.GetInstance().ActualizaVisto(notificacionDetalle.ClaveNotificacion, notificacionDetalle.ClaveUsuario, true, notificacionDetalle.ClaveUsuarioActualizacion);

        }

        public WebResult<string> EnviarCorreoReporte(int claveCorreo, string rutaNombreAdjunto)
        {
            throw new NotImplementedException();
        }
    }
}
