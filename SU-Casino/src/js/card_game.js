var cards = {};
var disable = false;

var card1 = document.querySelector('#betCard1');
card1.addEventListener('click', function () {
    if (!disable) {
        card1.classList.toggle('is-flipped');
        cards.sound.play();
        setTimeout(function () { cardClicked(card1); }, 500);
    }
});

var card2 = document.querySelector('#betCard2');
card2.addEventListener('click', function () {
    if (!disable) {
        card2.classList.toggle('is-flipped');
        cards.sound.play();
        setTimeout(function () { cardClicked(card2); }, 500);
    }
});

$(document).ready(function () {
    initCardGame();
    $("#message-container").hide();
   
});

$(".winchance-btn").click(function () {
    cards.winChance = getWinChance(this.id);
    $("#winchance-container").hide();
});

function initCardGame() {
    var baseUrl = "src/images/cards/";
    var theme = $("#HiddenField_theme").val();
    var game = $("#HiddenField_game").val();
    if (cards.game === "Transfer_test") {
        $("#moneyLable").hide();
    }
    if (cards.game === "DET_realworld") {
        $("#piggySpan").show();
    }
    gameInit(theme);
    var cardSound = new Audio("src/sound/effects/cardSlide.mp3");
    cards = {
        card1: $("#HiddenField_card1").val(),
        card2: $("#HiddenField_card2").val(),
        showCard: $("#HiddenField_card3").val(),
        winChance: "",
        winLose: "",
        sound: cardSound,
        game: game
    };
   
    $("[id ^= 'notEqual']").hide();
    $("#imgCard1").attr("src", baseUrl + cards.card1 + ".png");
    $("#imgCard2").attr("src", baseUrl + cards.card2 + ".png");
    $("#imgCard3").attr("src", baseUrl + cards.showCard + ".png");
   
    disable = false;
}

function cardClicked(selectedCard) {
    disable = true;
    var isWinner = false;
    var result = cards.winChance + ",";
    var winningAmount;
    $(".lost").hide();
    $(".winner").hide();
    if (selectedCard.id === 'betCard1') {
        result = result + "bet_R1,";
        if (cards.card1 === cards.showCard) {
            isWinner = true;
            $("#winCredit").html("+" + $("#HiddenField_win1").val());
        }
    }
    else {
        result = result + "bet_R2,";
        if (cards.card2 === cards.showCard) {
            isWinner = true;
            $("#winCredit").html("+" + $("#HiddenField_win2").val());
        }
    }
    console.log($("#winCredit").html());
    if (isWinner && cards.game !== "Transfer_test") {
        result = result + "win";
        $("#HiddenField_result").val(result); 
        setTimeout(function () { showWinner(winningAmount); }, 1500);
    }
    else {
        result = result + "lose";
        $("#HiddenField_result").val(result);
        setTimeout(function () { $("#btnPlay").click(); }, 1500);
    }
    console.log(result);
}

$(function () {
    document.addEventListener("keydown", function (event) {
        if (event.keyCode === 81) {
            cards.winChance = "0";
        } else if (event.keyCode === 87) {
            cards.winChance = "1";
        } else if (event.keyCode === 69) {
            cards.winChance = "2";
        } else if (event.keyCode === 82) {
            cards.winChance = "3";
        }
        $("#winchance-container").hide();
    });
});

