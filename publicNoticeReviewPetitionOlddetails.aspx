<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true" CodeFile="publicNoticeReviewPetitionOlddetails.aspx.cs" Inherits="publicNoticeReviewPetitionOlddetails"%>

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
                        <%=Resources.HercResource.PreviousYears %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <b>Last Update:</b> <%=lastUpdatedDate %>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                 <div id="DRti" runat="server" class="RTI_link2">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lnkreview" runat="server" OnClick="lnkreview_Click">View Petition Public Notice</asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <br />
                <br />
                <asp:GridView ID="gvPubNotice" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPubNotice_RowCommand"
                   OnRowDataBound="gvPubNotice_RowDataBound" UseAccessibleHeader="true" Width="100%" DataKeyNames="PublicNoticeID" ToolTip="Public Notices" summary="This table show all public notices">
                    <Columns>
                    <asp:BoundField HeaderText="RA Number (HERC/RA)" DataField="RP_NO" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField HeaderText="Date" DataField="Date" />
                        <asp:BoundField HeaderText="Year" DataField="Start_Date" />
                         <asp:BoundField HeaderText="Petitioner(s)" DataField="Petitioner_Name" />
                        <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" />
                         <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkTitle" runat="server" Text='<%#Eval("Title") %>' CommandName="ViewDetails"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PublicNoticeID") %>'></asp:LinkButton>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Server.HtmlDecode((string)Eval("Description"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:BoundField HeaderText="Description" DataField="Description" />--%>
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("PublicNotice")%>'>
                                  
                                     <img src="images/pdf-icon.jpg" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>--%>
                                  <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("PublicNotice") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <p id="pyear" runat="server" visible="false">
                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tbody>
                            <tr>
                                <th colspan="12" class="align">
                                    <span>Year</span>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <asp:DataList ID="datalistYear" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="6"
                        RepeatDirection="Horizontal" Width="730px" OnItemCommand="datalistYear_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkYear" runat="server" Text='<%#bind("year") %>' CommandArgument='<%#bind("year") %>'
                                CommandName="View"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </p>
                <div class="clear">
                </div>
             <div style="float: right;" class="paging" >
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
                        <%=Resources.HercResource.PubliceNoticeOld%>
                    </h2>
                </div>
               
            </div>
             <div class="clear">
                </div>
             <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href='<%=ResolveUrl("~/PublicNoticeReviewPetitionDetails.aspx") %>' title="Current Year"  >
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href='<%=ResolveUrl("~/publicNoticeReviewPetitionOlddetails.aspx") %>' title="Previous Year" class="current">
                        <%=Resources.HercResource.PreviousYears %>
                    </a></li>
                    <% }
                       else
                       { %>
                    <li><a href='<%=ResolveUrl("~/Content/Hindi/PublicNoticeReviewPetitionDetails.aspx") %>' >
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href='<%=ResolveUrl("~/publicNoticeReviewPetitionOlddetails.aspx") %>' title="Previous Year" class="current">
                        <%=Resources.HercResource.PreviousYears %>
                    </a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

