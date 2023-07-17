<%@ Page Title="" Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true" CodeFile="SearchOmbudsman.aspx.cs" Inherits="Ombudsman_SearchOmbudsman" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" Runat="Server">

<div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"> </asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="contentrightholder" runat="server" ContentPlaceHolderID="cphrightholder">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.Search %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--     <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>--%>
                        </div>
                        <div class="last-updated">
                            <%--   <b><%=Resources.HercResource.LastUpdated %>:</b><%=lastUpdatedDate %>--%>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <p>
                    <script type="text/javascript" src="http://www.google.com/coop/cse/brand?form=searchbox_014373149466545347614%3Adygl7dqicp4"></script>
                    <!-- 
		                In order to make the button's click event fire when the text box is highlighted, we have to
		                have a second text box be a part of the form.  Make it invisible so that the user never 
		                knows it's there.
                		
		                From what research I did, this appears to be an IE-specific problem.  Nevertheless, this 
		                was the only "solution" I found.
	                -->
                    <input name="dummyHidden" style="visibility: hidden; display: none;" />
                    <!-- Emulated Google CSE Search Box Ends -->
                    <!-- Google Search Result Snippet Begins -->
                    <div id="results_016057011625966138646:nsxdhmd1lxg"><%--results_013280925726808751639:bg7jtz0icqy--%>
                    </div>
                </p>
             <%--   <script type="text/javascript">
                    (function () {
                        var cx = '009726609744311564328:uyk8qxspemi';
                        var gcse = document.createElement('script');
                        gcse.type = 'text/javascript';
                        gcse.async = true;
                        gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') +
        '//www.google.com/cse/cse.js?cx=' + cx;
                        var s = document.getElementsByTagName('script')[0];
                        s.parentNode.insertBefore(gcse, s);
                    })();
                </script>--%>
            <%--       <script type="text/javascript">
                       var googleSearchIframeName = "results_009726609744311564328:uyk8qxspemi";
                       var googleSearchFormName = "searchbox_009726609744311564328:uyk8qxspemi";
                    var googleSearchFrameWidth = 600;
                    var googleSearchFrameborder = "0";
                    var googleSearchDomain = "www.google.com";
                    var googleSearchPath = "/cse";
                </script>--%>


                  <script type="text/javascript">
                      var googleSearchIframeName = "results_016057011625966138646:nsxdhmd1lxg";
                      var googleSearchFormName = "searchbox_016057011625966138646:nsxdhmd1lxg";
                      var googleSearchFrameWidth = 600;
                      var googleSearchFrameborder = "0";
                      var googleSearchDomain = "www.google.com";
                      var googleSearchPath = "/cse";
            </script>
                <script type="text/javascript" language="javascript" src="js/CSearch.js"></script>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <!--mid-holder-Close-->
    </div>
    <div class="clear">
    </div>
</asp:Content>

<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.Search %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <%--  <uc1:leftmenufor_internalpagesusercontrol ID="LeftMenuFor_InternalPagesUserControl1"
                runat="server" />--%>
        </div>
    </div>
</asp:Content>