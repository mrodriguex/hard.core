// -----------------------------------------------------------------------------------------------
// RadTextBox
// -----------------------------------------------------------------------------------------------

function validar_Textbox(sender, mensaje) {
    
    if (sender.get_textBoxValue().trim() === "") {
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}
function validar_TextboxToastify(sender, mensaje) {

    if (sender.get_textBoxValue().trim() === "") {

        Toastify({
            text: mensaje,
            duration: 3000,
            gravity: "center",  // ← SOLO ESTA LÍNEA
            position: "center", // ← Y ESTA LÍNEA
            style: {
                background: "var(--MoradoHARDCORE, #ffc107)",
                color: "white"
            }
        }).showToast();


        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}
function validar_TextboxE(sender, mensaje) {

    if (sender.isEmpty() || sender.get_textBoxValue().trim() === "") {
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_TextboxC(sender, mensaje, valida) {

    if (valida === true) {

        if (sender.get_textBoxValue().trim() === "") {
            alert(mensaje);
            sender.focus();
            sender.clear();
            return false;
        }

    }

    return true;

}

function validar_Textbox_Tab(sender, mensaje, tabstrip, index) {

    if (sender.get_textBoxValue().trim() === "") {
        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_Textbox_Wizard(sender, mensaje, rdWiz, activeIndex) {

    if (sender.get_textBoxValue().trim() === "") {
        rdWiz.set_activeIndex(activeIndex);
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_TextboxC_Tab(sender, mensaje, valida, tabstrip, index) {

    if (sender.get_textBoxValue().trim() === "" && valida === true) {
        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_TextboxSoloNumeros(txtSoloNumeros) {

    var reg = /^([0-9])+$/;
    return reg.test(txtSoloNumeros);
}

function validar_TextboxSoloNumerosC(txtSoloNumeros, valida) {

    var reg = /^([0-9])+$/;
    if ((!reg.test(txtSoloNumeros)) && valida === true) {
        return false;
    }

    return true;
}

function validar_TextboxSoloLetrasEspacios(txtSoloLetrasEspacios) {
    var reg = /^[\p{L}\s]+$/u;
    return reg.test(txtSoloLetrasEspacios);
}

function validar_TextboxSoloLetrasEspaciosC(txtSoloLetrasEspacios, valida) {
    var reg = /^[\p{L}\s]+$/u;
    if ((!reg.test(txtSoloLetrasEspacios)) && valida === true) {
        return false;
    }

    return true;
}

function validar_TextboxTextoCopiadoLetrasEspacios(txtTextoCopiado) {
    var reg = /^[\p{L}\s]+$/u;
    return reg.test(txtTextoCopiado);
}

function validar_TextboxTextoCopiadoLetrasEspaciosC(txtTextoCopiado, valida) {
    var reg = /^[\p{L}\s]+$/u;
    if ((!reg.test(txtTextoCopiado)) && valida === true) {
        return false;
    }

    return true;
}


// -----------------------------------------------------------------------------------------------
// RadNumericTextBox
// -----------------------------------------------------------------------------------------------

function validar_NumericTextbox_Wizard(sender, mensaje, rdWiz, activeIndex) {

    if (sender.get_value() === "") {
        rdWiz.set_activeIndex(activeIndex);
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;
}

function validar_NumericTextbox_Tab(sender, mensaje, tabstrip, index) {

    if (sender.get_value() === "") {
        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_NumericTextboxC_Tab(sender, mensaje, valida, tabstrip, index) {

    if (sender.get_value() === "" && valida === true) {
        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_NumericTextboxC_TabVI(sender, valorInvaliado, mensaje, valida, tabstrip, index) {

    if ((sender.get_value() === "" || sender.get_value() === valorInvaliado) && valida === true) {
        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_NumericTextbox(sender, mensaje) {

    if (sender.get_value() === "") {
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_NumericTextboxC(sender, mensaje, valida) {

    if (sender.get_value() === "" && valida === true) {
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

function validar_NumericTextboxCE(sender, valorInvalido, mensaje, valida) {

    if ((sender.get_value() === "" || sender.get_value() === valorInvalido) && valida === true) {
        alert(mensaje);
        sender.focus();
        sender.clear();
        return false;
    }

    return true;

}

// -----------------------------------------------------------------------------------------------
// RadComboBox
// -----------------------------------------------------------------------------------------------

function validar_Combo(sender, valorInvalido, mensaje) {

    if (sender.get_value() === valorInvalido) {
        Toastify({
            text: mensaje,
            duration: 3000,
            gravity: "center",  // ← SOLO ESTA LÍNEA
            position: "center", // ← Y ESTA LÍNEA
            style: {
                background: "var(--MoradoHARDCORE, #ffc107)",
                color: "white"
            }
        }).showToast();
        sender.showDropDown();
        return false;
    }

    return true;

}

function validar_ComboC(sender, valorInvalido, mensaje, valida) {

    if (valida === true || valida === 'true') {

        if (sender.get_value() === valorInvalido) {
            alert(mensaje);
            sender.showDropDown();
            return false;
        }

    }

    return true;

}

function validar_Combo_Tab(sender, valorInvalido, mensaje, tabstrip, index) {

    if (sender.get_value() === valorInvalido) {

        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.showDropDown();
        return false;
    }

    return true;

}

function validar_ComboC_Tab(sender, valorInvalido, mensaje, valida, tabstrip, index) {

    if (valida === true || valida === 'true') {

        if (sender.get_value() === valorInvalido) {
            tabstrip.get_tabs().getTab(index).click();
            alert(mensaje);
            sender.showDropDown();
            return false;
        }

    }

    return true;

}

function validar_Combo_Wizard(sender, valorInvalido, mensaje, rdWiz, activeIndex) {

    if (sender.get_value() === valorInvalido) {

        rdWiz.set_activeIndex(activeIndex);
        alert(mensaje);
        sender.showDropDown();
        return false;
    }

    return true;

}

// -----------------------------------------------------------------------------------------------
// RadDatePicker / TimePicker
// -----------------------------------------------------------------------------------------------

function validar_DatePicker(sender, mensaje) {

    if (sender.get_selectedDate() === null) {
        alert(mensaje);
        sender.showPopup();
        return false;
    }

    return true;

}

function validar_DatePickerC(sender, mensaje, valida) {

    if (valida) {

        if (sender.get_selectedDate() === null) {
            alert(mensaje);
            sender.showPopup();
            return false;
        }

    }

    return true;

}

function validar_DatePicker_Tab(sender, mensaje, tabstrip, index) {

    if (sender.get_selectedDate() === null) {
        tabstrip.get_tabs().getTab(index).click();
        alert(mensaje);
        sender.showPopup();
        return false;
    }

    return true;

}

function validar_DatePicker_Wizard(sender, mensaje, rdWiz, activeIndex) {

    if (sender.get_selectedDate() === null) {
        rdWiz.set_activeIndex(activeIndex);
        alert(mensaje);
        sender.showPopup();
        return false;
    }

    return true;

}



function validar_TimePicker(sender, mensaje) {

    if (sender.get_selectedDate() === null) {
        alert(mensaje);
        sender.showTimePopup();
        return false;
    }

    return true;

}

function validar_MonthPickerC(sender, valorInvalido, mensaje, valida) {

    if (valida === true || valida === 'true') {
        if (sender._validationInput.value === valorInvalido) {
            alert(mensaje);
            sender.get_focusedDate();
            sender.showPopup();
            return false;
        }
    }
    return true;

}

function validar_MonthPicker_Tab(sender, valorInvalido, mensaje, valida, tabstrip, index) {

    if (valida === true || valida === 'true') {
        if (sender._validationInput.value === valorInvalido) {
            tabstrip.get_tabs().getTab(index).click();
            alert(mensaje);
            sender.get_focusedDate();
            sender.showPopup();
            return false;
        }
    }
    return true;

}

// -----------------------------------------------------------------------------------------------
// RadWindow
// -----------------------------------------------------------------------------------------------
var RadWindowManagerAlto = null;
var RadWindowManagerAncho = null;
var RadWindowManagerObjeto = null;

function getRadWindow() {

    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;

}

function centerActiveRadWindow(isBrowserWindow) {

    var windowManager = null;

    if (isBrowserWindow) {
        windowManager = GetRadWindowManager();
    } else {
        windowManager = getRadWindow().BrowserWindow.GetRadWindowManager()
    }

    if (windowManager) {

        var eWindow = windowManager.getActiveWindow();

        if (eWindow) {
            eWindow.center();
        }
    }

}

function resizeRadWindow(windowName, width, heigth, isBrowserWindow) {

    var windowManager = null;

    if (isBrowserWindow) {
        windowManager = GetRadWindowManager();
    } else {
        windowManager = getRadWindow().BrowserWindow.GetRadWindowManager()
    }

    if (windowManager) {

        var eWindow = windowManager.getWindowById(windowName);

        if (eWindow) {
            eWindow.setSize(width, heigth);
        }
    }

}

function openRadWindow(url, windowName, width, heigth, isBrowserWindow) {
    var windowManager = null;

    if (isBrowserWindow) {
        windowManager = GetRadWindowManager();
    } else {
        windowManager = getRadWindow().BrowserWindow.GetRadWindowManager();
    }

    if (windowManager) {

        var eWindow = windowManager.getWindowById(windowName);

        hideScroll();

        window.scrollTo(0, 0);
        
        if (eWindow) {
            eWindow.set_navigateUrl(url);
            eWindow.show();
        } else {

            if (isBrowserWindow) {
                eWindow = window.radopen(url, windowName, width, heigth);
            } else {
                eWindow = getRadWindow().BrowserWindow.radopen(url, windowName, width, heigth);
            }

            eWindow.set_maxWidth(width);
            eWindow.set_maxHeight(heigth);

        }

        if (eWindow) {            
            eWindow.add_close(closeHandler);
            RadWindowManagerAlto = heigth;
            RadWindowManagerAncho = width;
            RadWindowManagerObjeto = eWindow;
            var anchoPagina = window.outerWidth;
            var altoPagina = window.outerHeight - 50;            
            RadWindowManagerObjeto.SetWidth(Math.min(RadWindowManagerAncho, anchoPagina));
            RadWindowManagerObjeto.SetHeight(Math.min(RadWindowManagerAlto, altoPagina));
        }

    }

}

function closeAndRebind(args) {

    getRadWindow().BrowserWindow.refreshGrid();
    getRadWindow().close();
    RadWindowManagerObjeto = null;
    RadWindowManagerAlto = null;
    RadWindowManagerAncho = null;

}

function closeHandler(sender, args) {
    //restore the overflow   
    document.body.style.overflow = "visible";
    document.documentElement.style.overflow = "visible";
    sender.remove_close(closeHandler);
    RadWindowManagerObjeto = null;
    RadWindowManagerAlto = null;
    RadWindowManagerAncho = null;
}

function refreshParentPage() {
    getRadWindow().BrowserWindow.location.reload();
}

function hideScroll() {
    //store the overflow   
    bodyOverflow = document.body.style.overflow;
    htmlOverflow = document.documentElement.style.overflow;
    //hide the overflow   
    document.body.style.overflow = "hidden";
    document.documentElement.style.overflow = "hidden";
}

// -----------------------------------------------------------------------------------------------
// Generales
// -----------------------------------------------------------------------------------------------

function validar_Correo(email) {

    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    return reg.test(email);

}

function modal(url, ancho, alto, postback) {

    window.showModalDialog(url, 'Modal', 'dialogWidth:' + ancho + 'px;dialogHeight:' + alto + 'px;center:yes;status:no;autoSize:yes;');
    return postback;

}

function queryString(name) {

    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results === null)
        return -1;
    else
        return results[1];

}

function abrirLB(lightBox, url, width, heigth) {

    var items = lightBox.get_items();
    var item = items.getItem(0);

    item.set_navigateUrl(url);
    item.set_width(width);
    item.set_height(heigth);

    lightBox.show();

}

function abrirMapaCordenadas(latitud, longitud) {

    var x = screen.width / 2 - 1100 / 2;
    var y = screen.height / 2 - 800 / 2;

    var strWindowFeatures = "width=1100,height=800,resizable=yes,left=" + x + ",top=" + y;
    window.open('http://www.google.com.mx/maps/place/' + latitud + ',' + longitud, "_blank", strWindowFeatures);

}

function abrirMapaDireccion(cadena) {

    var x = screen.width / 2 - 1100 / 2;
    var y = screen.height / 2 - 800 / 2;

    var strWindowFeatures = "width=1100,height=800,resizable=yes,left=" + x + ",top=" + y;
    window.open('http://www.google.com.mx/maps/place/' + cadena, "_blank", strWindowFeatures);

}

function seleccionarTodo(sender, args) {
    sender.selectAllText();
}

function validaNumeros(sender, args) {

    if (isNaN(String.fromCharCode(event.keyCode))) {
        args.set_cancel(true);
    }

}

function noback() {
    window.location.hash = "NB";
    window.location.hash = "Again-No-back-button"; //chrome
    window.onhashchange = function () { window.location.hash = ""; };
}

// -----------------------------------------------------------------------------------------------
// RadioButtonList
// -----------------------------------------------------------------------------------------------

function validar_RadioButton(sender, mensaje) {

    var seleccionado = false;
    var inputs = null;
    if (sender !== null) {
        inputs = sender.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                seleccionado = true;
            }
        }
    }
    if (!seleccionado) {
        alert(mensaje);
        return false;
    }

    return true;

}

function delayedRedirect() {
    window.location = "/default.aspx"
}

function validar_Editor(sender, mensaje) {
    if (sender.get_text().trim() == "") {
        alert(mensaje);
        sender.setFocus(true);
        return false;
    }
    return true;
}

function validar_EditorC(sender, mensaje, valida) {
    if (sender != null) {
        if (sender.get_text().trim() == "" && valida == true) {
            alert(mensaje);
            sender.setFocus(true);
            return false;
        }
    }
    return true;
}

var coll = document.getElementsByClassName("filterButton");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = 100 + "px";
        }
    });
}

function OpenPopOnFocusRadComboBox(sender, args) {

    var radComboBox = $find(sender.get_id());
    radComboBox.showDropDown();

}

