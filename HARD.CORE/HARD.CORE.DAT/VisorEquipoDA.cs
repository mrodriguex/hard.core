
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{

    public class VisorEquipoDA
    {

        #region "Singleton"

        private static VisorEquipoDA instance = null;

        private static object mutex = new object();

        private VisorEquipoDA()
        {
        }

        public static VisorEquipoDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new VisorEquipoDA();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerEquipos(int numeroEconomicoTanque)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Tanques_VisorEquipos_ObtenerEquipos]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Clave", numeroEconomicoTanque);
                cmd.Parameters.AddWithValue("@ClaveTipoEquipo", 1);
                //cmd.Parameters.AddWithValue("@ClaveTipoEquipo", claveTipoEquipo);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

        public DataTable ObtenerEquiposHistorico(int claveHistorico, int claveTipoEquipo)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Tanques_VisorEquipos_ObtenerEquiposHistorico]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveHistorico", claveHistorico);
                cmd.Parameters.AddWithValue("@ClaveTipoEquipo", claveTipoEquipo);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

        public DataTable ObtenerEquiposAplicacion(int claveEquipoAplicacion)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[EquiposAplicacion_VisorEquipos_ObtenerEquipos]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEquipoAplicacion", claveEquipoAplicacion);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }
        public DataTable ObtenerEquiposAplicacionHistorico(int claveEquipoAplicacionHistorico)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[EquiposAplicacion_VisorEquipos_ObtenerEquiposHistorico]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEquipoAplicacionHistorico", claveEquipoAplicacionHistorico);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

    }

}
