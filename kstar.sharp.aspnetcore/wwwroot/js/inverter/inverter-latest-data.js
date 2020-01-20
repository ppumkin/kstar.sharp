


$(document).ready(function () {
    setInterval(function () { getLatest(); }, 25 * 1000); //need to synch this time setting to something common
    getLatest();

    setInterval(function () { getHourly(); }, 60 * 1000 * 60); //need to synch this time setting to something common
    getHourly();
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

function getHourly() {
    $.ajax({
        url: "/api/hourly",
        method: "GET",
        context: document.body
    }).done(function (data) {
        $(document).trigger("onGetHourly", [data]);
    });
}