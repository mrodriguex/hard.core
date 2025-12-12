using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HARD.CORE.DAT
{

    public class ArchivosDA : IArchivosDA
    {

        private readonly string _connectionString;
        private readonly IRutasDA _rutasDA;

        /// <summary>
        /// Initializes a new instance of the <see cref="RutasDA"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration to use.
        /// </param>
        /// <remarks>
        /// Initializes a new instance of the <see cref="RutasDA"/> class.
        /// </remarks>
        public ArchivosDA(IConfiguration configuration, IRutasDA rutasDA)
        {
            _connectionString = configuration.GetConnectionString("SqlConn_HARDCORE");
            _rutasDA = rutasDA;
        }

        public string ObtenerRuta(EnumRuta claveRuta)
        {
            Rutas rutas = _rutasDA.Obtener((int)claveRuta);
            return rutas.Ruta;

        }
        public string GuardarArchivo(byte[] imageByteArray, string nombreArchivo, string rutaCarpeta)
        {
            if (imageByteArray is null) return "";
            string extension = Path.GetExtension(nombreArchivo);
            string archivoSinExtension = Path.GetFileNameWithoutExtension(nombreArchivo);

            //string archivoSinExtension = nombreArchivo.Substring(0, nombreArchivo.Length - extension.Length);
            if (!Directory.Exists(rutaCarpeta)) { Directory.CreateDirectory(rutaCarpeta); }

            string respuesta = ValidaNombreArchivo(rutaCarpeta, archivoSinExtension, extension);
            //string rutaFinal = string.Concat(rutaCarpeta, respuesta);
            string rutaFinal = Path.Combine(rutaCarpeta, respuesta);
            File.WriteAllBytes(rutaFinal, imageByteArray);

            return respuesta;
        }

        public string ActualizarArchivo(byte[] imageByteArray, string nombreArchivo, string rutaCarpeta, string rutaArchivoAnterior)
        {
            string respuesta = "";
            if (imageByteArray != null)
            {
                string extension = Path.GetExtension(nombreArchivo);
                string archivoSinExtension = nombreArchivo.Substring(0, nombreArchivo.Length - extension.Length);
                if (!Directory.Exists(rutaCarpeta)) { Directory.CreateDirectory(rutaCarpeta); }

                respuesta = ValidaNombreArchivo(rutaCarpeta, archivoSinExtension, extension);
                //string rutaFinal = string.Concat(rutaCarpeta, respuesta);
                string rutaFinal = Path.Combine(rutaCarpeta, respuesta);
                File.WriteAllBytes(rutaFinal, imageByteArray);

                if (File.Exists(rutaFinal) && File.Exists(Path.Combine(rutaCarpeta, rutaArchivoAnterior)))
                {
                    EliminarArchivo(Path.Combine(rutaCarpeta, rutaArchivoAnterior));
                }
            }
            else
            {
                EliminarArchivo(Path.Combine(rutaCarpeta, rutaArchivoAnterior));
            }
            return respuesta;
        }

        public byte[] LeerArchivo(string nombreArchivo, string rutaCarpeta)
        {
            //if (nombreArchivo.Length > 0) { return File.ReadAllBytes(string.Concat(rutaCarpeta, nombreArchivo)); }
            if (nombreArchivo.Length > 0) { return File.ReadAllBytes(Path.Combine(rutaCarpeta, nombreArchivo)); }
            return null;
        }

        public byte[] LeerArchivo(string ruta)
        {
            if (File.Exists(ruta)) { return File.ReadAllBytes(ruta); }
            return null;
        }
        public bool EliminarArchivo(string rutaArchivo)
        {
            bool resupuesta = false;
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                    resupuesta = true;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return resupuesta;
        }

        public string ValidaNombreArchivo(string rutaCarpeta, string archivoSinExtension, string extension)
        {
            string archivoConExtension = string.Concat(archivoSinExtension, extension);
            int numero = 1;
            int ceros = 2;
            //while (File.Exists(string.Concat(rutaCarpeta, archivoConExtension)))
            while (File.Exists(Path.Combine(rutaCarpeta, archivoConExtension)))
            {
                ceros = (3 - numero.ToString().Length > 0) ? 3 - numero.ToString().Length : 0;
                archivoConExtension = string.Concat(archivoSinExtension, "_", new string('0', ceros), numero, extension);

                numero++;
            }
            return archivoConExtension;
        }
    }
}
