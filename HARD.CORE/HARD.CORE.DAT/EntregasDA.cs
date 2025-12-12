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
    public class EntregasDA
    {

        #region " Singleton "

        private static EntregasDA instance = null;

        private static object mutex = new object();

        private EntregasDA()
        {
        }

        public static EntregasDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EntregasDA();
                }
            }

            return instance;

        }

        #endregion

        #region Private
        private DataTable ObtenerClavesArrendamientosDB(int? claveCliente = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Entregas_ObtenerClavesArrendamientos";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
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
        private DataTable ObtenerEntregasDB(int? claveCliente = null, int? claveProducto = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Entregas_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
                cmd.Parameters.AddWithValue("@ClaveProducto", claveProducto);
                cmd.Parameters.AddWithValue("@FechaInicial", fechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);
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
        private DataTable ObtenerArrendamientosDB(int? claveCliente = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null, string claveArrendamiento = "")
        {

            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "Entregas_ObtenerArrendamientos";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
                cmd.Parameters.AddWithValue("@FechaInicial", fechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                cmd.Parameters.AddWithValue("@ClaveArrendamiento", claveArrendamiento);
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
        public DataTable ObtenerClavesArrendamientos(int? claveCliente = null)
        {
            return ObtenerClavesArrendamientosDB(claveCliente);
        }
        public DataTable ObtenerEntregas(int? claveCliente = null, int? claveProducto = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            return ObtenerEntregasDB(claveCliente, claveProducto, fechaInicial, fechaFinal);
        }
        public DataTable ObtenerPorProducto(int? claveCliente = null, int? claveProducto = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            return ObtenerEntregasDB(claveCliente, claveProducto, fechaInicial, fechaFinal);
        }
        public DataTable ObtenerArrendamientos(int? claveCliente = null, string claveArrendamiento = "", DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            return ObtenerArrendamientosDB(claveCliente, fechaInicial, fechaFinal, claveArrendamiento);
        }
        #endregion
    }
}