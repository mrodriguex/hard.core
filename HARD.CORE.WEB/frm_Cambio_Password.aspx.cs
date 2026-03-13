using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Web;

public partial class frm_Cambio_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                txtAviso.InnerText = "Estimado(a) " +
                    ", por seguridad, le pedimos que cambie su contraseña.";
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        try
        {
            bool autenticado = false;
            Usuario usuario = (Usuario)Session["Usuario"];

            autenticado = UsuarioSER.GetInstance().AutenticarUsuario(usuario.ClaveUsuario, txtContrasenaActual.Text.Trim());

            if (autenticado == false)
            {
                raManager.Alert("La contraseña ingresada no coincide con la registrada en el sistema");
            }
            else
            {
                //string passwordUsuario = CryptographerSER.CreateHash(txtNuevaContrasena.Text.Trim());
                bool result = UsuarioSER.GetInstance().ActualizaContrasena(usuario.ClaveUsuario, txtNuevaContrasena.Text.Trim());
                if (result)
                {
                    raManager.Alert("La contraseña ha sido modificada exitosamente");
                    raManager.Redirect("Default.aspx");
                }
            }

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}