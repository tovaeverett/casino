//Functins for game logic in the application

//**Globals**//
let backgroundSound;
let game = $('body')[0].id;
let theme = "";


function gameInit(sentTheme) {
    let StartTime = new Date();
    let showInfo = $("#HiddenField_showInfo").val();
    $("#jackpotInfo").hide();
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
    } else {
        if (game !== 'roulette' && $("#HiddenField_Trail").val() === '0')
            $("#winchance-container").show();

    }

    //Deprecated - Not used but keeping the code if they should want it again //
    let backgroundSoundSource = "";
    let baseSoundUrl = "src/sound/background/";
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
    let winnerSound = new Audio("src/sound/effects/cashoutWinning.mp3");
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

function showJackpot() {

    // if (!jackpotTextType) {
    //     return;
    // }
    $("#jackpotInfo").show();

  
    setInterval(function () {
      console.log(
          
      )
    });
    // REMOVE TO HAVE ACTUAL TIME
    // $("body").bgrotate({ delay: 4000, imagedir: "'/src/images/backgrounds/'", images: ["banner-background.png"] });
    setTimeout(
        function () {
            $("#jackpotInfo").hide();
        }, parseInt(HiddenField_jackpot_time.value) * 1000);
}


//Closing the start information layer
$('#btnShowInfo').click(function () {
    $("#startInfo").hide();
    showJackpot();
    if (game !== 'roulette' && $("#HiddenField_Trail").val() === '0')
        $("#winchance-container").show();
    //backgroundSound.play();

});

$("#btnClose").click(function () {
    $("#message-container").hide();
});

//Closing the win announce layer
$("#btnCloseWin").click(function () {
    let EndTime = new Date();
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
    let x = event.clientX;
    let y = event.clientY;
    document.getElementById("currentBet").innerText = betValue;
    document.getElementById("currentBet").setAttribute(`style`, `font-size: 60px;`);
    h = $("#currentBet").outerHeight();
    w = $("#currentBet").outerWidth() / 2;
    document.getElementById("currentBet").setAttribute(`style`, `position: fixed; top: ${y - h}px; left: ${x - w}px; font-size: 60px;`);
    animateCss('#currentBet', 'zoomOut', function () {
        document.getElementById("currentBet").setAttribute(`style`, `display: none;`);
    });
    if (typeof callback === 'function') callback();
    /*
    animateCss('#currentBet', 'zoomOut', function () {
        document.getElementById("currentBet").setAttribute(`style`, `display: none;`);
        if (typeof callback === 'function') callback();
    });
    */
}

function showMultiply(event, betValue, offSetX, offSetY, callback) {
    // let h = $("#multiplier").position().top;
    // let w = $("#multiplier").position().left;
    document.getElementById("multiplyNumber").innerText = betValue;
    document.getElementById("multiplyNumber").setAttribute(`style`, `font-size: 22px;`);
    document.getElementById("multiplyNumber").setAttribute(`style`, `position: absolute; top: ${offSetY - 16}px; left: ${offSetX + 9}px; font-size: 30px;`);
    animateCss('#multiplyNumber', 'zoomIn', function () {
        document.getElementById("multiplyNumber").setAttribute(`style`, `display: none;`);
    });
    if (typeof callback === 'function') callback();
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


