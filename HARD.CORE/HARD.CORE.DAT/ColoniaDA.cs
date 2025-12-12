//using HARD.CORE.OBJ;
//using HARD.CORE.OBJ.Configuration;

//using Microsoft.Extensions.Configuration;

//using System;
//using System.Data;
//using Microsoft.Data.SqlClient;

//namespace HARD.CORE.DAT
//{
//    public class ColoniaDA
//    {

//        #region "Singleton"

//        private static ColoniaDA instance = null;

//        private static object mutex = new object();

//        private ColoniaDA()
//        {
//        }

//        public static ColoniaDA GetInstance()
//        {

//            if (instance == null)
//            {
//                lock ((mutex))
//                {
//                    instance = new ColoniaDA();
//                }
//            }

//            return instance;

//        }

//        #endregion

//        public Colonia ObtenerColonia(int clave)
//        {
//            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
//            SqlConnection connection = new SqlConnection(sqlConn);

//            try
//            {
//                connection.Open();
//                string queryName = "[dbo].[Colonias_ObtenerColonia]";

//                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@Clave", clave);

//                using (IDataReader reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        return new Colonia((int)reader["Clave"], (int)reader["ClaveEstado"], (int)reader["ClaveMunicipio"], reader["Descripcion"].ToString(), reader["TipoAsentamiento"].ToString(), reader["CodigoPostal"].ToString());
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                connection.Close();
//            }

//            return null;
//        }

//        public DataTable ObtenerColonias(int claveEstado, int claveMunicipio, string colonia = "")
//        {
//            DataTable dataTable = new DataTable();

//            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
//            SqlConnection connection = new SqlConnection(sqlConn);

//            try
//            {
//                connection.Open();
//                string queryName = "[dbo].[Colonias_ObtenerColonias]";

//                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@ClaveEstado", claveEstado);
//                cmd.Parameters.AddWithValue("@ClaveMunicipio", claveMunicipio);
//                cmd.Parameters.AddWithValue("@Colonia", colonia);

//                IDataReader reader = cmd.ExecuteReader();
//                dataTable.Load(reader);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                connection.Close();
//            }

//            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
//        }

//    }
//}
