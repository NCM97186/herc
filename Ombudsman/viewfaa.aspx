<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="viewfaa.aspx.cs" Inherits="Ombudsman_viewfaa" %>

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
                        <%=Resources.HercResource.RTI %>
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                title="Print" />
                        </div>
                        <div class="last-updated">
                            <%=Resources.HercResource.LastUpdated %>:
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
                <asp:GridView ID="Grdappeal" DataKeyNames="RTI_Id" runat="server" Width="100%" AutoGenerateColumns="false"
                    UseAccessibleHeader="true" OnRowDataBound="Grdappeal_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Ref No">
                            <ItemTemplate>
                                <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" />
                        <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                        <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                        <asp:BoundField HeaderText="Subject" DataField="Subject" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("RTI_Status_Id") %>' />
                                <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_Id") %>' />
                                <asp:HiddenField ID="rtistatus" runat="server" Value='<%#Eval("rtistatusId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField HeaderText="Status" DataField="RTI_Status_Id" />--%>
                    </Columns>
                </asp:GridView>
                <br />
                <div id="pfaa" runat="server">
                    <asp:GridView ID="grdrtifAA" runat="server" Width="100%" AutoGenerateColumns="false"
                        OnRowDataBound="grdrtifAA_RowDataBound" OnRowCommand="grdrtifAA_RowCommand" OnSelectedIndexChanged="grdrtifAA_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="Ref No" DataField="RTI_Id" />
                            <asp:BoundField HeaderText="Year" DataField="Year" />
                            <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                            <asp:BoundField HeaderText="FAA(First Applet Authority)" DataField="FAA" />
                            <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                            <asp:BoundField HeaderText="Subject" DataField="Subject" />
                            <%-- <asp:BoundField HeaderText="STATUS" DataField="RTI_FAA_Status_Id" />--%>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblFAAstatus" runat="server" Text='<%#Eval("RTI_FAA_Status_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Appeal To SAA">
                                <ItemTemplate>
                                    <asp:HiddenField ID="status" runat="server" Value='<%#Eval("RTIFaaStatusId") %>' />
                                    <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_FAA_Id") %>' />
                                    <asp:LinkButton ID="lnlbtn" runat="server" CommandArgument='<%#Eval("RTI_FAA_Id") %>'
                                        CommandName="vdetail" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div id="PSAA" runat="server">
                    <asp:GridView ID="grdFAA_SAA_RTI" runat="server" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="Ref No" DataField="RTI_FAA_Id" />
                            <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                            <asp:BoundField HeaderText="SAA(Second Applet Authority)" DataField="SAA" />
                            <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                            <asp:BoundField HeaderText="Subject" DataField="Subject" />
                            <asp:BoundField HeaderText="Status" DataField="RTI_SAA_Status_Id" />
                        </Columns>
                    </asp:GridView>
                    <%-- <asp:Button ID="btnback" CssClass="btn" runat="server" Text="Back" OnClick="btnback_Click" />--%>
                </div>
                <div class="clear">
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
