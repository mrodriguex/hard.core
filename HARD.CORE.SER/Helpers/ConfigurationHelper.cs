namespace HARD.CORE.SER.Helpers
{
    public static class ConfigurationHelper
    {
        public static string BackendApiUrl => System.Configuration.ConfigurationManager.AppSettings["BackendApiUrl"];

    }
}
