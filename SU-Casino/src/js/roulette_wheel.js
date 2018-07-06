$(function() {
  document.addEventListener("keydown", function(event) {
    if (event.keyCode === 81) {
      setSelectedWinningChange("high");
    } else if (event.keyCode === 87) {
      setSelectedWinningChange("low");
    } else if (event.keyCode === 69) {
      setSelectedWinningChange("zero");
    } else if (event.keyCode === 82) {
      setSelectedWinningChange("dont know");
    }
  });

  var $guessChange = $("#firstShow");
  $guessChange[0].showModal();
  // dialogPolyfill.registerDialog($guessChange);

  btnSpin.prop("disabled", true);

  // $("#selected-color").on("DOMSubtreeModified", function(event) {
  //   console.log(event);
  //   var changedColor = event.currentTarget.innerText
  //     ? event.currentTarget.innerText
  //     : event.srcElement.innerText;
  //   console.log(changedColor);
  //   if (changedColor === "") {
  //     console.log("tom text");
  //     if (!btnSpin[0].disabled) {
  //       btnSpin.prop("disabled", true);
  //     }
  //   } else {
  //     console.log("inte tom text");
  //     btnSpin.prop("disabled", false);
  //   }
  // });
});

var rotationsTime = 8;
var wheelSpinTime = 6;
var ballSpinTime = 5;
var numorder = [
  0,
  32,
  15,
  19,
  4,
  2,
  25,
  17,
  34,
  6,
  27,
  13,
  36,
  11,
  30,
  8,
  23,
  10,
  5,
  24,
  16,
  33,
  1,
  20,
  14,
  31,
  9,
  22,
  18,
  29,
  7,
  28,
  12,
  35,
  3,
  26
];
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
  26
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
var hiddenWinLose = $("input[name=HiddenFieldWinLose]:hidden"); // $('input[name="testing"]').val('Work!');
var btnDontKnow = $("#btnDontKnow");
var toppart = $("#toppart");
var pfx = $.keyframe.getVendorPrefix();
var transform = pfx + "transform";
var rinner = $("#rcircle");
var numberLoc = [];
var betOption = "";
var expectedWinningChance = "";
$.keyframe.debug = true;

createWheel();
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
  betOption = "red";
  setSelectedColor();
  btnSpin.prop("disabled", false);
});

btnBlack.click(function() {
  betOption = "black";
  setSelectedColor();
  btnSpin.prop("disabled", false);
});

btnHigh.click(function() {
  setSelectedWinningChange("high");
});

btnLow.click(function() {
  setSelectedWinningChange("low");
});

btnZero.click(function() {
  setSelectedWinningChange("zero");
});

btnDontKnow.click(function() {
  setSelectedWinningChange("dont know");
});

function setSelectedColor() {
  $("#selected-color").text(betOption);
}

function setSelectedWinningChange(chance) {
  $("#selected-winning-chance").text(
    `You expect your winning change to be: ${chance}`
  );
  expectedWinningChance = chance;
  //$accountDeleteDialog[0].close();
  //$("#firstShow").hide();
  $("#firstShow")[0].close();
}

function switchButtons(disable) {
  btnBlack.prop("disabled", disable);
  btnRed.prop("disabled", disable);
  btnSpin.prop("disabled", disable);
}

btnSpin.click(function() {
  var rndNum = hiddenRouletteNr[0].value; // Math.floor(Math.random() * 34 + 0);
  switchButtons(true);
  winningNum = rndNum;
  spinTo(winningNum);
});

function finishSpin() {
  let isWin = false;
  if (numred.indexOf(winningNum) > 0) {
    if (betOption === "red") {
      winnerWinnerChickenDinner();
    }
  } else {
    if (betOption === "black") {
      winnerWinnerChickenDinner();
    }
  }

  PageMethods.WinOrLose(
    isWin,
    betOption,
    expectedWinningChance,
    callBack,
    failed
  );

  console.log(winningNum, isWin);
  betOption = "";
  setSelectedColor();
  switchButtons(false);

  btnSpin.prop("disabled", true);
}

function winnerWinnerChickenDinner() {
  $("#winnerAnnouncer").show();
  isWin = true;

  // https://www.jqueryscript.net/animation/Realistic-Fireworks-Animations-Using-jQuery-And-Canvas-fireworks-js.html
  $("#winnerAnnouncer").fireworks({
    sound: true, // sound effect
    opacity: 0.9,
    width: "100%",
    height: "100%"
  });
}

function failed(error) {
  alert(error.get_message());
}

function callBack(response) {
  // här kan man "stänga ner" efteråt
  alert(response);
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
  setTimeout(function() {
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
