var featureItemActive = 1;
var featureTimer;
var featureLoop = 1;
var featureDelay = 5000;
var numSlides = 0;



function showFeature(featureNo)
{
	var previousImage = $('.fcc_carousel .active');
	$('.featureIndex li a').removeClass('featureIndexOn');
	$('.featureIndex' + featureNo + ' a').addClass('featureIndexOn');

	$('.featureIndexDots li').removeClass('featureIndexOn');
	$('.featureIndexDots' + featureNo).addClass('featureIndexOn');
	
	$(previousImage).fadeOut(function() {
		$(this).removeClass('active');
		$('.featureImage' + featureNo).addClass('active').fadeIn();
	});
	
}
 
function rotateFeature()
{
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
