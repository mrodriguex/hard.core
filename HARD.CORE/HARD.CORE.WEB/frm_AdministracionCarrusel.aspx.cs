using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionCarrusel : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!IsPostBack)
            {
                //    Seguridad seguridad = SeguridadAccionSER.GetInstance().Obtener(Usuario.Perfil.ClavePerfil, (int)EnumEntidad.Aviso);

                //    if (!seguridad.Consultar)
                //        Response.Redirect("NoAutorizado.aspx");

                //    if (!seguridad.Crear)
                //        btnNuevo.Visible = false;

            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgContactosCliente_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        rgAvisoCarrusel.DataSource = AvisoSER.GetInstance().ObtenerAvisosTodosCarrusel();
    }

    //public List<Aviso> ObtenerAvisosTodosCarrusel()
    //{
    //    return new List<Aviso>
    //    {
    //        new Aviso
    //        {
    //            ClaveAviso = 1,
    //            Titulo = "Mantenimiento programado",
    //            Descripcion = "El sistema estará en mantenimiento el 5 de noviembre de 2 a 4 AM.",
    //            RutaImagen = "App_Resources/Imagenes/Carrusel/mantenimiento.jpg",
    //            Visible = true
    //        },
    //        new Aviso
    //        {
    //            ClaveAviso = 2,
    //            Titulo = "Nueva actualización",
    //            Descripcion = "Versión 2.1 con mejoras de rendimiento y seguridad.",
    //            RutaImagen = "App_Resources/Imagenes/Carrusel/actualizacion.jpg",
    //            Visible = true
    //        },
    //        new Aviso
    //        {
    //            ClaveAviso = 3,
    //            Titulo = "Aviso de seguridad",
    //            Descripcion = "No compartas tus credenciales con nadie. Seguridad ante todo.",
    //            RutaImagen = "App_Resources/Imagenes/Carrusel/seguridad.jpg",
    //            Visible = false
    //        }
    //    };
    //}

    protected void rgAvisoCarrusel_ItemDataBound(object sender, GridItemEventArgs e)
    {
        GridDataItem dataItem = e.Item as GridDataItem;
        Generales.GetInstance().ConfigurarFiltros(rgAvisoCarrusel);

        //if (dataItem != null)
        //{
        //    Seguridad seguridad = SeguridadAccionSER.GetInstance().Obtener(Usuario.Perfil.ClavePerfil, (int)EnumEntidad.Aviso);
        //    RadButton btnEditar = dataItem.FindControl("btnEditar") as RadButton;
        //    RadButton btnBorrar = dataItem.FindControl("btnBorrar") as RadButton;

        //    if (btnEditar != null)
        //    {
        //        if (!seguridad.Editar)
        //            btnEditar.Visible = false;
        //    }

        //    if (btnBorrar != null)
        //    {
        //        if (!seguridad.Eliminar)
        //            btnBorrar.Visible = false;
        //    }
        //}
    }

    protected void rgAvisoCarrusel_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFilteringItem)
        {
            GridFilteringItem filterItem = (GridFilteringItem)e.Item;

            TextBox txtFiltroTitulo = filterItem["Titulo"].Controls[0] as TextBox;

            TextBox txtFiltroDescripccion = filterItem["Descripcion"].Controls[0] as TextBox;


            if (txtFiltroTitulo != null || txtFiltroDescripccion != null)
            {
                txtFiltroTitulo.MaxLength = 50;
                txtFiltroDescripccion.MaxLength = 50;
            }
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        bool resul = AvisoSER.GetInstance().PuedeInsertarCarrusel();

        if (resul)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EjecutarNuevo", "nuevo(null, { set_cancel: function() {} });", true);
        else
        {
            RadAjaxManager.GetCurrent(this).ResponseScripts.Add("alert('Tiene un limite de insersion de aviso carrusel.');");
            rgAvisoCarrusel.Rebind();
        }
    }

    protected void btnBorrar_Click(object sender, EventArgs e)
    {
        Usuario usuario = (Usuario)Session["Usuario"];
        //Aviso aviso = new Aviso();
        Telerik.Web.UI.RadButton btn = (Telerik.Web.UI.RadButton)sender;
        GridDataItem item = (GridDataItem)btn.NamingContainer;
        //aviso.ClaveUsuarioActualizacion = usuario.ClaveUsuario;
        string claveAviso = item.GetDataKeyValue("ClaveAviso").ToString();

        //aviso.ClaveAviso = Convert.ToInt32(claveAviso);

        bool Result = AvisoSER.GetInstance().EliminarAvisoCarrusel(Convert.ToInt32(claveAviso));

        if (Result)
        {
            RadAjaxManager.GetCurrent(this).ResponseScripts.Add("alert('Se ha eliminado el aviso correctamente');");
            rgAvisoCarrusel.Rebind();
        }
    }

    public string ObtenerRutaFoto(object foto)
    {
        if (foto == null || string.IsNullOrEmpty(foto.ToString()))
            return ResolveUrl("~/App_Resources/Imagenes/Sitio/User_Default.png");

        return ResolveUrl("~/Uploads/Fotos/" + foto.ToString());
    }
}