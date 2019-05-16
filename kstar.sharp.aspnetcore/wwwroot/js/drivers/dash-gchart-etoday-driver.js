
google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(setData);

var dash_etoday_data; 
var dash_pvnow_data; 

var inverterData;

$(document).on("onGetLatest", function (event, data) {

    inverterData = data;
    setData();

});

function setData() {
    //var textDataTime = new Date(data.recordedDateTime).toString();
    //var textData = `PV: ${data.pvPower}KW Battery: ${data.bat1Charge}% ${data.bat1Power}W Grid: ${data.gridPower}W Load: ${data.loadPower}W EToday: ${data.eToday}KWh`;

    if (inverterData === undefined)
        return;

    if (google.visualization.arrayToDataTable === undefined)
        return;

    dash_etoday_data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Today', inverterData.eToday],
        ['Average', 25 - inverterData.eToday]

    ]);
    $('#etoday').text(inverterData.eToday);
    drawChartEtoday();


    dash_pvnow_data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Today', inverterData.pvPower],
        ['Average', 3000 - inverterData.pvPower]

    ]);
    $('#pvnow').text(inverterData.pvPower);
    drawChartPvnow();
}


function drawChartEtoday() {
    //var data = google.visualization.arrayToDataTable([
    //    ['Task', 'Hours per Day'],
    //    ['PV', 1100],
    //    ['Eat', 3500],

    //]);

    var options = {
        //title: 'My Daily Activities',
        height: 450,
        pieHole: 0.92,
        pieSliceBorderColor: 'none',
        legend: { position: 'none' },
        pieSliceText: 'none',
        backgroundColor: '#242f3a',
        pieStartAngle: 180,
        slices: [{ color: '#f15a2c' }, { color: '#43576b' }]
        //enableInteractivity: false
    };

    var chart = new google.visualization.PieChart(document.getElementById('donutchart-etoday'));
    chart.draw(dash_etoday_data, options);
}

function drawChartPvnow() {
    //var data = google.visualization.arrayToDataTable([
    //    ['Task', 'Hours per Day'],
    //    ['PV', 1100],
    //    ['Eat', 3500],

    //]);

    var options = {
        //title: 'My Daily Activities',
        height: 450,
        pieHole: 0.92,
        pieSliceBorderColor: 'none',
        legend: { position: 'none' },
        pieSliceText: 'none',
        backgroundColor: '#242f3a',
        pieStartAngle: 180,
        slices: [{ color: '#f15a2c' }, { color: '#43576b' }]
        //enableInteractivity: false
    };

    var chart = new google.visualization.PieChart(document.getElementById('donutchart-pvnow'));
    chart.draw(dash_pvnow_data, options);
}



$(window).resize(function () {
    drawChartEtoday();
    drawChartPvnow();
});