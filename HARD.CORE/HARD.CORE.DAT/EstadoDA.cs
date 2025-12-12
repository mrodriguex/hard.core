using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class EstadoDA
    {

        #region "Singleton"

        private static EstadoDA instance = null;

        private static object mutex = new object();

        private EstadoDA()
        {
        }

        public static EstadoDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EstadoDA();
                }
            }

            return instance;

        }

        #endregion

        public Estado ObtenerEstado(int claveEstado)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Estados_ObtenerEstados]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEstado", claveEstado);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Estado((int)reader["ClaveEstado"], reader["Abreviatura"].ToString(), reader["Descripcion"].ToString(), (bool)reader["Estatus"]);
                    }
                }

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return null;

        }

        public DataTable ObtenerEstadosActivos()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Estados_ObtenerEstados]";

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
