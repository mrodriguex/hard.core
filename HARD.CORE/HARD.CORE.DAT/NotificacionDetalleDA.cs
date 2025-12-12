using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HARD.CORE.DAT
{
    public class NotificacionDetalleDA
    {
        #region "Singleton"
        private static NotificacionDetalleDA instance = null; private static object mutex = new object(); private NotificacionDetalleDA() { }
        public static NotificacionDetalleDA GetInstance() { if (instance == null) { lock ((mutex)) { instance = new NotificacionDetalleDA(); } } return instance; }
        #endregion

        #region Privat
        private DataTable ObtenerNotificacionDetalleDB(int? claveNotificacion = null, int? claveNotificacionDetalle = null, string claveUsuario = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "NotificacionDetalle_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (claveNotificacionDetalle.HasValue) { cmd.Parameters.AddWithValue("@ClaveNotificacionDetalle", claveNotificacion); }
                if (claveNotificacion.HasValue) { cmd.Parameters.AddWithValue("@ClaveNotificacion", claveNotificacion); }
                if (claveUsuario != null) { cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario); }

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        private List<NotificacionDetalle> ObtenerNotificacionesDetalleOBJ(int? claveNotificacion = null, int? claveNotificacionDetalle = null, string claveUsuario = null)
        {
            List<NotificacionDetalle> NotificacionesDetalle = new List<NotificacionDetalle>();
            DataTable dtDetalle = ObtenerNotificacionDetalleDB(claveNotificacion, claveNotificacionDetalle, claveUsuario);
            foreach (DataRow dr in dtDetalle.Rows)
            {
                NotificacionDetalle notificacionDetalle = new NotificacionDetalle(
                    (int)dr["ClaveNotificacionDetalle"]
                    , (int)dr["ClaveNotificacion"]
                    , dr["ClaveUsuario"].ToString()
                    , Convert.ToBoolean(dr["Visto"])
                    , EnumAccion.SinCambio
                    );
                NotificacionesDetalle.Add(notificacionDetalle);
            }


            return NotificacionesDetalle;
        }

        private bool EliminarDB(int claveNotificacionDetalle, string claveUsuarioActualizacion)
        {
            bool clave = false;
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NotificacionDetalle_Eliminar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveNotificacionDetalle", claveNotificacionDetalle);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", claveUsuarioActualizacion);
                SqlParameter parameter = new SqlParameter("@RespuestaEliminar", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                clave = Convert.ToBoolean(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return clave;
        }

        private bool EliminarDB(int claveNotificacionDetalle, string claveUsuarioActualizacion, SqlConnection connection, SqlTransaction transaction)
        {
            bool clave = false;
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            try
            {
                string queryName = "NotificacionDetalle_Eliminar";

                SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveNotificacionDetalle", claveNotificacionDetalle);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", claveUsuarioActualizacion);
                SqlParameter parameter = new SqlParameter("@RespuestaEliminar", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                clave = Convert.ToBoolean(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            return clave;
        }

        #endregion

        #region Public_Obtener
        DataTable ObtenerTodos()
        {
            return ObtenerNotificacionDetalleDB();
        }

        public DataTable ObtenerActivos()
        {
            return ObtenerNotificacionDetalleDB();
        }

        public List<NotificacionDetalle> Obtener(int? claveNotificacion,string claveUsuario = null)
        {
            return ObtenerNotificacionesDetalleOBJ(claveNotificacion,claveUsuario: claveUsuario);
        }
        #endregion

        #region Cambio_en_Basee
        public int Insertar(NotificacionDetalle notificacionDetalle, SqlConnection connection, SqlTransaction transaction)
        {
            int clave = 0;
            if (notificacionDetalle.EnumAccion == EnumAccion.Insertar)
            {
                try
                {
                    string queryName = "NotificacionDetalle_Insertar";

                    SqlCommand cmd = new SqlCommand(queryName, connection, transaction); cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClaveNotificacion", notificacionDetalle.ClaveNotificacion);
                    cmd.Parameters.AddWithValue("@ClaveUsuario", notificacionDetalle.ClaveUsuario.Trim());
                    cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", notificacionDetalle.ClaveUsuarioActualizacion);
                    SqlParameter parameter = new SqlParameter("@ClaveNotificacionDetalle", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                    cmd.ExecuteNonQuery();
                    clave = Convert.ToInt32(parameter.Value);
                }
                catch (Exception ex) { throw ex; }
            }
            return clave;
        }

        public bool Actualiza(NotificacionDetalle notificacionDetalle, SqlConnection connection, SqlTransaction transaction)
        {
            bool clave = false;
            if (notificacionDetalle.EnumAccion == EnumAccion.Actualizar)
            {
                string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
                try
                {
                    string queryName = "NotificacionDetalle_Actualizar";

                    SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClaveNotificacionDetalle", notificacionDetalle.ClaveNotificacionDetalle);
                    cmd.Parameters.AddWithValue("@ClaveNotificacion", notificacionDetalle.ClaveNotificacion);
                    cmd.Parameters.AddWithValue("@ClaveUsuario", notificacionDetalle.ClaveUsuario.Trim());
                    cmd.Parameters.AddWithValue("@Visto", notificacionDetalle.Visto);
                    cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", notificacionDetalle.ClaveUsuarioActualizacion);
                    SqlParameter parameter = new SqlParameter("@RespuestaActualizacion", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                    cmd.ExecuteNonQuery();

                    clave = Convert.ToBoolean(parameter.Value);
                }
                catch (Exception ex) { throw ex; }
            }
            return clave;
        }

        public bool ActualizaVisto(int claveNotificacion, string claveUsuario, bool visto, string claveUsuarioActualizacion)
        {
            bool clave = false;
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "NotificacionDetalle_ActualizarVisto";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveNotificacion", claveNotificacion);
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                cmd.Parameters.AddWithValue("@Visto", visto);

                SqlParameter parameter = new SqlParameter("@RespuestaActualizacion", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                clave = Convert.ToBoolean(parameter.Value);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return clave;
        }

        public bool Eliminar(int claveNotificacionDetalle, string claveUsuarioActualizacion)
        {
            return EliminarDB(claveNotificacionDetalle, claveUsuarioActualizacion);
        }

        public bool Eliminar(NotificacionDetalle notificacionDetalle)
        {
            return EliminarDB(notificacionDetalle.ClaveNotificacionDetalle, notificacionDetalle.ClaveUsuarioActualizacion);
        }

        public bool Eliminar(NotificacionDetalle notificacionDetalle, SqlConnection connection, SqlTransaction transaction)
        {
            return EliminarDB(notificacionDetalle.ClaveNotificacionDetalle, notificacionDetalle.ClaveUsuarioActualizacion, connection, transaction);
        }
       
        #endregion
    }
}