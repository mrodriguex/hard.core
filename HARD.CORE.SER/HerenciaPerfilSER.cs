using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.SER
{
    public class HerenciaPerfilSER
    {

        #region "Singleton"

        private static HerenciaPerfilSER instance = null;

        private static object mutex = new object();
        private HerenciaPerfilSER()
        {
        }

        public static HerenciaPerfilSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new HerenciaPerfilSER();
                }
            }
            return instance;
        }

        #endregion

        public List<HerenciaPerfil> ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<HerenciaPerfil>>(endPoint: "api/v1/HerenciaPerfil/ObtenerTodos");
        }
        public HerenciaPerfil Obtener(int claveNivelIngles)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<HerenciaPerfil>(endPoint: "api/v1/HerenciaPerfil/ExisteHerencia", query: $"claveNivelIngles={claveNivelIngles}");
            return result;
        }


        public int Insertar(HerenciaPerfil herenciaPerfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            int result = httpClientManager.PostWebResult<int>(obj: herenciaPerfil, endPoint: "api/v1/HerenciaPerfil/Insertar");
            return result;
        }

        public bool Actualizar(HerenciaPerfil herenciaPerfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: herenciaPerfil, endPoint: "api/v1/HerenciaPerfil/Actualizar");
            return result;
        }
        public bool ExisteHerencia(HerenciaPerfil herenciaPerfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: herenciaPerfil, endPoint: "api/v1/HerenciaPerfil/Existe");
            return result;
        }
    }
}
