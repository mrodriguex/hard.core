using HARD.CORE.SER;
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class pop_DirectorioActivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarioLDAP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgUsuarioLDAP.DataSource = UsuarioSER.GetInstance().ObtenerUsuariosDirectorioActivo();
        }
        catch (Exception ex)
        { 
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarioLDAP_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                TextBox txtClave = (TextBox)item["ClaveUsuario"].Controls[0];

                txtClave.MaxLength = 8;
            }

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarioLDAP_DataBound(object sender, EventArgs e)
    {
        try
        {
            Generales.GetInstance().ConfigurarFiltros(rgUsuarioLDAP);
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarioLDAP_ItemCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Seleccionar")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ArrayList aryUsuario = new ArrayList();
                aryUsuario.Add(item.GetDataKeyValue("ClaveUsuario"));
                aryUsuario.Add(item.GetDataKeyValue("Nombre"));
                aryUsuario.Add(item.GetDataKeyValue("Apellidos"));
                aryUsuario.Add(item.GetDataKeyValue("Correo"));
                aryUsuario.Add(item.GetDataKeyValue("NumeroEmpleado"));







                Session["dtUsuario"] = aryUsuario;
                raManager.ResponseScripts.Add("salir()");
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}