<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionCarrusel.aspx.cs" Inherits="frm_AdministracionCarrusel" %>

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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">
            function nuevo(sender, args) {
                openRadWindow('pop_Administracion_UltimasNoticias.aspx?Accion=1', "Carrusel", 820, 550, true);
                args.set_cancel(true);
            }

            function EditarAviso(sender, args) {
                var button = sender;
                var claveAviso = button.get_commandArgument();
                var rutaImagen = button.get_element().getAttribute("data-rutaimagen");

                var url = 'pop_Administracion_UltimasNoticias.aspx?Accion=2&ClaveAviso=' + encodeURIComponent(claveAviso)
                    + '&RutaImagen=' + encodeURIComponent(rutaImagen);

                openRadWindow(url, "Carrusel", 820, 550, true);
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
                openRadWindow(url, "Carrusel", 1000, 530, true);
            }

            function confirmarEliminar(sender, args) {
                var confirmado = confirm("¿Está seguro de que desea eliminar este elemento?");
                if (!confirmado) {
                    args.set_cancel(true);
                }
            }
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
                Administración de aviso
            </div>

            <div class="acciones">
                <telerik:RadButton ID="btnNuevo" runat="server" Width="32px" Height="32px" ToolTip="Nuevo" OnClientClicking="nuevo" OnClick="btnNuevo_Click" CssClass="btn-icono">
                <Image ImageUrl="App_Resources/Imagenes/Sitio/Nuevo.png" />
                </telerik:RadButton>
            </div>
        </div>

        <div class="contenido">

            <telerik:RadGrid ID="rgAvisoCarrusel" runat="server" AutoGenerateColumns="False" AllowPaging="True" Culture="es-ES" AllowFilteringByColumn="True" FilterItemStyle-HorizontalAlign="Center" FilterItemStyle-VerticalAlign="Middle" OnNeedDataSource="rgContactosCliente_NeedDataSource" OnItemCreated="rgAvisoCarrusel_ItemCreated" OnItemDataBound="rgAvisoCarrusel_ItemDataBound">
                <MasterTableView EditMode="InPlace" DataKeyNames="ClaveAviso,rutaImagen" ClientDataKeyNames="ClaveAviso">
                    <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>
                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ClaveAviso" HeaderText="ClaveAviso" HeaderStyle-Width="250px" FilterControlWidth="160px" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" AllowFiltering="true" Visible="false">
                            <ItemTemplate>
                                <div class="d-flex flex-column">
                                    <h5 class="text-dark mb-1"><%# Eval("ClaveAviso") %></h5>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="Titulo" UniqueName="Titulo" HeaderText="Titulo" HeaderStyle-Width="250px" FilterControlWidth="160px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" AllowFiltering="true">
                            <ItemTemplate>
                                <div class="d-flex flex-column">
                                    <h6 class="text-dark mb-1"><%# Eval("Titulo") %></h6>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" HeaderStyle-Width="500px" FilterControlWidth="370px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" AllowFiltering="true">
                            <ItemTemplate>
                                <div class="d-flex flex-column">
                                    <h6 class="text-dark mb-1"><%# Eval("Descripcion") %></h6>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="FechaInicial" HeaderText="Fecha de inicio" UniqueName="FechaInicial" FilterControlWidth="114px"
                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                            <HeaderStyle Width="140px" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="FechaFinal" HeaderText="Fecha de fin" UniqueName="FechaFinal" FilterControlWidth="114px"
                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                            <HeaderStyle Width="140px" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridTemplateColumn UniqueName="rutaImagen" Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfRutaImagen" runat="server" Value='<%# Eval("rutaImagen") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="Visible" DataType="System.Boolean" HeaderText="Visible" SortExpression="estatus" UniqueName="Cestatus" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <ItemTemplate>
                                <asp:Image ID="imgEstatus" runat="server" ImageUrl='<%# "~/App_Resources/Imagenes/Sitio/" + ((bool)Eval("Visible") ? "Activo" : "Inactivo") + ".png" %>' ToolTip='<%# ((bool)Eval("Visible") ? "Usuario activo" : "Usuario inactivo") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="90px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="FotoCol" HeaderText="Vista previa" HeaderStyle-Width="90px" AllowFiltering="false">
                            <ItemTemplate>
                                <div class="text-center">
                                    <a href="javascript:void(0);" onclick="verFotoAvisoCarrusel('<%# Eval("ClaveAviso") %>')" data-bs-toggle="tooltip"
                                        title="Vista previa">
                                        <svg xmlns="http://www.w3.org/2000/svg" height="60" width="37" viewBox="0 0 640 640">
                                            <!--!Font Awesome Free v7.1.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.-->
                                            <path fill="#35659d" d="M160 144C151.2 144 144 151.2 144 160L144 480C144 488.8 151.2 496 160 496L480 496C488.8 496 496 488.8 496 480L496 160C496 151.2 488.8 144 480 144L160 144zM96 160C96 124.7 124.7 96 160 96L480 96C515.3 96 544 124.7 544 160L544 480C544 515.3 515.3 544 480 544L160 544C124.7 544 96 515.3 96 480L96 160zM224 192C241.7 192 256 206.3 256 224C256 241.7 241.7 256 224 256C206.3 256 192 241.7 192 224C192 206.3 206.3 192 224 192zM360 264C368.5 264 376.4 268.5 380.7 275.8L460.7 411.8C465.1 419.2 465.1 428.4 460.8 435.9C456.5 443.4 448.6 448 440 448L200 448C191.1 448 182.8 443 178.7 435.1C174.6 427.2 175.2 417.6 180.3 410.3L236.3 330.3C240.8 323.9 248.1 320.1 256 320.1C263.9 320.1 271.2 323.9 275.7 330.3L292.9 354.9L339.4 275.9C343.7 268.6 351.6 264.1 360.1 264.1z" />
                                        </svg>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="90px" AllowFiltering="false">
                            <ItemTemplate>
                                <div class="d-flex justify-content-center gap-2">
                                    <telerik:RadButton ID="btnEditar" runat="server"
                                        Width="24px" Height="24px"
                                        CommandName="Editar"
                                        CommandArgument='<%# (Container as GridDataItem).GetDataKeyValue("ClaveAviso") %>'
                                        data-rutaimagen='<%# Eval("rutaImagen") %>'
                                        ToolTip="Editar"
                                        OnClientClicking="EditarAviso">
                                        <Image ImageUrl="App_Resources/Imagenes/Pagina/pen-to-square-regular-full.svg" />
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btnBorrar" runat="server"
                                        Width="22px" Height="24px"
                                        CommandName="Borrar"
                                        CommandArgument='<%# (Container as GridDataItem).GetDataKeyValue("ClaveAviso") %>'
                                        ToolTip="Borrar" OnClick="btnBorrar_Click" OnClientClicking="confirmarEliminar">
                                        <Image ImageUrl="App_Resources/Imagenes/Pagina/trash-can-regular-full.svg" />

                                    </telerik:RadButton>

                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
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
    <telerik:RadWindowManager RenderMode="Lightweight" ID="rwManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true" CenterIfModal="true" Behaviors="Close, Reload" VisibleStatusbar="false" KeepInScreenBounds="true" Style="z-index: 7000" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
