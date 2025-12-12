using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IArchivosDA
    {
        public string ObtenerRuta(EnumRuta claveRuta);
        public string GuardarArchivo(byte[] imageByteArray, string nombreArchivo, string rutaCarpeta);
        public string ActualizarArchivo(byte[] imageByteArray, string nombreArchivo, string rutaCarpeta, string rutaArchivoAnterior);
        public byte[] LeerArchivo(string nombreArchivo, string rutaCarpeta);
        public byte[] LeerArchivo(string ruta);
        public bool EliminarArchivo(string rutaArchivo);
        public string ValidaNombreArchivo(string rutaCarpeta, string archivoSinExtension, string extension);

    }
}
