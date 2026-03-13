<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_Administracion_Avisos.aspx.cs" Inherits="frm_Administracion_Avisos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="frm_Administracion_UltimasNoticias" ContentPlaceHolderID="contenido" runat="Server">
    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAvisoCarrusel" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAvisoCarrusel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAvisoCarrusel" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnEditar">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rgAvisoCarrusel" LoadingPanelID="LoadingPanel" />
        </UpdatedControls>
    </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">
            function nuevo(sender, args) {
                openRadWindow('pop_Administracion_UltimasNoticias.aspx?Accion=1', "Carrusel", 1050, 480, true);
                args.set_cancel(true);
            }

            function EditarClienteUsuario(sender, args) {
                var button = sender;
                var claveAviso = button.get_commandArgument();
                var rutaImagen = button.get_element().getAttribute("data-rutaimagen");

                var url = 'pop_Administracion_UltimasNoticias.aspx?Accion=2&ClaveAviso=' + encodeURIComponent(claveAviso)
                    + '&RutaImagen=' + encodeURIComponent(rutaImagen);

                openRadWindow(url, "Carrusel", 1050, 480, true);
                args.set_cancel(true);
            }

            function OnClientClose(sender, args) {
                var arg = args.get_argument();

                if (arg) {

                    if (sender.get_name() == "Edición") {

                        var windowManager = GetRadWindowManager();
                        var eWindow = windowManager.getWindowById("Cliente");
                        eWindow.get_contentFrame().contentWindow.refreshControls();

                    }
                }
            }

            function refreshGrid() {
                var masterTable = $find("<%= rgAvisoCarrusel.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function GridResize() {
                const bodyheight = $(window).height() - 300;
                $find('<%= rgAvisoCarrusel.ClientID%>').get_element().style.height = bodyheight + 'px';
            }

            function verFotoAvisoCarrusel(Id) {
                var url = 'pop_Visor_ImagenesCarrusel.aspx?clave=' + Id;
                openRadWindow(url, "Carrusel", 1000, 620, true);
            }

            function confirmarEliminar(sender, args) {
                var confirmado = confirm("¿Está seguro de que desea eliminar este elemento?");
                if (!confirmado) {
                    args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadScriptBlock>

    <div class="d-flex flex-column align-items-center justify-content-start" style="height: 50vh;">
        <div class="web-content w-100" style="max-width: 800px;">

            <div class="pt-3">

                <div class="mb-3 text-end">
                    <telerik:RadButton ID="btnNuevo" runat="server" Width="22px" Height="24px" ToolTip="Nuevo aviso" OnClick="btnNuevo_Click">
                        <Image ImageUrl="App_Resources/Imagenes/Sitio/plus-solid.svg" />
                    </telerik:RadButton>
                </div>

                <telerik:RadGrid ID="rgAvisoCarrusel" runat="server" AutoGenerateColumns="False" AllowPaging="True" Culture="es-ES" AllowFilteringByColumn="True" FilterItemStyle-HorizontalAlign="Center" FilterItemStyle-VerticalAlign="Middle" OnNeedDataSource="rgContactosCliente_NeedDataSource" OnItemCreated="rgAvisoCarrusel_ItemCreated" OnItemDataBound="rgAvisoCarrusel_ItemDataBound">
                    <MasterTableView EditMode="InPlace" DataKeyNames="ClaveAviso,rutaImagen" ClientDataKeyNames="ClaveAviso">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="ClaveAviso" HeaderText="ClaveAviso" HeaderStyle-Width="250px" FilterControlWidth="160px" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" AllowFiltering="true" Visible="false">
                                <ItemTemplate>
                                    <div class="d-flex flex-column">
                                        <h5 class="text-dark mb-1"><%# Eval("ClaveAviso") %></h5>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn DataField="Titulo" UniqueName="Titulo" HeaderText="Titulo" HeaderStyle-Width="250px" FilterControlWidth="160px" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" AllowFiltering="true">
                                <ItemTemplate>
                                    <div class="d-flex flex-column">
                                        <h6 class="text-dark mb-1"><%# Eval("Titulo") %></h6>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" HeaderStyle-Width="250px" FilterControlWidth="370px" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" AllowFiltering="true">
                                <ItemTemplate>
                                    <div class="d-flex flex-column">
                                        <h6 class="text-dark mb-1"><%# Eval("Descripcion") %></h6>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="rutaImagen" Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfRutaImagen" runat="server" Value='<%# Eval("rutaImagen") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn DataField="estatus" DataType="System.Boolean" HeaderText="Estatus" SortExpression="estatus" UniqueName="Cestatus" AllowFiltering="true" ShowFilterIcon="true" AutoPostBackOnFilter="true">
                                <ItemTemplate>
                                    <i id="imgActivo" runat="server" class='fa-solid fa-square-check fa-2xl' visible='<%# (bool)Eval("estatus") %>' style="color: forestgreen" />
                                    <i id="imgInactivo" runat="server" class='fa-regular fa-square-check fa-2xl' visible='<%# !(bool)Eval("estatus") %>' style="color: gray" />
                                </ItemTemplate>
                                <HeaderStyle Width="180px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="FotoCol" HeaderText="Vista previa" HeaderStyle-Width="100px" AllowFiltering="false">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <a href="javascript:void(0);" onclick="verFotoAvisoCarrusel('<%# Eval("ClaveAviso") %>')" data-bs-toggle="tooltip"
                                            title="Vista previa">
                                            <i class="fa-solid fa-image fa-2xl" style="color: #3f51b5;"></i>
                                        </a>

                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="150px" AllowFiltering="false">
                                <ItemTemplate>
                                    <div class="d-flex justify-content-center gap-2">
                                        <telerik:RadButton ID="btnEditar" runat="server"
                                            Width="24px" Height="24px"
                                            CommandName="Editar"
                                            CommandArgument='<%# (Container as GridDataItem).GetDataKeyValue("ClaveAviso") %>'
                                            data-rutaimagen='<%# Eval("rutaImagen") %>'
                                            ToolTip="Editar"
                                            OnClientClicking="EditarClienteUsuario">
                                            <Image ImageUrl="App_Resources/Imagenes/Sitio/pen-solid.svg" />
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btnBorrar" runat="server"
                                            Width="22px" Height="24px"
                                            CommandName="Borrar"
                                            CommandArgument='<%# (Container as GridDataItem).GetDataKeyValue("ClaveAviso") %>'
                                            ToolTip="Borrar" OnClick="btnBorrar_Click" OnClientClicking="confirmarEliminar">
                                            <Image ImageUrl="App_Resources/Imagenes/Sitio/trash-solid.svg" />

                                        </telerik:RadButton>

                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnGridCreated="GridResize"></ClientEvents>
                        <Selecting AllowRowSelect="true"></Selecting>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="300px" />
                    </ClientSettings>
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Height="50px" Font-Size="Larger" BackColor="#26669E" ForeColor="White" />
                    <ItemStyle HorizontalAlign="Center" Height="50px" />
                    <AlternatingItemStyle HorizontalAlign="Center" Height="50px" BackColor="WhiteSmoke" />
                    <FilterItemStyle HorizontalAlign="Center" />
                    <PagerStyle Mode="NextPrevAndNumeric" PageButtonCount="5" PageSizeLabelText="Registros por página" PagerTextFormat="{4} Página {0} de {1}, registros {2} a {3} de {5}" AlwaysVisible="true" />
                </telerik:RadGrid>
            </div>
        </div>
    </div>
    <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
