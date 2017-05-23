//$(document).ready(function () {

//    $("#limitation-checkbox").click(function (e) {
//        $("#limitation-textbox").toggle(100);
//        $("#limitation-textbox-error").hide();
//    });

//});

(function () {
    var absChartContainer = $(".canvasjs-chart-canvas").toArray()[0];
    if (!absChartContainer)
        return;
    $("#chartContainer").css("height", absChartContainer.offsetHeight+"px");
    }
)();

