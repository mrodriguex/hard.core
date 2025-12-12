using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using HARD.CORE.DAT.Interfaces;
using System.Linq;
using System.IO;

namespace HARD.CORE.DAT
{
    public class AvisoDA : IAvisoDA
    {

        private readonly string _connectionString;
        private readonly string _rutaArchivos;
        private readonly IArchivosDA _archivosDA;

        public AvisoDA(IConfiguration configuration, IArchivosDA archivosDA)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
            _rutaArchivos = configuration.GetSection("RutasArchivos")["Avisos"].ToString();
            _archivosDA = archivosDA;
        }

        #region Private
        private DataTable ObtenerDB(int? claveAviso = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Avisos_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (claveAviso.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveAviso", claveAviso);
                }
                if (estatus.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Estatus", estatus);
                }

                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }
        private List<Aviso> ObtenerAvisos(int? claveAviso = null, bool? estatus = null)
        {
            DataTable dt = ObtenerDB(claveAviso, estatus);
            List<Aviso> avisos = new List<Aviso>();

            foreach (DataRow dr in dt.Rows)
            {
                Aviso aviso = new Aviso(
                        (int)dr["ClaveAviso"]
                        , dr["Titulo"].ToString()
                        , dr["Descripcion"].ToString()
                        , (DateTime)dr["FechaInicial"]
                        , (DateTime)dr["FechaFinal"]
                        , (bool)dr["Visible"]
                        , dr["NombreImagen"].ToString()
                        , dr["RutaImagen"].ToString()
                        , _archivosDA.LeerArchivo(dr["RutaImagen"].ToString(), _rutaArchivos)
                        , dr["ClaveUsuarioUltimaActualizacion"].ToString()
                    );
                avisos.Add(aviso);
            }
            return avisos;
        }
        private int InsertarDB(Aviso Aviso)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Avisos_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Titulo", Aviso.Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", Aviso.Descripcion);
                cmd.Parameters.AddWithValue("@NombreImagen", Aviso.NombreImagen);
                cmd.Parameters.AddWithValue("@RutaImagen", _archivosDA.GuardarArchivo(Aviso.ImagenByte, Aviso.NombreImagen, _rutaArchivos));
                cmd.Parameters.AddWithValue("@Visible", Aviso.Visible);
                cmd.Parameters.AddWithValue("@FechaInicial", Aviso.FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", Aviso.FechaFinal);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", Aviso.ClaveUsuarioActualizacion);

                SqlParameter parameter = new SqlParameter("@ClaveAviso", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }
        private bool ActualizarDB(Aviso Aviso)
        {
            bool actualizo = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Avisos_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveAviso", Aviso.ClaveAviso);
                cmd.Parameters.AddWithValue("@Titulo", Aviso.Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", Aviso.Descripcion);
                cmd.Parameters.AddWithValue("@NombreImagen", Aviso.NombreImagen);
                cmd.Parameters.AddWithValue("@RutaImagen", _archivosDA.ActualizarArchivo(Aviso.ImagenByte, Aviso.NombreImagen, _rutaArchivos, Aviso.RutaImagen));
                cmd.Parameters.AddWithValue("@FechaInicial", Aviso.FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", Aviso.FechaFinal);
                cmd.Parameters.AddWithValue("@Visible", Aviso.Visible);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", Aviso.ClaveUsuarioActualizacion);

                cmd.ExecuteNonQuery();

                actualizo = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return actualizo;
        }
        private bool EliminarDB(Aviso Aviso)
        {
            bool respuestaEliminar = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Avisos_Eliminar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveAviso", Aviso.ClaveAviso);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", Aviso.ClaveUsuarioActualizacion);

                cmd.ExecuteNonQuery();
                respuestaEliminar = true;

                if (respuestaEliminar)
                    _archivosDA.EliminarArchivo(Path.Combine(_rutaArchivos, Aviso.RutaImagen));
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return respuestaEliminar;
        }
        #endregion

        #region Public_Obtener
        public List<Aviso> ObtenerTodos(bool? estatus = null)
        {
            return ObtenerAvisos(null, estatus);
        }
        public List<Aviso> ObtenerActivosLista()
        {
            return ObtenerAvisos(null, true);
        }
        public Aviso Obtener(int claveAviso)
        {
            return ObtenerAvisos(claveAviso).FirstOrDefault();
        }
        #endregion

        #region Cambio_en_Basee
        public int Insertar(Aviso Aviso)
        {
            return InsertarDB(Aviso);
        }
        public bool Actualizar(Aviso Aviso)
        {
            return ActualizarDB(Aviso);
        }
        public bool Eliminar(Aviso Aviso)
        {
            return EliminarDB(Aviso);
        }

        #endregion
    }
}
