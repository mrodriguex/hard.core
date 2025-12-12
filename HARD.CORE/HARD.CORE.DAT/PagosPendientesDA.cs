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
    public class PagosPendientesDA
    {
        #region " Singleton "

        private static PagosPendientesDA instance = null;

        private static object mutex = new object();

        private PagosPendientesDA()
        {
        }
        public static PagosPendientesDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PagosPendientesDA();
                }
            }

            return instance;

        }
        #endregion

        #region Private
        private DataTable ObtenerPagosPendientesDB(int? claveCliente = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "ReporteCartera_Obtener";

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

        #endregion

        #region Public_Obtener
        public DataTable ObtenerTodos()
        {
            return ObtenerPagosPendientesDB();
        }
        public DataTable ObtenerCartera(int? claveCliente = null)
        {
            return ObtenerPagosPendientesDB(claveCliente);
        }
        #endregion
    }
}