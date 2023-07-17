<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="DiscussionPapers.aspx.cs" Inherits="DiscussionPapers" %>

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
                        <%=headerName %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                            
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <strong>
                                <%=Resources.HercResource.LastUpdated %>:</strong><%=lastUpdatedDate %>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <asp:Label ID="lblmsg" runat="server" Visible="false" CssClass="redtext"></asp:Label>
                <p id="pyear" runat="server" visible="false">
                    <label for="<%=drpyear.ClientID %>">
                        Select for old Draft/Discussion Paper:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <br />
                <asp:GridView ID="gvDDPapaers" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCommand="gvDDPapaers_RowCommand" OnRowDataBound="gvDDPapaers_RowDataBound"
                    DataKeyNames="Link_Id" Width="100%" GridLines="None" ToolTip='<%#Resources.HercResource.DDPapers %>' summary="This table show all records of Draft and Discussion papers">
                    <Columns>
                        <asp:BoundField HeaderText="S.No." DataField="RowNumber" ItemStyle-CssClass="Text-Center" />
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkTitle" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Name"),200)%>'
                                    CommandName="View" CommandArgument='<%#Eval("Link_Id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Description" DataField="SmallDesc" Visible="false" />
                        <asp:BoundField HeaderText="Start_Date" DataField="Start_Date" Visible="false" />
                        <asp:BoundField HeaderText="Last Date of Receiving Comments" DataField="LastDateOfReceivingComment"
                            ItemStyle-CssClass="Text-Center" />
                        <asp:TemplateField HeaderText="Date of Public Hearing">
                            <ItemTemplate>
                                <asp:Label ID="lblPublicHearingDate" runat="server" Text='<%#Eval("PublicHearingDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Venue">
                            <ItemTemplate>
                                <asp:Label ID="lblVenue" runat="server" Text='<%#Eval("Venu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" Visible="false">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="clear">
                </div>
                <br />
                <p style="float: right;" class="paging">
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                    <label for="<%=ddlPageSize.ClientID %>">
                        &nbsp;
                    </label>
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="20" Value="20" />
                        <asp:ListItem Text="30" Value="30" />
                    </asp:DropDownList>
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
                        <%=Resources.HercResource.DDPapers %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="DiscussionPapers.aspx" class="margin" id="current" runat="server">
                        <%=Resources.HercResource.CurrentDraftDisscussionPaper%></a></li>
                    <li><a href="~/DiscussionPapersPrevYear/1.aspx" class="margin" id="previous" runat="server">
                        <%=Resources.HercResource.OldDraftDisscussionPaper %>
                    </a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/DiscussionPapers.aspx" class="margin" id="currentHindi"
                        runat="server">
                        <%=Resources.HercResource.CurrentDraftDisscussionPaper%></a></li>
                    <li><a href="~/content/Hindi/DiscussionPapersPrevYear/1.aspx" class="margin" id="previousHindi"
                        runat="server">
                        <%=Resources.HercResource.OldDraftDisscussionPaper%>
                    </a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
