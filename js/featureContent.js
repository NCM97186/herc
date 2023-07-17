var featureItemActive = 1;
var featureTimer;
var featureLoop = 1;
var featureDelay = 3000;
var numSlides = 0;
var play = true; 
var width8 =  $('.featureImageArea').width(); 
$('.featureImage img').width(width8);
function showFeature(featureNo)

{
     
	if( play == true) {
	var width8 =  $('.featureImageArea').width(); 
$('.featureImage img').width(width8);


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
		$('a#stop_slide').click(function(){
				play = false; 


	  });
			$('a#play_slide').click(function(){
				play = true; 

	  });
	
	
}


 
function rotateFeature()
{
	// Increase the active item index
	featureItemActive++;
//	alert (play);

	// Increase the number of times we've looped in case we want to know that in the future
	// Reset the active item to 1 if we've reached the last slide
	if (featureItemActive > numSlides  ) {
		//				alert (play);
		featureLoop++;
		featureItemActive = 1;
	}
	// Show the active feature
	showFeature(featureItemActive);
}

function stopslider()
{
    play = false; 
	
}

