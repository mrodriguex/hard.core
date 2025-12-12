<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Administracion_Usuarios.aspx.cs" Inherits="pop_Administracion_Usuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Administración de usuarios</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/jquery-3.1.1.min.js"></script>
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
    
        <link href="App_Scripts/notiflix.css" rel="stylesheet" />
    <script src="App_Scripts/notiflix.js"></script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnGuardar">

        <asp:ScriptManager ID="sManager" runat="server">
        </asp:ScriptManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">

                function seleccionarUsuario(sender, args) {

                    openRadWindow("pop_DirectorioActivo.aspx", "DirectorioActivo", 1080, 600, false);
                    args.set_cancel(true);

                }

                function validaInsercionUsuario(sender, args) {

                    var txtClave = $find('<%= txtClave.ClientID%>');
                    var txtNombreUsuario = $find('<%= txtNombreUsuario.ClientID%>');
                    var txtNoEmpleado = $find('<%= txtNoEmpleado.ClientID%>');
                    var txtApellidoPaterno = $find('<%= txtApellidoPaterno.ClientID%>');
                    var txtApellidoMaterno = $find('<%= txtApellidoMaterno.ClientID%>');
                    var txtCorreo = $find('<%= txtCorreo.ClientID%>');
                    var rlbPerfilesAdd = $find('<%= rlbPerfilesAdd.ClientID%>');
                    var rcbEmpresa = $find('<%= rcbEmpresa.ClientID%>');
                    var seleccionados = rcbEmpresa.get_checkedItems();

                    var accion = queryString("Accion");
                    var mensaje = "Se encuentra seguro de ingresar usuario";

                    if (accion == 2) {
                        mensaje = "Se encuentra seguro de actualizar usuario";
                    }

                    var rblTipoUsuario = document.getElementById('<%= rblTipoUsuario.ClientID%>');

                    if (rblTipoUsuario != null) {
                        var inputs = rblTipoUsuario.getElementsByTagName('input');
                    }

                    var valida = false;
                    if (inputs != null) {
                        if (inputs[1].checked) {
                            valida = true;
                        }
                    }
                    //else if (!validar_Combo(rcbEmpresa, "0", "Favor de seleccionar una ubicación")) {
                    //    args.set_cancel(true);
                    //}
                    if (txtClave.get_value().trim() == "" && !valida) {
                        alert("Favor de seleccionar usuario de directorio activo");
                        document.getElementById("btnDirectorioActivo").focus();
                        args.set_cancel(true);
                    } else if (!validar_Textbox(txtNombreUsuario, "Favor de ingresar nombre del usuario")) {
                        args.set_cancel(true);
                    } else if (!validar_NumericTextbox(txtNoEmpleado, "Favor de ingresar número de empleado del usuario")) {
                        args.set_cancel(true);
                    } else if (!validar_Textbox(txtApellidoPaterno, "Favor de ingresar apellido paterno del usuario")) {
                        args.set_cancel(true);
                    } else if (!validar_TextboxC(txtApellidoMaterno, "Favor de ingresar apellido materno del usuario", valida)) {
                        args.set_cancel(true);
                    } else if (!validar_Textbox(txtCorreo, "Favor de ingresar dirección de correo electrónico del usuario")) {
                        args.set_cancel(true);
                    } else if (!validar_Correo(txtCorreo.get_value())) {
                        alert("Es necesario ingresar una dirección de correo válida");
                        txtCorreo.focus();
                        args.set_cancel(true);
                    } else if (seleccionados.length === 0) {
                        alert("Debe seleccionar una empresa.");
                        rcbEmpresa.showDropDown();
                        args.set_cancel(true);
                    } else if (rlbPerfilesAdd.get_items().get_count() == 0) {
                        alert("Debe asignar por lo menos un perfil al usuario");
                        args.set_cancel(true);
                    } else if (!confirm(mensaje)) {
                        args.set_cancel(true);
                    }

                }

                function refreshControls() {

                    var ajaxManager = $find("<%= raManager.ClientID %>");
                    ajaxManager.ajaxRequestWithTarget("<%= btnDirectorioActivo.UniqueID %>", "");

                }

                function salir(sender, args) {

                    if (sender == null) {
                        getRadWindow().BrowserWindow.refreshGrid();
                    }

                    getRadWindow().close(5);

                }

            </script>
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rblTipoUsuario">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rblTipoUsuario" />
                        <telerik:AjaxUpdatedControl ControlID="txtClave" />
                        <telerik:AjaxUpdatedControl ControlID="btnDirectorioActivo" />
                        <telerik:AjaxUpdatedControl ControlID="txtNombreUsuario" />
                        <telerik:AjaxUpdatedControl ControlID="txtNoEmpleado" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoPaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoMaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtCorreo" />
                        <telerik:AjaxUpdatedControl ControlID="rcbEmpresa" />
                        <telerik:AjaxUpdatedControl ControlID="cbEstatus" />
                        <telerik:AjaxUpdatedControl ControlID="cbCambioPassword" />
                        <telerik:AjaxUpdatedControl ControlID="rcbDepartamento" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            
                <telerik:AjaxSetting AjaxControlID="btnDirectorioActivo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtClave" />
                        <telerik:AjaxUpdatedControl ControlID="txtNombreUsuario" />
                        <telerik:AjaxUpdatedControl ControlID="txtNoEmpleado" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoPaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoMaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtCorreo" />
                        <telerik:AjaxUpdatedControl ControlID="rcbEmpresa" />
                        <telerik:AjaxUpdatedControl ControlID="cbCambioPassword" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtClave" />
                        <telerik:AjaxUpdatedControl ControlID="txtNombreUsuario" />
                        <telerik:AjaxUpdatedControl ControlID="txtNoEmpleado" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoPaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoMaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtCorreo" />
                        <telerik:AjaxUpdatedControl ControlID="rcbEmpresa" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSalir">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtClave" />
                        <telerik:AjaxUpdatedControl ControlID="txtNombreUsuario" />
                        <telerik:AjaxUpdatedControl ControlID="txtNoEmpleado" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoPaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtApellidoMaterno" />
                        <telerik:AjaxUpdatedControl ControlID="txtCorreo" />
                        <telerik:AjaxUpdatedControl ControlID="rcbEmpresa" />
                        <telerik:AjaxUpdatedControl ControlID="cbEstatus" />
                        <telerik:AjaxUpdatedControl ControlID="cbCambioPassword" />
                        <telerik:AjaxUpdatedControl ControlID="rlbPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="rlbPerfilesAdd" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>


        <div class="modal" style="width: 680px">

            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Administrar usuario"></asp:Label>
            </div>

            <div class="contenido">

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="padding-bottom: 5px">
                            <asp:RadioButtonList ID="rblTipoUsuario" runat="server" AutoPostBack="true" CssClass="radioButtonList" RepeatDirection="Horizontal" Width="250px" OnSelectedIndexChanged="rblTipoUsuario_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="true">Usuario de red</asp:ListItem>
                                <asp:ListItem Value="false">Nuevo usuario</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblClave" AssociatedControlID="txtClave" runat="server" Text="Clave :"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtClave" runat="server" MaxLength="25" ReadOnly="True" Width="160px">
                            </telerik:RadTextBox>
                        </td>
                        <td colspan="2">
                            <telerik:RadButton ID="btnDirectorioActivo" runat="server" Text="Directorio" OnClick="btnDirectorioActivo_Click" OnClientClicking="seleccionarUsuario">
                                <Icon PrimaryIconUrl="~/App_Resources/Imagenes/Sitio/Usuario.png" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblNombreUsuario" AssociatedControlID="txtNombreUsuario" runat="server" Text="Nombre usuario :" Width="100%"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNombreUsuario" runat="server" MaxLength="100" ReadOnly="True" Width="160px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadLabel ID="lblNoEmpleado" AssociatedControlID="txtNoEmpleado" runat="server" Text="No. empleado :"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtNoEmpleado" runat="server" MaxLength="7" ReadOnly="True" Width="160px">
                                <NegativeStyle Resize="None" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblApellidoPaterno" AssociatedControlID="txtApellidoPaterno" runat="server" Text="Apellido paterno :" Width="100%"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtApellidoPaterno" runat="server" MaxLength="50" ReadOnly="True" Width="160px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadLabel ID="lblApellidoMaterno" AssociatedControlID="txtApellidoMaterno" runat="server" Text="Apellido materno :" Width="100%"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtApellidoMaterno" runat="server" MaxLength="50" ReadOnly="True" Width="160px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblCorreo" AssociatedControlID="txtCorreo" runat="server" Text="Dirección correo :" Width="100%"></telerik:RadLabel>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txtCorreo" runat="server" MaxLength="50" ReadOnly="True" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblEmpresa" AssociatedControlID="rcbEmpresa" runat="server" Text="Empresa:"></telerik:RadLabel>
                        </td>
                        <td colspan="3">
                            <telerik:RadComboBox ID="rcbEmpresa" runat="server" Width="100%"
                                MaxHeight="200px" MarkFirstMatch="true" AutoPostBack="true"  CheckBoxes="true" EmptyMessage="Seleccione">
                            </telerik:RadComboBox>

                      
                        </td>
                       
                    </tr>
                    <%-- <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblEstatus" AssociatedControlID="btnEstatus" runat="server" Text="Estatus :"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnEstatus" runat="server" AutoPostBack="false" ButtonType="LinkButton" ToggleType="CheckBox" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked" Text="Activo" Value="true" Width="160px" />
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox" Text="Inactivo" Value="false" Width="160px" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
                        <td colspan="2" rowspan="3">
                            <div class="Nota" style="width: 260px;">
                                <b>Nota: </b>El cambio de password unicamente aplica para usuarios que no pertenecen al grupo de usuarios de red.
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblCambioPassword" AssociatedControlID="btnCambioPassword" runat="server" Text="Cambio password :"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnCambioPassword" runat="server" AutoPostBack="false" ButtonType="LinkButton" ToggleType="CheckBox" Enabled="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked" Text="Activo" Value="true" Width="160px" />
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox" Text="Inactivo" Value="false" Width="160px" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>

                <table style="margin: auto;">
                    <tr>
                        <td colspan="2">
                            <table style="margin: auto;">
                                <tr>
                                    <td style="padding-left: 60px">
                                        <asp:Label ID="lblDisponibles" runat="server" Text="Perfiles disponibles" CssClass="Subtitulo"></asp:Label>
                                    </td>
                                    <td style="padding-left: 90px">
                                        <asp:Label ID="lblAsignados" runat="server" Text="Perfiles asignados" CssClass="Subtitulo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadListBox ID="rlbPerfiles" runat="server" AllowTransfer="True" Height="200px" Sort="Ascending" TransferToID="rlbPerfilesAdd" Width="280px" ButtonSettings-AreaWidth="40px">
                                        </telerik:RadListBox>
                                    </td>
                                    <td style="padding-left: 26px">
                                        <telerik:RadListBox ID="rlbPerfilesAdd" runat="server" Height="200px" Width="240px">
                                        </telerik:RadListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 15px; padding-bottom: 5px">
                            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="125px" SingleClick="true" OnClientClicking="validaInsercionUsuario" OnClick="btnGuardar_Click" CssClass="rbPrimaryButton"/>
                            &nbsp;
                            <telerik:RadButton ID="btnSalir" runat="server" Text="Salir sin cambios" Width="125px" OnClientClicking="salir" />
                        </td>
                    </tr>
                </table>

            </div>
            <asp:Literal ID="itemsClientSide" runat="server" />
        </div>

    </form>
</body>
</html>
