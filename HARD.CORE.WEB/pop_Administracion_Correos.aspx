<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pop_Administracion_Correos.aspx.cs" Inherits="pop_Administracion_Correos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Administración de correos</title>
    <base target="_self" />
    <link href="App_Themes/Estilo_Cryoinfra.css" rel="stylesheet" />
    <script src="App_Scripts/Cryoinfra.js" type="text/javascript"></script>
</head>

<body>

    <form id="frmCorreos" runat="server" defaultbutton="btnGuardar">

        <asp:ScriptManager ID="sManager" runat="server">
        </asp:ScriptManager>

        <telerik:RadAjaxManager ID="raManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rcbTipoCorreo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lsVariables" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="reCuerpo" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadScriptBlock ID="rsBlock" runat="server">

            <style type="text/css">
                .Combo {
                    padding-left: 10px !important;
                }
            </style>

            <script type="text/javascript">

                function salir(sender, args) {

                    self.close();

                }

                function copiarCorreos() {

                    var combo = $find("<%= rcbUsuarios.ClientID %>");
                    var txtUsuarios = $find("<%= txtUsuarios.ClientID %>");

                    var values = "";
                    var items = combo.get_checkedItems();

                    for (var i = 0; i < items.length; i++) {
                        values += items[i].get_value() + ";";
                    }

                    txtUsuarios.set_value(values);

                }

                function ValidaRequeridos(sender, args) {

                    var txtTitulo = $find('<%= txtTitulo.ClientID%>');
                    var txtSubtitulo = $find('<%= txtSubtitulo.ClientID%>');
                    var txtAsunto = $find('<%= txtAsunto.ClientID%>');
                    var txtPie = $find('<%= txtPie.ClientID%>');
                    var rcbTipoCorreo = $find('<%= rcbTipoCorreo.ClientID%>');
                    var editor = $find("<%= reCuerpo.ClientID%>");

                    var accion = queryString("Accion");
                    var mensaje = "Se encuentra seguro de ingresar correo";

                    if (accion == 2) {
                        mensaje = "Se encuentra seguro de actualizar correo";
                    }

                    if (!validar_Textbox(txtAsunto, "Favor de ingresar asunto del correo")) {
                        args.set_cancel(true);
                    } else if (!validar_Textbox(txtTitulo, "Favor de ingresar título del correo")) {
                        args.set_cancel(true);
                    } else if (!validar_NumericTextbox(txtSubtitulo, "Favor de ingresar subtítulo de correo")) {
                        args.set_cancel(true);
                    } else if (!validar_NumericTextbox(txtPie, "Favor de ingresar pie de correo")) {
                        args.set_cancel(true);
                    } else if (!validar_Combo(rcbTipoCorreo, 0, "Favor de seleccionar tipo de correo")) {
                        args.set_cancel(true);
                    } else if (editor.get_text().trim() == "") {
                        alert("Favor de ingresar cuerpo del correo");
                        args.set_cancel(true);
                    } else if (!confirm(mensaje)) {
                        args.set_cancel(true);
                    }

                }

                function OnClientNodeDragStart() {

                    setOverlayVisible(true);

                }

                function OnClientNodeDropping(sender, args) {

                    var editor = $find("<%=reCuerpo.ClientID%>");

                    var event = args.get_domEvent();

                    document.body.style.cursor = "default";

                    var result = isMouseOverEditor(editor, event);

                    if (result) {

                        var itemValue = sender.get_selectedItem().get_value();

                        editor.setFocus();

                        editor.pasteHtml(itemValue);

                    }

                    setOverlayVisible(false);

                }

                function OnClientNodeDragging(sender, args) {

                    var editor = editor = $find("<%=reCuerpo.ClientID%>");

                    var event = args.get_domEvent();

                    if (isMouseOverEditor(editor, event)) {

                        document.body.style.cursor = "hand";

                    }

                    else {

                        document.body.style.cursor = "no-drop";

                    }

                }

                var shimId = null;

                function setOverlayVisible(toShow) {

                    if (!shimId) {

                        var div = document.createElement("DIV");

                        document.body.appendChild(div);

                        shimId = new Telerik.Web.UI.ModalExtender(div);

                    }



                    if (toShow) shimId.show();

                    else shimId.hide();

                }

                function isMouseOverEditor(editor, events) {

                    var editorFrame = editor.get_contentAreaElement();

                    var editorRect = $telerik.getBounds(editorFrame);

                    var mouseX = events.clientX;

                    var mouseY = events.clientY;


                    if (mouseX < (editorRect.x + editorRect.width) &&

                        mouseX > editorRect.x &&

                        mouseY < (editorRect.y + editorRect.height) &&

                        mouseY > editorRect.y) {

                        return true;

                    }

                    return false;

                }

                function OnClientItemDoubleClicked(sender, args) {

                    var editor = editor = $find("<%=reCuerpo.ClientID%>");

                    var itemValue = sender.get_selectedItem().get_value();

                    editor.setFocus();

                    editor.pasteHtml(itemValue);

                }

                function salir(sender, args) {

                    if (sender == null) {
                        getRadWindow().BrowserWindow.refreshGrid();
                    }
                    getRadWindow().close();

                }

            </script>

        </telerik:RadScriptBlock>

        <div class="modal" style="width: 1020px">

            <div class="titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="Alta de correo"></asp:Label>
            </div>

            <div class="contenido" id="Contenido" runat="server">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 120px">
                                        <telerik:RadLabel ID="lblUsuario" AssociatedControlID="rcbUsuarios" runat="server" Text="Para :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox DropDownWidth="410px" ID="rcbUsuarios" runat="server" Width="310px" EmptyMessage="Agregar correos" OnClientItemChecked="copiarCorreos" CheckBoxes="true" Height="300px" CheckedItemsTexts="DisplayAllInInput"></telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txtUsuarios" TextMode="MultiLine" Rows="4" runat="server" EmptyMessage="Correos agregados" Width="440px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="lblPrioridad" AssociatedControlID="rblImportancia" runat="server" Text="Prioridad :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblImportancia" runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList" Width="200px">
                                            <asp:ListItem Value="Normal" Text="Normal" Selected="True">
                                            </asp:ListItem>
                                            <asp:ListItem Value="Low" Text="Baja">                                                
                                            </asp:ListItem>
                                            <asp:ListItem Value="High" Text="Alta">
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 90%; padding-left: 15px">
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="lblAsunto" AssociatedControlID="txtAsunto" runat="server" Text="Asunto :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtAsunto" runat="server" Width="340px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="lblTitulos" AssociatedControlID="txtTitulo" runat="server" Text="Título :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtTitulo" runat="server" Width="340px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="lblSubtitulo" AssociatedControlID="txtTitulo" runat="server" Text="Subtítulo :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSubtitulo" runat="server" Width="340px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="lblPie" AssociatedControlID="txtPie" runat="server" Text="Pie :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPie" runat="server" Width="340px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="lblTipoCorreo" AssociatedControlID="rcbTipoCorreo" runat="server" Text="Tipo de correo :"></telerik:RadLabel>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rcbTipoCorreo" DropDownAutoWidth="Enabled" runat="server" Width="340px" EmptyMessage="Seleccione un tipo" AutoPostBack="True"
                                            OnSelectedIndexChanged="rcbTipoCorreo_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="margin-top: 35px">
                                <tr>
                                    <td style="width: 700px">
                                        <telerik:RadEditor EmptyMessage="Cuerpo del correo" ID="reCuerpo" runat="server" Width="100%" EnableResize="False" Height="360px">
                                            <Tools>
                                                <telerik:EditorToolGroup>
                                                    <telerik:EditorTool Name="Bold"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="Italic"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="Underline"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="Cut"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="Copy"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="Paste"></telerik:EditorTool>
                                                    <telerik:EditorDropDown Name="FontName">
                                                    </telerik:EditorDropDown>
                                                    <telerik:EditorDropDown Name="RealFontSize">
                                                    </telerik:EditorDropDown>
                                                    <telerik:EditorSplitButton Name="ForeColor">
                                                    </telerik:EditorSplitButton>
                                                    <telerik:EditorSplitButton Name="BackColor">
                                                    </telerik:EditorSplitButton>
                                                </telerik:EditorToolGroup>
                                                <telerik:EditorToolGroup>
                                                    <telerik:EditorToolStrip Name="InsertTable">
                                                    </telerik:EditorToolStrip>
                                                    <telerik:EditorTool Name="Unlink"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="InsertOrderedList"></telerik:EditorTool>
                                                    <telerik:EditorTool Name="InsertUnorderedList"></telerik:EditorTool>
                                                </telerik:EditorToolGroup>
                                            </Tools>
                                            <Content>                                                                                       
                                            </Content>
                                            <ImageManager ViewPaths="~/images" DeletePaths="~/images" UploadPaths="~/images" />
                                            <TrackChangesSettings CanAcceptTrackChanges="False" />
                                        </telerik:RadEditor>
                                    </td>
                                    <td style="vertical-align: top; width: 240px">
                                        <div style="margin-left: 6px; padding-left: 6px; height: 360px; border: 1px solid; border-color: #688caf; background-color: #D6E6F4;">
                                            <div class="Subtitulo">
                                                Variables:
                                            </div>
                                            <telerik:RadListBox runat="server" ID="lsVariables" Width="220px" Height="300px" Font-Size="14px" EnableDragAndDrop="true"
                                                OnClientDragging="OnClientNodeDragging"
                                                OnClientDropping="OnClientNodeDropping"
                                                OnClientDragStart="OnClientNodeDragStart"
                                                OnClientItemDoubleClicked="OnClientItemDoubleClicked">
                                            </telerik:RadListBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>

            <div style="margin: auto; padding-top: 20px;">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar correo" UseSubmitBehavior="false" Width="120px" OnClientClicking="ValidaRequeridos" OnClick="btnGuardar_Click" />
                &nbsp;
                <telerik:RadButton ID="btnSalir" runat="server" Text="Salir sin cambios" Width="120px" OnClientClicking="salir" />
            </div>

            <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server">
            </telerik:RadAjaxLoadingPanel>

        </div>

    </form>
</body>
</html>
