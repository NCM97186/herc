<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAppealDetails.aspx.cs"
    Inherits="Ombudsman_ViewAppealDetails" Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Appeal Details</title>
    <link type="text/css" rel="stylesheet" href="../css/layout.css">
    <script type="text/javascript" language="javascript">

        function closeIt() {
            close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PGridDetails" runat="server">
            <asp:GridView ID="Grdappeal" DataKeyNames="Appeal_Id" runat="server" AutoGenerateColumns="false"
                UseAccessibleHeader="true" GridLines="None" CssClass="more_details" OnRowDataBound="Grdappeal_RowDataBound"
                OnRowCommand="Grdappeal_RowCommand" Caption="APPEAL DETAILS" Width="90%" ToolTip="View Appeal" summary="This table show all  appeal records">
                <Columns>
                    <asp:BoundField HeaderText="Appeal No" DataField="Appeal_Number" HeaderStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                    <asp:BoundField HeaderText="Appeal Date" DataField="Appeal_Date" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Appellant(s)" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAppellant" runat="server" Text='<%#Eval("Applicant_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondent" runat="server" Text='<%#Eval("Respondent_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("Subject") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" DataField="AppealStatusName" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Award pronounced" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedAwardProunced" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Schedule of Hearing" HeaderStyle-CssClass="Text-Center"
                        ItemStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblConnectedSoh_ID" runat="server" Visible="false" Text="No" />
                            <asp:LinkButton ID="lnkConnectedSoh" ToolTip="Click here to view details" runat="server"
                                CommandName="connectedSoh" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Appeal_Id")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hyappealId" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                            <asp:Literal ID="ltrlConnectedSoh" runat="server" Text="Yes" Visible="false">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />--%>
                    <asp:TemplateField HeaderText="Award under appeal" HeaderStyle-CssClass="Text-Center"
                        ItemStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAwardunderappeal" runat="server" Visible="false" Text="No" />
                            <asp:LinkButton ID="lnkAwardunderappeal" runat="server" ToolTip="Click here to view details"
                                CommandName="connectedAwardunderappeal" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Appeal_Id")%>'></asp:LinkButton>
                            <asp:Literal ID="ltrlAwardUnderAppeal" runat="server" Text="Yes" Visible="false">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="PnlSOH" runat="server" Visible="false">
            <asp:GridView ID="gvSOH" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                DataKeyNames="Soh_ID" Width="90%" OnRowCommand="gvSOH_RowCommand" OnRowDataBound="gvSOH_RowDataBound"
                CssClass="more_details" GridLines="None" Caption="SCHEDULE OF HEARING" ToolTip="View Schedule of hearing" summary="This table show all Schedule of hearing records">
                <Columns>
                    <asp:BoundField HeaderText="Appeal No" DataField="Appeal_Number" Visible="false"
                        HeaderStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                    <asp:BoundField HeaderText="Date & Time" DataField="Date" HeaderStyle-CssClass="Text-Center"
                        ItemStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Petitioner(s)" DataField="Applicant_Name" Visible="false" />
                    <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" Visible="false" />
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject4" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Venue" DataField="Venue" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedFileSOHDetails" runat="server"></asp:Literal>
                            <%--   <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("soh_file")%>'>
                                  
                                     <img src="<%=ResolveUrl("~/images/pdf-icon.jpg")%>" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>--%>
                            <%-- <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("soh_file") %>' />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlAward" runat="server" Visible="false">
            <asp:GridView ID="gvAward" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                DataKeyNames="RA_Id" Width="90%" OnRowCommand="gvAward_RowCommand" OnRowDataBound="gvAward_RowDataBound"
                CssClass="more_details" GridLines="None" Caption="AWARD PRONOUNCED" ToolTip="Award Pronounced" summary="This table show all award pronounced records">
                <Columns>
                    <asp:BoundField HeaderText="Award Date" DataField="AwardDate" />
                    <asp:TemplateField HeaderText="Appeal No">
                        <ItemTemplate>
                            <asp:Label ID="lnlappealno" runat="server" Text='<%#Eval("Appeal_Number") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appellant(s)">
                        <ItemTemplate>
                            <asp:Label ID="lblPetitioner" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent(s)">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PlaceholderTwo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlAwardProunced" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hdfFileAward" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarksAward" runat="server" Text='<%#Eval("PlaceholderFour") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="PSOHDetails" runat="server" Visible="false">
            <asp:GridView ID="gvSOHDetails" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                DataKeyNames="Soh_ID" Width="90%" CssClass="more_details" GridLines="None" Caption="SCHEDULE OF HEARING DETAILS"
                OnRowDataBound="gvSOHDetails_RowDataBound" ToolTip="Schedule of hearing details" summary="This table show all schedule of hearing records">
                <Columns>
                    <asp:BoundField HeaderText="Date & Time" DataField="Date" HeaderStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Appeal No" DataField="PRO_No" HeaderStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                    <asp:TemplateField HeaderText="Applicant(s)" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantSoh" runat="server" Text='<%#Eval("Applicant_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondentSoh" runat="server" Text='<%#Eval("Respondent_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubjectSoh" runat="server" Text='<%#Eval("Subject") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Venue" DataField="Venue" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarksSoh" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table id="SOHFiles" runat="server" width="90%" cellpadding="0" cellspacing="0" class="more_details"
                align="center" visible="false">
                <tr id="trFileName" runat="server">
                    <td>
                        <strong>Download:</strong>
                    </td>
                    <td>
                        <asp:Literal ID="ltrlConnectedFileSOH" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PAwardUnderAppeal" runat="server" Visible="false">
            <asp:GridView ID="GvAwardUnderAppeal" runat="server" AutoGenerateColumns="false"
                UseAccessibleHeader="true" DataKeyNames="Appeal_Id" Width="90%" OnRowDataBound="GvAwardUnderAppeal_RowDataBound"
                CssClass="more_details" GridLines="None" Caption="AWARD UNDER APPEAL" ToolTip="View Award Under Appeal" summary="This table show all award under appeal records">
                <Columns>
                    <asp:BoundField HeaderText="Appeal No" DataField="Appeal_Number" HeaderStyle-CssClass="Text-Center"
                        Visible="false" />
                    <asp:BoundField HeaderText="Where Appealed" DataField="Where_Appealed" HeaderStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Ref No (Where Appealed)" DataField="ReferenceNumber"
                        HeaderStyle-CssClass="Text-Center" />
                    <asp:BoundField HeaderText="Date (Where Appealed)" DataField="Appeal_Date" HeaderStyle-CssClass="Text-Center"
                        Visible="false" />
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAwardStatus" runat="server" Text='<%#Eval("Award_StatusID") %>' />
                            <%#DataBinder.Eval(Container.DataItem, "OtherDescription")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlAwardUnderAppeal" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hyappealId" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:BoundField HeaderText='Status' DataField='Award_StatusID' HeaderStyle-CssClass="Text-Center" />--%>
                    <asp:BoundField HeaderText="Judgement Description" DataField="Description" HeaderStyle-CssClass="Text-Center"
                        Visible="false" />
                    <asp:TemplateField HeaderText="Judgement Link" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem,"Description")%>
                            <asp:HyperLink ID="Hypjudgement" runat="server" ToolTip="Click here to view details"
                                NavigateUrl='<%# Eval("Judgement_Link") %>' Text='<%# Eval("Judgement_Link") %>'
                                Target="_blank" OnClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"
                                AlternateText="Read online" />
                            <img id="img1" runat="server" width="14" height="14" alt="External Link Image" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                           <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'>
                           </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <%--  <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />--%>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
