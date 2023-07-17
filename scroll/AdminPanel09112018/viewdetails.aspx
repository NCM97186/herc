<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="viewdetails.aspx.cs" Inherits="AdminPanel_viewdetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Details Page</title>

    <script type="text/javascript" language="javascript">
  
function closeIt() {
  close();
}
    </script>

    <style type="text/css">
        .style1
        {
            width: 185px;
        }
    </style>
</head>
<body style="background-color: White; background-image:none;">
    <form id="form1" runat="server" style="background-color: White;">
    <div style="text-align: center">
        <asp:Panel ID="PnlUserDetails" runat="server">
            <table>
                <tr>
                    <td class="style1">
                        <strong>&nbsp;Name:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblName" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Address:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblAddress" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>City:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblCity" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Country:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblCountry" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Email:</strong>
                    </td>
                    <td class="text_padding">
                        <asp:Label ID="LblEmail" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="text_padding">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Mobile No.:</strong>
                    </td>
                    <td class="text_padding">
                        <asp:Label ID="LblMobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="text_padding">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Role Name:</strong>
                    </td>
                    <td class="text_padding">
                        <asp:Label ID="LblRole" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
       <%-- <input type="button" value="Close Window" onclick="closeIt()" class="button" />--%>
    </div>
    <div>
    <div style="text-align: center">
        <asp:Panel ID="PnlModule" runat="server">
            <table>
                <tr>
                    <td class="style1">
                        <strong>&nbsp;Title:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblTitle" runat="server">
                        </asp:Label>
                    </td>
                </tr>
               
              <%--  <tr>
                    <td class="style1">
                        <strong>Description:</strong>
                    </td>
                    <td>
                        <asp:Label ID="Lbldescription" runat="server">
                        </asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
               <%-- <tr>
                    <td class="style1">
                        <strong>Image:</strong>
                    </td>
                    <td>
                        <asp:Image ID="ImgName" runat="server" Height="150px" Width="150px"  />
                   
                    </td>
                </tr>--%>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Download:</strong>
                    </td>
                    <td>
                        <asp:Label ID="LblFileName" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Start Date:</strong>
                    </td>
                    <td class="text_padding">
                        <asp:Label ID="LblStartDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="text_padding">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>End Date:</strong>
                    </td>
                    <td class="text_padding">
                        <asp:Label ID="LblEndDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="text_padding">
                        &nbsp;
                    </td>
                </tr>
           
            </table>
        </asp:Panel>
        <input type="button" value="Close Window" onclick="closeIt()" class="button" />
    </div>
    </div>
    </form>
</body>
</html>
