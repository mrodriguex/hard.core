using System.Configuration;
using System.Web.UI.HtmlControls;

using Telerik.Web.UI;

/// <summary>
/// Dentro de esta clase se carga el objeto que controla el tiempo de sesión
/// </summary>
public class SessionTimeOut
{

    #region "Singleton"

    private static SessionTimeOut instance = null;

    private static object mutex = new object();
    private SessionTimeOut()
    {
    }

    public static SessionTimeOut GetInstance()
    {

        if (instance == null)
        {
            lock ((mutex))
            {
                instance = new SessionTimeOut();
            }
        }

        return instance;

    }

    #endregion    

    public void CargarSession(HtmlForm Form, int TimeOut, string claveUsuario)
    {

        RadNotification notificacion = new RadNotification();
        notificacion.ID = "Notificacion";
        notificacion.Width = 420;
        notificacion.Height = 220;
        notificacion.AutoCloseDelay = 60000;
        notificacion.ShowCloseButton = false;
        notificacion.EnableRoundedCorners = true;
        notificacion.Position = NotificationPosition.Center;
        notificacion.LoadContentOn = NotificationLoad.PageLoad;
        notificacion.OnClientHidden = "notification_Hidden";
        notificacion.OnClientShowing = "notification_Showing";
        notificacion.ShowInterval = (TimeOut - 1) * 60000;
        notificacion.Value = ConfigurationManager.AppSettings["DefaultRedirect"] + "?SessionExpired=" + Encryption.GetInstance().EncryptQS(claveUsuario);

        notificacion.Title = "Tu sesión esta a punto de expirar";
        notificacion.TitleIcon = "";
        notificacion.ContentTemplate = new SessionTemplate();
        notificacion.ContentContainer.Controls.Add(GetDivNotification());

        Form.Controls.Add(notificacion);

    }

    public HtmlGenericControl GetDivNotification()
    {

        RadButton btnContinuarSession = new RadButton();
        HtmlGenericControl div = new HtmlGenericControl("div");

        btnContinuarSession.ID = "continueSession";
        btnContinuarSession.Text = "Continuar sesión";
        btnContinuarSession.OnClientClicked = "ContinueSession";
        btnContinuarSession.AutoPostBack = false;
        btnContinuarSession.Style.Add("margin-top", "15px");
        btnContinuarSession.Style.Add("margin-left", "160px");
        btnContinuarSession.Width = 160;
        btnContinuarSession.CssClass = "botonEnviar";
        div.Attributes.Add("class", "clockSession");
        div.InnerHtml = "<div class='sessionExpire'>Tiempo restante</div>" + "<div class='timeRemain'>" + "     <span class='timeSeconds'><span id='timeLbl'>60</span></span>segundo (s) " + "</div>";
        div.Controls.Add(btnContinuarSession);

        return div;

    }

    protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
    {
    }

}
