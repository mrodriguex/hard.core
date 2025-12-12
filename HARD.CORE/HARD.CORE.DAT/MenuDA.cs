using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using HARD.CORE.DAT.Interfaces;

namespace HARD.CORE.DAT
{
    public class MenuDA : IMenuDA
    {

        private readonly string _connectionString;

        public MenuDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private List<Menu> ObtenerMenu_UsuarioDB(string claveUsuario, int clavePerfil)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                string queryName = "UsuarioMenu_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);
                cmd.Parameters.AddWithValue("@ClavePerfil", clavePerfil);

                List<Menu> menu = new List<Menu>();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        menu.Add(new Menu((int)reader["ClaveMenu"],
                            (string)reader["Nombre"],
                            (string)reader["Imagen"],
                            (string)reader["Ruta"],
                            (int)reader["ClaveMenuPadre"]));
                    }
                }

                return menu;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }
        private List<Menu> ObtenerTodosDB(bool? estatus = null, int? clavePerfil = null, string claveUsuario = null)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                string queryName = "UsuarioMenu_Obtener";

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

                List<Menu> menuS = new List<Menu>();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Menu menu = new Menu();
                        menu.ClaveMenu = (int)reader["ClaveMenu"];
                        menu.Nombre = reader["Nombre"].ToString();  
                        menu.Imagen = reader["Imagen"].ToString();
                        menu.Ruta = reader["Ruta"].ToString();
                        menu.Orden = (int)reader["Orden"];
                        menu.ClaveMenuPadre = (int)reader["ClaveMenuPadre"];

                        menuS.Add(menu);
                    }
                }

                return menuS;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        private Menu ObtenerDB(int claveMenu)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                string queryName = "UsuarioMenu_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveMenu", claveMenu);

                Menu menu = new Menu();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        menu.ClaveMenu = (int)reader["ClaveMenu"];
                        menu.Nombre = reader["Nombre"].ToString();
                        menu.ClaveMenuPadre = (int)reader["ClaveMenuPadre"];
                    }
                }

                return menu;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        private List<Menu> ObtenerMenu_PerfilDB(int clavePerfil)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                string queryName = "UsuarioMenu_Perfil";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClavePerfil", clavePerfil);

                List<Menu> menu = new List<Menu>(); 

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        Menu item = new Menu();

                        item.ClaveMenu = (int)reader["ClaveMenu"];
                        item.Nombre = reader["Nombre"].ToString();
                        item.ClaveMenuPadre = (int)reader["ClaveMenuPadre"];
                        item.PerteneceAPerfil = (bool)reader["PerteneceAPerfil"];

                        menu.Add(item);
                    }
                }

                return menu;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }
        #endregion

        #region Public
        public List<Menu> ObtenerTodos(bool? estatus = null, int? clavePerfil = null, string claveUsuario = null)
        {
            return ObtenerTodosDB(estatus, clavePerfil, claveUsuario);
        }
        public Menu Obtener(int claveMenu)
        {
            return ObtenerDB(claveMenu);
        }
        public List<Menu> ObtenerMenu_Usuario(string claveUsuario, int clavePerfil)
        {
            return ObtenerMenu_UsuarioDB(claveUsuario, clavePerfil);
        }
        public List<Menu> ObtenerMenu_Perfil(int clavePerfil)
        {
            return ObtenerTodosDB(estatus: null,clavePerfil:clavePerfil,claveUsuario: null);
        }
        #endregion

        #region Cambios_En_Base
        public int Insertar(Menu Menu)
        {
            throw new NotImplementedException();
        }
        public bool Actualizar(Menu Menu)
        {
            throw new NotImplementedException();
        }
        #endregion

        public bool ConfigurarMenu_Perfil(int clavePerfil, List<Menu> menus)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    EliminarMenu_Perfil(clavePerfil, connection, transaction);

                    foreach (var menu in menus)
                    {
                        IngresarMenu_Perfil(clavePerfil, menu.ClaveMenu, connection, transaction);
                    }

                    transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }

        public void EliminarMenu_Perfil(int clavePerfil, SqlConnection connection, SqlTransaction transaction)
        {
            string queryName = "[dbo].[Menu_EliminarMenu]";

            SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClavePerfil", clavePerfil);

            cmd.ExecuteNonQuery();

        }

        public void IngresarMenu_Perfil(int clavePerfil, int claveMenu, SqlConnection connection, SqlTransaction transaction)
        {

            string queryName = "[dbo].[Menu_IngresarMenu]";
            SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ClavePerfil", clavePerfil);
            cmd.Parameters.AddWithValue("@ClaveMenu", claveMenu);

            cmd.ExecuteNonQuery();

        }

    }

}