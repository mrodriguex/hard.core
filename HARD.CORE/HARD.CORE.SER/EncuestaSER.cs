using CRY.PCC.OBJ;
using CRY.PCC.SER.Helpers;

using System;
using System.Data;

namespace CRY.PCC.SER
{
    public class EncuestaSER
    {

        #region "Singleton"

        private static EncuestaSER instance = null;

        private static object mutex = new object();
        private EncuestaSER()
        {
        }

        public static EncuestaSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EncuestaSER();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerCabeceros()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>("api/Encuesta/ObtenerCabeceros");
        }

        public DataTable ObtenerDetalleEncuesta(int claveEncuesta)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>("api/Encuesta/ObtenerDetalleEncuesta", $"claveEncuesta={claveEncuesta}");
        }

        public DataTable ObtenerPreguntasBase()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<DataTable>("api/Encuesta/ObtenerPreguntasBase");
        }

        public int InsertaEncuesta(Encuesta encuesta)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<int>(encuesta, "api/Encuesta/InsertaEncuesta");
        }

        public void ActualizaEncuesta(Encuesta encuesta)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.PutWebResult<object>(encuesta, "api/Encuesta/ActualizaEncuesta");
        }

        public Encuesta ObtenerEncuesta(int claveEncuesta)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Encuesta>("api/Encuesta/ObtenerEncuesta", $"claveEncuesta={claveEncuesta}");
        }

        public int ObtieneEstatusUsuario(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<int>("api/Encuesta/ObtieneEstatusUsuario", $"claveUsuario={encodedClaveUsuario}");
        }

        public Encuesta ObtenerEncuestaActiva()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Encuesta>("api/Encuesta/ObtenerEncuestaActiva");
        }

        public Pregunta ObtenerPregunta(int clavePregunta)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<Pregunta>("api/Encuesta/ObtenerPregunta", $"clavePregunta={clavePregunta}");
        }

        public void InsertaRespuestas(Encuesta encuesta, string comentario, string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.PostWebResult<object>(encuesta, "api/Encuesta/InsertaRespuestas", $"comentario={comentario}&claveUsuario={encodedClaveUsuario}");
        }

    }

}
