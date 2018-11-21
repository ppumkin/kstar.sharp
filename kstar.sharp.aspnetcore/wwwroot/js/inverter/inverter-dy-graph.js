
var dyGraph;

$(document).ready(function () {

    updateGraph();

    $('#historyHours').on('change', function () {
        updateGraph();
    });

});

function updateGraph() {

    if (dyGraph)
        dyGraph.destroy();


    $.ajax({
        url: "/Home/GetData",
        data: { historyHours: getHoursForHistory() },
        context: document.body
    }).done(function (data) {

        dyArray = new Array();
        var ds = $.map(data, function (val, i) {
            //dyArray.push([new Date(val.RecordedDateTime), val.PVPower, val.Bat1Charge, val.GridPower, val.LoadPower, val.PV1Volt, val.PV2Volt]);
            dyArray.push([new Date(val.RecordedDateTime), val.PVPower, val.Bat1Charge * 10, val.Bat1Power, val.GridPower, val.LoadPower, val.EToday]);

        });

        dyGraph = new Dygraph(document.getElementById("graph"), dyArray,
        {
            //labels: ["Time", "PV", "Bat%", "Grid", "Load", "PV1Volt","PV2Volt"],
            labels: ["Time", "PV", "Bat%", "BatLoad", "Grid", "Load", "EDay"],
            //title: 'PV',
            //ylabel: 'Stuff',
            legend: 'always',
            labelsDivStyles: { 'textAlign': 'right' },
            labelsDivWidth: 800,
            showRangeSelector: true,
            valueRange: [-3500, 3500],

            //highlightSeriesOpts: {
            //    strokeWidth: 2,
            //    strokeBorderWidth: 1,
            //    highlightCircleSize: 3
            //},
            //valueFormatter : function
            'PV': {
                strokeWidth: 0.1,
                drawPoints: false,
                pointSize: 0,
                //highlightCircleSize: 2,
                fillGraph: true,
                stepPlot: true,
                color: '#b3b300'
            },
            'EDay': {
                color: '#b3b300',
                strokeWidth: 2
            },
            'Load': {
                color: '#ff5050'
            },
            'Grid': {
                color: '#cc0000'
            }
            //axisLabelWidth : [50,60]
        }

            );

        //[data],
        //{
        //    labels: ["Bat1Amp", "Bat1Charge", "Bat1Power", "Bat1Voltage", "EToday", "ETotal", "GridPower", "LoadPower", "PV1Volt", "PV2Volt", "PVPower", "RecordedDateTime", "TempCelcius" ]
        //});



    });
}

function getHoursForHistory() {
    return $('#historyHours :selected').val();
}