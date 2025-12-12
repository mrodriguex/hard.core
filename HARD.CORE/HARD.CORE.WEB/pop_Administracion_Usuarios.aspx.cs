using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class pop_Administracion_Usuarios : System.Web.UI.Page
{
    private Usuario Usuario
    {
        get
        {
            if (Session["Usuario"] != null)
            {
                return Session["Usuario"] as Usuario;
            }

            return null;
        }
    }

    private Usuario UsuarioEdicion
    {
        get
        {
            if (Session["UsuarioEdicion"] == null)
            {
                Session["UsuarioEdicion"] = new Usuario();
            }

            string claveUsuario = Request.QueryString["ClaveUsuario"];

            if (!string.IsNullOrEmpty(claveUsuario))
            {
                Session["UsuarioEdicion"] = UsuarioSER.GetInstance().Obtener(claveUsuario);
            }
            else
            {
                Session["UsuarioEdicion"] = new Usuario();
            }

            return Session["UsuarioEdicion"] as Usuario;
        }
        set { Session["UsuarioEdicion"] = value; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!Page.IsPostBack)
            {

                Session.Remove("dtUsuario");
                CargarEmpresas();
                if (Request.QueryString["Accion"] == "2")
                {
                    CargaDatosUsuario();
                }
                CargarRoles();

            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    private void CargaDatosUsuario()
    {

        try
        {
            rblTipoUsuario.Enabled = false;
            btnDirectorioActivo.Enabled = false;
            rlbPerfilesAdd.DataSource = UsuarioEdicion.Perfiles;
            rlbPerfilesAdd.DataTextField = "Descripcion";
            rlbPerfilesAdd.DataValueField = "ClavePerfil";
            rlbPerfilesAdd.DataBind();
            txtClave.Text = UsuarioEdicion.ClaveUsuario;
            txtNombreUsuario.Text = UsuarioEdicion.Nombre;
            txtNoEmpleado.Text = UsuarioEdicion.NumeroEmpleado.ToString();
            txtApellidoPaterno.Text = UsuarioEdicion.ApellidoPaterno;
            txtApellidoMaterno.Text = UsuarioEdicion.ApellidoMaterno;
            txtCorreo.Text = UsuarioEdicion.Correo;
            btnEstatus.Checked = UsuarioEdicion.Estatus;
            btnCambioPassword.Checked = UsuarioEdicion.CambioContrasena;

            List<string> claveEmpresasStr = UsuarioEdicion.Empresas.Select(y => y.ClaveEmpresa.ToString()).ToList();
            foreach (var item in rcbEmpresa.Items.Where(x => claveEmpresasStr.Contains(x.Value)))
            {
                if (item != null)
                {
                    item.Checked = true;
                }
            };

            if (!UsuarioEdicion.esActive)
            {
                rblTipoUsuario.SelectedIndex = 1;
                txtNombreUsuario.ReadOnly = false;
                txtApellidoPaterno.ReadOnly = false;
                txtApellidoMaterno.ReadOnly = false;
                txtCorreo.ReadOnly = false;
                btnCambioPassword.Enabled = true;
                btnDirectorioActivo.Visible = false;
            }

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }

    protected void CargarRoles()
    {

        try
        {
            rlbPerfiles.DataSource = PerfilSER.GetInstance().ObtenerPerfiles().Where(x => !UsuarioEdicion.Perfiles.Select(y => y.ClavePerfil).Contains(x.ClavePerfil));
            rlbPerfiles.DataTextField = "Descripcion";
            rlbPerfiles.DataValueField = "ClavePerfil";
            rlbPerfiles.DataBind();

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }

    protected DataView CargaListBox(int agregado)
    {

        try
        {
            //string claveUsuario = Request.QueryString["ClaveUsuario"];
            //DataTable dtUsuarioPerfil = UsuarioB.GetInstance().ObtenerPerfilesUsuario(claveUsuario);
            //DataView dvPerfiles = new DataView(dtUsuarioPerfil, "Agregado = " + agregado, "", DataViewRowState.CurrentRows);
            //return dvPerfiles;

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

        return null;

    }

    protected void CargarEmpresas()
    {
        try
        {
            List<Empresa> empresasUsuario = EmpresaSER.GetInstance().ObtenerTodos();
            rcbEmpresa.DataTextField = "Descripcion";
            rcbEmpresa.DataValueField = "ClaveEmpresa";
            rcbEmpresa.DataSource = empresasUsuario;
            rcbEmpresa.DataBind();

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rblTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            bool esLDAP = false;

            txtClave.Text = "";
            txtNombreUsuario.Text = "";
            txtNoEmpleado.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtCorreo.Text = "";
            rcbEmpresa.ClearSelection();

            bool esActiveDirectory = Convert.ToBoolean(rblTipoUsuario.SelectedValue);

            if (!esActiveDirectory)
            {
                esLDAP = false;
                txtNoEmpleado.Text = "999999";
                txtNombreUsuario.Focus();
                btnDirectorioActivo.Visible = false;
                btnCambioPassword.Checked = true;
                btnCambioPassword.Enabled = true;
                txtNombreUsuario.ReadOnly = esLDAP;
                txtApellidoPaterno.ReadOnly = esLDAP;
                txtApellidoMaterno.ReadOnly = esLDAP;
                txtCorreo.ReadOnly = esLDAP;
                //rcbEmpresa.Enabled = !esLDAP;

            }
            else
            {

                btnDirectorioActivo.Visible = true;
                btnCambioPassword.Checked = false;
                btnCambioPassword.Enabled = false;
                txtNombreUsuario.ReadOnly = true;
                txtApellidoPaterno.ReadOnly = true;
                txtApellidoMaterno.ReadOnly = true;
                txtCorreo.ReadOnly = true;
                //rcbEmpresa.Enabled = false;

            }

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }

    protected void btnDirectorioActivo_Click(object sender, EventArgs e)
    {

        try
        {

            if (Session["dtUsuario"] != null)
            {

                ArrayList dtUsuario = (ArrayList)Session["dtUsuario"];
                txtClave.Text = dtUsuario[0].ToString();
                txtNombreUsuario.Text = dtUsuario[1].ToString();
                txtNoEmpleado.Text = dtUsuario[4].ToString();

                string usuario = dtUsuario[2].ToString();
                int i = usuario.IndexOf(" ");
                if (i > 0)
                {
                    txtApellidoPaterno.Text = usuario.Substring(0, i);
                    txtApellidoMaterno.Text = usuario.Substring(i + 1, usuario.Length - i - 1);

                }
                else
                {
                    txtApellidoPaterno.Text = usuario;
                }

                txtCorreo.Text = dtUsuario[3].ToString();
                //rcbEmpresa.SelectedValue = dtUsuario[5].ToString();
                Session.Remove("dtUsuario");
                btnCambioPassword.Checked = false;

            }

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        try
        {
            List<Perfil> perfiles = new List<Perfil>();

            rlbPerfilesAdd.Items.ToList().ForEach(dataitem => perfiles.Add(new Perfil()
            {
                ClavePerfil = int.Parse(dataitem.Value)
            }));
           

            List<string> selectedEmpresas = rcbEmpresa.CheckedItems.Select(x => x.Value).ToList();
            List<Empresa> empresas = selectedEmpresas.Select(x => new Empresa() { ClaveEmpresa = Convert.ToInt32(x) }).ToList();

            Usuario usuario = new Usuario
            {
                ClaveUsuario = txtClave.Text,
                Nombre = txtNombreUsuario.Text.Trim(),
                ApellidoPaterno = txtApellidoPaterno.Text.Trim(),
                ApellidoMaterno = txtApellidoMaterno.Text.Trim(),
                NumeroEmpleado = (int)txtNoEmpleado.Value,
                Estatus = Convert.ToBoolean(btnEstatus.SelectedToggleState.Value),
                CambioContrasena = Convert.ToBoolean(btnCambioPassword.SelectedToggleState.Value),
                Correo = txtCorreo.Text.Trim(),
                esActive = Convert.ToBoolean(rblTipoUsuario.SelectedValue),
                Perfiles = perfiles,
                Empresas = empresas
            };

            switch (Request.QueryString["Accion"])
            {
                case "1":

                    if (!Convert.ToBoolean(rblTipoUsuario.SelectedValue))
                    {

                        usuario.esActive = false;
                        usuario.NumeroEmpleado = 999999;
                        usuario.ClaveUsuario = UsuarioSER.GetInstance().ObtenerUsuarioSugerido(usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Nombre);
                        txtClave.Text = usuario.ClaveUsuario;
                    }

                    UsuarioSER.GetInstance().Insertar(usuario);
                    raManager.Alert("El usuario se ha registrado exitosamente con clave: " + txtClave.Text.Trim());
                    break;

                case "2":

                    UsuarioSER.GetInstance().Actualizar(usuario);
                    raManager.Alert("El usuario " + usuario.ClaveUsuario + " se ha actualizado exitosamente");
                    break;

            }
            raManager.ResponseScripts.Add("salir();");
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }  
}