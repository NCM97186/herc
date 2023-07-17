<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="CurrentDailyOrders.aspx.cs" Inherits="CurrentDailyOrders" %>

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
                            <a href="<%=UrlPrint%>?format=Print" title="Print" target="_blank">
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
                <asp:Label ID="lblmsg" runat="server" CssClass="redtext"></asp:Label>
                <p id="pyear" runat="server" visible="false">
                    <label for="<%=drpyear.ClientID %>">
                        Select for Previous Years Daily Orders:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <br />
                <asp:GridView ID="gvDailyOrders" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCommand="gvDailyOrders_RowCommand" OnRowDataBound="gvDailyOrders_RowDataBound"
                    DataKeyNames="OrderId" Width="100%" ToolTip="Current Year Daily Orders" summary="This table show all current year daily orders">
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataField="OrderDate" ItemStyle-CssClass="Text-Center" />
                        <asp:TemplateField HeaderText="Petition/RA Number" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblnumber" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-CssClass="Text-Center"
                            Visible="false" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Petitioner_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblsubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>'
                                    CommandName="viewDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderId") %>'
                                    ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                <asp:HiddenField ID="hydSubject" runat="server" Value='<%# Miscelleneous_DL.FixCharacters(Eval("OrderTitle"),100)%>'
                                    Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("OrderFile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
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
                        <label for="<%=ddlPageSize.ClientID %>">&nbsp;
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
                        <%=Resources.HercResource.DailyOrders %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="CurrentDailyOrders.aspx" title="Current Year" id="current" runat="server">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href="~/PrevYear/1.aspx" title="Previous Year" id="previous" runat="server">
                        <%=Resources.HercResource.PreviousYears%></a></li>
                    <li><a href="<%=ResolveUrl("~/SearchDailyOrders.aspx")%>" title='<%=Resources.HercResource.DailyOrdersSearch%>'>
                        <%=Resources.HercResource.DailyOrdersSearch%></a></li>
                    <%--<li><a href="~/CurrentDailyOrders/2.aspx" title="Repealed Orders" id="repealed" runat="server">
                        <%=Resources.HercResource.RepealedDailyOrders%></a></li>--%>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/CurrentDailyOrders.aspx" title="Current Year" id="currentHindi"
                        runat="server">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href="~/content/Hindi//PrevYear/1.aspx" title="Previous Year" id="previousHindi"
                        runat="server">
                        <%=Resources.HercResource.PreviousYears%></a></li>
                    <li><a href="<%=ResolveUrl("~/content/Hindi/SearchDailyOrders.aspx")%>" title='<%=Resources.HercResource.DailyOrdersSearch%>'>
                        <%=Resources.HercResource.DailyOrdersSearch%></a></li>
                    <%--<li><a href="~/content/Hindi/CurrentDailyOrders/2.aspx" id="repealedHindi" runat="server">
                        <%=Resources.HercResource.RepealedDailyOrders%></a></li>--%>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
