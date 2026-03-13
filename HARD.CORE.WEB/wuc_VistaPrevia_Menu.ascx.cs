using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

public partial class wuc_VistaPrevia_Menu : System.Web.UI.UserControl, IToolTip
{

    public int Clave { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        HttpContext context = HttpContext.Current;
        Generales.GetInstance().LimpiaCache(ref context);

        List<Menu> menuPerfil = MenuSER.GetInstance().ObtenerMenu_Perfil(Clave);

        rtvMenu.DataTextField = "Nombre";
        rtvMenu.DataFieldID = "ClaveMenu";
        rtvMenu.DataFieldParentID = "ClaveMenuPadre";
        rtvMenu.DataValueField = "ClaveMenu";
        rtvMenu.DataSource = menuPerfil;
        rtvMenu.DataBind();

    }

}