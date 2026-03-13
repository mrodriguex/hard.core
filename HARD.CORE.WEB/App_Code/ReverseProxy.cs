using System;
using System.IO;
using System.Net;
using System.Web;

public static class ReverseProxy
{
    static ReverseProxy()
    {
        // Forzar TLS 1.2 (o 1.3) globalmente
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

        // Configuraciones adicionales de seguridad
        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true; // Solo para desarrollo
    }

    public static void ProcessRequest(HttpContext context, string backendApiUrl)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(backendApiUrl + context.Request.RawUrl);
            request.Method = context.Request.HttpMethod;

            // 1. Configurar headers críticos
            request.ContentType = context.Request.ContentType; // Conservar el Content-Type original

            // Forzar TLS en cada petición (redundante pero seguro)
            request.ProtocolVersion = HttpVersion.Version11;
            request.ServicePoint.Expect100Continue = false;


            // 3. Configuración adicional
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 30000;

            // Copiar headers (opcional, excluye algunos headers sensibles)
            foreach (string header in context.Request.Headers)
            {
                if (!WebHeaderCollection.IsRestricted(header))
                {
                    request.Headers[header] = context.Request.Headers[header];
                }
            }

            // Manejar POST/PUT
            if (context.Request.HttpMethod == "POST" || context.Request.HttpMethod == "PUT")
            {
                using (var requestStream = request.GetRequestStream())
                {
                    context.Request.InputStream.CopyTo(requestStream);
                }
            }

            // Enviar la petición al backend
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                context.Response.StatusCode = (int)response.StatusCode;

                // Copiar headers de respuesta
                foreach (string header in response.Headers)
                {
                    if (!WebHeaderCollection.IsRestricted(header))
                    {
                        context.Response.Headers[header] = response.Headers[header];
                    }
                }

                // Copiar el cuerpo de la respuesta
                using (var responseStream = response.GetResponseStream())
                {
                    responseStream.CopyTo(context.Response.OutputStream);
                }
            }
        }
        catch (WebException ex)
        {
            var errorResponse = ex.Response as HttpWebResponse;
            if (errorResponse != null)
            {
                context.Response.StatusCode = (int)errorResponse.StatusCode;
                using (var errorStream = errorResponse.GetResponseStream())
                {
                    errorStream.CopyTo(context.Response.OutputStream);
                }
            }
        }
    }
}