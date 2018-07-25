var slotContent = {};
var rouletter = {};
var rouletter2 = {};
var rouletter3 = {};


$(document).ready(function () {
    initSlotGame();
});

$(".winchance-btn").click(function () {
    slotContent.winChance = getWinChance(this.id);
    $("#winchance-container").hide();
});


function initSlotGame() {
    var theme = $("#HiddenField_theme").val();
    gameInit(theme);
    $(".winner").hide();
    $(".lost").hide();
    var slotSound = new Audio("src/sound/effects/slotMachine.wav");
    slotContent = {
        img1: $("#HiddenField_Spin1").val(),
        img2: $("#HiddenField_Spin2").val(),
        img3: $("#HiddenField_Spin3").val(),
        result: $("#HiddenField_WinLose").val(),
        winChance: "",
        sound: slotSound
    };

    var optionFirst = {
        speed: 10,
        duration: 0.5,
        stopImageNumber: slotContent.img1,
        startCallback: function () {
            
        },
        slowDownCallback: function () {
            
        },
        stopCallback: function ($stopElm) {
           
        }
    }

    var optionSecond = {
        speed: 10,
        duration: 1,
        stopImageNumber: slotContent.img2,
        startCallback: function () {
           
        },
        slowDownCallback: function () {
            
        },
        stopCallback: function ($stopElm) {
           
        }
    }

    var optionThird = {
        speed: 10,
        duration: 1.5,
        stopImageNumber: slotContent.img3,
        startCallback: function () {
           
        },
        slowDownCallback: function () {
           
        },
        stopCallback: function ($stopElm) {
            setTimeout(function () { slotContent.sound.pause(); }, 300);
            slotContent.result === 'Lose' ? $(".lost").show() : $(".winner").show();
            $("#HiddenField_result").val(slotContent.winChance + ",null," + slotContent.result.toLowerCase());
            if (slotContent.result === 'Lose')
                setTimeout(function () { $("#btnPlay").click(); }, 1500);
            else
               setTimeout(function () { showWinner(); }, 1000);
            
        }
    }
    rouletter = $('div.roulette1').roulette(optionFirst);
    rouletter2 = $('div.roulette2').roulette(optionSecond);
    rouletter3 = $('div.roulette3').roulette(optionThird);

}


$('.start').click(function (e) {
    e.preventDefault();
    $(this).prop("disabled", true);
    slotContent.sound.loop = true;
    slotContent.sound.play();
	rouletter.roulette('start');
	rouletter2.roulette('start');
	rouletter3.roulette('start');
});

$(function () {
    document.addEventListener("keydown", function (event) {
        if (event.keyCode === 81) {
            slotContent.winChance="0";
        } else if (event.keyCode === 87) {
            slotContent.winChance = "1";
        } else if (event.keyCode === 69) {
            slotContent.winChance = "2";
        } else if (event.keyCode === 82) {
            slotContent.winChance = "3";
        }
        $("#winchance-container").hide();
    });
});