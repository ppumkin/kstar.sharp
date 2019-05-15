
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
        $.map(data, function (val, i) {
            dyArray.push([new Date(val.recordedDateTime), val.pvPower, val.bat1Charge * 10, val.bat1Power, val.gridPower, val.loadPower, val.eToday]);
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
    });
}

function getHoursForHistory() {
    return $('#historyHours :selected').val();
}