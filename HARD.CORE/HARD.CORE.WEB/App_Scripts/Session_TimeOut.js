var mainLblCounter = null;
var timeLeftCounter = null;
var seconds = 60;
var secondsBeforeShow = 180;

//start the main label counter when the page loads
function pageLoad() {

    resetTimer("mainLblCounter", updateMainLabel, 1000);

    var notification = getNotification();

    var showIntervalStorage = notification.get_showInterval(); //store the original value
    notification.set_showInterval(0); //change the timer to avoid untimely showing, 0 disables automatic showing
    notification.set_showInterval(showIntervalStorage);

};

//stop timers for UI 
function stopTimer(timer) {

    clearInterval(this[timer]);
    this[timer] = null;

};

//reset timers for UI
function resetTimer(timer, func, interval) {

    this.stopTimer(timer);
    this[timer] = setInterval(Function.createDelegate(this, func), interval);

};

function notification_Showing(sender, args) {

    showModalDiv();
    resetTimer("timeLeftCounter", UpdateTimeLabel, 1000);
    stopTimer("mainLblCounter");

}

function notification_Hidden() {

    updateMainLabel(true);
    resetTimer("mainLblCounter", updateMainLabel, 1000);

}

function updateMainLabel(toReset) {

    secondsBeforeShow = (toReset === true) ? 60 : secondsBeforeShow - 1;

}

function UpdateTimeLabel(toReset) {

    var sessionExpired = (seconds === 2);

    if (sessionExpired) {

        stopTimer("timeLeftCounter");
        alert("Tu sesión ha expirado");
        window.location.href = getNotification().get_value();

    } else {

        var timeLbl = $get("timeLbl");
        seconds--;
        timeLbl.innerHTML = seconds - 1;
        
    }

}

function ContinueSession() {

    var notification = getNotification();

    //we need to contact the server to restart the Session - the fastest way is via callback
    //calling update() automatically performs the callback, no need for any additional code or control

    notification.update();
    notification.hide();

    //resets the showInterval for the scenario where the Notification is not disposed (e.g. an AJAX request is made)
    //You need to inject a call to the ContinueSession() function from the code behind in such a request

    var showIntervalStorage = notification.get_showInterval(); //store the original value
    notification.set_showInterval(0); //change the timer to avoid untimely showing, 0 disables automatic showing
    notification.set_showInterval(showIntervalStorage); //sets back the original interval which will start counting from its full value again

    stopTimer("timeLeftCounter");
    seconds = 60;
    updateMainLabel(true);
    hideModalDiv();    

}

function parentExists() {

    var opener = window.dialogArguments;
    return (opener === null) ? false : true;

}

function getNotification() {

    var notification = $find("Notificacion")

    if (notification === null) {
        notification = $find("ctl00_Notificacion");
    }

    return notification;
}


var modalDiv = null;

function showModalDiv(sender, args) {

    if (!modalDiv) {
        modalDiv = document.createElement("div");
        modalDiv.style.width = "100%";
        modalDiv.style.height = "100%";
        modalDiv.style.backgroundColor = "#AAAAAA";
        modalDiv.style.position = "absolute";
        modalDiv.style.left = "0px";
        modalDiv.style.top = "0px";
        modalDiv.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=80)";
        modalDiv.style.opacity = ".8";
        modalDiv.style.MozOpacity = ".8";
        modalDiv.style.zIndex = 9999
        modalDiv.setAttribute("unselectable", "on");
        document.body.appendChild(modalDiv);
    }

    modalDiv.style.display = "";

}

function hideModalDiv() {

    if (modalDiv != null) {
        modalDiv.style.display = "none";
    }    

}