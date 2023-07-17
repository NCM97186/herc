<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="RtiPages.aspx.cs" Inherits="RtiPages" %>

<%@ Register Src="UserControl/RTI_Control.ascx" TagName="RTI_Control" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="contentrightholder" runat="server" ContentPlaceHolderID="cphrightholder">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.RTICurrent %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                           
                        </div>
                        <div class="last-updated">
                            <b>Last Update:</b> <a href="javascript:void(0);">07-02-2013</a>
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
                        <asp:LinkButton ID="lnkRTI" runat="server" OnClick="lnkRTI_click" CssClass="active_rti1">View Current Year Applications</asp:LinkButton>
                        <asp:LinkButton ID="lnkFAA" runat="server" OnClick="lnkFAA_click" CssClass="active_rti2">View Current Year FAA Applications</asp:LinkButton>
                    </div>
                    <div class="RTI_bottom">
                        <asp:LinkButton ID="lnkSAA" runat="server" OnClick="lnkSAA_click" CssClass="active_rti2">View Current Year SAA Applications</asp:LinkButton>
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
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.RTI %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <uc1:RTI_Control ID="RTI_Control2" runat="server" />
        </div>
    </div>
</asp:Content>
