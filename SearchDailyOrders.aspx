﻿<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="SearchDailyOrders.aspx.cs" Inherits="SearchDailyOrders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                            <%-- <b>Last Update:</b> <%=lastUpdatedDate %>--%>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <p class="search_form_Date">
                    <label for="<%=ddlconnectionYearWise.ClientID %>">
                        <span>Select :</span></label>
                    <asp:DropDownList ID="ddlconnectionYearWise" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlconnectionYearWise_SelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                        <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                    </asp:DropDownList>
                </p>
                <div class="search_form">
                    <div>
                        <label for="<%=drpyear.ClientID %>">
                            <span>Select Year:</span></label>
                        <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label for="<%=drppro.ClientID %>">
                            <span>Petition/RA Number:</span></label>
                        <asp:DropDownList ID="drppro" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="search_btn">
                        <asp:Button ID="btnsearchYearwise" runat="server" Text="GO" OnClick="btnsearchYearwise_click" />
                    </div>
                </div>
                <div class="optional_or">
                    
                    OR
                </div>
                <p class="search_form_Date">
                    <label for="<%=ddlConnectionType.ClientID %>">
                        <span>Select :</span></label>
                    <asp:DropDownList ID="ddlConnectionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlConnectionType_SelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                        <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                        <asp:ListItem Value="3"> Others</asp:ListItem>
                    </asp:DropDownList>
                </p>
                <asp:Panel ID="pnlDailyOrderSearch" runat="server" DefaultButton="btnsearch">
                <div class="search_form">
                    <div>
                        <label for="<%=txtname.ClientID %>">
                            <span>Petitioner Name: </span>
                        </label>
                        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%=txtrespodent.ClientID %>">
                            <span>Respondent Name: </span>
                        </label>
                        <asp:TextBox ID="txtrespodent" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%=txtsubject.ClientID %>">
                            <span>Subject: </span>
                        </label>
                        <asp:TextBox ID="txtsubject" runat="server"></asp:TextBox>
                    </div>
                    <div class="search_btn">
                        <asp:Button ID="btnsearch" runat="server" Text="GO" OnClick="btnsearch_click" />
                    </div>
                </div>
                </asp:Panel>
                <div class="optional_or">
                    <asp:Label ID="Label4" runat="server" Text="OR"></asp:Label>
                </div>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btndate">
                    <p class="search_form_Date">
                        <label for="<%=ddlDate.ClientID %>">
                            <span>Select:</span></label>
                        <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                            <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                            <asp:ListItem Value="3"> Others</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <div class="search_form_Date">
                        <label for="<%=txtDate.ClientID %>">
                            <span>Daily Order Date<strong>*</strong>:</span></label>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <label for="<%=ImageButton3.ClientID %>">
                            &nbsp;</label>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                            CausesValidation="false" AlternateText="Cal" />
                        <em><span style="font-size: 8pt; font-weight: bold; font-family: Verdana">Tip:-dd/mm/yyyy</span></em>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Add"
                            ControlToValidate="txtDate" ErrorMessage="Please select date.">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                            ValidationGroup="Add">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="search_btn">
                        <asp:Button ID="btndate" runat="server" Text="GO" OnClick="btndate_click" ValidationGroup="Add" />
                    </div>
                </asp:Panel>
                <div class="clear">
                </div>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                    PopupButtonID="ImageButton3">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                </cc1:CalendarExtender>
                <br />
                <asp:GridView ID="gvDailyOrders" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCommand="gvDailyOrders_RowCommand" OnRowDataBound="gvDailyOrders_RowDataBound"
                    DataKeyNames="OrderId" Width="100%" ToolTip='<%#Resources.HercResource.DailyOrdersSearch %>'>
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataField="OrderDate" />
                        <asp:BoundField HeaderText="Petition/RA Number" DataField="PRO_No" />
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Petitioner_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>'
                                    CommandName="viewDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderId") %>'
                                    ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("OrderFile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <%=Resources.HercResource.NoDataAvailable %>
                    </EmptyDataTemplate>
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
                    <li><a href="<%=ResolveUrl("~/CurrentDailyOrders.aspx")%>" title=' <%=Resources.HercResource.CurrentYear %>'
                        class="margin">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href="<%=ResolveUrl("~/PrevYear/1.aspx")%>" title='<%=Resources.HercResource.PreviousYears%>'
                        class="margin">
                        <%=Resources.HercResource.PreviousYears%></a></li>
                    <li><a href="<%=ResolveUrl("~/SearchDailyOrders.aspx")%>" title='<%=Resources.HercResource.DailyOrdersSearch%>'
                        class="current">
                        <%=Resources.HercResource.DailyOrdersSearch%></a></li>
                    <%-- <li><a href="<%=ResolveUrl("~/CurrentDailyOrders/2.aspx")%>" title='<%=Resources.HercResource.RepealedDailyOrders%>'
                        class="margin">
                        <%=Resources.HercResource.RepealedDailyOrders%></a></li>--%>
                    <% }

                       else
                       { %>
                    <li><a href="<%=ResolveUrl("~/content/Hindi/CurrentDailyOrders.aspx")%>" title='<%=Resources.HercResource.CurrentYear %>'>
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href="<%=ResolveUrl("~/content/Hindi//PrevYear/1.aspx")%>" title='<%=Resources.HercResource.PreviousYears%>'>
                        <%=Resources.HercResource.PreviousYears%></a></li>
                    <li><a href="<%=ResolveUrl("~/content/Hindi/SearchDailyOrders.aspx")%>" class="current"
                        title='<%=Resources.HercResource.DailyOrdersSearch%>'>
                        <%=Resources.HercResource.DailyOrdersSearch%></a></li>
                    <%--<li><a href="<%=ResolveUrl("~/content/Hindi/CurrentDailyOrders/2.aspx")%>" title='<%=Resources.HercResource.RepealedDailyOrders%>'>
                        <%=Resources.HercResource.RepealedDailyOrders%></a></li>--%>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
