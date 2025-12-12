<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Administracion_NivelIngles.aspx.cs" Inherits="pop_Administracion_NivelIngles" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Nivel de inglés</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/jquery-3.1.1.min.js"></script>
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
    <link href="App_Scripts/toastify.css" rel="stylesheet" />
    <script src="App_Scripts/toastify.js"></script>

        <link href="App_Scripts/notiflix.css" rel="stylesheet" />
    <script src="App_Scripts/notiflix.js"></script>

    <style type="text/css">
        /* Estilos responsivos adicionales */
        .RadUpload_Bootstrap .ruButton {
            padding: 3px !important;
            height: 25px !important;
            width: 108px !important;
            margin-left: 3px;
        }
        
        /* Estilos responsivos */
        .modal {
            width: 100%;
            max-width: 800px;
            margin: 0 auto;
            padding: 10px;
            box-sizing: border-box;
        }
        
        .contenido table {
            width: 100%;
            table-layout: fixed;
        }
        
        .contenido table tr {
            display: block;
            margin-bottom: 10px;
        }
        
        .contenido table td {
            display: block;
            width: 100% !important;
            padding: 5px 0;
        }
        
        .contenido table td:first-child {
            padding-bottom: 5px;
        }
        
        .RadTextBox, .RadAsyncUpload {
            width: 100% !important;
            max-width: 100%;
            box-sizing: border-box;
        }
        
        .RadLabel {
            display: block;
            width: 100% !important;
        }
        
        .botones-container {
            text-align: center;
            padding: 18px 0 10px;
        }
        
        .botones-container .RadButton {
            width: 140px;
            margin: 5px;
            display: inline-block;
        }
        
        /* Media Queries para diferentes tamaños de pantalla */
        @media (min-width: 768px) {
            .contenido table tr {
                display: table-row;
                margin-bottom: 0;
            }
            
            .contenido table td {
                display: table-cell;
                width: auto !important;
                padding: 5px;
            }
            
            .contenido table td:first-child {
                width: 180px !important;
                padding-bottom: 5px;
            }
            
            .RadTextBox, .RadAsyncUpload {
                width: 100% !important;
                max-width: 670px;
            }
        }
        
        @media (max-width: 480px) {
            .botones-container .RadButton {
                width: 100%;
                max-width: 200px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="sManager" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">
            <script type="text/javascript">
               

            <%--    function validaInsercion(sender, args) {
                    var params = new URLSearchParams(window.location.search);
                    var accion = params.get('Accion');
                    var txtDescripcion = $find('<%= txtDescripcion.ClientID%>');

                    // Primero validar el textbox
                    if (!validar_TextboxToastify(txtDescripcion, "Favor de ingresar la descripción")) {
                        args.set_cancel(true);
                        return; // Detener aquí si la validación falla
                    }

                    // Definir el mensaje según la acción
                    var mensaje = "¿Se encuentra seguro de insertar el nivel de inglés?";
                    if (accion == "2") {
                        mensaje = "¿Se encuentra seguro de actualizar nivel de inglés?";
                    }

                    // Prevenir el postback inmediato
                    args.set_cancel(true);

                    // Mostrar confirmación de Notiflix
                    Notiflix.Confirm.show(
                        'Confirmación',     // Título
                        mensaje,            // Mensaje
                        'Sí',               // Texto botón aceptar
                        'Cancelar',         // Texto botón cancelar
                        function okCb() {
                            // Usuario presionó Sí - ejecutar el postback manualmente
                            __doPostBack(sender.name, '');
                        },
                        function cancelCb() {
                            // Usuario presionó Cancelar - no hacer nada
                            Notiflix.Notify.info('Operación cancelada');
                        },
                        {
                            width: '320px',
                            borderRadius: '8px',
                            titleColor: '#ff0000',   // Título en rojo
                            okButtonColor: '#28a745',    // Botón Sí verde
                            cancelButtonColor: '#dc3545', // Botón Cancelar rojo
                            messageColor: '#000000',
                            fontSize: '16px',
                            plainText: false,
                            cssAnimation: true,
                            cssAnimationDuration: 400
                        }
                    );
                }--%>
            <%--   function validaInsercion(sender, args) {
                    var params = new URLSearchParams(window.location.search);

                    var accion = params.get('Accion');
                    mensaje = "¿Se encuentra seguro de insertar el nivel de inglés?";

                    var txtDescripcion = $find('<%= txtDescripcion.ClientID%>');
                   
                    if (accion == "2") {
                        mensaje = "¿Se encuentra seguro de actualizar nivel de inglés?";


                        Notiflix.Confirm.show(
                            'Confirmación',     // Título
                            mensaje,            // Mensaje
                            'Sí',               // Texto botón aceptar
                            'Cancelar',         // Texto botón cancelar
                            function okCb() {
                                // Usuario presionó Sí
                                sender.click(); // Continua con el postback
                            },
                            function cancelCb() {
                                // Usuario presionó Cancelar
                                // No hacemos nada, el postback queda cancelado
                            },
                            {
                                width: '320px',
                                borderRadius: '8px',
                                titleColor: '#ff0000',   // Título en rojo
                                okButtonColor: '#28a745',    // Botón Sí verde
                                cancelButtonColor: '#dc3545', // Botón No rojo
                                messageColor: '#000000',
                                fontSize: '16px',
                                plainText: false,
                                cssAnimation: true,
                                cssAnimationDuration: 400
                            }
                        );


                    }

                    if (!validar_TextboxToastify(txtDescripcion, "Favor de ingresar la descripción")) {
                        args.set_cancel(true);
                    } 
                }--%>

                function validaInsercion(sender, args) {
                    var params = new URLSearchParams(window.location.search);

                    var accion = params.get('Accion');
                    mensaje = "¿Se encuentra seguro de insertar el nivel de inglés?";

                    var txtDescripcion = $find('<%= txtDescripcion.ClientID%>');

                    if (accion == "2") {
                        mensaje = "¿Se encuentra seguro de actualizar nivel de inglés?";
                    }

                    if (!validar_TextboxToastify(txtDescripcion, "Favor de ingresar la descripción")) {
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
        </telerik:RadScriptBlock>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtDescripcion" LoadingPanelID="LoadingPanel" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>


        <div class="modal" style="width: auto;">

            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Administración nivel inglés"></asp:Label>
            </div>
            <div class="contenido">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel5" AssociatedControlID="txtTitulo" runat="server"
                                Text='Titulo: <span style="color:red">*</span>' Width="180px" HtmlEncode="false" Visible="false" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtClaveAviso" runat="server" MaxLength="50" Width="670px" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                       
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <telerik:RadLabel ID="RadLabel2" AssociatedControlID="txtDescripcion" runat="server"
                                Text='Descripción: <span style="color:red">*</span>' Width="180px" HtmlEncode="false" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="50" Width="100%" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblEstatus" AssociatedControlID="cbEstatus" runat="server"
                                Text="Estatus:" HtmlEncode="false"/>

                        </td>
                        <td>
                            <telerik:RadSwitch ID="cbEstatus" runat="server" AutoPostBack="false" Checked="true" CssClass="customIcons"></telerik:RadSwitch>
                        </td>
                    </tr>
                   
                </table>



            </div>
            <div class="botones-container">
                <telerik:RadButton ID="btnSalir" runat="server" Text="Salir sin cambios" Width="140px" OnClientClicking="salir" />
                &nbsp;
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Width="140px" SingleClick="true" OnClientClicking="validaInsercion" OnClick="btnGuardar_Click" CssClass="rbPrimaryButton" />
            </div>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>