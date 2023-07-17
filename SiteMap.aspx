﻿<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="SiteMap.aspx.cs" Inherits="SiteMap" %>

<%@ Register Src="UserControl/LeftMenuFor_InternalPagesUserControl.ascx" TagName="LeftMenuFor_InternalPagesUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content_head" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%--<asp:Content ID="Content_banner" ContentPlaceHolderID="cphbanner" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content_breadcurm" ContentPlaceHolderID="cphbreadcrumholder" runat="Server">
 <div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"> </asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="content_leftpanel" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                     <%=Resources.HercResource.Sitemap %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <uc1:LeftMenuFor_InternalPagesUserControl ID="LeftMenuFor_InternalPagesUserControl1"
                runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="content_rightpanel" runat="server" ContentPlaceHolderID="cphrightholder">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                       <%=Resources.HercResource.Sitemap %></h2>
                    <div class="page-had-right-side">
                      <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
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

			  <div id="DRti" runat="server" class="RTI_link2">
                    <ul>
                        <li>
                            <asp:LinkButton ID="site" runat="server"  OnClick="site_Click"><%=Resources.HercResource.ViewOmbudsmanSitemap %></asp:LinkButton>
                        </li>
                    </ul>
                </div>

                <div class="sitemap-holder">
				<h3>
                        <strong> <%=Resources.HercResource.Toplinks %></strong></h3>
                    <asp:Literal ID="ltrlMenu" runat="server"> </asp:Literal>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <!--mid-holder-Close-->
    </div>
    <div class="clear">
    </div>
</asp:Content>
