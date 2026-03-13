using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System.Data;

namespace HARD.CORE.SER
{
    public class ClienteSER
    {

        #region "Singleton"

        private static ClienteSER instance = null;

        private static object mutex = new object();
        private ClienteSER()
        {
        }

        public static ClienteSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new ClienteSER();
                }
            }

            return instance;

        }

        #endregion
        public DataTable ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Cliente/ObtenerTodos");
        }

        public Cliente Obtener(int claveCliente)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Cliente>(endPoint: "api/v1/Cliente/Obtener", $"claveCliente={claveCliente}");
        }

        public DataTable ObtenerNoRegistrados()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Cliente/ObtenerNoRegistrados");
        }

        public DataTable ObtenerClientesHijos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Cliente/ObtenerClientesHijos");
        }
    }
}
