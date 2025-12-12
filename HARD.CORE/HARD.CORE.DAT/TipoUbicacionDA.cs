using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class TipoUbicacionDA
    {

        #region "Singleton"

        private static TipoUbicacionDA instance = null;

        private static object mutex = new object();

        private TipoUbicacionDA()
        {
        }

        public static TipoUbicacionDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new TipoUbicacionDA();
                }
            }

            return instance;

        }

        #endregion

        public TipoUbicacion ObtenerTipoUbicacion(int claveTipoUbicacion)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[TiposUbicacion_ObtenerTiposUbicacion]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveTipoUbicacion", claveTipoUbicacion);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        return new TipoUbicacion((int)reader["ClaveTipoUbicacion"]
                                            , reader["Abreviatura"].ToString()
                                            , reader["Descripcion"].ToString()
                                            , (bool)reader["Estatus"]);

                    }
                }

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return null;

        }

        public DataTable ObtenerTiposUbicacion()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[TiposUbicacion_ObtenerTiposUbicacion]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

        public DataTable ObtenerTiposUbicacionActivos()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[TiposUbicacion_ObtenerTiposUbicacion]";

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
