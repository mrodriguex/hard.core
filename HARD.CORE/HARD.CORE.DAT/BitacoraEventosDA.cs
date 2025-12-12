using Azure.Core.Pipeline;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT
{
    public class BitacoraEventosDA : IBitacoraEventosDA
    {
        private readonly string _connectionString;
        public BitacoraEventosDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerBitacorasDB(int? claveEvento = null, int? claveTipoEvento = null, int? claveEntidad = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Bitacora_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (claveEvento.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveEvento", claveEvento.Value);
                }
                if (claveTipoEvento.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveTipoEvento", claveTipoEvento.Value);
                }
                if (claveEntidad.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveEntidad", claveEntidad.Value);
                }
                if (fechaInicial.HasValue)
                {
                    cmd.Parameters.AddWithValue("@FechaInicial", fechaInicial.Value);
                }
                if (fechaFinal.HasValue)
                {
                    cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal.Value);
                }

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
        private DataTable ObtenerBitacorasAccesosDB()
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "BitacoraAccesos_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
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
        private List<BitacoraAcessos> ObtenerBitacorasAccesos()
        {
            List<BitacoraAcessos> BitacoraAcessos = new List<BitacoraAcessos>();
            DataTable BitacoraAcessosdt = ObtenerBitacorasAccesosDB();

            foreach (DataRow dr in BitacoraAcessosdt.Rows)
            {
                BitacoraAcessos BitacoraAcessosItem = new BitacoraAcessos();
                BitacoraAcessosItem.ClaveUsuario = dr["ClaveUsuario"].ToString();
                BitacoraAcessosItem.NombreUsuario = dr["NombreUsuario"].ToString();
                BitacoraAcessosItem.NumeroIngresos = (int)dr["NumeroIngresos"];
                BitacoraAcessosItem.UltimaConexion = (DateTime)dr["UltimaConexion"];
                BitacoraAcessos.Add(BitacoraAcessosItem);
            }
            return BitacoraAcessos;
        }
        private BitacoraEventos ObtenerBitacoraDB(int claveBitacoraEventos)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            BitacoraEventos bitacoraEventos = new BitacoraEventos();

            try
            {
                connection.Open();
                string queryName = "Bitacora_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveBitacoraEventos", claveBitacoraEventos);
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bitacoraEventos.ClaveBitacoraEvento = (int)reader["ClaveBitacoraEvento"];
                    bitacoraEventos.ClaveEntidad = (int)reader["ClaveEntidad"];
                    bitacoraEventos.ClaveTipoEvento = (int)reader["ClaveTipoEvento"];
                    bitacoraEventos.ClaveEvento = (int)reader["ClaveEvento"];
                    bitacoraEventos.ClaveUsuario = reader["ClaveUsuario"].ToString();
                    bitacoraEventos.Descripcion = reader["Descripcion"].ToString();
                    bitacoraEventos.Fecha = (DateTime)reader["Fecha"];
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
            return bitacoraEventos;
        }
        private List<BitacoraEventos> ObtenerBitacoras(int? claveEvento = null, int? claveTipoEvento = null, int? claveEntidad = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            List<BitacoraEventos> bitacoras = new List<BitacoraEventos>();
            DataTable bitacoradt = ObtenerBitacorasDB(claveEvento, claveTipoEvento, claveEntidad, fechaInicial, fechaFinal);
            foreach (DataRow dr in bitacoradt.Rows)
            {
                BitacoraEventos bitacoraItem = new BitacoraEventos();
                bitacoraItem.ClaveBitacoraEvento = (int)dr["ClaveBitacoraEvento"];
                bitacoraItem.ClaveEntidad = (int)dr["ClaveEntidad"];
                bitacoraItem.ClaveTipoEvento = (int)dr["ClaveTipoEvento"];
                bitacoraItem.ClaveEvento = (int)dr["ClaveEvento"];
                bitacoraItem.ClaveUsuario = dr["ClaveUsuario"].ToString();
                bitacoraItem.Descripcion = dr["Descripcion"].ToString();
                bitacoraItem.Fecha = (DateTime)dr["Fecha"];
                bitacoras.Add(bitacoraItem);
            }

            return bitacoras;
        }
        private int InsertarBitacora(BitacoraEventos bitacoraEventos)
        {

            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Bitacora_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveEntidad", bitacoraEventos.ClaveEntidad);
                cmd.Parameters.AddWithValue("@ClaveTipoEvento", bitacoraEventos.ClaveTipoEvento);
                cmd.Parameters.AddWithValue("@ClaveEvento", bitacoraEventos.ClaveEvento);
                cmd.Parameters.AddWithValue("@ClaveUsuario", bitacoraEventos.ClaveUsuario);
                cmd.Parameters.AddWithValue("@Descripcion", bitacoraEventos.Descripcion);

                SqlParameter parameter = new SqlParameter("@ClaveBitacoraEvento", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }

        #endregion

        #region Public
        public List<BitacoraEventos> ObtenerTodos(int? claveEvento = null, int? claveTipoEvento = null, int? claveEntidad = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            return ObtenerBitacoras(claveEvento, claveTipoEvento, claveEntidad, fechaInicial, fechaFinal);
        }
        public BitacoraEventos Obtener(int claveBitacoraEvento)
        {
            return ObtenerBitacoraDB(claveBitacoraEvento);
        }
        public List<BitacoraAcessos> BitacoraAcessos()
        {
            return ObtenerBitacorasAccesos();
        }
        #endregion

        #region Cambio_en_base
        public int Insertar(BitacoraEventos bitacora)
        {
            return InsertarBitacora(bitacora);
        }
        #endregion
    }
}
