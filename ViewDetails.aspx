<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDetails.aspx.cs" Inherits="ViewDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Details</title>
    <link type="text/css" rel="stylesheet" href="css/layout.css">
    <script type="text/javascript" language="javascript">

        function closeIt() {
            close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PGridDetails" runat="server">
        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" OnRowCommand="gvDetails_RowCommand"
            Caption="PETITION DETAILS" DataKeyNames="Petition_id" OnRowDataBound="gvDetails_RowDataBound"
            Width="90%" CssClass="more_details" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="Petition Number" DataField="PRO_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="Petition_Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center"  ItemStyle-CssClass="Text-Center" >
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile1" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Petition_File") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="Petition_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Orders including Interim Orders" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                        <%--  <asp:Label ID="lblcat" runat="server"></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Public Notices
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublicNotice" runat="server" Visible="false" Text="No" />
                        <asp:HiddenField ID="hidPublicNotice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "connectedPublicNotice")%>' />
                        <asp:LinkButton ID="lnkConnectedPublicNotice" runat="server" ToolTip="Click here to view details"
                            CommandName="connectedPublic" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Petition_Id")%>'>
                        </asp:LinkButton>
                        <asp:Literal ID="ltrlConnectedPublicNotic" runat="server" Text="Yes" Visible="false">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Schedule of Hearing" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%--<asp:Literal ID="ltrlConnectedSohFile" runat="server"></asp:Literal>--%>
                        <asp:Label ID="lblConnectedSoh_ID" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkConnectedSoh" runat="server" ToolTip="Click here to view details"
                            CommandName="connectedSoh" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Petition_ID")%>'></asp:LinkButton>
                        <asp:Literal ID="ltrlConnectedSoh" runat="server" Text="Yes" Visible="false">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Public Notice" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedPublicNotice" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center"
                    Visible="false">
                    <HeaderTemplate>
                        Review(Y/N)
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPetition_ID" runat="server" Text='<%#Eval("Petition_Id")%>' Visible="false" />
                        <asp:Label ID="lblReview" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Review")%>'></asp:Label>
                        <asp:LinkButton ID="lnkReview" ToolTip="Click here to view details" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "Review")%>' CommandName="Review"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Petition_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Connected Petition(s)(Y/N)
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblConnectedPetition_ID" runat="server" Visible="false" Text="No" />
                        <asp:Label ID="lblConnectedPetition_ID1" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkConnectedPetition" runat="server" ToolTip="Click here to view details"
                            Text='<%# DataBinder.Eval(Container.DataItem, "connectedPetition")%>' CommandName="connected"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "connectedPetition") + ";" + DataBinder.Eval(Container.DataItem, "connectionID")%>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkpetition" runat="server" ToolTip="Click here to view details"
                            Text='<%#Eval("Petition_Id")%>' CommandName="connectedPetition" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Petition_Id")%>'
                            Visible="false"></asp:LinkButton>
                        <asp:Literal ID="ltrlConnectedPetition" runat="server" Text="Yes" Visible="false">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlPublic" runat="server" Visible="false">
        <asp:GridView ID="gvPubNotice" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPubNotice_RowCommand"
            OnRowDataBound="gvPubNotice_RowDataBound" UseAccessibleHeader="true" Width="90%"
            GridLines="None" CssClass="more_details" Caption="PUBLIC NOTICE DETAILS" DataKeyNames="PublicNoticeID">
            <Columns>
                <asp:BoundField HeaderText="Date" DataField="Start_Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:TemplateField HeaderText="Title" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkTitle" runat="server" ToolTip="Click here to view details"
                            Text='<%#Eval("Title") %>' CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PublicNoticeID") %>'></asp:LinkButton>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" Visible="false" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" >
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFilePublic" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("PlaceHolderFive")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlSOH" runat="server" Visible="false">
        <asp:GridView ID="gvSOH" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
            DataKeyNames="Soh_ID" Width="90%" OnRowCommand="gvSOH_RowCommand" OnRowDataBound="gvSOH_RowDataBound"
            CssClass="more_details" GridLines="None" Caption="SCHEDULE OF HEARING DETAILS">
            <Columns>
                <asp:BoundField HeaderText="Date & Time" DataField="Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Petition Number" DataField="PRO_No" Visible="false" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Petitioner(s)" DataField="Petitioner_Name" Visible="false" />
                <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" Visible="false" />
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject4" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Venue" DataField="Venue" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" >
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFileSOHDetails" runat="server"></asp:Literal>
                      
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarksSoh" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pPetitionGridDetatils" runat="server" Visible="false">
        <asp:GridView ID="gvPetition" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
            OnRowCommand="gvPetition_RowCommand" OnRowDataBound="gvPetition_RowDataBound"
            GridLines="None" CssClass="more_details" Caption="CONNECTED PETITIONS DETAILS"
            DataKeyNames="Petition_id" Width="90%">
            <Columns>
                <asp:BoundField HeaderText="Petition Number" DataField="PRO_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="Petition_Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile2" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Petition_File") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="PetitionStatusName" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlReview" runat="server">
        <table id="TblConnectedOrderWithREview" runat="server" width="90%" cellpadding="0"
            cellspacing="0" class="more_details" align="center" visible="false">
            <tr id="tr2" runat="server">
                <th colspan="2" class="caption" style="background: white !important">
                    REVIEW PETITION DETAILS
                </th>
            </tr>
            <tr id="tr1" runat="server">
                <td style="background-color: White !important;">
                    <strong>Review petition Against Final Order/Interim Order</strong>
                </td>
                <td style="background-color: White !important;">
                    <%--Intrim/Final Order dated Desc.--%>
                    <asp:LinkButton ID="lnkReviewPetitionConnectedOrder" runat="server" OnClick="lnkReviewPetitionConnectedOrder_Click"
                        ToolTip="Click here to view details"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReview" runat="server" DataKeyNames="RP_Id" AutoGenerateColumns="false"
            Visible="false" GridLines="None" OnRowCommand="gvReview_RowCommand" OnRowDataBound="gvReview_RowDataBound"
            Width="90%" CssClass="more_details">
            <Columns>
                <asp:BoundField HeaderText="RA No" DataField="RP_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="ReviewpetitionDate" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblApplicant" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Applicant_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" >
                    <ItemTemplate>
                        <asp:Literal ID="ltrlrEVIEW" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 

                <asp:BoundField HeaderText="Status" DataField="RP_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Orders including Interim Orders" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlReviewConnectedFile" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Public Notices
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublicNoticeReview" runat="server" Visible="false" Text="No" />
                        <asp:HiddenField ID="hidPublicNoticeReview" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "connectedPublicNotice")%>' />
                        <asp:LinkButton ID="lnkConnectedPublicNoticeReview" runat="server" ToolTip="Click here to view details"
                            CommandName="connectedPublicReview" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'>
                        </asp:LinkButton>
                        <asp:Literal ID="ltrlConnectedRpPublicNotice" runat="server" Text="Yes" Visible="false">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Schedule of Hearing" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReviewConnectedSoh_ID" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkReviewConnectedSoh" runat="server" ToolTip="Click here to view details"
                            CommandName="ReviewconnectedSoh" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'></asp:LinkButton>
                        <asp:Literal ID="ltrlConnectedRpSoh" runat="server" Text="Yes" Visible="false">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center"
                    Visible="false">
                    <HeaderTemplate>
                        Appeal(Y/N)
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionID" runat="server" Text='<%#Eval("RP_Id")%>' Visible="false" />
                        <asp:Label ID="lblReview1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RP_Id")%>'></asp:Label>
                        <asp:LinkButton ID="lnkReview1" runat="server" ToolTip="Click here to view details"
                            Text='<%# DataBinder.Eval(Container.DataItem, "Appeal")%>' CommandName="Appeal"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Connected Review Petition(s)(Y/N)
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblConnectedReviewPetition_ID" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkConnectedReviewPetition" runat="server" ToolTip="Click here to view details"
                            Text='<%# DataBinder.Eval(Container.DataItem, "connectedPetition")%>' CommandName="connectedReview"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "connectedPetition") + ";" + DataBinder.Eval(Container.DataItem, "connectionID")%>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkReviewpetition" runat="server" ToolTip="Click here to view details"
                            Text='<%#Eval("RP_Id")%>' CommandName="connectedReviewPetition" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'
                            Visible="false"></asp:LinkButton>
                        <asp:Literal ID="ltrlConnectedRpPetition" runat="server" Text="Yes" Visible="false">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks1" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Connected petition(s)(Y/N)" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReviewConnectedPetitions" runat="server" Text="No" Visible="false" />
                        <asp:LinkButton ID="lnkReviewConnectedPetitions" runat="server" CommandName="ReviewconnectedPetition"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>' Text='<%# DataBinder.Eval(Container.DataItem, "RP_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <asp:GridView ID="gvDailyOrders" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
            OnRowDataBound="gvDailyOrders_RowDataBound" Caption="FINAL ORDER / INTERIM ORDER DETAILS AGAINST WHICH REVIEW PETITION HAS BEEN FILED"
            DataKeyNames="OrderId" Width="90%" CssClass="more_details" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="Date" DataField="OrderDate" ItemStyle-CssClass="Text-Center"
                    HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="PRO/RA Number" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblnumber" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-CssClass="Text-Center"
                    Visible="false" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitioner" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblsubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>'></asp:Label>
                        <asp:HiddenField ID="hydSubject" runat="server" Value='<%# Eval("OrderTitle")%>'
                            Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("OrderFile") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <div style="text-align: center">
        <asp:Panel ID="PnlModule" runat="server">
            <table width="90%" cellpadding="0" cellspacing="0" class="more_details" align="center">
                <tr>
                    <th>
                        Title:
                    </th>
                    <th>
                        <asp:Label ID="LblTitle" runat="server">
                        </asp:Label>
                    </th>
                </tr>
                <tr id="trDesc" runat="server" visible="false">
                    <td>
                        <strong>Description:</strong>
                    </td>
                    <td>
                        <asp:Label ID="Lbldescription" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr id="trdownload" runat="server">
                    <td>
                        <strong>Download:</strong>
                    </td>
                    <td>
                        <asp:Literal ID="ltrlConnectedFile" runat="server" Visible="false"></asp:Literal>
                        <asp:Literal ID="ltrNotification" runat="server" Visible="false"></asp:Literal>
                    </td>
                </tr>
                <tr id="trlastdate" runat="server" visible="false">
                    <td>
                        <strong>
                            <asp:Label ID="lbllastdate" runat="server"></asp:Label></strong>
                    </td>
                    <td>
                        <asp:Label ID="LblStartDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trhearingDate" runat="server" visible="false">
                    <td>
                        <strong>Date of Public Hearing:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblEndDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trvenu" runat="server" visible="false">
                    <td>
                        <strong>Venue:</strong>
                    </td>
                    <td>
                        <asp:Label ID="lblvenu" runat="server"></asp:Label>
                    </td>
                </tr>
                <%-- <tr>
                    <td colspan="2"  >
                            
                    </td>
                </tr>--%>
            </table>
            <p align="center">
                <input type="button" value="Close Window" onclick="closeIt()" />
            </p>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlreviewPetition" runat="server" Visible="false">
        <asp:GridView ID="gvReview1" runat="server" AutoGenerateColumns="false" DataKeyNames="Petition_id"
            OnRowDataBound="gvReview1_RowDataBound" Width="90%" CssClass="more_details" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="Petition Number" DataField="PRO_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="Petition_Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Petitioner(s)" DataField="Petitioner_Name" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Respondent" DataField="Respondent_Name" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download Petition" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%--<asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("Petition_File")%>'>
                                  
                                     <img src="<%=ResolveUrl("~/images/pdf-icon.jpg")%>" title="View Document" width="20" alt="View Document"
                                    height="20" />     
                        </asp:LinkButton>--%>
                        <asp:Literal ID="ltrlConnectedFile1" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Petition_File") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="Petition_Status" HeaderStyle-CssClass="Text-Center" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="GvReviewPetition" runat="server" AutoGenerateColumns="false" OnRowCommand="GvReviewPetition_RowCommand"
            Caption="REVIEW PETITION DETAILS" OnRowDataBound="GvReviewPetition_RowDataBound"
            Width="100%" GridLines="None" DataKeyNames="RP_Id" CssClass="more_details">
            <Columns>
                <asp:BoundField HeaderText="RA Number" DataField="RP_No" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="ReviewpetitionDate" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblApplicant" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Applicant_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--   <asp:BoundField HeaderText="Petitioner(s)" DataField="Applicant_Name" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent" HeaderStyle-CssClass="Text-Center" />--%>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orders including Interim Orders" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionID" runat="server" Text='<%#Eval("RP_Id")%>' Visible="false" />
                        <asp:Literal ID="ltrlReviewConnectedFile1" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Public Notices
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublicNoticeRv" runat="server" Visible="false" Text="No" />
                        <asp:HiddenField ID="hidPublicNoticeRv" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "connectedPublicNotice")%>' />
                        <asp:LinkButton ID="lnkConnectedPublicNoticeRv" runat="server" CommandName="connectedPublicRv"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Schedule of Hearing" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRvConnectedSoh_ID" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkRvConnectedSoh" runat="server" CommandName="RvconnectedSoh"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="RP_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Remark" DataField="PlaceholderTwo" HeaderStyle-CssClass="Text-Center" />
            </Columns>
        </asp:GridView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnlPublicNoticeReview" runat="server" Visible="false">
        <asp:GridView ID="gvPubNoticeReview" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPubNoticeReview_RowCommand"
            OnRowDataBound="gvPubNoticeReview_RowDataBound" UseAccessibleHeader="true" DataKeyNames="PublicNoticeID"
            Width="90%" GridLines="None" CssClass="more_details" Caption="REVIEW PETITION PUBLIC NOTICE DETAILS">
            <Columns>
                <asp:BoundField HeaderText="Date" DataField="Start_Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:TemplateField HeaderText="Title" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkTitleReview" runat="server" ToolTip="Click here to view details"
                            Text='<%#Eval("Title") %>' CommandName="ViewDetails1" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PublicNoticeID") %>'></asp:LinkButton>
                        <asp:Label ID="lblTitleReview" runat="server" Text='<%#Eval("Title") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" Visible="false" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDescReview" runat="server" Text='<%# Server.HtmlDecode((string)Eval("Description"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" >
                    <ItemTemplate>
                        <asp:Literal ID="ltrlFilePublicConnected" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarksReview" runat="server" Text='<%# Eval("PlaceHolderFive")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlReviewSoh" runat="server" Visible="false">
        <asp:GridView ID="gvSOHReview" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
            DataKeyNames="Soh_ID" Width="90%" OnRowCommand="gvSOHReview_RowCommand" OnRowDataBound="gvSOHReview_RowDataBound"
            CssClass="more_details" GridLines="None" Caption="REVIEW PETITION SCHEDULE OF HEARING DETAILS">
            <Columns>
                <asp:BoundField HeaderText="Date & Time" DataField="Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="RA No" DataField="RP_No" Visible="false" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Petitioner(s)" DataField="Petitioner_Name" Visible="false" />
                <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" Visible="false" />
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject1" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Venue" DataField="Venue" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" >
                    <ItemTemplate>
                        <asp:Literal ID="ltrlFileReviewConnected" runat="server"></asp:Literal>
                        <%-- <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("soh_file") %>' />--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Remarks")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />--%>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlAppeal" runat="server">
        <asp:GridView ID="grdAppeal" runat="server" AutoGenerateColumns="false" Visible="false"
            OnRowCommand="grdAppeal_RowCommand" OnRowDataBound="grdAppeal_RowDataBound" Width="90%"
            Caption="PETITION APPEAL DETAILS" CssClass="more_details" GridLines="None" DataKeyNames="PA_Id">
            <Columns>
                <asp:BoundField HeaderText="Appeal No" DataField="Appeal_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="AppealDate" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Where Appealed" DataField="Where_Appealed" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Status" DataField="PA_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Judgement Link" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="ImageButton1" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                            Text='<%#Eval("Judgement_Link") %>' Target="_blank" OnClientClick='<%# String.Format("javascript:return openTargetURL(\"{0}\")", Eval("Judgement_Link")) %>'
                            AlternateText="Read online" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedAppeal" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFileAppeal" runat="server" Value='<%#Eval("File_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlSohDetails" runat="server" Visible="false">
        <asp:GridView ID="GvSohDetails" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
            DataKeyNames="Soh_ID" Width="90%" OnRowCommand="GvSohDetails_RowCommand" OnRowDataBound="GvSohDetails_RowDataBound"
            CssClass="more_details" GridLines="None" Caption="SCHEDULE OF HEARING DETAILS">
            <Columns>
                <asp:BoundField HeaderText="Date & Time" DataField="Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petition/RA No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblnumber" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitioner" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondent" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject4" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Venue" DataField="Venue" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarksSohDetails" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
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
    <asp:Panel ID="pnlconnectedReview" runat="server" Visible="false">
        <asp:GridView ID="GrdReviewConnected" runat="server" DataKeyNames="RP_Id" AutoGenerateColumns="false"
            GridLines="None" Width="90%" CssClass="more_details" Caption="CONNECTED REVIEW PETITION DETAILS"
            OnRowDataBound="GrdReviewConnected_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="RA No" DataField="RP_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="ReviewpetitionDate" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblApplicant" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Applicant_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlrEVIEWConnected" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="RP_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlAppealDetails" runat="server" Visible="false">
        <table id="TblAppealOrder" runat="server" width="90%" cellpadding="0" cellspacing="0"
            class="more_details" align="center" visible="false">
            <tr id="tr3" runat="server">
                <th colspan="2" class="caption" style="background: white !important">
                    “ORDER UNDER APPEAL” DETAILS
                </th>
            </tr>
            <tr id="tr4" runat="server">
                <td style="background-color: White !important;">
                    <strong>Appeal against Final Order/Interim Order</strong>
                </td>
                <td style="background-color: White !important;">
                    <asp:LinkButton ID="LnkconnectedorderAppeal" runat="server" OnClick="LnkconnectedorderAppeal_Click"
                        ToolTip="Click here to view details"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:GridView ID="grdAppealDetails" runat="server" AutoGenerateColumns="false" OnRowCommand="grdAppealDetails_RowCommand"
            OnRowDataBound="grdAppealDetails_RowDataBound" CssClass="more_details" Width="90%"
            GridLines="None" DataKeyNames="PA_Id">
            <%--Caption="APPEAL DETAILS"--%>
            <Columns>
                <%--     <asp:BoundField HeaderText="Appeal Number" DataField="Appeal_No" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-CssClass="Text-Center" />--%>
                <asp:BoundField HeaderText="Date" DataField="AppealDate" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="AppealDate" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" Visible="false" />
                <asp:BoundField HeaderText="Ref No(Where Appealed)" DataField="AppealRefNo" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblApplicantAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Applicant_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondentAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Petition No" DataField="pronumber" HeaderStyle-CssClass="Text-Center"
                    Visible="false" />
                <%-- <asp:BoundField HeaderText="Where Appealed" DataField="Where_Appealed" HeaderStyle-CssClass="Text-Center" />--%>
                <asp:TemplateField HeaderText="Where Appealed" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem, "Where_Appealed"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="PA_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Judgement Link" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "JudgementDesc")%>
                        &nbsp;
                        <asp:HyperLink ID="Hypjudgement" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                            Text='<%# Eval("Judgement_Link") %>' Target="_blank" ToolTip="Click here to view details"
                            OnClick="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"
                            AlternateText="Read online" />
                        <img id="img1" runat="server" width="14" height="14" alt="External Link Image" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedAppealfiles" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />--%>
                <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--   <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Connected Review Petition(s)(Y/N)
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblConnectedReviewAppeal" runat="server" Visible="false" Text="No" />
                       
                        <asp:LinkButton ID="lnkConnectedReviewAppeal" runat="server" Text='<%#Eval("PA_Id")%>'
                            CommandName="connectedReviewAppeal" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "PA_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnlReviewConnectedPetition" runat="server" Visible="false">
        <asp:GridView ID="gvConnectedPetitionWithPetition" runat="server" AutoGenerateColumns="false"
            UseAccessibleHeader="true" OnRowCommand="gvConnectedPetitionWithPetition_RowCommand"
            OnRowDataBound="gvConnectedPetitionWithPetition_RowDataBound" GridLines="None"
            CssClass="more_details" Caption="CONNECTED PETITIONS DETAILS" DataKeyNames="Petition_id"
            Width="90%">
            <Columns>
                <asp:BoundField HeaderText="Petition Number" DataField="PRO_No" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="Petition_Date" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile2" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Petition_File") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="PetitionStatusName" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PAppealReview" runat="server">
        <asp:GridView ID="grdAppealReview" runat="server" AutoGenerateColumns="false" OnRowCommand="grdAppealReview_RowCommand"
            Caption="REVIEW PETITION DETAILS" OnRowDataBound="grdAppealReview_RowDataBound"
            Width="90%" GridLines="None" DataKeyNames="RP_Id" CssClass="more_details">
            <Columns>
                <asp:BoundField HeaderText="RA Number" DataField="RP_No" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="ReviewpetitionDate" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblApplicant" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Applicant_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orders including Interim Orders" Visible="false" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionID" runat="server" Text='<%#Eval("RP_Id")%>' Visible="false" />
                        <asp:Literal ID="ltrlReviewConnectedFile1" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Schedule of Hearing" HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRvConnectedSoh_ID" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkRvConnectedSoh" runat="server" CommandName="RvconnectedSoh"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RP_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Status" DataField="RP_Status" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Remark" DataField="PlaceholderTwo" HeaderStyle-CssClass="Text-Center" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PnlAppealOrderconnected" runat="server" Visible="false">
        <asp:GridView ID="GvAppealConnectedorder" runat="server" AutoGenerateColumns="false"
            UseAccessibleHeader="true" OnRowDataBound="GvAppealConnectedorder_RowDataBound"
            Caption="FINAL ORDER / INTERIM ORDER DETAILS AGAINST WHICH APPEAL PETITION HAS BEEN FILED"
            DataKeyNames="OrderId" Width="90%" CssClass="more_details" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="Date" DataField="OrderDate" ItemStyle-CssClass="Text-Center"
                    HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petition/RA Number" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblnumberAppeal" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-CssClass="Text-Center"
                    Visible="false" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Petitioner(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionerAppeal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent(s)" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondentAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblsubjectAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subject") %>'></asp:Label>
                        <asp:HiddenField ID="hydSubjectAppeal" runat="server" Value='<%# Eval("OrderTitle")%>'
                            Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFileAppeal" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFileAppeal" runat="server" Value='<%#Eval("OrderFile") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlWhatsNew" runat="server" Visible="false">
        <table width="90%" cellpadding="0" cellspacing="0" class="more_details" align="center">
            <tr>
                <th>
                    Title:
                </th>
                <th>
                    <asp:Label ID="lblWhatsnewTitle" runat="server">
                    </asp:Label>
                </th>
            </tr>
            <tr id="tr5" runat="server">
                <td>
                    <strong>Description:</strong>
                </td>
                <td>
                    <asp:Label ID="lblWhatsnewDesc" runat="server">
                    </asp:Label>
                </td>
            </tr>
         
            <tr id="trfile" runat="server" visible="false">
                <td>
                    <strong>Download:</strong>
                </td>
                <td>
                    <asp:Literal ID="ltrDownload" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
