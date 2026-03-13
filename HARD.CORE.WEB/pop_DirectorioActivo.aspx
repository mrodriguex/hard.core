<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_DirectorioActivo.aspx.cs" Inherits="pop_DirectorioActivo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Directorio activo</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/jquery-3.4.1.min.js"></script>
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="sManager" runat="server">
        </asp:ScriptManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">
                function salir(sender, args) {
                    if (sender == null) {
                        getRadWindow().close(1);
                    }

                    getRadWindow().close();
                }
            </script>

            <style type="text/css">
                .groupNivel1 {
                    background-color: #006699 !important;
                    color: white !important;
                    font-weight: 600 !important;
                }
            </style>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgUsuarioLDAP">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgUsuarioLDAP" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div class="modal" style="width: 700px; z-index: 90000;">
            <div class="contenidoSimple">
                <telerik:RadGrid ID="rgUsuarioLDAP" runat="server" AllowFilteringByColumn="True" AllowPaging="True" PageSize="5" AutoGenerateColumns="False" OnDataBound="rgUsuarioLDAP_DataBound" OnNeedDataSource="rgUsuarioLDAP_NeedDataSource" OnItemCreated="rgUsuarioLDAP_ItemCreated" OnItemCommand="rgUsuarioLDAP_ItemCommand">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ClaveUsuario,Nombre,Apellidos,Correo,NumeroEmpleado" Width="100%">
                        <ColumnGroups>
                            <telerik:GridColumnGroup HeaderText="Directorio activo" Name="ActiveDirectory" HeaderStyle-CssClass="groupNivel1">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            </telerik:GridColumnGroup>
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ClaveUsuario" HeaderText="Clave usuario" SortExpression="ClaveUsuario" UniqueName="ClaveUsuario" ReadOnly="True" FilterControlWidth="80px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" ColumnGroupName="ActiveDirectory">
                                <HeaderStyle Width="120px" />
                            </telerik:GridBoundColumn>
                             <telerik:GridNumericColumn DataField="NumeroEmpleado" HeaderText="No. empleado" SortExpression="NumeroEmpleado" UniqueName="NumeroEmpleado" ReadOnly="True" FilterControlWidth="80px" DecimalDigits="2" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" ShowFilterIcon="False" ColumnGroupName="ActiveDirectory">
                                 <HeaderStyle Width="120px" />
                             </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" UniqueName="Nombre" ReadOnly="True" FilterControlWidth="180px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" ColumnGroupName="ActiveDirectory">
                                <HeaderStyle Width="220px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos" UniqueName="Apellidos" ReadOnly="True" FilterControlWidth="180px" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" ShowFilterIcon="False" ColumnGroupName="ActiveDirectory">
                                <HeaderStyle Width="220px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="~/App_Resources/Imagenes/Sitio/Accion_Aceptar.svg" UniqueName="CAceptar" CommandName="Seleccionar" ColumnGroupName="ActiveDirectory">
                                <HeaderStyle Width="60px" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Height="30px" />
                    <AlternatingItemStyle HorizontalAlign="Center" Height="30px" />
                    <FilterItemStyle HorizontalAlign="Center" />
                    <PagerStyle Mode="NextPrevAndNumeric" PageButtonCount="5" PagerTextFormat="{4} Pagína {0} de {1}" />
                </telerik:RadGrid>
            </div>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" />
    </form>
</body>
</html>
