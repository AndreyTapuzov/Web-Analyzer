function renderChart(data) {
    var chart = new CanvasJS.Chart("chartContainer",
    {
        theme: "theme2",
        animationEnabled: true,
        title: {
            text: "Urls performance"
        },
        data: [
            {
                type: "column",
                dataPoints: data,
            }
        ],
        axisX: {
            title: "Urls",
            titleFontSize: 20,
            labelFontSize: 0,

        },
        axisY: {
            title: "Elapsed time (sec)",
            titleFontSize: 20,
        },
        toolTip: {
            content:
                "url: <span style='\"'color: #FFAB58;'\"'>{label}</span>, time: <span style='\"'color: #6C9DFF;'\"'>{y} (sec)</span>",
        },
        dataPointMaxWidth: 70,
});
    chart.render();
};