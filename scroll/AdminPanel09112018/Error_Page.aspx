<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error_Page.aspx.cs" Inherits="Auth_AdminPanel_Error_Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Panel - Error Page</title>
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
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
                        <td class="login-new-bg" valign="top">
                            <table width="98%" border="0" align="left" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="login-heading">
                                        Error
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="98%"border="0" cellspacing="0" cellpadding="0" width="475px">
                                            <tr>
                                                <td align="center" style="text-align: center; font-size:100%;" class="login-text">
                                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    
                                                    <div align="left">
                                                        <strong>It seems that the page you were looking for has been moved or is no longer there. Or maybe you just mistyped something. Please check to make sure you've typed the URL correctly.
                                                    <br /><br />
                                                        You could return to the <a href="Login.aspx">Login Page</a><%--, or return 
                                                        <a style="cursor: pointer;" onClick="javascript:history.back(); return false;">Back</a>
                                                        <asp:LinkButton ID="lbBack" runat="server" OnClientClick="javascript:history.back(); return false;" >Back</asp:LinkButton> 
                                                        to the page.--%></strong>
                                                    </div>
                                                    <%-- <asp:Button ID="btnSubmit" runat="server" Text="Go to Login Page" 
                                CssClass="button" onclick="btnSubmit_Click" />--%>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td align="center" class="login-text">
                                                Go to
                                                <asp:LinkButton ID="LnkLogin" runat="server" Style="color: #459300;" CausesValidation="false" 
                                                        onclick="LnkLogin_Click">Login Page</asp:LinkButton>
                                                </td>
                                            </tr>--%>
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
