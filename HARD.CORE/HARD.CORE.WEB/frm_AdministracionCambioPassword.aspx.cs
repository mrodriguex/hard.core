using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_AdministracionCambioPassword : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!IsPostBack)
            {
                //Usuario usuario = (Usuario)Session["Usuario"];
                //Seguridad seguridad = SeguridadAccionSER.GetInstance().Obtener(usuario.Perfil.ClavePerfil, (int)EnumEntidad.Contraseña);

                //if (!seguridad.Consultar)
                //    Response.Redirect("NoAutorizado.aspx");

                //if (!seguridad.Editar)
                //{
                //    HabilitarControles(false);
                //    btnCambiar.Visible = false;
                //}
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
                bool result =UsuarioSER.GetInstance().ActualizaContrasena(usuario.ClaveUsuario, txtConfirmaContrasena.Text.Trim());
                if (result)
                {
                    raManager.Alert("La contraseña se ha cambiado correctamente.");
                    raManager.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void HabilitarControles(bool estado)
    {
        txtContrasenaActual.Enabled = estado;
        txtNuevaContrasena.Enabled = estado;
        txtConfirmaContrasena.Enabled = estado;
    }
}