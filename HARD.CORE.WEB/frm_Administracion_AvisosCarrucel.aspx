<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frm_Administracion_AvisosCarrucel.aspx.cs" Inherits="frm_Administracion_AvisosCarrucel" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="frm_Administracion_CarrucelNoticias" ContentPlaceHolderID="contenido" runat="Server">

    <telerik:RadAjaxManager ID="raManager" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptBlock ID="rsBlock" runat="server">

        <script type="text/javascript">
            function onRotatorClientLoad(sender, args) {
                var rotator = sender;
                var itemCount = rotator.get_items().length;
                var intervaloId = null;

                function avanzarRotador() {
                    var currentIndex = rotator.get_currentItemIndex();
                    var currentItem = rotator.get_items()[currentIndex].get_element();
                    var currentImg = currentItem.querySelector("img");

                    setTimeout(function () {
                        var nextIndex = (currentIndex + 1) % itemCount;
                        rotator.set_currentItemIndex(nextIndex);

                        setTimeout(function () {
                            var nextItem = rotator.get_items()[nextIndex].get_element();
                            var nextImg = nextItem.querySelector("img");

                        }, 100);
                    }, 300);
                }
                // Iniciar el intervalo y guardar el id
                intervaloId = setInterval(avanzarRotador, 10000);
                avanzarRotador();

                // Funciones para detener y continuar
                function pausarRotador() {
                    if (intervaloId !== null) {
                        clearInterval(intervaloId);
                        intervaloId = null;
                    }
                }

                function continuarRotador() {
                    if (intervaloId === null) {
                        intervaloId = setInterval(avanzarRotador, 10000);
                    }
                }

                // Agregar eventos mouseenter y mouseleave sobre cada imagen
                for (var i = 0; i < itemCount; i++) {
                    var item = rotator.get_items()[i].get_element();
                    var img = item.querySelector("img");
                    if (img) {
                        img.addEventListener("mouseenter", pausarRotador);
                        img.addEventListener("mouseleave", continuarRotador);
                    }
                }
            }

        </script>

        <style type="text/css">
            .coverFlowItem {
                width: 660px;
                height: 507px;
                background-color: rgba(255, 255, 255, 0.7) !important; /* Blanco con 70% de opacidad */
                border-radius: 8px;
                border-color: black;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                display: flex;
                align-items: center;
                justify-content: center;
                font-family: Arial;
                font-size: 16px;
                color: #333;
                transition: all 0.3s ease;
                padding: 5px; /* Añadido para mejor espaciado */
                margin-left: -214px;
                margin-top: -220px;
            }

            .coverFlowItem:hover {
                transform: scale(1.05);
                background-color: rgba(66, 139, 202, 0.8) !important; /* Azul con 80% de opacidad */
            }

            .currentItem {
                background-color: #356082 !important;
                color: white !important;
            }

            .rrClipRegion {
                border: none !important;
            }

            .RadRotator_Bootstrap .rrButton.rrButtonLeft, .RadRotator_Bootstrap .rrButton.rrButtonLeft:hover {
                background-color: hsl(31, 100%, 83%) !important;
            }

            .RadRotator_Bootstrap .rrButton.rrButtonRight, .RadRotator_Bootstrap .rrButton.rrButtonRight:hover {
                background-color: hsl(31, 100%, 83%)!important;   
            }
        </style>

    </telerik:RadScriptBlock>

    <div class="container">
        <div class="row align-items-center justify-content-center">
            <div class="col-auto" style="margin-left: 5% !important;">

                <telerik:RadRotator ID="radCoverFlow" runat="server" RotatorType="CoverFlowButtons" OnClientLoad="onRotatorClientLoad" Width="1000px" ItemWidth="100" Height="715px" ItemHeight="113" ScrollDuration="500" FrameDuration="10000" PauseOnMouseOver="true">
                    <ItemTemplate>
                        <div class="coverFlowItem">
                            <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ImageAlt") %>' style="width: 100%; height: 100%; object-fit: cover;" />
                        </div>
                    </ItemTemplate>
                </telerik:RadRotator>

                <telerik:RadCard ID="CardVacioCarrusel" runat="server" Orientation="Horizontal" Visible="false" Style="font-size: 2rem; color: #072e6f; border: none; padding-top: 299px;">
                    <div class="k-vbox k-column" style="display: flex; align-items: center; padding-right: 24px;">
                        <div style="display: flex; justify-content: space-between; align-items: center; width: 100%;">
                            <telerik:CardBodyComponent runat="server" Style="font-weight: bold; font-size: 2.5rem; color: #072e6f;">
                                ESPERA A LOS PRÓXIMOS AVISOS
                            </telerik:CardBodyComponent>
                            <i class="fa-solid fa-exclamation fa-bounce" style="font-size: 3.5rem; color: #072e6f;"></i>
                        </div>
                    </div>
                </telerik:RadCard>
            </div>
        </div>
    </div>

    <telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>