//= test
var menuburger = document.querySelector(".menu-burger");
var menu = document.querySelector(".menu");
menuburger.addEventListener("click", function(event) {
  event.preventDefault();
  menu.classList.toggle("menu-open");
});
