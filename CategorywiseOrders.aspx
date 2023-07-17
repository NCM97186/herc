<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="CategorywiseOrders.aspx.cs" Inherits="CategorywiseOrders" %>

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
                        <%=Resources.HercResource.CategorywiseOrders %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                        
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                      
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <p>
                    <label for="<%=ddlOrderCategory.ClientID %>">
                        Order Category :</label>
                    <asp:DropDownList ID="ddlOrderCategory" runat="server" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvOrderCategory" ValidationGroup="Add" runat="server"
                        SetFocusOnError="true" ControlToValidate="ddlOrderCategory" ErrorMessage="Please select order category."
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </p>
                <br />
                <br />
                <asp:GridView ID="gvDailyOrders" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    DataKeyNames="OrderId" OnRowDataBound="gvDailyOrders_RowDataBound" Width="100%"  OnRowCommand="gvDailyOrders_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataField="OrderDate" />
                        <asp:TemplateField HeaderText="Petition/RA Number" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblnumber" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Petitioner_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"OrderTitle"),100) %>' CommandName="viewDetails"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderId") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("OrderFile")%>'>
                                  
                                     <img src='<%=ResolveUrl("images/pdf-icon.jpg") %>' title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("OrderFile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:Literal ID="lnkConnectedFile" runat="server"></asp:Literal>
                                <%--  <asp:LinkButton ID="lnkConnectedFile" runat="server" CausesValidation="false" Text=""></asp:LinkButton>--%>
                                <asp:Label ID="lblorderId" runat="server" Text='<%#Eval("OrderId") %>' Visible="false"></asp:Label>
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
                               <label for="<%=ddlPageSize.ClientID %>">&nbsp;</label>
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
                    <li><a href="~/CurrentYearOrders.aspx"  id="current" runat="server">
                        <%=Resources.HercResource.CurrentYearOrder %></a></li>
                    <li><a href="~/CurrentYearOrders/1.aspx" id="previous"
                        runat="server">
                        <%=Resources.HercResource.PreviousYearOrder %></a></li>
                    <li><a href='<%=ResolveUrl("~/OrderUnderAppeal.aspx") %>' title='<%=Resources.HercResource.OrdersunderAppeal %>'>
                        <%=Resources.HercResource.OrdersunderAppeal %></a></li>
                    <li><a href='<%=ResolveUrl("~/CurrentOrderSearch.aspx") %>' title='<%=Resources.HercResource.OrdersSearch %>'>
                        <%=Resources.HercResource.OrdersSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/CategorywiseOrders.aspx") %>' title='<%=Resources.HercResource.CategorywiseOrders %>'  class="current">
                        <%=Resources.HercResource.CategorywiseOrders %></a></li>
                    <% }
                       else
                       { %>
                    <li><a href="~/content/Hindi/CurrentYearOrders.aspx" 
                        id="currentHindi" runat="server">
                        <%=Resources.HercResource.CurrentYearOrder %></a></li>
                    <li><a href="~/content/Hindi/CurrentYearOrders/1.aspx" 
                        id="previousHindi" runat="server">
                        <%=Resources.HercResource.PreviousYearOrder %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/OrderUnderAppeal.aspx") %>' title='<%=Resources.HercResource.OrdersunderAppeal %>'>
                        <%=Resources.HercResource.OrdersunderAppeal %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/CurrentOrderSearch.aspx") %>' title='<%=Resources.HercResource.OrdersSearch %>'>
                        <%=Resources.HercResource.OrdersSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/CategorywiseOrders.aspx") %>' title='<%=Resources.HercResource.CategorywiseOrders %>'  class="current">
                        <%=Resources.HercResource.CategorywiseOrders %></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
