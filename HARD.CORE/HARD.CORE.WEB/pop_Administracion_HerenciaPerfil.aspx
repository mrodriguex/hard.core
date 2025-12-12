<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Administracion_HerenciaPerfil.aspx.cs" Inherits="pop_Administracion_HerenciaPerfil" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Herencia de perfil</title>
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

                function seleccionarUsuario(sender, args) {

                    openRadWindow("pop_DirectorioUsuarios.aspx", "Directorio", 1080, 640, false);
                    args.set_cancel(true);

                }

                function validaRequeridos(sender, args) {

                    var txtNombreUsuario = $find('<%= txtNombreUsuario.ClientID%>');
                    var dpFechaInicial = $find('<%= dpFechaInicial.ClientID%>');
                    var dpFechaFinal = $find('<%= dpFechaFinal.ClientID%>');
                    var mensaje = "¿Se encuentra seguro de heredar su perfil de usuario?";

                    if (dpFechaInicial.get_selectedDate() == null) {
                        alert("Favor de seleccionar la fecha inicial");
                        dpFechaInicial.showPopup()
                        args.set_cancel(true);
                    } else if (dpFechaFinal.get_selectedDate() == null) {
                        alert("Favor de seleccionar la fecha final");
                        dpFechaFinal.showPopup()
                        args.set_cancel(true);
                    } else if (dpFechaFinal.get_selectedDate() < dpFechaInicial.get_selectedDate()) {
                        alert("La fecha de fin debe ser mayor a la fecha de inicio de la herencia del perfil");
                        args.set_cancel(true);
                    } else if (!validar_Textbox(txtNombreUsuario, "Favor de seleccionar el usuario al que desea heredar su perfil")) {
                        args.set_cancel(true);
                    } else if (!confirm(mensaje)) {
                        args.set_cancel(true);
                    }

                }

                function refreshControls(args) {

                    var usuario = args.split("|");
                    if (usuario !== 'undefined') {
                        var hdnClaveUsuario = document.getElementById("hdnClaveUsuario");
                        var txtNombreUsuario = $find('<%= txtNombreUsuario.ClientID%>');

                        hdnClaveUsuario.value = usuario[0];
                        txtNombreUsuario.set_value(usuario[1]);
                    }

                }

                function salir(sender, args) {

                    if (sender == null) {
                        getRadWindow().BrowserWindow.refreshGrid();
                    }

                    getRadWindow().close();

                }

            </script>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnDirectorioActivo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtNombreUsuario" />
                        <telerik:AjaxUpdatedControl ControlID="hdnClaveUsuario" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnHeredarPerfil">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtNombreUsuario" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div class="modal" style="width: 650px">

            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Heredar mi perfil de usuario"></asp:Label>
            </div>

            <div class="contenido">
                <table>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblFechaInicio" AssociatedControlID="dpFechaInicial" runat="server" Text="Fecha de inicio :" Width="120px"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="dpFechaInicial" runat="server" Width="120px">
                                <DatePopupButton ToolTip="Seleccionar fecha de inicio" />
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <telerik:RadLabel ID="lblFechaFin" AssociatedControlID="dpFechaFinal" runat="server" Text="Fecha de fin :" Width="100px"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="120px">
                                <DatePopupButton ToolTip="Seleccionar fecha de fin" />
                            </telerik:RadDatePicker>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblNombreUsuario" AssociatedControlID="txtNombreUsuario" runat="server" Text="Usuario :" Width="100px"></telerik:RadLabel>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txtNombreUsuario" runat="server" ReadOnly="True" Width="358px">
                            </telerik:RadTextBox>
                        </td>
                        <td style="padding-left: 5px">
                            <telerik:RadButton ID="btnDirectorioActivo" runat="server" Text="Directorio" OnClientClicking="seleccionarUsuario">
                                <Icon PrimaryIconUrl="~/App_Resources/Imagenes/Sitio/Usuario.png" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div class="Nota" style="width: 420px; margin-top: 15px;">
                                <b>Nota : </b>La herencia de perfil permitirá que el usuario de tu elección tenga acceso a todas tus funciones por un tiempo determinado.
                            </div>
                        </td>
                    </tr>
                </table>

            </div>

            <asp:HiddenField ID="hdnClaveUsuario" runat="server" Value="" />
            <div style="margin: auto; width: 300px; margin-top: 20px">
                <telerik:RadButton ID="btnCancelar" runat="server" Text="Salir sin cambios" Width="140px" OnClientClicking="salir" />
                <telerik:RadButton ID="btnHeredarPerfil" runat="server" Text="Heredar perfil" Width="140px" OnClientClicking="validaRequeridos" OnClick="btnHeredarPerfil_Click" CssClass="rbPrimaryButton" />
            </div>

        </div>

    </form>
</body>
</html>
