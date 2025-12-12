<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_AdministracionFlujoAutorizacion.aspx.cs" Inherits="frm_AdministracionFlujoAutorizacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgFlujoAutorizacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFlujoAutorizacion" LoadingPanelID="LoadingPanel"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">

            function GridResize() {
                const bodyheight = $(window).height() - 305;
                $find('<%= rgFlujoAutorizacion.ClientID%>').get_element().style.height = bodyheight + 'px';
            }
        </script>
        <style>
          
        </style>
    </telerik:RadScriptBlock>

    <div class="catalogo">
        <div class="barra">
            <div class="titulo">
                Flujo de autorización
            </div>
            <div class="acciones">

            </div>
        </div>
        &nbsp;

        <div class="contenido">
            <input type="hidden" id="rowIndex" name="rowIndex" />
            <telerik:RadGrid ID="rgFlujoAutorizacion" runat="server" AutoGenerateColumns="False" Culture="es-ES" OnNeedDataSource="rgFlujoAutorizacion_NeedDataSource" OnItemCreated="rgFlujoAutorizacion_ItemCreated" AllowPaging="True" AllowFilteringByColumn="True" FilterItemStyle-VerticalAlign="Middle" FilterItemStyle-HorizontalAlign="Center">
                <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="False"></GroupingSettings>
                <MasterTableView >
                    <NoRecordsTemplate>No hay registros para mostrar</NoRecordsTemplate>
                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>

                        <telerik:GridNumericColumn DataField="ClaveFlujoAutorizacion" FilterControlAltText="Filter ClaveUsuario column" HeaderText="Clave" ReadOnly="True" ShowNoSortIcon="False" SortExpression="ClaveFlujoAutorizacion" UniqueName="ClaveFlujoAutorizacion" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" FilterControlWidth="90%">
                       <HeaderStyle Width="25px" />

                        </telerik:GridNumericColumn>

                        <telerik:GridBoundColumn DataField="Puesto" FilterControlAltText="Filter NombreUsuario column"  HeaderText="Puesto" ReadOnly="True" ShowNoSortIcon="False" SortExpression="Descripcion" UniqueName="Descripcion" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="90%">
                             <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>

                        
                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter NombreUsuario column" HeaderText="Nombre" ReadOnly="True" ShowNoSortIcon="False" SortExpression="Nombre" UniqueName="Nombre" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="90%" Visible="false">
                                                    <HeaderStyle Width="100px" />

                            </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn DataField="Correo" FilterControlAltText="Filter NombreUsuario column" HeaderText="Correo" ReadOnly="True" ShowNoSortIcon="False" SortExpression="Correo" UniqueName="Correo" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="90%" Visible="false">
                                                     <HeaderStyle Width="60px" />

                         </telerik:GridBoundColumn>

                        <telerik:GridNumericColumn DataField="ClaveEstatus" HeaderText="Orden" UniqueName="Orden"  DecimalDigits="0" ReadOnly="True" AllowFiltering="false">
                                                     <HeaderStyle Width="25px" />

                        </telerik:GridNumericColumn>

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