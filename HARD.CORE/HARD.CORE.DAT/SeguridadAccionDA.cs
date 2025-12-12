using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HARD.CORE.NEG
{
    public class SeguridadAccionDA : ISeguridadAccionDA
    {
        private readonly string _connectionString;

        public SeguridadAccionDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }

        #region Private
        private DataTable ObtenerDB(int ClavePerfil, int? ClaveMenu = null)
        {
            DataTable dataTable = new DataTable();
            string sqlConn = _connectionString; SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "SeguridadAccion_Obtener";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClavePerfil", ClavePerfil);
                if (ClaveMenu.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ClaveMenu", ClaveMenu);
                }

                IDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));

        }
        private List<SeguridadAccion> ObtenerSeguridadAccion(int ClavePerfil, int? ClaveMenu = null)
        {
            DataTable dt = ObtenerDB(ClavePerfil, ClaveMenu);
            List<SeguridadAccion> seguridadAccion = new List<SeguridadAccion>();

            foreach (DataRow dr in dt.Rows)
            {
                SeguridadAccion item = new SeguridadAccion();
                item.ClaveSeguridadAccion = (int)dr["ClaveSeguridadAccion"];
                item.ClavePerfil = (int)dr["ClavePerfil"];
                item.ClaveMenu = (int)dr["ClaveMenu"];
                item.Descripcion = dr["Descripcion"].ToString();
                item.Crear = (bool)dr["Crear"];
                item.Modificar = (bool)dr["Modificar"];
                item.Consultar = (bool)dr["Consultar"];
                item.Eliminar = (bool)dr["Eliminar"];
                item.Autorizar = (bool)dr["Autorizar"];
                item.Rechazar = (bool)dr["Rechazar"];
                item.Imprimir = (bool)dr["Imprimir"];
                item.Asignar = (bool)dr["Asignar"];
                item.Cancelar = (bool)dr["Cancelar"];
                item.Orden = (int)dr["Orden"];
                item.ClaveUsuarioUltimaActualizacion = dr["ClaveUsuarioUltimaActualizacion"].ToString();
                seguridadAccion.Add(item);
            }
            return seguridadAccion;
        }
        private bool ActualizarDB(List<SeguridadAccion> seguridadAccionList)
        {
            bool actualizo = false;
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();

                foreach (SeguridadAccion seguridadAccion in seguridadAccionList)
                {
                    string queryName = "SeguridadAccion_Actualizar";

                    SqlCommand cmd = new SqlCommand(queryName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClavePerfil", seguridadAccion.ClavePerfil);
                    cmd.Parameters.AddWithValue("@ClaveMenu", seguridadAccion.ClaveMenu);

                    cmd.Parameters.AddWithValue("@Crear", seguridadAccion.Crear);
                    cmd.Parameters.AddWithValue("@Modificar", seguridadAccion.Modificar);
                    cmd.Parameters.AddWithValue("@Consultar", seguridadAccion.Consultar);
                    cmd.Parameters.AddWithValue("@Eliminar", seguridadAccion.Eliminar);
                    cmd.Parameters.AddWithValue("@Autorizar", seguridadAccion.Autorizar);
                    cmd.Parameters.AddWithValue("@Rechazar", seguridadAccion.Rechazar);
                    cmd.Parameters.AddWithValue("@Imprimir", seguridadAccion.Imprimir);
                    cmd.Parameters.AddWithValue("@Asignar", seguridadAccion.Asignar);
                    cmd.Parameters.AddWithValue("@Cancelar", seguridadAccion.Cancelar);

                    cmd.Parameters.AddWithValue("@ClaveUsuarioUltimaActualizacion", seguridadAccion.ClaveUsuarioUltimaActualizacion);

                    cmd.ExecuteNonQuery();

                }
                actualizo = true;

            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return actualizo;

        }
        #endregion

        #region Public
        public List<SeguridadAccion> ObtenerTodos(int ClavePerfil)
        {
            return ObtenerSeguridadAccion(ClavePerfil);
        }
        public SeguridadAccion Obtener(int ClavePerfil, int ClaveMenu)
        {
            return ObtenerSeguridadAccion(ClavePerfil, ClaveMenu).FirstOrDefault();
        }
        #endregion

        #region Cambios_En_Base
        public bool Actualizar(List<SeguridadAccion> seguridadAccion)
        {
            return ActualizarDB(seguridadAccion);
        }
        #endregion
    }
}
