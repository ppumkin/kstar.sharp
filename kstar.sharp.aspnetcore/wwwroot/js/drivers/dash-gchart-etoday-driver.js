let chartEtoday;
let chartPvToday;
let chartPvNow;
let chartGridNow;

$(document).on("initialiseGoogleCharts", function (event, data) {
    chartEtoday = new google.visualization.PieChart(document.getElementById('donutchart-etoday'));
    chartPvToday = new google.visualization.ColumnChart(document.getElementById('columnchart-pvtoday'));
    chartPvNow = new google.visualization.PieChart(document.getElementById('donutchart-pvnow'));
    chartGridNow = new google.visualization.PieChart(document.getElementById('donutchart-gridnow'));
});


$(document).on("onGetHourly", function (event, inverterDataList) {
    setHourlyData(inverterDataList);
});


const etodayMaxChartValue = 15;
const pvPowerMaxChartValue = 3000;
const gridPowerMaxChartValue = 1500;


function setHourlyData(data) {
    //var textData = `PV: ${data.pvPower}KW Battery: ${data.bat1Charge}% ${data.bat1Power}W Grid: ${data.gridPower}W Load: ${data.loadPower}W EToday: ${data.eToday}KWh`;

    if (data === undefined)
        return;

    if (google.visualization.arrayToDataTable === undefined)
        return;

    // Latest known record
    var options = { weekday: 'long', day: 'numeric', hour: 'numeric', minute: '2-digit' };
    var lastknown = new Date(data.latest.recordedDateTime);
    $('#last-known-time').text(lastknown.toLocaleDateString("en-GB", options));
    // Latest known record

    //PV Now
    let roundedPvPower = data.latest.pvPower.toFixed(0);
    $('#pvnow').text(roundedPvPower);

    let chartPvNow_data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Today', data.latest.pvPower],
        ['Average', pvPowerMaxChartValue - data.latest.pvPower <= 0 ? 0 : pvPowerMaxChartValue - data.latest.pvPower]

    ]);
    chartPvNow_update(chartPvNow_data);
    //PV Now

    //Grid Now
    let rounderGripPower = data.latest.gridPower.toFixed(0);
    $('#gridnow').text(rounderGripPower);

    let chartGridNow_data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Today', data.latest.gridPower < 0 ? data.latest.gridPower * -1 : data.latest.gridPower],
        ['Average', gridPowerMaxChartValue - data.latest.gridPower < 0 ? 0 : gridPowerMaxChartValue - data.latest.gridPower]

    ]);
    chartGridNow_update(chartGridNow_data);
    //Grid Now

    // E-Today
    const maxEToday = 20;
    let remainderEToday = maxEToday - data.totalConsumption.value;

    let chartEtoday_data = google.visualization.arrayToDataTable([
        ['Task', 'kWh'],
        ['Produced', data.totalProduction.value],
        ['Purchased', data.totalPurchased.value],
        ['Total', remainderEToday]
    ]);
    $('#etoday').text(data.totalProduction.value.toFixed(2));
    chartEtoday_update(chartEtoday_data);
    // E-Today


    //bar chart
    let dash_pvtoday_data = google.visualization.arrayToDataTable([
        ['Hour', 'kWh'],
        [new Date(data.hourlyStats[6].hour), Math.round(data.hourlyStats[6].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[7].hour), Math.round(data.hourlyStats[7].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[8].hour), Math.round(data.hourlyStats[8].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[9].hour), Math.round(data.hourlyStats[9].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[10].hour), Math.round(data.hourlyStats[10].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[11].hour), Math.round(data.hourlyStats[11].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[12].hour), Math.round(data.hourlyStats[12].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[13].hour), Math.round(data.hourlyStats[13].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[14].hour), Math.round(data.hourlyStats[14].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[15].hour), Math.round(data.hourlyStats[15].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[16].hour), Math.round(data.hourlyStats[16].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[17].hour), Math.round(data.hourlyStats[17].production.value * 1e2) / 1e2],
        [new Date(data.hourlyStats[18].hour), Math.round(data.hourlyStats[18].production.value * 1e2) / 1e2]
    ]);

    chartPvToday_update(data.totalConsumption.value.toFixed(2), data.totalProduction.value.toFixed(2), dash_pvtoday_data);
    //bar chart

}


const silverHex = '#43576B';
const greenHex = '#BDBF09';
const amberHex = '#F15A2C';


function chartEtoday_update(chartEtoday_data) {
    let options = {
        //title: 'My Daily Activities',
        height: 300,
        pieHole: 0.9,
        pieSliceBorderColor: 'none',
        legend: { position: 'none' },
        pieSliceText: 'none',
        backgroundColor: '#242f3a',
        pieStartAngle: 180,
        slices: [{ color: greenHex }, { color: amberHex }, { color: silverHex }, { color: 'transparent' }]
        //enableInteractivity: false
    };
    chartEtoday.draw(chartEtoday_data, options);
}

function chartPvNow_update(chartPvNow_data) {
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
    chartPvNow.draw(chartPvNow_data, options);
}

function chartGridNow_update(chartGridNow_data) {
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
    chartGridNow.draw(chartGridNow_data, options);
}

function chartPvToday_update(totalConsumed, totalProduced, chartPvToday_data) {
    var options = {
        title: 'Consumed ' + totalConsumed + ' kWh',
        titleTextStyle: {
            color: '#fff',
            italic: false,
            bold: false
        },
        backgroundColor: '#242f3a',
        colors: ['#f15a2c'],
        legend: { position: "none" },
        //fontName: 'LatoLight',
        hAxis: {
            title: '', //PV Today',
            format: 'HH:mm', //'h:mm a',
            titleTextStyle: {
                color: '#fff',
                italic: false,
                bold: false
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
            title: totalProduced + ' kWh',
            titleTextStyle: {
                color: '#fff',
                italic: false,
                bold: false
            },
            gridlines: {
                color: '#242f3a',
                count: 2
            },
            maxValue: 4,
            minValue: 0
        }
    };
    chartPvToday.draw(chartPvToday_data, options);
}


$(window).resize(function () {
    chartEtoday_update();
    chartPvNow_update();
    chartGridNow_update();
    chartPvToday_update();
});