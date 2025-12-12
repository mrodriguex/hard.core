using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.AccessControl;

namespace HARD.CORE.DAT
{
    public class CorreoVariableDA : ICorreoVariableDA
    {

        private readonly string _connectionString;

        public CorreoVariableDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerDB(int? claveVariable = null, int? claveTipoCorreo = null, bool? estatus = false)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Correo_ObtenerVariables";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (claveVariable.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveVariable", claveVariable);
                }
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
        private List<CorreoVariable> ObtenerVariables(int? claveVariable = null, int? claveTipoCorreo = null, bool? estatus = false)
        {
            DataTable dt = ObtenerDB(claveVariable, claveTipoCorreo, estatus);
            List<CorreoVariable> corrosVariables = new List<CorreoVariable>();

            foreach (DataRow dr in dt.Rows)
            {
                CorreoVariable correoVariable = new CorreoVariable();

                correoVariable.ClaveCorreoVariable = (int)dr["ClaveVariable"];
                correoVariable.Etiqueta = dr["Etiqueta"].ToString();
                correoVariable.Valor = dr["Valor"].ToString();
                correoVariable.Descripcion = dr["Descripcion"].ToString();
                corrosVariables.Add(correoVariable);
            }
            return corrosVariables;
        }
        #endregion

        #region Private
        public CorreoVariable Obtener(int claveVariable)
        {
            return ObtenerVariables(claveVariable).FirstOrDefault();
        }
        public List<CorreoVariable> ObtenerTodos(int? claveTipoCorreo = null, bool? estatus = false)
        {
            return ObtenerVariables(null, claveTipoCorreo, estatus);
        }
        #endregion

    }
}
