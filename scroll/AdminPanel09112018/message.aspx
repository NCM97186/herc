<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="Auth_AdminPanel_message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
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
                        <td class="login-new-bg" align="center">
                            <asp:Label ID="lblMsg" runat="server" Style="color: Green; font-weight: bold"></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Go to Login Page" CssClass="button"
                                OnClick="btnSubmit_Click" />
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
