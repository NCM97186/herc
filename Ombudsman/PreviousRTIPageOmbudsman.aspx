<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="PreviousRTIPageOmbudsman.aspx.cs" Inherits="Ombudsman_PreviousRTIPageOmbudsman" %>

<%@ Register Src="~/usercontrol/OmbudsmanLeftMenuFor_InternalPagesUserControl.ascx"
    TagName="OmbudsmanLeftMenuFor_InternalPagesUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" runat="Server">
    <div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"></asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphrightholder" runat="Server">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.RTIPrevious%></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <%--<img src="<%=ResolveUrl("~/images/print-icon.png")%>"
                                width="18" height="16" alt="Print" title="Print" />--%></div>
                        <div class="last-updated">
                            <%=Resources.HercResource.LastUpdated %>:<a href="javascript:void(0);">07-02-2013</a>
                           
                            </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <div class="RTI_link">
                  <div class="RTI_top">
                            <asp:LinkButton ID="lnkRTI" runat="server" OnClick="lnkRTI_click" CssClass="active_rti1">View Previous Years Applications</asp:LinkButton>
                    
                            <asp:LinkButton ID="lnkFAA" runat="server" OnClick="lnkFAA_click" CssClass="active_rti2">View Previous Years FAA Applications</asp:LinkButton>
                       </div>
                          <div class="RTI_bottom"> 
                            <asp:LinkButton ID="lnkSAA" runat="server" OnClick="lnkSAA_click" CssClass="active_rti2">View Previous Years SAA Applications</asp:LinkButton>
                   </div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server">
                        <%=Resources.HercResource.RTI %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <uc1:OmbudsmanLeftMenuFor_InternalPagesUserControl ID="LeftMenuFor_InternalPagesUserControl1"
                runat="server" />
        </div>
    </div>
</asp:Content>
