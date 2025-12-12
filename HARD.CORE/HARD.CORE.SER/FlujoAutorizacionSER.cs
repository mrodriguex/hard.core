using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRY.PCC.SER
{
    public class FlujoAutorizacionSER
    {
        #region "Singleton"

        private static FlujoAutorizacionSER instance = null;

        private static object mutex = new object();
        private FlujoAutorizacionSER()
        {
        }

        public static FlujoAutorizacionSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new FlujoAutorizacionSER();
                }
            }
            return instance;
        }

        #endregion

        public List<FlujoAutorizacion> FlujoAutorizacionTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<FlujoAutorizacion>>(endPoint: "api/v1/FlujoAutorizacion/ObtenerTodos");
        }

    }
}
