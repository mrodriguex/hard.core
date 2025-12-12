using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

public partial class pop_Administracion_Perfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            if (!Page.IsPostBack)
            {

                List<Menu> menu = new List<Menu>();

                Session.Remove("MenuPerfil");

                int clavePerfil = int.Parse(Request.QueryString["ClavePerfil"]);

                if (clavePerfil == 0)
                {
                    cbEstatus.Checked = true;
                    btnGuardar.Text = "Guardar perfil";
                    txtDescripcion.Focus();
                    cargarMenuAsigndo(menu);
                }
                else
                {
                    menu = MenuSER.GetInstance().ObtenerMenu_Perfil(clavePerfil);
                    cargarMenuAsigndo(menu);
                    Perfil perfil = PerfilSER.GetInstance().ObtenerPerfil(clavePerfil);

                    if (perfil != null)
                    {
                        lblTitulo.Text = "Configuración perfil (" + perfil.Descripcion + ")";
                        txtDescripcion.Text = perfil.Descripcion;
                        cbEstatus.Checked = perfil.Estatus;
                        btnGuardar.Text = "Actualizar perfil";
                    }
                }

                cargarMenuDisponible(menu);

            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    public void cargarMenuAsigndo(List<Menu> menu)
    {
        try
        {
            rtvMenuAsignado.DataTextField = "Nombre";
            rtvMenuAsignado.DataFieldID = "ClaveMenu";
            rtvMenuAsignado.DataFieldParentID = "ClaveMenuPadre";
            rtvMenuAsignado.DataValueField = "ClaveMenu";
            rtvMenuAsignado.DataSource = menu;
            rtvMenuAsignado.DataBind();

        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    public void cargarMenuDisponible(List<Menu> menu)
    {
        try
        {
            rtvMenu.DataTextField = "Nombre";
            rtvMenu.DataFieldID = "ClaveMenu";
            rtvMenu.DataFieldParentID = "ClaveMenuPadre";
            rtvMenu.DataValueField = "ClaveMenu";
            rtvMenu.DataSource = MenuSER.GetInstance().ObtenerMenu_Perfil(0);
            rtvMenu.DataBind();

            foreach (Menu unMenu in menu)
            {
                var nodo = rtvMenu.Nodes.FindNodeByValue(unMenu.ClaveMenu.ToString());
                if (nodo != null)
                {
                    nodo.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    protected void rtvMenu_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        try
        {
            rtvMenuAsignado.Nodes.Clear();
            foreach (RadTreeNode node in rtvMenu.CheckedNodes)

                AddWithParents(node);

            rtvMenuAsignado.ExpandAllNodes();
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }

    private void AddWithParents(RadTreeNode source)
    {
        // If parent exists, ensure its path is created first
        RadTreeNode parent = source.Parent as RadTreeNode;
        RadTreeNode parentInB = null;

        if (parent != null)
        {
            AddWithParents(parent); // ensures parent chain is created
            parentInB = rtvMenuAsignado.FindNodeByValue(parent.Value);
        }

        // If node already exists → nothing to do
        if (rtvMenuAsignado.FindNodeByValue(source.Value) != null)
            return;

        // Create this node
        RadTreeNode clone = new RadTreeNode(source.Text, source.Value);

        // Add to parent or root
        if (parentInB == null)
            rtvMenuAsignado.Nodes.Add(clone);
        else
            parentInB.Nodes.Add(clone);
    }


    protected void rtvMenu_NodeDataBound(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Expanded = true;
    }

    protected void rtvMenuAsignado_NodeDataBound(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Expanded = true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            var seleccionados = rtvMenu.GetAllNodes()
                                            .Where(x => x.Checked);

            List<Menu> menus = new List<Menu>();
            foreach (var nodo in seleccionados)
            {
                menus.Add(new Menu
                {
                    ClaveMenu = Convert.ToInt32(nodo.Value),
                    Nombre = nodo.Text
                });
            }

            Usuario usuario = (Usuario)Session["Usuario"];

            int clavePerfil = int.Parse(Request.QueryString["ClavePerfil"]);

            if (clavePerfil != 0)
            {
                Perfil perfil = PerfilSER.GetInstance().ObtenerPerfil(clavePerfil);
                perfil.Estatus = cbEstatus.Checked;
                perfil.Descripcion = txtDescripcion.Text;
                perfil.Menus = menus;

                PerfilSER.GetInstance().ActualizarPerfil(perfil);
                MenuSER.GetInstance().ConfigurarMenu_Perfil(perfil.ClavePerfil, perfil.Menus);
                raManager.Alert("Se ha actualizado el perfil con clave : " + perfil.ClavePerfil);
            }
            else
            {
                Perfil perfil = new Perfil(0, txtDescripcion.Text, cbEstatus.Checked);
                perfil.Menus = menus;
                int result = PerfilSER.GetInstance().InsertarPerfil(perfil);
                MenuSER.GetInstance().ConfigurarMenu_Perfil(result, perfil.Menus);
                raManager.Alert("Se ha insertado el perfil con clave : " + result);
            }

            Session.Remove("MenuPerfil");
            raManager.ResponseScripts.Add("salir();");
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
}