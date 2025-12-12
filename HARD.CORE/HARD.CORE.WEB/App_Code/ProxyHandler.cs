using System.Web;

public class ProxyHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        // URL de tu API backend (configurable en web.config)
        string backendUrl = System.Configuration.ConfigurationManager.AppSettings["BackendApiUrl"];
        ReverseProxy.ProcessRequest(context, backendUrl);
    }

    public bool IsReusable { get { return (false); } }
}