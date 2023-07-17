<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="rtisearch.aspx.cs" Inherits="Ombudsman_rtisearch" %>

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
                        <%=Resources.HercResource.ApplicationSearch%>
                    </h2>
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
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearchYearwise">
                    <div class="search_form">
                        <div>
                            <label for="<%=drpyear.ClientID %>">
                                <span>Select Year:</span></label>
                            <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label for="<%=drpreference.ClientID %>">
                                <span>Reference No: </span>
                            </label>
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
                <br />
                <asp:Panel ID="pnlRtiSearch" runat="server" DefaultButton="btnsearch">
                    <div class="search_form">
                        <div>
                            <label for="<%=txtname.ClientID %>">
                                <span>Applicant Name:</span></label>
                            <asp:TextBox ID="txtname" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                        <div>
                            <label for="<%=txtSubject.ClientID %>">
                                <span>Subject: </span>
                            </label>
                            <asp:TextBox ID="txtSubject" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="search_btn">
                            <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_click" />
                        </div>
                    </div>
                </asp:Panel>
                <div class="clear">
                </div>
                <div class="clear">
                </div>
                <br />
                <asp:GridView ID="GrdRTI" DataKeyNames="RTI_Id" Width="100%" runat="server" AutoGenerateColumns="false"
                    UseAccessibleHeader="true" OnRowDataBound="GrdRTI_RowDataBound" OnRowCommand="GrdRTI_RowCommand"
                    GridLines="None" ToolTip="Application Search" summary="This table show all the RTI which will search by the users">
                    <EmptyDataTemplate>
                        <%=Resources.HercResource.NoDataAvailable %>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="RTI Ref No">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' CommandArgument='<%#Eval("RTI_Id") %>'
                                    CommandName="Vdetail" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                <%--     <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                        <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApplicant" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Applicant_Name"),100) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:HiddenField ID="status" runat="server" Value='<%# Eval("rtistatusId") %>' />
                                <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_Id") %>' />
                                <asp:Label ID="lblstatus" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(Eval("RTI_Status_Id"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("FileName")%>'>
                                  
                                     <img src="<%=ResolveUrl("~/images/pdf-icon.jpg")%>" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("FileName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appeal" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnklink" runat="server" Font-Bold="true" CommandArgument='<%#Eval("RTI_Id") %>'
                                    CommandName="Vdetail"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"PlaceholderFive"),100) %>' />
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
<asp:Content ID="Content4" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server">
                        <%=Resources.HercResource.RTI %>
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
