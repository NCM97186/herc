<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Auth_AdminPanel_ForgotPassword" %>

<%@ Register Assembly="ncmcaptcha" Namespace="ncmcaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password: HERC</title>
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />

    <script src="js/sha512.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function getPass() {

            var value1 = document.getElementById('<%=txtUserName.ClientID%>').value;
            var value2 = document.getElementById('<%=txtCaptcha.ClientID%>').value;
            var exp = /((?=.*\d)(?=.*[a-z])(?=.*[@#$&]).{8,15})/;
            var exp1 = /^[A-Za-z0-9._]{5,25}$/;
            var exp3 = /(^[\s\S]{0,5}$)/;
            var exp4 = /(^[0-9a-zA-Z ]+$)/;
            if (value1 == '') {
                alert("Please enter username");
                return false;
            }
          
            if (value2 == '') {
                alert('Please Enter Captcha Code');
                return false;
            }



            if (value2.search(exp3) == -1) {
                alert("Maximum 5 characters required.");
                return false;
            }
            if (value2.search(exp4) == -1) {
                alert("Don't use any special characters in captcha");
                return false;
            }

         

        }
        function tablemain_onclick() {

        }

</script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin" autocomplete="off">
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
                                        Forgot Password
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
                                                <td width="38%" align="right" class="login-text">
                                                    <label for="txtUserName">
                                                        User Name:
                                                    </label>
                                                </td>
                                                <td width="62%">
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="login-textfield"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RevName" runat="server" ErrorMessage="*" ControlToValidate="txtUserName"
                                                        ValidationGroup="add" ValidationExpression="[A-Za-z][A-Za-z0-9._]{1,50}"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <cc1:CaptchaControl ID="CaptchaControl1" runat="server" RefreshImageURL="images/images.jpeg"
                                                        CaptchaWidth="170" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <%--<td align="center"><span  class="login-text2">*Enter Above Characters being displayed in above image.</span></td>--%>
                                            </tr>
                                            <tr>
                                                <td width="38%" align="right" class="login-text">
                                                    <label for="txtCaptcha">
                                                        Please enter the code shown above:</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="login-textfield" Width="179px"
                                                        MaxLength="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="Submit" OnClientClick="javascript:return getPass();"
                                                        OnClick="btnLogin_Click" />
                                                    <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" 
                                                        OnClick="btnBack_Click" />
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
