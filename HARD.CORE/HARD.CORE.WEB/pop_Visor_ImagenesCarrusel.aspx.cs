using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Web;

public partial class pop_Visor_ImagenesCarrusel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            string clave = Request.QueryString["clave"];
            if (!string.IsNullOrEmpty(clave))
            {
                Aviso AvisoCarrusel = AvisoSER.GetInstance().ObtenerAviso(Convert.ToInt32(clave));
              

                byte[] contenido = AvisoCarrusel.ImagenByte;

                if (contenido != null && contenido.Length > 0)
                {
                    string base64 = Convert.ToBase64String(contenido);
                    imgVisualizar.ImageUrl = string.Format("data:image/jpeg;base64,{0}", base64);
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}