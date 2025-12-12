using HARD.CORE.SER;
using HARD.CORE.OBJ;
using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionHerenciaPerfil : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("UsuariosHerencia");
            Usuario usuario = (Usuario)Session["Usuario"];
        }

    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        rgHerenciaPerfil.Rebind();
    }

    protected void btnRecargar_Click(object sender, EventArgs e)
    {
        rgHerenciaPerfil.Rebind();
    }

    protected void rgHerenciaPerfil_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

        try
        {
            rgHerenciaPerfil.DataSource = HerenciaPerfilSER.GetInstance().ObtenerTodos();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }
 
    protected void rmHerencia_ItemClick(object sender, RadMenuEventArgs e)
    {

        try
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            HerenciaPerfil herenciaPerfil = new HerenciaPerfil();
            if (e.Item.Index == 0 || e.Item.Index == 1)
            {
                int rowIndex = int.Parse(Request.Form["rowIndex"]);
                int claveHerenciaPerfil = (int)rgHerenciaPerfil.Items[rowIndex].GetDataKeyValue("ClaveHerenciaPerfil");
                int claveEstatus = (int)rgHerenciaPerfil.Items[rowIndex].GetDataKeyValue("ClaveEstatus");
                string claveUsuarioHereda = rgHerenciaPerfil.Items[rowIndex].GetDataKeyValue("ClaveUsuarioHeredado").ToString();
                DateTime fechaInicial = (DateTime)rgHerenciaPerfil.Items[rowIndex].GetDataKeyValue("FechaInicial");
                DateTime fechaFinal = (DateTime)rgHerenciaPerfil.Items[rowIndex].GetDataKeyValue("FechaFinal");


                herenciaPerfil.ClaveUsuario = usuario.ClaveUsuario;
                herenciaPerfil.ClaveUsuarioHeredado = claveUsuarioHereda;
                herenciaPerfil.FechaInicial = fechaFinal;
                herenciaPerfil.FechaFinal = fechaFinal;
             


                switch (e.Item.Text)
                {
                    case "Desactivar herencia":
                        //claveEstatus = (int)ClaveEstatus.Finalizada;

                        herenciaPerfil.Estatus = false;
                        break;

                    case "Cancelar herencia":
                        //claveEstatus = (int)ClaveEstatus.Cancelado;
                        herenciaPerfil.Estatus = false;

                        break;

                }
                bool result= HerenciaPerfilSER.GetInstance().Actualizar(herenciaPerfil);
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

        rgHerenciaPerfil.Rebind();

    }


    protected void rgHerenciaPerfil_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                RadNumericTextBox txtClaveHerenciaPerfil = (RadNumericTextBox)item["ClaveHerenciaPerfil"].Controls[0];
                TextBox txtUsuarioHereda = (TextBox)item["UsuarioHereda"].Controls[0];
                //RadDatePicker dpFechaInicial = (RadDatePicker)item["FechaInicial"].Controls[0];
                //RadDatePicker dpFechaFinal = (RadDatePicker)item["FechaFinal"].Controls[0];

                txtClaveHerenciaPerfil.MaxLength = 10;
                txtClaveHerenciaPerfil.NumberFormat.GroupSeparator = "";
                txtUsuarioHereda.MaxLength = 50;

            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
    
}