<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NoAutorizado.aspx.cs" Inherits="NoAutorizado" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenido" runat="Server">
    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <script type="text/javascript">
            window.onload = function () {
                setTimeout(delayedRedirect, 4000);;
            };

            function delayedRedirect() {
                window.location = "frm_Administracion_CarrucelNoticias.aspx"
            }
        </script>
    </telerik:RadScriptBlock>

    <div class="catalogo" style="width: 600px">
        <div class="titulo" style="padding-top: 50px"></div>

        <div style="padding-bottom: 20px; text-align: left" class="SubtituloWizard">
            <table>
                <tr>
                    <td>
                        <img src="App_Resources/Imagenes/Sitio/Oops.png" />
                    </td>
                    <td style="vertical-align: top; padding-left: 20px;">
                        <table>
                            <tr>
                                <td class="Titulo">Lo sentimos / No esta autorizado
                                </td>
                            </tr>
                            <tr>
                                <td class="SubtituloWizard" style="text-align: left">No esta autorizado para acceder a este contenido. Te estamos regiridiendo.
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>