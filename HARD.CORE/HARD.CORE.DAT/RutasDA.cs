using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;
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
    public class RutasDA : IRutasDA
    {


        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RutasDA"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration to use.
        /// </param>
        /// <remarks>
        /// Initializes a new instance of the <see cref="RutasDA"/> class.
        /// </remarks>
        public RutasDA(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
        }


        #region Private
        private DataTable ObtenerRutasDB(int? claveRuta = null, bool? estatus = null)
        {
            DataTable dataTable = new DataTable();

            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn);
            try
            {
                connection.Open();
                string queryName = "Rutas_ObtenerRutas";

                SqlCommand cmd = new SqlCommand(queryName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveRuta", claveRuta);

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

        private Rutas ObtenerRutaObj(int claveRuta = 0)
        {
            DataTable dtRutas = ObtenerRutasDB(claveRuta);
            DataRow dr = dtRutas.Rows[0];
            Rutas ruta = new Rutas(
                (int)dr["ClaveRuta"]
                , (string)dr["Descripcion"].ToString()
                , (string)dr["Ruta"].ToString()
            );

            return ruta;
        }
        #endregion

        #region Public_Obtener

        public Rutas Obtener(int claveRuta)
        {
            return ObtenerRutaObj(claveRuta);
        }

        public List<Rutas> ObtenerTodos(bool? estatus = null)
        {
            DataTable dtRutas = ObtenerRutasDB(estatus: estatus);
            List<Rutas> rutas = new List<Rutas>();

            foreach (DataRow dr in dtRutas.Rows)
            {
                Rutas ruta = new Rutas(
                    (int)dr["ClaveRuta"],
                    (string)dr["Descripcion"].ToString(),
                    (string)dr["Ruta"].ToString()
                );
                rutas.Add(ruta);
            }

            return rutas;
        }

        public int Insertar(Rutas ruta)
        {
            throw new NotImplementedException();
        }

        public bool Actualizar(Rutas ruta)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Rutas ruta)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
