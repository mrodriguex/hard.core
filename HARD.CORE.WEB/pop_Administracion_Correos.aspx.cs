using CRY.PCC.SER;
using HARD.CORE.OBJ;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

public partial class pop_Administracion_Correos : System.Web.UI.Page
{

    public int Accion
    {
        get
        {
            int claveGrupoTecnico = 0;
            int.TryParse(Request.QueryString["Accion"], out claveGrupoTecnico);
            return (claveGrupoTecnico);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!Page.IsPostBack)
            {
                CargarTiposCorreo();
                CargarUsuarios();

                switch (Accion)
                {
                    case 3:
                        Carga_Datos();
                        HabilitaControles(false);
                        lblTitulo.Text = "Consultar correo";
                        break;
                    case 2:
                        Carga_Datos();
                        lblTitulo.Text = "Editar correo";
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void Carga_Datos()
    {
        try
        {
            int claveCorreo = Convert.ToInt32(Request.QueryString["ClaveCorreo"]);
            Correo correo = CorreosSER.GetInstance().Obtener(claveCorreo);

            foreach (string usuario in correo.Para.Split(';'))
            {
                RadComboBoxItem item = rcbUsuarios.Items.FindItemByValue(usuario);
                if (item != null)
                {
                    item.Checked = true;
                }
            }

            txtUsuarios.Text = correo.Para;
            txtTitulo.Text = correo.Titulo;
            txtSubtitulo.Text = correo.Subtitulo;
            txtAsunto.Text = correo.Asunto;
            txtPie.Text = correo.Pie;
            reCuerpo.Content = correo.Cuerpo;
            rblImportancia.SelectedValue = correo.Importancia;
            rcbTipoCorreo.SelectedValue = correo.ClaveTipoCorreo.ToString();

            CargarVariables();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void CargarTiposCorreo()
    {
        rcbTipoCorreo.DataTextField = "Descripcion";
        rcbTipoCorreo.DataValueField = "ClaveTipoCorreo";
        rcbTipoCorreo.DataSource = CorreosSER.GetInstance().ObtenerTiposCorreo();
        rcbTipoCorreo.DataBind();
    }

    protected void CargarVariables()
    {
        try
        {
            int claveTipoCorreo = Convert.ToInt32(rcbTipoCorreo.SelectedValue);
            lsVariables.DataTextField = "Etiqueta";
            lsVariables.DataValueField = "Valor";
            lsVariables.DataSource = CorreosSER.GetInstance().ObtenerVariables(claveTipoCorreo);
            lsVariables.DataBind();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void CargarUsuarios()
    {


        rcbUsuarios.DataTextField = "Correo";
        rcbUsuarios.DataValueField = "Correo";
        rcbUsuarios.DataSource = CorreosSER.GetInstance().ObtenerCorreos().Select(x => new { Correo = x });
        rcbUsuarios.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
       try
       {
            int claveCorreo = 0;
            string accion = Request.QueryString["Accion"];
            Usuario usuario = (Usuario)Session["Usuario"];

            if (Request.QueryString["ClaveCorreo"] != null)
            {
                claveCorreo = Convert.ToInt32(Request.QueryString["ClaveCorreo"]);
            }

            Correo correo = new Correo
            {
                ClaveCorreo = claveCorreo,
                Asunto = txtAsunto.Text,
                Para = txtUsuarios.Text.Trim(),
                CC = "",
                CCO = "",
                Titulo = txtTitulo.Text.Trim(),
                Subtitulo = txtSubtitulo.Text.Trim(),
                Cuerpo = reCuerpo.Content,
                Pie = txtPie.Text.Trim(),
                Importancia = rblImportancia.SelectedValue,
                ClaveTipoCorreo = Convert.ToInt32(rcbTipoCorreo.SelectedValue)
            };

            switch (Accion)
            {
                case 1:
                    claveCorreo = CorreosSER.GetInstance().Insertar(correo);
                    raManager.Alert("El correo se ha registrado exitosamente con clave: " + claveCorreo.ToString());
                    break;
                case 2:
                    CorreosSER.GetInstance().Actualizar(correo);
                    raManager.Alert("El correo con clave " + correo.ClaveCorreo + " se ha actualizado exitosamente");
                    break;
            }
            raManager.ResponseScripts.Add("salir();");
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void HabilitaControles(bool habilita)
    {
        rcbUsuarios.Enabled = habilita;
        txtUsuarios.ReadOnly = !habilita;
        rblImportancia.Enabled = habilita;

        txtTitulo.ReadOnly = !habilita;
        txtSubtitulo.ReadOnly = !habilita;
        txtAsunto.ReadOnly = !habilita;
        txtPie.ReadOnly = !habilita;
        rcbTipoCorreo.Enabled = habilita;

        reCuerpo.EditModes = ((!habilita) ? EditModes.Preview : EditModes.Design);
        btnGuardar.Visible = habilita;
        lsVariables.Enabled = habilita;
    }

    protected void rcbTipoCorreo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        CargarVariables();
    }
}