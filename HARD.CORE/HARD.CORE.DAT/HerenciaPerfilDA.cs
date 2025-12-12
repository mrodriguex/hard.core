using Azure.Core.Cryptography;
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
    public class HerenciaPerfilDA : IHerenciaPerfilDA
    {
        private readonly string _connectionString;
        private readonly IHerenciaPerfilDA _herenciaPerfilDA;

        public HerenciaPerfilDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private 
        private DataTable ObtenerDB(bool? estatus = false)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "HerenciaPerfil_ObtenerUsuarios";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (estatus.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Estatus", estatus.Value);
                }

                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }
        private List<HerenciaPerfil> ObtenerHerencia(bool? estatus = null)
        {
            DataTable dt = ObtenerDB(estatus);
            List<HerenciaPerfil> herenciaPerfil = new List<HerenciaPerfil>();

            foreach (DataRow dr in dt.Rows)
            {
                HerenciaPerfil item = new HerenciaPerfil();

                item.ClaveHerenciaPerfil = (int)dr["ClaveHerenciaPerfil"];
                item.ClaveUsuario = dr["ClaveUsuario"].ToString();
                item.ClaveUsuarioHeredado = dr["ClaveUsuarioHeredado"].ToString();
                item.FechaInicial = (DateTime)dr["FechaInicial"];
                item.FechaFinal = (DateTime)dr["FechaFinal"];
                item.FechaUltimaActualizacion = (DateTime)dr["FechaUltimaActualizacion"];
                item.UsuarioHeredado = dr["UsuarioHeredado"].ToString();
                item.Estatus = (bool)dr["Estatus"];

                herenciaPerfil.Add(item);
            }
            return herenciaPerfil;
        }
        private int InsertarDB(HerenciaPerfil herenciaPerfil)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "HerenciaPerfil_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveUsuario", herenciaPerfil.ClaveUsuario);
                cmd.Parameters.AddWithValue("@ClaveUsuarioHereda", herenciaPerfil.ClaveUsuarioHeredado);
                cmd.Parameters.AddWithValue("@FechaInicial", herenciaPerfil.FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", herenciaPerfil.FechaFinal);
                cmd.Parameters.AddWithValue("@Estatus", herenciaPerfil.Estatus);

                SqlParameter parameter = new SqlParameter("@ClaveHerenciaPerfil", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }
        private bool ActualizarDB(HerenciaPerfil herenciaPerfil)
        {
            bool actualizo = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "HerenciaPerfil_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveHerenciaPerfil", herenciaPerfil.ClaveHerenciaPerfil);
                cmd.Parameters.AddWithValue("@ClaveUsuario", herenciaPerfil.ClaveUsuario);
                cmd.Parameters.AddWithValue("@ClaveUsuarioHeredado", herenciaPerfil.ClaveUsuarioHeredado);
                cmd.Parameters.AddWithValue("@FechaInicial", herenciaPerfil.FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", herenciaPerfil.FechaFinal);
                cmd.Parameters.AddWithValue("@Estatus", herenciaPerfil.Estatus);
                cmd.ExecuteNonQuery();
                actualizo = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return actualizo;
        }
        private bool ExisteDB(HerenciaPerfil herenciaPerfil)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "HerenciaPerfil_ExisteUsuario";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", herenciaPerfil.ClaveUsuario);
                cmd.Parameters.AddWithValue("@ClaveUsuarioHeredado", herenciaPerfil.ClaveUsuarioHeredado);
                cmd.Parameters.AddWithValue("@FechaInicial", herenciaPerfil.FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", herenciaPerfil.FechaFinal);

                SqlParameter parameter = new SqlParameter("@Existe", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(parameter.Value);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        #endregion

        #region Public 
        public List<HerenciaPerfil> ObtenerTodos(bool? estatus = null)
        {
            return ObtenerHerencia(estatus);
        }
        public bool Existe(HerenciaPerfil herenciaPerfil)
        {
            return ExisteDB(herenciaPerfil);
        }
        #endregion

        #region Cambios_En_Base
        public int Insertar(HerenciaPerfil herenciaPerfil)
        {
            return InsertarDB(herenciaPerfil);
        }
        public bool Actualizar(HerenciaPerfil herenciaPerfil)
        {
            return ActualizarDB(herenciaPerfil);
        }
        #endregion

    }
}
