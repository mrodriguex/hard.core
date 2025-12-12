using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionPerfiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!IsPostBack)
            {
                //Usuario usuario = Session["Usuario"] as Usuario;
                ////Seguridad seguridad = SeguridadAccionSER.GetInstance().Obtener(usuario.Perfil.ClavePerfil, (int)EnumEntidad.Perfil);

                //if (!seguridad.Consultar)
                //    Response.Redirect("NoAutorizado.aspx");

                //if (!seguridad.Crear)
                //    btnNuevo.Visible = false;

                //if (!seguridad.Editar)
                //    rmPerfiles.Items[0].Enabled = false;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgPerfiles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgPerfiles.DataSource = PerfilSER.GetInstance().ObtenerPerfiles();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgPerfiles_DataBound(object sender, EventArgs e)
    {
        try
        {
            Generales.GetInstance().ConfigurarFiltros(rgPerfiles);
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgPerfiles_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item | e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                ImageButton vistaPreviaMenu = (ImageButton)item.FindControl("vistaPreviaMenu");
                string clavePerfil = item.GetDataKeyValue("ClavePerfil").ToString();
                rttPerfiles.TargetControls.Add(vistaPreviaMenu.ClientID, clavePerfil, true);
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgPerfiles_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                RadNumericTextBox txtNumeroEmpleado = (RadNumericTextBox)item["ClavePerfil"].Controls[0];
                TextBox txtDescripcion = (TextBox)item["Descripcion"].Controls[0];

                txtNumeroEmpleado.MaxLength = 10;
                txtNumeroEmpleado.NumberFormat.GroupSeparator = "";
                txtDescripcion.MaxLength = 50;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
    protected void rgPerfiles_PreRender(object sender, EventArgs e)
    {
        GridFilterMenu menu = rgPerfiles.FilterMenu;

        // Eliminar filtros no aplicables
        foreach (RadMenuItem mi in menu.Items.ToList())
        {
            if (mi.Value != "NoFilter" && mi.Value != "EqualTo")
            {
                menu.Items.Remove(mi);
            }
        }

        // Traducir los que quedan
        foreach (RadMenuItem mi in menu.Items)
        {
            if (mi.Value == "NoFilter") mi.Text = "Sin filtro";
            if (mi.Value == "EqualTo") mi.Text = "Igual a";
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        rttPerfiles.TargetControls.Clear();
        rgPerfiles.Rebind();
    }

    protected void rmPerfiles_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    {
        try
        {
            rttPerfiles.TargetControls.Clear();
            rgPerfiles.Rebind();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    private void UpdateToolTip(string elementID, UpdatePanel panel)
    {
        Control vistaPrevia = Page.LoadControl("wuc_VistaPrevia_Menu.ascx");
        IToolTip vista = (IToolTip)vistaPrevia;
        vista.Clave = int.Parse(elementID);
        panel.ContentTemplateContainer.Controls.Add(vistaPrevia);
    }

    protected void OnAjax(object sender, ToolTipUpdateEventArgs args)
    {
        this.UpdateToolTip(args.Value, args.UpdatePanel);
    }
}