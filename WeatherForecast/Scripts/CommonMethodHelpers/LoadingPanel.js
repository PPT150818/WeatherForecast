var loadingPanel;
$(function () {
    setTimezoneCookie();
    //Loading panel 
    loadingPanel = $('#loadingPanelContainer').dxLoadPanel({
        shadingColor: "rgba(0,0,0,0.4)",
        visible: false,
        showIndicator: true,
        showPane: true,
        shading: true,
        closeOnOutsideClick: false,
    }).dxLoadPanel("instance");
});

function setTimezoneCookie() {
    try {
        
        var timezone_cookie = "timezoneoffset";

        // if the timezone cookie not exists create one.
        if (!$.cookie(timezone_cookie)) {

            // check if the browser supports cookie
            var test_cookie = 'test cookie';
            $.cookie(test_cookie, true);

            // browser supports cookie
            if ($.cookie(test_cookie)) {

                // delete the test cookie
                $.cookie(test_cookie, null);

                // create a new cookie 
                $.cookie(timezone_cookie, new Date().getTimezoneOffset());

                // re-load the page
                //location.reload();
            }
        }
        // if the current timezone and the one stored in cookie are different
        // then store the new timezone in the cookie and refresh the page.
        else {

            var storedOffset = parseInt($.cookie(timezone_cookie));
            var currentOffset = new Date().getTimezoneOffset();

            // user may have changed the timezone
            if (storedOffset !== currentOffset) {
                $.cookie(timezone_cookie, new Date().getTimezoneOffset());
            }
        }
    } catch (err) {
        HandleError(err);
    }
}

function showLoadingPanel(message, divId) {
    if (loadingPanel) {
        if (divId != null && divId != undefined && divId != "") {
            var divName = '#' + divId;
            loadingPanel._options.position = { of: divName };
        }

        if (message != null && message != undefined && message != "")
            loadingPanel.option("message", message);
        else
            loadingPanel.option("message", "Loading....");

        loadingPanel.show();
    }
}

function showSavingPanel() {
    showLoadingPanel("Saving your changes...");
}

function hideLoadingPanel() {
    if (loadingPanel) {
        loadingPanel.hide();
        loadingPanel._options.position = { at: "center", my: "center" };
    }
}