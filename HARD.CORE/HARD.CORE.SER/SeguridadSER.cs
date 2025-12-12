using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;

namespace HARD.CORE.SER
{
    public class SeguridadSER
    {
        #region "Singleton"

        private static SeguridadSER instance = null;

        private static object mutex = new object();
        private SeguridadSER()
        {
        }

        public static SeguridadSER GetInstance()
        {
            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new SeguridadSER();
                }
            }

            return instance;
        }

        #endregion

        public List<Seguridad> ObtenerTodos(int clavePerfil)
        {
            string encodedClavePerfil = Uri.EscapeDataString(clavePerfil.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Seguridad>>(endPoint: "api/v1//Seguridad/ObtenerTodos", query: $"clavePerfil={encodedClavePerfil}");
        }

        public List<Seguridad> Obtener(int clavePerfil, int asignado)
        {
            string encodedClavePerfil = Uri.EscapeDataString(clavePerfil.ToString());
            string encodedAsignado = Uri.EscapeDataString(asignado.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Seguridad>>(endPoint: "api/v1//Seguridad/Obtener", query: $"clavePerfil={encodedClavePerfil}&asignado={asignado}");
        }
          
        public int Asignar(int clavePerfil, Seguridad seguridad)
        {
            string encodedClavePerfil = Uri.EscapeDataString(clavePerfil.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<int>(obj: seguridad, endPoint: "api/v1/Seguridad/Asignar", query: $"clavePerfil={clavePerfil}");
        }

        public Seguridad ObtenerSeguridad(int tipoEntidad, string claveUsuario)
        {
            string encodedTipoEntidad = Uri.EscapeDataString(tipoEntidad.ToString());
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Seguridad>(endPoint: "api/v1//Seguridad/ObtenerSeguridad", query: $"clavePerfil={encodedTipoEntidad}&asignado={encodedClaveUsuario}");
        }
    }
}
