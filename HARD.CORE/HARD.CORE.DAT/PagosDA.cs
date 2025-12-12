using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class PagosDA
    {

        #region " Singleton "

        private static PagosDA instance = null;

        private static object mutex = new object();

        private PagosDA()
        {
        }

        public static PagosDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PagosDA();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerReporteCartera(int claveCliente)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[ReporteCartera_Obtener]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

    }
}
