using HARD.CORE.SER;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionBitacoraAcceso : System.Web.UI.Page
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

    protected void rgIngresos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgIngresos.DataSource = UsuarioSER.GetInstance().ObtenerActividad();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rgIngresos_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem item = (GridFilteringItem)e.Item;
                TextBox CClaveUsuario = (TextBox)item["CClaveUsuario"].Controls[0];
                TextBox txtDescripcion = (TextBox)item["CNombreUsuario"].Controls[0];

                CClaveUsuario.MaxLength = 50;
                txtDescripcion.MaxLength = 50;
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void ExportarAExcell_Click(object sender, EventArgs e)
    {
        foreach (GridColumn column in rgIngresos.MasterTableView.RenderColumns)
        {
            if (column.UniqueName == "CClaveUsuario" || column.UniqueName == "CNombreUsuario" || column.UniqueName == "CNumeroIngresos"
                || column.UniqueName == "UltimaConexion")
            {
                column.Visible = true;
            }
            else
            {
                column.Visible = false;
            }
        }

        rgIngresos.ExportSettings.OpenInNewWindow = true;
        rgIngresos.ExportSettings.IgnorePaging = true;
        rgIngresos.ExportSettings.ExportOnlyData = true;
        rgIngresos.ExportSettings.FileName = string.Concat("BitacoraIngresos", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

        rgIngresos.ExportSettings.Excel.WorksheetName = "BitacoraIngresos";
        rgIngresos.MasterTableView.ExportToExcel();
    }
}