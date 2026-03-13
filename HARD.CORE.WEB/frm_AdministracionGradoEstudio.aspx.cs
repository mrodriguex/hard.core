using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionGradoEstudio : System.Web.UI.Page
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
    protected void rgNivelEstudio_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        rgNivelEstudio.DataSource = NivelMinimoEstudiosSER.GetInstance().ObtenerTodos();

    }
    protected void rgNivelEstudio_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFilteringItem)
        {

            GridFilteringItem filterItem = (GridFilteringItem)e.Item;

            RadNumericTextBox txtClaveNivelMinimoEstudios = (RadNumericTextBox)filterItem["ClaveNivelMinimoEstudios"].Controls[0];

            TextBox txtFiltroDescripccion = filterItem["Descripcion"].Controls[0] as TextBox;


            if (txtClaveNivelMinimoEstudios != null || txtFiltroDescripccion != null)
            {
                txtClaveNivelMinimoEstudios.MaxLength = 5;
                txtFiltroDescripccion.MaxLength = 50;
            }
        }
    }

    private List<NivelMinimoEstudios> GenerarNivelesInglesDummy()
    {
        var datos = new List<NivelMinimoEstudios>();

        // Datos realistas para niveles de inglés
        var niveles = new[]
        {
        new { Clave = 1, Descripcion = "Principiante A1", Estatus = true },
        new { Clave = 2, Descripcion = "Elemental A2", Estatus = true },
        new { Clave = 3, Descripcion = "Intermedio B1", Estatus = true },
        new { Clave = 4, Descripcion = "Intermedio Alto B2", Estatus = true },
        new { Clave = 5, Descripcion = "Avanzado C1", Estatus = true },
        new { Clave = 6, Descripcion = "Maestría C2", Estatus = true },
        new { Clave = 7, Descripcion = "Básico", Estatus = false },
        new { Clave = 8, Descripcion = "Pre-Intermedio", Estatus = true },
        new { Clave = 9, Descripcion = "Conversacional", Estatus = true },
        new { Clave = 10, Descripcion = "Business English", Estatus = false },
        new { Clave = 11, Descripcion = "Inglés Técnico", Estatus = true },
        new { Clave = 12, Descripcion = "Inglés Académico", Estatus = true },
        new { Clave = 13, Descripcion = "Inglés para Viajes", Estatus = false },
        new { Clave = 14, Descripcion = "Inglés para Niños", Estatus = true },
        new { Clave = 15, Descripcion = "Preparación TOEFL", Estatus = true }
    };


        foreach (var nivel in niveles)
        {
            datos.Add(new NivelMinimoEstudios
            {
                ClaveNivelMinimoEstudios = nivel.Clave,
                Descripcion = nivel.Descripcion,
                Estatus = nivel.Estatus,
            });
        }

        return datos;
    }

    protected void btnRecargar_Click(object sender, EventArgs e)
    {
        rgNivelEstudio.Rebind();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        //rttPerfiles.TargetControls.Clear();
        rgNivelEstudio.Rebind();
    }

    protected void btnBorrar_Click(object sender, EventArgs e)
    {

    }
}