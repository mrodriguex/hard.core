<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Administracion_CambioContrasena.aspx.cs" Inherits="frm_Administracion_CambioContrasena" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">

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

                if (!validar_Textbox(txtContrasenaActual, "Favor de ingresar la contraseña actual")) {
                    args.set_cancel(true);
                } else if (!validar_Textbox(txtNuevaContrasena, "Favor de ingresar la nueva contraseña")) {
                    args.set_cancel(true);
                } else if (!validar_Textbox(txtConfirmaContrasena, "Favor de confirmar la nueva contraseña")) {
                    args.set_cancel(true);
                } else if (contrasenaActual == nuevaContrasena) {
                    alert("La nueva contraseña debe ser diferente a la contraseña actual");
                    args.set_cancel(true);
                } else if (nuevaContrasena != confirmarContrasena) {
                    alert("La nueva contraseña y la confirmación no coinciden. Es necesario que vuelva a confirmar la nueva contraseña");
                    args.set_cancel(true);
                } else if (password_strength.innerHTML == "Muy débil" || password_strength.innerHTML == "Débil" || password_strength.innerHTML == "Medio") {
                    alert("La nueva contraseña no cumple con los parámetros de seguridad recomendados, favor de volver a intentar.");
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

        <style type="text/css">
            .riContentWrapper {
                width: 100% !important;
            }

            .botonEnviarContrasena {
                padding: 0px !important;
                height: 45px !important;
                width: 222px !important;
                margin-left: 250px;
            }

            .rbDecorated {
                height: 100% !important;
                width: 100% !important;
            }

            .control-container {
                float: left;
                padding-left: 50px;
                width: 330px;
            }

            .Base {
                display: inline-block;
                font: "segoe ui",arial,sans-serif bold;
                height: 100%;
                overflow: hidden;
                text-align: center;
                vertical-align: middle;
                color: #FFFFFF;
            }

            .rbDecorated {
                font-family: "segoe ui",arial,sans-serif bold;
            }

            .L0 {
                border: 0 none;
                background-color: transparent;
            }

            .L1 {
                background-color: #8A0808;
            }

            .L2 {
                background-color: #8A4B08;
            }

            .L3 {
                background-color: #868A08;
            }

            .L4 {
                background-color: #86B404;
            }

            .L5 {
                background-color: #298A08;
            }
        </style>

    </telerik:RadScriptBlock>

    <div class="container bg-blanco py-4 mt-5" style="padding-inline: 100px; max-width: 770px; background-color: rgb(205, 213, 226) !important;">
        <p class="text-center fs-3 fc-azul fw-semibold ">CAMBIO DE CONTRASEÑA</p>

        <div class="row align-items-center mt-4">
            <div class="col-md-5">
                <p id="lblContrasenaActual" class="fs-6 mb-0" style="color: rgb(29, 64, 109);">Contraseña actual:</p>
            </div>
            <div class="col-md-5">
                <telerik:RadTextBox ID="txtContrasenaActual" runat="server" Width="100%" MaxLength="200" PasswordStrengthSettings-IndicatorWidth="0px" TextMode="Password" Height="40" />
            </div>
        </div>

        <div class="row align-items-center mt-4">
            <div class="col-md-5" style="margin-top:-5px;">
                <p id="lblNuevaContrasena" class="fs-6 mb-0" style="color: rgb(29, 64, 109);">Nueva contraseña:</p>
            </div>
            <div class="col-md-5" style="margin-top:20px;">
                <telerik:RadTextBox ID="txtNuevaContrasena" runat="server" Width="100%" MaxLength="200" PasswordStrengthSettings-IndicatorWidth="0px" TextMode="Password" onkeyup="checkPasswordMatch()" Height="40">
                    <PasswordStrengthSettings
                        ShowIndicator="true"
                        TextStrengthDescriptions="Muy débil;Débil;Medio;Fuerte;Muy fuerte"
                        IndicatorElementBaseStyle="Base"
                        TextStrengthDescriptionStyles="L0;L1;L2;L3;L4;L5"
                        IndicatorElementID="CustomIndicator" />
                </telerik:RadTextBox>
                <span id="CustomIndicator" style="width: 100%; height: 25px;">&nbsp;</span>
            </div>
        </div>

        <div class="row align-items-center mt-4">
            <div class="col-md-5" style="margin-top:-20px;">
                <p id="lblConfirmaContrasena" class="fs-6 mb-0" style="color: rgb(29, 64, 109);">Confirmar contraseña:</p>
            </div>

            <div class="col-md-5">
                <telerik:RadTextBox ID="txtConfirmaContrasena" runat="server" Width="100%" MaxLength="200" TextMode="Password" onkeyup="checkPasswordMatch()" Height="40" />
                <span id="PasswordRepeatedIndicator" style="width: 100%; height: 25px" class="Base L0">&nbsp; </span>
            </div>
        </div>

        <div class="">
            <div class="col-auto">
                <telerik:RadButton ID="btnCambiar" runat="server" Text="Cambiar contraseña" OnClick="btnCambiar_Click" OnClientClicking="validaInsercion" ButtonType="StandardButton" CssClass="botonEnviarContrasena" BorderWidth="1"></telerik:RadButton>
            </div>
        </div>

        <div class="card mb-1 mt-1" style="background-color: #E49517">
            <div class="card-body">
                <div class="row">
                    <div class="col">
                        <p class="fc-texto-obscuro fs-6	">
                            <strong>Nota:</strong> Recuerda utilizar letras mayúsculas, minúsculas y caracteres especiales para hacer tu contraseña más segura.
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </div>
</asp:Content>