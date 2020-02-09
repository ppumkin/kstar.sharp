


google.charts.load("current", { packages: ["corechart", "bar"] });
google.charts.setOnLoadCallback(startCharts);

function startCharts() {
    $(document).trigger("initialiseGoogleCharts");

    //setInterval(function () { getLatest(); }, 25 * 1000); //need to synch this time setting to something common
    //getLatest();

    setInterval(function () { getHourly(); }, 30 * 1000); //60 * 1000 * 60); //need to synch this time setting to something common
    getHourly();
}

var dayOffset = 0;

function getHourly() {
    $.ajax({
        url: `/api/hourly?dayOffset=${dayOffset}`,
        method: "GET",
        context: document.body
    }).done(function (data) {
        $(document).trigger("onGetHourly", [data]);
    });
}




$(document).ready(function () {

});
function getLatest() {
    $.ajax({
        url: "/api/live",
        method: "GET",
        context: document.body
    }).done(function (data) {
        // ["Bat1Amp", "Bat1Charge", "Bat1Power", "Bat1Voltage", "EToday", "ETotal", "GridPower", "LoadPower", "PV1Volt", "PV2Volt", "PVPower", "RecordedDateTime", "TempCelcius" ]
        $(document).trigger("onGetLatest", [data]);
    });
}
