<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRTIOmbudsmanDetails.aspx.cs"
    Inherits="Ombudsman_ViewRTIOmbudsmanDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View RTI Details of Ombudsman</title>
    <link type="text/css" rel="stylesheet" href="../css/layout.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PGridDetails" runat="server">
            <asp:GridView ID="Grdappeal" DataKeyNames="RTI_Id" runat="server" Width="90%" AutoGenerateColumns="false"
                UseAccessibleHeader="true" OnRowDataBound="Grdappeal_RowDataBound" OnRowCommand="Grdappeal_RowCommand"
                GridLines="None" CssClass="more_details" Caption="RTI DETAILS" ToolTip="View RTI Details" summary="This table show RTI details">
                <Columns>
                    <asp:TemplateField HeaderText="RTI Ref No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Date" DataField="Application_Date" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("RTI_Status_Id") %>' />
                            <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_Id") %>' />
                            <asp:HiddenField ID="rtistatus" runat="server" Value='<%#Eval("rtistatusId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                        Visible="false">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedFile" runat="server" Text='<%#Eval("FileName") %>'></asp:Literal>
                            <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("FileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appeal To FAA" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:HiddenField ID="status" runat="server" Value='<%#Eval("rtistatusId") %>' />
                            <%-- <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_Id") %>' />--%>
                            <asp:LinkButton ID="lnklink" runat="server" Font-Bold="true" CommandArgument='<%#Eval("RTI_Id") %>'
                                CommandName="Vdetail" ToolTip="View details"></asp:LinkButton>
                            <asp:Literal ID="ltrlAppealToFAA" runat="server" Visible="false" Text="YES"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" HeaderStyle-CssClass="Text-Center" />
                    <%-- <asp:BoundField HeaderText="Status" DataField="RTI_Status_Id" />--%>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <div id="pfaa" runat="server">
            <asp:Panel ID="pPetitionGridDetatils" runat="server">
                <asp:GridView ID="grdrtifAA" runat="server" Width="90%" AutoGenerateColumns="false"
                    OnRowDataBound="grdrtifAA_RowDataBound" OnRowCommand="grdrtifAA_RowCommand" OnSelectedIndexChanged="grdrtifAA_SelectedIndexChanged"
                    GridLines="None" CssClass="more_details" Caption="RTI-FAA DETAILS" ToolTip="View RTI-FAA" summary="This table show all RTI-FAA records">
                    <Columns>
                        <asp:TemplateField HeaderText="FAA Ref No" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFAArefno" runat="server" Text='<%#Eval("FAA") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date" DataField="Application_Date" ItemStyle-CssClass="Text-Center"
                            HeaderStyle-CssClass="Text-Center" />
                        <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApplicant" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField HeaderText="STATUS" DataField="RTI_FAA_Status_Id" />--%>
                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFAAstatus" runat="server" Text='<%#Eval("RTI_FAA_Status_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedFile" runat="server" Text='<%#Eval("FileName") %>'></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("FileName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appeal To SAA" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:HiddenField ID="status" runat="server" Value='<%#Eval("RTIFaaStatusId") %>' />
                                <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_FAA_Id") %>' />
                                <asp:LinkButton ID="lnlbtn" runat="server" CommandArgument='<%#Eval("RTI_FAA_Id") %>'
                                    CommandName="vdetail" Font-Bold="true" ToolTip="View details" />
                                <asp:Literal ID="ltrlAppealToSAA" runat="server" Text="YES" Visible="false">
                                </asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" HeaderStyle-CssClass="Text-Center" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
        <br />
        <div id="PSAA" runat="server">
            <asp:GridView ID="grdFAA_SAA_RTI" runat="server" Width="90%" AutoGenerateColumns="false"
                OnRowDataBound="grdFAA_SAA_RTI_RowDataBound" OnRowCommand="grdFAA_SAA_RTI_RowCommand"
                GridLines="None" CssClass="more_details" Caption="RTI-SAA DETAILS">
                <Columns>
                    <asp:TemplateField HeaderText="Ref No" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblrtino" runat="server" Text='<%#Eval("RTI_FAA_Id") %>' />
                            <asp:HiddenField ID="hdfRefNo" runat="server" Value='<%#Eval("RTI_year") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SAA Ref No" HeaderStyle-CssClass="Text-Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSAArefno1" runat="server" Text='<%#Eval("SAA_RefNo") %>' />
                            <asp:HiddenField ID="HypYear" runat="server" Value='<%#Eval("Year") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SIC Ref No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAArefno" runat="server" Text='<%#Eval("SAA") %>' />
                            <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_SAA_Id") %>' />
                            <asp:HiddenField ID="Hystatus" runat="server" Value='<%#Eval("RTISAAStatusId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Date" DataField="Application_Date" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAAstatus" runat="server" Text='<%#Eval("RTI_SAA_Status_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAAstatus" runat="server" Text='<%#Eval("RTI_SAA_Status_Id") %>' />
                            <asp:HyperLink ID="Hypjudgement" runat="server" NavigateUrl='<%# Eval("PlaceholderFour") %>'
                                Text='<%# Eval("PlaceholderFour") %>' ToolTip="This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop." Target="_blank" OnClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"
                                AlternateText="Read online" />
                            <img id="img1" runat="server" width="14" height="14" alt="External Link Image" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                        Visible="false">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedFile" runat="server" Text='<%#Eval("FileName") %>'></asp:Literal>
                            <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("FileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" />
                </Columns>
            </asp:GridView>
            <%-- <asp:Button ID="btnback" CssClass="btn" runat="server" Text="Back" OnClick="btnback_Click" />--%>
        </div>
    </div>
    </form>
</body>
</html>
