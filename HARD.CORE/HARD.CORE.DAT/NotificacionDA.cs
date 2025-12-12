using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;

using static HARD.CORE.DAT.NotificacionDA;

namespace HARD.CORE.DAT
{
    public class NotificacionDA : INotificacionDA
    {

        private readonly string _connectionString;
        private readonly IArchivosDA _archivosDA;

        public NotificacionDA(IConfiguration configuration, IArchivosDA archivosDA)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
            _archivosDA = archivosDA;
        }


        #region Metodos_Privados

        /// <summary>
        /// Initializes a new instance of the <see cref="AvisoDA"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration to use.
        /// </param>
        /// <remarks>
        /// Initializes a new instance of the <see cref="AvisoDA"/> class.
        /// </remarks>

        public bool Actualizar(Notificacion notificacion)
        {
            throw new NotImplementedException();
        }

        private DataTable ObtenerDB(int? claveNotificacion = null, bool? estatus = null, string claveUsuario = "")
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Notificacion_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (claveNotificacion.HasValue) { cmd.Parameters.AddWithValue("@ClaveNotificacion", claveNotificacion); }
                if (estatus.HasValue) { cmd.Parameters.AddWithValue("@Estatus", estatus); }
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        private Notificacion ObtenerOBJ(int? claveNotificacion, bool estatus = false)
        {
            DataTable dt = ObtenerDB(claveNotificacion, estatus);
            DataRow dr = dt.Rows[0];
            Notificacion notificacion = new Notificacion(
                (int)dr["Clavenotificacion"]
                , NotificacionDetalleDA.GetInstance().Obtener((int)dr["ClaveNotificacion"])
                , TiposContenidoDA.GetInstance().Obtener((int)dr["ClaveTipoContenido"])
                , dr["Titulo"].ToString()
                , dr["Subtitulo"].ToString()
                , dr["Descripcion"].ToString()
                , Convert.ToBoolean(dr["Imagen"])
                , dr["NombreImagen"].ToString()
                , dr["RutaImagen"].ToString()
                , null
                , Convert.ToBoolean(dr["Estatus"])
                , Convert.ToDateTime(dr["FechaInicioVigencia"])
                , Convert.ToDateTime(dr["FechaFinVigencia"])
                , dr["ClaveUsuarioActualizacion"].ToString()
                , EnumAccion.SinCambio
                );

            return notificacion;
        }

        private DataTable ObtenerUsuariosPermitidosDB(int? claveNotificacion = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Notificacion_ObtenerUsuariosPermitidos";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (claveNotificacion.HasValue) { cmd.Parameters.AddWithValue("@ClaveNotificacion", claveNotificacion); }

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            DataTable dt = (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
            dt.Columns.Add("Accion");
            dt.Rows.Cast<DataRow>().ToList().ForEach(r => r["Accion"] = "0");
            return dt;
        }

        private DataTable ObtenerPorUsuarioDB(string claveUsuario)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Notificacion_ObtenerPorUsuario";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        private List<Notificacion> ObtenerPorUsuarioOBJ(string claveUsuario)
        {
            List<Notificacion> notificaciones = new List<Notificacion>();
            DataTable dt = ObtenerPorUsuarioDB(claveUsuario: claveUsuario);
            foreach (DataRow dr in dt.Rows)
            {
                Notificacion notificacion = new Notificacion(
                    (int)dr["Clavenotificacion"]
                    , NotificacionDetalleDA.GetInstance().Obtener((int)dr["ClaveNotificacion"], claveUsuario)
                    , TiposContenidoDA.GetInstance().Obtener((int)dr["ClaveTipoContenido"])
                    , dr["Titulo"].ToString()
                    , dr["Subtitulo"].ToString()
                    , dr["Descripcion"].ToString()
                    , Convert.ToBoolean(dr["Imagen"])
                    , dr["RutaImagen"].ToString()
                    , dr["NombreImagen"].ToString()
                    , null
                    , Convert.ToBoolean(dr["Estatus"])
                    , Convert.ToDateTime(dr["FechaInicioVigencia"])
                    , Convert.ToDateTime(dr["FechaFinVigencia"])
                    , dr["ClaveUsuarioActualizacion"].ToString()
                    , EnumAccion.SinCambio
                    );
                notificaciones.Add(notificacion);
            }
            return notificaciones;
        }
#endregion

        #region Public_Obtener
       public List<Notificacion> ObtenerTodos(bool? estatus = null)
        {
            List<Notificacion> notificaciones = new List<Notificacion>();
            DataTable dt = ObtenerDB(estatus: estatus);
            foreach (DataRow dr in dt.Rows)
            {
                Notificacion notificacion = new Notificacion(
                    (int)dr["Clavenotificacion"]
                    , NotificacionDetalleDA.GetInstance().Obtener((int)dr["ClaveNotificacion"])
                    , TiposContenidoDA.GetInstance().Obtener((int)dr["ClaveTipoContenido"])
                    , dr["Titulo"].ToString()
                    , dr["Subtitulo"].ToString()
                    , dr["Descripcion"].ToString()
                    , Convert.ToBoolean(dr["Imagen"])
                    , dr["NombreImagen"].ToString()
                    , dr["RutaImagen"].ToString()
                    , dr["RutaImagen"] != DBNull.Value ? _archivosDA.LeerArchivo(dr["RutaImagen"].ToString(), null) : null
                    , Convert.ToBoolean(dr["Estatus"])
                    , Convert.ToDateTime(dr["FechaInicioVigencia"])
                    , Convert.ToDateTime(dr["FechaFinVigencia"])
                    , dr["ClaveUsuarioActualizacion"].ToString()
                    , EnumAccion.SinCambio
                );
                notificaciones.Add(notificacion);
            }
            return notificaciones;
        }

        public Notificacion Obtener(int claveNotificacion)
        {
            return ObtenerOBJ(claveNotificacion);
        }

        public List<Notificacion> ObtenerUsuariosPermitidos(int? claveNotificacion = null)
        {
            DataTable dt = ObtenerUsuariosPermitidosDB(claveNotificacion);
            List<Notificacion> notificaciones = new List<Notificacion>();
            foreach (DataRow dr in dt.Rows)
            {
                Notificacion notificacion = new Notificacion(
                    dr.Table.Columns.Contains("Clavenotificacion") && dr["Clavenotificacion"] != DBNull.Value ? Convert.ToInt32(dr["Clavenotificacion"]) : 0,
                    null, // NotificacionDetalle, set to null or fetch as needed
                    null, // TiposContenido, set to null or fetch as needed
                    dr.Table.Columns.Contains("Titulo") ? dr["Titulo"].ToString() : "",
                    dr.Table.Columns.Contains("Subtitulo") ? dr["Subtitulo"].ToString() : "",
                    dr.Table.Columns.Contains("Descripcion") ? dr["Descripcion"].ToString() : "",
                    dr.Table.Columns.Contains("Imagen") && dr["Imagen"] != DBNull.Value ? Convert.ToBoolean(dr["Imagen"]) : false,
                    dr.Table.Columns.Contains("NombreImagen") ? dr["NombreImagen"].ToString() : "",
                    dr.Table.Columns.Contains("RutaImagen") ? dr["RutaImagen"].ToString() : "",
                    null, // ImagenByte, set to null or fetch as needed
                    dr.Table.Columns.Contains("Estatus") && dr["Estatus"] != DBNull.Value ? Convert.ToBoolean(dr["Estatus"]) : false,
                    dr.Table.Columns.Contains("FechaInicioVigencia") && dr["FechaInicioVigencia"] != DBNull.Value ? Convert.ToDateTime(dr["FechaInicioVigencia"]) : DateTime.MinValue,
                    dr.Table.Columns.Contains("FechaFinVigencia") && dr["FechaFinVigencia"] != DBNull.Value ? Convert.ToDateTime(dr["FechaFinVigencia"]) : DateTime.MinValue,
                    dr.Table.Columns.Contains("ClaveUsuarioActualizacion") ? dr["ClaveUsuarioActualizacion"].ToString() : "",
                    EnumAccion.SinCambio
                );
                notificaciones.Add(notificacion);
            }
            return notificaciones;
        }

        public List<Notificacion> ObtenerPorUsuario(string claveUsuario)
        {
            return ObtenerPorUsuarioOBJ(claveUsuario);
        }
        #endregion

        #region Cambio_en_Basee
        public int Insertar(Notificacion notificacion)
        {
            int clave = 0;
            if (notificacion.EnumAccion == EnumAccion.Insertar)
            {
                string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
                SqlConnection connection = new SqlConnection(sqlConn);
                try
                {
                    connection.Open(); SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        string rutaCarpeta = "";
                        string queryName = "Notificacion_Insertar";

                        SqlCommand cmd = new SqlCommand(queryName, connection, transaction); cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ClaveTipoContenido", notificacion.TipoContenido.ClaveTipoContenido);
                        cmd.Parameters.AddWithValue("@Titulo", notificacion.Titulo);
                        cmd.Parameters.AddWithValue("@Subtitulo", notificacion.Subtitulo);
                        cmd.Parameters.AddWithValue("@Descripcion", notificacion.Descripcion.Trim());
                        cmd.Parameters.AddWithValue("@Imagen", notificacion.Imagen);
                        cmd.Parameters.AddWithValue("@NombreImagen", notificacion.NombreImagen);
                        cmd.Parameters.AddWithValue("@RutaImagen", notificacion.Imagen == false ? "" : _archivosDA.GuardarArchivo(notificacion.ImagenByte, notificacion.NombreImagen, rutaCarpeta));
                        cmd.Parameters.AddWithValue("@Estatus", notificacion.Estatus);
                        cmd.Parameters.AddWithValue("@FechaInicioVigencia", notificacion.FechaInicioVigencia);
                        cmd.Parameters.AddWithValue("@FechaFinVigencia", notificacion.FechaFinVigencia);
                        cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", notificacion.ClaveUsuarioActualizacion);

                        SqlParameter parameter = new SqlParameter("@ClaveNotificacion", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                        cmd.ExecuteNonQuery();

                        foreach (NotificacionDetalle notificacionDetalle in notificacion.NotificacionDetalle)
                        {
                            notificacionDetalle.ClaveNotificacion = Convert.ToInt32(parameter.Value);
                            NotificacionDetalleDA.GetInstance().Insertar(notificacionDetalle, connection, transaction);
                        }

                        transaction.Commit();
                        clave = Convert.ToInt32(parameter.Value);
                    }
                    catch (Exception ex) { transaction.Rollback(); throw ex; }
                }
                catch (Exception ex) { throw ex; }
                finally { connection.Close(); }
            }
            return clave;
        }

        public bool Actualiza(Notificacion notificacion)
        {
            bool clave = false;
            string rutaImagenNueva = "";
            string rutaCarpeta = "";
            if (notificacion.EnumAccion == EnumAccion.Actualizar)
            {
                string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
                SqlConnection connection = new SqlConnection(sqlConn);
                try
                {
                    connection.Open(); SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        if (notificacion.Imagen) { rutaImagenNueva = _archivosDA.GuardarArchivo(notificacion.ImagenByte, notificacion.NombreImagen, rutaCarpeta); }
                        string queryName = "Notificacion_Actualizar";

                        SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClaveNotificacion", notificacion.ClaveNotificacion);
                        cmd.Parameters.AddWithValue("@ClaveTipoContenido", notificacion.TipoContenido.ClaveTipoContenido);
                        cmd.Parameters.AddWithValue("@Titulo", notificacion.Titulo);
                        cmd.Parameters.AddWithValue("@Subtitulo", notificacion.Subtitulo);
                        cmd.Parameters.AddWithValue("@Descripcion", notificacion.Descripcion);
                        cmd.Parameters.AddWithValue("@Imagen", notificacion.Imagen);
                        cmd.Parameters.AddWithValue("@RutaImagen", rutaImagenNueva);
                        cmd.Parameters.AddWithValue("@Estatus", notificacion.Estatus);
                        cmd.Parameters.AddWithValue("@FechaInicioVigencia", notificacion.FechaInicioVigencia);
                        cmd.Parameters.AddWithValue("@FechaFinVigencia", notificacion.FechaFinVigencia);
                        cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", notificacion.ClaveUsuarioActualizacion);
                        SqlParameter parameter = new SqlParameter("@Respuesta", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                        cmd.ExecuteNonQuery();

                        clave = Convert.ToBoolean(parameter.Value);

                        foreach (NotificacionDetalle notificacionDetalle in notificacion.NotificacionDetalle.AsEnumerable().Where(x => x.EnumAccion != EnumAccion.SinCambio).ToList())
                        {
                            if (notificacionDetalle.EnumAccion == EnumAccion.Insertar)
                            {
                                NotificacionDetalleDA.GetInstance().Insertar(notificacionDetalle, connection, transaction);
                            }
                            else if (notificacionDetalle.EnumAccion == EnumAccion.Actualizar)
                            {
                                NotificacionDetalleDA.GetInstance().Actualiza(notificacionDetalle, connection, transaction);
                            }
                            else if (notificacionDetalle.EnumAccion == EnumAccion.Eliminar)
                            {
                                NotificacionDetalleDA.GetInstance().Eliminar(notificacionDetalle, connection, transaction);
                            }
                        }
                        transaction.Commit();
                        if (notificacion.RutaImagen != "") { _archivosDA.EliminarArchivo(rutaCarpeta + notificacion.RutaImagen); }
                        clave = true;
                    }
                    catch (Exception ex) { transaction.Rollback(); if (rutaImagenNueva != "") { _archivosDA.EliminarArchivo(rutaCarpeta + rutaImagenNueva); }; throw ex; }
                }
                catch (Exception ex) { throw ex; }
                finally { connection.Close(); }
            }
            return clave;
        }
        #endregion
    }
}
