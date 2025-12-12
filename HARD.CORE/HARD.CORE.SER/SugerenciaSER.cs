using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System.Data;

namespace HARD.CORE.SER
{
    public class SugerenciaSER
    {

        #region "Singleton"

        private static SugerenciaSER instance = null;

        private static object mutex = new object();
        private SugerenciaSER()
        {
        }

        public static SugerenciaSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new SugerenciaSER();
                }
            }
            return instance;
        }

        #endregion

        public bool InsertarSugerencia(Sugerencia sugerencia, bool esAnonimo)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<bool>(sugerencia, "api/Sugerencia/InsertarSugerencia", $"esAnonimo={esAnonimo}");
        }

        public DataTable ObtenerSugerencias()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Sugerencia/ObtenerSugerencias");
        }
    }
}
