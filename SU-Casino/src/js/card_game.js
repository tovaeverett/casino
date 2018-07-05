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
    initGame();
    $("#winchance-container").hide();
    $("#message-container").hide();
    console.log(cards);
});

$("#btnClose").click(function () {
    $("#message-container").hide();
});

function initGame() {
    var baseUrl = "src/images/cards/";
    cards = {
        card1:'3', //$("#HiddenField_card1").val(),
        card2:'2', //$("#HiddenField_card2").val(),
        showCard:'2' //$("#HiddenField_card3").val()
    };
    $("[id ^= 'notEqual']").hide();
    $("#imgCard1").attr("src", baseUrl + cards.card1 + "C.png");
    $("#imgCard2").attr("src", baseUrl + cards.card2 + "C.png");
    $("#imgCard3").attr("src", baseUrl + cards.showCard + "C.png");
   
    disable = false;
}

function cardClicked(selectedCard) {
    console.log(selectedCard, $(card1).find('img')[1].id);
    disable = true;
    var isWinner = false;
    if (selectedCard.id === 'betCard1') {
        $("#HiddenField_card2").val("null");
        if (cards.card1 === cards.showCard) {
            $("#message-container").show();
            isWinner = true;
        }
    }
    else {
        $("#HiddenField_card1").val("null");
        if (cards.card2 === cards.showCard) {
            $("#message-container").show();
            isWinner = true;
        }
    }
    isWinner ? $("#HiddenFieldWinLose").val("win") : $("#HiddenFieldWinLose").val("lose"); 
    console.log($("#HiddenFieldWinLose").val());
}

function resetGame() {
    resultMessage = hide();
    initGame();
}
