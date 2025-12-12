<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionSeguridadAccion.aspx.cs" Inherits="frm_AdministracionSeguridadAccion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgSeguridadAccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSeguridadAccion" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnActualizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSeguridadAccion" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbPerfil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSeguridadAccion" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">

            function seleccionarConsultar(index) {

                var rgSeguridadAccion = $find('<%= rgSeguridadAccion.ClientID%>');
                var masterTable = rgSeguridadAccion.get_masterTableView();
                var dataItem = masterTable.get_dataItems()[index];
                var chkSustituye = dataItem.findElement("Imprimir");
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
                $find("<%= rgSeguridadAccion.ClientID %>").get_masterTableView().rebind();
            }

            function refreshGrid() {

                var masterTable = $find("<%= rgSeguridadAccion.ClientID %>").get_masterTableView();
                masterTable.rebind();

            }

        </script>
    </telerik:RadScriptBlock>

    <div class="catalogo">

        <div class="barra">
            <div class="titulo">
                Seguridad acción
            </div>

        </div>
        <div class="acciones">
            <table>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="rcbPerfil" CssClass="ComboBoxPerfil" runat="server" AutoPostBack="true" MarkFirstMatch="true" EnableViewState="true" Width="100%" EmptyMessage="Seleccione un perfil" OnSelectedIndexChanged="rcbPerfil_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                    <%--<td>
                        <telerik:RadComboBox ID="rcbAsignado" runat="server" MarkFirstMatch="true" Width="200px" MaxHeight="300px"
                            Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="rcbAsignado_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Todos" Value="0" />
                                <telerik:RadComboBoxItem runat="server" Text="Asignado" Value="1" />
                                <telerik:RadComboBoxItem runat="server" Text="No asignado" Value="2" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>--%>
                </tr>

            </table>
        </div>

        <div class="contenido">
            <input type="hidden" id="rowIndex" name="rowIndex" />
            <telerik:RadGrid ID="rgSeguridadAccion" runat="server" Height="600px"
                AutoGenerateColumns="False"
                AllowFilteringByColumn="True"
                FilterItemStyle-VerticalAlign="Middle"
                FilterItemStyle-HorizontalAlign="Center"
                GroupingEnabled="false"
                OnNeedDataSource="rgSeguridadAccion_NeedDataSource"
                OnItemCreated="rgSeguridadAccion_ItemCreated"
                OnItemDataBound="rgSeguridadAccion_ItemDataBound">
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView Width="100%" ClientDataKeyNames="ClaveSeguridadAccion,Descripcion"
                    DataKeyNames="ClaveSeguridadAccion,Descripcion,ClaveMenu">
                    <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>

                    <Columns>

                        <telerik:GridNumericColumn DataField="ClaveSeguridadAccion" AllowFiltering="false" HeaderText="Clave" UniqueName="ClaveSeguridadAccion" Display="true" Visible="false">
                            <HeaderStyle Width="50px" />
                        </telerik:GridNumericColumn>

                        <telerik:GridTemplateColumn HeaderText="ClaveMenu" UniqueName="ClaveMenu" Visible="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <telerik:RadNumericTextBox runat="server" ID="txtClaveMenu" Value='<%#(int)Eval("ClaveMenu") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="Descripcion" AllowFiltering="false" HeaderText="Modulo" UniqueName="Descripcion" Display="true">
                            <HeaderStyle Width="75px" />
                        </telerik:GridBoundColumn>


                        <telerik:GridTemplateColumn HeaderText="Crear" UniqueName="Crear" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkCrear" Checked='<%#(bool)Eval("Crear") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn HeaderText="Modificar" UniqueName="Modificar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkModificar" Checked='<%#(bool)Eval("Modificar") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Consultar" UniqueName="Consultar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkConsultar" Checked='<%#(bool)Eval("Consultar") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Eliminar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkEliminar" Checked='<%#(bool)Eval("Eliminar") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Autorizar" UniqueName="Autorizar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkAutorizar" Checked='<%#(bool)Eval("Autorizar") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Rechazar" UniqueName="Rechazar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkRechazar" Checked='<%#(bool)Eval("Rechazar") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Imprimir" UniqueName="Imprimir" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkImprimir" Checked='<%#(bool)Eval("Imprimir") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Asignar" UniqueName="Asignar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkAsignar" Checked='<%#(bool)Eval("Asignar") %>' />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Cancelar" UniqueName="Cancelar" AllowFiltering="false">
                            <HeaderStyle Width="90px" />
                            <ItemTemplate>
                                <div class="checkbox-cell">
                                    <asp:CheckBox runat="server" ID="chkCancelar" Checked='<%#(bool)Eval("Cancelar") %>' />
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
