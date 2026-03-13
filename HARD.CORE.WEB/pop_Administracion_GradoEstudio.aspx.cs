using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class pop_Administracion_GradoEstudio : System.Web.UI.Page
{
    private int ClaveNivelMinimoEstudios
    {
        get
        {
            if (Request.QueryString["ClaveNivelMinimoEstudios"] != null)
                return Convert.ToInt32(Request.QueryString["ClaveNivelMinimoEstudios"]);
            return 0;
        }
    }

    private int Accion
    {
        get
        {
            if (Request.QueryString["Accion"] != null)
                return Convert.ToInt32(Request.QueryString["Accion"]);
            return 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        Generales.GetInstance().LimpiaCache(ref context);

        if (!Page.IsPostBack)
        {
            if (Accion == 1)
            {
                btnGuardar.Text = "Guardar";
            }
            else
            {
                btnGuardar.Text = "Actualizar";
                CargarNivelMinimoEstudios();
            }
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        NivelMinimoEstudios nivelMinimoEstudios = new NivelMinimoEstudios();

        nivelMinimoEstudios.Descripcion = txtDescripcion.Text;
        nivelMinimoEstudios.Estatus = cbEstatus.Checked.Value;
        if (Accion == 1)
        {

            int result = NivelMinimoEstudiosSER.GetInstance().Insertar(nivelMinimoEstudios);

            if (result > 0)
            {
                raManager.Alert("Se ha registrado correctamente el nivel de estudio con clave : " + result);
                raManager.ResponseScripts.Add("salir();");
            }

        }
        else
        {
            nivelMinimoEstudios.ClaveNivelMinimoEstudios = ClaveNivelMinimoEstudios;

            bool result = NivelMinimoEstudiosSER.GetInstance().Actualizar(nivelMinimoEstudios);

            if (result)
            {
                raManager.Alert("Se ha actualizado correctamente el estudio de inglés");
                raManager.ResponseScripts.Add("salir();");
            }
        }
    }


    public void CargarNivelMinimoEstudios()
    {
        NivelMinimoEstudios NivelMinimoEstudios = NivelMinimoEstudiosSER.GetInstance().Obtener(ClaveNivelMinimoEstudios);
        txtDescripcion.Text = NivelMinimoEstudios.Descripcion;
        cbEstatus.Checked = NivelMinimoEstudios.Estatus;
    }
}