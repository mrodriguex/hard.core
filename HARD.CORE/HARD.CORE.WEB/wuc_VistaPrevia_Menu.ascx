<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_VistaPrevia_Menu.ascx.cs" Inherits="wuc_VistaPrevia_Menu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="margin:2px">
    <div class="tooltipTitulo">
        Menú Asignado
    </div>
    <div style="text-align: left">
        <telerik:RadTreeView ID="rtvMenu" runat="server">
        </telerik:RadTreeView>
    </div>
</div>
