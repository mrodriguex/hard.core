
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace HARD.CORE.DAT
{
    public class EncuestaDA
    {

        #region "Singleton"

        private static EncuestaDA instance = null;

        private static object mutex = new object();
        private EncuestaDA()
        {
        }

        public static EncuestaDA GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EncuestaDA();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerCabeceros()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtieneCabecero]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        public DataTable ObtenerDetalleEncuesta(int claveEncuesta)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtieneDetalle]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEncuesta", claveEncuesta);

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        public DataTable ObtenerPreguntasBase()
        {
            DataTable dataTable = new DataTable();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtienePreguntasBase]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                IDataReader reader = cmd.ExecuteReader(); dataTable.Load(reader);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
            return (DataTable)Convert.ChangeType(dataTable, typeof(DataTable));
        }

        public int InsertaEncuesta(Encuesta encuesta)
        {
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);
            string queryName = "[dbo].[Encuesta_InsertaCabecero]";

            SqlCommand cmd = new SqlCommand(queryName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreEncuesta", encuesta.NombreEncuesta);
            cmd.Parameters.AddWithValue("@Estatus", encuesta.Estatus);

            SqlParameter parameter = new SqlParameter("@ClaveEncuesta", 0);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);

            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    cmd.ExecuteNonQuery();

                    int claveEncuesta = Convert.ToInt32(parameter.Value);

                    foreach (Pregunta pregunta in encuesta.Preguntas)
                    {
                        InsertaPreguntas(claveEncuesta, pregunta, connection, transaction);
                    }

                    transaction.Commit();

                    return claveEncuesta;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }

        public void ActualizaEncuesta(Encuesta encuesta)
        {
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ActualizaCabecero]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClaveEncuesta", encuesta.ClaveEncuesta);
                cmd.Parameters.AddWithValue("@NombreEncuesta", encuesta.NombreEncuesta);
                cmd.Parameters.AddWithValue("@Estatus", encuesta.Estatus);

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    cmd.ExecuteNonQuery();

                    EliminaPreguntas(encuesta.ClaveEncuesta, connection, transaction);

                    foreach (Pregunta pregunta in encuesta.Preguntas)
                    {
                        InsertaPreguntas(encuesta.ClaveEncuesta, pregunta, connection, transaction);
                    }

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }

        protected void InsertaPreguntas(Int32 ClaveEncuesta, Pregunta pregunta, SqlConnection connection, SqlTransaction transaction)
        {

            string queryName = "[dbo].[Encuesta_InsertaDetalle]";
            SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ClaveEncuesta", ClaveEncuesta);
            cmd.Parameters.AddWithValue("@Pregunta", pregunta.Descripcion);

            SqlParameter parameter = new SqlParameter("@ClavePregunta", 0);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);

            cmd.ExecuteNonQuery();

        }

        protected void EliminaPreguntas(Int32 ClaveEncuesta, SqlConnection connection, SqlTransaction transaction)
        {

            string queryName = "[dbo].[Encuesta_EliminaDetalle]";
            SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ClaveEncuesta", ClaveEncuesta);

            cmd.ExecuteNonQuery();

        }

        public Encuesta ObtenerEncuesta(int claveEncuesta)
        {
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtieneCabecero]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEncuesta", claveEncuesta);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Encuesta(
                            (int)reader["ClaveEncuesta"]
                            , (string)reader["NombreEncuesta"]
                            , (bool)reader["Estatus"]
                            , ObtenerPreguntas(claveEncuesta)
                            );
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return null;
        }

        private List<Pregunta> ObtenerPreguntas(int claveEncuesta)
        {
            List<Pregunta> preguntas = new List<Pregunta>();

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtieneDetalle]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveEncuesta", claveEncuesta);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pregunta pregunta = new Pregunta((int)reader["ClavePregunta"], (string)reader["Descripcion"], 0);
                        preguntas.Add(pregunta);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return preguntas;
        }

        public int ObtieneEstatusUsuario(string claveUsuario)
        {
            int clave = 0;
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE");
            SqlConnection connection = new SqlConnection(sqlConn);

            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string queryName = "[dbo].[Encuesta_ObtenerEstatusUsuario]";
                    SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

                    SqlParameter sqlParameter = new SqlParameter("@EstatusEncuesta", 0);
                    sqlParameter.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(sqlParameter);

                    cmd.ExecuteNonQuery();
                    clave = Convert.ToInt32(sqlParameter.Value);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
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
            return (clave);
        }

        public Encuesta ObtenerEncuestaActiva()
        {
            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtieneCabecero]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estatus", true);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Encuesta(
                            (int)reader["ClaveEncuesta"]
                            , (string)reader["NombreEncuesta"]
                            , (bool)reader["Estatus"]
                            , ObtenerPreguntas((int)reader["ClaveEncuesta"])
                            );
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return null;
        }

        public Pregunta ObtenerPregunta(int clavePregunta)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();
                string queryName = "[dbo].[Encuesta_ObtieneDetalle]";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClavePregunta", clavePregunta);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Pregunta((int)reader["ClavePregunta"], (string)reader["Descripcion"], 0);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return null;

        }

        public void InsertaRespuestas(Encuesta encuesta, string comentario, string claveUsuario)
        {

            string sqlConn = Config.Configuration.GetConnectionString("SqlConn_HARDCORE"); SqlConnection connection = new SqlConnection(sqlConn); try
            {
                connection.Open();


                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (Pregunta pregunta in encuesta.Preguntas)
                    {
                        InsertaRespuesta(encuesta.ClaveEncuesta, pregunta, claveUsuario, connection, transaction, null);
                    }
                    InsertaRespuesta(encuesta.ClaveEncuesta, null, claveUsuario, connection, transaction, comentario);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

        }

        protected void InsertaRespuesta(int claveEncuesta, Pregunta pregunta, string claveUsuario, SqlConnection connection, SqlTransaction transaction, string comentario = "")
        {

            string queryName = "[dbo].[Encuesta_InsertaRespuesta]";
            SqlCommand cmd = new SqlCommand(queryName, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ClaveEncuesta", claveEncuesta);
            if (pregunta != null)
            {
                cmd.Parameters.AddWithValue("@ClavePregunta", pregunta.ClavePregunta);
                cmd.Parameters.AddWithValue("@Calificacion", pregunta.Calificacion);
            }
            if (comentario != "")
            {
                cmd.Parameters.AddWithValue("@Comentario", comentario);
            }
            cmd.Parameters.AddWithValue("@ClaveUsuario", claveUsuario);

            SqlParameter parameter = new SqlParameter("@ClaveRespuesta", 0); parameter.Direction = ParameterDirection.Output; cmd.Parameters.Add(parameter);

            cmd.ExecuteNonQuery();
        }

    }
}
