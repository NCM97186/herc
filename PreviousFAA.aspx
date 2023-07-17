﻿<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="PreviousFAA.aspx.cs" Inherits="PreviousFAA" Title="" %>

<%@ Register Src="UserControl/RTI_Control.ascx" TagName="RTI_Control" TagPrefix="uc1" %>
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
                        <%=Resources.HercResource.PreviousYearApplicationsFAA%></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" title="Print" target="_blank">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>

                                    
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <b><%=Resources.HercResource.LastUpdated %>:</b> <%=lastUpdatedDate %>
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
                            <asp:LinkButton ID="lnkRTI" runat="server" OnClick="lnkRTI_click">View Previous Years RTI Applications</asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lnkSAA" runat="server" OnClick="lnkSAA_click">View Previous Years SAA Applications</asp:LinkButton></li>
                    </ul>
                </div>
                <br />
                <br />
				 <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                <p id="pyear" runat="server" visible="false">
                    Select for Previous Years RTI FAA Applications:
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                   
                </p>
                <br />
                <asp:GridView ID="grdrtifAA" runat="server" Width="100%" AutoGenerateColumns="false"
                    OnRowDataBound="grdrtifAA_RowDataBound" OnRowCommand="grdrtifAA_RowCommand" ToolTip='<%#Resources.HercResource.PreviousYearApplicationsFAA %>' summary="This table show all RTI-FAA records">
                    <Columns>
                     
                        <asp:TemplateField HeaderText="FAA Ref No" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFAArefno" runat="server" Text='<%#Eval("FAAnew") %>' CommandName="View"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RTI_FAA_Id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                <%--     <asp:Label ID="lblFAArefno" runat="server" Text='<%#Eval("FAA") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date" DataField="Application_Date" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"/>
                       
                        <asp:TemplateField HeaderText="RTI Ref No"  ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
   
                            <%# DataBinder.Eval(Container.DataItem, "refno")%>
                            </ItemTemplate>
                        </asp:TemplateField>  
                       
                        <%--<asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" HeaderStyle-HorizontalAlign="Center"/>--%>
                        <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApplicant" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")),100) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFAAstatus" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"statusName"),100) %>' />
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
                        <asp:TemplateField HeaderText="Appeal To SAA" Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="status" runat="server" Value='<%#Eval("RTIFaaStatusId") %>' />
                                <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_FAA_Id") %>' />
                                <asp:LinkButton ID="lnlbtn" runat="server" CommandArgument='<%#Eval("RTI_FAA_Id") %>'
                                    CommandName="vdetail" Font-Bold="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"PlaceholderFive")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
                <br />
                
                <div class="clear">
                </div>
                <p>
                   <div style="float: right;" class="paging" >
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
                    </div>
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
                        <%=Resources.HercResource.RTI %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <uc1:RTI_Control ID="RTI_Control2" runat="server" />
        </div>
    </div>
</asp:Content>
