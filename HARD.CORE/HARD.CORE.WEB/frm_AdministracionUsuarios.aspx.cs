using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionUsuarios : System.Web.UI.Page
{
    public Usuario Usuario
    {
        get
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            return (usuario == null ? new Usuario() : usuario);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Generales.GetInstance().Acceso(Context, Usuario.Menu.Select(x => x.Ruta));
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        rgUsuarios.Rebind();
    }




    protected void rgUsuarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
         rgUsuarios.DataSource = UsuarioSER.GetInstance().ObtenerTodos();        
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarios_DataBound(object sender, EventArgs e)
    {
        try
        {
            Generales.GetInstance().ConfigurarFiltros(rgUsuarios);
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarios_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                TextBox txtClaveUsuario = (TextBox)item["ClaveUsuario"].Controls[0];
                RadNumericTextBox txtNumeroEmpleado = (RadNumericTextBox)item["NumeroEmpleado"].Controls[0];

                txtClaveUsuario.MaxLength = 10;
                txtNumeroEmpleado.MaxLength = 8;
                txtNumeroEmpleado.NumberFormat.GroupSeparator = "";
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rmUsuarios_ItemClick(object sender, RadMenuEventArgs e)
    {
        if (e.Item.Text == "Desbloquear usuario")
        {
            int rowIndex = int.Parse(Request.Form["rowIndex"]);
            string claveUsuario = (string)rgUsuarios.Items[rowIndex].GetDataKeyValue("ClaveUsuario");
            bool Result = UsuarioSER.GetInstance().Desbloquea(claveUsuario);
        }
        rgUsuarios.Rebind();
    }

    protected void rgUsuarios_PreRender(object sender, EventArgs e)
    {
        GridFilterMenu menu = rgUsuarios.FilterMenu;

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
}