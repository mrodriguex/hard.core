using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Web;

public partial class pop_Administracion_HerenciaPerfil : System.Web.UI.Page
{
    private string Accion
    {
        get
        {
            if (Request.QueryString["Accion"] != null)
            {
                return Request.QueryString["Accion"];
            }

            return "";
        }
    }
    private int ClaveHerenciaPerfil
    {
        get
        {
            if (Request.QueryString["ClaveHerenciaPerfil"] != null)
            {
                return Convert.ToInt32(Request.QueryString["ClaveHerenciaPerfil"]);
            }

            return 0;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);
            if (!IsPostBack)
            {
                dpFechaInicial.SelectedDate = DateTime.Today;
                dpFechaFinal.SelectedDate = DateTime.Today;

                //if (Accion == "2") //Editar
                //{
                //    CargarDatos(ClaveHerenciaPerfil);
                //}
            }


        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }
    //private void CargarDatos(int claveHerenciaPerfil)
    //{
    //    Usuario usuario = (Usuario)Session["Usuario"];
    //    //HerenciaPerfilSER herenciaPerfil = HerenciaPerfilSER.GetInstance().Obtener(usuario.ClaveUsuario, claveHerenciaPerfil);
    //    //dpFechaInicial.SelectedDate = Convert.ToDateTime(dtDatos.Rows[0]["FechaInicial"]);
    //    //dpFechaFinal.SelectedDate = Convert.ToDateTime(dtDatos.Rows[0]["FechaFinal"]);
    //    //hdnClaveUsuario.Value = dtDatos.Rows[0]["ClaveUsuarioHeredado"].ToString();
    //    //txtNombreUsuario.Text = dtDatos.Rows[0]["UsuarioHereda"].ToString(); ;

    //}
    protected void btnHeredarPerfil_Click(object sender, EventArgs e)
    {

        try
        {
            HerenciaPerfil perfilHerencia = new HerenciaPerfil();


            Usuario usuario = (Usuario)Session["Usuario"];
            string claveUsuarioHereda = hdnClaveUsuario.Value.ToString();
            DateTime fechaInicial = (DateTime)dpFechaInicial.SelectedDate.Value.Date == DateTime.Now.Date ? DateTime.Now : Convert.ToDateTime(dpFechaInicial.SelectedDate.Value.ToString("dd/MM/yyyy"));
            DateTime fechaFinal = (DateTime)dpFechaFinal.SelectedDate.Value.AddDays(1).AddSeconds(-1);

            perfilHerencia.ClaveUsuario = usuario.ClaveUsuario;
            perfilHerencia.ClaveUsuarioHeredado = claveUsuarioHereda;
            perfilHerencia.FechaInicial = fechaFinal;
            perfilHerencia.FechaFinal = fechaFinal;
            perfilHerencia.Estatus = true;

            int claveHerencia = ClaveHerenciaPerfil;
            string mensaje = "";

            bool existeHerencia = HerenciaPerfilSER.GetInstance().ExisteHerencia(perfilHerencia);

            if (existeHerencia)
            {

                if (Accion == "2") //Edición
                {
                    bool Clave = HerenciaPerfilSER.GetInstance().Actualizar(perfilHerencia);
                    mensaje = "Herencia de perfil actualizada exitosamente";
                }
                else //Inserción
                {
                    int Clave = HerenciaPerfilSER.GetInstance().Insertar(perfilHerencia);
                    mensaje = "Perfil heredado exitosamente";
                }

                raManager.Alert(mensaje);
                raManager.ResponseScripts.Add("salir();");
            }
            else
            {
                string validacion = "No se puede " + (Accion == "2" ? "actualizar" : "registrar") + " la herencia de perfil debido a que ";
                raManager.Alert(validacion + existeHerencia);
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }

    }

}