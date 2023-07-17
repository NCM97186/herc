<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="Locate.aspx.cs" Inherits="Locate" %>
<%@ Register Src="UserControl/LeftMenuFor_InternalPagesUserControl.ascx" TagName="LeftMenuFor_InternalPagesUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDY0kkJiTPVd2U7aTOAwhc9ySH6oHxOIYM&sensor=false"></script>

    <script type="text/javascript">
        function initialize() {
            var mapProp = {
                center: new google.maps.LatLng(30.692591,76.857271),
                zoom: 18,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("googleMap")
  , mapProp);

var marker = new google.maps.Marker
(
    {
        position: new google.maps.LatLng(30.692591,76.857271),
        map: map,
        title: 'HERC Click Me'
    }
)

var infowindow = new google.maps.InfoWindow({
        content: 'Location info:<br/>Country Name:<br/>LatLng:'
    })    }

        google.maps.event.addDomListener(window, 'load', initialize);
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" runat="Server">
   <div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"> </asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="contentrightholder" ContentPlaceHolderID="cphrightholder" runat="Server">
 <%--<body onload="initialize()" onunload="GUnload()" >--%>
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                          <%=Resources.HercResource.LocateUs %></h2>

						   <div class="page-had-right-side">
                    </div>
                </div>
				 <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
			
            <div class="text-holder">
               
                    <div style="width: 100%; height: 380px;">
                    <iframe title="Haryana Electricity Regulatory Commission" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3430.9155675199613!2d76.85501031552639!3d30.69265159461343!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x390f936598c4f42f%3A0xb84f8330d8ab00a0!2sHaryana+Electricity+Regulatory+Commission!5e0!3m2!1sen!2sin!4v1490265200867" width="740" height="450" frameborder="0" style="border:0" allowfullscreen></iframe>
                    </div>
               
            </div>
			 
        </div>

    </div>
	<%--</body>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                      <%=Resources.HercResource.ContactUs %>
                    </h2>
                </div>
                <div class="clear">
                </div>
                
                 <uc1:LeftMenuFor_InternalPagesUserControl ID="LeftMenuFor_InternalPagesUserControl1"
                runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
