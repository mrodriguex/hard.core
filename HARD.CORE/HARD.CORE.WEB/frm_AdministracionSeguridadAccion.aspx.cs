using HARD.CORE.OBJ;
using HARD.CORE.SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionSeguridadAccion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string eventTarget = Request["__EVENTTARGET"]; // RadFloatingActionButton

        if (!string.IsNullOrWhiteSpace(eventTarget) && eventTarget == btnActualizar.ClientID)
        {
            btnActualizar_Click(eventTarget, e);
        }

        if (!IsPostBack)
        {
            CargaComboPerfiles();
        }
    }

    protected void rgSeguridadAccion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        rgSeguridadAccion.DataSource = new List<SeguridadAccion>();
        int clavePerfil = rcbPerfil.SelectedValue == "" ? 0 : Convert.ToInt32(rcbPerfil.SelectedValue);
        //int valorAsignado = Convert.ToInt32(rcbAsignado.SelectedValue);

        if (clavePerfil != 0)
        {
            int[] clavesExcluir = { 15, 16, 17, 18 };

            rgSeguridadAccion.DataSource =
                SeguridadAccionSER.GetInstance()
                .ObtenerPorPerfil(clavePerfil)
                .Where(x => !clavesExcluir.Contains(x.ClaveMenu))
                .ToList();

      
            //rgSeguridadAccion.DataSource = SeguridadAccionSER.GetInstance().ObtenerPorPerfil(clavePerfil);

            //if (valorAsignado != 0)
            //{
            //    bool asignado = valorAsignado == 1;
            //    rgSeguridadAccion.DataSource = SeguridadAccionSER.GetInstance().ObtenerPorPerfil(clavePerfil);
            //}
            //else
            //{
            //    rgSeguridadAccion.DataSource = SeguridadAccionSER.GetInstance().ObtenerPorPerfil(clavePerfil);
            //}
        }


    }

    private void CargaComboPerfiles()
    {
        rcbPerfil.DataValueField = "ClavePerfil";
        rcbPerfil.DataTextField = "Descripcion";
        rcbPerfil.DataSource = PerfilSER.GetInstance().ObtenerPerfiles();
        rcbPerfil.DataBind();
    }
    protected void rcbPerfil_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rgSeguridadAccion.Rebind();
    }
    //protected void rcbAsignado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //    rgSeguridadAccion.Rebind();
    //    //btnActualizar.Enabled = rgSeguridadAccion.MasterTableView.Items.Count > 0;
    //}

    protected void rgSeguridadAccion_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            //if (e.Item.ItemType == GridItemType.FilteringItem)
            //{
            //    GridFilteringItem item = (GridFilteringItem)e.Item;
            //    TextBox txtTipoEntidad = (TextBox)item["TipoEntidad"].Controls[0];
            //    txtTipoEntidad.MaxLength = 50;
            //}
            if (e.Item is GridDataItem)
            {
                //GridDataItem item = (GridDataItem)e.Item;
                //CheckBox chkSustituye = (CheckBox)item["Sustituye"].FindControl("chkSustituye");
                //Boolean ConfigurarSustituye = (Boolean)item.GetDataKeyValue("ConfigurarSustituye");
                //chkSustituye.Enabled = ConfigurarSustituye;
                //chkSustituye.Attributes.Add("onclick", "seleccionarConsultar('" + e.Item.ItemIndex + "')");

                //CheckBox chkEliminar = (CheckBox)item["Eliminar"].Controls[0];
                //Boolean ConfigurarEliminar = (Boolean)item.GetDataKeyValue("ConfigurarEliminar");
                //chkEliminar.Enabled = ConfigurarEliminar;

                //CheckBox chkModificar = (CheckBox)item["Modificar"].FindControl("chkModificar");
                //Boolean ConfigurarModificar = (Boolean)item.GetDataKeyValue("ConfigurarModificar");
                //chkModificar.Enabled = ConfigurarModificar;
                //chkModificar.Attributes.Add("onclick", "seleccionarConsultar('" + e.Item.ItemIndex + "')");

                //CheckBox chkConsultar = (CheckBox)item["Consultar"].FindControl("chkConsultar");
                //Boolean ConfigurarConsultar = (Boolean)item.GetDataKeyValue("ConfigurarConsultar");
                //chkConsultar.Enabled = ConfigurarConsultar;

                //CheckBox chkAutorizar = (CheckBox)item["Autorizar"].Controls[0];
                //Boolean ConfigurarAutorizar = (Boolean)item.GetDataKeyValue("ConfigurarAutorizar");
                //chkAutorizar.Enabled = ConfigurarAutorizar;

                //CheckBox chkRechazar = (CheckBox)item["Rechazar"].Controls[0];
                //Boolean ConfigurarRechazar = (Boolean)item.GetDataKeyValue("ConfigurarRechazar");
                //chkRechazar.Enabled = ConfigurarRechazar;

                //CheckBox chkImprimir = (CheckBox)item["Imprimir"].Controls[0];
                //Boolean ConfigurarImprimir = (Boolean)item.GetDataKeyValue("ConfigurarImprimir");
                //chkImprimir.Enabled = ConfigurarImprimir;

                //CheckBox chkAsignar = (CheckBox)item["Asignar"].Controls[0];
                //Boolean ConfigurarAsignar = (Boolean)item.GetDataKeyValue("ConfigurarAsignar");
                //chkAsignar.Enabled = ConfigurarAsignar;

                //CheckBox chkAtender = (CheckBox)item["Atender"].Controls[0];
                //Boolean ConfigurarAtender = (Boolean)item.GetDataKeyValue("ConfigurarAtender");
                //chkAtender.Enabled = ConfigurarAtender;

                //CheckBox chkCancelar = (CheckBox)item["Cancelar"].Controls[0];
                //Boolean ConfigurarCancelar = (Boolean)item.GetDataKeyValue("ConfigurarCancelar");
                //chkCancelar.Enabled = ConfigurarCancelar;



            }
        }
        catch (Exception ex)
        {
            raManager.Alert(ex.Message);
        }
    }
    protected void rgSeguridadAccion_ItemDataBound(object sender, GridItemEventArgs e)
    {
        GridDataItem dataItem = e.Item as GridDataItem;
        if (dataItem != null)
        {
            //CheckBox chkSustituye = dataItem.FindControl("chkSustituye") as CheckBox;

            //string script = string.Format("console.log('Fila {0} Sustituye={1}');",
            //    dataItem.ItemIndex,
            //    chkSustituye.Checked.ToString().ToLower());

            //RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(script);
        }
    }



    //protected void rgSeguridadAccion_ItemDataBound(object sender, GridItemEventArgs e)
    //{
    //    btnActualizar.Enabled = rgMotivoVacante.MasterTableView.Items.Count > 0;

    //    if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
    //    {
    //        GridDataItem item = (GridDataItem)e.Item;


    //        CheckBox chkSustituye = item.FindControl("chkSustituye") as CheckBox;
    //        HtmlGenericControl divSustituye = chkSustituye.Parent as HtmlGenericControl;


    //        if (chkSustituye != null)
    //        {
    //            item["Sustituye"].BackColor = chkSustituye.Checked
    //? System.Drawing.Color.FromArgb(189, 225, 253)
    //: System.Drawing.Color.MistyRose;

    //        }
    //        //i)
    //        //{

    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("Asignado")))
    //        //{
    //        //    item["Asignado"].BackColor = System.Drawing.Color.FromArgb(189, 225, 253);
    //        //    item["Asignado"].ToolTip = "No asignado";
    //        //}
    //        //else
    //        //{
    //        //    item["Asignado"].BackColor = System.Drawing.Color.FromArgb(155, 207, 168);
    //        //    item["Asignado"].ToolTip = "Asignado";
    //        //}

    //        //item["Asignado"].Text = "";


    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarSustituye")))
    //        //{
    //        //    item["Sustituye"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Sustituye"].FindControl("chkSustituye").Visible = false;
    //        //}


    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarEliminar")))
    //        //{
    //        //    item["Eliminar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Eliminar"].Controls[0].Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarModificar")))
    //        //{
    //        //    item["Modificar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Modificar"].FindControl("chkModificar").Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarConsultar")))
    //        //{
    //        //    item["Consultar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Consultar"].FindControl("chkConsultar").Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarAutorizar")))
    //        //{
    //        //    item["Autorizar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Autorizar"].Controls[0].Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarRechazar")))
    //        //{
    //        //    item["Rechazar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Rechazar"].Controls[0].Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarImprimir")))
    //        //{
    //        //    item["Imprimir"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Imprimir"].Controls[0].Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarAsignar")))
    //        //{
    //        //    item["Asignar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Asignar"].Controls[0].Visible = false;
    //        //}

    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarAtender")))
    //        //{
    //        //    item["Atender"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Atender"].Controls[0].Visible = false;
    //        //}
    //        //if (!Convert.ToBoolean(item.GetDataKeyValue("ConfigurarCancelar")))
    //        //{
    //        //    item["Cancelar"].BackColor = System.Drawing.Color.WhiteSmoke;
    //        //    item["Cancelar"].Controls[0].Visible = false;
    //        //}



    //    }
    //}

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        List<SeguridadAccion> listaSeguridad = new List<SeguridadAccion>();

        foreach (GridDataItem item in rgSeguridadAccion.Items)
        {
            var claveMenu = ((Telerik.Web.UI.RadNumericTextBox)item.FindControl("txtClaveMenu")).Value;

            SeguridadAccion seguridadAccion = new SeguridadAccion
            {
                ClaveSeguridadAccion = 0,
                ClavePerfil = Convert.ToInt32(rcbPerfil.SelectedValue.ToString()),


                ClaveMenu = Convert.ToInt32(claveMenu),
                Descripcion = item["Descripcion"].Text,
                Crear = ((CheckBox)item.FindControl("chkCrear")).Checked,
                Modificar = ((CheckBox)item.FindControl("chkModificar")).Checked,
                Consultar = ((CheckBox)item.FindControl("chkConsultar")).Checked,
                Eliminar = ((CheckBox)item.FindControl("chkEliminar")).Checked,
                Autorizar = ((CheckBox)item.FindControl("chkAutorizar")).Checked,

                Rechazar = ((CheckBox)item.FindControl("chkRechazar")).Checked,
                Imprimir = ((CheckBox)item.FindControl("chkImprimir")).Checked,
                Asignar = ((CheckBox)item.FindControl("chkAsignar")).Checked,
                Cancelar = ((CheckBox)item.FindControl("chkCancelar")).Checked


            };
            listaSeguridad.Add(seguridadAccion);

        }
        bool result = SeguridadAccionSER.GetInstance().Actualizar(listaSeguridad);
        if (result)
        {
            raManager.Alert("Se ha actualizado correctamente");
            rgSeguridadAccion.Rebind();
        }
        else
        {
            raManager.Alert("Error");

        }


    }

}