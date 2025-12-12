<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_AdministracionPerfiles.aspx.cs" Inherits="frm_AdministracionPerfiles" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPerfiles" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="rttPerfiles" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgPerfiles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPerfiles" UpdatePanelCssClass="" LoadingPanelID="LoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="rttPerfiles" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rmPerfiles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPerfiles" UpdatePanelCssClass="" />
                    <telerik:AjaxUpdatedControl ControlID="rttPerfiles" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <style>
            .btn-icono {
                background-color: transparent !important;
                border: none !important;
                box-shadow: none !important;
                padding: 0 !important;
            }

                .btn-icono i {
                    color: #314f7c;
                    font-size: 1.8rem; /* ajusta tamaño */
                    line-height: 1;
                }
        </style>
        <script type="text/javascript">
            function nuevo() {
                openRadWindow("pop_Administracion_Perfil.aspx?ClavePerfil=0", "AsignacionMenu", 880, 780, true)
            }

            function refreshGrid() {
                var masterTable = $find("<%= rgPerfiles.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function RowContextMenu(sender, eventArgs) {
                var menu = $find("<%= rmPerfiles.ClientID%>");
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
                var rowIndex = document.getElementById("rowIndex").value;
                var rgPerfiles = $find('<%= rgPerfiles.ClientID%>');
                var dataItem = rgPerfiles.get_masterTableView().get_dataItems()[rowIndex];

                var clavePerfil = dataItem.getDataKeyValue("ClavePerfil");
                var menu = $find("<%= rmPerfiles.ClientID%>");

                if (args.get_item().get_text() == "Edición") {
                    openRadWindow('pop_Administracion_Perfil.aspx?ClavePerfil=' + clavePerfil, "AsignacionMenu", 880, 780, true);
                }
                return false;
            }

            function OnClientClose(sender, args) {
                var arg = args.get_argument();

                if (arg) {

                    if (sender.get_name() == "Edición") {

                        var windowManager = GetRadWindowManager();
                        var eWindow = windowManager.getWindowById("AsignacionMenu");
                        eWindow.get_contentFrame().contentWindow.refreshControls();
                    }
                }
            }

            function GridResize() {
                const bodyheight = $(window).height() - 300;
                $find('<%= rgPerfiles.ClientID%>').get_element().style.height = bodyheight + 'px';
            }
        </script>
    </telerik:RadScriptBlock>

    <div class="catalogo">
        <div class="barra">
            <div class="titulo">
                Administración de perfiles
            </div>
            <div class="acciones">
                <telerik:RadButton ID="btnNuevo" runat="server" Width="32px" Height="32px" ToolTip="Nuevo perfil" OnClientClicking="nuevo" OnClick="btnNuevo_Click" CssClass="btn-icono">
                 <Image ImageUrl="App_Resources/Imagenes/Sitio/Nuevo.png" />
                </telerik:RadButton>
            </div>
        </div>

        <div class="contenido">

            <input type="hidden" id="rowIndex" name="rowIndex" />

            <telerik:RadGrid ID="rgPerfiles" runat="server" OnPreRender="rgPerfiles_PreRender" AutoGenerateColumns="False" Culture="es-ES" AllowPaging="True" AllowFilteringByColumn="true" FilterItemStyle-HorizontalAlign="Center" FilterItemStyle-VerticalAlign="Middle" OnNeedDataSource="rgPerfiles_NeedDataSource" OnDataBound="rgPerfiles_DataBound" OnItemDataBound="rgPerfiles_ItemDataBound" OnItemCreated="rgPerfiles_ItemCreated">

                <GroupingSettings CaseSensitive="False" />

                <MasterTableView DataKeyNames="ClavePerfil" ClientDataKeyNames="ClavePerfil">
                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>

                        <telerik:GridNumericColumn DataField="ClavePerfil" HeaderText="Clave perfil" UniqueName="ClavePerfil" DecimalDigits="0" ReadOnly="True" FilterControlWidth="80px" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" AllowFiltering="true">
                            <HeaderStyle Width="120px" />
                        </telerik:GridNumericColumn>

                        <telerik:GridTemplateColumn HeaderText="Menú" UniqueName="VistaPrevia" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="VistaPreviaMenu" runat="server" ImageUrl="~/App_Resources/Imagenes/Sitio/Consulta.png" />
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" UniqueName="Descripcion" FilterControlWidth="90%" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" AllowFiltering="true">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn DataField="Estatus" DataType="System.Boolean" HeaderText="Estatus" SortExpression="Estatus" UniqueName="CEstatus" AllowFiltering="True">
                            <ItemTemplate>
                                <asp:Image ID="imgEstatus" runat="server" ImageUrl='<%# "~/App_Resources/Imagenes/Sitio/" + ((bool)Eval("Estatus") ? "Activo" : "Inactivo") + ".png" %>' ToolTip='<%# ((bool)Eval("Estatus") ? "Usuario activo" : "Usuario inactivo") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="180px" />
                        </telerik:GridTemplateColumn>

                    </Columns>

                    <EditFormSettings>
                        <EditColumn ShowNoSortIcon="False"></EditColumn>
                    </EditFormSettings>
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

            <telerik:RadContextMenu ID="rmPerfiles" runat="server" OnItemClick="rmPerfiles_ItemClick"
                EnableRoundedCorners="true" EnableShadows="true" OnClientItemClicking="menu">
                <Items>
                    <telerik:RadMenuItem Text="Edición">
                    </telerik:RadMenuItem>

                </Items>
            </telerik:RadContextMenu>
        </div>
    </div>

    <telerik:RadToolTipManager ID="rttPerfiles" runat="server" Position="BottomRight" Skin="Default" RelativeTo="Element" Width="320px"
        Animation="Resize" OnAjaxUpdate="OnAjax" ShowEvent="OnClick" HideEvent="ManualClose" RenderInPageRoot="true">
    </telerik:RadToolTipManager>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
