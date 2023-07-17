
var delayb4scroll=200 //Specify initial delay before marquee starts to scroll on page (2000=2 seconds)
var marqueespeed=1 //Specify marquee scroll speed (larger is faster 1-10)
var pauseit=1 //Pause marquee onMousever (0=no. 1=yes)?

////NO NEED TO EDIT BELOW THIS LINE////////////

var copyspeed=marqueespeed
var pausespeed=(pauseit==0)? copyspeed: 0
var actualheight=''

function scrollmarquee(){
	if (parseInt(cross_marquee.style.top)>(actualheight*(-1)+8))
	cross_marquee.style.top=parseInt(cross_marquee.style.top)-copyspeed+"px"
	else
	cross_marquee.style.top=parseInt(marqueeheight)+8+"px"
}

function initializemarquee(){
cross_marquee=document.getElementById("vmarquee")
cross_marquee.style.top=0
marqueeheight=document.getElementById("marqueecontainer").offsetHeight
actualheight=cross_marquee.offsetHeight
if (window.opera || navigator.userAgent.indexOf("Netscape/7")!=-1){ //if Opera or Netscape 7x, add scrollbars to scroll and exit
cross_marquee.style.height=marqueeheight+"px"
cross_marquee.style.overflow="scroll"
return
}
setTimeout('lefttime=setInterval("scrollmarquee()",35)', delayb4scroll)
}

if (window.addEventListener)
window.addEventListener("load", initializemarquee, false)
else if (window.attachEvent)
window.attachEvent("onload", initializemarquee)
else if (document.getElementById)
window.onload=initializemarquee

function playpause(speed){
	cross_marquee=document.getElementById("vmarquee");
	if(speed==1){
		document.getElementById("marqueecontainer").style.overflow="scroll";
		cross_marquee.style.top=0
		cross_marquee.style.overflow="scroll";
		return speed=0;
	}
	if(speed==0){
		document.getElementById("marqueecontainer").style.overflow="hidden";
		cross_marquee.style.overflow="hidden";
		cross_marquee.style.top=0
		return speed=1;
		cross_marquee.height=20
	}
}
