<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Notificaciones.ascx.cs" Inherits="wuc_Notificaciones" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style>
    .lblNotificacion {
        font-size: 14px;
    }

    .listview-layout {
        max-width: 100%;
        overflow: hidden;
        overflow-y: auto;
        height: 230px;
        display: flex;
        flex-direction: column;
        box-sizing: content-box;
    }

    .listview-item {
        position: relative;
        padding: 8px;
        transition: background-color 0.2s ease-in-out;
        border-top: solid 1px;
    }

    .listview-item:hover {
        background-color: lightgray;
    }

    .notificacion-id {
        position: absolute;
        top: 5px;
        right: 10px;
        background-color: #007bff;
        color: white;
        padding: 2px 6px;
        border-radius: 12px;
        font-size: 10px;
        display: none;
    }

    .listview-item:hover .notificacion-id {
        display: inline-block;
    }

    .alert-sin-notificaciones {
        display: flex;
        height: 100%;
        justify-content: center;
        margin-top: -115px;
    }
</style>

<div>
    <telerik:RadListView ID="rlvNotificaciones" runat="server" ItemPlaceholderID="PlaceHolder1" OnNeedDataSource="rlvNotificaciones_NeedDataSource" OnDataBound="rlvNotificaciones_DataBound">
        <LayoutTemplate>
            <div class="listview-layout">
                <div class="lblNotificacion fw-bold">Notificaciones</div>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="listview-item d-flex flex-row align-items-center" style='<%# (bool)Eval("Imagen") ? "border-color: var(--naranja-ei-deg);": "border-color: var(--azul-ei);" %> <%# GetVistoPorClaveUsuario(Container.DataItem) %>' onclick="mostrarDetalleNotificacion(<%# Eval("ClaveNotificacion") %>)">
                <div style="width: 90%">
                    <div>
                        <%# string.IsNullOrEmpty(Eval("Descripcion") as string) ? "Se ha compartido un archivo de imagen." : "Se ha compartido información." %>
                    </div>
                    <div class="fw-bold pe-2" style="<%# (bool)Eval("Imagen") ? "color: var(--naranja-ei-deg);": "color: var(--azul-ei);"%>; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; width: 300px;">
                        <%# Eval("Titulo") %>
                    </div>
                    <div class="text-secondary" runat="server">
                        <%# Eval("FechaInicioVigencia", "{0:dd/MM/yyyy}") %>
                    </div>
                </div>
                <div>
                    <div style='<%# (bool)Eval("Imagen") ? "display: block;": "display:none;" %>; background-color: var(--naranja-ei-deg); padding: 6px; border-radius: 6px;'>
                        <i class="fa-solid fa-file-image text-white fa-3x" runat="server"></i>
                    </div>
                    <div style="<%# (bool)Eval("Imagen") ? "display: none;": "display:block;" %>; background-color: var(--azul-ei); padding: 6px; border-radius: 6px;">
                        <i class="fa-solid fa-message text-white fa-2x" runat="server"></i>
                    </div>
                    <div class="notificacion-id">Ver</div>
                </div>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
    <div id="alertSinNotificaciones" runat="server" class="alert-sin-notificaciones" visible="false">Sin notificaciones pendiente por ver.</div>
</div>