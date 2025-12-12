using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System.Collections.Generic;
using System.Data;


namespace HARD.CORE.SER
{
    public class NivelInglesSER
    {


        #region "Singleton"

        private static NivelInglesSER instance = null;

        private static object mutex = new object();
        private NivelInglesSER()
        {
        }

        public static NivelInglesSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new NivelInglesSER();
                }
            }
            return instance;
        }

        #endregion

        public List<NivelIngles> ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<NivelIngles>>(endPoint: "api/v1/NivelIngles/ObtenerTodos");
        }
        public NivelIngles Obtener(int claveNivelIngles)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<NivelIngles>(endPoint: "api/v1/NivelIngles/Obtener", query: $"claveNivelIngles={claveNivelIngles}");
            return result;
        }


        public int Insertar(NivelIngles nivelIngles)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            int result = httpClientManager.PostWebResult<int>(obj: nivelIngles, endPoint: "api/v1/NivelIngles/Insertar");
            return result;
        }

        public bool Actualizar(NivelIngles nivelIngles)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: nivelIngles, endPoint: "api/v1/NivelIngles/Actualizar");
            return result;
        }

    }
}
