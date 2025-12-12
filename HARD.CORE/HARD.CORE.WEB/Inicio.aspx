<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="frm_Administracion_CarrucelNoticias" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptBlock ID="rsBlock" runat="server">
        <style>
            .RadRotator {
                margin: 0 0 0 0 !important;
                z-index: 10 !important;
            }

            /*.radrotator-efecto-moderno .rrItem img {
                border: none !important;
                border-radius: 100px;*/
                /*animation: respiracion 3s ease-in-out infinite;
                box-shadow: 0 10px 35px rgba(0,0,0,0.55);*/
            /*}*/

            .RadRotator .rrItemsList {
                z-index: 15 !important;
            }

            .EstilosCarrusel {
                background-color: #ffffff !important;
                z-index: 1000 !important;
            }

            .RadRotator,
            .RadRotator .rrClipRegion,
            .RadRotator .rrItemsList {
                border: none !important;
                background: transparent !important;
                padding: 0 !important;
            }

            .RadRotator .rrButton {
                border-radius: 14px !important;
                background: linear-gradient(-45deg, #26669E, #3a8ccc, #4facfe, #26669E) !important;
                background-size: 400% 400% !important;
                animation: gradient-shift 3s ease infinite !important;
                width: 40px !important;
                height: 40px !important;
                transition: all 0.3s ease !important;
            }

            @keyframes gradient-shift {
            0%, 100% {
                background-position: 0% 50%;
            }

            50% {
                background-position: 100% 50%;
            }
            }

            .RadRotator .rrButton:hover {
                animation-duration: 1s !important;
                box-shadow: 0 10px 30px rgba(38, 102, 158, 0.6) !important;
            }
        </style>
        <script type="text/javascript">

            (function (global, undefined) {
                function OnClientItemClicked(rotator, args) {
                    rotator.set_currentItemIndex(args.get_item().get_index(), true);
                }

                global.OnClientItemClicked = OnClientItemClicked;
            })(window);

        </script>

    </telerik:RadScriptBlock>

    <div class="catalogo">
        <div class="contenido">
            <div class="radrotator-efecto-moderno" style="width: 90%; margin: 0 auto;">
                <telerik:RadRotator RenderMode="Lightweight" ID="CImagenes" runat="server"
                    Width="100%" ItemWidth="650"
                    Height="800px" ItemHeight="400"
                    ScrollDuration="500" FrameDuration="2000" PauseOnMouseOver="false"
                    RotatorType="CarouselButtons" OnClientItemClicked="OnClientItemClicked" RandomAnimation="true">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server"
                            ImageUrl='<%# Container.DataItem %>'
                            Style="width: 100%; height: 100%; object-fit: cover; margin-top: -10%;"></asp:Image>
                    </ItemTemplate>
                </telerik:RadRotator>

                <div id="cardMensaje" class="card text-center mx-auto shadow-lg border-0" style="box-shadow: 0 10px 30px rgba(38, 102, 158, 0.6); background-color: #26669E; border-radius: 20px; max-width: 600px; margin-top: 17%; padding: 40px;" runat="server" visible="false">
                    <i class="fa-solid fa-book-open-reader fa-bounce fa-2xl" style="color: #c5bebe;"></i>
                    <div class="card-body">
                        <h5 class="card-title fw-bold mb-3" style="color: white;">Aún no hay noticias, pero algo interesante viene en camino.</h5>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
