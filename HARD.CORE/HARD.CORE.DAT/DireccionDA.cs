using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HARD.CORE.DAT
{
    public class DireccionDA
    {

        #region "Singleton"

        private static DireccionDA instance = null;

        private static object mutex = new object();

        private DireccionDA()
        {
        }

        public static DireccionDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new DireccionDA();
                }
            }

            return instance;

        }

        #endregion


        public Direccion ObtenerDireccion(int claveTipoUbicacion, int claveUbicacion)
        {
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "[dbo].[Direccion_ObtenerDireccion]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveTipoUbicacion", claveTipoUbicacion);
                cmd.Parameters.AddWithValue("@ClaveUbicacion", claveUbicacion);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Direccion direccion = new Direccion(
                                                TipoUbicacionDA.GetInstance().ObtenerTipoUbicacion((int)reader["ClaveTipoUbicacion"])
                                                , (int)reader["ClaveUbicacion"]
                                                , reader["Ubicacion"].ToString()
                                                , PaisDA.GetInstance().ObtenerPais((int)reader["ClavePais"])
                                                , EstadoDA.GetInstance().ObtenerEstado((int)reader["ClaveEstado"])
                                                , reader["DelegacionMunicipio"].ToString()
                                                , reader["Colonia"].ToString()
                                                , reader["Calle"].ToString()
                                                , reader["NumeroExterior"].ToString().Trim()
                                                , reader["NumeroInterior"].ToString().Trim()
                                                , reader["CodigoPostal"].ToString().Trim()
                                                , reader["Referencias"].ToString()
                                                , Convert.ToDecimal(reader["Latitud"])
                                                , Convert.ToDecimal(reader["Longitud"])
                                                , new Telefono(reader["Lada"].ToString(), reader["Telefono"].ToString()));

                        return direccion;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public Direccion ObtenerDireccion_Historico(int claveDireccionHistorico)
        {
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "[dbo].[Direccion_ObtenerDireccion_Historico]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveDireccionHistorico", claveDireccionHistorico);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Direccion direccion = new Direccion(
                                                TipoUbicacionDA.GetInstance().ObtenerTipoUbicacion((int)reader["ClaveTipoUbicacion"])
                                                , (int)reader["ClaveUbicacion"]
                                                , reader["Ubicacion"].ToString()
                                                , PaisDA.GetInstance().ObtenerPais((int)reader["ClavePais"])
                                                , EstadoDA.GetInstance().ObtenerEstado((int)reader["ClaveEstado"])
                                                , reader["DelegacionMunicipio"].ToString()
                                                , reader["Colonia"].ToString()
                                                , reader["Calle"].ToString()
                                                , reader["NumeroExterior"].ToString().Trim()
                                                , reader["NumeroInterior"].ToString().Trim()
                                                , reader["CodigoPostal"].ToString().Trim()
                                                , reader["Referencias"].ToString()
                                                , Convert.ToDecimal(reader["Latitud"])
                                                , Convert.ToDecimal(reader["Longitud"])
                                                , new Telefono(reader["Lada"].ToString(), reader["Telefono"].ToString()));

                        return direccion;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public DataTable ObtenerDirecciones_CorreosMexico(int claveEstado, int claveMunicipio)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "[dbo].[CorreosMexico_ObtenerDirecciones]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEstado", claveEstado);
                cmd.Parameters.AddWithValue("@ClaveMunicipio", claveMunicipio);

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

    }
}
