var GlobalApplicationName = "Weather Forecast";
function ShowPopupMessage(message, header, popupMessageMode, okCallback, cancelCallBack, showclosebutton) {
    if (header == undefined || header == "" || header == null) {
        header = GlobalApplicationName;
    }

    if (showclosebutton === undefined || showclosebutton === "" || showclosebutton === null) {
        showclosebutton = true;
    }

    if (header.toLowerCase() == "error" && message.indexOf("ModelValidationError") > -1) {
        var validationError = "<b>Following validation error(s) occured, please fix them and try again:</b><br/>";
        message = message.replace("ModelValidationError", "");
        message = validationError + "<br>" + message;
        header = "Validation Error(s)";
    }
    else
        message = message.replace("ModelValidationError", "");

    var height = 170;
    var width = 450;
    var messageLength = message.length;
    if (messageLength > 60) {
        height += (messageLength / 60) * 10;
        width = 550;
    }

    var count = message.split('<br>').length;
    if (count > 1)
        height += (count - 1) * 10;

    $("#popupMessageContainer").dxPopup({
        visible: true,
        showTitle: true,
        width: width,
        height: height,
        maxHeight: height,
        minHeight: height,
        title: header,
        showCloseButton: showclosebutton,
        onHiding: function (e) {
            if (header === "Automatic Log Out") {
                okCallback();
            }
        },
        contentTemplate: $("<div>" + message + "</div>"),
        toolbarItems: [
              {
                  toolbar: 'bottom', location: 'after', widget: 'dxButton', options: {
                      text: 'OK',
                      onClick: function (e) {
                          $("#popupMessageContainer").dxPopup("instance").hide();
                          if (okCallback)
                              okCallback();
                      }
                  }
              },
              {
                  toolbar: 'bottom', location: 'after', widget: 'dxButton', options: {
                      text: 'Cancel',
                      visible: !(!popupMessageMode || popupMessageMode === 1),
                      onClick: function () {
                          $("#popupMessageContainer").dxPopup("instance").hide();
                          if (cancelCallBack)
                              cancelCallBack();
                      }
                  }
              }
        ],
    });
}

function HidePopupMessage() {
    var popup = $("#popupMessageContainer").dxPopup("instance");
    if (popup)
        popup.hide();
}

function InsertAndShowPopup(containerName, popupControlName, popupFormName, response) {
    try {
        $("#" + containerName).html(response);

        // Enable unobtrusive validation for newly inserted content
        if (popupFormName)
            $.validator.unobtrusive.parse($("#" + popupFormName));

    }
    catch (err) {
        HandleError(err);
    }
}

function AdjustPopupSize(popupWindow) {
    try {
        if (popupWindow && popupWindow.IsVisible()) {
            var available = window.innerWidth - 50;
            popupWindow.SetWidth(available);
            popupWindow.UpdatePosition();
        }
    }
    catch (err) {
        HandleError(err);
    }
}