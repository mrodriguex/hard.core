using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.AccessControl;

namespace HARD.CORE.DAT
{
    public class ClienteDA
    {

        #region " Singleton "

        private static ClienteDA instance = null;

        private static object mutex = new object();

        private ClienteDA()
        {
        }

        public static ClienteDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new ClienteDA();
                }
            }

            return instance;

        }

        #endregion

        #region Private
        private DataTable ObtenerClientesBD(int ClaveCliente = 0)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Cliente_ObtenerClientesJDE";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCliente", ClaveCliente);
                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
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
        private Cliente ObtenerClienteObj(int ClaveCliente = 0)
        {
            DataTable dtCliente = ObtenerClientesBD(ClaveCliente);
            DataRow dr = dtCliente.Rows[0];
            Cliente cliente = new Cliente(
                  (int)dr["ClaveCliente"]
                , dr["RFC"].ToString()
                , dr["RazonSocial"].ToString()
                , 0
            );

            return cliente;
        }
        private DataTable ObtenerClientesDB()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Cliente_ObtenerClientes";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
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
        private DataTable ObtenerNoRegistradosDB()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Cliente_ObtenerNoRegistrados";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
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
        private DataTable ObtenerClientesHijosJDEDB(int ClaveClientePadre = 0)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Cliente_ObtenerClientesHijosJDE";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveClientePadre", ClaveClientePadre);
                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
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
        #endregion

        #region Obtener
        public DataTable ObtenerTodos()
        {
            return ObtenerClientesBD();
        }
        public Cliente ObtenerCliente(int ClaveCliente)
        {
            return ObtenerClienteObj(ClaveCliente);
        }
        public DataTable ObtenerClientes()
        {
            return ObtenerClientesDB();
        }
        public DataTable ObtenerNoRegistrados()
        {
            return ObtenerNoRegistradosDB();
        }
        public DataTable ObtenerClientesHijosJDE(int ClaveClientePadre)
        {
            return ObtenerClientesHijosJDEDB(ClaveClientePadre);
        }
        public Cliente ObtenerClientesHijosObj(int ClaveCliente)
        {
            DataTable dtCliente = ObtenerClientesHijosJDE(ClaveCliente);
            DataRow dr = dtCliente.Rows[0];
            Cliente cliente = new Cliente(
                  (int)dr["ClaveCliente"]
                , dr["RFC"].ToString()
                , dr["RazonSocial"].ToString()
                , (int)dr["ClaveClientePadre"]
            );

            return cliente;
        }
        #endregion
    }
}