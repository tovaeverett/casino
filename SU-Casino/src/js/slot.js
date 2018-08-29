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
    console.log(localStorage.getItem("images")); 

    if (localStorage.getItem("images") && localStorage.getItem("images")!== null ) {
        var images = localStorage.getItem("images").split(',');
        images[0] = Number(images[0]) + 1;
        images[1] = Number(images[1]) + 1;
        images[2] = Number(images[2]) + 1;
        var src1 = "/src/images/slot/img" + Number(images[0])+ ".png";
        var src2 = "/src/images/slot/img" + Number(images[1]) + ".png";
        var src3 = "/src/images/slot/img" + Number(images[2]) + ".png";
    }
    else {
        /*var src1 = "/src/images/slot/img" + Math.floor(Math.random() * 5) + ".png";
        var src2 = "/src/images/slot/img" + Math.floor(Math.random() * 5) + ".png";
        var src3 = "/src/images/slot/img" + Math.floor(Math.random() * 5) + ".png";*/
        var src1 = "/src/images/slot/img1.png";
        var src2 = "/src/images/slot/img2.png";
        var src3 = "/src/images/slot/img3.png";
    }
    $("#slot_1_1").attr('src', src1);
    $("#slot_2_1").attr('src', src2);
    $("#slot_3_1").attr('src', src3);

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
        speed: 30,
        duration: 0.25,
        stopImageNumber: slotContent.img1,
        startCallback: function () {
            
        },
        slowDownCallback: function () {
            
        },
        stopCallback: function ($stopElm) {
           
        }
    }

    var optionSecond = {
        speed: 30,
        duration: 0.75,
        stopImageNumber: slotContent.img2,
        startCallback: function () {
           
        },
        slowDownCallback: function () {
            
        },
        stopCallback: function ($stopElm) {
           
        }
    }

    var optionThird = {
        speed: 30,
        duration: 1,
        stopImageNumber: slotContent.img3,
        startCallback: function () {
           
        },
        slowDownCallback: function () {
           
        },
        stopCallback: function ($stopElm) {
            setTimeout(function () { slotContent.sound.pause(); }, 300);
            slotContent.result === 'lose' ? $(".lost").show() : $(".winner").show();
            $("#HiddenField_result").val(slotContent.winChance + ",null," + slotContent.result.toLowerCase());
            localStorage.setItem("images", slotContent.img1 + ',' + slotContent.img2 + ',' + slotContent.img3);
            if (slotContent.result === 'lose')
                setTimeout(function () {
                    var EndTime = new Date();
                    $("#HiddenField_Time3").val(EndTime.getTime());
                    $("#btnPlay").click();
                }, 1500);
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