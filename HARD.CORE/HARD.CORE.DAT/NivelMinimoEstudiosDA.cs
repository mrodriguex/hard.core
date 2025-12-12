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
    public class NivelMinimoEstudiosDA : INivelMinimoEstudiosDA
    {
        private readonly string _connectionString;
        public NivelMinimoEstudiosDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerGradoMinimoEstidiosDB(int? claveNivelMinimoEstudios = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NivelMinimoEstudios_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveNivelMinimoEstudios", claveNivelMinimoEstudios);
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
        private List<NivelMinimoEstudios> ObtenerNivelMinimoEstudios(int? ClaveNivelMinimoEstudios = null, bool? estatus = null)
        {
            List<NivelMinimoEstudios> gradoMinimoEstudios = new List<NivelMinimoEstudios>();
            DataTable gradoMinimoEstudiosdt = ObtenerGradoMinimoEstidiosDB(ClaveNivelMinimoEstudios, estatus);

            foreach (DataRow dr in gradoMinimoEstudiosdt.Rows)
            {
                NivelMinimoEstudios gradoMinimoEstudiosItem = new NivelMinimoEstudios();
                gradoMinimoEstudiosItem.ClaveNivelMinimoEstudios = (int)dr["ClaveNivelMinimoEstudios"];
                gradoMinimoEstudiosItem.Descripcion = dr["Descripcion"].ToString();
                gradoMinimoEstudiosItem.Estatus = (bool)dr["Estatus"];
                gradoMinimoEstudiosItem.ClaveUsuarioUltimaActualizacion = dr["ClaveUsuarioUltimaActualizacion"].ToString();
                gradoMinimoEstudios.Add(gradoMinimoEstudiosItem);
            }

            return gradoMinimoEstudios;
        }
        private int InsertarDB(NivelMinimoEstudios gradoMinimoEstudios)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NivelMinimoEstudios_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Descripcion", gradoMinimoEstudios.Descripcion);
                cmd.Parameters.AddWithValue("@Estatus", gradoMinimoEstudios.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", gradoMinimoEstudios.ClaveUsuarioUltimaActualizacion);

                SqlParameter parameter = new SqlParameter("@ClaveNivelMinimoEstudios", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        private bool ActualizarDB(NivelMinimoEstudios gradoMinimoEstudios)
        {
            string sqlConn = _connectionString;
            bool actualizo = false;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "NivelMinimoEstudios_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveNivelMinimoEstudios", gradoMinimoEstudios.ClaveNivelMinimoEstudios);
                cmd.Parameters.AddWithValue("@Descripcion", gradoMinimoEstudios.Descripcion);
                cmd.Parameters.AddWithValue("@Estatus", gradoMinimoEstudios.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", gradoMinimoEstudios.ClaveUsuarioUltimaActualizacion);

                cmd.ExecuteNonQuery();
                actualizo = true;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return actualizo;
        }

        #endregion

        #region Public
        public List<NivelMinimoEstudios> ObtenerTodos(bool? estatus = null)
        {
            List<NivelMinimoEstudios> gradoMinimoEstudios = ObtenerNivelMinimoEstudios(null, estatus);
            return gradoMinimoEstudios;
        }
        public NivelMinimoEstudios Obtener(int ClaveNivelMinimoEstudios)
        {
            List<NivelMinimoEstudios> gradoMinimoEstudios = ObtenerNivelMinimoEstudios(ClaveNivelMinimoEstudios);
            return gradoMinimoEstudios.FirstOrDefault();
        }
        #endregion

        #region Cambio_en_base
        public int Insertar(NivelMinimoEstudios gradoMinimoEstudios)
        {
            return InsertarDB(gradoMinimoEstudios);
        }
        public bool Actualizar(NivelMinimoEstudios gradoMinimoEstudios)
        {
            return ActualizarDB(gradoMinimoEstudios);
        }
        #endregion
    }
}

