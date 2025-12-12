using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.SER
{
    public class AvisoSER
    {
        #region "Singleton"
        private static AvisoSER instance = null;
        private static readonly object mutex = new object();

        private AvisoSER() { }

        public static AvisoSER GetInstance()
        {
            if (instance == null)
            {
                lock (mutex)
                {
                    instance ??= new AvisoSER();
                }
            }
            return instance;
        }
        #endregion

        public List<Aviso> ObtenerActivosLista()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<List<Aviso>>(endPoint: "api/v1/Aviso/ObtenerActivosLista");
            return result;
        }

        public List<Aviso> ObtenerAvisosTodosCarrusel()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Aviso>>(endPoint: "api/v1/Aviso/ObtenerTodos");
        }

        public Aviso ObtenerAviso(int claveAviso)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Aviso>(endPoint: "api/v1/Aviso/Obtener", $"claveAviso={claveAviso}");
        }

        public int InsertarAvisoCarrusel(Aviso aviso)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            int Result = httpClientManager.PostWebResult<int>(aviso, "api/v1/Aviso/Insertar");

            return Result;

        }

        public bool ActualizarAvisoCarrusel(Aviso aviso)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool Result = httpClientManager.PutWebResult<bool>(aviso, "api/v1/Aviso/Actualizar");
            return Result;
        }

        public bool EliminarAvisoCarrusel(int claveAviso)
        {
          
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool Result = httpClientManager.PutWebResult<bool>(claveAviso, "api/v1/Aviso/Eliminar");

            return Result;
        }

        public bool PuedeInsertarCarrusel()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.GetWebResult<bool>(endPoint: "api/v1//Aviso/PuedeInsertarAviso");
            return result;
        }
    }
}
