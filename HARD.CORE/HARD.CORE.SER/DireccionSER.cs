using CRY.PCC.OBJ;
using CRY.PCC.SER.Helpers;

using System.Data;

namespace CRY.PCC.SER
{
    public class DireccionSER
    {

        #region "Singleton"

        private static DireccionSER instance = null;

        private static object mutex = new object();

        private DireccionSER()
        {
        }

        public static DireccionSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new DireccionSER();
                }
            }

            return instance;

        }

        #endregion

        public Direccion ObtenerDireccion(int claveTipoUbicacion, int claveUbicacion)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Direccion>("api/Direccion/ObtenerDireccion", $"claveTipoUbicacion={claveTipoUbicacion}&claveUbicacion={claveUbicacion}");
        }

        public DataTable ObtenerDirecciones_CorreosMexico(int claveEstado, int claveMunicipio)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>("api/Direccion/ObtenerDirecciones_CorreosMexico", $"claveEstado={claveEstado}&claveMunicipio={claveMunicipio}");
        }

    }
}
