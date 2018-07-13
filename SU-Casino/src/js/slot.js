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
    slotContent = {
        img1: $("#HiddenField_Spin1").val(),
        img2: $("#HiddenField_Spin2").val(),
        img3: $("#HiddenField_Spin3").val(),
        result: $("#HiddenField_WinLose").val(),
        winChance: ""
    };

    var optionFirst = {
        speed: 10,
        duration: 1,
        stopImageNumber: slotContent.img1,
        startCallback: function () {
            //console.log('start');
        },
        slowDownCallback: function () {
            console.log('slowDown');
        },
        stopCallback: function ($stopElm) {
            //console.log('stop');
        }
    }

    var optionSecond = {
        speed: 10,
        duration: 2,
        stopImageNumber: slotContent.img2,
        startCallback: function () {
            //console.log('start2');
        },
        slowDownCallback: function () {
            //console.log('slowDown2');
        },
        stopCallback: function ($stopElm) {
           // console.log('stop2');
        }
    }

    var optionThird = {
        speed: 10,
        duration: 3,
        stopImageNumber: slotContent.img3,
        startCallback: function () {
            console.log('start3');
        },
        slowDownCallback: function () {
            console.log('slowDown3');
        },
        stopCallback: function ($stopElm) {
            slotContent.result === 'Lose' ? $(".lost").show() : $(".winner").show();
            $("#HiddenField_result").val(slotContent.winChance + ",null," + slotContent.result.toLowerCase());
            if (slotContent.result === 'Lose')
                setTimeout(function () { $("#btnPlay").click(); }, 500);
            else
                $("#message-container").show();
            
        }
    }
    rouletter = $('div.roulette1').roulette(optionFirst);
    rouletter2 = $('div.roulette2').roulette(optionSecond);
    rouletter3 = $('div.roulette3').roulette(optionThird);

}


$('.start').click(function (e) {
    e.preventDefault();
	rouletter.roulette('start');
	rouletter2.roulette('start');
	rouletter3.roulette('start');
});
