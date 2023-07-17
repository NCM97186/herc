<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AwardsPronouncedDetails.aspx.cs"
    Inherits="Ombudsman_AwardsPronouncedDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Awards Pronounced Details</title>
    <link type="text/css" rel="stylesheet" href="../css/layout.css">
</head>
<body>
    <form id="form1" runat="server">
    <div id="pappeal" runat="server">
        <%--<asp:GridView ID="grdappeal" EnableViewState="true" AutoGenerateColumns="false" runat="server"
            CssClass="more_details" Caption="AWARDS UNDER APPEAL DETAILS">
            <EmptyDataTemplate>
                No Record Found
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Date of Award" DataField="AwardDate" />
                <asp:BoundField HeaderText="Appeal No" DataField="Appeal_Number" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" />
                <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" />
                <asp:BoundField HeaderText="Subject" DataField='Subject' />
                <asp:BoundField HeaderText="Where Appealed" DataField="whap" />
                <asp:TemplateField HeaderText="Judgement Link">
                    <ItemTemplate>
                        <asp:HyperLink ID="ImageButton1" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                        
                            Text='<%#Eval("Judgement_Link") %>' Target="_blank" OnClientClick='<%# String.Format("javascript:return openTargetURL(\"{0}\")", Eval("Judgement_Link")) %>'
                            AlternateText="Read online" />
                    </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>
        </asp:GridView>--%>
        <asp:GridView ID="grdappealDetails" DataKeyNames="Appeal_Id" runat="server" AutoGenerateColumns="false"
            UseAccessibleHeader="true" GridLines="None" CssClass="more_details" OnRowDataBound="grdappealDetails_RowDataBound"
            OnRowCommand="grdappealDetails_RowCommand" Caption="APPEAL DETAILS" Width="100%" ToolTip="View Appeal" summary="This table show all appeal records">
            <Columns>
                <asp:BoundField HeaderText="Appeal No" DataField="Appeal_Number" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="Appeal_Date" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Subject" DataField="Subject" HeaderStyle-CssClass="Text-Center" />
                <asp:BoundField HeaderText="Status" DataField="AppealStatusName" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Schedule Of Hearing" HeaderStyle-CssClass="Text-Center"  ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblConnectedSoh_ID" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkConnectedSoh" runat="server" CommandName="connectedSoh" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Appeal_Id")%>'></asp:LinkButton>
                        <asp:HiddenField ID="hyappealId" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedAwardProunced" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="Text-Center" />
                <asp:TemplateField HeaderText="Award under appeal" HeaderStyle-CssClass="Text-Center"  ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblAwardunderappeal" runat="server" Visible="false" Text="No" />
                        <asp:LinkButton ID="lnkAwardunderappeal" runat="server" CommandName="connectedAwardunderappeal"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Appeal_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="grdappeal" EnableViewState="true" AutoGenerateColumns="false" runat="server"
            UseAccessibleHeader="true" GridLines="None" CssClass="more_details" OnRowDataBound="grdappeal_RowDataBound"
            Caption="AWARDS UNDER APPEAL DETAILS" Width="100%">
            <EmptyDataTemplate>
                No Record Found
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Date" DataField='AwardDate' />
                <asp:TemplateField HeaderText="Appeal No">
                    <ItemTemplate>
                        <asp:Label ID="lnlappealno" runat="server" Text='<%#Eval("AppealNumber") %>' />
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText='Subject' DataField='Description' />
                <asp:BoundField HeaderText='Where_Appealed' DataField='Where_Appealed' />
              <%--  <asp:BoundField HeaderText='Status' DataField='Award_StatusID' />--%>
                <asp:TemplateField HeaderText="Judgement Link">
                    <ItemTemplate>

                 <asp:Label ID="Description" runat="server" Text='<%#Eval("Description") %>'/>
                         
                                <asp:HyperLink ID="Hypjudgement" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>' Text='<%# Eval("Judgement_Link") %>'
                                 Target="_blank" OnClick ="javascript:return confirm( 'This link shall take you to a webpage outside HERC website. HERC is not responsible for the contents and reliability of the linked websites and does not necessarily endorse the views expressed in them. For details you may refer to Hyperlinking Policy under Website Policies. Click OK to continue. Click Cancel to stop.');"
                                AlternateText="Read online" />
                                <img id="img1" runat="server"  width="14" height="14"   alt="External Link Image" />



                      <%--  <asp:HyperLink ID="ImageButton1" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                            Text='<%#Eval("Judgement_Link") %>' Target="_blank" OnClientClick='<%# String.Format("javascript:return openTargetURL(\"{0}\")", Eval("Judgement_Link")) %>'
                            AlternateText="Read online" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedAwardProunced" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText='Remarks' DataField='Remarks' />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
