$(document).ready(function(){
	
	
		
		$("#main-nav li ul").hide();
		$("#main-nav li a.current").parent().find("ul").slideToggle("slow"); 
		
		$("#main-nav li a.nav-top-item").click( 
			function () {
				$(this).parent().siblings().find("ul").slideUp("normal"); 
				$(this).next().slideToggle("normal"); 
				return false;
			}
		);
		
		$("#main-nav li a.no-submenu").click( 
			function () {
				window.location.href=(this.href); 
				return false;
			}
		); 

  
		
		$("#main-nav li .nav-top-item").hover(
			function () {
				$(this).stop().animate({ paddingRight: "25px" }, 200);
			}, 
			function () {
				$(this).stop().animate({ paddingRight: "15px" });
			}
		);

  
		
		$(".content-box-header h3").css({ "cursor":"s-resize" }); 
		$(".closed-box .content-box-content").hide(); 
		$(".closed-box .content-box-tabs").hide(); 
		
		$(".content-box-header h3").click( 
			function () {
			  $(this).parent().next().toggle(); 
			  $(this).parent().parent().toggleClass("closed-box"); 
			  $(this).parent().find(".content-box-tabs").toggle(); 
			}
		);

 
		
		$('.content-box .content-box-content div.tab-content').hide(); 
		$('ul.content-box-tabs li a.default-tab').addClass('current'); 
		$('.content-box-content div.default-tab').show(); 
		
		$('.content-box ul.content-box-tabs li a').click( 
			function() { 
				$(this).parent().siblings().find("a").removeClass('current'); 
				$(this).addClass('current'); 
				var currentTab = $(this).attr('href'); 
				$(currentTab).siblings().hide(); 
				$(currentTab).show(); 
				return false; 
			}
		);

    
		
		$(".close").click(
			function () {
				$(this).parent().fadeTo(400, 0, function () { 
					$(this).slideUp(400);
				});
				return false;
			}
		);

   
		
		$('tbody tr:even').addClass("alt-row"); 

   
		
		$('.check-all').click(
			function(){
				$(this).parent().parent().parent().parent().find("input[type='checkbox']").attr('checked', $(this).is(':checked'));   
			}
		);

    
		
		//$('a[rel*=modal]').facebox(); 

    
		
	//	$(".wysiwyg").wysiwyg(); 

});
  
  
  