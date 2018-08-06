$(document).ready(function () {
    $("#hiddenfield_device").val(window.navigator.appVersion + "," + window.navigator.platform);
    var pageStatus = $("#hiddenfield_showInfo").val();
    if (pageStatus === '0') {
        $("#introInfoText").html($("#hiddenfield_text").val());
    }
    else {
        $("#introInfo").hide();
        $("#startPlay").show();
        var end = parseInt($("#hiddenfield_startCredit").val());
        animateValue("value", 0, end, 300);
    }
});

$("#btnCloseWin").click(function () {
    $("#btnPlay").trigger("click");
    return false;
});

function animateValue(id, start, end, duration) {
    var range = end - start;
    var current = start;
    var increment = end > start ? 10 : -1;
    var stepTime = Math.abs(Math.floor(duration / range));
    var obj = document.getElementById(id);
    var timer = setInterval(function () {
        current += increment;
        obj.innerHTML = current;
        if (current === end) {
            clearInterval(timer);
        }
    }, stepTime);
}





