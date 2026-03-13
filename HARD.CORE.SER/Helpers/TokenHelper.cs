using Newtonsoft.Json.Linq;

using System;
using System.Text;
using System.Web;

namespace HARD.CORE.SER.Helpers
{
    public static class TokenHelper
    {

        public static string Token
        {
            get
            {
                string token = (HttpContext.Current?.Session["Token"] as string) ?? "";
                if (!string.IsNullOrEmpty(token) && IsTokenExpired(token))
                {
                    HttpContext.Current?.Session.Clear();
                    throw new Exception("El token ya no es válido, por favor autenticar nuevamente.");
                }
                return (token);
            }
        }

        public static bool IsTokenExpired(string token)
        {
            try
            {
                // Un JWT tiene 3 partes separadas por '.': Header, Payload y Signature.
                var parts = token.Split('.');
                if (parts.Length != 3) return true; // Token inválido

                // Decodificar la parte del Payload (segunda parte del token)
                var payloadJson = DecodeBase64Url(parts[1]);

                // Parsear con Newtonsoft.Json
                var payloadData = JObject.Parse(payloadJson);

                // Extraer la fecha de expiración "exp"
                if (!payloadData.ContainsKey("exp")) return true;

                long expTime = payloadData["exp"].Value<long>();
                var expDate = DateTimeOffset.FromUnixTimeSeconds(expTime).UtcDateTime;

                return expDate < DateTime.UtcNow; // Devuelve true si el token ha expirado
            }
            catch
            {
                return true; // Si hay un error, suponer que el token ha expirado
            }
        }

        private static string DecodeBase64Url(string base64Url)
        {
            string base64 = base64Url.Replace('-', '+').Replace('_', '/'); // Convertir Base64Url a Base64
            switch (base64.Length % 4) // Asegurar que la longitud sea múltiplo de 4
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }


    }
}
