using HARD.CORE.SER.Helpers;
using System.Data;

namespace HARD.CORE.SER
{
    public class ProductoSER
    {
        #region "Singleton"

        private static ProductoSER instance = null;

        private static object mutex = new object();
        private ProductoSER()
        {
        }

        public static ProductoSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new ProductoSER();
                }
            }

            return instance;

        }

        #endregion
        public DataTable ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Producto/ObtenerTodos");
        }

        public DataTable ObtenerPorCliente(int claveCliente)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Producto/ObtenerPorCliente", query: $"claveCliente={claveCliente}");
        }
    }
}

