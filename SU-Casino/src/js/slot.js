//Game logic for OneArmdBandt.aspx
//Using functions in index.js
//Using roulette.js

/**Globals**/
var slotContent = {};
var rouletter = {};
var rouletter2 = {};
var rouletter3 = {};


$(document).ready(function () {
    initSlotGame();
    $(".winner").hide();
});


function initSlotGame() {
    /*** Sets the start images from local storage if not the first trail ***/
    if (localStorage.getItem("images") && localStorage.getItem("images") !== null) {
        var images = localStorage.getItem("images").split(',');
        images[0] = Number(images[0]) + 1;
        images[1] = Number(images[1]) + 1;
        images[2] = Number(images[2]) + 1;
        var src1 = "/src/images/slot/img" + images[0] + ".png";
        var src2 = "/src/images/slot/img" + images[1] + ".png";
        var src3 = "/src/images/slot/img" + images[2] + ".png";
    } else {
        var src1 = "/src/images/slot/img1.png";
        var src2 = "/src/images/slot/img2.png";
        var src3 = "/src/images/slot/img3.png";
    }
    $("#slot_1_1").attr('src', src1);
    $("#slot_2_1").attr('src', src2);
    $("#slot_3_1").attr('src', src3);

    var theme = $("#HiddenField_theme").val();
    gameInit(theme);// in index.js

    var slotSound = new Audio("src/sound/effects/slotMachine.wav");

    slotContent = {
        img1: $("#HiddenField_Spin1").val(),
        img2: $("#HiddenField_Spin2").val(),
        img3: $("#HiddenField_Spin3").val(),
        result: $("#HiddenField_WinLose").val(),
        winChance: "",
        sound: slotSound
    };
    
    let spinDelay1 = Number($("#HiddenField_spin_delay1").val());
    let spinDelay2 = Number($("#HiddenField_spin_delay2").val());
    let middleSlotIsLongest = false; 
    if(spinDelay1 <= 0) {
        spinDelay1 = 0.75;
    }   
    if(spinDelay2 <= 0) {
        spinDelay2 = 1;
    }
    console.log("spinDelay1" , spinDelay1);
    console.log("spinDelay2" , spinDelay2);
    if(spinDelay1 > spinDelay2) {
        middleSlotIsLongest = true; 
       
    }
   
    /**** First  slot animation object ****/
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
    /**** Second slot animation object ****/
    var optionSecond = {
        speed: 30,
        duration: spinDelay1,
        stopImageNumber: slotContent.img2,
        startCallback: function () {

        },
        slowDownCallback: function () {

        },
        stopCallback: function ($stopElm) {
            if(middleSlotIsLongest) {
                endSpin();
            }
        }
        
    }
    /**** Third slot animation object ****/
    var optionThird = {
        speed: 30,
        duration: spinDelay2,
        stopImageNumber: slotContent.img3,
        startCallback: function () {

        },
        slowDownCallback: function () {

        },
        /**
         checking if it's a win or lose, which card that was selected and saving that with the winchance response to HiddenField_result,
         example: 1, bet_R1, win
         **/
        stopCallback: function ($stopElm) {
            if(!middleSlotIsLongest) {
                endSpin();
            }
        }
    }
    rouletter = $('div.roulette1').roulette(optionFirst);
    rouletter2 = $('div.roulette2').roulette(optionSecond);
    rouletter3 = $('div.roulette3').roulette(optionThird);
    initMultiplyButton()
}

function endSpin() {
    setTimeout(function () {
        slotContent.sound.pause();
    }, 300);
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
        setTimeout(function () {
            showWinner();
        }, 1000);
    setTimeout(function () {
        $('#multiplier_0').prop("disabled", false);
    }, 1000);
}

function initMultiplyButton() {
    let multiplyValue = $("#HiddenField_Multiply").val();
    if (multiplyValue) {
        document.querySelector("#multiplier > tbody > tr > td > label").textContent = "x" + multiplyValue;
        if ($('#multiplier_0').prop('checked')) {
            $('#multiplier').css('opacity', '1');
            $("#winCredit").html("+" + $("#HiddenField_Multiplied_Credit").val());
        } else {
            $('#multiplier').css('opacity', '0.4');
            $("#winCredit").html("+" + $("#HiddenField_credit").val());
        }
    } else {
        $('#multiplier').css('visibility', 'hidden');
    }
}

$('.start').click(function (e) {
    e.preventDefault();
    //Disable Multiply Button during spin 
    $('#multiplier_0').prop("disabled", true);
    var ClickTime = new Date();
    $("#HiddenField_Time2").val(ClickTime.getTime());
   
    $(this).prop("disabled", true);
    //Set Betvalue conditionally 
    let betValue = $("#HiddenField_Bet_R1").val()
    if ($('#multiplier_0').prop('checked')) {
        betValue = $("#HiddenField_Multiplied_Bet_R1").val()
    }

    showBet(e, betValue, 28, 50, () => {
        slotContent.sound.loop = true;
        slotContent.sound.play();
        rouletter.roulette('start');
        rouletter2.roulette('start');
        rouletter3.roulette('start');
    });
});


$('.multiplyButton').on("change", function (event) {
    if ($('#multiplier_0').prop('checked')) {
        $('#multiplier').css('opacity', '1');
        //Animation to enhance Multiplier
        showMultiply(event, "x" + $("#HiddenField_Multiply").val(), this.offsetLeft, this.offsetHeight,  () => {
        })
        $("#winCredit").html("+" + $("#HiddenField_Multiplied_Credit").val());
    } else {
        $('#multiplier').css('opacity', '0.4');
        $("#winCredit").html("+" + $("#HiddenField_credit").val());
    }
});

$(".winchance-btn").click(function () {
    slotContent.winChance = getWinChance(this.id);
    $("#winchance-container").hide();

});

$(function () {
    document.addEventListener("keydown", function (event) {
        if (event.keyCode === 81) {
            slotContent.winChance = "0";
            $("#winchance-container").hide();
        } else if (event.keyCode === 87) {
            slotContent.winChance = "1";
            $("#winchance-container").hide();
            /*} else if (event.keyCode === 69) {
                slotContent.winChance = "2";*/
        } else if (event.keyCode === 82) {
            slotContent.winChance = "3";
            $("#winchance-container").hide();
        }
        //$("#winchance-container").hide();
    });
});