﻿<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="RTI.aspx.cs" Inherits="Ombudsman_RTI" %>

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
                        <%=Resources.HercResource.RTICurrent %>
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
			
                <div id="DRti" runat="server" class="RTI_link2">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lnkFAA" runat="server" OnClick="lnkFAA_click">View Current Year FAA Applications</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnkSAA" runat="server" OnClick="lnkSAA_click">View Current Year SAA Applications</asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <br />
                <br />
				<asp:Label ID="lblmsg" runat="server" Visible="false"  CssClass="redtext"></asp:Label>
                <asp:GridView ID="Grdappeal" DataKeyNames="RTI_Id" runat="server" Width="100%" AutoGenerateColumns="false"
                    UseAccessibleHeader="true" OnRowDataBound="Grdappeal_RowDataBound" OnRowCommand="Grdappeal_RowCommand"
                    ToolTip="Current Year Applications" summary="This table show all current year applications records">
                   
                    <Columns>
                        <asp:TemplateField HeaderText="RTI Ref No" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:LinkButton ID="lblrefno" runat="server"  Text='<%#Eval("Ref_No") %>' CommandArgument='<%#Eval("RTI_Id") %>'
                                    CommandName="Vdetail" ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                <%--<asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                        <%--<asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />--%>
                        <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApplicant" runat="server"  Text='<%#Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")),100) %>' >
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        
                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblStatusname" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"RTI_Status"),100) %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Appeal To FAA" Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_Id") %>' />
                                <asp:HiddenField ID="status" runat="server" Value='<%#Eval("rtistatusId") %>' />
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
