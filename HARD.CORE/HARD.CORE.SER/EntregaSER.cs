using HARD.CORE.SER.Helpers;
using System;
using System.Data;

namespace HARD.CORE.SER
{
    public class EntregaSER
    {

        #region "Singleton"
        private static EntregaSER instance = null;
        private static readonly object mutex = new object();

        private EntregaSER() { }

        public static EntregaSER GetInstance()
        {
            if (instance == null)
            {
                lock (mutex)
                {
                    instance ??= new EntregaSER();
                }
            }
            return instance;
        }
        #endregion
        public DataTable ObtenerClavesArrendamientos(int claveCliente)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1//Entrega/ObtenerClavesArrendamientos", query: $"claveCliente={claveCliente}");
        }

        public DataTable ObtenerPorCliente(int claveCliente, DateTime fechaInicio, DateTime fechaFin, int? claveProducto = null)
        {
            string encodedFechaInicio = Uri.EscapeDataString(fechaInicio.ToString("yyyy-MM-dd"));
            string encodedFechaFin = Uri.EscapeDataString(fechaFin.ToString("yyyy-MM-dd"));
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1//Entrega/ObtenerPorCliente", query: $"claveCliente={claveCliente}&fechaInicio={encodedFechaInicio}&fechaFin={encodedFechaFin}&claveProducto={claveProducto}");
        }

        public DataTable ObtenerArrendamientosPorCliente(int claveCliente, DateTime fechaInicio, DateTime fechaFin, string claveTanque = "")
        {
            string encodedFechaInicio = Uri.EscapeDataString(fechaInicio.ToString("yyyy-MM-dd"));
            string encodedFechaFin = Uri.EscapeDataString(fechaFin.ToString("yyyy-MM-dd"));
            string encodedClaveTanque = Uri.EscapeDataString(claveTanque);

            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1//Entrega/ObtenerArrendamientosPorCliente", query: $"claveCliente={claveCliente}&fechaInicio={encodedFechaInicio}&fechaFin={encodedFechaFin}&claveArrendamiento={encodedClaveTanque}");
        }
    }
}
