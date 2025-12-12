using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private string nombreMenuActivo;

    private string NombreMenu
    {
        get
        {
            string nombreMenu = "";
            if (Request.QueryString["nombre"] != null)
            {
                nombreMenu = Request.QueryString["nombre"].ToString();
            }
            return nombreMenu;
        }
    }

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
        if (!IsPostBack)
        {
         
           string FechaHoy = DateTime.Now.ToString("dddd, dd MMMM yyyy");
         
                switch (Usuario.EmpresaActivo.ClaveEmpresa)
            {
                case 1: 
                    // Empresa Cryo
                    imgLogoCryo.Visible = true;
                    imgLogoEnergia.Visible = false;
                    imgLogoCryoinfraNorte.Visible = false;
                    imgLogoNA.Visible = false;
                    imgLogoTaxis.Visible = false;
                    break;
                   
                case 3:
                    // Empresa taxis
                    imgLogoCryo.Visible = false;
                    imgLogoEnergia.Visible = false;
                    imgLogoCryoinfraNorte.Visible = false;
                    imgLogoNA.Visible = false;
                    imgLogoTaxis.Visible = true;
                    break;
                case 7:
                    //NA trasnport
                    imgLogoCryo.Visible = false;
                    imgLogoEnergia.Visible = false;
                    imgLogoCryoinfraNorte.Visible = false;
                    imgLogoNA.Visible = true;
                    imgLogoTaxis.Visible = false;
                    break;
                case 8:
                    //Cryoinfra norte
                    imgLogoCryo.Visible = false;
                    imgLogoEnergia.Visible = false;
                    imgLogoCryoinfraNorte.Visible = true;
                    imgLogoNA.Visible = false;
                    imgLogoTaxis.Visible = false;
                    break;
                case 9:
                    //Energia infra
                    imgLogoCryo.Visible = false;
                    imgLogoEnergia.Visible = true;
                    imgLogoCryoinfraNorte.Visible = false;
                    imgLogoNA.Visible = false;
                    imgLogoTaxis.Visible = false;
                    break;

                default:
                    imgLogoCryo.Visible = true;
                    imgLogoEnergia.Visible = false;
                    imgLogoCryoinfraNorte.Visible = false;
                    imgLogoNA.Visible = false;
                    imgLogoTaxis.Visible = false;
                    break;
            }


        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context, true);

            //IndicarSiHayNotificacionesPendientes();
            ConfiguracionInicial();

            if (Usuario != null)
            {
                //lblTituloMaster.InnerText = nombreMenuActivo.ToUpper();
                Usuario usuario = (Usuario)Session["Usuario"];
                string claveUsuario = usuario.ClaveUsuario;
                int clavePerfil = usuario.PerfilActivo.ClavePerfil;

                List<Menu> menuUsuario = null;

                if (Session["Menu"] == null)
                    menuUsuario = MenuSER.GetInstance().ObtenerMenu_Usuario(claveUsuario, clavePerfil);
                else
                    menuUsuario = (List<Menu>)Session["Menu"];

                menu.DataTextField = "Nombre";
                menu.DataNavigateUrlField = "Ruta";
                menu.DataFieldID = "ClaveMenu";
                menu.DataFieldParentID = "ClaveMenuPadre";
                menu.DataSource = menuUsuario;
                menu.DataBind();

                //string urlActual = Request.Url.AbsolutePath.ToLower();

                //var menuActual = menuUsuario.FirstOrDefault(m => !string.IsNullOrEmpty(m.Ruta) && urlActual.EndsWith(m.Ruta.ToLower()));

                //if (menuActual != null)
                //{
                //    nombreMenuActivo = menuActual.Nombre;
                //    //lblTituloMaster.InnerText = nombreMenuActivo.ToUpper();
                //}
                //else
                    //lblTituloMaster.InnerText = "SIN TÍTULO";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + ex.Message + "')", true);
        }
    }

    protected void rttmNotificacion_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
    {
        UpdateToolTipNotificacion(e.Value, e.UpdatePanel);
    }

    protected void rttmInformacionUsuario_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
    {
        UpdateToolTipInformacionUsuario(e.Value, e.UpdatePanel);
    }

    private void UpdateToolTipNotificacion(string claveUsuario, UpdatePanel panel)
    {
        Control notifiacionUsuario = Page.LoadControl("wuc_Notificaciones.ascx");
        notifiacionUsuario.ID = "Notificaciones1";
        IToolTipNotificacionUsuario view = (IToolTipNotificacionUsuario)notifiacionUsuario;
        view.ClaveUsuario = claveUsuario;
        panel.ContentTemplateContainer.Controls.Add(notifiacionUsuario);
    }

    private void UpdateToolTipInformacionUsuario(string claveUsuario, UpdatePanel panel)
    {
        Control informacionUsuario = Page.LoadControl("wuc_InformacionUsuario.ascx");
        informacionUsuario.ID = "InformacionUsuario1";
        IToolTipInformacionUsuario view = (IToolTipInformacionUsuario)informacionUsuario;
        view.ClaveUsuario = claveUsuario;
        panel.ContentTemplateContainer.Controls.Add(informacionUsuario);
    }

    protected void menu_ItemDataBound(object sender, RadMenuEventArgs e)
    {
        Menu itemData = (Menu)e.Item.DataItem;
        if (!string.IsNullOrEmpty(itemData.Imagen))
        {
            e.Item.Text = string.Format("<i class='{0}' style='margin-right: 23px;margin-left: -13px; font-size: 24px; position: absolute; left: 16px;'></i>{1}", itemData.Imagen, itemData.Nombre);
            e.Item.Attributes["style"] = "white-space: nowrap;";
        }
    }

    private void ConfiguracionInicial()
    {
        nombreMenuActivo = NombreMenu;
        //rttmNotificacion.TargetControls.Add(imgNotificaciones.ClientID, Usuario.ClaveUsuario, true);
        rttmInformacionUsuario.TargetControls.Add(imgInformacionUsuario.ClientID, Usuario.ClaveUsuario, true);
    }

    //private void IndicarSiHayNotificacionesPendientes()
    //{
    //    try
    //    {
    //        bool hayNotificacionesPendientes = NotificacionSER.GetInstance().ExistenPendientesPorLeer(Usuario.ClaveUsuario);
    //        circulo.Visible = hayNotificacionesPendientes;
    //        if (hayNotificacionesPendientes)
    //            imgNotificaciones.Attributes.Add("class", "fa-solid fa-bell fa-2x fa-shake");
    //        else
    //            imgNotificaciones.Attributes.Add("class", "fa-solid fa-bell fa-2x");
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('No se pudo validar si existen notificaciones pendientes por ver.')", true);
    //    }
    //}
}
