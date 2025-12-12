using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HARD.CORE.DAT.Interfaces;

namespace HARD.CORE.DAT
{
    public class SugerenciaDA : ISugerenciaDA
    {
        public SugerenciaDA() { }

        public int InsertarSugerencia(Sugerencia sugerencia, bool esAnonimo)
        {
            int claveSugerencia = 0;
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Sugerencias_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (!esAnonimo)
                {
                    cmd.Parameters.AddWithValue("@Nombre", sugerencia.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", sugerencia.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", sugerencia.Telefono);
                }
                cmd.Parameters.AddWithValue("@ClaveUsuario", sugerencia.ClaveUsuario);
                cmd.Parameters.AddWithValue("@Comentarios", sugerencia.Comentarios);

                SqlParameter parameter = new SqlParameter("@ClaveSugerencia", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                claveSugerencia = Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return claveSugerencia;

        }

        public DataTable ObtenerSugerencias()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Sugerencias_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }

    }
}
