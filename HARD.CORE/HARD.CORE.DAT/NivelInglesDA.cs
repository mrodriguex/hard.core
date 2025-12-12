using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT
{
    public class NivelInglesDA : INivelInglesDA
    {
        private readonly string _connectionString;

        public NivelInglesDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerNivelInglesDB(int? claveNivelIngles = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NivelIngles_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveNivelIngles", claveNivelIngles);
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }
        private List<NivelIngles> ObtenerNivelIngles(int? claveNivelIngles = null, bool? estatus = null)
        {
            List<NivelIngles> nivelIngles = new List<NivelIngles>();
            DataTable nivelInglesdt = ObtenerNivelInglesDB(claveNivelIngles, estatus);

            foreach (DataRow dr in nivelInglesdt.Rows)
            {
                NivelIngles nivelInglesItem = new NivelIngles();
                nivelInglesItem.ClaveNivelIngles = (int)dr["ClaveNivelIngles"];
                nivelInglesItem.Descripcion = dr["Descripcion"].ToString();
                nivelInglesItem.Estatus = (bool)dr["Estatus"];
                nivelInglesItem.ClaveUsuarioUltimaActualizacion = dr["ClaveUsuarioUltimaActualizacion"].ToString();
                nivelIngles.Add(nivelInglesItem);
            }

            return nivelIngles;
        }
        private int InsertarDB(NivelIngles nivelIngles)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NivelIngles_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Descripcion", nivelIngles.Descripcion);
                cmd.Parameters.AddWithValue("@Estatus", nivelIngles.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", nivelIngles.ClaveUsuarioUltimaActualizacion);

                SqlParameter parameter = new SqlParameter("@ClaveNivelIngles", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        private bool ActualizarDB(NivelIngles nivelIngles)
        {
            string sqlConn = _connectionString;
            bool actualizo = false;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NivelIngles_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveNivelIngles", nivelIngles.ClaveNivelIngles);
                cmd.Parameters.AddWithValue("@Descripcion", nivelIngles.Descripcion);
                cmd.Parameters.AddWithValue("@Estatus", nivelIngles.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", nivelIngles.ClaveUsuarioUltimaActualizacion);

                cmd.ExecuteNonQuery();
                actualizo = true;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return actualizo;
        }
        #endregion

        #region Public
        public List<NivelIngles> ObtenerTodos(bool? estatus = null)
        {
            List<NivelIngles> nivelIngles = ObtenerNivelIngles(null, estatus);
            return nivelIngles;
        }
        public NivelIngles Obtener(int ClaveNivelIngles)
        {
            List<NivelIngles> nivelIngles = ObtenerNivelIngles(ClaveNivelIngles);
            return nivelIngles.FirstOrDefault();
        }
        #endregion

        #region Cambio_en_base
        public int Insertar(NivelIngles nivelIngles)
        {
            return InsertarDB(nivelIngles);
        }
        public bool Actualizar(NivelIngles nivelIngles)
        {
            return ActualizarDB(nivelIngles);
        }
        #endregion

    }
}
