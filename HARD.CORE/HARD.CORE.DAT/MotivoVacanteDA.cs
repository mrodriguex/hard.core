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
    public class MotivoVacanteDA : IMotivoVacanteDA
    {
        private readonly string _connectionString;
        public MotivoVacanteDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }
        #region Private
        private DataTable ObtenerMotivoVacanteDB(bool? estatus = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "MotivoVacante_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (estatus.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Estatus", estatus);
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
        private List<MotivoVacante> ObtenerMotivoVacante(bool? estatus = null)
        {
            List<MotivoVacante> motivoVacante = new List<MotivoVacante>();
            DataTable motivoVacantedt = ObtenerMotivoVacanteDB(estatus);

            foreach (DataRow dr in motivoVacantedt.Rows)
            {
                MotivoVacante motivoVacanteItem = new MotivoVacante();
                motivoVacanteItem.ClaveMotivoVacante = (int)dr["ClaveMotivoVacante"];
                motivoVacanteItem.Descripcion = dr["Descripcion"].ToString();
                motivoVacanteItem.Sustituye = (bool)dr["Sustituye"];
                motivoVacanteItem.CentroCostos = (bool?)dr["CentroCostos"];
                motivoVacanteItem.MotivoIncapacidad = (bool?)dr["MotivoIncapacidad"];
                motivoVacanteItem.NumeroMeses = (bool?)dr["NumeroMeses"];
                motivoVacanteItem.JustificacionNuevoPuesto = (bool?)dr["JustificacionNuevoPuesto"];
                motivoVacanteItem.Estatus = (bool)dr["Estatus"];
                motivoVacanteItem.ClaveUsuarioUltimaActualizacion = dr["ClaveUsuarioUltimaActualizacion"].ToString();
                motivoVacante.Add(motivoVacanteItem);
            }

            return motivoVacante;
        }
        private bool ActualizarDB(MotivoVacante motivoVacante)
        {
            string sqlConn = _connectionString;
            bool actualizo = false;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "MotivoVacante_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveMotivoVacante", motivoVacante.ClaveMotivoVacante);
                cmd.Parameters.AddWithValue("@Descripcion", motivoVacante.Descripcion);
                cmd.Parameters.AddWithValue("@Sustituye", motivoVacante.Sustituye);
                cmd.Parameters.AddWithValue("@CentroCostos", motivoVacante.CentroCostos);
                cmd.Parameters.AddWithValue("@MotivoIncapacidad", motivoVacante.MotivoIncapacidad);
                cmd.Parameters.AddWithValue("@NumeroMeses", motivoVacante.NumeroMeses);
                cmd.Parameters.AddWithValue("@JustificacionNuevoPuesto", motivoVacante.JustificacionNuevoPuesto);
                cmd.Parameters.AddWithValue("@Estatus", motivoVacante.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", motivoVacante.ClaveUsuarioUltimaActualizacion);

                cmd.ExecuteNonQuery();
                actualizo = true;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return actualizo;
        }
        #endregion
        #region Public
        public List<MotivoVacante> ObtenerTodos(bool? estatus = null)
        {
            return ObtenerMotivoVacante(estatus);
        }
        #endregion
        #region Cambios_En_Base
        public bool Actualizar(MotivoVacante motivoVacante)
        {
            return ActualizarDB(motivoVacante);
        }
        #endregion
    }
}

