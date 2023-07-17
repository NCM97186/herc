<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true" CodeFile="RTI.aspx.cs" Inherits="RTI" %>

<%@ Register src="UserControl/RTI_Control.ascx" tagname="RTI_Control" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="contentbreadhum" runat="server" ContentPlaceHolderID="cphbreadcrumholder">
   <div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"> </asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="content_rightholder" runat="server" ContentPlaceHolderID="cphrightholder">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=headerName %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <img src="<%=ResolveUrl("~/images/print-icon.png") %>" width="18" height="16" alt="Print" title="Print" />
                        </div>
                        <div class="last-updated">
                        <%=Resources.HercResource.LastUpdated %>:
                    <%=lastUpdatedDate%>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
            <asp:Literal ID="ltrlMainContent" runat="server">
                </asp:Literal>
               
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
<asp:Content ID="Content_leftholder" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server" ></h2>
                        
                </div>
            </div>
            <div class="clear">
            </div>
           
             <uc1:RTI_Control ID="RTI_Control2" runat="server" />
        </div>
    </div>
   
  
</asp:Content>


