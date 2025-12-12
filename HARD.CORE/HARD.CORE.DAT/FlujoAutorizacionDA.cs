using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
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
    public class FlujoAutorizacionDA : IFlujoAutorizacionDA
    {

        private readonly string _connectionString;

        public FlujoAutorizacionDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }
        #region Private
        private DataTable ObtenerDB()
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "FlujoAutorizacion_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estatus", true);

                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }
        #endregion

        #region Public
        public List<FlujoAutorizacion> ObtenerTodos()
        {
            DataTable dt = ObtenerDB();
            List<FlujoAutorizacion> flujo = new List<FlujoAutorizacion>();

            foreach (DataRow dr in dt.Rows)
            {
                FlujoAutorizacion flujoAutorizacion = new FlujoAutorizacion();

                flujoAutorizacion.ClaveFlujoAutorizacion = (int)dr["ClaveFlujoAutorizacion"];
                flujoAutorizacion.ClaveEstatus = (int)dr["ClaveEstatus"];
                flujoAutorizacion.CodigoPuesto = dr["CodigoPuesto"].ToString();
                flujoAutorizacion.Puesto = dr["Puesto"].ToString();
                flujoAutorizacion.Nombre = dr["Nombre"].ToString();
                flujoAutorizacion.Correo = dr["Correo"].ToString();
                flujoAutorizacion.Estatus = (bool)dr["Estatus"];

                flujo.Add(flujoAutorizacion);
            }

            return flujo;
        }
        #endregion
    }
}
