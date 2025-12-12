using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;

using System;
using System.Data;
using System.IO;
using System.Transactions;

namespace HARD.CORE.DAT
{
    public class ArchivosDescargasDA: IArchivosDescargasDA
    {
        private readonly string _connectionString;
        private readonly IArchivosDA _archivosDA;

        /// <summary>
        /// Initializes a new instance of the <see cref="RutasDA"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration to use.
        /// </param>
        /// <remarks>
        /// Initializes a new instance of the <see cref="RutasDA"/> class.
        /// </remarks>
        public ArchivosDescargasDA(IConfiguration configuration, IArchivosDA archivosDA)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
            _archivosDA = archivosDA;
        }

        // public byte[] ObtenerPoliticasPrivacidad()
        // {
        //     return _archivosDA.LeerArchivo("Política de Privacidad.pdf", _archivosDA.ObtenerRuta(EnumRuta.Login));
        // }

        public byte[] ObtenerSTPS(int claveCliente, int numeroEconomico)
        {
            string ruta = "";
            string sqlConn = _connectionString;
            SqlConnection connection = new SqlConnection(sqlConn); connection.Open();
            try
            {
                string queryName = "Archivo_Obtener_Certificado_STPS";

                SqlCommand cmd = new SqlCommand(queryName, connection); cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
                cmd.Parameters.AddWithValue("@NumeroEconomico", numeroEconomico);

                SqlParameter parameter = new SqlParameter("@Ruta", "@Ruta");
                parameter.Direction = ParameterDirection.Output;
                parameter.Size = 500;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                ruta = Convert.ToString(parameter.Value);
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }

            return _archivosDA.LeerArchivo(ruta);
        }
    }
}
