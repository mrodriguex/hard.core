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

        public List<Seguridad> ObtenerTodos(int idPerfil)
        {
            string encodedIdPerfil = Uri.EscapeDataString(idPerfil.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Seguridad>>(endPoint: "api/v1//Seguridad/ObtenerTodos", query: $"idPerfil={encodedIdPerfil}");
        }

        public List<Seguridad> Obtener(int idPerfil, int asignado)
        {
            string encodedIdPerfil = Uri.EscapeDataString(idPerfil.ToString());
            string encodedAsignado = Uri.EscapeDataString(asignado.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Seguridad>>(endPoint: "api/v1//Seguridad/Obtener", query: $"idPerfil={encodedIdPerfil}&asignado={asignado}");
        }
          
        public int Asignar(int idPerfil, Seguridad seguridad)
        {
            string encodedIdPerfil = Uri.EscapeDataString(idPerfil.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<int>(obj: seguridad, endPoint: "api/v1/Seguridad/Asignar", query: $"idPerfil={idPerfil}");
        }

        public Seguridad ObtenerSeguridad(int tipoEntidad, int idUsuario)
        {
            string encodedTipoEntidad = Uri.EscapeDataString(tipoEntidad.ToString());
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario.ToString());
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Seguridad>(endPoint: "api/v1//Seguridad/ObtenerSeguridad", query: $"idPerfil={encodedTipoEntidad}&asignado={encodedClaveUsuario}");
        }
    }
}
