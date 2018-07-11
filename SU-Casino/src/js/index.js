function gameInit() {
    var showInfo = $("#HiddenField_showInfo").val();
    showInfo === '1' ? $("#startInfo").show() : $("#winchance-container").show();
}

function getWinChance(button) {
    switch (button) {
        case 'btnHigh':
            return 3;
        case 'btnLow':
            return 2;
        case 'btnZero':
            return 1;
        case 'btnDontKnow':
            return 0;
    }
}

function sound(src) {
    this.sound = document.createElement("audio");
    this.sound.src = src;
    this.sound.setAttribute("preload", "auto");
    this.sound.setAttribute("controls", "none");
    this.sound.style.display = "none";
    document.body.appendChild(this.sound);
    this.play = function () {
        this.sound.play();
    }
    this.stop = function () {
        this.sound.pause();
    }
}

$('#btnShowInfo').click(function () {
    $("#startInfo").hide();
    $("#winchance-container").show();
 
});

$("#btnClose").click(function () {
    $("#message-container").hide();
});

$("#btnCloseWin").click(function () {
    $("#btnPlay").trigger("click");
    return false;
});

