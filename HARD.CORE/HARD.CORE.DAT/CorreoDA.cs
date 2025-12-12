using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace HARD.CORE.DAT
{
    public class CorreoDA : ICorreoDA
    {

        private readonly string _connectionString;
        public CorreoDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region private
        public DataTable ObtenerDB(int? claveCorreo = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Correo_ObtenerCorreos";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (claveCorreo.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveCorreo", claveCorreo);
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
        private List<Correo> ObtenerCorreos(int claveCorreo)
        {

            List<Correo> correos = new List<Correo>();
            DataTable correosdt = ObtenerDB(claveCorreo);
            correos = ObtenerLista(correosdt);
            return correos;
        }
        private List<Correo> ObtenerTodosDB(bool? estatus = null)
        {
            List<Correo> correos = new List<Correo>();
            DataTable correosdt = ObtenerDB(null, estatus);
            correos = ObtenerLista(correosdt);
            return correos;
        }
        private List<Correo> ObtenerLista(DataTable source)
        {
            List<Correo> correos = new List<Correo>();

            foreach (DataRow dr in source.Rows)
            {
                Correo correo = new Correo();

                correo.ClaveCorreo = (int)dr["ClaveCorreo"];
                correo.Asunto = dr["Asunto"].ToString();
                correo.Para = dr["Para"].ToString();
                correo.CC = dr["CC"].ToString();
                correo.CCO = dr["CCO"].ToString();
                correo.Titulo = dr["Titulo"].ToString();
                correo.Subtitulo = dr["Subtitulo"].ToString();
                correo.Cuerpo = dr["Cuerpo"].ToString();
                correo.Pie = dr["Pie"].ToString();
                correo.Importancia = dr["Importancia"].ToString();
                correo.Archivos = dr["Archivos"].ToString();
                correo.ClaveTipoCorreo = (int)dr["ClaveTipoCorreo"];
                correo.TipoCorreo = dr["TipoCorreo"].ToString();
                correo.Estatus = (bool)dr["Estatus"];
                correo.ClaveUsuarioActualizacion = dr["ClaveUsuarioActualizacion"].ToString();

                correos.Add(correo);

            }

            return correos;
        }
        private int InsertarDB(Correo correo)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Correo_InsertarCorreo";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Asunto", correo.Asunto);
                cmd.Parameters.AddWithValue("@Para", correo.Para);
                cmd.Parameters.AddWithValue("@CC", correo.CC);
                cmd.Parameters.AddWithValue("@CCO", correo.CCO);
                cmd.Parameters.AddWithValue("@Titulo", correo.Titulo);
                cmd.Parameters.AddWithValue("@Subtitulo", correo.Subtitulo);
                cmd.Parameters.AddWithValue("@Cuerpo", correo.Cuerpo);
                cmd.Parameters.AddWithValue("@Pie", correo.Pie);
                cmd.Parameters.AddWithValue("@Importancia", correo.Importancia);
                cmd.Parameters.AddWithValue("@Archivos", correo.Archivos);
                cmd.Parameters.AddWithValue("@ClaveTipoCorreo", correo.ClaveTipoCorreo);
                cmd.Parameters.AddWithValue("@Estatus", correo.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", correo.ClaveUsuarioActualizacion);

                SqlParameter parameter = new SqlParameter("@ClaveCorreo", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        private bool ActualizarDB(Correo correo)
        {
            bool actualizo = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Correo_ActualizarCorreo";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCorreo", correo.ClaveCorreo);
                cmd.Parameters.AddWithValue("@Asunto", correo.Asunto);
                cmd.Parameters.AddWithValue("@Para", correo.Para);
                cmd.Parameters.AddWithValue("@CC", correo.CC);
                cmd.Parameters.AddWithValue("@CCO", correo.CCO);
                cmd.Parameters.AddWithValue("@Titulo", correo.Titulo);
                cmd.Parameters.AddWithValue("@Subtitulo", correo.Subtitulo);
                cmd.Parameters.AddWithValue("@Cuerpo", correo.Cuerpo);
                cmd.Parameters.AddWithValue("@Pie", correo.Pie);
                cmd.Parameters.AddWithValue("@Importancia", correo.Importancia);
                cmd.Parameters.AddWithValue("@Archivos", correo.Archivos);
                cmd.Parameters.AddWithValue("@ClaveTipoCorreo", correo.ClaveTipoCorreo);
                cmd.Parameters.AddWithValue("@Estatus", correo.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", correo.ClaveUsuarioActualizacion);

                cmd.ExecuteNonQuery();

                actualizo = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return actualizo;
        }
        private bool EnviarCorreoDB(int ClaveCorreo, int ClaveRequisicion, string ClaveUsuario)
        {
            bool envio = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Correo_EnviarCorreo";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCorreo", ClaveCorreo);
                cmd.Parameters.AddWithValue("@ClaveRequisicion", ClaveRequisicion);
                cmd.Parameters.AddWithValue("@ClaveUsuario", ClaveUsuario);

                cmd.ExecuteNonQuery();

                envio = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return envio;


        }
        #endregion

        #region public
        public Correo Obtener(int claveCorreo)
        {
            return ObtenerCorreos(claveCorreo).FirstOrDefault();
        }
        public List<Correo> ObtenerTodos(bool? estatus = false)
        {
            return ObtenerTodosDB(estatus);
        }
        #endregion

        #region Cambios_En_Base
        public int Insertar(Correo correo)
        {
            return InsertarDB(correo);
        }
        public bool Actualizar(Correo correo)
        {
            return ActualizarDB(correo);
        }
        public bool EnviarCorreo(int ClaveCorreo, int ClaveRequisicion, string ClaveUsuario)
        {
            return EnviarCorreoDB(ClaveCorreo, ClaveRequisicion, ClaveUsuario);
        }
        #endregion
    }
}