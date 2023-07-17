<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDetailsWhatsNew.aspx.cs" Inherits="Ombudsman_ViewDetailsWhatsNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Whats New details</title>
       <link type="text/css" rel="stylesheet" href="../css/layout.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="Grdaaward" DataKeyNames="Appeal_Id" runat="server" AutoGenerateColumns="false" CssClass="more_details"
                    UseAccessibleHeader="true" GridLines="None" Width="100%" OnRowDataBound="Grdaaward_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Ref No" DataField="RA_Id" Visible="false" />
                        <asp:BoundField HeaderText="Date" DataField="AwardDate" />
                        <asp:BoundField HeaderText="Appeal Date" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Appeal No">
                            <ItemTemplate>
                                <asp:Label ID="lnlappealno" runat="server" Text='<%#Eval("AppealNumber") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Applicant(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%# Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Respondent_Name")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Subject" DataField="PlaceholderTwo" />
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedAwardProunced" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
               
    </div>
    </form>
</body>
</html>
