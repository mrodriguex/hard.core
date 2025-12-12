using HARD.CORE.SER;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionNivelIngles : System.Web.UI.Page
{
    private Usuario Usuario
    {
        get
        {
            if ((Usuario)Session["Usuario"] != null)
            {
                return (Usuario)Session["Usuario"];
            }

            return null;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void rgNivelIngles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        rgNivelIngles.DataSource = NivelInglesSER.GetInstance().ObtenerTodos();
    }

    protected void rgNivelIngles_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFilteringItem)
        {

            GridFilteringItem filterItem = (GridFilteringItem)e.Item;

            RadNumericTextBox txtClaveNivelIngles = (RadNumericTextBox)filterItem["ClaveNivelIngles"].Controls[0];

            TextBox txtFiltroDescripccion = filterItem["Descripcion"].Controls[0] as TextBox;


            if (txtClaveNivelIngles != null || txtFiltroDescripccion != null)
            {
                txtClaveNivelIngles.MaxLength = 5;
                txtFiltroDescripccion.MaxLength = 50;
            }
        }
    }

    protected void btnRecargar_Click(object sender, EventArgs e)
    {
        rgNivelIngles.Rebind();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        rgNivelIngles.Rebind();
    }

    protected void btnBorrar_Click(object sender, EventArgs e)
    {

    }
}