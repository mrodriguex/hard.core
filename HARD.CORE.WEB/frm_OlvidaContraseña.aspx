<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_OlvidaContraseña.aspx.cs" Inherits="frm_OlvidaContraseña" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="h-100 w-100">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Portal de socios Cryoinfra</title>
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
    <link href="App_Scripts/toastify.css" rel="stylesheet"/>
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
                        <telerik:AjaxUpdatedControl ControlID="CheckBoxListClientes" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnCancelar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="CheckBoxListClientes" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">
                function validarCheckBoxList(clickedCheckbox) {
                    var checkboxes = document.querySelectorAll('[id$="CheckBoxListClientes"] input[type=checkbox]');
                    checkboxes.forEach(function (checkbox) {
                        if (checkbox !== clickedCheckbox) {
                            checkbox.checked = false;
                        }
                    });
                }
            </script>

        </telerik:RadScriptBlock>

        <div class="container">
            <div class="row pt-3">
                <div class="col d-md-flex justify-content-center">
                    <img id="ImagenLogo" src="App_Resources/Imagenes/Sitio/CryoLogo.png" style="width: 300px;" />
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <p class="text-center fs-3 fc-azul fw-semibold">Sistema de requisición personal</p>
                </div>
            </div>
            <div class="col d-md-flex justify-content-center ContenedorMensaje">
                <div class="Contenedorsecundario row align-items-center " style="width:50%;">
                    <div class="col" id="IconoOlvida" style="max-width: 50px">
                        <i class="fa-solid fa-circle-exclamation fc-azul fs-2"></i>
                    </div>
                    <div class="col alert-aviso rounded p-2">
                        <p class="mb-0 fc-blanco fs-6" runat="server" id="txtAviso"></p>
                        <p class="mb-0 fc-blanco fs-6" runat="server" id="txtAvisoActive" visible="false"></p>
                    </div>
                </div>

            </div>
            <div class="card text-bg-lightn mx-auto my-4" style="width: 50%;">
                <div class="card-body">
                    <div class="row">
                        <div class="col d-md-flex justify-content-center">
                            <telerik:RadRadioButtonList ID="rblClientes" runat="server" RepeatDirection="Vertical" CssClass="form-radios" />
                            <div style="padding-bottom: 108px;" id="divCambioPassword" runat="server" visible="false">
                                <a href="https://passwordreset.microsoftonline.com/passwordreset#!/" target="_self" style="color: rgb(36,113,163);" class="underlineHover">Ir a Recuperación de contraseña<br />
                                    (usuarios de Windows)</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="d-flex justify-content-center gap-2">
                <telerik:RadButton CssClass="BtnCambia" ForeColor="White" RenderMode="Lightweight" ID="btnCambiar" runat="server" OnClick="btnCambiar_Click"
                    Text="Enviar">
                </telerik:RadButton>
                <telerik:RadButton CssClass="BtnCancela" RenderMode="Lightweight" ID="btnCancelar" runat="server" OnClick="btnCancelar_Click"
                    Text="Cancelar">
                </telerik:RadButton>
            </div>
        </div>

        <div class="d-flex justify-content-center">
            <a href="Default.aspx" class="text-end text-decoration-underline;" style="color: blue; cursor: pointer;">Ir a Log In</a>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
