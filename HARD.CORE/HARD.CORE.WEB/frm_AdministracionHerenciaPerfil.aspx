<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionHerenciaPerfil.aspx.cs" Inherits="frm_AdministracionHerenciaPerfil" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgHerenciaPerfil" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRecargar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgHerenciaPerfil" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgHerenciaPerfil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgHerenciaPerfil" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rmHerencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgHerenciaPerfil" LoadingPanelID="LoadingPanel" />
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

            function nuevo(sender, args) {

                openRadWindow("pop_Administracion_HerenciaPerfil.aspx?Accion=1", "Herencia", 820, 480, true);
                args.set_cancel(true);

            }

            function RowContextMenu(sender, eventArgs) {

                var menu = $find("<%= rmHerencia.ClientID%>");
                var items = menu.get_items();
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("rowIndex").value = index;

                var dataItem = sender.get_masterTableView().get_dataItems()[index];
                sender.get_masterTableView().selectItem(dataItem.get_element(), true);

                var claveEstatus = dataItem.getDataKeyValue("ClaveEstatus");
                var claveUsuarioPorAusencia = document.getElementById('<%= hfClaveUsuarioPorAusencia.ClientID %>').value;

                //items.getItem(0).set_visible(false);
                //items.getItem(1).set_visible(false);
                //items.getItem(2).set_visible(false);

                //if (claveUsuarioPorAusencia == "") {
                //    if (claveEstatus == "1") {
                //        items.getItem(0).set_visible(true);

                //    } else if (claveEstatus == "12") {
                //        items.getItem(1).set_visible(true);
                //        items.getItem(2).set_visible(true);

                //    }
                //}

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }

            }

            function menu(sender, args) {
                var menu = $find("<%= rmHerencia.ClientID%>");
                var rowIndex = document.getElementById("rowIndex").value
                var rgHerenciaPerfil = $find('<%= rgHerenciaPerfil.ClientID%>');

                var dataItem = rgHerenciaPerfil.get_masterTableView().get_dataItems()[rowIndex];
                var claveHerenciaPerfil = dataItem.getDataKeyValue("ClaveHerenciaPerfil");

                if (args.get_item().get_text() == "Editar herencia") {
                    openRadWindow("pop_Administracion_HerenciaPerfil.aspx?Accion=2&ClaveHerenciaPerfil=" + claveHerenciaPerfil, "Herencia", 820, 480, true);
                    menu.hide();
                    args.set_cancel(true);

                } else if (args.get_item().get_text() == "Histórico de herencia") {
                    openRadWindow("pop_Administracion_HerenciaPerfil_Historico.aspx?ClaveHerenciaPerfil=" + claveHerenciaPerfil, "HistoricoHerencia", 920, 560, true);
                    menu.hide();
                    args.set_cancel(true);

                }
                return false;

            }

            function refreshGrid() {

                var masterTable = $find("<%= rgHerenciaPerfil.ClientID %>").get_masterTableView();
                masterTable.rebind();

            }

            function OnClientClose(sender, args) {

                var arg = args.get_argument();
                if (arg) {

                    if (typeof arg === 'string') {
                        if (sender.get_navigateUrl() == "pop_DirectorioUsuarios.aspx") {
                            var windowManager = GetRadWindowManager();
                            var eWindow = windowManager.getWindowById("Herencia");
                            eWindow.get_contentFrame().contentWindow.refreshControls(arg);
                        }
                    }

                }

            }

            function GridResize() {
                const bodyheight = $(window).height() - 305;
                $find('<%= rgHerenciaPerfil.ClientID%>').get_element().style.height = bodyheight + 'px';
            }
        </script>
    </telerik:RadScriptBlock>

    <div class="catalogo">

        <div class="barra">
            <div class="titulo">
                Administración herencia de perfil
            </div>

            <div class="acciones">
                <telerik:RadButton
                    ID="RadButton1"
                    runat="server"
                    Width="32px"
                    Height="32px"
                    ToolTip="Nuevo perfil"
                    OnClientClicking="nuevo"
                    OnClick="btnNuevo_Click"
                    CssClass="btn-icono">
                    <Image ImageUrl="App_Resources/Imagenes/Sitio/Nuevo.png" />
                </telerik:RadButton>


            </div>

        </div>

        <div class="contenido">

            <input type="hidden" id="rowIndex" name="rowIndex" />
            <asp:HiddenField runat="server" ID="hfClaveUsuarioPorAusencia" Value="" />



            <telerik:RadGrid ID="rgHerenciaPerfil" runat="server" AutoGenerateColumns="False" Culture="es-ES" AllowPaging="True" AllowFilteringByColumn="true" FilterItemStyle-HorizontalAlign="Center" FilterItemStyle-VerticalAlign="Middle" OnNeedDataSource="rgHerenciaPerfil_NeedDataSource" OnItemCreated="rgHerenciaPerfil_ItemCreated">




                <GroupingSettings CaseSensitive="False" />
                <MasterTableView ClientDataKeyNames="ClaveHerenciaPerfil,ClaveUsuarioHeredado,FechaFinal,FechaInicial" DataKeyNames="ClaveHerenciaPerfil">
                    <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>

                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridNumericColumn DataField="ClaveHerenciaPerfil" HeaderText="Clave" UniqueName="ClaveHerenciaPerfil" ReadOnly="True" DecimalDigits="0" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" FilterControlWidth="80px">
                            <HeaderStyle Width="120px" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="ClaveUsuario" HeaderText="Usuario que hereda" SortExpression="UsuarioHereda" UniqueName="UsuarioHereda"
                            FilterControlWidth="180px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" Visible="true">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="UsuarioHeredado" HeaderText="Usuario Heredado" SortExpression="UsuarioHeredado" UniqueName="UsuarioHeredado"
                             FilterControlWidth="180px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" Visible="true">
                             <HeaderStyle Width="120px" />
                         </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="ClaveUsuarioHeredado" HeaderText="Usuario Heredado" UniqueName="ClaveUsuarioHeredado" ReadOnly="True" DecimalDigits="0"
                            AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" FilterControlWidth="80px" Visible="false">
                            <HeaderStyle Width="120px" />
                        </telerik:GridNumericColumn>
                        <telerik:GridDateTimeColumn DataField="FechaInicial" HeaderText="Fecha de inicio" UniqueName="FechaInicial" FilterControlWidth="114px"
                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                            <HeaderStyle Width="140px" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="FechaFinal" HeaderText="Fecha de fin" UniqueName="FechaFinal" FilterControlWidth="114px"
                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                            <HeaderStyle Width="140px" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="FechaUltimaActualizacion" HeaderText="Fecha de modificación" UniqueName="FechaUltimoMovimiento" FilterControlWidth="114px"
                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                            <HeaderStyle Width="140px" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridTemplateColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus" AllowFiltering="false">
                            <EditItemTemplate>
                                <asp:CheckBox ID="cbEstatus" runat="server" Checked='<%# Eval("Estatus") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                 <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/App_Resources/Imagenes/Sitio/" + ((bool)Eval("Estatus") ? "Activo" : "Inactivo") + ".png" %>' ToolTip='<%# ((bool)Eval("Estatus") ? "Herencia de perfil activa" : "Herencia de perfil inactiva") %>' />

                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                        </telerik:GridTemplateColumn>
                       <%-- <telerik:GridBoundColumn DataField="ClaveEstatus" HeaderText="" UniqueName="ClaveEstatus" FilterControlWidth="114px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" Visible="false">
                            <HeaderStyle Width="100px" />
                        </telerik:GridBoundColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents OnRowContextMenu="RowContextMenu" OnGridCreated="GridResize" />



                    <Selecting AllowRowSelect="true"></Selecting>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>

                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Height="35px" />
                <AlternatingItemStyle HorizontalAlign="Center" Height="35px" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" PageSizeLabelText="Registros por página" PagerTextFormat="{4} Página {0} de {1}, registros {2} a {3} de {5}" />
            </telerik:RadGrid>

            <telerik:RadContextMenu ID="rmHerencia" runat="server" OnItemClick="rmHerencia_ItemClick"
                EnableRoundedCorners="true" EnableShadows="true" OnClientItemClicking="menu">
                <Items>
                    <telerik:RadMenuItem Text="Desactivar herencia">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Cancelar herencia">
                    </telerik:RadMenuItem>
                    <%--<telerik:RadMenuItem Text="Editar herencia">
                    </telerik:RadMenuItem>--%>
                   <%-- <telerik:RadMenuItem Text="Histórico de herencia">
                    </telerik:RadMenuItem>--%>
                </Items>
            </telerik:RadContextMenu>

        </div>

    </div>


    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" />

</asp:Content>

