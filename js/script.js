<!--
function makeItMove(containerId,userActionId,textItemId,i)
{
var width8 =  $('.featureImageArea').width(); 
$('.featureImage img').width(width8);

var hi8 =  $('.featureImageArea').height(); 
$('.featureImage img').height(316);

// Values used by the script.
// BEWARE : YOU WILL HAVE TO MODIFIE THESE VALUES IN THE HTML CODE AS WELL, SETTING THEM TO THEIR NUMERIC VALUES!
var startValue=-180;// The value the movement will start. Must be big enough to let some white space.
var endValue=190; // the value the movement will end. Must be big enough to let the whoile text disappear
var stopValue=210; // Must be greater than endValue;
//
var containerPositionFromTop=201;//201
var containerPositionFromLeft=45;//35
var containerWidth=100//290
var containerHeight=230;//178

//
if (document.getElementById)
    {

    if(!document.getElementById('userAction'))
        {
        //alert("No action");

        }
    else
        {
        document.getElementById(userActionId).style.visibility="visible";
        document.getElementById(userActionId).style.display="block";
        document.getElementById(userActionId).style.zIndex="15";
        document.getElementById(userActionId).style.position="relative";
        document.getElementById(userActionId).style.top="-3px";
        document.getElementById(userActionId).style.left="0";
        document.getElementById(userActionId).style.width="auto";
        document.getElementById(userActionId).style.height="auto";
		  }
    document.getElementById(containerId).style.zIndex="11";
    document.getElementById(containerId).style.position="relative";
	document.getElementById(containerId).style.width=containerPositionFromLeft+"%";
    document.getElementById(containerId).style.width=containerWidth+"%";
	document.getElementById(containerId).style.height=containerHeight+"px";
    document.getElementById(containerId).style.overflow="hidden";

    document.getElementById(textItemId).style.zIndex="11";
    document.getElementById(textItemId).style.position="relative";
    document.getElementById(textItemId).style.top=-i+"px";	
    }
if(i<endValue)
    {
    i++;

    timer=setTimeout("makeItMove('"+containerId+"','"+userActionId+"','"+textItemId+"',"+i+")",50);
    }
else
    if(i==endValue)
        {
        i=startValue;
        timer=setTimeout("makeItMove('"+containerId+"','"+userActionId+"','"+textItemId+"',"+i+")",100);
        }
     else
        {
        clearTimeout(timer);
        document.getElementById(textItemId).style.zIndex="11";
        document.getElementById(textItemId).style.position="relative";
        document.getElementById(textItemId).style.top=10+"px";
        document.getElementById(textItemId).style.left=0+"px";
        document.getElementById(containerId).style.overflow="scroll";
        }
}
	
	function makeItMove2(containerId,userActionId,textItemId,i)
{

// Values used by the script.
// BEWARE : YOU WILL HAVE TO MODIFIE THESE VALUES IN THE HTML CODE AS WELL, SETTING THEM TO THEIR NUMERIC VALUES!
var startValue=-180;// The value the movement will start. Must be big enough to let some white space.
var endValue=190; // the value the movement will end. Must be big enough to let the whoile text disappear
var stopValue=210; // Must be greater than endValue;
//
var containerPositionFromTop=201;//201
var containerPositionFromLeft=45;//35
var containerWidth=100//290
var containerHeight=230;//178

//
if (document.getElementById)
    {

    if(!document.getElementById('userAction2'))
        {
        //alert("No action");

        }
    else
        {
        document.getElementById(userActionId).style.visibility="visible";
        document.getElementById(userActionId).style.display="block";
        document.getElementById(userActionId).style.zIndex="15";
        document.getElementById(userActionId).style.position="relative";
        document.getElementById(userActionId).style.top="-3px";
        document.getElementById(userActionId).style.left="0";
        document.getElementById(userActionId).style.width="auto";
        document.getElementById(userActionId).style.height="auto";
		  }
    document.getElementById(containerId).style.zIndex="11";
    document.getElementById(containerId).style.position="relative";
	document.getElementById(containerId).style.width=containerPositionFromLeft+"%";
    document.getElementById(containerId).style.width=containerWidth+"%";
	document.getElementById(containerId).style.height=containerHeight+"px";
    document.getElementById(containerId).style.overflow="hidden";

    document.getElementById(textItemId).style.zIndex="11";
    document.getElementById(textItemId).style.position="relative";
    document.getElementById(textItemId).style.top=-i+"px";	
    }
if(i<endValue)
    {
    i++;

    timer=setTimeout("makeItMove2('"+containerId+"','"+userActionId+"','"+textItemId+"',"+i+")",50);
    }
else
    if(i==endValue)
        {
        i=startValue;
        timer=setTimeout("makeItMove2('"+containerId+"','"+userActionId+"','"+textItemId+"',"+i+")",100);
        }
     else
        {
        clearTimeout(timer);
        document.getElementById(textItemId).style.zIndex="11";
        document.getElementById(textItemId).style.position="relative";
        document.getElementById(textItemId).style.top=10+"px";
        document.getElementById(textItemId).style.left=0+"px";
        document.getElementById(containerId).style.overflow="scroll";
        }
}
	
	function makeItMove3(containerId,userActionId,textItemId,i)
{

// Values used by the script.
// BEWARE : YOU WILL HAVE TO MODIFIE THESE VALUES IN THE HTML CODE AS WELL, SETTING THEM TO THEIR NUMERIC VALUES!
var startValue=-180;// The value the movement will start. Must be big enough to let some white space.
var endValue=190; // the value the movement will end. Must be big enough to let the whoile text disappear
var stopValue=210; // Must be greater than endValue;
//
var containerPositionFromTop=201;//201
var containerPositionFromLeft=45;//35
var containerWidth=100//290
var containerHeight=230;//178

//
if (document.getElementById)
    {

    if(!document.getElementById('userAction3'))
        {
        //alert("No action");

        }
    else
        {
        document.getElementById(userActionId).style.visibility="visible";
        document.getElementById(userActionId).style.display="block";
        document.getElementById(userActionId).style.zIndex="15";
        document.getElementById(userActionId).style.position="relative";
        document.getElementById(userActionId).style.top="-3px";
        document.getElementById(userActionId).style.left="0";
        document.getElementById(userActionId).style.width="auto";
        document.getElementById(userActionId).style.height="auto";
		  }
    document.getElementById(containerId).style.zIndex="11";
    document.getElementById(containerId).style.position="relative";
	document.getElementById(containerId).style.width=containerPositionFromLeft+"%";
    document.getElementById(containerId).style.width=containerWidth+"%";
	document.getElementById(containerId).style.height=containerHeight+"px";
    document.getElementById(containerId).style.overflow="hidden";

    document.getElementById(textItemId).style.zIndex="11";
    document.getElementById(textItemId).style.position="relative";
    document.getElementById(textItemId).style.top=-i+"px";	
    }
if(i<endValue)
    {
    i++;

    timer=setTimeout("makeItMove3('"+containerId+"','"+userActionId+"','"+textItemId+"',"+i+")",50);
    }
else
    if(i==endValue)
        {
        i=startValue;
        timer=setTimeout("makeItMove3('"+containerId+"','"+userActionId+"','"+textItemId+"',"+i+")",100);
        }
     else
        {
        clearTimeout(timer);
        document.getElementById(textItemId).style.zIndex="11";
        document.getElementById(textItemId).style.position="relative";
        document.getElementById(textItemId).style.top=10+"px";
        document.getElementById(textItemId).style.left=0+"px";
        document.getElementById(containerId).style.overflow="scroll";
        }
}
	
	
//-->	