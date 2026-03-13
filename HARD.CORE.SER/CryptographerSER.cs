using HARD.CORE.SER.Helpers;
using System;

namespace HARD.CORE.SER
{
    public static class CryptographerSER
    {

        public static string UrlApi => System.Configuration.ConfigurationManager.AppSettings["UrlApi"];
        /// <summary>
        /// Compara el hash calculado del input con el hash almacenado utilizando el algoritmo indicado.
        /// </summary>
        /// <param name="plainText">Texto a hashear (por ejemplo, el password en texto plano)</param>
        /// <param name="storedHash">Hash almacenado para comparación (se asume en formato hexadecimal)</param>
        /// <returns>true si los hashes coinciden; false en caso contrario</returns>
        public static bool CompareHash(string plainText, string storedHash)
        {
            string encodedPlainText = Uri.EscapeDataString(plainText);
            string encodedStoredHash = Uri.EscapeDataString(storedHash);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<bool>(endPoint: "api/v1/Cryptographer/CompareHash", $"plainText={encodedPlainText}&storedHash={encodedStoredHash}");
        }

        public static string CreateHash(string plainText)
        {
            string encodedPlainText = Uri.EscapeDataString(plainText);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<string>(endPoint: "api/v1/Cryptographer/CreateHash", $"plainText={encodedPlainText}");
        }

    }
}
