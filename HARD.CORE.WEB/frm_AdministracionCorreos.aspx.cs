using CRY.PCC.SER;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionCorreos : System.Web.UI.Page
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
        rgCorreos.Rebind();
    }

    protected void rgCorreos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgCorreos.DataSource = CorreosSER.GetInstance().ObtenerTodos();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
  
    protected void rgCorreos_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                //RadNumericTextBox txtAsunto = (RadNumericTextBox)filterItem["Asunto"].Controls[0];
                TextBox txtFiltroAsunto = filterItem["Asunto"].Controls[0] as TextBox;
                TextBox txtFiltroTitulo = filterItem["Titulo"].Controls[0] as TextBox;
                TextBox txtFiltroSubtitulo = filterItem["Subtitulo"].Controls[0] as TextBox;

                txtFiltroAsunto.MaxLength = 50;
                txtFiltroTitulo.MaxLength = 50;
                txtFiltroSubtitulo.MaxLength = 50;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }

    protected void rmCorreo_ItemClick(object sender, RadMenuEventArgs e)
    {
        try
        {

            //if (rmCorreo.SelectedItem.Text == "Eliminar")
            //{
            //    int radGridClickedRowIndex = 0;
            //    int ClaveCorreo = 0;

            //    radGridClickedRowIndex = Convert.ToInt32(Request.Form("radGridClickedRowIndex"));
            //    ClaveCorreo = Convert.ToInt32(this.rgCorreos.MasterTableView.DataKeyValues(radGridClickedRowIndex)("ClaveCorreo"));

            //    CorreoB.GetInstance().Eliminar_Correo(ClaveCorreo);
            //    raManager.Alert("Se ha eliminado exitosamente el correo con clave:" + ClaveCorreo);
            //    rgCorreos.Rebind();

            //}
            //else if (rmCorreo.SelectedItem.Text == "Editar")
            //{
            //    rgCorreos.Rebind();
            //}

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}