using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;


namespace CRY.PCC.SER
{
    public class CorreosSER
    {

        #region "Singleton"

        private static CorreosSER instance = null;

        private static object mutex = new object();
        private CorreosSER()
        {
        }

        public static CorreosSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new CorreosSER();
                }
            }
            return instance;
        }

        #endregion

        public List<Correo> ObtenerTodos()
        {
          
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Correo>>(endPoint: "api/v1/Correo/ObtenerTodos");
        }

        public List<Correo> ObtenerPorPerfil(int clavePerfil, bool? asignado = null)
        {
            string encodedClavePerfil = Uri.EscapeDataString(clavePerfil.ToString());
            string encodedAsignado = Uri.EscapeDataString(asignado.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Correo>>(endPoint: "api/v1/SeguridadAccion/ObtenerPorPerfil", query: $"clavePerfil={encodedClavePerfil}");
        }

        public Correo Obtener(int claveCorreo)
        {
           
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Correo>(endPoint: "api/v1/Correo/Obtener", query: $"claveCorreo={claveCorreo}");
        }

        public List<TipoCorreo> ObtenerTiposCorreo()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<TipoCorreo>>(endPoint: "api/v1/Correo/ObtenerTiposCorreo");
        }

        public List<CorreoVariable> ObtenerVariables(int claveTipoCorreo)
        {

            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<CorreoVariable>>(endPoint: "api/v1/Correo/ObtenerVariables", query: $"claveTipoCorreo={claveTipoCorreo}");
        }

        public List<string> ObtenerCorreos()
        {

            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<string>>(endPoint: "api/v1/Correo/ObtenerCorreosUsuarios");
        }

        public bool Actualizar(Correo correo)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: correo, endPoint: "api/v1/Correo/Actualizar");
            return result;
        }



        public int Insertar(Correo correo)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            int result = httpClientManager.PostWebResult<int>(obj: correo, endPoint: "api/v1/Correo/Insertar");
            return result;
        }

    }
}
