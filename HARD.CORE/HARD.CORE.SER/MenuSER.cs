using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.SER
{
    public class MenuSER
    {

        #region " Singleton "

        private static MenuSER instance = null;

        private static object mutex = new object();
        private MenuSER()
        {
        }

        public static MenuSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new MenuSER();
                }
            }

            return instance;

        }

        #endregion

        public List<Menu> ObtenerMenu_Usuario(string claveUsuario,int clavePerfil)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.GetWebResult<List<Menu>>(endPoint: "api/v1/Menu/ObtenerMenu_Usuario", $"claveUsuario={encodedClaveUsuario}&clavePerfil={clavePerfil}");
        }

        public List<Menu> ObtenerMenu_Perfil(int clavePerfil)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<List<Menu>>(endPoint: "api/v1/Menu/ObtenerMenu_Perfil", $"clavePerfil={clavePerfil}");
            foreach (var item in result)
            {
                if (item.ClaveMenuPadre == 0)
                {
                    item.ClaveMenuPadre = null; 
                }
            }

            return result;
        }

        public bool ConfigurarMenu_Perfil(int clavePerfil, List<Menu> menus)
        {
            Perfil perfil = new Perfil();
            perfil.Menus = menus;
            perfil.ClavePerfil = clavePerfil;

            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<bool>(perfil, "api/v1/Menu/ConfigurarMenu_Perfil");
        }
    }
}
