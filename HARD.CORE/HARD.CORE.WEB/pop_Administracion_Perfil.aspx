<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Administracion_Perfil.aspx.cs" Inherits="pop_Administracion_Perfil" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Asignación de menú</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
    <style>
        /* Estilos base responsivos */
        body {
            margin: 0;
            font-family: Arial, sans-serif;
        }

        .modal {
            width: 100%;
            max-width: 800px;
            margin: 0 auto;
            box-sizing: border-box;
            background: white;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        .titulo {
            background-color: white;
            color: white;
            padding: 15px;
            font-weight: bold;
            font-size: 18px;
            border-radius: 5px 5px 0 0;
        }

        .contenidoSimple {
            width: 100%;
            box-sizing: border-box;
        }

        /* Estilos para la tabla de información del perfil */
        .info-perfil {
            width: 100%;
            margin-bottom: 20px;
            border-collapse: collapse;
        }

            .info-perfil td {
                padding: 8px;
                vertical-align: middle;
            }

        .RadLabel {
            display: contents !important;
        }
        /* Contenedor para los árboles de menú */
        .menu-container {
            display: flex;
            flex-direction: column;
            gap: 20px;
            margin-bottom: 20px;
        }

        .menu-section {
            width: 100%;
        }

        .menu-title {
            font-weight: bold;
            margin-bottom: 10px;
            text-align: center;
            color: #00306E;
        }

        .menu-box {
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 10px;
            height: 420px;
            overflow: auto;
            background: #f9f9f9;
        }

        /* Contenedor de botones */
        .botones-container {
            text-align: center;
            padding: 20px 0;
        }

            .botones-container .RadButton {
                margin: 5px;
                display: inline-block;
            }

        /* Media Queries para diferentes tamaños */
        @media (min-width: 768px) {
            .menu-container {
                flex-direction: row;
            }

            .menu-section {
                width: 50%;
            }

            .info-perfil {
                width: 90%;
                margin-left: auto;
                margin-right: auto;
            }

                .info-perfil td:first-child {
                    width: 150px;
                }
        }

        @media (max-width: 480px) {
            .botones-container .RadButton {
                width: 100%;
                max-width: 200px;
            }

            .menu-box {
                height: 300px;
            }
        }

        /* Asegurar que los controles Telerik se adapten */
        .RadTextBox, .RadButton, .RadTreeView, .RadLabel {
            box-sizing: border-box;
            max-width: 100%;
        }

        .RadTreeView {
            width: 100% !important;
        }

        /* Estilos para mantener compatibilidad */
        .Marco {
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 10px;
            background: #f9f9f9;
        }

        .Subtitulo {
            font-weight: bold;
            color: #00306E;
            margin-bottom: 10px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="sManager" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtvMenu">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rtvMenu" />
                        <telerik:AjaxUpdatedControl ControlID="rtvMenuAsignado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Menu" LoadingPanelID="LoadingPanel" />
                        <telerik:AjaxUpdatedControl ControlID="MenuAsignado" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">

            <script type="text/javascript">

                var clavePerfil = 0

                $(document).ready(function () {
                    clavePerfil = queryString("ClavePerfil");
                });

                function validaRequeridos(sender, args) {
                    var NombrePerfil = $find("<%= txtDescripcion.ClientID %>");

                    var valor = NombrePerfil.get_value();

                    var mensaje = "¿Está seguro de ingresar el nuevo perfil: \"" + valor + "\"?";
                    if (clavePerfil > 0) {
                        mensaje = "Se encuentra seguro de actualizar perfil de usuario:  \"" + valor + "\"?";
                    }

                    var txtDescripcion = $find('<%= txtDescripcion.ClientID%>');
                    var nodosMenuAsignado = $find('<%=rtvMenuAsignado.ClientID%>').get_nodes().get_count();

                    if (!validar_Textbox(txtDescripcion, "Favor de ingresar la descripción del perfil")) {
                        args.set_cancel(true);
                    } else if (nodosMenuAsignado <= 0) {
                        alert("Es necesario que el perfil cuente con al menos una opción de menu asignada!")
                        args.set_cancel(true);
                    } else if (!confirm(mensaje)) {
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

        <div class="modal">

            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Configuración perfil"></asp:Label>
            </div>

            <div class="contenidoSimple">

                <table class="info-perfil">
                    <tr>
                        <td>
                            <telerik:RadLabel ID="lblDescripcion" AssociatedControlID="txtDescripcion" runat="server" Text="Nombre de perfil :"></telerik:RadLabel>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="100%" MaxLength="50"></telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadButton ID="cbEstatus" runat="server" AutoPostBack="false" ButtonType="LinkButton" ToggleType="CheckBox" Checked="true">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckboxChecked" Text="Activo" Value="1" />
                                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleCheckbox" Text="Inactivo" Value="0" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>

                <div class="menu-container">
                    <div class="menu-section">
                        <asp:Label ID="lblMenuDisponible" runat="server" Text="Menú Disponible" CssClass="menu-title"></asp:Label>
                        <div class="menu-box">
                            <telerik:RadTreeView ID="rtvMenu" runat="server" CheckBoxes="True" CheckChildNodes="True"
                                MultipleSelect="True" TriStateCheckBoxes="True" OnNodeCheck="rtvMenu_NodeCheck" OnNodeDataBound="rtvMenu_NodeDataBound">
                            </telerik:RadTreeView>
                        </div>
                    </div>

                    <div class="menu-section">
                        <asp:Label ID="lblMenuAsignado" runat="server" Text="Menú Asignado" CssClass="menu-title"></asp:Label>
                        <div class="menu-box">
                            <telerik:RadTreeView ID="rtvMenuAsignado" runat="server" CheckChildNodes="True"
                                MultipleSelect="True" TriStateCheckBoxes="True" OnNodeDataBound="rtvMenuAsignado_NodeDataBound">
                            </telerik:RadTreeView>
                        </div>
                    </div>
                </div>

                <div class="botones-container">
                    <telerik:RadButton ID="btnCancelar" CssClass="boton" runat="server" Text="Salir sin cambios" Width="140px" OnClientClicking="salir" />
                    <telerik:RadButton ID="btnGuardar" runat="server" Text="Asignar menú a perfil" Width="140px" OnClientClicking="validaRequeridos" SingleClick="true" OnClick="btnGuardar_Click" CssClass="rbPrimaryButton" />
                </div>

            </div>

        </div>

        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" />

    </form>
</body>
</html>
