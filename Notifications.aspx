<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="Notifications.aspx.cs" Inherits="Notifications" Title="" ValidateRequest="false" %>

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
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.Notifications %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="~/Notifications/1.aspx" id="herc" runat="server">
                        <%=Resources.HercResource.herc %></a></li>
                    <li><a href="~/content/774_3_Others.aspx" id="other" runat="server">
                        <%=Resources.HercResource.Others %></a></li>
                    <li><a href="~/Notifications/8.aspx" id="repealed" runat="server">
                        <%=Resources.HercResource.RepealedNotifications%></a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/Notifications/1.aspx" id="hercHindi" runat="server">
                        <%=Resources.HercResource.herc %></a></li>
                    <li><a href="~/content/Hindi/776_3_Others.aspx" id="otherHindi" runat="server">
                        <%=Resources.HercResource.Others %></a></li>
                    <li><a href="~/content/Hindi/Notifications/8.aspx" id="repealedHindi" runat="server">
                        <%=Resources.HercResource.RepealedNotifications%></a></li>
                    <%} %>
                </ul>
            </div>
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
                                <%=Resources.HercResource.LastUpdated %>:</strong>
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
			  <asp:Label ID="lblmsg" runat="server" Visible="false" CssClass="redtext"></asp:Label>
                <asp:GridView ID="gvNotifications" OnRowDataBound="gvNotifications_RowDataBound"
                    runat="server" OnRowCommand="gvNotifications_RowCommand" AutoGenerateColumns="false"
                    UseAccessibleHeader="true" DataKeyNames="Link_Id" Width="100%" GridLines="None" ToolTip="Notifications" summary="This table show all notifications">
                    <Columns>
                      
                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("RowNumber")  %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:HiddenField ID="hid" runat="server" Value='<%#Eval("Details") %>' />
                                <asp:LinkButton ID="lnkTitle" runat="server" Text='<%#Eval("Name") %>' CommandName="ViewDetail"
                                    CommandArgument='<%#Eval("Link_Id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                <asp:Label ID="lblDetails" runat="server" Visible="false" Text='<%#Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedFile" runat="server" Text='<%#Eval("File_Name") %>'></asp:Literal>
                                   <%--  <asp:HyperLink ID="hypFile" runat="server"
                                        NavigateUrl='<%# "~/"+"WriteReadData/Pdf/" + Eval("File_Name") %>' Target="_blank"></asp:HyperLink>--%>
                                           <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module_Id") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Link_Id") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        Record is not available.
                    </EmptyDataTemplate>
                </asp:GridView>
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
