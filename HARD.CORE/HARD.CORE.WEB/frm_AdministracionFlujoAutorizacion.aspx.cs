using CRY.PCC.SER;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionFlujoAutorizacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");

            if (!IsPostBack)
            {
                //Usuario usuario = Session["Usuario"] as Usuario;
                //Seguridad seguridad = SeguridadAccionSER.GetInstance().Obtener(usuario.Perfil.ClavePerfil, (int)EnumEntidad.BitacoraAcceso);

                //if (!seguridad.Consultar)
                //    Response.Redirect("NoAutorizado.aspx");

                //if (!seguridad.Exportar)
                //    btnExportToExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgFlujoAutorizacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgFlujoAutorizacion.DataSource = FlujoAutorizacionSER.GetInstance().FlujoAutorizacionTodos();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

   

    protected void rgFlujoAutorizacion_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                RadNumericTextBox ClavePuesto = (RadNumericTextBox)item["ClaveFlujoAutorizacion"].Controls[0];
                TextBox Descripcion = (TextBox)item["Descripcion"].Controls[0];
                TextBox Nombre = (TextBox)item["Nombre"].Controls[0];
                TextBox Correo = (TextBox)item["Correo"].Controls[0];

                ClavePuesto.MaxLength = 10;
                Descripcion.MaxLength = 50;
                Nombre.MaxLength = 50;
                Correo.MaxLength = 50;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

}