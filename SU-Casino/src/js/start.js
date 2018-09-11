$(document).ready(function () {
    //Saves country to hiddenfield_country
    ipLookUp();
    //Saves device information to hiddenfield_device
    $("#hiddenfield_device").val(window.navigator.appVersion + "," + window.navigator.platform);
    //Handles start information text 
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
    $("#hiddenfield_text").val("0");
});

$("#btnCloseWin").click(function () {
    $("#btnPlay").trigger("click");
    return false;
});

function ipLookUp() {
    $.ajax('http://ip-api.com/json')
        .then(
            function success(response) {
                console.log(response);
                $("#hiddenfield_country").val(response.country + ":" + response.city);
                console.log($("#hiddenfield_country").val());
            },

            function fail(data, status) {
                console.log('Request failed.  Returned status of',
                    status);
            }
        );
}



//Credits countdown
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





