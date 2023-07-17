<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailsPage.aspx.cs" Inherits="DetailsPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
     <title>Details Page</title>

		<link type="text/css" rel="stylesheet" href="css/layout.css">

    <script type="text/javascript" language="javascript">

        function closeIt() {
            close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="PnlPublicNotice" runat="server"  Visible="false">
            <table width="90%" cellpadding="0" cellspacing="0" class="more_details" align="center";>
         
                <tr>
                    <th> 
                    <strong>Title:</strong>
                     
                    </th>
                    <th>
                        <asp:Label ID="lblPublicNoticeTitle" runat="server">
                        </asp:Label>
                    </th>
                </tr>
                
                <tr id="TrDes" runat="server" >
                    <td >	
                        <strong>Description:</strong>
                    </td>
                    <td>
                        <asp:Label ID="lblPublicNoticeDesc" runat="server">
                        </asp:Label>
                    </td>
                </tr>
               
                
                <tr id="trFileName" runat="server" visible="false">
                    <td >
                        <strong>Download:</strong>
                    </td>
                    <td>
                     <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                       <%-- <asp:Label ID="lblPublicNoticeFile" runat="server">
                        </asp:Label>--%>
                    </td>
                </tr>
                   <tr id="trremarks" runat="server">
                    <td >
                        <strong>Remarks:</strong>
                    </td>
                    <td>
                     <asp:Label ID="lblRemarks" runat="server">
                        </asp:Label>
                    </td>
                </tr>

            <%--   <tr>
                    <td colspan="2"  align="right">
                           
                    </td>
                </tr>--%>
           
            </table>
            
        </asp:Panel>
        <asp:GridView ID="gvPubNotice" runat="server" AutoGenerateColumns="false" Visible="false" 
   
                    UseAccessibleHeader="true" Width="90%" GridLines="None" CssClass="more_details" OnRowDataBound="gvPubNotice_RowDataBound">
                    <Columns>
                    
                         <asp:BoundField DataField="ID" />
                          <asp:TemplateField HeaderText="Petitioner Name" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Respondent Name" HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                       <%-- <asp:BoundField HeaderText="Petitioner Name" DataField="Petitioner_Name" />
                        <asp:BoundField HeaderText="Respondent Name" DataField="Respondent_Name" />--%>
           
                     
                    </Columns>
                </asp:GridView><br />
               
                
        
        <asp:Panel ID="PGridDetails" runat="server" Visible="false">
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCommand="gvOrders_RowCommand" OnRowDataBound="gvOrders_RowDataBound"
                    Width="100%" DataKeyNames="OrderId"   CssClass="more_details" GridLines="None">
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataField="OrderDate" />
                        <asp:TemplateField HeaderText="PRO/RA Number" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblnumber" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Petitioner_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Respondent_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OrderTitle") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
               
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                            
                                <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("OrderFile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                            
                               <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        
        </asp:Panel>
         <p align="center">
                <input type="button" value="Close Window" onclick="closeIt()" />   
                </p>
    </div>
    </form>
</body>
</html>
