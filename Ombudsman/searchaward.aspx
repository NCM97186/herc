﻿<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="searchaward.aspx.cs" Inherits="Ombudsman_searchaward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/OmbudsmanLeftMenuFor_InternalPagesUserControl.ascx"
    TagName="OmbudsmanLeftMenuFor_InternalPagesUserControl" TagPrefix="uc1" %>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphrightholder" runat="Server">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.AwardsSearch%>
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div class="last-updated">
                            <%--  <%=Resources.HercResource.LastUpdated %>:--%>
                            <%--  <%=lastUpdatedDate%>--%>
                           </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <asp:Panel ID="pnlYearWise" runat="server" DefaultButton="btnsearchYearwise">
                    <div class="search_form">
                        <div>
                            <label for="<%=drpyear.ClientID %>">
                                <span>Select Year:</span></label>
                            <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label for="<%=drpreference.ClientID %>">
                                <span>Appeal Number:</span></label>
                            <asp:DropDownList ID="drpreference" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="search_btn">
                            <asp:Button ID="btnsearchYearwise" runat="server" Text="GO" OnClick="btnsearchYearwise_click" />
                        </div>
                    </div>
                </asp:Panel>
                <div class="optional_or">
                    <asp:Label ID="lbl" runat="server" Text="OR"></asp:Label>
                </div>
                <asp:Panel ID="pnlname" runat="server" DefaultButton="btnsearch">
                    <div class="search_form">
                        <div>
                            <label for="<%=txtname.ClientID %>">
                               <span> Appellant Name:</span></label>
                            <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <label for="<%=txtrespodent.ClientID %>">
                                <span> Respodent Name: </span></label>
                            <asp:TextBox ID="txtrespodent" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <label for="<%=txtsubject.ClientID %>">
                                <span> Subject: </span></label>
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
                <asp:Panel ID="pnlDate" runat="server" DefaultButton="btndate">
                    <div class="search_form_Date">
                        <label for="<%=txtDate.ClientID %>">
                           <span>  Date: </span>
                        </label>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <label for="<%=ImageButton3.ClientID %>">
                            &nbsp;
                        </label>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                            CausesValidation="false" AlternateText="calender" />
                        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                            Tip:-dd/mm/yyyy</span></em>
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
                <div>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                        PopupButtonID="ImageButton3">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                    </cc1:CalendarExtender>
                    <br />
                    <br />
                    <asp:GridView ID="Grdaaward" DataKeyNames="Appeal_Id" Width="100%" runat="server"
                        AutoGenerateColumns="false" UseAccessibleHeader="true" OnRowCommand="Grdaaward_RowCommand"
                        OnRowDataBound="Grdaaward_RowDataBound">
                        <EmptyDataTemplate>
                            <%=Resources.HercResource.NoDataAvailable %>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="Ref No" DataField="RA_Id" Visible="false" />
                            <asp:BoundField HeaderText="Award Date" DataField="Appeal_Date" />
                            <asp:BoundField HeaderText="Appeal Date" DataField="Year" Visible="false" />
                            <asp:TemplateField HeaderText="Appeal No">
                                <ItemTemplate>
                                    <asp:Label ID="lnlappealno" runat="server" Text='<%#Eval("AppealNumber") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Appellant(s)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters( DataBinder.Eval(Container.DataItem,"Applicant_Name"),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Respondent(s)">
                                <ItemTemplate>
                                    <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblSubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"subject"),100) %>'
                                        CommandName="getappeal_Detail" CommandArgument='<%#Eval("Appeal_Id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrlConnectedAwardProunced" runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
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
                        <asp:DropDownList ID="ddlPageSize" runat="server" Visible="false" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server">
                        <%=Resources.HercResource.AwardsPronounced %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <uc1:OmbudsmanLeftMenuFor_InternalPagesUserControl ID="LeftMenuFor_InternalPagesUserControl1"
                runat="server" />
        </div>
    </div>
</asp:Content>
