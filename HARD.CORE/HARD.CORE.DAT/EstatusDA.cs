using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class EstatusDA
    {

        #region "Singleton"

        private static EstatusDA instance = null;

        private static object mutex = new object();

        private EstatusDA()
        {
        }

        public static EstatusDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EstatusDA();
                }
            }

            return instance;

        }

        #endregion

        public Estatus ObtenerEstatus(int claveEstatus)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Estatus_ObtenerEstatus]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEstatus", claveEstatus);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Estatus((int)reader["ClaveEstatus"], reader["Abreviatura"].ToString(), reader["Descripcion"].ToString(), (bool)reader["Estatus"]);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return null;

        }

        //public DataTable ObtenerEstatus()
        //{

        //    string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");            SqlConnection connection = new SqlConnection(sqlConn);
        //    string queryName = "[dbo].[Estatus_ObtenerEstatus]";

        //    SqlCommand cmd = new SqlCommand(queryName, connection);                cmd.CommandType = CommandType.StoredProcedure;

        //    IDataReader reader = cmd.ExecuteReader(); DataTable dataTable = new DataTable();dataTable.Load(reader); return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        //}

        //public DataTable ObtenerEstatusActivos()
        //{

        //    string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");            SqlConnection connection = new SqlConnection(sqlConn);
        //    string queryName = "[dbo].[Estatus_ObtenerEstatus]";

        //    SqlCommand cmd = new SqlCommand(queryName, connection);                cmd.CommandType = CommandType.StoredProcedure;
        //   cmd.Parameters.AddWithValue("@Estatus",true);

        //    IDataReader reader = cmd.ExecuteReader(); DataTable dataTable = new DataTable();dataTable.Load(reader); return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        //}

        //public int InsertarEstatus(Estatus estatus)
        //{

        //    string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");            SqlConnection connection = new SqlConnection(sqlConn);
        //    string queryName = "[dbo].[Estatus_InsertarEstatus]";

        //    SqlCommand cmd = new SqlCommand(queryName, connection);                cmd.CommandType = CommandType.StoredProcedure;
        //   cmd.Parameters.AddWithValue("@Abreviatura",estatus.Abreviatura);
        //   cmd.Parameters.AddWithValue("@Descripcion",estatus.Descripcion);
        //   cmd.Parameters.AddWithValue("@Estatus",estatus.Activo);
        //   SqlParameter parameter = new SqlParameter("ClavePerfil", 0);parameter.Direction = ParameterDirection.Output;cmd.Parameters.Add(parameter); "@ClaveEstatus",32);

        //    cmd.ExecuteNonQuery();

        //    return Convert.ToInt32(parameter.Value);

        //}

        //public void ActualizarEstatus(Estatus estatus)
        //{

        //    string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");            SqlConnection connection = new SqlConnection(sqlConn);
        //    string queryName = "[dbo].[Estatus_ActualizarEstatus]";

        //    SqlCommand cmd = new SqlCommand(queryName, connection);                cmd.CommandType = CommandType.StoredProcedure;
        //   cmd.Parameters.AddWithValue("@ClaveEstatus",estatus.ClaveEstatus);
        //   cmd.Parameters.AddWithValue("@Abreviatura",estatus.Abreviatura);
        //   cmd.Parameters.AddWithValue("@Descripcion",estatus.Descripcion);
        //   cmd.Parameters.AddWithValue("@Estatus",estatus.Activo);

        //    cmd.ExecuteNonQuery();

        //}

        //public DataTable ObtenerEstatus_Entidad(int claveEntidad)
        //{
        //    string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");            SqlConnection connection = new SqlConnection(sqlConn);
        //    string queryName = "[dbo].[Estatus_ObtenerEstatus_Entidad]";
        //    SqlCommand cmd = new SqlCommand(queryName, connection);                cmd.CommandType = CommandType.StoredProcedure;
        //   cmd.Parameters.AddWithValue("@ClaveEntidad",claveEntidad);

        //    IDataReader reader = cmd.ExecuteReader(); DataTable dataTable = new DataTable();dataTable.Load(reader); return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        //}

        //public DataTable ObtenerEstatusSaiec()
        //{
        //    string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");            SqlConnection connection = new SqlConnection(sqlConn);
        //    string queryName = "[dbo].[RPT_ObtenerEstatus]";
        //    SqlCommand cmd = new SqlCommand(queryName, connection);                cmd.CommandType = CommandType.StoredProcedure;

        //    IDataReader reader = cmd.ExecuteReader(); DataTable dataTable = new DataTable();dataTable.Load(reader); return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        //}

    }
}
