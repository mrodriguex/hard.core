using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System.Collections.Generic;
using System.Data;


namespace HARD.CORE.SER
{
    public class NivelMinimoEstudiosSER
    {


        #region "Singleton"

        private static NivelMinimoEstudiosSER instance = null;

        private static object mutex = new object();
        private NivelMinimoEstudiosSER()
        {
        }

        public static NivelMinimoEstudiosSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new NivelMinimoEstudiosSER();
                }
            }
            return instance;
        }

        #endregion

        public List<NivelMinimoEstudios> ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<NivelMinimoEstudios>>(endPoint: "api/v1/NivelMinimoEstudios/ObtenerTodos");
        }
        public NivelMinimoEstudios Obtener(int claveNivelMinimoEstudios)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<NivelMinimoEstudios>(endPoint: "api/v1/NivelMinimoEstudios/Obtener", query: $"claveNivelMinimoEstudios={claveNivelMinimoEstudios}");
            return result;
        }


        public int Insertar(NivelMinimoEstudios nivelMinimoEstudios)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            int result = httpClientManager.PostWebResult<int>(obj: nivelMinimoEstudios, endPoint: "api/v1/NivelMinimoEstudios/Insertar");
            return result;
        }

        public bool Actualizar(NivelMinimoEstudios nivelMinimoEstudios)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: nivelMinimoEstudios, endPoint: "api/v1/NivelMinimoEstudios/Actualizar");
            return result;
        }

    }
}
