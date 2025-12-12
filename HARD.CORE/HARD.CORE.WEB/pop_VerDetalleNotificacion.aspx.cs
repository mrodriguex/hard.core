using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Web;

public partial class pop_VerDetalleNotificacion : System.Web.UI.Page
{
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
    }

    private int ClaveNotificacion
    {
        get
        {
            if (Request.QueryString["ClaveNotificacion"] != null)
            {
                return Convert.ToInt32(Request.QueryString["ClaveNotificacion"]);
            }
            return 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        Generales.GetInstance().LimpiaCache(ref context);

        if (!IsPostBack)
        {
            CargarNotificacion();
            MarcarComoVisto();
        }
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        raManager.ResponseScripts.Add("Salir()");
    }

    private void CargarNotificacion()
    {
        try
        {
            Notificacion notificacion = NotificacionSER.GetInstance().Obtener(ClaveNotificacion);

            if (notificacion != null)
            {
                lblTitulo.Text = notificacion.Titulo + " - " + notificacion.Subtitulo;

                switch (notificacion.TipoContenido.ClaveTipoContenido)
                {
                    case 1:
                        lblDescripcion.Text = notificacion.Descripcion;
                        rbiImagenNotificacion.Visible = false;
                        break;  
                    case 2:
                        rbiImagenNotificacion.DataValue = notificacion.ImagenByte;
                        lblDescripcion.Visible = false;
                        break;
                }
            }
        }

        catch(Exception ex) 
        {
            raManager.Alert(ex.Message);        
        }
    }

    private void MarcarComoVisto()
    {
        try
        {
            Notificacion notificacion = NotificacionSER.GetInstance().Obtener(ClaveNotificacion);

            foreach (var item in notificacion.NotificacionDetalle)
            {
                if (item.ClaveUsuario == Usuario.ClaveUsuario)
                {
                    NotificacionSER.GetInstance().MarcarComoVisto(item);
                    break;
                }
            }
        }

        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}