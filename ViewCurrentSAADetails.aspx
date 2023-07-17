<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCurrentSAADetails.aspx.cs"
    Inherits="ViewCurrentSAADetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Current SAA Details</title>
    <link type="text/css" rel="stylesheet" href="css/layout.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PGridDetails" runat="server">
            <asp:GridView ID="GvCurrentRTI" DataKeyNames="Temp_RTI_Id" runat="server" Width="90%"
                AutoGenerateColumns="false" UseAccessibleHeader="true" OnRowDataBound="GvCurrentRTI_RowDataBound"
                OnRowCommand="GvCurrentRTI_RowCommand" CssClass="more_details" GridLines="None"
                Caption="RTI DETAILS" ToolTip="View RTI" summary="This table show RTI details">
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="RTI Ref No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("Ref_No") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Refernce Number" DataField="Ref_No" />--%>
                    <asp:TemplateField HeaderText="Year" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Year" DataField="Year" />--%>
                    <asp:BoundField HeaderText="Date" DataField="Application_Date" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Applicant(s)">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject2" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusname" runat="server" Text='<%#Eval("RTI_Status") %>'></asp:Label>
                            <%--     <%# DataBinder.Eval(Container.DataItem, "RTI_Status")%>--%>
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
                            <asp:HiddenField ID="status" runat="server" Value='<%#Eval("RTI_Status_Id") %>' />
                            <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("Temp_RTI_Id") %>' />
                            <asp:LinkButton ID="lnklink" runat="server" Font-Bold="true" CommandArgument='<%#Eval("Temp_RTI_Id") %>'
                                CommandName="Vdetail" ToolTip="View details"></asp:LinkButton>
                            <asp:Literal ID="ltrlAppealToFAA" runat="server" Text="YES" Visible="false">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" HeaderStyle-CssClass="Text-Center" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pPetitionGridDetatils" runat="server">
            <asp:GridView ID="grdrtifAA" runat="server" Width="90%" AutoGenerateColumns="false"
                OnRowDataBound="grdrtifAA_RowDataBound" OnRowCommand="grdrtifAA_RowCommand" CssClass="more_details"
                GridLines="None" Caption="RTI-FAA DETAILS" ToolTip="View RTI-FAA" summary="This table show RTI-FAA details">
                <Columns>
                    <%--<asp:TemplateField HeaderText="Ref No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("RTI_Id") %>' />
                                <asp:HiddenField ID="hdfRefNo" runat="server" Value='<%#Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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
                    <asp:TemplateField HeaderText="Applicant(s)">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject3" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
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
                            <asp:HiddenField ID="status" runat="server" Value='<%#Eval("RTIFAAStatusId") %>' />
                            <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("Temp_RTI_FAA_Id") %>' />
                            <asp:LinkButton ID="lnlbtn" runat="server" CommandArgument='<%#Eval("Temp_RTI_FAA_Id") %>'
                                CommandName="vdetail" Font-Bold="true" ToolTip="View details" />
                            <asp:Literal ID="ltrlAppealToSAA" runat="server" Text="YES" Visible="false">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" HeaderStyle-CssClass="Text-Center" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:GridView ID="grdFAA_SAA_RTI" runat="server" Width="90%" AutoGenerateColumns="false"
            OnRowDataBound="grdFAA_SAA_RTI_RowDataBound" OnRowCommand="grdFAA_SAA_RTI_RowCommand"
            CssClass="more_details" GridLines="None" Caption="RTI-SAA DETAILS" ToolTip="View RTI-SAA" summary="This table show RTI-SAA details">
            <Columns>
                <%-- <asp:BoundField HeaderText="Ref No" DataField="RTI_FAA_Id" />--%>
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
                <%-- <asp:BoundField HeaderText="SAA(FIRST APPLET AUTHORITY)" DataField="SAA" />--%>
                <asp:TemplateField HeaderText="Applicant(s)">
                    <ItemTemplate>
                        <asp:Label ID="lblApplicant" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject5" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSAAstatus" runat="server" Text='<%#Eval("RTI_SAA_Status_Id") %>' />
                         <asp:LinkButton ID="lblUrl" runat="server" Text='<%#Eval("PlaceholderFour") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSAAstatus" runat="server" Text='<%#Eval("RTI_SAA_Status_Id") %>' />
                        <asp:HyperLink ID="Hypjudgement" runat="server" NavigateUrl='<%# Eval("PlaceholderFour") %>'
                            Text='<%# Eval("PlaceholderFour") %>' ToolTip="This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop." Target="_blank" OnClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"
                            AlternateText="Read online" />
                        <img id="img1" runat="server" width="14" height="14" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile" runat="server" Text='<%#Eval("FileName") %>'></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("FileName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Remarks" DataField="PlaceholderFive" HeaderStyle-CssClass="Text-Center" />
                <%--   <asp:BoundField HeaderText="Status" DataField="RTI_SAA_Status_Id" />--%>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
