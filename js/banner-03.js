// JavaScript Document
var featureItemActive = 1;
var featureTimer;
var featureLoop = 1;
var featureDelay = 4000;
var numSlides = 0;
function showFeature(featureNo) {
var previousImage = jQuery('.fcc_carousel .active');
jQuery('.featureIndex li a').removeClass('featureIndexOn');
jQuery('.featureIndex' + featureNo + ' a').addClass('featureIndexOn');
jQuery('.featureIndexDots li').removeClass('featureIndexOn');
jQuery('.featureIndexDots' + featureNo).addClass('featureIndexOn');
jQuery(previousImage).fadeOut(function() {
jQuery(this).removeClass('active');
jQuery('.featureImage' + featureNo).addClass('active').fadeIn();
});
}
function rotateFeature() {
// Increase the active item index
featureItemActive++;
// Increase the number of times we've looped in case we want to know that in the future
// Reset the active item to 1 if we've reached the last slide
if (featureItemActive > numSlides) {
featureLoop++;
featureItemActive = 1
}
// Show the active feature
showFeature(featureItemActive);
}
//var $j = jQuery.noConflict();
jQuery(document).ready(function() {
numSlides = jQuery('.featureImage').length;
jQuery('.fcc_carousel .featureImage:first-child').addClass('active').fadeIn();
jQuery('.fcc_carousel .featureIndex li:first-child a').addClass('featureIndexOn');
if (numSlides > 1) {
// Start the timer for the carousel
featureTimer = setInterval("rotateFeature()", featureDelay);
// Set the click events for the dots
// If a dot is clicked, we're going to stop the timer
jQuery('.featureIndexDots a, .featureIndex a').each(function(i) {
jQuery(this).click(function(e) {
e.preventDefault();
clearInterval(featureTimer);
showFeature(i + 1);
});
});
} else {
jQuery('.featureIndexDots').hide();
}
});