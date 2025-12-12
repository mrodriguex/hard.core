using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRY.PCC.SER
{
    public class MotivoVacanteSER
    {
        #region "Singleton"

        private static MotivoVacanteSER instance = null;

        private static object mutex = new object();

        private MotivoVacanteSER()
        {
        }

        public static MotivoVacanteSER GetInstance()
        {
            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new MotivoVacanteSER();
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
        //    return httpClientManager.GetWebResult<Seguridad>(endPoint: "api/v1//SeguridadAccion/Obtener", query: $"clavePerfil={encodedClavePerfil}&claveEntidad={encodedClaveEntidad}");
        //}

        public List<MotivoVacante> ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<MotivoVacante>>(endPoint: "api/v1/MotivoVacante/ObtenerTodos");
        }

        public bool Actualizar(MotivoVacante listadoMotivoVacante)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PutWebResult<bool>(obj: listadoMotivoVacante, endPoint: "api/v1/MotivoVacante/Actualizar");
        }

    }
}

