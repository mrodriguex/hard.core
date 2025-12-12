using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace HARD.CORE.DAT
{
    public class TiposContenidoDA
    {
        #region "Singleton"
        private static TiposContenidoDA instance = null; private static object mutex = new object(); private TiposContenidoDA() { }
        public static TiposContenidoDA GetInstance() { if (instance == null) { lock ((mutex)) { instance = new TiposContenidoDA(); } } return instance; }
        #endregion

        #region Privat
        private DataTable ObtenerTiposContenidoDB(int? claveTipoContenido = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "TiposContenido_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (claveTipoContenido.HasValue) { cmd.Parameters.AddWithValue("@ClaveTipoContrato", claveTipoContenido); }

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        private TipoContenido ObtenerTiposContenidoOBJ(int? claveNotificacion)
        {
            DataTable dt = ObtenerTiposContenidoDB(claveNotificacion);
            DataRow dr = dt.Rows[0];
            TipoContenido tipoContenido = new TipoContenido(
                (int)dr["ClaveTipoContenido"]
                , dr["Descripcion"].ToString()
                );

            return tipoContenido;
        }
        #endregion

        #region Public_Obtener
        public TipoContenido Obtener(int claveNotificacion)
        {
            return ObtenerTiposContenidoOBJ(claveNotificacion: claveNotificacion);
        }
        #endregion
    }
}
