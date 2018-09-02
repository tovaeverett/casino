var backgroundSound;
var game = $('body')[0].id;
var theme = "";



function gameInit(sentTheme) {
    $("#message-container").hide();
    $("#winchance-container").hide();
    $("#introInfoText").html($("#Hiddenfield_text").val());
    $("#Hiddenfield_text").val("0");
    $("#winCredit").html("+" + $("#HiddenField_credit").val());
    theme = sentTheme;
    var StartTime = new Date();
    $("#HiddenField_Time1").val(StartTime.getTime());
    console.log(StartTime.getTime());
    var showInfo = $("#HiddenField_showInfo").val();
    if (showInfo === '1') {
        $("#startInfo").show();
    }
    else {
        if (theme !== '99')
            $("#winchance-container").show();
    }
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

    backgroundSound = sound(baseSoundUrl + backgroundSoundSource);
    //if(theme !== "null")
    //backgroundSound.play();
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

function showWinner(creditAmount) {
    backgroundSound.stop();
    var winnerSound = new Audio("src/sound/effects/cashoutWinning.mp3");
    winnerSound.play();
    $("#message-container").fireworks({
        sound: true, // sound effect
        opacity: 0.5,
        width: "100%",
        height: "100%"
    });
    $(".winner").show();
    //$(".winner-inner").addClass('zoom');
    $("#message-container").show();
}

function sound(src) {
    this.sound = document.createElement("audio");
    this.sound.src = src;
    this.sound.setAttribute("preload", "");
    this.sound.setAttribute("controls", "none");
    this.sound.style.display = "none";
    document.body.appendChild(this.sound);
    this.play = function () {
        this.sound.play();
    }
    this.stop = function () {
        this.sound.pause();
    }
    //console.log(this);
    return this;
}

$('#btnShowInfo').click(function () {
    $("#startInfo").hide();
    if(game !== 'roulette' && theme !== '99')
        $("#winchance-container").show();
    //backgroundSound.play();
 
});

$("#btnClose").click(function () {
    $("#message-container").hide();
});

$("#btnCloseWin").click(function () {
    var EndTime = new Date();
    $("#HiddenField_Time3").val(EndTime.getTime());
    $("#btnPlay").trigger("click");
    return false;
});



