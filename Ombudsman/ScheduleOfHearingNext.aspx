<%@ Page Title="" Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true" CodeFile="ScheduleOfHearingNext.aspx.cs" Inherits="Ombudsman_ScheduleOfHearingNext" %>

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
                        <%=Resources.HercResource.Nextyear %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                          
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <strong>
                                <%=Resources.HercResource.LastUpdated %>:</strong>
                            <%=lastUpdatedDate %>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
               <%-- <p id="pyear" runat="server" visible="false">
                    <label for="<%=drpyear.ClientID %>">
                        Select for Next Year Schedule of Hearings:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>--%>
                <br />
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <asp:GridView ID="gvSOH" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    DataKeyNames="Soh_ID" OnRowCommand="gvSOH_RowCommand" OnRowDataBound="gvSOH_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Date & Time" DataField="Date" />
                        <asp:BoundField HeaderText="Appeal No" DataField="PRO_No" />
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Applicant(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblApplicant" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Applicant_Name"),100)%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Respondent_Name"),100)%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Subject"),100)%>'
                                    CommandName="ViewDetails" CommandArgument='<%#Eval("Soh_ID")%>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarksSoh" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Venue" DataField="Venue" Visible="false" />
                    </Columns>
                </asp:GridView>
                <div class="clear">
                </div>
                <p>
                    <div style="float: right;" class="paging">
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                               <label for="<%=ddlPageSize.ClientID %>">
                            &nbsp;</label>
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
                        <%=Resources.HercResource.CLSH %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                   
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href='<%=ResolveUrl("~/Ombudsman/ScheduleOfHearing/260_1_Currentyear.aspx") %>'
                        title="Current Year">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href='<%=ResolveUrl("~/Ombudsman/ScheduleOfHearingPreviousYear/261_1_Previousyear.aspx") %>'
                        title="Previous Year">
                        <%=Resources.HercResource.PreviousYears %>
                    </a></li>

                     <li><a href='<%=ResolveUrl("~/Ombudsman/ScheduleOfHearingNextYear/1005_1_Nextyear.aspx") %>'
                        title="Next Year" class="current">
                        <%=Resources.HercResource.Nextyear %>
                    </a></li>

                    <li><a href='<%=ResolveUrl("~/Ombudsman/OmbudsmanSearchSOH.aspx") %>' title="Search">
                        <%=Resources.HercResource.Search%>
                    </a></li>
                    <% }

                       else
                       { %>
                    <li><a href='<%=ResolveUrl("~/OmbudsmanContent/Hindi/ScheduleOfHearing/298_1_Currentyear.aspx") %>'
                        title="Current Year">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href='<%=ResolveUrl("~/OmbudsmanContent/Hindi/ScheduleOfHearingPreviousYear/299_1_Previousyear.aspx") %>'
                        title="Previous Year">
                        <%=Resources.HercResource.PreviousYears %>
                    </a></li>

                     <li><a href='<%=ResolveUrl("~/OmbudsmanContent/Hindi/ScheduleOfHearingNextYear/1006_1_Nextyear.aspx") %>'
                        title="Previous Year" class="current">
                        <%=Resources.HercResource.Nextyear %>
                    </a></li>

                    <li><a href='<%=ResolveUrl("~/OmbudsmanContent/Hindi/OmbudsmanSearchSOH/300_1_Search.aspx") %>'
                        title="Search">
                        <%=Resources.HercResource.Search%>
                    </a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

