<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="ViewRtiFaa.aspx.cs" Inherits="ViewRtiFaa" Title="Untitled Page" %>

<%@ Register Src="UserControl/RTI_Control.ascx" TagName="RTI_Control" TagPrefix="uc1" %>
<%@ Register Src="UserControl/hercfooter.ascx" TagName="hercfooter" TagPrefix="uc2" %>
<asp:Content ID="contentbreadhum" runat="server" ContentPlaceHolderID="cphbreadcrumholder">
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
                        <%=Resources.HercResource.RTI %>
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="javascript:void(0);" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div class="last-updated">
                            <b>Last Update:</b> <a href="javascript:void(0);">07-07-2010</a>
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
                    UseAccessibleHeader="true" OnRowDataBound="Grdappeal_RowDataBound" OnRowCommand="Grdappeal_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Ref No">
                            <ItemTemplate>
                                <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="Year" DataField="Year" />--%>
                        <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                        <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                        <asp:BoundField HeaderText="Subject" DataField="Subject" />
                        <%--<asp:BoundField HeaderText="Status" DataField="RTI_Status_Id" />--%>
                         <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("RTI_Status_Id") %>' />
                                  <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_Id") %>' />
                                   <asp:HiddenField ID="rtistatus" runat="server" Value='<%#Eval("rtistatusId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("FileName")%>'>
                                  
                                     <img src="../images/pdf-icon.jpg" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("FileName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" />
                    </Columns>
                </asp:GridView>
                <br />
                <div id="pfaa" runat="server">
                    <asp:GridView ID="grdrtifAA" runat="server" Width="100%" AutoGenerateColumns="false"
                        OnRowDataBound="grdrtifAA_RowDataBound" OnRowCommand="grdrtifAA_RowCommand" OnSelectedIndexChanged="grdrtifAA_SelectedIndexChanged" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="Ref No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("RTI_Id") %>' />
                                    <asp:HiddenField ID="hdfRefNo" runat="server" Value='<%#Eval("RTI_year") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FAA Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblFAArefno" runat="server" Text='<%#Eval("FAA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Year">
                           <ItemTemplate>
                             <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                           </ItemTemplate>
                         </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="Year" DataField="Year" />--%>
                            <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                           <%-- <asp:TemplateField HeaderText="FAA Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblFAArefno" runat="server" Text='<%#Eval("FAA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:BoundField HeaderText="FAA(FIRST APPLET AUTHORITY)" DataField="FAA" />--%>
                            <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                            <asp:BoundField HeaderText="Subject" DataField="Subject" />
                            <%--<asp:BoundField HeaderText="Status" DataField="RTI_FAA_Status_Id" />--%>
                            
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
                <br />
                <div id="PSAA" runat="server">
                    <asp:GridView ID="grdFAA_SAA_RTI" runat="server" Width="100%" AutoGenerateColumns="false" OnRowDataBound="grdFAA_SAA_RTI_RowDataBound" GridLines="None">
                        <Columns>
                           <%-- <asp:BoundField HeaderText="Ref No" DataField="RTI_FAA_Id" />--%>
                           <asp:TemplateField HeaderText="Ref No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblrtino" runat="server" Text='<%#Eval("RTI_FAA_Id") %>' />
                                    <asp:HiddenField ID="hdfRefNo" runat="server" Value='<%#Eval("RTI_year") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            
                            <asp:TemplateField HeaderText="SIC Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSAArefno" runat="server" Text='<%#Eval("SAA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Date" DataField="Application_Date" />
                            <%-- <asp:BoundField HeaderText="SAA(FIRST APPLET AUTHORITY)" DataField="SAA" />--%>
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
