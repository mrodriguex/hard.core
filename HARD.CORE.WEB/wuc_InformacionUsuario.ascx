<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_InformacionUsuario.ascx.cs" Inherits="wuc_InformacionUsuario" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<div class="d-flex flex-row p-1">
    <div class="d-flex flex-column">
        <telerik:RadLabel ID="lblNombreUsuario" runat="server" Font-Bold="true" Font-Size="16px" Width="250px"></telerik:RadLabel>
        <telerik:RadLabel ID="lblCorreo" runat="server" Font-Size="14px" Width="250px"></telerik:RadLabel>
        <telerik:RadLabel ID="lblNumeroEmpleado" runat="server" Font-Size="14px" Width="250px"></telerik:RadLabel>
        <asp:HyperLink ID="btnCambioContrasenia" runat="server" NavigateUrl="~/frm_AdministracionCambioPassword.aspx" Text="Cambio de contraseña" Style="word-break: break-all;" CssClass="ms-3"></asp:HyperLink>
    </div>

    <div class="text-end">
        <telerik:RadButton ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" OnClick="btnCerrarSesion_Click" RenderMode="Lightweight"></telerik:RadButton>
    </div>
</div>
