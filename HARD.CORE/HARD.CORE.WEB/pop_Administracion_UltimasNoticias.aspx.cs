using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
public partial class pop_Administracion_UltimasNoticias : System.Web.UI.Page
{
    private int ClaveAviso
    {
        get
        {
            if (Request.QueryString["ClaveAviso"] != null)
                return Convert.ToInt32(Request.QueryString["ClaveAviso"]);
            return 0;
        }
    }
    private string RutaImagen
    {
        get
        {
            if (Request.QueryString["rutaImagen"] != null)
                return Request.QueryString["rutaImagen"].ToString();

            return string.Empty;
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
                RadAsyncUpload1.Localization.Select = "Adjuntar imagen";

            }
            else
            {
                btnGuardar.Text = "Actualizar";

                RadAsyncUpload1.Localization.Select = "Cambiar imagen";

                CargaAviso();
            }
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Usuario usuario = (Usuario)Session["Usuario"];
        
        if (Accion == 1)
        {

            Aviso Aviso = new Aviso();

            Aviso.Titulo = txtTitulo.Text;
            Aviso.Descripcion = txtDescripcion.Text;
            Aviso.Visible = (bool)cbEstatus.Checked;
            DateTime fechaInicial = (DateTime)dpFechaInicial.SelectedDate.Value.Date == DateTime.Now.Date ? DateTime.Now : Convert.ToDateTime(dpFechaInicial.SelectedDate.Value.ToString("dd/MM/yyyy"));
            DateTime fechaFinal = (DateTime)dpFechaFinal.SelectedDate.Value.AddDays(1).AddSeconds(-1);
            foreach (UploadedFile file in RadAsyncUpload1.UploadedFiles)
            {
                string nombreArchivo = file.FileName;

                using (var stream = file.InputStream)
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        byte[] imagenBytes = ms.ToArray();

                        Aviso.ImagenByte = imagenBytes;
                        Aviso.NombreImagen = nombreArchivo;
                    }
                }
            }
            Aviso.RutaImagen = "";

            Aviso.ClaveUsuarioActualizacion = usuario.ClaveUsuario;
            Aviso.FechaInicial = fechaInicial;
            Aviso.FechaFinal = fechaFinal;

            int ClaveAvisoInserto=  AvisoSER.GetInstance().InsertarAvisoCarrusel(Aviso);

            if (ClaveAvisoInserto > 0)
            {
                raManager.Alert("Se ha registrado correctamente la configuración de avisos con clave : " + ClaveAvisoInserto);
                raManager.ResponseScripts.Add("salir();");

            }
        }
        else
        {
            Aviso Aviso = new Aviso();
            DateTime fechaInicial = (DateTime)dpFechaInicial.SelectedDate.Value.Date == DateTime.Now.Date ? DateTime.Now : Convert.ToDateTime(dpFechaInicial.SelectedDate.Value.ToString("dd/MM/yyyy"));
            DateTime fechaFinal = (DateTime)dpFechaFinal.SelectedDate.Value.AddDays(1).AddSeconds(-1);

            Aviso.Titulo = txtTitulo.Text;
            Aviso.Descripcion = txtDescripcion.Text;
            Aviso.FechaInicial = fechaInicial;
            Aviso.FechaFinal = fechaFinal;
            Aviso.Visible = (bool)cbEstatus.Checked;
            Aviso.ClaveAviso = ClaveAviso;
            foreach (UploadedFile file in RadAsyncUpload1.UploadedFiles)
            {
                string nombreArchivo = file.FileName;

                using (var stream = file.InputStream)
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        byte[] imagenBytes = ms.ToArray();

                        Aviso.ImagenByte = imagenBytes;
                        Aviso.NombreImagen = nombreArchivo;
                    }
                }
            }
            if (Aviso.ImagenByte == null || Aviso.ImagenByte.Length == 0)
            {
                byte[] imagen = Session["ImagenAvisoCarrusel"] as byte[];
                string NombreImagen = Session["NombreImagen"] as string;
                Aviso.ImagenByte = imagen;
                Aviso.NombreImagen = NombreImagen;
            }


            Aviso.ClaveUsuarioActualizacion = usuario.ClaveUsuario;
            Aviso.RutaImagen = RutaImagen;


            bool Result = AvisoSER.GetInstance().ActualizarAvisoCarrusel(Aviso);

            if (Result)
            {
                raManager.Alert("Se ha actualizado correctamente la configuración de avisos con clave : " + ClaveAviso);
                Session.Remove("NombreImagen");
                Session.Remove("ImagenAvisoCarrusel");
                raManager.ResponseScripts.Add("salir();");
            }

        }

    }
    public void CargaAviso()
    {
        Aviso AvisoCarrusel = AvisoSER.GetInstance().ObtenerAviso(ClaveAviso);
        txtTitulo.Text = AvisoCarrusel.Titulo;
        txtDescripcion.Text = AvisoCarrusel.Descripcion;
        cbEstatus.Checked = AvisoCarrusel.Visible;
        byte[] imagen = AvisoCarrusel.ImagenByte;
        Session["ImagenAvisoCarrusel"] = imagen;
        string nombreImagen = AvisoCarrusel.NombreImagen;
        Session["NombreImagen"] = nombreImagen;
        string extension = Path.GetExtension(AvisoCarrusel.NombreImagen).ToLower().TrimStart('.');
        if (extension == "jpg")
        {
            extension = "jpeg"; // Data URL usa "jpeg" no "jpg"
        }
        var imagenByte = ComunesSER.RedimensionarImagen(imagen, 700, 700);
        string imagenBase64 = Convert.ToBase64String(imagenByte);
        imgVistaPrevia.Src = "data:image/" + extension + ";base64," + imagenBase64;
        imgVistaPrevia.Style["display"] = "block";
        dpFechaInicial.SelectedDate = AvisoCarrusel.FechaInicial;
        dpFechaFinal.SelectedDate = AvisoCarrusel.FechaFinal;
    }
    protected void rauImagen_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        try
        {
            Context.Cache.Remove(Session.SessionID + "UploadedFile");
            //foreach (UploadedFile file in RadAsyncUpload1.UploadedFiles)
            //{
            //    Stream fs = file.InputStream;
            //    BinaryReader br = new BinaryReader(fs);
            //    byte[] bytes = br.ReadBytes(Convert.ToInt32(fs.Length));
            //    //ByteImagen = bytes;
            //    //NombreImagen = file.FileName;
            //}
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}