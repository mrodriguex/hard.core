using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.SER
{
    public class EmpresaSER
    {

        #region "Singleton"

        private static EmpresaSER instance = null;

        private static object mutex = new object();
        private EmpresaSER()
        {
        }

        public static EmpresaSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EmpresaSER();
                }
            }
            return instance;
        }

        #endregion


        public List<Empresa> ObtenerActivos(string claveUsuario,int clavePerfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Empresa>>(endPoint: "api/v1/Empresa/ObtenerAsignado", $"claveUsuario={claveUsuario}&clavePerfil={clavePerfil}");
        }
        public List<Empresa> ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Empresa>>(endPoint: "api/v1/Empresa/ObtenerTodos");
        }
    }
       
}
