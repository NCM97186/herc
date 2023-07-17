<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="ProfileDetail.aspx.cs" Inherits="ProfileDetail" %>

<%@ Register Src="UserControl/LeftMenuFor_InternalPagesUserControl.ascx" TagName="LeftMenuFor_InternalPagesUserControl"
    TagPrefix="uc1" %>
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
<asp:Content ID="contentrightholder" ContentPlaceHolderID="cphrightholder" runat="Server">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.Profiledetails %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                              <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                     
                        </div>
                        <div id="DlastUpdate" runat="server"  class="last-updated">
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
            <div class="text-holder descripton">
                <div class="img_box" id="imgDiv" runat="server">
                    <asp:Image ID="imag" runat="server"  />
                </div>
                <h3>
                    <asp:Label ID="lblName" runat="server"></asp:Label></h3>
                <h4>
                    <asp:Label ID="lbldesignation" runat="server"></asp:Label></h4>
                <p>
                    <asp:Literal ID="lrtProfile" runat="server" Text='<%# Eval("Details") %>'>             
                    </asp:Literal></p>
                <p style="text-align: right;">
                    <asp:LinkButton ID="Lnkback" runat="server"  OnClick="Lnkback_Click"></asp:LinkButton></p>
                <%--<asp:GridView ID="grid" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Phone" HeaderText="Phone No." />
                        <asp:BoundField DataField="Epabx_Ext" HeaderText="EPABX Ext" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Details" HeaderText="OtherInformation" />
                    </Columns>
                </asp:GridView>--%>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.Aboutus%>
                    </h2>
                </div>
                <div class="clear">
                </div>
                <uc1:LeftMenuFor_InternalPagesUserControl ID="LeftMenuFor_InternalPagesUserControl2"
                    runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
