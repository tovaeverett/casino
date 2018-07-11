var cards = {};
var disable = false;

var card1 = document.querySelector('#betCard1');
card1.addEventListener('click', function () {
    if (!disable) {
        card1.classList.toggle('is-flipped');
        setTimeout(function () { cardClicked(card1); }, 500);
        console.log($(this).find('img'));
    }
});

var card2 = document.querySelector('#betCard2');
card2.addEventListener('click', function () {
    if (!disable) {
        card2.classList.toggle('is-flipped');
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
    gameInit();
    cards = {
        card1: $("#HiddenField_card1").val(),
        card2: $("#HiddenField_card2").val(),
        showCard: $("#HiddenField_card3").val(),
        winChance: "",
        winLose:""
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
    var result = "";
    $(".lost").hide();
    $(".winner").hide();
    if (selectedCard.id === 'betCard1') {
        result = "PressCard:1,";
        if (cards.card1 === cards.showCard) {
            isWinner = true;
        }
    }
    else {
        result = "PressCard:2,";
        if (cards.card2 === cards.showCard) {
            isWinner = true;
        }
    }
    result = result + "WinChance:" + cards.winChance + ",";
    if (!isWinner) {
        result = result + "WinLose:lose";
        //result = JSON.stringify(cards).split('"').join('');
        $("#HiddenField_result").val(result);
        setTimeout(function () { $("#btnPlay").click(); }, 500);
    }
    else {
        result = result + "WinLose:win";
        $("#HiddenField_result").val(result);
        $(".winner").show();
        $("#message-container").show();
    }
    console.log(result);
}

