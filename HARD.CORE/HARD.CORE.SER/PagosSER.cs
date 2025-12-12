using HARD.CORE.SER.Helpers;
using System.Data;

namespace HARD.CORE.SER
{
    public class PagosSER
    {

        #region " Singleton "

        private static PagosSER instance = null;

        private static object mutex = new object();
        private PagosSER()
        {
        }

        public static PagosSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PagosSER();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerReporteCartera(int claveCliente)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Pagos/ObtenerReporteCartera", $"claveCliente={claveCliente}");
        }
    }
}
