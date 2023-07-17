<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmationPageAdmin.aspx.cs"
    Inherits="Auth_AdminPanel_ConfirmationPageAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Confirmation Page: Admin Panel</title>
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />
    <script src="js/sha512.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnBack">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="wrapper-login">
        <div id="container">
            <div class="login-header">
                <div class="left-title">
                    HERC
                </div>
                <div class="right-title">
                    ADMIN PANEL</div>
            </div>
            <!--main content starts here-->
            <div class="main_content-login">
                <table width="481" border="0" align="center" cellpadding="0" cellspacing="0" class="tablemain"
                    language="javascript" onclick="return tablemain_onclick()">
                    <tr>
                        <td class="login-new-bg">
                            <table width="98%" border="0" align="left" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="login-heading">
                                        Confirmation
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="right" class="login-text">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" style="text-align: center">
                                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <%--<td align="center"><span  class="login-text2">*Enter Above Characters being displayed in above image.</span></td>--%>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <p align="center">
                                                        <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click"
                                                            OnClientClick="return getPass();" />
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <!--main content ends here-->
        </div>
    </div>
    </form>
</body>
</html>
