using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.SER
{
    public class NotificacionSER
    {

        #region "Singleton"
        private static NotificacionSER instance = null;
        private static readonly object mutex = new object();

        private NotificacionSER() { }

        public static NotificacionSER GetInstance()
        {
            if (instance == null)
            {
                lock (mutex)
                {
                    instance ??= new NotificacionSER();
                }
            }
            return instance;
        }
        #endregion

        public Notificacion Obtener(int claveNotifiacion)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Notificacion>(endPoint: "api/v1//Notificacion/Obtener", query: $"claveNotificacion={claveNotifiacion}");
        }

        public DataTable ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Notificacion/ObtenerTodos");
        }

        public DataTable ObtenerActivos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Notificacion/ObtenerActivos");
        }

        public DataTable ObtenerUsuariosPermitidos(int? claveNotificacion = null)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1//Notificacion/ObtenerUsuariosPermitidos", query: $"claveNotificacion={claveNotificacion}");
        }

        public List<Notificacion> ObtenerPorUsuario(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Notificacion>>(endPoint: "api/v1//Notificacion/ObtenerPorUsuario", query: $"claveUsuario={encodedClaveUsuario}");
        }

        public bool ExistenPendientesPorLeer(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<bool>(endPoint: $"api/v1/Notificacion/ExistenPendientesPorLeer", query: $"claveUsuario={encodedClaveUsuario}");
        }

        public bool ConClientesEspecificos(int claveNotificacion)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<bool>(endPoint: $"api/v1/Notificacion/ConClientesEspecificos", query: $"claveNotificacion={claveNotificacion}");
        }

        public int Insertar(Notificacion notificacion)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<int>(notificacion, $"api/Notificacion/Insertar");
        }

        public bool Actualizar(Notificacion notificacion)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PutWebResult<bool>(notificacion, $"api/Notificacion/Actualizar");
        }

        public bool MarcarComoVisto(NotificacionDetalle notificacionDetalle)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PutWebResult<bool>(notificacionDetalle, $"api/Notificacion/MarcarComoVisto");
        }
    }
}


