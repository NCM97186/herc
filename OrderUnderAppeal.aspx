<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="OrderUnderAppeal.aspx.cs" Inherits="OrderUnderAppeal" %>

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
                        <%=Resources.HercResource.OrdersunderAppeal %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
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
                <p id="pyear" runat="server" visible="false">
                    <label for="<%=drpyear.ClientID %>">
                        Select Year:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <asp:GridView ID="grdAppeal" runat="server" AutoGenerateColumns="false" OnRowCommand="grdAppeal_RowCommand"
                    OnRowDataBound="grdAppeal_RowDataBound" Width="100%" DataKeyNames="PA_Id" ToolTip='<%#Resources.HercResource.OrdersunderAppeal %>' summary="This table show all the petition appeal records">
                    <Columns>
                        <asp:TemplateField HeaderText="Order Under Appeal" >
                            <ItemTemplate>
                                <asp:Label ID="LnkconnectedorderAppeal" runat="server" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PA_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Petition No" DataField="pronumber" Visible="false" />
                        <asp:TemplateField HeaderText="Appeal Subject">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Where Appealed" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDetails" runat="server" Text='<%#Server.HtmlDecode((string)Eval("Where_Appealed")) %>'
                                    CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PA_Id") %>'
                                    ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date" DataField="AppealDate" />
                        <asp:BoundField HeaderText="Status" DataField="PA_Status" />
                        <asp:TemplateField HeaderText="Judgement Link" Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="ImageButton1" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                                    Text='<%#Eval("Judgement_Link") %>' Target="_blank" OnClientClick='<%# String.Format("javascript:return openTargetURL(\"{0}\")", Eval("Judgement_Link")) %>'
                                    AlternateText="Read online" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblViewDocAppeal" runat="server" CommandName="ViewDocAppeal"
                                    CommandArgument='<%#Eval("File_Name")%>'>
                                  
                                     <img src="images/pdf-icon.jpg" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFileAppeal" runat="server" Value='<%#Eval("File_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Remarks"),100) %>' />
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
                        <label for="<%=ddlPageSize.ClientID %>">
                            &nbsp;</label>
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
                        <%=Resources.HercResource.Orders %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href='<%=ResolveUrl("~/CurrentYearOrders.aspx") %>' title="Current Year Order">
                        <%=Resources.HercResource.CurrentYearOrder %></a></li>
                    <li><a href='<%=ResolveUrl("~/CurrentYearOrders/1.aspx") %>' title="Previous Year Order">
                        <%=Resources.HercResource.PreviousYearOrder %></a></li>
                    <li><a href='<%=ResolveUrl("~/OrderUnderAppeal.aspx") %>' title="Orders under Appeal"
                        class="current">
                        <%=Resources.HercResource.OrdersunderAppeal %></a></li>
                    <li><a href='<%=ResolveUrl("~/CurrentOrderSearch.aspx") %>' title="Orders Search">
                        <%=Resources.HercResource.OrdersSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/CategorywiseOrders.aspx") %>' title="Category wise Orders">
                        <%=Resources.HercResource.CategorywiseOrders %></a></li>
                    <% }
                       else
                       { %>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentYearOrders.aspx") %>' title="Current Year Order">
                        <%=Resources.HercResource.CurrentYearOrder %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentYearOrders/1.aspx") %>' title="Previous Year Order">
                        <%=Resources.HercResource.PreviousYearOrder %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/OrderUnderAppeal.aspx") %>' title="Orders under Appeal"
                        class="current">
                        <%=Resources.HercResource.OrdersunderAppeal %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentOrderSearch.aspx") %>' title="Orders Search">
                        <%=Resources.HercResource.OrdersSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/CategorywiseOrders.aspx") %>' title="Category wise Orders">
                        <%=Resources.HercResource.CategorywiseOrders %></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
