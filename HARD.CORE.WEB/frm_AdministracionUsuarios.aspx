<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionUsuarios.aspx.cs" Inherits="frm_AdministracionUsuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Administracion_Usuarios" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUsuarios" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rmUsuarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUsuarios" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgUsuarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUsuarios" LoadingPanelID="LoadingPanel" />
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

                openRadWindow("pop_Administracion_Usuarios.aspx?Accion=1", "Usuarios", 900, 800, true);
                args.set_cancel(true);

            }

            function RowContextMenu(sender, eventArgs) {

                var menu = $find("<%= rmUsuarios.ClientID%>");
                var items = menu.get_items();
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("rowIndex").value = index;

                var dataItem = sender.get_masterTableView().get_dataItems()[index];
                sender.get_masterTableView().selectItem(dataItem.get_element(), true);

                if (dataItem.getDataKeyValue("Bloqueado") == "False") {
                    items.getItem(1).set_enabled(false);
                } else {
                    items.getItem(1).set_enabled(true);
                }

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }

            }

            function menu(sender, args) {

                var menu = $find("<%= rmUsuarios.ClientID%>");
                var rowIndex = document.getElementById("rowIndex").value
                var rgUsuarios = $find('<%= rgUsuarios.ClientID%>');

                var dataItem = rgUsuarios.get_masterTableView().get_dataItems()[rowIndex];
                var claveUsuario = dataItem.getDataKeyValue("ClaveUsuario")

                if (args.get_item().get_text() == "Edición") {

                    openRadWindow('pop_Administracion_Usuarios.aspx?ClaveUsuario=' + claveUsuario + '&Accion=2', 'Usuarios', 900, 800, true);
                    menu.hide();
                    args.set_cancel(true);

                }

                return false;

            }

            function refreshGrid() {

                var masterTable = $find("<%= rgUsuarios.ClientID %>").get_masterTableView();
                masterTable.rebind();

            }

            function OnClientClose(sender, args) {

                var arg = args.get_argument();

                if (arg) {

                    if (sender.get_name() == "DirectorioActivo") {

                        var windowManager = GetRadWindowManager();
                        var eWindow = windowManager.getWindowById("Usuarios");
                        eWindow.get_contentFrame().contentWindow.refreshControls();

                    }

                }

            }
            function GridResize() {
                const bodyheight = $(window).height() - 290;
                $find('<%= rgUsuarios.ClientID%>').get_element().style.height = bodyheight + 'px';
                }
        </script>

    </telerik:RadScriptBlock>

    <div class="catalogo">
    
          <div class="barra">
      <div class="titulo">
            Administración de usuarios
      </div>
      <div class="acciones">
          <telerik:RadButton
              ID="btnNuevo"
              runat="server"
              Width="32px"
              Height="32px"
              ToolTip="Nuevo usuario"
              OnClientClicking="nuevo"
              OnClick="btnNuevo_Click"
              CssClass="btn-icono">
              <Image ImageUrl="App_Resources/Imagenes/Sitio/Nuevo.png" />
          </telerik:RadButton>


      </div>
  </div>

        <div class="contenido">

            <input type="hidden" id="rowIndex" name="rowIndex" />

            <telerik:RadGrid ID="rgUsuarios" runat="server" AllowFilteringByColumn="True" AllowPaging="True" AutoGenerateColumns="False"
                OnDataBound="rgUsuarios_DataBound" OnNeedDataSource="rgUsuarios_NeedDataSource" OnItemCreated="rgUsuarios_ItemCreated"
                 FilterItemStyle-VerticalAlign="Middle" FilterItemStyle-HorizontalAlign="Center" OnPreRender="rgUsuarios_PreRender" >
                <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="False"></GroupingSettings>
                <MasterTableView DataKeyNames="ClaveUsuario" ClientDataKeyNames="ClaveUsuario,Bloqueado">
                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="ClaveUsuario" HeaderText="Clave usuario" SortExpression="ClaveUsuario" UniqueName="ClaveUsuario" ReadOnly="True" FilterControlWidth="80px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                            <HeaderStyle Width="100px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="NumeroEmpleado" HeaderText="No empleado" SortExpression="NumeroEmpleado" UniqueName="NumeroEmpleado" ReadOnly="True" FilterControlWidth="100px" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False">
                            <HeaderStyle Width="120px" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre usuario" SortExpression="NombreUsuario" UniqueName="NombreUsuario" ReadOnly="True" FilterControlWidth="380px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                              <HeaderStyle Width="400px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Correo" HeaderText="Correo" SortExpression="Correo" UniqueName="Correo" FilterControlWidth="240px" ReadOnly="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                            <HeaderStyle Width="260px" />
                        </telerik:GridBoundColumn>
                      <%--  <telerik:GridBoundColumn DataField="Ubicacion" HeaderText="Ubicación" SortExpression="Ubicacion" UniqueName="Ubicacion" FilterControlWidth="240px" ReadOnly="True" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                            <HeaderStyle Width="260px" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" UniqueName="Estatus">
                            <ItemTemplate>
                                <asp:Image ID="imgEstatus" runat="server" ImageUrl='<%# "~/App_Resources/Imagenes/Sitio/" + ((bool)Eval("Estatus") ? "Activo" : "Inactivo") + ".png" %>' ToolTip='<%# ((bool)Eval("Estatus") ? "Usuario activo" : "Usuario inactivo") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="90px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Cambio password" SortExpression="CambioPassword" UniqueName="CambioPassword" DataField="CambioContrasena">
                            <ItemTemplate>
                                <asp:Image ID="imgCambioPassword" runat="server" ImageUrl='<%# "~/App_Resources/Imagenes/Sitio/" + ((bool)Eval("CambioContrasena") ? "Activo" : "InactivoCP") + ".png" %>' ToolTip='<%# ((bool)Eval("CambioContrasena") ? "Cambio de password activo" : "Cambio de password inactivo") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="90px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Bloqueado" HeaderText="Bloqueo" SortExpression="Bloqueado" UniqueName="Bloqueado">
                            <ItemTemplate>
                                <asp:Image ID="imgBloqueo" runat="server" ImageUrl="~/App_Resources/Imagenes/Sitio/Bloqueado.png" Visible='<%# ((bool)Eval("Bloqueado") ? true : false) %>' ToolTip="Usuario bloqueado" />
                            </ItemTemplate>
                            <HeaderStyle Width="90px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>

                <ClientSettings>
                    <ClientEvents OnRowContextMenu="RowContextMenu" OnGridCreated="GridResize" ></ClientEvents>
                    <Selecting AllowRowSelect="true"></Selecting>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>

                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Height="35px" />
                <AlternatingItemStyle HorizontalAlign="Center" Height="35px" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" PageSizeLabelText="Registros por página" PagerTextFormat="{4} Página {0} de {1}, registros {2} a {3} de {5}" />

            </telerik:RadGrid>

            <telerik:RadContextMenu ID="rmUsuarios" runat="server" OnItemClick="rmUsuarios_ItemClick"
                EnableRoundedCorners="true" EnableShadows="true" OnClientItemClicking="menu">
                <Items>
                    <telerik:RadMenuItem Text="Edición">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Desbloquear usuario">
                    </telerik:RadMenuItem>
                </Items>
            </telerik:RadContextMenu>

        </div>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000" OnClientClose="OnClientClose">
        </telerik:RadWindowManager>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>

    </div>


</asp:Content>



