var redirect;
function AjaxRequest(requestType, controller, action, area, data, showLoadingMsg, successCallback, errorCallback, completeCallback, displayErrorAlert, async) {
    displayErrorAlert = typeof displayErrorAlert !== 'undefined' ? displayErrorAlert : false;
    async = typeof async !== 'undefined' ? async : true;
    var url = "/" + controller + "/" + action + "/";
    if (area)
        url = "/" + area + url;
    $.ajax({
        type: requestType,
        url: url,
        data: data,
        async: async,
        beforeSend: function () {
            if (showLoadingMsg) {
                if (action.toLowerCase().indexOf("save") >= 0)
                    showSavingPanel();
                else
                    showLoadingPanel();
            }
        }
    }).done(function (data, textStatus, jqXHR) {
        if (data.Success === false) {
            ShowPopupMessage(data.ErrorMessage, data.ErrorTitle);
        }
        else if (successCallback != null && typeof successCallback !== 'undefined')
            successCallback(data, textStatus, jqXHR);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        if (displayErrorAlert)
            alert("Status: " + jqXHR.status + "\nError: " + errorThrown);
    }).always(function (data, textStatus, jqXHR) {
        if (showLoadingMsg)
            hideLoadingPanel();

        if (completeCallback != null && typeof completeCallback !== 'undefined')
            completeCallback(data, textStatus, jqXHR);
    });
}

function HandleError(err) {
    alert("Error : " + err.message);
}

function AjaxRequestError(jqXHR, textStatus, errorThrown) {
    try {
        ShowPopupMessage("Error !!\n" + "Status: " + jqXHR.status + "\nError: " + errorThrown + "\nText Status: " + textStatus);
    }
    catch (err) {
        HandleError(err);
    }
}