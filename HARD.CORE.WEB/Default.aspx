<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <title>SISTEMA DE REQUISICIÓN DE TALENTO</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <link rel="icon" type="image/png" href="App_Resources/Imagenes/Sitio/CryoIcon.png" />
    <link href="App_Themes/Estilo_Login.css" rel="stylesheet" />
    <link rel="stylesheet" href="App_Resources/Bootstrap/css/bootstrap.min.css" />
    <link href="App_Resources/Fontawesome/css/fontawesome.css" rel="stylesheet" />
    <link href="App_Resources/Fontawesome/css/regular.css" rel="stylesheet" />
    <link href="App_Resources/Fontawesome/css/all.css" rel="stylesheet" />
    <script src="App_Resources/Bootstrap/js/bootstrap.min.js"></script>
    <script src="App_Scripts/Cryoinfra.js"></script>
    <link href="App_Scripts/toastify.css" rel="stylesheet" />
    <script src="App_Scripts/toastify.js"></script>

</head>
<body>
    <div>

        <form id="formL" runat="server">

            <telerik:RadScriptManager ID="sManager" runat="server">
            </telerik:RadScriptManager>
            <telerik:RadScriptBlock ID="rsBlock" runat="server">
                <script type="text/javascript">

                    document.addEventListener('DOMContentLoaded', function () {
                        var form = document.querySelector('form');

                        form.addEventListener('keypress', function (e) {
                            if (e.key === 'Enter') {
                                e.preventDefault();
                                var btnLogin = document.getElementById('<%= btnLogin.ClientID %>');
                           btnLogin.click();
                       }
                   });
               });

                    function ValidaDatosLogin(sender, args) {
                        const txtUsuario = document.getElementById("txtUsuario");
                        const usuario = txtUsuario.value.trim();
                        var rcbEmpresa = $find('<%= rcbEmpresa.ClientID%>');
                   var rcbPerfil = $find('<%= rcbPerfil.ClientID%>');
                        const txtContraseña = document.getElementById("txtPassword");
                        const contraseña = txtContraseña.value.trim();

                        const mensajeElemento = document.getElementById("txtIncorrecto");
                        let mensajeError = "";

                        if (usuario === "") {
                            mensajeError += "• Favor de ingresar la clave del usuario.";
                            Toastify({
                                text: "¡Favor de ingresar el usuario !",
                                duration: 3000,
                                gravity: "center",  // ← SOLO ESTA LÍNEA
                                position: "center", // ← Y ESTA LÍNEA
                                style: {
                                    background: "#ffc107",
                                    color: "white"
                                }
                            }).showToast();
                            args.set_cancel(true); // Cancelar el postback

                        }

                        if (contraseña === "") {
                            mensajeError += "• Favor de ingresar la contraseña.";
                            Toastify({
                                text: "¡Favor de ingresar la contraseña!",
                                duration: 3000,
                                gravity: "center",  // ← SOLO ESTA LÍNEA
                                position: "center", // ← Y ESTA LÍNEA
                                style: {
                                    background: "#ffc107",
                                    color: "white"
                                }
                            }).showToast();
                            args.set_cancel(true); // Cancelar el postback

                        }
                        if (rcbPerfil) {
                            if (!validar_Combo(rcbPerfil, "", "Favor de seleccionar un perfil")) {
                                args.set_cancel(true);
                            }
                        }
                        if (rcbEmpresa) {
                            if (!validar_Combo(rcbEmpresa, "", "Favor de seleccionar la empresa")) {
                                args.set_cancel(true);
                            }
                        }

                        return true;
                    }

                    function ValidaDatosExtraLogin(sender, args) {
                        var rcbEmpresa = $find('<%= rcbEmpresa.ClientID%>');
                        var rcbPerfil = $find('<%= rcbPerfil.ClientID%>');

                        const mensajeElemento = document.getElementById("txtIncorrecto");
                        let mensajeError = "";


                        if (rcbPerfil) {
                            if (!validar_Combo(rcbPerfil, "", "Favor de seleccionar un perfil")) {
                                args.set_cancel(true);
                            }
                        }
                        if (rcbEmpresa) {
                            if (!validar_Combo(rcbEmpresa, "", "Favor de seleccionar la empresa")) {
                                args.set_cancel(true);
                            }
                        }
                        return true;
                    }

                    function olvidarContrasena(sender, args) {
                        const txtUsuario = document.getElementById("txtUsuario");
                        const usuario = txtUsuario.value.trim();

                        if (usuario === "") {


                            Toastify({
                                text: "¡Por favor, ingresa usuario antes de continuar!",
                                duration: 3000,
                                gravity: "center",  // ← SOLO ESTA LÍNEA
                                position: "center", // ← Y ESTA LÍNEA
                                style: {
                                    background: "#ffc107",
                                    color: "white"
                                }
                            }).showToast();
                            txtUsuario.focus();
                            args.set_cancel(true); // Cancelar el postback
                        }
                    }


                    //Notiflix.Confirm.show(
                    //    'Confirmación',     // Título
                    //    mensaje,            // Mensaje
                    //    'Sí',               // Texto botón aceptar
                    //    'Cancelar',         // Texto botón cancelar
                    //    function okCb() {
                    //        // Usuario presionó Sí
                    //        sender.click(); // Continua con el postback
                    //    },
                    //    function cancelCb() {
                    //        // Usuario presionó Cancelar
                    //        // No hacemos nada, el postback queda cancelado
                    //    },
                    //    {
                    //        width: '320px',
                    //        borderRadius: '8px',
                    //        titleColor: '#ff0000',   // Título en rojo
                    //        okButtonColor: '#28a745',    // Botón Sí verde
                    //        cancelButtonColor: '#dc3545', // Botón No rojo
                    //        messageColor: '#000000',
                    //        fontSize: '16px',
                    //        plainText: false,
                    //        cssAnimation: true,
                    //        cssAnimationDuration: 400
                    //    }
                    //);
                </script>
            </telerik:RadScriptBlock>
            <telerik:RadAjaxManager ID="raManager" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="btnLogin">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="txtUsuario" LoadingPanelID="LoadingPanel" />
                            <telerik:AjaxUpdatedControl ControlID="txtPassword" LoadingPanelID="LoadingPanel" />
                            <telerik:AjaxUpdatedControl ControlID="txtIncorrecto" />
                            <telerik:AjaxUpdatedControl ControlID="rcbPerfil" LoadingPanelID="LoadingPanel" />
                            <telerik:AjaxUpdatedControl ControlID="rcbEmpresa" LoadingPanelID="LoadingPanel" />
                            <telerik:AjaxUpdatedControl ControlID="txtPassword" LoadingPanelID="LoadingPanel" />
                            <telerik:AjaxUpdatedControl ControlID="btnEntar" LoadingPanelID="LoadingPanel" />
                            <telerik:AjaxUpdatedControl ControlID="btnLogin" LoadingPanelID="LoadingPanel" />

                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="rcbPerfil">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="rcbEmpresa" LoadingPanelID="LoadingPanel" />

                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>


            <div id="prueba" class="container-login">

                <!-- Panel izquierdo -->
                <div class="left-section">
                    <div class="floating-images">
                        <img src="App_Resources/Imagenes/Sitio/bg-default1.png" alt="" class="floating-img2 img-1">
                        <img src="App_Resources/Imagenes/Sitio/bg-default2.png" alt="" class="floating-img2 img-2">
                        <img src="App_Resources/Imagenes/Sitio/FondoLoginMuestra1.png" alt="" class="floating-img2 img-3">
                    </div>
                    <div class="floating-images2">
                        <img src="App_Resources/Imagenes/Sitio/bg-default1.png" alt="" class="floating-img img-12">
                        <img src="App_Resources/Imagenes/Sitio/bg-default2.png" alt="" class="floating-img img-22">
                        <img src="App_Resources/Imagenes/Sitio/FondoLoginMuestra1.png" alt="" class="floating-img img-32">
                    </div>
                    <div class="logo-container">
                        <img src="App_Resources/Imagenes/Sitio/LogoCryoinfra.png" alt="Cryoinfra" class="logo">
                    </div>

                    <div class="bottom-text">
                        SISTEMA DE REQUISICIÓN DE TALENTO                   
                    </div>
                </div>

                <!-- Panel derecho -->
                <div class="right-section">
                    <div class="login-card">
                        <div class="circle-logo">
                            <img src="App_Resources/Imagenes/Sitio/icono_cryoinfra.png" alt="Icono">
                        </div>
                        <h2>Iniciar sesión</h2>

                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="txtPlaceholder"
                            MaxLength="25"
                            placeholder="Usuario">
                        </asp:TextBox>
                        <input name="login" type="password" class="txtPlaceholder" id="txtPassword" placeholder="Contraseña" runat="server" maxlength="25" />

                        <telerik:RadComboBox ID="rcbPerfil" CssClass="ComboBoxPerfil" runat="server" AutoPostBack="true" MarkFirstMatch="true" EnableViewState="true" Width="100%" EmptyMessage="Seleccione un perfil" OnSelectedIndexChanged="rcbPerfil_SelectedIndexChanged"></telerik:RadComboBox>

                        <telerik:RadComboBox ID="rcbEmpresa" CssClass="ComboBoxEmpresa" runat="server" Width="100%" MarkFirstMatch="true" AutoPostBack="true" EnableViewState="true" EmptyMessage="Seleccione una empresa"></telerik:RadComboBox>

                        <telerik:RadButton ID="btnLogin" runat="server" Text="Entrar" CssClass="boton-login" OnClick="btnLogin_Click" AutoPostBack="true" OnClientClicking="ValidaDatosLogin" ForeColor="White" />

                        <telerik:RadButton ID="btnEntar" runat="server" Text="Entrar" AutoPostBack="true" CssClass="boton-login" OnClick="btnLoginEntrar_Click" OnClientClicking="ValidaDatosExtraLogin" ForeColor="White" />

                        <p class="text-center fw-semibold" style="color: #314F7C" id="txtIncorrecto" runat="server" visible="false"></p>

                    </div>
                    <p class="credits">Tecnologías de la Información 2025</p>
                </div>
            </div>
            <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
        </form>
    </div>
</body>
</html>
