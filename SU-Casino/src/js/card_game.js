var card1 = document.querySelector('#betCard1');
card1.addEventListener( 'click', function() {
  card1.classList.toggle('is-flipped');
});

var card2 = document.querySelector('#betCard2');
card2.addEventListener( 'click', function() {
  card2.classList.toggle('is-flipped');
});
