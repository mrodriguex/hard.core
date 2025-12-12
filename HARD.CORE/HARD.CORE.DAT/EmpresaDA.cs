using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT
{
    public class EmpresaDA : IEmpresaDA
    {

        private readonly string _connectionString;

        public EmpresaDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerEmpresasDB(int? clavePerfil = null, string claveUsuario = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Empresas_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (clavePerfil.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClavePerfil", clavePerfil.Value);
                }

                if (!string.IsNullOrEmpty(claveUsuario))
                {
                    cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                }

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
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
        private Empresa ObtenerEmpresaDB(int? claveEmpresa = null)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            Empresa empresa = new Empresa();

            try
            {
                connection.Open();
                string queryName = "Empresas_ObtenerEmpresa";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEmpresa", claveEmpresa);
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empresa.ClaveEmpresa = (int)reader["ClaveEmpresa"];
                    empresa.Descripcion = reader["Descripcion"].ToString();
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
            return empresa;
        }

        private bool InsertarEmpresaUsuarioDB(int claveEmpresa, string claveUsuario)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Empresas_InsertarUsuarioEmpresa";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                cmd.Parameters.AddWithValue("@claveEmpresa", claveEmpresa);

                SqlParameter parameter = new SqlParameter("@ClaveUsuarioEmpresa", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(parameter.Value) > 0;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }

        private bool EliminarEmpresasUsuarioDB(string ClaveUsuario)
        {
            string sqlConn = _connectionString;
            bool elimino = false;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Empresas_EliminarEmpresasUsuario";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveUsuario", ClaveUsuario);

                cmd.ExecuteNonQuery();
                elimino = true;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return elimino;
        }

        private List<Empresa> ObtenerEmpresas(int? clavePerfil = null, string claveUsuario = null)
        {
            List<Empresa> empresas = new List<Empresa>();
            DataTable empresdt = ObtenerEmpresasDB(clavePerfil, claveUsuario);

            foreach (DataRow dr in empresdt.Rows)
            {
                Empresa empresa = new Empresa();
                empresa.ClaveEmpresa = (int)dr["ClaveEmpresa"];
                empresa.Descripcion = (string)dr["Descripcion"];
                empresas.Add(empresa);
            }

            return empresas;

        }

        public List<Empresa> ObtenerEmpresasUsuario(string? claveUsuario = null)
        {
            List<Empresa> empresas = new List<Empresa>();
            string connectionString = _connectionString;
            SqlConnection sqlConn = new SqlConnection(connectionString);
            string queryname = "Empresas_ObtenerEmpresasUsuario";

            SqlCommand cmd = new SqlCommand(queryname, sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(claveUsuario))
            {
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
            }

            try
            {
                sqlConn.Open();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Empresa empresa = new Empresa();
                        empresa.ClaveEmpresa = (int)reader["ClaveEmpresa"];
                        empresa.Descripcion = (string)reader["Descripcion"];
                        empresas.Add(empresa);
                    }
                }
            }
            catch (Exception ex)
            {
                empresas = null;
                Console.WriteLine(ex.Message);
            }
            finally { sqlConn.Close(); }

            return empresas;


        }
        private Empresa ObtenerEmpresa(int? clavePerfil = null, string claveUsuario = null)
        {
            Empresa empresa = new Empresa();
            DataTable empresasdt = ObtenerEmpresasDB(clavePerfil, claveUsuario);

            foreach (DataRow dr in empresasdt.Rows)
            {
                empresa.ClaveEmpresa = (int)dr["ClaveEmpresa"];
                empresa.Descripcion = (string)dr["Descripcion"].ToString();
            }
            return empresa;

        }

        /// <summary>
        /// Insertar empresa para usuario
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="claveUsuario"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool InsertarEmpresaUsuario(Empresa empresa, string claveUsuario)
        {
            return InsertarEmpresaUsuarioDB(claveEmpresa: empresa.ClaveEmpresa, claveUsuario: claveUsuario);
        }


        /// <summary>
        /// Eliminar todas las empresas para un usuario
        /// </summary>
        /// <param name="claveUsuario">
        /// 
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool EliminarEmpresasUsuario(string claveUsuario)
        {
            return EliminarEmpresasUsuarioDB(claveUsuario);
        }

        #endregion
        #region Public_Obtener
        public List<Empresa> ObtenerTodos(int? clavePerfil = null, string claveUsuario = null)
        {
            return ObtenerEmpresas(clavePerfil, claveUsuario);
        }
        public Empresa Obtener(int? claveEmpresa = null)
        {
            return ObtenerEmpresaDB(claveEmpresa);
        }

        #endregion
        #region Cambio_en_Basee
        #endregion

    }
}
