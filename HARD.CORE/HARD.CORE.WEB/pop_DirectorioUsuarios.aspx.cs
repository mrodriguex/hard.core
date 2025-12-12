using HARD.CORE.SER;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class pop_DirectorioUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!Page.IsPostBack)
            {
                Session.Remove("dtUsuario");
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgUsuarios.DataSource = UsuarioSER.GetInstance().ObtenerUsuariosDirectorioActivoTodos();
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
                RadNumericTextBox txtNumeroEmpleado = (RadNumericTextBox)e.Item.Controls[2].Controls[0];
                TextBox txtClaveUsuario = (TextBox)e.Item.Controls[3].Controls[0];

                txtNumeroEmpleado.MaxLength = 8;
                txtNumeroEmpleado.NumberFormat.GroupSeparator = "";
                txtClaveUsuario.MaxLength = 25;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }
}