<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="signout" %>
<%@ Register Assembly="ncmcaptcha" Namespace="ncmcaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logout</title>
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />

</head>
<body id="login" style="background: White; color: black;">
<form id="form1" runat="server" >
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
                        <td class="login-new-bg1">
                            <b>You have been successfully Logged Out!!</b>
                            <br />
                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Auth/AdminPanel/Login.aspx"><< Back to Login Page</asp:HyperLink>
                <a class="password" href="Login.aspx"></a>
                        </td>
                    </tr>
                </table>
            </div>
            <!--main content ends here-->
        </div>
    </div>
    <%--<div id="login-wrapper" class="png_bg">
        <div id="login-top">
        </div>
        <div id="login-content">
            <form id="form1" runat="server">
            <div class="clear">
            </div>
            <p style="font-size: 15px;">
                <asp:Label ID="Lblmsg1" runat="server" Text=""><b>You have been successfully Logged Out!!</b></asp:Label>
            </p>
            <div class="clear">
            </div>
            <br />
            <p>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Auth/AdminPanel/Login.aspx"><< Back to Login Page</asp:HyperLink>
                <a class="password" href="Login.aspx"></a>
            </p>
            <div class="clear">
            </div>
            </form>
        </div>
    </div>--%>
    </form>
</body>
</html>
