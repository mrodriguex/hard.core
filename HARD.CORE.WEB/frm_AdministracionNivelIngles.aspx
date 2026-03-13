<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionNivelIngles.aspx.cs" Inherits="frm_AdministracionNivelIngles" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="frm_AdministracionNivelIngles" ContentPlaceHolderID="contenido" runat="Server">
    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNivelIngles" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgNivelIngles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNivelIngles" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNivelIngles" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">
            function nuevo(sender, args) {
                openRadWindow('pop_Administracion_NivelIngles.aspx?Accion=1', "Ingles", 850, 440, true);
                args.set_cancel(true);
            }

            function EditarNivelIngles(sender, args) {
                var button = sender;
                var ClaveNivelIngles = button.get_commandArgument();

                var url = 'pop_Administracion_NivelIngles.aspx?Accion=2&ClaveNivelIngles=' + encodeURIComponent(ClaveNivelIngles);

                openRadWindow(url, "Ingles", 850, 440, true);
                args.set_cancel(true);
            }

            function OnClientClose(sender, args) {
                var arg = args.get_argument();

                if (arg) {

                    if (sender.get_name() == "Edición") {

                        var windowManager = GetRadWindowManager();
                        var eWindow = windowManager.getWindowById("Ingles");
                        eWindow.get_contentFrame().contentWindow.refreshControls();

                    }
                }
            }

            function refreshGrid() {
                var masterTable = $find("<%= rgNivelIngles.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function GridResize() {
                const bodyheight = $(window).height() - 200;
                $find('<%= rgNivelIngles.ClientID%>').get_element().style.height = bodyheight + 'px';
            }
            $(document).ready(function () {//Espera accion en la pagina

                $(window).resize(function () {//cuando la accion cambio de tamaño
                    GridResize();
                });
            });

            //function confirmarEliminar(sender, args) {
            //    var confirmado = confirm("¿Está seguro de que desea eliminar este elemento?");
            //    if (!confirmado) {
            //        args.set_cancel(true);
            //    }
            //}
        </script>
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
    </telerik:RadScriptBlock>

    <div class="catalogo">
        <div class="barra">
            <div class="titulo">
                Administración nivel de inglés
            </div>
            <div class="acciones">
                <telerik:RadButton ID="btnNuevo" runat="server" Width="32px" Height="32px" ToolTip="Nuevo" OnClientClicking="nuevo" OnClick="btnNuevo_Click" CssClass="btn-icono">
                  <Image ImageUrl="App_Resources/Imagenes/Sitio/Nuevo.png" />
                </telerik:RadButton>
            </div>
        </div>

        <div class="contenido">

            <telerik:RadGrid ID="rgNivelIngles" runat="server" AutoGenerateColumns="False" AllowPaging="True" Culture="es-ES" AllowFilteringByColumn="True" OnItemCreated="rgNivelIngles_ItemCreated" FilterItemStyle-HorizontalAlign="Center" FilterItemStyle-VerticalAlign="Middle" OnNeedDataSource="rgNivelIngles_NeedDataSource">
                <MasterTableView EditMode="InPlace" DataKeyNames="ClaveNivelIngles,Descripcion" ClientDataKeyNames="ClaveNivelIngles,Descripcion">
                    <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>

                    <Columns>
                        <telerik:GridNumericColumn DataField="ClaveNivelIngles" UniqueName="ClaveNivelIngles"
                            HeaderText="Clave" FilterControlWidth="90%"
                            AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo"
                            ShowFilterIcon="False" AllowFiltering="true" Visible="true">
                            <HeaderStyle Width="20px" />

                        </telerik:GridNumericColumn>

                        <telerik:GridBoundColumn DataField="Descripcion" UniqueName="Descripcion"
                            HeaderText="Título" FilterControlWidth="90%"
                            AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                            ShowFilterIcon="False" AllowFiltering="true">
                            <HeaderStyle Width="120px" />

                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn DataField="estatus" DataType="System.Boolean" HeaderText="Estatus" SortExpression="estatus" UniqueName="Cestatus" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <ItemTemplate>
                                <asp:Image ID="imgEstatus" runat="server" ImageUrl='<%# "~/App_Resources/Imagenes/Sitio/" + ((bool)Eval("Estatus") ? "Activo" : "Inactivo") + ".png" %>' ToolTip='<%# ((bool)Eval("Estatus") ? "Usuario activo" : "Usuario inactivo") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                            <ItemStyle Width="20px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Acciones" AllowFiltering="false">
                            <ItemTemplate>
                                <div class="d-flex justify-content-center gap-2">

                                    <telerik:RadButton ID="btnEditar" runat="server"
                                        Width="30px" Height="30px"
                                        CommandName="Editar"
                                        CommandArgument='<%# (Container as GridDataItem).GetDataKeyValue("ClaveNivelIngles") %>'
                                        ToolTip="Editar"
                                        OnClientClicking="EditarNivelIngles">
                                        <Image ImageUrl="App_Resources/Imagenes/Pagina/pen-to-square-solid-full-Grande.svg" />
                                    </telerik:RadButton>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                            <ItemStyle Width="20px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents OnGridCreated="GridResize"></ClientEvents>
                    <Selecting AllowRowSelect="true"></Selecting>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="300px" />
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Height="35px" />
                <AlternatingItemStyle HorizontalAlign="Center" Height="35px" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" PageSizeLabelText="Registros por página" PagerTextFormat="{4} Página {0} de {1}, registros {2} a {3} de {5}" />
            </telerik:RadGrid>
        </div>
    </div>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>