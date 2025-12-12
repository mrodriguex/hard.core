using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    public static string BackendApiUrl
    {
        get { return ConfigurationManager.AppSettings["BackendApiUrl"]; }
        set { ConfigurationManager.AppSettings["BackendApiUrl"] = value; }
    }
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
        set
        {
            Session["Usuario"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                txtUsuario.Focus();
                Session.RemoveAll();
                Session.Abandon();
                rcbPerfil.Items.Clear();
                rcbPerfil.ClearSelection();
                rcbPerfil.Text = string.Empty;
                rcbEmpresa.Items.Clear();
                rcbEmpresa.ClearSelection();
                rcbEmpresa.Text = string.Empty;
                rcbEmpresa.Visible = false;
                rcbPerfil.Visible = false;
                btnEntar.Visible = false;

                if (Request.QueryString["SessionExpired"] != null)
                {
                    string claveUsuario = Encryption.GetInstance().DecryptQS(Request.QueryString["SessionExpired"]);
                    UsuarioSER.GetInstance().RegistrarActividad(claveUsuario, 2);
                }

                if (Request.QueryString["backendApiUrl"] != null)
                {
                    // Open the Web.config of the current application
                    var config = WebConfigurationManager.OpenWebConfiguration("~");
                    var appSettings = config.AppSettings;

                    // Set the new value
                    appSettings.Settings["BackendApiUrl"].Value = Request.QueryString["backendApiUrl"];

                    // Save the changes
                    config.Save(ConfigurationSaveMode.Modified);

                    // Force a refresh of the AppSettings section in memory
                    ConfigurationManager.RefreshSection("appSettings");
                }

            }
        }
        catch (Exception ex)
        {
            string alerta = "No se pudo iniciar sesión. Inténtelo nuevamente.";
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        txtIncorrecto.InnerText = "";
        try
        {
            string claveUsuario = Request.Form["txtUsuario"].Trim();
            string password = Request.Form["txtPassword"].ToString().Trim();
            Login login = new Login() { Username = claveUsuario, Password = password };
            var token = AuthSER.GetInstance().Login(login: login);
            HttpContext.Current.Session["Token"] = token;
            Usuario usuario = UsuarioSER.GetInstance().Obtener(claveUsuario);

            if (usuario != null)
            {
                if (usuario.Bloqueado)
                {
                    txtIncorrecto.InnerText = "Usuario bloqueado, favor de recuperar contraseña";

                    btnLogin.Visible = true;
                    btnEntar.Visible = false;
                    txtUsuario.Visible = true;
                    txtPassword.Visible = true;
                    rcbEmpresa.Visible = false;
                    rcbPerfil.Visible = false;                  
                }
                else
                {
                    if (usuario.Estatus)
                    {
                        Usuario = usuario;

                        if (!usuario.esActive && usuario.CambioContrasena)
                        {
                            Response.Redirect("frm_Cambio_Password.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                        else
                        {
                            btnLogin.Visible = false;
                            btnEntar.Visible = true;
                            txtUsuario.Visible = false;
                            txtPassword.Visible = false;                         
                            rcbEmpresa.Visible = true;
                            rcbPerfil.Visible = true;
                            UsuarioSER.GetInstance().RegistrarActividad(claveUsuario, 1);
                            //Session.Add("Usuario", usuario);
                            CargaComboPerfiles();                         
                        }
                    }
                    else
                    {
                        txtIncorrecto.InnerText = "Usuario inactivo, contacte al administrador";
                    }
                }
            }
            else
                txtIncorrecto.InnerText = "Usuario no registrado";

            txtUsuario.Focus();
        }
        catch (Exception ex)
        {
            txtIncorrecto.InnerText = ex.Message;
            txtIncorrecto.Visible = true;
        }
    }

    protected void btnLoginEntrar_Click(object sender, EventArgs e)
    {    
        string EmpresaSeleccionado = rcbEmpresa.SelectedValue;
        Usuario.EmpresaActivo.ClaveEmpresa = Convert.ToInt32(EmpresaSeleccionado); ;
        List<HARD.CORE.OBJ.Menu> menu = MenuSER.GetInstance().ObtenerMenu_Usuario(Usuario.ClaveUsuario, Usuario.PerfilActivo.ClavePerfil);
        HARD.CORE.OBJ.Menu paginaInicio = menu[0];
        Response.Redirect(paginaInicio.Ruta + "?nombre=" + paginaInicio.Nombre, false);
    }

    protected void btnOlvidaContraseña_Click(object sender, EventArgs e)
    {
        string usuario = Request.Form["txtUsuario"].Trim();

        bool ExisteUsuario = true;
        //ExisteUsuario = UsuarioSER.GetInstance().ExisteUsuario(usuario);
        if (ExisteUsuario)
        {
            Response.Redirect("frm_OlvidaContraseña.aspx?usuario=" + Server.UrlEncode(usuario));
        }
        else

        {
            string script = @"
            Toastify({
                text: '¡Usuario no existe en el sistema.!',
                duration: 3000,
                gravity: 'center',
                position: 'center',
                style: {
                    background: 'var(--MoradoHARDCORE, #6a0dad)',
                    color: 'white'
                }
            }).showToast();
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "toastMensaje", script, true);
        }
    }

    private void CargaComboPerfiles()
    {
        rcbPerfil.DataValueField = "ClavePerfil";
        rcbPerfil.DataTextField = "Descripcion";
        rcbPerfil.DataSource = PerfilSER.GetInstance().ObtenerActivos(Usuario.ClaveUsuario);
        rcbPerfil.DataBind();
    }

    public void CargarComboEmpresa(string ClaveUsuario,int ClavePerfil)
    {
        rcbEmpresa.DataValueField = "ClaveEmpresa";
        rcbEmpresa.DataTextField = "Descripcion";
        rcbEmpresa.DataSource = EmpresaSER.GetInstance().ObtenerActivos(ClaveUsuario,ClavePerfil);
        rcbEmpresa.DataBind();
    }

    protected void rcbPerfil_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string PerflSeleccionado = rcbPerfil.SelectedValue;
        Usuario.PerfilActivo.ClavePerfil = Convert.ToInt32(PerflSeleccionado);

        if (Usuario.ClaveUsuario != null & Usuario.PerfilActivo.ClavePerfil != 0)
        {
            CargarComboEmpresa(Usuario.ClaveUsuario, Usuario.PerfilActivo.ClavePerfil);
        }
    }
}