using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.AccessControl;

namespace HARD.CORE.DAT
{
    public class TipoCorreoDA : ITipoCorreoDA
    {

        private readonly string _connectionString;

        #region Private
        public TipoCorreoDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerDB(int? claveTipoCorreo = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "TipoCorreo_ObtenerTipoCorreo";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (claveTipoCorreo.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveTipoCorreo", claveTipoCorreo);
                }
                if (estatus.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Estatus", estatus);
                }
                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }
        private List<TipoCorreo> ObtenerTipoCorreo(int? claveTipoCorreo = null, bool? estatus = null)
        {
            DataTable dt = ObtenerDB(claveTipoCorreo, estatus);
            List<TipoCorreo> tiposCorreo = new List<TipoCorreo>();

            foreach (DataRow dr in dt.Rows)
            {
                TipoCorreo tipoCorreo = new TipoCorreo();

                tipoCorreo.ClaveTipoCorreo = (int)dr["ClaveTipoCorreo"];
                tipoCorreo.Descripcion = dr["Descripcion"].ToString();
                tipoCorreo.Estatus = (bool)dr["Estatus"];
                tiposCorreo.Add(tipoCorreo);
            }
            return tiposCorreo;
        }
        #endregion
        public TipoCorreo Obtener(int claveTipoCorreo)
        {
            return ObtenerTipoCorreo(claveTipoCorreo).FirstOrDefault();
        }

        public List<TipoCorreo> ObtenerTodos(bool? estatus)
        {
            return ObtenerTipoCorreo(null, estatus);
        }
        #endregion

    }
}
