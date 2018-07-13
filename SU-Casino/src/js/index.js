function gameInit(theme) {
    $("#message-container").hide();
    $("#winchance-container").hide();
    var showInfo = $("#HiddenField_showInfo").val();
    showInfo === '1' ? $("#startInfo").show() : $("#winchance-container").show();

    var backgroundSoundSource = "";
    var baseSoundUrl = "src/sound/background/";
    switch (theme) {
        case '1':
            backgroundSoundSource = "bensound-badass.mp3";
            break;
        case '2':
            backgroundSoundSource = "bensound-creativeminds.mp3";
            break;
        case '3':
            backgroundSoundSource = "bensound-downtown.mp3";
            break;
        case '4':
            backgroundSoundSource = "bensound-funkyelement.mp3";
            break;
        default:
            backgroundSoundSource = "bensound-straight.mp3";
    }

    var backgroundSound = sound(baseSoundUrl+backgroundSoundSource);
    backgroundSound.play();
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
    console.log(this);
    return this;
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



