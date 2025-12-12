using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class PaisDA
    {

        #region "Singleton"

        private static PaisDA instance = null;

        private static object mutex = new object();

        private PaisDA()
        {
        }

        public static PaisDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PaisDA();
                }
            }

            return instance;

        }

        #endregion

        public Pais ObtenerPais(int clavePais)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Paises_ObtenerPaises]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClavePais", clavePais);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Pais((int)reader["ClavePais"], reader["Descripcion"].ToString(), (bool)reader["Estatus"]);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return null;

        }

        public DataTable ObtenerPaises()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Paises_ObtenerPaises]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

        public DataTable ObtenerPaisesActivos()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Paises_ObtenerPaises]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estatus", true);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

    }
}
