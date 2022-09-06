$( document ).ready(function() {
    var colors = [
      "purple",
      "blue",
      "green",
      "orange"
    ];
    var randomIndex = Math.floor(Math.random() * colors.length);
    $(".fp-hero").addClass(colors[randomIndex]);
});