<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="prevyearawardpronounce.aspx.cs" Inherits="Ombudsman_prevyearawardpronounce" %>

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
                        <%=Resources.HercResource.Previousyearawards %>
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                     
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <%=Resources.HercResource.LastUpdated %>:
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
			   <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                <p id="pyear" runat="server" visible="false">
                <label for="<%=drpyear.ClientID %>">
                    Select for Previous Years Awards:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                   
                </p>
                <br />
                <asp:GridView ID="Grdaaward" DataKeyNames="Appeal_Id" Width="100%" runat="server"
                    AutoGenerateColumns="false" UseAccessibleHeader="true" OnRowCommand="Grdaaward_RowCommand"
                    OnRowDataBound="Grdaaward_RowDataBound">
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="Ref No" DataField="RA_Id" Visible="false" />
                        <asp:BoundField HeaderText="Award Date" DataField="AwardDate" />
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Appeal No" >
                            <ItemTemplate>
                                <asp:Label ID="lnlappealno" runat="server" Text='<%#Eval("AppealNumber") %>' />
                                <%--<asp:LinkButton ID="lnlappealno" runat="server" Text='<%#Eval("AppealNumber") %>'
                                    CommandName="getappeal_Detail" CommandArgument='<%#Eval("Appeal_Number") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appellant(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Applicant_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"PlaceholderTwo"),100)%>' CommandName="getappeal_Detail" CommandArgument='<%#Eval("Appeal_Id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>' />
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
                <%--<div id="pappeal" runat="server" visible="false">
                    <asp:GridView ID="grdappeal" AutoGenerateColumns="false" runat="server" GridLines="None" >
                        <Columns>
                            <asp:BoundField HeaderText="Date of Award" DataField='AwardDate' />
                            <asp:BoundField HeaderText='Appeal No' DataField='Appeal_Number' />
                            <asp:BoundField HeaderText='Year' DataField='Year' />
                            <asp:BoundField HeaderText='Applicant(s)' DataField='Applicant_Name' />
                            <asp:BoundField HeaderText='Respondent(s)' DataField='Respondent_Name' />
                            <asp:BoundField HeaderText='Subject' DataField='Subject' />
                            <asp:BoundField HeaderText='Where Appealed' DataField='whap' />
                          
                            <asp:TemplateField HeaderText="Judgement Link">
                                <ItemTemplate>
                                    <asp:HyperLink ID="ImageButton1" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                                        Text='<%#Eval("Judgement_Link") %>' Target="_blank" OnClientClick='<%# String.Format("javascript:return openTargetURL(\"{0}\")", Eval("Judgement_Link")) %>'
                                        AlternateText="Read online" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>--%>
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
