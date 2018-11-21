


$(document).ready(function () {
    setInterval(function () { getLatest(); }, 30000); //need to synch this time setting to something common
    getLatest();
});

function getLatest() {
    $.ajax({
        url: "/Home/GetLatest",
        context: document.body
    }).done(function (data) {

        //debugger;
        //var textData = new Date(parseInt(data.RecordedDateTime.substr(6))).toString() + '     PV: ' + data.PVPower + 'kW     Bat1: ' + data.Bat1Charge + '%  ' + data.Bat1Power + 'W     Grid: ' + data.GridPower + 'W     Load: ' + data.LoadPower + 'W     EToday: ' + data.EToday;
        //$('#liveData').text(textData);

        // ["Bat1Amp", "Bat1Charge", "Bat1Power", "Bat1Voltage", "EToday", "ETotal", "GridPower", "LoadPower", "PV1Volt", "PV2Volt", "PVPower", "RecordedDateTime", "TempCelcius" ]
        $(document).trigger("onGetLatest", [data]);

    });

}