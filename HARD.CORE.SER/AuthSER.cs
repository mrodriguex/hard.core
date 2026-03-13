using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;

namespace HARD.CORE.SER
{
    public class AuthSER
    {

        #region "Singleton"
        private static AuthSER instance = null;
        private static readonly object mutex = new object();

        private AuthSER() { }

        public static AuthSER GetInstance()
        {
            if (instance == null)
            {
                lock (mutex)
                {
                    instance ??= new AuthSER();
                }
            }
            return instance;
        }
        #endregion

        public string Login(Login login)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            return httpClientManager.PostWebResult<string>(login, "api/v1/Auth/Login");
        }
    }
}
