function btnView_OnClick() {
    var WFMform = $("#WFMform").dxForm("instance").option("formData");
    
    AjaxRequest("POST", "ForecastWeather", "GetDailyWeather", "", { weatherForecastInput: WFMform },
        true, OnGetDailyWeatherSuccess, AjaxRequestError);
}

function OnGetDailyWeatherSuccess(response) {
    alert(response);
}