<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionBitacoraAcceso.aspx.cs" Inherits="frm_AdministracionBitacoraAcceso" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgIngresos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgIngresos" LoadingPanelID="LoadingPanel"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">

           <%-- function RowContextMenu(sender, eventArgs) {
                var menu = $find("<%= rmIngresos.ClientID%>");
                var items = menu.get_items();
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("rowIndex").value = index;

                var dataItem = sender.get_masterTableView().get_dataItems()[index];
                sender.get_masterTableView().selectItem(dataItem.get_element(), true);

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }--%>

           <%-- function menu(sender, args) {
                var rowIndex = document.getElementById("rowIndex").value
                var rgIngresos = $find('<%= rgIngresos.ClientID%>');

                var dataItem = rgIngresos.get_masterTableView().get_dataItems()[rowIndex];
                var claveUsuario = dataItem.getDataKeyValue("ClaveUsuario")

                if (args.get_item().get_text() == "Detalle") {
                    openRadWindow('pop_Administracion_Ingresos.aspx?ClaveUsuario=' + claveUsuario, "Detalle", 700, 450, true)
                }

                return false;
            }--%>

            //function OnClientClose(sender, args) {
            //    var arg = args.get_argument();

            //    if (arg) {

            //        if (sender.get_name() == "Detalle") {

            //            var windowManager = GetRadWindowManager();
            //            var eWindow = windowManager.getWindowById("Ingresos");
            //            eWindow.get_contentFrame().contentWindow.refreshControls();

            //        }
            //    }
            //}

            function GridResize() {
                const bodyheight = $(window).height() - 305;
                $find('<%= rgIngresos.ClientID%>').get_element().style.height = bodyheight + 'px';
            }
        </script>
        <style>
            .RadButton {
                background-color: transparent !important;
                border: none !important;
                box-shadow: none !important;
            }
        </style>
    </telerik:RadScriptBlock>

    <div class="catalogo">
        <div class="barra">
            <div class="titulo">
                Administración bitácora ingresos
            </div>
            <div class="acciones">
                <telerik:RadButton ID="btnExportToExcel" runat="server" OnClick="ExportarAExcell_Click" ToolTip="Exportar a Excel" Width="24px" Height="24px">
                    <Image ImageUrl="App_Resources/Imagenes/Pagina/Accion_Descargar.svg" />
                </telerik:RadButton>
            </div>
        </div>

        <div class="contenido">
            <input type="hidden" id="rowIndex" name="rowIndex" />
            <telerik:RadGrid ID="rgIngresos" runat="server" AutoGenerateColumns="False" Culture="es-ES" FilterItemStyle-HorizontalAlign="Center" OnNeedDataSource="rgIngresos_NeedDataSource" OnItemCreated="rgIngresos_ItemCreated" AllowPaging="True" AllowFilteringByColumn="True" FilterItemStyle-VerticalAlign="Middle">
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView DataKeyNames="ClaveUsuario" ClientDataKeyNames="ClaveUsuario">
                    <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>
                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>

                        <telerik:GridBoundColumn DataField="ClaveUsuario" FilterControlAltText="Filter ClaveUsuario column" HeaderText="Clave usuario" ItemStyle-Width="60px" ReadOnly="True" ShowNoSortIcon="False" SortExpression="Telefono" UniqueName="CClaveUsuario" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="90%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="NombreUsuario" FilterControlAltText="Filter NombreUsuario column" ItemStyle-Width="60px" HeaderText="Nombre usuario" ReadOnly="True" ShowNoSortIcon="False" SortExpression="NombreUsuario" UniqueName="CNombreUsuario" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="90%">
                        </telerik:GridBoundColumn>

                        <telerik:GridNumericColumn DataField="NumeroIngresos" HeaderText="No. ingresos" UniqueName="CNumeroIngresos" ItemStyle-Width="25px" DecimalDigits="0" ReadOnly="True" AllowFiltering="false">
                        </telerik:GridNumericColumn>

                        <telerik:GridDateTimeColumn AllowFiltering="False" ItemStyle-Width="70px" DataField="UltimaConexion" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter UltimaConexion column" HeaderText="Ultima conexión" ReadOnly="True" SortExpression="UltimaConexion" UniqueName="UltimaConexion">
                        </telerik:GridDateTimeColumn>

                    </Columns>

                    <EditFormSettings>
                        <EditColumn ShowNoSortIcon="False"></EditColumn>
                    </EditFormSettings>

                </MasterTableView>

                <ClientSettings>
                    <ClientEvents OnGridCreated="GridResize"></ClientEvents>
                    <Selecting AllowRowSelect="true"></Selecting>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>

                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Height="35px" />
                <AlternatingItemStyle HorizontalAlign="Center" Height="35px" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" PageSizeLabelText="Registros por página" PagerTextFormat="{4} Página {0} de {1}, registros {2} a {3} de {5}" />

            </telerik:RadGrid>
        </div>
    </div>
    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>