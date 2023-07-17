<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="WhatsNew.aspx.cs" Inherits="WhatsNew" %>

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
                        <%=Resources.HercResource.WhatNew %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                           
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <%--<b>
                                <%=Resources.HercResource.LastUpdated %>:</b><%=lastUpdatedDate %>--%>
                                
                                </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>



           <div class="text-holder">
          

                <br />
                <h3 id="hPetition" runat="server">
                </h3>
                <asp:Repeater ID="rptPetitonNew" runat="server" OnItemCommand="rptPetitonNew_ItemCommand" OnItemDataBound="rptPetitonNew_ItemDataBound">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem, "Petition_Date")%>

                                  <asp:LinkButton ID="lnkDetailsPetition" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id")%>'
                                    CommandName="view" Text='<%#Eval("Subject") %>' Visible="false" ToolTip="View details">
                                </asp:LinkButton>
                               <asp:Label ID="lblmsgPetition" runat="server" Text="- Petition number:"></asp:Label>
                              
                                <asp:LinkButton ID="lnkLatestUpdatePetition" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id")%>'
                                    CommandName="view" Text='<%# DataBinder.Eval(Container.DataItem, "PRO_No")%>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>

                <h3 id="hReviewPetition" runat="server">
                </h3>
                <asp:Repeater ID="rptReviewPetition" runat="server" OnItemCommand="rptReviewPetition_ItemCommand" OnItemDataBound="rptReviewPetition_ItemDataBound">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem, "ReviewpetitionDate")%>

                                  <asp:LinkButton ID="lnkDetailsRwPetition" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RP_Id")%>'
                                    CommandName="view" Text='<%#Eval("Subject") %>' Visible="false" ToolTip="View details">
                                </asp:LinkButton>
                               <asp:Label ID="lblmsgPetition" runat="server" Text="- Review Petition number:"></asp:Label>
                              
                                <asp:LinkButton ID="lnkLatestUpdateRwPetition" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RP_Id")%>'
                                    CommandName="viewReview" Text='<%# DataBinder.Eval(Container.DataItem, "RP_No")%>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>



                <h3 id="hPublic" runat="server">
                </h3>
                <asp:Repeater ID="rptrPublic" runat="server" OnItemCommand="rptrPublic_ItemCommand"  OnItemDataBound="rptrPublic_ItemDataBound">
                    <ItemTemplate>
                        <ul>
                            <li>
                               <%-- <%#DataBinder.Eval(Container.DataItem, "EndDate")%>---%>
                                 <asp:LinkButton ID="lnkDetails" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PublicNoticeID")%>'
                                    CommandName="viewPublic" Text='<%#Eval("Title") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'  >
                                </asp:LinkButton>
                            
                              
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                  
                  <h3 id="hSOH" runat="server">
                </h3>
                <asp:Repeater ID="rptrSOH" runat="server"  OnItemDataBound="rptrSOH_ItemDataBound" OnItemCommand="rptrSOH_ItemCommand">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem, "Date")%>

                                  <asp:LinkButton ID="lnkDetailsSoh" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Soh_ID")%>'
                                    CommandName="viewSoh" Text='<%#Eval("Subject") %>' Visible="false" ToolTip="View details">
                                </asp:LinkButton>
                               <asp:Label ID="lblmsgSoh" runat="server" Text="- Schedule of Hearing PRO/RA number:"></asp:Label>
                               
                                
                                <asp:LinkButton ID="lnkLatestUpdateSoh" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Soh_ID")%>'
                                    CommandName="viewSoh" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' ><%--Text='<%# DataBinder.Eval(Container.DataItem, "PRO_No")%>'--%>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>

                  <h3 id="hOrders" runat="server">
                </h3>
                <asp:Repeater ID="rptrOrders" runat="server" OnItemDataBound="rptrOrders_ItemDataBound" OnItemCommand="rptrOrders_ItemCommand" >
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem, "OrderDate")%>
                                   <asp:LinkButton ID="lnkDetailsOrder" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                    CommandName="viewOrder" Text='<%#Eval("OrderTitle") %>' Visible="false" ToolTip="View details">
                                </asp:LinkButton>
                                   <asp:Label ID="lblmsgOrders" runat="server" Text="-Final Orders against PRO/RA number:"></asp:Label>
                              
                                <asp:LinkButton ID="lnkLatestUpdate" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                    CommandName="viewOrder" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' ><%--Text='<%# DataBinder.Eval(Container.DataItem, "pro_no")%>'--%>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>


                 <h3 id="hInterimOrder" runat="server">
                </h3>
                <asp:Repeater ID="rptInterimOrder" runat="server" OnItemDataBound="rptInterimOrder_ItemDataBound" OnItemCommand="rptInterimOrder_ItemCommand" >
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem, "OrderDate")%>
                                   <asp:LinkButton ID="lnkDetailsOrder" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                    CommandName="viewOrder" Text='<%#Eval("OrderTitle") %>' Visible="false" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                </asp:LinkButton>
                                   <asp:Label ID="lblmsgOrders" runat="server" Text="-Interim Orders against PRO/RA number:"></asp:Label>
                              
                                <asp:LinkButton ID="lnkLatestUpdate" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                    CommandName="viewOrder" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' ><%--Text='<%# DataBinder.Eval(Container.DataItem, "pro_no")%>'--%>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>



                 <h3 id="hDraftDiscussion" runat="server">
                </h3>
                <asp:Repeater ID="rptrDiscussion" runat="server"  OnItemCommand="rptrDiscussion_ItemCommand">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%--<%#DataBinder.Eval(Container.DataItem, "LastDateOfReceivingComment")%>- --%>
                                   <asp:LinkButton ID="lnkDetailsDraft" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Link_Id")%>'
                                    CommandName="viewDiscussion" Text='<%#Eval("Name") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'>
                                </asp:LinkButton>
                                 <%--  <asp:Label ID="lblmsgOrders" runat="server" Text="-Orders against PRO/RA number:"></asp:Label>--%>
                             <%--  <%#DataBinder.Eval(Container.DataItem, "Venu")%>--%>
                            
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>


                 <h3 id="hRegulation" runat="server"></h3>
           <asp:Repeater ID="RptrRegulation" runat="server" OnItemDataBound="RptrRegulation_ItemDataBound" >
                    <ItemTemplate>
                        <ul>
                            <li>
 
                             <asp:HyperLink ID="hypFile" runat="server" Text='<%# Server.HtmlDecode((string)Eval("Name"))%>'
                                        NavigateUrl='<%# "~/"+"WriteReadData/Pdf/" + Eval("File_Name") %>' Target="_blank" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:HyperLink>
                                    <asp:HiddenField ID="hidFile" runat="server" Value='<%# Eval("File_Name") %>' />

                                      <asp:Label ID="lblname" runat="server" Text='<%#Server.HtmlDecode((string)Eval("Name")) %>'></asp:Label>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>


                  <h3 id="News" runat="server"></h3>
           <asp:Repeater ID="gvWhatsNew" runat="server">
                <ItemTemplate>
                    <ul>
                        <li>
                            <asp:LinkButton ID="lnkDetailsPetition" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "News_ID")%>'
                                OnClick="lnkDetailsPetition_Click" Text='<%#Eval("News_TITLE") %>'>
                            </asp:LinkButton>
                           </li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
                <div class="clear">
                </div>
                <p id="PPaging" runat="server" visible="false">
                    <div style="float: right;" class="paging">
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
                    </div>
                </p>
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
                        <%=Resources.HercResource.WhatNew %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
