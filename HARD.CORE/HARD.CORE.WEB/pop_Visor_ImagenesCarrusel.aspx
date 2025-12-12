<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Visor_ImagenesCarrusel.aspx.cs" Inherits="pop_Visor_ImagenesCarrusel" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Aviso carrusel</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/jquery-3.1.1.min.js"></script>
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="sManager" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">   
            </script>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="modal" style="width: auto;">
<%--            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Aviso carrusel"></asp:Label>
            </div>--%>
            <div class="contenido">
                <div style="display: flex; justify-content: center; align-items: center; height: 90%;">
                    <asp:Image ID="imgVisualizar" runat="server" Width="500px" Height="400px" />
                </div>
            </div>
        </div>
        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
