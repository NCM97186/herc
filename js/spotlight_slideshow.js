$(function () {
	$('.slider1').anythingSlider({
	width : 649,          // Override the default CSS width
	easing: 'easeInOutExpo'
	});
	$("#slider_box").jCarouselLite({
          btnNext: "#next",
          btnPrev: "#prev",
          visible: 4,
          scroll: 1,
          circular: true,
          start: 0,
          auto:false
      });
});
	