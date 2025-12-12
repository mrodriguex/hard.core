
using HARD.CORE.OBJ;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;

namespace HARD.CORE.SER.Helpers
{

    public class HttpClientManager
    {
        private string _urlBase;
        private string _token;

        private JsonSerializerSettings _settings
        {
            get; set;
        }

        public HttpClientManager(string urlBase = null, string token = null)
        {
            _urlBase = urlBase;
            _token = token;

            // Configurar las opciones de serialización
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented // Opcional: para formato legible
            };
        }

        public HttpClient GetHttpClient(string token = null)
        {
            var client = new HttpClient();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Agregar todos los encabezados importantes
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PostmanRuntime/7.43.3");
            client.DefaultRequestHeaders.Add("Postman-Token", Guid.NewGuid().ToString());
            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
            client.DefaultRequestHeaders.ExpectContinue = false;

            return (client);
        }


        public void GetWebResult(string endPoint = "", string query = "") => GetWebResult<object>(endPoint: endPoint, query: query);

        public T GetWebResult<T>(string endPoint = "", string query = "")
        {
            var client = this.GetHttpClient(token: _token);
            var queryMark = string.IsNullOrEmpty(query) ? "" : "?";
            var fullUrl = $"{_urlBase}{endPoint}{queryMark}{query}";
            var response = client.GetAsync($"{fullUrl}").Result;
            WebResult<T> webResult = ResponseToWebResult<T>(response: response);
            return webResult.Data;
        }

        public void PostWebResult(object obj, string endPoint = "", string query = "") => PostWebResult<object>(obj: obj, endPoint: endPoint, query: query);

        public T PostWebResult<T>(object obj, string endPoint = "", string query = "")
        {
            var client = this.GetHttpClient(token: _token);

            var content = new StringContent(JsonConvert.SerializeObject(obj, _settings), Encoding.UTF8, "application/json");
            var queryMark = string.IsNullOrEmpty(query) ? "" : "?";
            var fullUrl = $"{_urlBase}{endPoint}{queryMark}{query}";


            var response = client.PostAsync($"{fullUrl}", content).ConfigureAwait(false)
    .GetAwaiter()
    .GetResult();
            response.EnsureSuccessStatusCode();

            WebResult<T> webResult = ResponseToWebResult<T>(response: response);
            return webResult.Data;
        }

        public void PutWebResult(object obj, string endPoint = "", string query = "") => PutWebResult<object>(obj: obj, endPoint: endPoint, query: query);

        public T PutWebResult<T>(object obj, string endPoint = "", string query = "")
        {
            var client = this.GetHttpClient(token: _token);

            var content = new StringContent(JsonConvert.SerializeObject(obj, _settings), Encoding.UTF8, "application/json");
            var queryMark = string.IsNullOrEmpty(query) ? "" : "?";
            var fullUrl = $"{_urlBase}{endPoint}{queryMark}{query}";
            var response = client.PutAsync($"{fullUrl}", content).Result;
            WebResult<T> webResult = ResponseToWebResult<T>(response: response);
            return webResult.Data;
        }

        public void DeleteWebResult(string endPoint = "", string query = "") => DeleteWebResult<object>(endPoint: endPoint, query: query);

        public T DeleteWebResult<T>(string endPoint = "", string query = "")
        {
            var client = this.GetHttpClient(token: _token);

            var queryMark = string.IsNullOrEmpty(query) ? "" : "?";
            var fullUrl = $"{_urlBase}{endPoint}{queryMark}{query}";
            var response = client.DeleteAsync($"{fullUrl}").Result;
            WebResult<T> webResult = ResponseToWebResult<T>(response: response);
            return webResult.Data;
        }

        private WebResult<T> ResponseToWebResult<T>(HttpResponseMessage response)
        {
            WebResult<T> webResult = new WebResult<T>();

            if (response.IsSuccessStatusCode)
            {
                var objJson = response.Content.ReadAsStringAsync().Result;
                if (SerializerHelper.TryDeserializeWebResult(objJson, out webResult))
                {
                    if (webResult.Success)
                    {
                        return (webResult);
                    }
                    else
                    {
                        throw new Exception($"{webResult.Message}: {string.Join(", ", webResult.Errors)}");
                    }
                }
                else
                {
                    throw new Exception($"Ocurrió un error al deserializar ({typeof(WebResult<T>).Name}) desde ({response.RequestMessage.RequestUri.ToString()}).");
                }
            }
            else
            {
                string errorMensaje = "";
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        errorMensaje = $"No se cuenta con acceso al recurso en ({response.RequestMessage.RequestUri.ToString()}): {response.ReasonPhrase}";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        errorMensaje = $"El servidor rechazó atender la petición de acceso al recurso en ({response.RequestMessage.RequestUri.ToString()}): {response.ReasonPhrase}";
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        errorMensaje = $"No se encontró el recurso en ({response.RequestMessage.RequestUri.ToString()}): {response.ReasonPhrase}";
                        break;
                    default:
                        errorMensaje = $"Ocurrió un error al obtener respuesta desde ({response.RequestMessage.RequestUri.ToString()}): {response.ReasonPhrase}";
                        break;
                }
                throw new Exception(errorMensaje);
            }
        }

    }
}