using CRY.PCC.SER;
using HARD.CORE.OBJ;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_AdministracionMotivoVacante : System.Web.UI.Page
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
        }
    }

    protected void rgMotivoVacante_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
       rgMotivoVacante.DataSource = MotivoVacanteSER.GetInstance().ObtenerTodos();
    }

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
            CheckBox chkSustituye = dataItem.FindControl("chkSustituye") as CheckBox;

            string script = string.Format("console.log('Fila {0} Sustituye={1}');",
                dataItem.ItemIndex,
                chkSustituye.Checked.ToString().ToLower());

            RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(script);
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
        foreach (GridDataItem item in rgMotivoVacante.Items)
        {
            MotivoVacante motivo = new MotivoVacante
            {
                ClaveMotivoVacante = Convert.ToInt32(item["ClaveMotivoVacante"].Text),
                Descripcion = item["Descripcion"].Text,
                Sustituye = ((CheckBox)item.FindControl("chkSustituye")).Checked,
                CentroCostos = ((CheckBox)item.FindControl("chkCentroCostos")).Checked,
                MotivoIncapacidad = ((CheckBox)item.FindControl("chkMotivoIncapacidad")).Checked,
                NumeroMeses = ((CheckBox)item.FindControl("chkNumeroMeses")).Checked,
                JustificacionNuevoPuesto = ((CheckBox)item.FindControl("chkJustificacionNuevoPuesto")).Checked,
                Estatus = true,
                ClaveUsuarioUltimaActualizacion = "usuarioActual"
            };

            MotivoVacanteSER.GetInstance().Actualizar(motivo);
        }
        rgMotivoVacante.Rebind();

        raManager.Alert("Se ha actualizado correctamente");

        // 🔄 Actualiza el grid con AJAX (y se repinta el color)
    }


}