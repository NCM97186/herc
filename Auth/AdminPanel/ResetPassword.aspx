<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Auth_AdminPanel_ResetPassword" %>

<%@ Register Assembly="ncmcaptcha" Namespace="ncmcaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />
</head>
<script src="js/sha512.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    function getPass() {
        var exp =/((?=.*\d)(?=.*[a-z])(?=.*[@#$&]).{8,15})/;
        var value = document.getElementById('<%=txtNewPass.ClientID%>').value;
        if (value == '') {
            alert('Please Enter user name and password');
            return false;
        }
        if (value.search(exp) == -1) {
            alert("The password should be between 8 to 15 Characters. The password must have at least one alphabet, one digit and one special character (@#$&).");
            return false;
        }


        var salt = '<%=Session["salt"]%>';
        var value = document.getElementById('<%=txtNewPass.ClientID%>').value;

        var hash = hex_sha512(value);
        document.getElementById('<%=txtNewPass.ClientID %>').value = hash;

        var value2 = document.getElementById('<%=txtConfirmPass.ClientID%>').value;
        var hash2 = hex_sha512(value2);

        document.getElementById('<%=txtConfirmPass.ClientID %>').value = hash2;

    }
    function tablemain_onclick() {

    }

</script>
<body>
    <form id="form1" runat="server"  autocomplete="off">
    <noscript>
        <!-- Show a notification if the user has disabled javascript -->
        <div>
            <div class="JavaScriptMessage" align="center">
                Javascript is disabled or is not supported by your browser. Please enable browsers
                java script....
            </div>
        </div>
    </noscript>
     <div id="wrapper-login">
        <div id="container">
            <div class="login-header">
                <div class="left-title">
                    SWAN
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
                                    <td class="login-heading">
                                        Reset Password
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlReset" runat="server" DefaultButton="BtnSubmit">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td colspan="2" align="center" style="text-align: center">
                                                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="style3">
                                                        New Password:
                                                    </td>
                                                    <td width="62%">
                                                        <label for="txtNewPass">
                                                        </label>
                                                        <asp:TextBox ID="txtNewPass" runat="server" CssClass="login-textfield" TextMode="Password"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Password"
                                                            ControlToValidate="txtNewPass" ValidationGroup="Reset"></asp:RequiredFieldValidator>--%>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewPass"
                                                            ErrorMessage="The password should be between 8 to 15 Characters. The password must have at least one alphabet, one digit and one special character (@#$&)."
                                                            ValidationExpression="(?=^.{6,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$">
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td align="right" valign="top" class="style6">
                                                        <%--style1--%>
                                                        Confirm Password:
                                                    </td>
                                                    <td width="62%" class="style2">
                                                        <label for="txtConfirmPass">
                                                        </label>
                                                        <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="login-textfield" TextMode="Password"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter confirm password"
                                                            ControlToValidate="txtConfirmPass" ValidationGroup="Reset"></asp:RequiredFieldValidator>--%>
                                                        <%--  <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtConfirmPass" ControlToCompare="txtNewPass"
                                                            Display="Dynamic" Type="String" Operator="Equal" Text="Confirm Password did not match. Please enter correct password for confirmation."
                                                            runat="Server" ValidationGroup="Reset"  />--%><br />
                                                        <asp:CompareValidator ID="CompareValidator1" CssClass="errormesg2" runat="server"
                                                            ControlToCompare="txtNewPass" ControlToValidate="txtConfirmPass" ErrorMessage="Password did not match.">
                                                        </asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="style4">
                                                    </td>
                                                    <td colspan="2" style="height: 53px">
                                                        <cc1:CaptchaControl ID="CaptchaControl1" runat="server" RefreshImageURL="images/images.jpeg"
                                                            CaptchaWidth="170" Width="277px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="font-size: 14px; font-style: normal; color: #5F5F5F;" class="style5">
                                                        <asp:Label ID="Label5" runat="server" Text="">Enter code<%--<span class="redtext">*</span>--%>:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaptcha" runat="server" Width="179px" CssClass="text-input"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rqtxtCaptcha" runat="server" ControlToValidate="txtCaptcha"
                                                            Display="Dynamic" ErrorMessage="Enter Verification Code" SetFocusOnError="True"
                                                            ValidationGroup="Forgot" CssClass="errormesg2"></asp:RequiredFieldValidator><br />
                                                        <asp:Label ID="Label2" runat="server" CssClass="errormesg2" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnSubmit" runat="server" CssClass="button" Text="Submit" OnClientClick="javascript:return getPass();"
                                                            ValidationGroup="Reset" OnClick="BtnSubmit_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlwelcome" runat="server">
                                            <table border="0" cellspacing="0" cellpadding="0" align="center">
                                                <tr>
                                                    <td height="13">
                                                    </td>
                                                </tr>
                                                <tr class="content-main">
                                                    <td align="center">
                                                        Your password reset link has been expired.<br />
                                                        You may now proceed to <a href="<%=ResolveUrl("~/AdminPanel/Forgot_Password.aspx")%>">
                                                            <span style="font-weight: bold; color: #FF6501; font-style: normal;">Forgot Password</span></a>
                                                        to send password reset link again.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="13">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
