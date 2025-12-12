using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;
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
    public class PreciosDA
    {
        #region "Singleton"

        private static PreciosDA instance = null;

        private static object mutex = new object();

        private PreciosDA()
        {
        }
        public static PreciosDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PreciosDA();
                }
            }

            return instance;

        }
        #endregion

        #region Private
        private DataTable ObtenerPreciosDB(int? claveCliente = null, int? claveProducto = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Precios_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
                cmd.Parameters.AddWithValue("@ClaveProducto", claveProducto);

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
        private DataTable ObtenerDatosContratoDB(int? claveCliente = null, int? claveProducto = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Precios_ObtenerDatosContrato";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
                cmd.Parameters.AddWithValue("@ClaveProducto", claveProducto);

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

        #region Public_Obtener
        public DataTable ObtenerTodos(int? claveCliente = null, int? claveProducto = null)
        {
            return ObtenerPreciosDB(claveCliente, claveProducto);
        }
        public DataTable ObtenerDatosContrato(int? claveCliente = null, int? claveProducto = null)
        {
            return ObtenerDatosContratoDB(claveCliente, claveProducto);
        }
        #endregion
    }
}
