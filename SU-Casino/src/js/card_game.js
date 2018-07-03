var cards = {};
var disable = false;

var card1 = document.querySelector('#betCard1');
card1.addEventListener('click', function () {
    if (!disable) {
        card1.classList.toggle('is-flipped');
        //setTimeout(function () { cardClicked(card1) }, 2000);
        console.log($(this).find('img'));
    }
});

var card2 = document.querySelector('#betCard1');
card2.addEventListener('click', function () {
    if (!disable) {
        card2.classList.toggle('is-flipped');
        //setTimeout(function () { cardClicked(card2) }, 2000);
    }
});



$(document).ready(function () {
    initGame();
    console.log(cards);
});

function initGame() {
    var baseUrl = "src/images/cards/";
    cards = {
        card1:'2', //$("#HiddenField_card1").val(),
        card2:'3', //$("#HiddenField_card2").val(),
        card3:'3', //$("#HiddenField_card3").val()
    };
    $("[id ^= 'notEqual']").hide();
   /* $("#imgCard1").attr("src", baseUrl + cards.card1 + "C.png");
    $("#imgCard2").attr("src", baseUrl + cards.card2 + "C.png");
    $("#imgCard3").attr("src", baseUrl + cards.card3 + "C.png");*/
   
    disable = false;
}

function cardClicked(selectedCard) {
    console.log(selectedCard, $(card1).find('img')[1].id);
    disable = true;
    var isWinner = false;
    if (selectedCard.id === 'betCard1') {
        if (cards.card1 === cards.card2) {
            alert('winner!');
            isWinner = true;
        }
        else {
            alert('Sorry!');
        }
    }
    else {

        if (cards.card3 === cards.card2) {
            alert('winner!');
            isWinner = true;
        }
        else {
            alert('Sorry!');
        }
    }
}

function resetGame() {
    resultMessage = hide();
    initGame();
}
