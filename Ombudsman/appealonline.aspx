<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="appealonline.aspx.cs" Inherits="Ombudsman_appealonline"  %>

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
                        <%=Resources.HercResource.OnlineStatus %>
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                           <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div class="last-updated">
                         </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
               <div class="search_form">
                    <div>
                     <label for="<%=drpyear.ClientID %>">
                        <span>Select Year:</span></label>
                        <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div>
                       <label for="<%=ddlappealNumber.ClientID %>">
                        <span>Appeal Number:</span></label>
                        <asp:DropDownList ID="ddlappealNumber" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="search_btn">
                        <asp:Button ID="btnsearchYearwise" runat="server" Text="GO" OnClick="btnsearchYearwise_click" />
                    </div>
                </div>
               <%-- <span style="width: 100%">
                    <asp:Label ID="lblappeal" runat="server" Text="Appeal Number"></asp:Label>
                    <asp:TextBox ID="txtaplno" runat="server" />
                    <asp:Button ID="btnsearch" runat="server" Text="search" OnClick="btnsearch_click" />
                </span>--%>  <div class="clear">
                </div>
                <br />  <br />
                <asp:GridView ID="gvOnlineStatus" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    DataKeyNames="Appeal_Id"  Width="100%" OnRowCommand="gvOnlineStatus_RowCommand" OnRowDataBound="gvOnlineStatus_RowDataBound">
                    <Columns>
                       <asp:TemplateField HeaderText="Appeal No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkappeal" runat="server" Text='<%#Eval("Appeal_Number") %>'
                                CommandName="View"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Appeal_Id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:BoundField HeaderText="Appeal Date" DataField="Appeal_Date" />
                   
                      <asp:TemplateField HeaderText="Appellant(s)" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApplicant" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Applicant_Name"),100) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Status" DataField="AppealStatusName" />
                    </Columns>
                    <EmptyDataTemplate>
                        <%=Resources.HercResource.NoDataAvailable %>
                    </EmptyDataTemplate>
                </asp:GridView>
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
<asp:Content ID="Content4" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server">
                        <%=Resources.HercResource.Appeal %>
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
