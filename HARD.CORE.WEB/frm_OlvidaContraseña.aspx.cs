using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;

public partial class frm_OlvidaContraseña : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string usuario = Request.QueryString["usuario"];
                bool PuedeCambiarContrasena = true;
                txtAviso.InnerHtml = "Favor de seleccionar un correo electrónico para recuperar la contraseña.<br /><br />" +
                "La opción <strong>Enviar</strong> es para confirmar.<br />" +
                "La opción <strong>Cancelar</strong> es para el caso en que no encuentres tu correo.<br />" +
                "En ese caso, por favor ponte en contacto con el administrador.";
                //PuedeCambiarContrasena = UsuarioSER.GetInstance().PuedeCambiarContrasena(usuario);

                //if (PuedeCambiarContrasena)
                //{
                  //  DataTable dtCorreos = UsuarioSER.GetInstance().ObtenerCorreosCambioContrasena(usuario);
                //    if (dtCorreos != null && dtCorreos.Rows.Count > 0)
                //    {
                //        rblClientes.Items.Clear(); // Limpiar antes

                //        foreach (DataRow row in dtCorreos.Rows)
                //        {
                //            string correo = row["correo"].ToString();
                //            string claveContacto = row["claveContacto"].ToString();
                //            rblClientes.Items.Add(new ButtonListItem(correo, claveContacto));
                //        }
                //    }
                //    else
                //    {
                //        raManager.Alert("No existen correos registrados para este usuario");
                //    }
                //}
                //else
                //{
                //    rblClientes.Visible = false;
                //    btnCambiar.Visible = false;
                //    btnCancelar.Visible = false;
                //    txtAviso.Visible = false;
                //    txtAvisoActive.Visible = true;
                //    divCambioPassword.Visible = true;
                //    txtAvisoActive.InnerHtml = "Favor de ingresar al enlace para usuarios de Active Directory.";
                //}
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        //var seleccionados = new List<string>();

        //foreach (ButtonListItem item in rblClientes.Items)
        //{
        //    if (item.Selected)
        //    {
        //        seleccionados.Add(item.Text);
        //    }
        //}
        //txtAviso.InnerText = "Seleccionaste: " + string.Join(", ", seleccionados);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            string script = @"
            Toastify({
                text: '¡Contactar al administrador del sistema.!',
                duration: 3000,
                gravity: 'center',
                position: 'center',
                style: {
                    background: '#FF0000',
                    color: 'white'
                }
            }).showToast();
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "toastMensaje", script, true);
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "setTimeout(function() { window.location.href = 'Default.aspx'; }, 2000);", true);
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
        //    string usuario = Request.QueryString["usuario"];
        //    string claveSeleccionada = string.Empty;
        //    foreach (ButtonListItem item in rblClientes.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            claveSeleccionada = item.Value;
        //            break;
        //        }
        //    }

        //    if (string.IsNullOrEmpty(claveSeleccionada))
        //    {
        //        raManager.Alert("Favor de seleccionar un correo electrónico");
        //        rblClientes.Focus();
        //        return;
        //    }
        //    else
        //    {
               //UsuarioSER.GetInstance().EnviarCambioContrasena(usuario, Convert.ToInt32(claveSeleccionada));
        //        raManager.Alert("Te enviamos una contraseña temporal al correo seleccionado válido por 24 horas");
        //        ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "setTimeout(function() { window.location.href = 'Default.aspx'; }, 3000);", true);
        //    }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}