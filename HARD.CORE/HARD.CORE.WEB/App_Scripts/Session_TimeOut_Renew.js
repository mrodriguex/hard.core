function pageLoad() {

    if (typeof window.top.ContinueSession === 'function') {
        window.top.ContinueSession();
    }

};