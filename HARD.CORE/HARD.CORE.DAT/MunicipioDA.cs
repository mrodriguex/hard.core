using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class MunicipioDA
    {


        #region "Singleton"

        private static MunicipioDA instance = null;

        private static object mutex = new object();

        private MunicipioDA()
        {
        }

        public static MunicipioDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new MunicipioDA();
                }
            }

            return instance;

        }

        #endregion

        public Municipio ObtenerMunicipio(int claveMunicipio)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Municipios_ObtenerMunicipios]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveMunicipio", claveMunicipio);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Municipio((int)reader["ClaveEstado"], (int)reader["ClaveMunicipio"], reader["Descripcion"].ToString(), (bool)reader["Estatus"]);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return null;

        }

        public DataTable ObtenerMunicipios()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Municipios_ObtenerMunicipios]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

        public DataTable ObtenerMunicipios(int claveEstado)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Municipios_ObtenerMunicipios]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEstado", claveEstado);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

        public DataTable ObtenerMunicipiosActivos(int claveEstado)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Municipios_ObtenerMunicipios]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estatus", true);
                cmd.Parameters.AddWithValue("@ClaveEstado", claveEstado);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

    }

}
