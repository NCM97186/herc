<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="Ombudsman.aspx.cs" Inherits="Ombudsman_Ombudsman" %>

<%@ Register Src="~/usercontrol/Left_Ombudsman.ascx" TagName="leftmenu_Homepage"
    TagPrefix="uc2" %>
<%@ Register Src="~/usercontrol/OmbudsmanLeftMenuFor_InternalPagesUserControl.ascx"
    TagName="OmbudsmanLeftMenuFor_InternalPagesUserControl" TagPrefix="uc3" %>
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
                        <%=headerName %>
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
									    
                        </div>
                        <div class="last-updated">
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
                <asp:Literal ID="ltrlMainContent" runat="server">
                </asp:Literal>
                <%--<asp:GridView ID="Grdaaward" DataKeyNames="Appeal_Id" runat="server" AutoGenerateColumns="false"
                    UseAccessibleHeader="true" OnRowCommand="Grdaaward_RowCommand" OnRowDataBound="Grdaaward_RowDataBound">
                    <EmptyDataTemplate>
                        Records not available.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="Ref No" DataField="RA_Id" />
                        <asp:BoundField HeaderText="Appeal Date" DataField="Year" />
                        <asp:TemplateField HeaderText="Appeal No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnlappealno" runat="server" Text='<%#Eval("Appeal_Number") %>'
                                    CommandName="getappeal_Detail" CommandArgument='<%#Eval("Appeal_Number") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Award Date" DataField="AwardDate" />
                        <asp:BoundField HeaderText="Appealicant(s)" DataField="Applicant_Name" />
                        <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" />
                        <asp:BoundField HeaderText="Subject" DataField="Subject" />
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("File_Name")%>'>
                                  
                                     <img src='<%=ResolveUrl("../images/pdf-icon.jpg") %>' title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
                <%-- <asp:Literal ID="ltrlMainContent" runat="server">
                </asp:Literal>--%>
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
<asp:Content ID="con_content" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server">
                        <%=Resources.HercResource.Aboutus %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <%--<uc3:OmbudsmanLeftMenuFor_InternalPagesUserControl ID="OmbudsmanLeftMenuFor_InternalPagesUserControl" runat="server" />--%>
            <uc2:leftmenu_Homepage ID="leftmenu_Homepage1" runat="server" />
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
