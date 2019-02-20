//Game logic for CardDraw.aspx
//Using functions in index.js

//****Globals 
var cards = {};
var disable = false;

//**** Animation for card 1 *****//
var card1 = document.querySelector('#betCard1');
$('#betCard1').one('click', function (event) {
    event.preventDefault();
    if (!disable) {
        showBet(event, $("#HiddenField_Bet_Card1").val(), () => {
            card1.classList.toggle('is-flipped');
            cards.sound.play();
            setTimeout(function () { cardClicked(card1); }, 500);
        });
    }
    $(this).prop('disabled', true);
});


//**** Animation for card 2*****//
var card2 = document.querySelector('#betCard2');
$('#betCard2').one('click', function (event) {
    event.preventDefault();
    if (!disable) {
        showBet(event, $("#HiddenField_Bet_Card2").val(), () => {
            card2.classList.toggle('is-flipped');
            cards.sound.play();
            setTimeout(function () { cardClicked(card2); }, 500);
        });
    }
    $(this).prop('disabled', true);
});




$(document).ready(function () {
    initCardGame();
    $("#message-container").hide(); 
    $(".winner").hide();
});

/****
    Init the cardgame:
    - if the game is Transfer_test: hide the credit sum  
    - if the game is DET_realworld: show an image of a piggy bank and additional text 
    - 
 ****/

function initCardGame() {
  
    var baseUrl = "src/images/cards/";
    var cardSound = new Audio("src/sound/effects/cardSlide.mp3");
    var theme = "";
    var game = $("#HiddenField_game").val();

    //**Hide credits**//
    if (game === "Transfer_test") {
        $("#moneyLable").hide();
    }
    //** Show the piggy bank image and text **//
    if (game === "DET_realworld") {
        $(".winSpan").addClass("winSpanSpecial"); 
        $("#piggySpan").css('display','block');
    }

    gameInit(theme);//in index.js

    
    //**Sets values for the cards and the type of card game, showCard is the card in the middle.**//
    cards = {
        card1: $("#HiddenField_card1").val(),
        card2: $("#HiddenField_card2").val(),
        showCard: $("#HiddenField_card3").val(),
        winChance: "",
        winLose: "",
        sound: cardSound,
        game: game
    };
   
    $("#imgCard1").attr("src", baseUrl + cards.card1 + ".png");
    $("#imgCard2").attr("src", baseUrl + cards.card2 + ".png");
    $("#imgCard3").attr("src", baseUrl + cards.showCard + ".png");
   
    disable = false;
}


 /*
    Card selected: checking if it's a win or lose, which card that was selected and saving that with the winchance response to HiddenField_result,
    example:1,bet_R1,win
    - if the game is Instrumental_acq2: bet_R3 or bet_R4 is saved for selected card
    - if the game is Transfer_test: No winning animation should be shown
    - HiddenField_win1 & HiddenField_win2 contains the winning amount that is shown in the winning animation
 */

function cardClicked(selectedCard) {
    var ClickTime = new Date();
    $("#HiddenField_Time2").val(ClickTime.getTime());

    var isWinner = false;
    var result = cards.winChance + ",";
    disable = true;
   
    if (selectedCard.id === 'betCard1') {
        result = cards.game === 'Instrumental_acq2' ? result + "bet_R3," : result + "bet_R1,";
        if (cards.card1 === cards.showCard) {
            isWinner = true;
            $("#winCredit").html("+" + $("#HiddenField_win1").val());
        }
    }
    else {
        result = cards.game === 'Instrumental_acq2' ? result + "bet_R4," : result + "bet_R2,";
        if (cards.card2 === cards.showCard) {
            isWinner = true;
            $("#winCredit").html("+" + $("#HiddenField_win2").val());
        }
    }
    if (isWinner) {
        result = result + "win";
        $("#HiddenField_result").val(result);
        if (cards.game !== "Transfer_test") {
            setTimeout(function () { showWinner(); }, 1500);
        }
        else
            setTimeout(function () {
                var EndTime = new Date();
                $("#HiddenField_Time3").val(EndTime.getTime());
                $("#btnPlay").click();
            }, 1500);
    }
   
    else {
        result = result + "lose";
        $("#HiddenField_result").val(result);
        setTimeout(function () { 
            var EndTime = new Date();
            $("#HiddenField_Time3").val(EndTime.getTime());
            $("#btnPlay").click(); 
        }, 1500);
    }
}



$(".winchance-btn").click(function () {
    cards.winChance = getWinChance(this.id);
    $("#winchance-container").hide();
});


$(function () {
    document.addEventListener("keydown", function (event) {
        if (event.keyCode === 81) {
            cards.winChance = "0";
            $("#winchance-container").hide();
        } else if (event.keyCode === 87) {
            cards.winChance = "1";
            $("#winchance-container").hide();
        } else if (event.keyCode === 69) {
            cards.winChance = "2";
            $("#winchance-container").hide();
        } else if (event.keyCode === 82) {
            cards.winChance = "3";
            $("#winchance-container").hide();
        }
        
    });
});

