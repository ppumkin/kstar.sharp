


$(document).ready(function () {
    setInterval(function () { getLatest(); }, 30000); //need to synch this time setting to something common
    getLatest();
});

function getLatest() {
    $.ajax({
        url: "/api/data",
        method: "GET",
        context: document.body
    }).done(function (data) {

       

        // ["Bat1Amp", "Bat1Charge", "Bat1Power", "Bat1Voltage", "EToday", "ETotal", "GridPower", "LoadPower", "PV1Volt", "PV2Volt", "PVPower", "RecordedDateTime", "TempCelcius" ]
        $(document).trigger("onGetLatest", [data]);

    });

}