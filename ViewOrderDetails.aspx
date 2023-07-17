<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewOrderDetails.aspx.cs"
    Inherits="ViewOrderDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Order Details</title>
    <link type="text/css" rel="stylesheet" href="css/layout.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PGridDetails" runat="server">
            <asp:GridView ID="gvDailyOrders" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                OnRowDataBound="gvDailyOrders_RowDataBound" Caption="Order Details" DataKeyNames="OrderId"
                Width="100%" CssClass="more_details" GridLines="None" ToolTip="View Order Details" summary="This table show Order details">
                <Columns>
                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:HiddenField ID="hidDate" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Orderdateconvert")%>' />
                            <asp:Label ID="lblOrderDate" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "OrderDate")%>'>
                            </asp:Label>
                            <asp:Literal ID="ltrlFinality" runat="server" Visible="false">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:BoundField HeaderText="Date" DataField="OrderDate" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />--%>
                    <asp:TemplateField HeaderText="Petition/RA Number" ItemStyle-HorizontalAlign="Center"
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
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlaceHolderFive")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:BoundField HeaderText="Remarks" DataField="PlaceHolderFive" />--%>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
