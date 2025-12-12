<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionMotivoVacante.aspx.cs" Inherits="frm_AdministracionMotivoVacante" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgMotivoVacante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMotivoVacante" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnActualizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMotivoVacante" UpdatePanelCssClass="" LoadingPanelID="LoadingPanel" />                                 
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">

            function seleccionarConsultar(index) {

                var rgSeguridadAccion = $find('<%= rgMotivoVacante.ClientID%>');
                var masterTable = rgSeguridadAccion.get_masterTableView();
                var dataItem = masterTable.get_dataItems()[index];
                var chkSustituye = dataItem.findElement("chkSustituye");
                //var chkModificar = dataItem.findElement("chkModificar");
                //var chkConsultar = dataItem.findElement("chkConsultar");

                //if (chkSustituye.checked && chkModificar.checked) {
                //    chkConsultar.checked = true;
                //}

            }

            function OnClientActualizar(ev) {
                var floatingButton = ev.get_id();
                __doPostBack(floatingButton);
            }

            function RecargarGrid() {
                $find("<%= rgMotivoVacante.ClientID %>").get_masterTableView().rebind();
            }

            function refreshGrid() {

                var masterTable = $find("<%= rgMotivoVacante.ClientID %>").get_masterTableView();
                masterTable.rebind();

            }

        </script>
    </telerik:RadScriptBlock>

    <div class="catalogo">

        <div class="barra">
            <div class="titulo">
                Motivos de vacante
            </div>
            <div class="acciones">
                &nbsp;
            </div>
        </div>

        <div class="contenido">
            <input type="hidden" id="rowIndex" name="rowIndex" />
            <telerik:RadGrid ID="rgMotivoVacante" runat="server" Height="600px"
                AutoGenerateColumns="False"
                AllowFilteringByColumn="True"
                FilterItemStyle-VerticalAlign="Middle"
                FilterItemStyle-HorizontalAlign="Center"
                GroupingEnabled="false"
                OnNeedDataSource="rgMotivoVacante_NeedDataSource"
                OnItemCreated="rgSeguridadAccion_ItemCreated"
                OnItemDataBound="rgSeguridadAccion_ItemDataBound">
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView Width="100%" ClientDataKeyNames="ClaveMotivoVacante,Descripcion"
                    DataKeyNames="">
                    <Columns>

                        <telerik:GridBoundColumn DataField="ClaveMotivoVacante" AllowFiltering="false" HeaderText="Clave" UniqueName="ClaveMotivoVacante" Display="true">
                            <HeaderStyle Width="50px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Descripcion" AllowFiltering="false" HeaderText="Descripción" UniqueName="Descripcion" Display="true">
                            <HeaderStyle Width="50px" />
                        </telerik:GridBoundColumn>


                        <telerik:GridTemplateColumn HeaderText="Sustituye" UniqueName="Sustituye" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkSustituye" Checked='<%#(bool)Eval("Sustituye") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn HeaderText="Centro costos" UniqueName="CentroCostos" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkCentroCostos" Checked='<%#(bool)Eval("CentroCostos") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Motivo incapacidad" UniqueName="MotivoIncapacidad" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkMotivoIncapacidad" Checked='<%#(bool)Eval("MotivoIncapacidad") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Numero meses" UniqueName="NumeroMeses" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkNumeroMeses" Checked='<%#(bool)Eval("NumeroMeses") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Justificacion nuevo puesto" UniqueName="JustificacionNuevoPuesto" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkJustificacionNuevoPuesto" Checked='<%#(bool)Eval("JustificacionNuevoPuesto") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents></ClientEvents>
                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <AlternatingItemStyle HorizontalAlign="Center" />
                <PagerStyle Mode="NextPrevAndNumeric" />
            </telerik:RadGrid>
        </div>
        <telerik:RadFloatingActionButton runat="server" ID="btnActualizar" PositionMode="Fixed" Align="BottomEnd"
            Size="Medium" ThemeColor="primary" Text="Actualizar configuración">
            <AlignOffsetSettings X="50" Y="50" />
            <ClientEvents OnClick="OnClientActualizar" />
        </telerik:RadFloatingActionButton>
    </div>
    <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
