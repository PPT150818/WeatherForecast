function btnView_OnClick() {
    var WFMform = $("#WFMform").dxForm("instance").option("formData");


        AjaxRequest("POST", "ForecastWeather", "GetDailyWeather", "", { weatherForecastInput: WFMform.WeatherForecastInput },
            true, OnGetDailyWeatherSuccess, AjaxRequestError);
    
}

function OnGetDailyWeatherSuccess(response) {

    $("#weatherForecastDetailsDiv").html(response);
    var txtAbbreviation = $("#txtAbbreviation").dxTextBox("instance");
    var btnGetLatestDetails = $("#btnGetLatestDetails").dxButton("instance");
    if (btnGetLatestDetails) {
        btnGetLatestDetails.option("visible", false);
    }

    if (txtAbbreviation && txtAbbreviation.option("value") == null) {
        ShowPopupMessage("No data available for entered latitude and longitude.", "Current Weather Details");
    }
}


function gvWeatherForcastMaster_OnRowClick(e) {
    document.getElementById('MasterKey').value = e.data.WFMasterId;
    /* e.rowElement.css('background-color', '#ddd');*/
    e.component.repaint();
    var nbLatitude = $("#nbLatitude").dxNumberBox("instance");
    if (nbLatitude) {
        nbLatitude.option("value", e.data.Latitude)
    }
    var nbLongitude = $("#nbLongitude").dxNumberBox("instance");
    if (nbLongitude) {
        nbLongitude.option("value", e.data.Longitude)
    }
   
    ClearFields();
    var weatherForecastInput = new WeatherForecastInput(nbLatitude.option("value"), nbLongitude.option("value"), document.getElementById('MasterKey').value);
    AjaxRequest("POST", "ForecastWeather", "GetDetailsGrid", "", { weatherForecastInput: weatherForecastInput },
        true, OnGetDetailsGridSuccess, AjaxRequestError);

}

function OnGetDetailsGridSuccess(response) {
    $("#detailgrid").html(response);
    //document.getElementById('DetailKey').value = response.DetailKey;
    var gvWeatherForcastDetails = $("#gvWeatherForcastDetails").dxDataGrid("instance");
    if (gvWeatherForcastDetails) {
        gvWeatherForcastDetails.option("focusedRowKey", document.getElementById('DetailKey').value); 
        gvWeatherForcastDetails.repaint();
    }
}

function ClearFields() {
    var nbGeneration = $("#nbGeneration").dxNumberBox("instance");
    if (nbGeneration) {
        nbGeneration.option("value", null)
    }
    var nbElevation = $("#nbElevation").dxNumberBox("instance");
    if (nbElevation) {
        nbElevation.option("value", null)
    }
    var txtTimezone = $("#txtTimezone").dxTextBox("instance");
    if (txtTimezone) {
        txtTimezone.option("value", null)
    }
    var txtAbbreviation = $("#txtAbbreviation").dxTextBox("instance");
    if (txtAbbreviation) {
        txtAbbreviation.option("value", null)
    }
    var nbTemperature = $("#nbTemperature").dxNumberBox("instance");
    if (nbTemperature) {
        nbTemperature.option("value", null)
    }
    var nbWindspeed = $("#nbWindspeed").dxNumberBox("instance");
    if (nbWindspeed) {
        nbWindspeed.option("value", null)
    }
    var nbWindDirection = $("#nbWindDirection").dxNumberBox("instance");
    if (nbWindDirection) {
        nbWindDirection.option("value", null)
    }
    var txtWeathercode = $("#txtWeathercode").dxTextBox("instance");
    if (txtWeathercode) {
        txtWeathercode.option("value", null)
    }
    var nbTime = $("#nbTime").dxTextBox("instance");
    if (nbTime) {
        nbTime.option("value", null)
    }
}

function OnGetDailyDataSuccess(response) {
    if (response != null) {
        var gvWeatherForcastMaster = $("#gvWeatherForcastMaster").dxDataGrid("instance");
        if (gvWeatherForcastMaster) {
            gvWeatherForcastMaster.option("focusedRowKey", response.MasterKey);
        }
        document.getElementById('DetailKey').value = response.DetailKey;
        if (response.weatherForecastDetailModel!=null) {
            for (var i = 0; i < response.weatherForecastDetailModel.length; i++) {
                response.weatherForecastDetailModel[i].LastUpdated = ConvertJsonDateString(response.weatherForecastDetailModel[i].LastUpdated);
            }
        }
        var GridInstance = $("#gvWeatherForcastDetails").dxDataGrid("instance");
        if (GridInstance) {
            GridInstance.option("dataSource").store._array = response.weatherForecastDetailModel;
            GridInstance.refresh();
        }
        DisplayCurrentData(response.weatherForecastLiveDataModel);
    }
}

function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        shortDate = monthString + '-' + dayString + '-' + year;
    }
    return shortDate;
};

function DisplayCurrentData(weatherForecastLiveDataModel) {
    var nbLatitude = $("#nbLatitude").dxNumberBox("instance");
    if (nbLatitude) {
        nbLatitude.option("value", weatherForecastLiveDataModel.WeatherForecast.Latitude)
    }
    var nbLongitude = $("#nbLongitude").dxNumberBox("instance");
    if (nbLongitude) {
        nbLongitude.option("value", weatherForecastLiveDataModel.WeatherForecast.Longitude)
    }
    var nbGeneration = $("#nbGeneration").dxNumberBox("instance");
    if (nbGeneration) {
        nbGeneration.option("value", weatherForecastLiveDataModel.WeatherForecast.GenerationTime)
    }
    var nbElevation = $("#nbElevation").dxNumberBox("instance");
    if (nbElevation) {
        nbElevation.option("value", weatherForecastLiveDataModel.WeatherForecast.Elevation)
    }
    var txtTimezone = $("#txtTimezone").dxTextBox("instance");
    if (txtTimezone) {
        txtTimezone.option("value", weatherForecastLiveDataModel.WeatherForecast.Timezone)
    }
    var txtAbbreviation = $("#txtAbbreviation").dxTextBox("instance");
    if (txtAbbreviation) {
        txtAbbreviation.option("value", weatherForecastLiveDataModel.WeatherForecast.Latitude)
    }
    var nbTemperature = $("#nbTemperature").dxNumberBox("instance");
    if (nbTemperature) {
        nbTemperature.option("value", weatherForecastLiveDataModel.WeatherForecast.CurrentWeather.Temperature)
    }
    var nbWindspeed = $("#nbWindspeed").dxNumberBox("instance");
    if (nbWindspeed) {
        nbWindspeed.option("value", weatherForecastLiveDataModel.WeatherForecast.CurrentWeather.Windspeed)
    }
    var nbWindDirection = $("#nbWindDirection").dxNumberBox("instance");
    if (nbWindDirection) {
        nbWindDirection.option("value", weatherForecastLiveDataModel.WeatherForecast.CurrentWeather.WindDirection)
    }
    var txtWeathercode = $("#txtWeathercode").dxTextBox("instance");
    if (txtWeathercode) {
        txtWeathercode.option("value", weatherForecastLiveDataModel.WeatherForecast.CurrentWeather.Weathercode)
    }
    var nbTime = $("#nbTime").dxTextBox("instance");
    if (nbTime) {
        nbTime.option("value", weatherForecastLiveDataModel.WeatherForecast.CurrentWeather.Time)
    }

}


function gvWeatherForcastDetails_OnRowClick(e) {


}

function gvWeatherForcastMaster_onFocusedRowChanging(e) {
    //var rowsCount = e.component.getVisibleRows().length,
    //    pageCount = e.component.pageCount(),
    //    pageIndex = e.component.pageIndex(),
    //    key = e.event && e.event.key;

    //if (key && e.prevRowIndex === e.newRowIndex) {
    //    if (e.newRowIndex === rowsCount - 1 && pageIndex < pageCount - 1) {
    //        e.component.pageIndex(pageIndex + 1).done(function () {
    //            e.component.option("focusedRowIndex", 0);
    //        });
    //    } else if (e.newRowIndex === 0 && pageIndex > 0) {
    //        e.component.pageIndex(pageIndex - 1).done(function () {
    //            e.component.option("focusedRowIndex", rowsCount - 1);
    //        });
    //    }
    //}
}

function gvWeatherForcastMaster_OnRowPrepared(row) {
    var masterkey = document.getElementById('MasterKey').value;

    if (row && row.rowType !== "header" && row.data !== undefined) {

        if (row.data.WFMasterId === parseInt(masterkey)) {

            row.rowElement.css('background-color', '#ddd');
        }
    }
}

function gvWeatherForcastDetails_OnRowPrepared(row) {
    var masterkey = document.getElementById('DetailKey').value;
    if (row && row.rowType !== "header" && row.data !== undefined) {
        if (row.data.WFDetailId === parseInt(masterkey)) {
            row.rowElement.css('background-color', '#ddd');
        }
    }
}

function gvWeatherForcastMaster_OnRowRemoved(e) {
    AjaxRequest("POST", "ForecastWeather", "DeleteMasterDetails", "", { masterId: e.data.WFMasterId },
        true, OnDeleteMasterDetailsSuccess, AjaxRequestError);
}

function OnDeleteMasterDetailsSuccess(response) {
    if (response != null) {
        document.getElementById('MasterKey').value = response.MasterKey;
        if (response.weatherForecastMasterModel != null) {
            for (var i = 0; i < response.weatherForecastMasterModel.length; i++) {
                response.weatherForecastMasterModel[i].LastUpdated = ConvertJsonDateString(response.weatherForecastMasterModel[i].LastUpdated);
            }
        }
        var gvWeatherForcastMaster = $("#gvWeatherForcastMaster").dxDataGrid("instance");
        if (gvWeatherForcastMaster) {
            gvWeatherForcastMaster.option("focusedRowKey", response.MasterKey);
            gvWeatherForcastMaster.option("dataSource").store._array = response.weatherForecastMasterModel;
            gvWeatherForcastMaster.refresh();
        }
        document.getElementById('DetailKey').value = response.DetailKey;
        if (response.weatherForecastDetailModel != null) {
            for (var i = 0; i < response.weatherForecastDetailModel.length; i++) {
                response.weatherForecastDetailModel[i].LastUpdated = ConvertJsonDateString(response.weatherForecastDetailModel[i].LastUpdated);
            }
        }
        var GridInstance = $("#gvWeatherForcastDetails").dxDataGrid("instance");
        if (GridInstance) {
            GridInstance.option("dataSource").store._array = response.weatherForecastDetailModel;
            GridInstance.refresh();
        }
        DisplayCurrentData(response.weatherForecastLiveDataModel);
    }
}

function gvWeatherForcastDetails_OnRowRemoved(e) {
   
}

class WeatherForecastInput {
    constructor(Latitude, Longitude, WFMasterId) {
        this.Latitude = Latitude
        this.Longitude = Longitude
        this.WFMasterId = WFMasterId
    }
}


function OnRefreshClick() {
    AjaxRequest("POST", "ForecastWeather", "GetRefreshedData", "", null,
        true, OnOnRefreshClickSuccess, AjaxRequestError);
}

function OnOnRefreshClickSuccess(response) {
    $("#dvHistoryPartial").html(response);
}

function btnGetLatestDetails() {
    var nbLatitude = $("#nbLatitude").dxNumberBox("instance");
    var nbLongitude = $("#nbLongitude").dxNumberBox("instance");

    var weatherForecastInput = new WeatherForecastInput(nbLatitude.option("value"), nbLongitude.option("value"), document.getElementById('MasterKey').value);
    AjaxRequest("POST", "ForecastWeather", "GetHistoryDetails", "", { weatherForecastInput: weatherForecastInput },
        true, OnGetDailyDataSuccess, AjaxRequestError);
}