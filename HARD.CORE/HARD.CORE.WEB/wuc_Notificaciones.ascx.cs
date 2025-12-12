using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Linq;
using System.Web;

public partial class wuc_Notificaciones : System.Web.UI.UserControl, IToolTipNotificacionUsuario
{
    public string ClaveUsuario { get; set; }

    private Usuario Usuario
    {
        get
        {
            if (Session["Usuario"] != null)
            {
                return Session["Usuario"] as Usuario;
            }

            return null;
        }
        set
        {
            Session["Usuario"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Usuario usuario = (Usuario)Session["Usuario"];
    }

    protected void rlvNotificaciones_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        CargarNotificaciones();
    }

    protected void rlvNotificaciones_DataBound(object sender, EventArgs e)
    {
        if (rlvNotificaciones.Items.Count == 0)
            alertSinNotificaciones.Visible = true;
    }

    protected void btnCloseSession_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        HttpContext context = HttpContext.Current;
        Generales.GetInstance().LimpiaCache(ref context);
    }

    protected void CargarNotificaciones()
    {
        try
        {
            rlvNotificaciones.DataSource = NotificacionSER.GetInstance().ObtenerPorUsuario(ClaveUsuario);
        }
        catch (Exception ex)
        {

        }
    }

    protected string GetVistoPorClaveUsuario(object dataItem)
    {
        var notificacion = dataItem as Notificacion;
        if (notificacion.NotificacionDetalle == null)
            return "background-color: white;";

        var detalle = notificacion.NotificacionDetalle.FirstOrDefault(d => d.ClaveUsuario == Usuario.ClaveUsuario);

        if (detalle == null)
            return "background-color: white;";

        return detalle.Visto ? "background-color: white;" : "background-color: #e6f7ff;";
    }
}