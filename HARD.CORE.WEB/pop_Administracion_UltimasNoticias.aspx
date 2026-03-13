<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Administracion_UltimasNoticias.aspx.cs" Inherits="pop_Administracion_UltimasNoticias" %>

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
                function archivoSeleccionado(sender, args) {
                    archivoRemovido();

                    var file = args.get_fileInputField().files[0];

                    if (file.type.toLowerCase().includes('image')) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var img = document.getElementById('imgVistaPrevia');
                            img.src = e.target.result;
                            img.style.display = 'block';
                        };
                        reader.readAsDataURL(file);
                    }
                }

                function archivoRemovido(sender, args) {
                    var img = document.getElementById('imgVistaPrevia');
                    img.src = '';
                    img.style.display = 'none';
                }

                function validaInsercionUsuarioContacto(sender, args) {
                    var params = new URLSearchParams(window.location.search);

                    var accion = params.get('Accion');
                    mensaje = "¿Se encuentra seguro de insertar aviso?";

                    var txtTitulo = $find('<%= txtTitulo.ClientID%>');
                    var txtDescripcion = $find('<%= txtDescripcion.ClientID%>');
                    var cargaImagen = $find("RadAsyncUpload1");
                    var imgVistaPrevia = document.getElementById("imgVistaPrevia");
                    var dpFechaInicial = $find('<%= dpFechaInicial.ClientID%>');
                    var dpFechaFinal = $find('<%= dpFechaFinal.ClientID%>');

                    var imagenValidaCargada = imgVistaPrevia &&
                        imgVistaPrevia.src &&
                        (imgVistaPrevia.src.startsWith("data:image/png") || imgVistaPrevia.src.startsWith("data:image/jpeg"));
                    if (accion == "2") {
                        mensaje = "¿Se encuentra seguro de actualizar aviso?";
                    }

                    if (!validar_Textbox(txtTitulo, "Favor de ingresar el titulo")) {
                        args.set_cancel(true);
                    } else if (!validar_Textbox(txtDescripcion, "Favor de ingresar la descripción")) {
                        args.set_cancel(true);
                    }  else if (dpFechaInicial.get_selectedDate() == null) {
                        alert("Favor de seleccionar la fecha inicial");
                        dpFechaInicial.showPopup()
                        args.set_cancel(true);
                    } else if (dpFechaFinal.get_selectedDate() == null) {
                        alert("Favor de seleccionar la fecha final");
                        dpFechaFinal.showPopup()
                        args.set_cancel(true);
                    } else if (dpFechaFinal.get_selectedDate() == null) {
                        alert("Favor de seleccionar la fecha final");
                        dpFechaFinal.showPopup()
                        args.set_cancel(true);
                    } else if (dpFechaFinal.get_selectedDate() < dpFechaInicial.get_selectedDate()) {
                        alert("La fecha de fin debe ser mayor a la fecha de inicio");
                        args.set_cancel(true);
                    } else if (!imagenValidaCargada) {
                        alert("Favor de ingresar una imagen válida.");
                        args.set_cancel(true);
                    }
                }

                function salir(sender, args) {

                    if (sender == null) {
                        getRadWindow().BrowserWindow.refreshGrid();
                    }

                    getRadWindow().close(5);

                }

            </script>
            <style type="text/css">
                .RadUpload_Bootstrap .ruButton {
                    padding: 3px !important;
                    height: 25px !important;
                    width: 108px !important;
                    margin-left: 3px;
                }
            </style>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtTitulo" LoadingPanelID="LoadingPanel" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="txtSubTitulo" LoadingPanelID="LoadingPanel" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="txtDescripcion" LoadingPanelID="LoadingPanel" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" LoadingPanelID="LoadingPanel" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>


        <div class="modal" >

            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Administración carrusel"></asp:Label>
            </div>
            <div class="contenido">
                <table>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel5" AssociatedControlID="txtTitulo" runat="server"
                                Text='Titulo: <span style="color:red">*</span>' Width="100%" HtmlEncode="false" Visible="false" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtClaveAviso" runat="server" MaxLength="50" Width="100%" Visible="false" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel1" AssociatedControlID="txtTitulo" runat="server"
                                Text='Titulo: <span style="color:red">*</span>' Width="100%" HtmlEncode="false" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTitulo" runat="server" MaxLength="50" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel2" AssociatedControlID="txtDescripcion" runat="server"
                                Text='Descripción: <span style="color:red">*</span>' Width="100%" HtmlEncode="false" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="50" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="display: flex; align-items: center; gap: 20px;">

                                <div style="display: flex; align-items: center; gap: 6px;">
                                    <telerik:RadLabel ID="lblFechaInicio" AssociatedControlID="dpFechaInicial"
                                        runat="server" Text="Fecha de inicio :" />
                                    <telerik:RadDatePicker ID="dpFechaInicial" runat="server" Width="140px">
                                        <DatePopupButton ToolTip="Seleccionar fecha de inicio" />
                                    </telerik:RadDatePicker>
                                </div>

                                <div style="display: flex; align-items: center; gap: 6px;">
                                    <telerik:RadLabel ID="lblFechaFin" AssociatedControlID="dpFechaFinal"
                                        runat="server" Text="Fecha de fin :" />
                                    <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="140px">
                                        <DatePopupButton ToolTip="Seleccionar fecha de fin" />
                                    </telerik:RadDatePicker>
                                </div>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel3" AssociatedControlID="cbEstatus" runat="server"
                                Text="Visible:" HtmlEncode="false" />

                        </td>
                        <td>
                            <telerik:RadSwitch ID="cbEstatus" runat="server" AutoPostBack="false" Checked="true" CssClass="customIcons"></telerik:RadSwitch>
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblFoto" AssociatedControlID="txtFoto" runat="server" Text='Imagen: <span style="color:red">*</span>' />
                        </td>
                        <td>
                            <img id="imgVistaPrevia" runat="server" style="max-width: 100px; display: none; max-height: 70px; margin-right: 10px; margin-left: 4px; padding-top: 2px;" src="" />
                            <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" HideFileInput="true" Localization-Select="Adjuntar imagen" Localization-Remove="Remover imagen" OnClientFileSelected="archivoSeleccionado" OnClientFileUploadRemoved="archivoRemovido" OnFileUploaded="rauImagen_FileUploaded" AllowedFileExtensions="jpg,png" MaxFileSize="10485760" AutoAddFileInputs="false" Width="500px" Style="padding-left: 0px;">
                            </telerik:RadAsyncUpload>
                        </td>

                    </tr>
                </table>



            </div>
            <table style="width: 100%">
                <tr>
                    <td colspan="4" style="text-align: center; padding-top: 25px; padding-bottom: 10px">
                        <telerik:RadButton ID="btnSalir" runat="server" Text="Salir sin cambios" Width="140px" OnClientClicking="salir" />
                        &nbsp;
             <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="140px" SingleClick="true" OnClientClicking="validaInsercionUsuarioContacto" OnClick="btnGuardar_Click" CssClass="rbPrimaryButton" />
                    </td>
                </tr>
            </table>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>

