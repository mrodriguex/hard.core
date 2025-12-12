
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;

namespace HARD.CORE.DAT
{
    public class UsuarioDA : IUsuarioDA
    {

        private readonly string _connectionString;

        IPerfilDA _perfilDA;
        IEmpresaDA _empresaDA;

        public UsuarioDA(IConfiguration configuration, IPerfilDA perfilDA, IEmpresaDA empresaDA)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
            _perfilDA = perfilDA;
            _empresaDA = empresaDA;
        }

        #region Private
        private DataTable ObtenerUsuarioDB(string ClaveUsuario = "")
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", ClaveUsuario);
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
        private Usuario ObtenerUsuario(string ClaveUsuario = "")
        {
            DataTable dtUsuario = ObtenerUsuarioDB(ClaveUsuario);

            Usuario usuario = new Usuario();

            if (dtUsuario.Rows.Count > 0)
            {
                DataRow dr = dtUsuario.Rows[0];
                usuario.ClaveUsuario = (string)dr["ClaveUsuario"].ToString();
                usuario.NumeroEmpleado = (int)dr["NumeroEmpleado"];
                usuario.EmpresaActivo.ClaveEmpresa = (int)dr["ClaveEmpresa"];
                usuario.esActive = (bool)dr["EsActive"];
                usuario.Nombre = dr["Nombre"].ToString();
                usuario.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                usuario.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                usuario.Correo = dr["Correo"].ToString();
                usuario.Contrasena = dr["Contrasena"].ToString();
                usuario.NumeroIntentos = (int)dr["NumeroIntentos"];
                usuario.Bloqueado = (bool)dr["Bloqueado"];
                usuario.CambioContrasena = (bool)dr["CambioContrasena"];
                usuario.Estatus = (bool)dr["Estatus"];
                usuario.Empresas = _empresaDA.ObtenerEmpresasUsuario(claveUsuario: usuario.ClaveUsuario);
                //usuario.Perfiles = _perfilDA.ObtenerPerfilesUsuario(usuario.ClaveUsuario);
                usuario.ClaveUsuarioPorAusencia = string.Empty;
                usuario.NombreUsuarioPorAusencia = string.Empty;
            }

            return usuario;

        }
        private List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            DataTable dtUsuarios = ObtenerUsuarioDB();

            foreach (DataRow dr in dtUsuarios.Rows)
            {
                Usuario usuario = new Usuario();

                usuario.ClaveUsuario = (string)dr["ClaveUsuario"].ToString();
                usuario.NumeroEmpleado = (int)dr["NumeroEmpleado"];

                usuario.esActive = (bool)dr["EsActive"];
                usuario.Nombre = dr["Nombre"].ToString();
                usuario.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                usuario.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                usuario.Correo = dr["Correo"].ToString();
                usuario.Contrasena = dr["Contrasena"].ToString();
                usuario.NumeroIntentos = (int)dr["NumeroIntentos"];
                usuario.Bloqueado = (bool)dr["Bloqueado"];
                usuario.CambioContrasena = (bool)dr["CambioContrasena"];
                usuario.Estatus = (bool)dr["Estatus"];

                usuario.ClaveUsuarioPorAusencia = string.Empty;
                usuario.NombreUsuarioPorAusencia = string.Empty;

                usuarios.Add(usuario);
            }
            return usuarios;
        }
        private DataTable ObtenerUsuariosDirectorioActivoDB(string ClaveUsuario = "")
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_ObtenerUsuariosDirectorioActivo";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", ClaveUsuario);
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
        private DataTable ObtenerUsuariosDirectorioActivoTodosDB()
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_ObtenerUsuariosDirectorioActivoTodos";

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


        private Usuario ObtenerUsuarioDirectorioActivoObj(string ClaveUsuario = "")
        {
            DataTable dtUsuario = ObtenerUsuariosDirectorioActivoDB(ClaveUsuario);

            DataRow dr = dtUsuario.Rows[0];
            Usuario usuario = new Usuario();
            usuario.ClaveUsuario = (string)dr["ClaveUsuario"].ToString();
            //usuario.TipoUsuario = ObtenerTipoUsuario((int)EnumTipoUsuario.Red);
            usuario.PerfilActivo = new Perfil();
            usuario.Nombre = dr["Nombre"].ToString();
            usuario.ApellidoPaterno = string.Empty;
            usuario.ApellidoMaterno = dr["Apellidos"].ToString();
            usuario.Correo = dr["Correo"].ToString();
            usuario.Contrasena = string.Empty;
            usuario.Bloqueado = false;
            usuario.NumeroIntentos = 0;
            usuario.Bloqueado = false;
            usuario.CambioContrasena = true;
            usuario.ClaveUsuarioUltimaActualizacion = string.Empty;
            return usuario;


        }
        private List<Usuario> ObtenerUsuarioDirectorioActivoTodosObj()
        {
            List<Usuario> usuarios = new List<Usuario>();
            DataTable dtUsuario = ObtenerUsuariosDirectorioActivoTodosDB();

            foreach (DataRow dr in dtUsuario.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.ClaveUsuario = (string)dr["ClaveUsuario"].ToString();
                usuario.PerfilActivo = new Perfil();
                usuario.Nombre = dr["Nombre"].ToString();
                usuario.ApellidoPaterno = string.Empty;
                usuario.ApellidoMaterno = dr["Apellidos"].ToString();
                usuario.Correo = dr["Correo"].ToString();
                usuario.Contrasena = string.Empty;
                usuario.Bloqueado = false;
                usuario.NumeroIntentos = 0;
                usuario.Bloqueado = false;
                usuario.CambioContrasena = true;
                usuario.ClaveUsuarioUltimaActualizacion = string.Empty;
                usuario.NumeroEmpleado = (int)dr["NumeroEmpleado"];
                usuario.esActive = true;
                usuarios.Add(usuario);
            }
            
            return usuarios;
        }
        private DataTable ObtenerNoRegistradosDirectorioActivoDB()
        {

            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_ObtenerUsuariosDirectorioActivo";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
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
        private List<Usuario> ObtenerNoRegistradosDirectorioActivo()
        {
            List<Usuario> usuarios = new List<Usuario>();
            DataTable dtUsuarios = ObtenerNoRegistradosDirectorioActivoDB();

            foreach (DataRow dr in dtUsuarios.Rows)
            {
                Usuario usuario = new Usuario();

                usuario.ClaveUsuario = dr["ClaveUsuario"].ToString();
                usuario.NumeroEmpleado = (int)dr["NumeroEmpleado"];
                usuario.Nombre = dr["Nombre"].ToString();
                usuario.Apellidos = dr["Apellidos"].ToString();
                usuario.Correo = dr["Correo"].ToString();
                usuario.Fotografia = dr["Fotografia"].ToString();

                usuarios.Add(usuario);
            }
            return usuarios;

        }
        private bool InsertarDB(Usuario usuario)
        {

            bool inserto = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_Insertar";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveUsuario", usuario.ClaveUsuario);
                cmd.Parameters.AddWithValue("@NumeroEmpleado", usuario.NumeroEmpleado);
                cmd.Parameters.AddWithValue("@ClaveEmpresa", usuario.EmpresaActivo.ClaveEmpresa);
                cmd.Parameters.AddWithValue("@esActive", usuario.esActive);
                cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);

                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);

                cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@CambioContrasena", usuario.CambioContrasena);

                cmd.Parameters.AddWithValue("@RutaFotografia", usuario.Fotografia);
                cmd.Parameters.AddWithValue("@ClaveUsuarioCreacion", usuario.ClaveUsuarioAlta);
                cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);

                cmd.ExecuteNonQuery();

                inserto = true;

                if (inserto)
                {
                    foreach (Perfil perfil in usuario.Perfiles)
                    {
                        perfil.ClaveUsuarioAlta = usuario.ClaveUsuarioAlta;
                        _perfilDA.InsertarPerfilUsuario(perfil, usuario.ClaveUsuario);
                    }

                    foreach (Empresa empresa in usuario.Empresas)
                    {
                        _empresaDA.InsertarEmpresaUsuario(empresa, usuario.ClaveUsuario);
                    }
                }

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return inserto;

        }
        private bool ActualizarDB(Usuario usuario)
        {
            bool actualizo = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_Actualizar";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveUsuario", usuario.ClaveUsuario);
                cmd.Parameters.AddWithValue("@NumeroEmpleado", usuario.NumeroEmpleado);
                cmd.Parameters.AddWithValue("@ClaveEmpresa", usuario.EmpresaActivo.ClaveEmpresa);
                cmd.Parameters.AddWithValue("@esActive", usuario.esActive);


                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);

                cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@NumeroIntentos", usuario.NumeroIntentos);

                cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);
                cmd.Parameters.AddWithValue("@CambiarContrasena", usuario.CambioContrasena);
                cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);

                cmd.Parameters.AddWithValue("@ClaveUsuarioActualizacion", usuario.ClaveUsuarioUltimaActualizacion);

                cmd.ExecuteNonQuery();
                actualizo = true;

                // if (actualizo)
                // {
                //     bool elimino = _perfilDA.EliminarPerfilUsuario(usuario.ClaveUsuario);

                //     if (elimino)
                //     {
                //         foreach (Perfil perfil in usuario.Perfiles)
                //         {
                //             perfil.ClaveUsuarioAlta = usuario.ClaveUsuarioUltimaActualizacion;
                //             _perfilDA.InsertarPerfilUsuario(perfil, usuario.ClaveUsuario);
                //         }
                //     }

                //     bool eliminoEmpresas = _empresaDA.EliminarEmpresasUsuario(usuario.ClaveUsuario);

                //     if (eliminoEmpresas)
                //     {
                //         foreach (Empresa empresa in usuario.Empresas)
                //         {
                //             _empresaDA.InsertarEmpresaUsuario(empresa, usuario.ClaveUsuario);
                //         }
                //     }
                // }

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return actualizo;
        }

        #endregion

        #region Obtener
        public Usuario Obtener(string ClaveUsuario)
        {
            Usuario usuario = ObtenerUsuario(ClaveUsuario);
            usuario ??= new Usuario();
            return usuario;
        }
        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> usuarios = ObtenerUsuarios();
            return usuarios;
        }
        public List<Usuario> ObtenerUsuariosDirectorioActivo()
        {
            return ObtenerNoRegistradosDirectorioActivo();
        }
        public Usuario ObtenerUsuariosDirectorioActivo(string ClaveUsuario)
        {
            return ObtenerUsuarioDirectorioActivoObj(ClaveUsuario);
        }
        public List<Usuario> ObtenerUsuariosDirectorioActivoTodos()
        {
            return ObtenerUsuarioDirectorioActivoTodosObj();
        }
        public Perfil ObtenerPerfil(string ClaveUsuario)
        {

            int ClavePerfil = 0;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Usuario_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", ClaveUsuario);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClavePerfil = (int)reader["ClavePerfil"];
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return _perfilDA.Obtener(ClavePerfil);

        }
        public DataTable ObtenerActividad()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "BitacoraActividad_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }
        public DataTable ObtenerDetalleActividad(string claveUsuario)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "BitacoraActividadDetalle_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }
        #endregion

        #region Cambio_en_Basee
        public bool Desbloquear(string claveUsuario)
        {
            bool result = false;
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Usuario_Desbloquear";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return result;
        }
        public string AutenticarUsuario(string claveUsuario)
        {

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Usuario_AutenticarUsuario";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                SqlParameter parameter = new SqlParameter("@Password", SqlDbType.VarChar, 1024);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return (string)parameter.Value;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        public void RegistrarActividad(string claveUsuario, int tipoRegistro)
        {

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "BitacoraActividad_Registrar";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                cmd.Parameters.AddWithValue("@TipoRegistro", tipoRegistro);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }
        public bool ActualizandoInformacion()
        {
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "ActualizaInformacion_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@Actualizando", SqlDbType.Bit); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                return (bool)parameter.Value;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        public void ActualizaIntento(string claveUsuario)
        {

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Usuario_ActualizarIntento";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }
        public bool ExisteUsuario(string claveUsuario)
        {
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Usuario_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return false;

        }
        public bool Insertar(Usuario usuario)
        {
            return InsertarDB(usuario);
        }
        public bool Actualizar(Usuario usuario)
        {
            return ActualizarDB(usuario);
        }
        public void ReiniciaIntento(string claveUsuario)
        {

            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "Usuario_ReiniciarIntento";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }

        #endregion
    }

}