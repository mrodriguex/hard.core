using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace HARD.CORE.SER
{
    public class PerfilSER
    {

        #region "Singleton"

        private static PerfilSER instance = null;

        private static object mutex = new object();
        private PerfilSER()
        {
        }

        public static PerfilSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PerfilSER();
                }
            }

            return instance;

        }

        #endregion

        public Perfil ObtenerPerfil(int clavePerfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Perfil>(endPoint: "api/v1/Perfil/Obtener", $"clavePerfil={clavePerfil}");
        }

        public List<Perfil> ObtenerPerfiles()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Perfil>>(endPoint: "api/v1/Perfil/ObtenerTodos");
        }
        public List<Perfil> ObtenerActivos(string claveUsuario)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Perfil>>(endPoint: "api/v1/Perfil/ObtenerAsignado", $"claveUsuario={claveUsuario}");
        }

        public int InsertarPerfil(Perfil perfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            int result = httpClientManager.PostWebResult<int>(perfil, "api/v1/Perfil/Insertar");
            return result;
        }

        public bool ActualizarPerfil(Perfil perfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PutWebResult<bool>(perfil, "api/v1/Perfil/Actualizar");
        }
    }
}
