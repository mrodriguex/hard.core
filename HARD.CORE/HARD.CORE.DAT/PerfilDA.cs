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
    public class PerfilDA : IPerfilDA
    {

        private readonly string _connectionString;

        public PerfilDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerPerfilesDB(int? clavePerfil = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Perfiles_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (clavePerfil.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClavePerfil", clavePerfil);
                    cmd.Parameters.AddWithValue("@Estatus", estatus);
                }

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }
        private List<Perfil> ObtenerPerfiles(int? clavePerfil = null, bool? estatus = null)
        {
            List<Perfil> perfiles = new List<Perfil>();
            DataTable dtPerfiles = ObtenerPerfilesDB(clavePerfil, estatus);

            foreach (DataRow dr in dtPerfiles.Rows)
            {
                Perfil perfil = new Perfil(
                    (int)dr["ClavePerfil"]
                    , dr["Descripcion"].ToString()
                    , (bool)dr["Estatus"]
                );

                perfiles.Add(perfil);
            }

            return perfiles;

        }
        private int InsertarDB(Perfil perfil)
        {
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Perfiles_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Descripcion", perfil.Descripcion);
                cmd.Parameters.AddWithValue("@Estatus", perfil.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", perfil.ClaveUsuarioUltimaActualizacion);

                SqlParameter parameter = new SqlParameter("@ClavePerfil", 0);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        private bool ActualizarDB(Perfil perfil)
        {
            string sqlConn = _connectionString;
            bool actualizo = false;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Perfiles_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClavePerfil", perfil.ClavePerfil);
                cmd.Parameters.AddWithValue("@Descripcion", perfil.Descripcion);
                cmd.Parameters.AddWithValue("@Estatus", perfil.Estatus);
                cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", perfil.ClaveUsuarioUltimaActualizacion);

                cmd.ExecuteNonQuery();
                actualizo = true;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return actualizo;
        }
        private bool InsertarPerfilUsuarioDB(Perfil perfil, string ClaveUsuario)
        {
            bool inserto = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Perfiles_InsertarUsuarioPerfil";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveUsuario", ClaveUsuario);
                cmd.Parameters.AddWithValue("@ClavePerfil", perfil.ClavePerfil);
                cmd.Parameters.AddWithValue("@ClaveUsurioAlta", perfil.ClaveUsuarioAlta);

                cmd.ExecuteNonQuery();
                inserto = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return inserto;
        }
        private bool EliminarDB(string ClaveUsuario)
        {
            string sqlConn = _connectionString;
            bool elimino = false;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Perfiles_EliminarPerfilesUsuario";

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
        private DataTable ObtenerPerfilesUsuarioDB(string claveUsuario)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Perfiles_ObtenerPerfilesUsuario";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                if (claveUsuario.Length > 0) { cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario); }

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }
        private List<Perfil> ObtenerPerfilesUsuarioList(string claveUsuario)
        {
            List<Perfil> perfiles = new List<Perfil>();
            DataTable dtPerfiles = ObtenerPerfilesUsuarioDB(claveUsuario);

            foreach (DataRow dr in dtPerfiles.Rows)
            {
                Perfil perfil = new Perfil(
                    (int)dr["ClavePerfil"]
                    , dr["Descripcion"].ToString()
                    , (bool)dr["Estatus"]
                );

                perfiles.Add(perfil);
            }

            return perfiles;
        }
        #endregion

        #region Public_Obtener
        public List<Perfil> ObtenerTodos(int? clavePerfil = null, bool? estatus = null)
        {
            return ObtenerPerfiles(clavePerfil, estatus);
        }
        public Perfil Obtener(int clavePerfil, bool? estatus = null)
        {
            List<Perfil> perfiles = ObtenerPerfiles(clavePerfil, estatus);
            return perfiles.FirstOrDefault();
        }
        public List<Perfil> ObtenerPerfilesUsuario(string claveUsuario)
        {
            return ObtenerPerfilesUsuarioList(claveUsuario);
        }
        #endregion

        #region Cambio_en_Basee
        public int Insertar(Perfil perfil)
        {
            return InsertarDB(perfil);
        }
        public bool Actualizar(Perfil perfil)
        {
            return ActualizarDB(perfil);
        }
        public bool InsertarPerfilUsuario(Perfil perfil, string ClaveUsuario)
        {
            return InsertarPerfilUsuarioDB(perfil, ClaveUsuario);
        }
        public bool EliminarPerfilUsuario(string ClaveUsuario)
        {
            return EliminarDB(ClaveUsuario);
        }
        public bool EliminarMenu_Perfil(Perfil perfil)
        {
            bool eliminar = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();

                string queryName = "Perfiles_EliminarMenuPerfil";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClavePerfil", perfil.ClavePerfil);

                cmd.ExecuteNonQuery();
                eliminar = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return eliminar;
        }

        public bool ConfigurarMenu_Perfil(Perfil perfil)
        {
            bool inserto = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();

                EliminarMenu_Perfil(perfil);

                perfil.Menus.ForEach(menu =>
                {
                    string queryName = "Perfiles_InsertarMenuPerfil";

                    SqlCommand cmd = new SqlCommand(queryName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClavePerfil", perfil.ClavePerfil);
                    cmd.Parameters.AddWithValue("@ClaveMenu", menu.ClaveMenu);

                    cmd.ExecuteNonQuery();
                });

                inserto = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return inserto;
        }
        #endregion

    }
}
