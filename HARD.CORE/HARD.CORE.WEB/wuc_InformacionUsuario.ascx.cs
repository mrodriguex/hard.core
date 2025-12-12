using HARD.CORE.OBJ;
using System;
using System.Web;

public partial class wuc_InformacionUsuario : System.Web.UI.UserControl, IToolTipInformacionUsuario
{
    public string ClaveUsuario { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Usuario usuario = (Usuario)Session["Usuario"];

        switch (usuario.esActive)
        {
            case true:
                btnCambioContrasenia.Visible = false;
                break;

            case false:
                break;
            default:
                lblNombreUsuario.Text = usuario.NombreCompleto;
                break;
        }

        lblCorreo.Text =  usuario.Correo;
        lblNumeroEmpleado.Text = "Clave de usuario: " + usuario.ClaveUsuario.ToString();
    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        HttpContext context = HttpContext.Current;
        Generales.GetInstance().LimpiaCache(ref context);
    }
}