function gameInit() {
    var showInfo = $("#HiddenField_showInfo").val(); console.log(showInfo);
    showInfo === '1' ? $("#startInfo").show() : $("#winchance-container").show();
}

$('#btnShowInfo').click(function () {
    $("#startInfo").hide();
    $("#winchance-container").show();
 
});

$("#btnClose").click(function () {
    $("#message-container").hide();
    console.log("close2");
});

$(".winchance-btn").click(function () {
    $("#winchance-container").hide();
});

$("#btnCloseWin").click(function () {
    $("#btnPlay").trigger("click");
    console.log("close");
    return false;
    //$("#message-container").hide();
});

