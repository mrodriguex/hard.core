<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_VerDetalleNotificacion.aspx.cs" Inherits="pop_VerDetalleNotificacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Notificación</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/jquery-3.4.1.min.js"></script>
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="sManager" runat="server" />

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">
                function Salir(sender, args) {
                    getRadWindow().close(5);
                }
            </script>

            <style type="text/css">
                .imagen-container{
                    width: 100%;
                    display: flex;
                    justify-content: center;
                }

                .imagen-notificacion {
                    max-width: 670px;
                    max-height: 350px;
                }

                .text-center {
                    text-align: center;
                }

                .h-450px {
                    height: 410px
                }

                .texto-justificado {
                    text-align: justify;
                }
            </style>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnSalir">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="btnSalir" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div class="modal">
            <div class="contenido d-flex flex-column align-content-center hide h-450px" style="word-wrap: break-word;">
                <telerik:RadLabel ID="lblTitulo" runat="server" Text="--" CssClass="pb-2 text-center text-break" Width="100%" Font-Bold="true"></telerik:RadLabel>
                <hr style="height: 1px; border: none; background-color: #ccc;" />
                <telerik:RadLabel ID="lblDescripcion" runat="server" Text="--" CssClass="p-2 text-break texto-justificado" Width="100%"></telerik:RadLabel>
                <div class="imagen-container">
                    <telerik:RadBinaryImage ID="rbiImagenNotificacion" runat="server" AutoAdjustImageControlSize="true" ResizeMode="Fit" CssClass="imagen-notificacion" />
                </div>
            </div>

            <div style="text-align: center; padding-top: 22px; padding-bottom: 10px">
                <telerik:RadButton ID="btnSalir" runat="server" Text="Salir sin cambios" Width="160px" OnClick="btnSalir_Click" />
            </div>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" />
    </form>
</body>
</html>
