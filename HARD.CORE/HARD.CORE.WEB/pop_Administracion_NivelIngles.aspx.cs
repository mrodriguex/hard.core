using HARD.CORE.SER;
using HARD.CORE.OBJ;
using System;
using System.Web;
using System.Web.UI;


public partial class pop_Administracion_NivelIngles : System.Web.UI.Page
{
    private int ClaveNivelIngles
    {
        get
        {
            if (Request.QueryString["ClaveNivelIngles"] != null)
                return Convert.ToInt32(Request.QueryString["ClaveNivelIngles"]);
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
                CargarNivelIngles();
            }
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        NivelIngles nivelIngles = new NivelIngles();

        nivelIngles.Descripcion = txtDescripcion.Text;
        nivelIngles.Estatus = cbEstatus.Checked.Value;
        if (Accion == 1)
        {
            
            int result = NivelInglesSER.GetInstance().Insertar(nivelIngles);

            if (result>0)
            {
                raManager.Alert("Se ha registrado correctamente el nivel de inglés con clave : " + result);
                       raManager.ResponseScripts.Add("salir();");
            }

        }
        else
        {

            nivelIngles.ClaveNivelIngles = ClaveNivelIngles;
            
            bool result = NivelInglesSER.GetInstance().Actualizar(nivelIngles);

            if (result)
            {
                raManager.Alert("Se ha actualizado correctamente el nivel de inglés");
                raManager.ResponseScripts.Add("salir();");
            }

        }

    }


    public void CargarNivelIngles()
    {
        NivelIngles NivelIngles = NivelInglesSER.GetInstance().Obtener(ClaveNivelIngles);
        
        txtDescripcion.Text = NivelIngles.Descripcion;
        cbEstatus.Checked = NivelIngles.Estatus;
    }

}