$(function() {
  document.addEventListener("keydown", function(event) {
    if (event.keyCode === 81) {
      setSelectedWinningChange("0");
    } else if (event.keyCode === 87) {
      setSelectedWinningChange("1");
    } else if (event.keyCode === 69) {
      setSelectedWinningChange("2");
    } else if (event.keyCode === 82) {
      setSelectedWinningChange("3");
    }
  });
  btnSpin.prop("disabled", true);
});

var rotationsTime = 9;
var wheelSpinTime = 6;
var ballSpinTime = 5;
var numorder = [0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26];
var numred = [
  32,
  19,
  21,
  25,
  34,
  27,
  36,
  30,
  23,
  5,
  16,
  1,
  14,
  9,
  18,
  7,
  12,
  3
];
var numblack = [
  15,
  4,
  2,
  17,
  6,
  13,
  11,
  8,
  10,
  24,
  33,
  20,
  31,
  22,
  29,
  28,
  35,
  26,
];
var numgreen = [0];
var numbg = $(".pieContainer");
var ballbg = $(".ball");
var btnSpin = $("#btnSpin");
var btnBlack = $("#btnBlack");
var btnRed = $("#btnRed");
var btnHigh = $("#btnHigh");
var btnLow = $("#btnLow");
var btnZero = $("#btnZero");
var hiddenRouletteNr = $("#HiddenFieldrouletteNr");
var hiddenWinLose = $("input[name=HiddenFieldWinLose]:hidden"); 
var btnDontKnow = $("#btnDontKnow");
var toppart = $("#toppart");
var pfx = $.keyframe.getVendorPrefix();
var transform = pfx + "transform";
var rinner = $("#rcircle");
var numberLoc = [];
var betOption = "";
var expectedWinningChance = "";
var wheelSound = new Audio("src/sound/effects/RouletteWheel.mp3");
$.keyframe.debug = false;

$(document).ready(function () {
    initRouletteGame();
});

function initRouletteGame() {
    gameInit('99');
    createWheel();
    console.log(hiddenRouletteNr);
}


function createWheel() {
    
  var temparc = 360 / numorder.length;
  for (var i = 0; i < numorder.length; i++) {
    numberLoc[numorder[i]] = [];
    numberLoc[numorder[i]][0] = i * temparc;
    numberLoc[numorder[i]][1] = i * temparc + temparc;

    newSlice = document.createElement("div");
    $(newSlice).addClass("hold");
    newHold = document.createElement("div");
    $(newHold).addClass("pie");
    newNumber = document.createElement("div");
    $(newNumber).addClass("num");

    newNumber.innerHTML = numorder[i];
    $(newSlice).attr("id", "rSlice" + i);
    $(newSlice).css(
      "transform",
      "rotate(" + numberLoc[numorder[i]][0] + "deg)"
    );

    $(newHold).css("transform", "rotate(9.73deg)");
    $(newHold).css("-webkit-transform", "rotate(9.73deg)");

    if ($.inArray(numorder[i], numgreen) > -1) {
      $(newHold).addClass("greenbg");
    } else if ($.inArray(numorder[i], numred) > -1) {
      $(newHold).addClass("redbg");
    } else if ($.inArray(numorder[i], numblack) > -1) {
      $(newHold).addClass("greybg");
    }

    $(newNumber).appendTo(newSlice);
    $(newHold).appendTo(newSlice);
    $(newSlice).appendTo(rinner);
  }
  console.log(numberLoc);
}

btnRed.click(function() {
    betOption = "2";
    btnRed.prop("disabled", false);
    btnBlack.prop("disabled", false);
    startSpinn(); 
});

btnBlack.click(function() {
  betOption = "1";
  btnRed.prop("disabled", false);
  btnBlack.prop("disabled", false);
  startSpinn();
});


$(".winchance-btn").click(function () {
    expectedWinningChance = getWinChance(this.id);
    $("#winchance-container").hide();
});

function setSelectedColor() {
  $("#selected-color").text(betOption);
}

function setSelectedWinningChange(chance) {
  expectedWinningChance = chance;
    $("#winchance-container").hide();
}

function switchButtons(disable) {
  btnBlack.prop("disabled", disable);
  btnRed.prop("disabled", disable);
}

function startSpinn() {
  var rndNum = hiddenRouletteNr[0].value; // Math.floor(Math.random() * 34 + 0);
  switchButtons(true);
  winningNum = rndNum;
  spinTo(winningNum);
}

function finishSpin() {
    let isWinner  = false;
    var result = expectedWinningChance + "," + betOption + ",";
    
    if (numred.indexOf(Number(winningNum)) > 0) {
        if (betOption === "2") {
           // result = result + "2,";
            isWinner = true;
    }
    } else {
        if (betOption === "1") {
          //  result = result + "1,";
            isWinner = true;
       }
    }
    console.log(result);
    if (!isWinner) {//TODO- function in index?
        result = result + "lose";
        $("#HiddenField_result").val(result);
        setTimeout(function () { $("#btnPlay").click(); }, 500);
    }
    else {
        result = result + "win";
        $("#HiddenField_result").val(result);
        setTimeout(function () { showWinner(); }, 300);
    }


  /*PageMethods.WinOrLose(
    isWinner ,
    betOption,
    expectedWinningChance,
    callBack,
    failed
  );*/

    console.log(winningNum, isWinner, result );
  betOption = "";
  setSelectedColor();
  switchButtons(false);
}

function failed(error) {
  console.log(error.get_message());
}

function callBack(response) {
  // här kan man "stänga ner" efteråt
  console.log(response);
}

function resetAni() {
  animationPlayState = "animation-play-state";
  playStateRunning = "running";
  $("#winnerAnnouncer").hide();
  $(ballbg)
    .css(pfx + animationPlayState, playStateRunning)
    .css(pfx + "animation", "none");

  $(numbg)
    .css(pfx + animationPlayState, playStateRunning)
    .css(pfx + "animation", "none");
  $(toppart)
    .css(pfx + animationPlayState, playStateRunning)
    .css(pfx + "animation", "none");

  $("#rotate2").html("");
  $("#rotate").html("");
}

function spinTo(num) {
  //get location
  var temp = numberLoc[num][0] + 4;

  //randomize
  var rndSpace = Math.floor(Math.random() * 360 + 1);

  resetAni();
    setTimeout(function () {
        wheelSound.play();
    bgrotateTo(rndSpace);
    ballrotateTo(rndSpace + temp);
  }, 500);
}

function ballrotateTo(deg) {
  var temptime = rotationsTime + "s";
  var dest = -360 * ballSpinTime - (360 - deg);
  $.keyframe.define({
    name: "rotate2",
    from: {
      transform: "rotate(0deg)"
    },
    to: {
      transform: "rotate(" + dest + "deg)"
    }
  });

  $(ballbg).playKeyframe({
    name: "rotate2", // name of the keyframe you want to bind to the selected element
    duration: temptime, // [optional, default: 0, in ms] how long you want it to last in milliseconds
    timingFunction: "ease-in-out", // [optional, default: ease] specifies the speed curve of the animation
    complete: function() {
      finishSpin();
    } //[optional]  Function fired after the animation is complete. If repeat is infinite, the function will be fired every time the animation is restarted.
  });
}

function bgrotateTo(deg) {
  var dest = 360 * wheelSpinTime + deg;
  var temptime = (rotationsTime * 1000 - 1000) / 1000 + "s";

  $.keyframe.define({
    name: "rotate",
    from: {
      transform: "rotate(0deg)"
    },
    to: {
      transform: "rotate(" + dest + "deg)"
    }
  });

  $(numbg).playKeyframe({
    name: "rotate", // name of the keyframe you want to bind to the selected element
    duration: temptime, // [optional, default: 0, in ms] how long you want it to last in milliseconds
    timingFunction: "ease-in-out", // [optional, default: ease] specifies the speed curve of the animation
    complete: function() {} //[optional]  Function fired after the animation is complete. If repeat is infinite, the function will be fired every time the animation is restarted.
  });

  $(toppart).playKeyframe({
    name: "rotate", // name of the keyframe you want to bind to the selected element
    duration: temptime, // [optional, default: 0, in ms] how long you want it to last in milliseconds
    timingFunction: "ease-in-out", // [optional, default: ease] specifies the speed curve of the animation
    complete: function() {} //[optional]  Function fired after the animation is complete. If repeat is infinite, the function will be fired every time the animation is restarted.
  });
}
