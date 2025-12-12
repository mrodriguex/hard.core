<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master"  CodeFile="frm_AdministracionCorreos.aspx.cs" Inherits="frm_AdministracionCorreos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCorreos" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCorreos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCorreos" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rmCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCorreos" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptBlock ID="rsBlock" runat="server">

        <script>

            function nuevo(sender, args) {

                openRadWindow("pop_Administracion_Correos.aspx?Accion=1", "AdministrarCorreo", 1200, 825, true)
                args.set_cancel(true);

            }

            function TipoCorreoIndexChanged(sender, args) {

                var tableView = $find("<% =rgCorreos.MasterTableView.ClientID%>");
                if (args.get_item().get_value() == "TODOS") {
                    tableView.filter("CTipoCorreo", args.get_item().get_value(), "NoFilter");
                } else {
                    tableView.filter("CTipoCorreo", args.get_item().get_value(), "EqualTo");
                }

            }

            function RowContextMenu(sender, eventArgs) {

                var menu = $find("<%= rmCorreo.ClientID%>");
                var items = menu.get_items();
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("rowIndex").value = index;

                sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }

            }

            function menu(sender, args) {

                var rowIndex = document.getElementById("rowIndex").value
                var rgCorreos = $find('<%= rgCorreos.ClientID%>');
                var masterTable = rgCorreos.get_masterTableView();
                var dataItem = masterTable.get_dataItems()[rowIndex];

                var menu = $find("<%= rmCorreo.ClientID%>");
                var nombreMenu = args.get_item().get_text();
                var claveCorreo = dataItem.getDataKeyValue("ClaveCorreo");

                if (nombreMenu == "Consultar") {
                    openRadWindow('pop_Administracion_Correos.aspx?Accion=3&ClaveCorreo=' + claveCorreo, "AdministrarCorreo", 1200, 825, true);
                    menu.hide();
                    args.set_cancel(true);
                } else if (nombreMenu == "Editar") {
                    openRadWindow('pop_Administracion_Correos.aspx?Accion=2&ClaveCorreo=' + claveCorreo, "AdministrarCorreo", 1200, 825, true);
                    args.set_cancel(true);
                }

            }

            function refreshGrid() {

                var masterTable = $find("<%= rgCorreos.ClientID %>").get_masterTableView();
                masterTable.rebind();

            }

            function GridResize() {
                const bodyheight = $(window).height() - 200;
                $find('<%= rgCorreos.ClientID%>').get_element().style.height = bodyheight + 'px';
            }
            $(document).ready(function () {//Espera accion en la pagina

                $(window).resize(function () {//cuando la accion cambio de tamaño
                    GridResize();
                });
            });
        </script>

    </telerik:RadScriptBlock>

    <div class="catalogo">

        <div class="titulo">
            Administración de correos
        </div>

        <div class="acciones">
            <telerik:RadButton ID="btnNuevo" runat="server" Width="32" Height="32" OnClientClicking="nuevo" ToolTip="Nuevo perfil" OnClick="btnNuevo_Click">
                <Image ImageUrl="App_Resources/Imagenes/Sitio/Nuevo.png" />
            </telerik:RadButton>
        </div>

        <div class="contenido">

            <input type="hidden" id="rowIndex" name="rowIndex" />

            <telerik:RadGrid ID="rgCorreos" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" AllowPaging="True" AllowFilteringByColumn="True" FilterItemStyle-HorizontalAlign="Center" PageSize="8" OnItemCreated="rgCorreos_ItemCreated" OnNeedDataSource="rgCorreos_NeedDataSource">
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView DataKeyNames="ClaveCorreo,Pie,CC,CCO,Archivos" ClientDataKeyNames="ClaveCorreo">
                     <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>
                    <Columns>

                        <telerik:GridNumericColumn DataField="ClaveCorreo" HeaderText="Clave" UniqueName="ClaveCorreo" AllowFiltering="false" FilterControlWidth="60px" >
                            <HeaderStyle Width="80px" />
                        </telerik:GridNumericColumn>

                        <telerik:GridBoundColumn FilterControlWidth="400px" DataField="Asunto" HeaderText="Asunto del correo" UniqueName="Asunto" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <HeaderStyle/>
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="300px" DataField="Titulo" HeaderText="Título" UniqueName="Titulo" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <HeaderStyle Width="340px" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn FilterControlWidth="180px" DataField="Subtitulo" HeaderText="Subtítulo" UniqueName="Subtitulo" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <HeaderStyle Width="250px" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="TipoCorreo" HeaderText="Tipo de correo" UniqueName="CTipoCorreo" AllowFiltering="false">
                           
                            <HeaderStyle Width="240px" />                    
                        </telerik:GridBoundColumn>

                    </Columns>
                </MasterTableView>

                <ClientSettings>
                    <ClientEvents OnRowContextMenu="RowContextMenu" OnGridCreated="GridResize"></ClientEvents>
                    <Selecting AllowRowSelect="true"></Selecting>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Height="35px" />
                <AlternatingItemStyle HorizontalAlign="Center" Height="35px" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" PageSizeLabelText="Registros por página" PagerTextFormat="{4} Página {0} de {1}, registros {2} a {3} de {5}" />           

            </telerik:RadGrid>

            <telerik:RadContextMenu ID="rmCorreo" runat="server" OnItemClick="rmCorreo_ItemClick"
                EnableRoundedCorners="true" EnableShadows="true" OnClientItemClicking="menu">
                <Items>
                    <telerik:RadMenuItem Text="Consultar">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Editar">
                    </telerik:RadMenuItem>
                </Items>
            </telerik:RadContextMenu>

        </div>

    </div>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000">
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
