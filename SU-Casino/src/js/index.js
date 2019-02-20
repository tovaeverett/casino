//Functins for game logic in the application

//**Globals**//
var backgroundSound;
var game = $('body')[0].id;
var theme = "";


/*

*/
function gameInit(sentTheme) {
    var StartTime = new Date();
    var showInfo = $("#HiddenField_showInfo").val();
    
    //Hides messages layers
    $("#message-container").hide();
    $("#winchance-container").hide();

    //If first sequence show start info and sets hidden field to empty
    $("#introInfoText").html($("#Hiddenfield_text").val());
    $("#Hiddenfield_text").val("");

    //Gets the win amount 
    $("#winCredit").html("+" + $("#HiddenField_credit").val());
    theme = sentTheme;
    
    $("#HiddenField_Time1").val(StartTime.getTime());
    console.log(StartTime.getTime());
   
    if (showInfo === '1') {
        $("#startInfo").show();
    }
    else {
        if ( game !== 'roulette' && $("#HiddenField_Trail").val() === '0')
            $("#winchance-container").show();
    }

    //Deprecated - Not used but keeping the code if they should want it again //
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

   // backgroundSound = sound(baseSoundUrl + backgroundSoundSource);
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
   // backgroundSound.stop();
    var winnerSound = new Audio("src/sound/effects/cashoutWinning.mp3");
    winnerSound.play();
    $("#message-container").fireworks({
        sound: true, // sound effect
        opacity: 0.5,
        width: "100%",
        height: "100%"
    });
    $(".winner").show();
    $("#message-container").show();
}



//Closing the start information layer
$('#btnShowInfo').click(function () {
    $("#startInfo").hide();
    if (game !== 'roulette' && $("#HiddenField_Trail").val() === '0')
        $("#winchance-container").show();
    //backgroundSound.play();
 
});

$("#btnClose").click(function () {
    $("#message-container").hide();
});

//Closing the win announce layer
$("#btnCloseWin").click(function () {
    var EndTime = new Date();
    $("#HiddenField_Time3").val(EndTime.getTime());
    $("#btnPlay").trigger("click");
    return false;
});


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

function showBet(event, betValue, offSetX, offSetY, callback) {
    const x = event.clientX;
    const y = event.clientY;
    document.getElementById("currentBet").innerText = betValue;
    document.getElementById("currentBet").setAttribute(`style`, `position: fixed; top: ${y - offSetY}px; left: ${x - offSetX}px; font-size: 40px;`);
    animateCss('#currentBet', 'zoomOut', function () {
        document.getElementById("currentBet").setAttribute(`style`, `display: none;`);
        if (typeof callback === 'function') callback();
    });
}

function animateCss(element, animationName, callback) {
    const node = document.querySelector(element)
    node.classList.add('animated', animationName)

    function handleAnimationEnd() {
        node.classList.remove('animated', animationName)
        node.removeEventListener('animationend', handleAnimationEnd)

        if (typeof callback === 'function') callback()
    }

    node.addEventListener('animationend', handleAnimationEnd)
}

