<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_DirectorioUsuarios.aspx.cs" Inherits="pop_DirectorioUsuarios" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Usuarios de sistema</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="sManager" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">

                function seleccionar(sender, args) {

                    var commandName = args.get_commandName();

                    if (commandName == "Seleccionar") {

                        var claveUsuario = '';
                        var nombreUsuario = '';

                        var index = args.get_commandArgument();
                        var grid = $find("<%=rgUsuarios.ClientID %>");
                        var item = grid.get_masterTableView().get_dataItems()[index];

                        if (item != null) {
                            claveUsuario = item.getDataKeyValue("ClaveUsuario");
                            nombreUsuario = item.getDataKeyValue("NombreCompleto");
                        }

                        getRadWindow().close(claveUsuario + '|' + nombreUsuario);

                    }

                }

                function seleccionarDC(sender, args) {

                    var claveUsuario = '';
                    var nombreUsuario = '';

                    if (args != null) {
                        claveUsuario = args.getDataKeyValue("ClaveUsuario");
                        nombreUsuario = args.getDataKeyValue("NombreCompleto");
                    }

                    getRadWindow().close(claveUsuario + '|' + nombreUsuario);

                }

            </script>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgUsuarios">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgUsuarios" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div class="general">

            <div class="modal" style="width: 920px">

                <div class="titulo">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Seleccionar usuario activo"></asp:Label>
                </div>

                <div class="contenidoSimple">

                    <telerik:RadGrid ID="rgUsuarios" runat="server" AllowFilteringByColumn="True" PageSize="7" AllowPaging="True" AutoGenerateColumns="False" GroupPanelPosition="Top" CellSpacing="-1" OnDataBound="rgUsuarios_DataBound" OnItemCreated="rgUsuarios_ItemCreated" OnNeedDataSource="rgUsuarios_NeedDataSource">
                        <GroupingSettings CaseSensitive="false" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ClaveUsuario,NombreCompleto">
                            <Columns>
                                <telerik:GridNumericColumn DataField="NumeroEmpleado" DecimalDigits="2" FilterControlWidth="70px" HeaderText="No. empleado" ReadOnly="True" SortExpression="NumeroEmpleado" UniqueName="NumeroEmpleado" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False">
                                    <HeaderStyle Width="140px" />
                                </telerik:GridNumericColumn>
                                <telerik:GridBoundColumn DataField="ClaveUsuario" FilterControlWidth="80px" HeaderText="Clave usuario" ReadOnly="True" SortExpression="ClaveUsuario" UniqueName="ClaveUsuario" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                    <HeaderStyle Width="140px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NombreCompleto" FilterControlWidth="360px" HeaderText="Nombre usuario" ReadOnly="True" SortExpression="NombreUsuario" UniqueName="NombreUsuario" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Seleccionar" ImageUrl="~/App_Resources/Imagenes/Sitio/Accion_Aceptar.svg" UniqueName="CAceptar" CommandName="Seleccionar">
                                    <HeaderStyle Width="80px" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <ClientEvents OnCommand="seleccionar" OnRowDblClick="seleccionarDC" />
                        </ClientSettings>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Height="35px" />
                        <AlternatingItemStyle HorizontalAlign="Center" Height="35px" />
                        <FilterItemStyle HorizontalAlign="Center" />
                        <PagerStyle Mode="NextPrevAndNumeric" PageButtonCount="5" />
                    </telerik:RadGrid>

                </div>

            </div>

        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>

    </form>

</body>
</html>
