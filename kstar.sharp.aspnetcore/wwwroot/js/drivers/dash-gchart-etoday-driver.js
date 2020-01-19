﻿
google.charts.load("current", { packages: ["corechart", "bar"] });
google.charts.setOnLoadCallback(setData);

let dash_etoday_data;
let dash_pvnow_data;
let dash_gridnow_data;

let dash_pvtoday_data;


let inverterData;

$(document).on("onGetLatest", function (event, data) {

    inverterData = data;
    setData();

});


const etodayMaxChartValue = 15;
const pvPowerMaxChartValue = 3000;
const gridPowerMaxChartValue = 1500;

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
        ['Average', etodayMaxChartValue - inverterData.eToday <= 0 ? 0 : etodayMaxChartValue - inverterData.eToday]

    ]);
    $('#etoday').text(inverterData.eToday);
    drawChartEtoday();

    let roundedPvPower = (inverterData.pvPower / 1000).toFixed(2);
    dash_pvnow_data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Today', inverterData.pvPower],
        ['Average', pvPowerMaxChartValue - inverterData.pvPower <= 0 ? 0 : pvPowerMaxChartValue - inverterData.pvPower]

    ]);
    $('#pvnow').text(roundedPvPower);
    drawChartPvNow();


    let rounderGripPower = inverterData.gridPower.toFixed(0);
    dash_gridnow_data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Today', inverterData.gridPower < 0 ? inverterData.gridPower * -1 : inverterData.gridPower ],
        ['Average', gridPowerMaxChartValue - inverterData.gridPower < 0 ? 0 : gridPowerMaxChartValue - inverterData.gridPower]

    ]);
    $('#gridnow').text(rounderGripPower);
    drawGridNow();


    dash_pvtoday_data = google.visualization.arrayToDataTable([
        ['Hour', 'kWh'],
        ['6:00', 0.1],
        ['7:00', 0.3],
        ['8:00', 0.45],
        ['9:00', 0.5],
        ['10:00', 0.55],
        ['11:00', 0.6],
        ['12:00', 0.65],
        ['13:00', 0.55],
        ['14:00', 0.45],
        ['15:00', 0.32],
        ['16:00', 0.2],
        ['17:00', 0.18],
        ['18:00', 0.2]
    ]);
    drawPvToday();

}


function drawChartEtoday() {

    var options = {
        //title: 'My Daily Activities',
        height: 300,
        pieHole: 0.9,
        pieSliceBorderColor: 'none',
        legend: { position: 'none' },
        pieSliceText: 'none',
        backgroundColor: '#242f3a',
        pieStartAngle: 180,
        slices: [{ color: '#f15a2c' }, { color: '#43576b' }]
        //enableInteractivity: false
    };

    let chart = new google.visualization.PieChart(document.getElementById('donutchart-etoday'));
    chart.draw(dash_etoday_data, options);
}

function drawChartPvNow() {

    var options = {
        //title: 'My Daily Activities',
        height: 300,
        pieHole: 0.9,
        pieSliceBorderColor: 'none',
        legend: { position: 'none' },
        pieSliceText: 'none',
        backgroundColor: '#242f3a',
        pieStartAngle: 180,
        slices: [{ color: '#f15a2c' }, { color: '#43576b' }]
        //enableInteractivity: false
    };

    let chart = new google.visualization.PieChart(document.getElementById('donutchart-pvnow'));
    chart.draw(dash_pvnow_data, options);
}

function drawGridNow() {

    var options = {
        //title: 'My Daily Activities',
        height: 300,
        pieHole: 0.9,
        pieSliceBorderColor: 'none',
        legend: { position: 'none' },
        pieSliceText: 'none',
        backgroundColor: '#242f3a',
        pieStartAngle: 180,
        slices: [{ color: '#f15a2c' }, { color: '#43576b' }]
        //enableInteractivity: false
    };

    let chart = new google.visualization.PieChart(document.getElementById('donutchart-gridnow'));
    chart.draw(dash_gridnow_data, options);
}

function drawPvToday() {
    //var data = google.visualization.arrayToDataTable([
    //    ['Task', 'Hours per Day'],
    //    ['PV', 1100],
    //    ['Eat', 3500],

    //]);

    var options = {
        title: '',
        backgroundColor: '#242f3a',
        colors: ['#f15a2c'],
        legend: { position: "none" },
        //fontName: 'LatoLight',
        hAxis: {
            title: '', //PV Today',
            format: 'h:mm a',
            titleTextStyle: {
                color: '#fff',
                italic: false
            },
            gridlines: {
                color: '#242f3a',
                count: 2
            }
            //viewWindow: {
            //    min: [7, 30, 0],
            //    max: [17, 30, 0]
            //}
        },
        vAxis: {
            title: 'kWh',
            titleTextStyle: {
                color: '#fff',
                italic: false
            },
            gridlines: {
                color: '#242f3a',
                count: 2
            }
        }
    };

    let chart = new google.visualization.ColumnChart(document.getElementById('columnchart-pvtoday'));
    chart.draw(dash_pvtoday_data, options);
}


$(window).resize(function () {
    drawChartEtoday();
    drawChartPvnow();
    drawGridNow();
    drawPvToday();
});