<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Cambio_Password.aspx.cs" Inherits="frm_Cambio_Password" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="h-100 w-100">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sistema de Requisición de Nuevo Presonal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <link rel="icon" type="image/png" href="App_Resources/Imagenes/Sitio/CryoIcon.png" />
    <script src="App_Scripts/Cryoinfra.js"></script>
    <link href="App_Themes/Estilo_CambioPassword.css" rel="stylesheet" />
    <link rel="stylesheet" href="App_Resources/Bootstrap/css/bootstrap.min.css" />
    <script src="App_Resources/Bootstrap/js/bootstrap.min.js"></script>
    <script src="App_Resources/Bootstrap/js/bootstrap.bundle.min.js"></script>
    <link href="App_Resources/Fontawesome/css/fontawesome.css" rel="stylesheet" />
    <link href="App_Resources/Fontawesome/css/regular.css" rel="stylesheet" />
    <link href="App_Resources/Fontawesome/css/solid.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.0/jquery.min.js"></script>
    <link href="App_Scripts/toastify.css" rel="stylesheet" />
    <script src="App_Scripts/toastify.js"></script>
</head>
<body class="h-100 w-100 position-absolute">
    <div class="h-100 w-100 position-absolute formCambioPassword"></div>
    <form id="FormCambioPassword" runat="server" autocomplete="off" class="h-100 w-100 position-absolute">
        <telerik:RadScriptManager ID="sManager" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnCambiar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtContrasenaActual" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="txtNuevaContrasena" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="txtConfirmaContrasena" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">
                function validaInsercion(sender, args) {
                    var txtContrasenaActual = $find('<%= txtContrasenaActual.ClientID%>');
                    var txtNuevaContrasena = $find('<%= txtNuevaContrasena.ClientID%>');
                    var txtConfirmaContrasena = $find('<%= txtConfirmaContrasena.ClientID%>');
                    var password_strength = document.getElementById("CustomIndicator");

                    var contrasenaActual = txtContrasenaActual.get_textBoxValue().trim();
                    var nuevaContrasena = txtNuevaContrasena.get_textBoxValue().trim();
                    var confirmarContrasena = txtConfirmaContrasena.get_textBoxValue().trim();

                    var mensaje = "¿Se encuentra seguro de actualizar la contraseña?";

                    if (!validar_TextboxToastify(txtContrasenaActual, "Favor de ingresar la contraseña actual")) {
                        args.set_cancel(true);
                    } else if (!validar_TextboxToastify(txtNuevaContrasena, "Favor de ingresar la nueva contraseña")) {
                        args.set_cancel(true);
                    } else if (!validar_TextboxToastify(txtConfirmaContrasena, "Favor de confirmar la nueva contraseña")) {
                        args.set_cancel(true);
                    } else if (nuevaContrasena != confirmarContrasena) {
                        alert("La nueva contraseña y la confirmación no coinciden. Es necesario que vuelva a confirmar la nueva contraseña");
                        args.set_cancel(true);
                    } else if (contrasenaActual == nuevaContrasena) {
                        Toastify({
                            text: "La nueva contraseña debe ser diferente a la contraseña actual",
                            duration: 3000,
                            gravity: "center",  // ← SOLO ESTA LÍNEA
                            position: "center", // ← Y ESTA LÍNEA
                            style: {
                                background: "var(--MoradoHARDCORE, #ff0000)",
                                color: "white"
                            }
                        }).showToast();
                        args.set_cancel(true);
                    } else if (password_strength.innerHTML == "Muy débil" || password_strength.innerHTML == "Débil" || password_strength.innerHTML == "Medio") {
                        Toastify({
                            text: "La nueva contraseña no cumple con los parámetros de seguridad recomendados, favor de volver a intentar.",
                            duration: 3000,
                            gravity: "center",  // ← SOLO ESTA LÍNEA
                            position: "center", // ← Y ESTA LÍNEA
                            style: {
                                background: "var(--MoradoHARDCORE, #ff0000)",
                                color: "white"
                            }
                        }).showToast();
                        args.set_cancel(true);
                    } else if (!confirm(mensaje)) {
                        args.set_cancel(true);
                    }
                }

                function checkPasswordMatch() {
                    var txtNuevaContrasena = $find('<%= txtNuevaContrasena.ClientID%>');
                    var txtConfirmaContrasena = $find('<%= txtConfirmaContrasena.ClientID%>');

                    var nuevaContrasena = txtNuevaContrasena.get_textBoxValue().trim();
                    var confirmaContrasena = txtConfirmaContrasena.get_textBoxValue().trim();

                    if (confirmaContrasena == '') {
                        $get('PasswordRepeatedIndicator').innerHTML = '';
                        $get('PasswordRepeatedIndicator').className = 'Base L0';
                    } else if (nuevaContrasena == confirmaContrasena) {
                        $get('PasswordRepeatedIndicator').innerHTML = 'Coincide';
                        $get('PasswordRepeatedIndicator').className = 'Base L5';
                    } else {
                        $get('PasswordRepeatedIndicator').innerHTML = 'No coincide';
                        $get('PasswordRepeatedIndicator').className = 'Base L1';
                    }
                }
            </script>
        </telerik:RadScriptBlock>

        <div class="container">
            <div class="row pt-3">
                <div class="col d-md-flex justify-content-center">
                    <img id="ImagenCambioPassword" src="App_Resources/Imagenes/Sitio/CryoLogo.png" style="width: 300px;" />
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <p class="text-center fs-3 fc-azul fw-semibold">Sistema de requisición de talento</p>
                </div>
            </div>
            <div class="row">
                <div class="col d-md-flex justify-content-center ContenedorMensaje">
                    <div class="row Contenedorsecundario w-50 align-items-center">
                        <div id="IconoOlvida" class="col" style="max-width: 50px">
                            <i class="fa-solid fa-circle-exclamation fc-blanco fs-2"></i>
                        </div>
                        <div class="col alert-aviso rounded p-2">
                            <p class="mb-0 fc-blanco fs-6" runat="server" id="txtAviso"></p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row d-md-flex justify-content-center">
                <div class="p-4 mt-4 bg-blanco " style="max-width: 1000px;">
                    <div class="row justify-content-center">
                        <div class="col-auto">
                            <div class="row">
                                <p class="fc-blanco fs-6 mb-0 fw-semibold">Contraseña actual:</p>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <telerik:RadTextBox ID="txtContrasenaActual" runat="server" Width="400px" MaxLength="200" PasswordStrengthSettings-IndicatorWidth="0px" TextMode="Password" />
                                </div>
                            </div>
                            <div class="row pt-3">
                                <p class="fc-blanco fs-6 mb-0 fw-semibold">Nueva contraseña:</p>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <telerik:RadTextBox ID="txtNuevaContrasena" runat="server" Width="400px" MaxLength="200" PasswordStrengthSettings-IndicatorWidth="0px" TextMode="Password" onkeyup="checkPasswordMatch()">
                                        <PasswordStrengthSettings ShowIndicator="true" TextStrengthDescriptions="Muy débil;Débil;Medio;Fuerte;Muy fuerte"
                                            IndicatorElementBaseStyle="Base" TextStrengthDescriptionStyles="L0;L1;L2;L3;L4;L5"
                                            IndicatorElementID="CustomIndicator" />
                                    </telerik:RadTextBox>
                                    <span id="CustomIndicator" style="width: 400px; height: 25px">&nbsp;</span>
                                </div>
                            </div>
                            <div class="row pt-3">
                                <p class="fc-blanco fs-6 mb-0 fw-semibold">Confirmación de contraseña:</p>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <telerik:RadTextBox ID="txtConfirmaContrasena" runat="server" Width="400px" MaxLength="200" TextMode="Password" onkeyup="checkPasswordMatch()" />
                                    <span id="PasswordRepeatedIndicator" style="width: 400px; height: 25px" class="Base L0">&nbsp;</span>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col d-flex justify-content-center">
                                    <telerik:RadButton ID="btnCambiar" runat="server" Text="Cambiar contraseña" OnClick="btnCambiar_Click" OnClientClicking="validaInsercion" ButtonType="StandardButton" CssClass="botonEnviar"></telerik:RadButton>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col text-center fc-recomendaciones fs-6 fw-semibold">
                                    <p>Recomendaciones para la contraseña:</p>
                                    <ul class="text-start d-inline-block text-wrap" style="max-width: 400px;">
                                        <li>Incluir números.</li>
                                        <li>Combinación de letras mayúsculas y minúsculas.</li>
                                        <li>Incluir caracteres especiales - * ? ! @ # $ / () {} = . ,; </li>
                                        <li>Longitud mayor o igual a 8 caracteres.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>