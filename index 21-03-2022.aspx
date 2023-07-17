	<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register Src="~/UserControl/BannerControl.ascx" TagName="BannerImage" TagPrefix="ucBanner" %>
<%@ Register Src="UserControl/leftmenu_Homepage.ascx" TagName="leftmenu_Homepage"
    TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/homepage_logo.ascx" TagName="homepage_logo" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/animateA.css")%>" />
<script type="text/javascript" src="<%= ResolveUrl("~/js/animatedcollapse.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/left-nav-01.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/banner-03.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/jquery-3.2.1.min.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/jquery-migrate-1.0.0.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/jquery.totemticker.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/featureContent.js")%>"></script>
<link href="<%= ResolveUrl("~/css/demo.css")%>" rel="stylesheet" />
<script type="text/javascript" src="<%= ResolveUrl("~/js/jquery-3.2.1.min.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/jquery.scrollbox.js")%>"></script>
<link href="<%= ResolveUrl("~/css/modern-ticker.css")%>" type="text/css" rel="stylesheet" />
<script src="<%= ResolveUrl("~/js/jquery.js")%>" type="text/javascript"> </script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/spotlight_slideshow.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/jquery.anythingslider.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/js/scroll.js")%>"></script> 
   
<script type="text/javascript" src="<%= ResolveUrl("~/js/bootstrap.min.js")%>"></script>
<%-- <script type="text/javascript" src="<%= ResolveUrl("~/js/custom-slider.js")%>"></script>--%>
<script type="text/javascript">
<!--        //--><![CDATA[//><!--
        var lang = "en"
        //--><!]]>
    
</script>
<style type="text/css">

.closebtn {
margin-left: 20px;
color: black;
font-weight: bold;
float: right;
font-size: 16px;
cursor: pointer;
margin-right: -3px;
transition: 0.3s;
margin-top: -5px;
	}
</style>
<script type="text/javascript" src="<%= ResolveUrl("~/jsA/noty/packaged/jquery.noty.packaged.js")%>"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/notification_htmlA.js")%>"></script>
<link href="<%= ResolveUrl("~/css/lightslider.css")%>" rel="stylesheet" />
<script type="text/javascript" src="<%= ResolveUrl("~/js/lightslider.js")%>"></script>

<script type="text/javascript">
function generate(type, text) {
var n = noty({
text        : text,
type        : type,
dismissQueue: true,
layout      : 'bottomLeft',
closeWith   : ['click'],	
theme       : 'relax',
maxVisible  : 10,
animation   : {	
open  : 'animated bounceInUp',
close : 'animated bounceOutDown',
easing: 'swing',
speed : 5000
					}
				});
console.log('html: ' + n.options.id);
			}
function generateAll() {
generate('information', notification_html[0]);
				
			}

$(document).ready(function () {

setTimeout(function() {
					generateAll();
				}, 500);

			});

		</script>
	
<script type="text/javascript">
$(document).ready(function(){
    // Activate Carousel
    $("#myCarousel").carousel();
    
    // Enable Carousel Indicators
    $(".item1").click(function(){
        $("#myCarousel").carousel(0);
    });
    $(".item2").click(function(){
        $("#myCarousel").carousel(1);
    });
    $(".item3").click(function(){
        $("#myCarousel").carousel(2);
    });
    $(".item4").click(function(){
        $("#myCarousel").carousel(3);
    });
	$(".item5").click(function(){
        $("#myCarousel").carousel(4);
    });
    
    // Enable Carousel Controls
    $(".left").click(function(){
        $("#myCarousel").carousel("prev");
    });
    $(".right").click(function(){
        $("#myCarousel").carousel("next");
    });
});
$(function () {
    $('#myCarousel').carousel({
        interval:2000,
        pause: "false"
    });
    $('#playButton').click(function () {
        $('#myCarousel').carousel('cycle');
    });
    $('#pauseButton').click(function () {
        $('#myCarousel').carousel('pause');
    });
});
</script>

</asp:Content>
<asp:Content ID="con_content" runat="server" ContentPlaceHolderID="cphcontent">
<div><a href="https://www.onlinesbi.com/sbicollect/icollecthome.htm?corpID=1952558" target="blank"><img src="/images/covidHrY.png" style="width: 100%;height:300px;"/></a>

</div>


<a style="margin-left: 329px;font-size: 20px;color:blue !important;" href="/WriteReadData/UserFiles/file/AarogyaSetuIVRS.pdf" target="blank">Aarogya Setu IVRS - Call 1921</a>

<div class="mid-holder">
<div class="section-1">
<div class="left-nav-holder">
<div class="left-nav-holder-top-corner">
</div>
<div class="sidebarmenu">
<ul id="sidebarmenu1">
<uc2:leftmenu_Homepage ID="leftmenu_Homepage1" runat="server" />
</ul>
</div>
<div class="clear">
</div>
<div class="left-nav-holder-bottom-corner">
</div>
</div>
<div class="banner-holder">
<div id="myCarousel" class="carousel slide axs">
<!-- Indicators -->
<ol class="carousel-indicators">
<li class="item1 active"></li>
<li class="item2"></li>
<li class="item3"></li>
<li class="item4"></li>
<li class="item5"></li>
</ol>
<div class="carousel-inner">
<ucBanner:BannerImage ID="bannerImage" runat="server" />
</div>
           <!-- <div class="carousel-inner">
            <div class="item active">
            <img src="images/banner-img.png" alt="">
            </div>            
            <div class="item">
            <img src="images/banner-img.png" alt="">
            </div>            
            <div class="item">
            <img src="images/banner-img.png" alt="">
            </div>            
            <div class="item">
            <img src="images/banner-img.png" alt="">
            </div>
            </div>-->
            
            <!-- Left and right controls -->
			<!-- Left and right controls -->
      <a class="left carousel-control axs" href="#">
      <span class="glyphicon glyphicon-chevron-left">
	  <img src="<%= ResolveUrl("~/images/arrow-left-direction.png")%>" alt="" />
	  </span>
      </a>
      <a class="right carousel-control axs" href="#">
      <span class="glyphicon glyphicon-chevron-right">
	  <img src="<%= ResolveUrl("~/images/arrow-right-direction.png")%>" alt="" /></span>
      </a>
	 <div class="carouselButtons">
     <button id="playButton" type="button" class="btn1 btn-default btn-xs">
     <span class="glyphicon glyphicon-play"></span>
     </button>
     <button id="pauseButton" type="button" class="btn1 btn-default btn-xs">
     <span class="glyphicon glyphicon-pause"></span>
     </button>
     </div>
     </div>
     <div class="clear">
     </div>
     </div>
        </div>
        <div class="clear">
        </div>
        <div class="section-2">
        <div class="ombusdsman-holder">
        <div class="ombusdsman-mid">
        <div class="ombusdsman-col-left">
        <div class="ombusdsman-link">
        <a class="ombusdsman-icon" href="Ombudsman/Ombudsman.aspx" title=" <%=Resources.HercResource.Ombudsman%>"
		target="_blank">
        <%=Resources.HercResource.Ombudsman%>
        </a>
        </div>
        <div class="licesens-link">
        <asp:Literal ID="ltrLicensees" runat="server"></asp:Literal>
        </div>
        </div>
        <div class="ombusdsman-div-line">
        </div>
        <div class="ombusdsman-col-right">
        <div class="state-advisory-link">
        <asp:Literal ID="ltrStatelink" runat="server"></asp:Literal>
        </div>
        <div class="ordination-link">
        <asp:Literal ID="ltrlOrdination" runat="server"></asp:Literal>
        </div>
        </div>
        </div>
        </div>
        <div class="ombusdsman-logo-right">
        <div id="slider">
        <div class="rightSlider">
        <div class="arrows">
		<a href='javascript:void(0);' tabindex="0">
		<img alt="Left" id="prev" src= "<%= ResolveUrl("~/App_Themes/Blue/images/prev-icon.png")%>" title="Move Left"/>
		</a>
		<a href='javascript:void(0);' tabindex="0" >
	    <img alt="Right" id="next" src="<%= ResolveUrl("~/App_Themes/Blue/images/next-icon.png")%>" title="Move Right"/>
		</a>
        <!--<asp:Literal ID="LtrTheme" runat="server"></asp:Literal>-->
        </div>
        <div class="slidercontent" id="slider_box">
		
		<ul id="content-slider" class="content-slider">
              <uc2:homepage_logo ID="homepagelog01" runat="server" />   
				</ul>
       
       </div>
       </div>
       </div>
       </div>
        </div>
        <div class="clear">
        </div>
        <div class="section-3">
            <div class="top-round-holder">
            </div>
            <div class="section-3-content">
                <div class="order-holder">
                    <div class="order-had">
                        <h2>
                            <%=Resources.HercResource.Orders %></h2>
                    </div>
                    <div class="order-list">
                        <ul>
                            <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                               { %>
                            <li><a href='<%=ResolveUrl("~/CurrentYearOrders.aspx") %>' title='<%=Resources.HercResource.CurrentYearOrder %>'>
                                <%=Resources.HercResource.CurrentYearOrder %></a></li>
                            <li><a href='<%=ResolveUrl("~/CurrentYearOrders/1.aspx") %>' title='<%=Resources.HercResource.PreviousYearOrder %>'>
                                <%=Resources.HercResource.PreviousYearOrder %></a></li>
                            <li><a href='<%=ResolveUrl("~/OrderUnderAppeal.aspx") %>' title='<%=Resources.HercResource.OrdersunderAppeal %>'>
                                <%=Resources.HercResource.OrdersunderAppeal %></a></li>
                            <li><a href='<%=ResolveUrl("~/CurrentOrderSearch.aspx") %>' title=' <%=Resources.HercResource.OrdersSearch %>'>
                                <%=Resources.HercResource.OrdersSearch %></a></li>
                            <li><a href='<%=ResolveUrl("~/CategorywiseOrders.aspx") %>' title='<%=Resources.HercResource.CategorywiseOrders %>'>
                                <%=Resources.HercResource.CategorywiseOrders %></a></li>
                            <% }
                               else
                               { %>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentYearOrders.aspx") %>' title='<%=Resources.HercResource.CurrentYearOrder %>'>
                                <%=Resources.HercResource.CurrentYearOrder %></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentYearOrders/1.aspx") %>' title='<%=Resources.HercResource.PreviousYearOrder %>'>
                                <%=Resources.HercResource.PreviousYearOrder %></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/OrderUnderAppeal.aspx") %>' title='<%=Resources.HercResource.OrdersunderAppeal %>'>
                                <%=Resources.HercResource.OrdersunderAppeal %></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentOrderSearch.aspx") %>' title=' <%=Resources.HercResource.OrdersSearch %>'>
                                <%=Resources.HercResource.OrdersSearch %></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/CategorywiseOrders.aspx") %>' title='<%=Resources.HercResource.CategorywiseOrders %>'>
                                <%=Resources.HercResource.CategorywiseOrders %></a></li>
                            <%} %>
                        </ul>
                    </div>
                </div>
                <div class="petitions-holder">
                <div class="petitions-had">
                 <h2>
                 <%=Resources.HercResource.Petitions %></h2>
                    </div>
                    <div class="petitions-list">
                        <ul>
                            <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                               { %>
                            <li><a href='<%=ResolveUrl("petition.aspx") %>' title='<%=Resources.HercResource.CurrentPetition%>'>
                                <%=Resources.HercResource.CurrentPetition%></a></li>
                            <li><a href='<%=ResolveUrl("~/petition/1.aspx") %>' title='<%=Resources.HercResource.PreviousYearPetitions %>'>
                                <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                            <li><a href='<%=ResolveUrl("PetitionSearch.aspx") %>' title='<%=Resources.HercResource.PetitionsSearch %>'>
                                <%=Resources.HercResource.PetitionsSearch %></a></li>
                            <li><a href='<%=ResolveUrl("OnlineStatus.aspx") %>' title='<%=Resources.HercResource.OnlineStatus %>'>
                                <%=Resources.HercResource.OnlineStatus %></a></li>
                            <% }

                               else
                               { %>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/petition.aspx") %>' title='<%=Resources.HercResource.CurrentPetition%>'>
                                <%=Resources.HercResource.CurrentPetition%></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/petition/1.aspx") %>' title='<%=Resources.HercResource.PreviousYearPetitions %>'>
                                <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/PetitionSearch.aspx") %>' title='<%=Resources.HercResource.PetitionsSearch %>'>
                                <%=Resources.HercResource.PetitionsSearch %></a></li>
                            <li><a href='<%=ResolveUrl("~/content/Hindi/OnlineStatus.aspx") %>' title='<%=Resources.HercResource.OnlineStatus %>'>
                                <%=Resources.HercResource.OnlineStatus %></a></li>
                            <%} %>
                        </ul>
                    </div>
                </div>
                <div class="tab-holder">
                <div class="dhtmlgoodies_question">
                <div class="title-head">
                <span class="what-new-icon" title="<%=Resources.HercResource.WhatNew %>">
                <%=Resources.HercResource.WhatNew %></span></div>
                </div>
                <div class="dhtmlgoodies_answer">
                <div class="dhtmlgoodies_answer-p">
                <div class="demo2 scroll-text">
                <ul>
                <asp:Repeater ID="rptPetitonNew" runat="server" OnItemCommand="rptPetitonNew_ItemCommand"
                OnItemDataBound="rptPetitonNew_ItemDataBound">
                <ItemTemplate>
				<li>
                <asp:Label ID="lblmsgPetition" runat="server" Text="Final Order Dated :"></asp:Label>
                                                <%#DataBinder.Eval(Container.DataItem, "OrderDate")%>,
                                                <asp:LinkButton ID="lnkDetailsPetition" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                                    CommandName="view" Text='<%#Eval("OrderTitle")%>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                                </asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="rptInterimOrder" runat="server" OnItemDataBound="rptInterimOrder_ItemDataBound"
                                        OnItemCommand="rptInterimOrder_ItemCommand">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lblmsgOrders" runat="server" Text="Interim Order Dated :"></asp:Label>
                                                <%#DataBinder.Eval(Container.DataItem, "OrderDate")%>,
                                                <asp:LinkButton ID="lnkDetailsOrder" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                                    CommandName="viewOrder" Text='<%#Eval("OrderTitle") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                                </asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RptrRegulation" runat="server" OnItemDataBound="RptrRegulation_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <asp:HyperLink ID="hypFile" runat="server" Text='<%# Eval("Name")%>' NavigateUrl='<%# "~/"+"WriteReadData/Pdf/" + Eval("File_Name") %>'
                                                    Target="_blank"></asp:HyperLink>
                                                <asp:HiddenField ID="hidFile" runat="server" Value='<%# Eval("File_Name") %>' />
                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <span><a href="WhatsNew.aspx" target="_blank" title='<%=Resources.HercResource.ReadMore %>'>
                                <%=Resources.HercResource.More %>...</a></span>
                            <div class="clear">
                            </div>
                            <div class="buttons">
                                <a href="javascript:void(0);" title="Play" class="play_b play">
                                    <img width="13" height="15" style="margin-top: 10px;" alt="play" src='<%=ResolveUrl("~/images/icon-play.png") %>'
                                        title="Click To Play" />
                                </a><a href="javascript:void(0);" title="Stop" class="stop_b stop">
                                    <img width="13" height="15" alt="stop" src='<%=ResolveUrl("~/images/icon-stop.png") %>'
                                        title="Click To Stop" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="dhtmlgoodies_question">
                    <div class="title-head">
                    <span class="power-icon" title="<%=Resources.HercResource.PowerSectorLinks %>">
                    <%=Resources.HercResource.PowerSectorLinks %></span></div>
                    </div>
                    <div class="dhtmlgoodies_answer">
                    <div class="dhtmlgoodies_answer-p">
                    <div class="demo2 scroll-text">
                    <asp:Literal ID="ltrlPublicSectorLinks" runat="server"></asp:Literal>
                    </div>
                    <span>
                    <asp:LinkButton ID="lnkPublicSectorLinks" runat="server" OnClick="lnkPublicSectorLinks_Click"
                    OnClientClick="window.document.forms[0].target='_blank';" ToolTip='<%$Resources:HercResource,ReadMore %>'><%=Resources.HercResource.More %>...</asp:LinkButton></span>
                    <div class="clear">
                    </div>
                    <div class="buttons">
                    <a href="javascript:void(0);" title="Play" class="play_b play">
                    <img width="13" height="15" style="margin-top: 10px;" alt="play" title="Click To Play"
                    src='<%=ResolveUrl("~/images/icon-play.png") %>' />
                    </a><a href="javascript:void(0);" title="Stop" class="stop_b stop">
                    <img width="13" height="15" alt="stop" src='<%=ResolveUrl("~/images/icon-stop.png") %>'
                    title="Click To Stop" />
                    </a>
                    </div>
                    </div>
                    </div>
                    <div class="dhtmlgoodies_question">
                    <div class="title-head">
                    <span class="cause-list-icon" title="<%=Resources.HercResource.CLSH %>">
                   <%=Resources.HercResource.CLSH %></span></div>
                    </div>
                    <div class="dhtmlgoodies_answer">
                    <div class="dhtmlgoodies_answer-p">
                    <div class="demo2 scroll-text">
                     <ul>
                                    <asp:Repeater ID="rptrSOH" runat="server" OnItemCommand="rptrSOH_ItemCommand" OnItemDataBound="rptrSOH_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <%#DataBinder.Eval(Container.DataItem, "date")%>
                                                <asp:LinkButton ID="lnkDetailsSoh" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Soh_ID")%>'
                                                    CommandName="view" Text='<%#Eval("subject")%>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                                </asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <span><a href="SOH.aspx" target="_blank" title='<%=Resources.HercResource.ReadMore %>'>
                                <%=Resources.HercResource.More %>...</a></span>
                            <div class="clear">
                            </div>
                            <div class="buttons">
                                <a href="javascript:void(0);" title="Play" class="play_b play">
                                    <img width="13" height="15" style="margin-top: 10px;" alt="play" title="Click To Play"
                                        src='<%=ResolveUrl("~/images/icon-play.png") %>' />
                                </a><a href="javascript:void(0);" title="Stop" class="stop_b stop">
                                    <img width="13" height="15" alt="stop" src='<%=ResolveUrl("~/images/icon-stop.png") %>'
                                        title="Click To Stop" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="dhtmlgoodies_question">
                        <div class="title-head">
                            <span class="public-notice-icon" title="<%=Resources.HercResource.PublicNotice %>">
                                <%=Resources.HercResource.PublicNotice %></span></div>
                    </div>
                    <div class="dhtmlgoodies_answer">
                        <div class="dhtmlgoodies_answer-p">
                            <div class="demo2 scroll-text">
                                
                                    <asp:Repeater ID="rptpublicNotice" runat="server" OnItemCommand="rptpublicNotice_ItemCommand"
                                        OnItemDataBound="rptpublicNotice_ItemDataBound">
                                        <ItemTemplate><ul>
                                            <li>
                                                <asp:LinkButton ID="lnkTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>'
                                                    CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PublicNoticeID") %>'
                                                    ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                            </li></ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                
                            </div>
                            <div class="clear">
                            </div>
                            <span><a href="PublicNoticeDetails.aspx" target="_blank" title='<%=Resources.HercResource.ReadMore %>'>
                                <%=Resources.HercResource.More %>...</a></span>
                            <div class="buttons">
                                <a href="javascript:void(0);" title="Play" class="play_b play">
                                    <img width="13" height="15" style="margin-top: 10px;" alt="play" title="Click To Play"
                                        src='<%=ResolveUrl("~/images/icon-play.png") %>' />
                                </a><a href="javascript:void(0);" title="Stop" class="stop_b stop">
                                    <img width="13" height="15" alt="stop" src='<%=ResolveUrl("~/images/icon-stop.png") %>'
                                        title="Click To Stop" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="bottom-round-holder">
            </div>
        </div>
<script type="text/javascript">
$(function () {
$('.demo2').scrollbox({
linear: true,
step: 1,
delay: 0,
speed: 100
});
});

 $(document).ready(function() {
 $("#content-slider").lightSlider({
                loop:true,
				//auto:true,
                keyPress:true,
				slideMove:1
            });
			var slider = $('#content-slider').lightSlider({
           controls: false
          });
		  $('#prev').on('click', function () {
            slider.goToPrevSlide();
          });
			$('#next').on('click', function () {
				slider.goToNextSlide();
			});
			
		});			
			
			
</script>
<script type="text/javascript">initShowHideDivs(); </script>
 </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
