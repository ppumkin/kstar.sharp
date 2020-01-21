
google.charts.load("current", { packages: ["corechart", "bar"] });
google.charts.setOnLoadCallback(setData);

let dash_etoday_data;
let dash_pvnow_data;
let dash_gridnow_data;

let dash_pvtoday_data;


$(document).on("onGetLatest", function (event, data) {
    setData(data);
});
$(document).on("onGetHourly", function (event, inverterDataList) {
    setHourlyDate(inverterDataList);
});

const etodayMaxChartValue = 15;
const pvPowerMaxChartValue = 3000;
const gridPowerMaxChartValue = 1500;

function setData(inverterData) {
    //var textDataTime = new Date(data.recordedDateTime).toString();
    //var textData = `PV: ${data.pvPower}KW Battery: ${data.bat1Charge}% ${data.bat1Power}W Grid: ${data.gridPower}W Load: ${data.loadPower}W EToday: ${data.eToday}KWh`;

    if (inverterData === undefined)
        return;

    if (google.visualization.arrayToDataTable === undefined)
        return;

    var options = { weekday: 'long', day: 'numeric', hour: 'numeric', minute: '2-digit' };
    var lastknown = new Date(inverterData.recordedDateTime);

    $('#last-known-time').text(lastknown.toLocaleDateString("en-GB", options));

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
        ['Today', inverterData.gridPower < 0 ? inverterData.gridPower * -1 : inverterData.gridPower],
        ['Average', gridPowerMaxChartValue - inverterData.gridPower < 0 ? 0 : gridPowerMaxChartValue - inverterData.gridPower]

    ]);
    $('#gridnow').text(rounderGripPower);
    drawGridNow();
}


function setHourlyDate(inverterDataList) {

    if (inverterDataList === undefined)
        return;

    if (google.visualization.arrayToDataTable === undefined)
        return;

    dash_pvtoday_data = google.visualization.arrayToDataTable([
        ['Hour', 'kWh'],
        [new Date(inverterDataList[6].recordedDateTime ), parseFloat(( inverterDataList[6].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[7].recordedDateTime ), parseFloat(( inverterDataList[7].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[8].recordedDateTime ), parseFloat(( inverterDataList[8].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[9].recordedDateTime ), parseFloat(( inverterDataList[9].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[10].recordedDateTime), parseFloat((inverterDataList[10].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[11].recordedDateTime), parseFloat((inverterDataList[11].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[12].recordedDateTime), parseFloat((inverterDataList[12].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[13].recordedDateTime), parseFloat((inverterDataList[13].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[14].recordedDateTime), parseFloat((inverterDataList[14].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[15].recordedDateTime), parseFloat((inverterDataList[15].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[16].recordedDateTime), parseFloat((inverterDataList[16].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[17].recordedDateTime), parseFloat((inverterDataList[17].pvPower/ 1000).toFixed(2))],
        [new Date(inverterDataList[18].recordedDateTime), parseFloat((inverterDataList[18].pvPower/ 1000).toFixed(2))]
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
            format: 'HH:mm', //'h:mm a',
            titleTextStyle: {
                color: '#fff',
                italic: false
            },
            gridlines: {
                color: '#242f3a',
                count: 6
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
            },
            maxValue: 4,
            minValue: 0
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