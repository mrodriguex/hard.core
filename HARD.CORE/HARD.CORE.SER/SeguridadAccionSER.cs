using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.SER
{
    public class SeguridadAccionSER
    {

        #region "Singleton"

        private static SeguridadAccionSER instance = null;

        private static object mutex = new object();

        private SeguridadAccionSER()
        {
        }

        public static SeguridadAccionSER GetInstance()
        {
            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new SeguridadAccionSER();
                }
            }

            return instance;
        }

        #endregion

        //public Seguridad Obtener(int clavePerfil, int claveEntidad)
        //{
        //    string encodedClavePerfil = Uri.EscapeDataString(clavePerfil.ToString());
        //    string encodedClaveEntidad = Uri.EscapeDataString(claveEntidad.ToString());
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    return httpClientManager.GetWebResult<Seguridad>(endPoint: "api/v1/SeguridadAccion/Obtener", query: $"clavePerfil={encodedClavePerfil}&claveEntidad={encodedClaveEntidad}");
        //}

        public List<SeguridadAccion> ObtenerPorPerfil(int clavePerfil, bool? asignado = null)
        {
            string encodedClavePerfil = Uri.EscapeDataString(clavePerfil.ToString());
            string encodedAsignado = Uri.EscapeDataString(asignado.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<SeguridadAccion>>(endPoint: "api/v1/SeguridadAccion/ObtenerPorPerfil", query: $"clavePerfil={encodedClavePerfil}");
        }

        public bool Actualizar(List<SeguridadAccion> listadoSeguridadAccion)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PutWebResult<bool>(obj: listadoSeguridadAccion, endPoint: "api/v1/SeguridadAccion/Actualizar");
        }
    }
}
