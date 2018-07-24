$(document).ready(function () {
    console.log($("#hiddenfield_text").val());
    var pageStatus = $("#hiddenfield_showInfo").val();
    if (pageStatus === '0') {
        $("#introInfoText").html($("#hiddenfield_text").val());
    }
    else {
        $("#introInfo").hide();
        $("#startPlay").show();
        animateValue("value", 000, 1000, 500);
    }
});

$("#btnCloseWin").click(function () {
    $("#btnPlay").trigger("click");
    return false;
});

function animateValue(id, start, end, duration) {
    var range = end - start;
    var current = start;
    var increment = end > start ? 1 : -1;
    var stepTime = Math.abs(Math.floor(duration / range));
    var obj = document.getElementById(id);
    var timer = setInterval(function () {
        current += increment;
        obj.innerHTML = current;
        if (current == end) {
            clearInterval(timer);
        }
    }, stepTime);
}



