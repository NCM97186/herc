<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="AdminPanel_AdminLogin" %>

<%@ Register Assembly="ncmcaptcha" Namespace="ncmcaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Admin Login: HERC  </title>
    <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />

    <script src="js/sha512.js" type="text/javascript"></script>
<script type="text/javascript">
        function DisableBackButton() 
        {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
 </script>
</head>

<script type="text/javascript" language="javascript">
    function getPass() {


        var exp = /((?=.*\d)(?=.*[a-z])(?=.*[@#$&]).{8,15})/;
        var exp1 = /^[A-Za-z0-9._]{5,25}$/;
        var exp3 = /(^[\s\S]{0,5}$)/;
        var exp4 = /(^[0-9a-zA-Z ]+$)/;
        var value = document.getElementById('txtPwd').value;
        var value1 = document.getElementById('<%=txtUserName.ClientID%>').value;
        var value2 = document.getElementById('<%=txtCaptcha.ClientID%>').value;

        if (value1 == '') {
            alert("Please enter username");
            return false;
        }
        if (value1.search(exp1) == -1) {
            alert("Don't use any special characters except . and _  in username");
            
            var value2 = document.getElementById('<%=txtCaptcha.ClientID%>').value = "";
            return false;
        }
        if (value == '') {
            alert('Please enter password');
            return false;
        }
        if (value.search(exp) == -1) {
            alert("Incorrect username or password.");
            document.getElementById('<%=txtUserName.ClientID%>').value = "";
            document.getElementById('txtPwd').value = "";
            document.getElementById('<%=txtUserName.ClientID%>').value;
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
            document.getElementById('<%=txtUserName.ClientID%>').value = "";
            document.getElementById('txtPwd').value = "";
            document.getElementById('<%=txtUserName.ClientID%>').value;
            return false;
        }

        var salt = '<%=Session["salt"]%>';
        var hash = hex_sha512(hex_sha512(value) + salt);

        document.getElementById('txtPwd').value = hash;
        document.getElementById('<%=hfpwd.ClientID%>').value = hash;

    }
    function tablemain_onclick() {

    }

</script>

<body onload="disableBackButton()">
    <form id="form1" runat="server" defaultbutton="btnLogin" autocomplete="off">
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
                                        Login
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
                                                    User Name:</label> 
                                                </td>
                                                <td width="62%">
                                                    
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="login-textfield" MaxLength="20"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RevName" runat="server" ErrorMessage="*" ControlToValidate="txtUserName"
                                                        ValidationGroup="add" ValidationExpression="[A-Za-z][A-Za-z0-9._]{1,50}"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="login-text">
                                                    <label for="txtPwd">
                                                    Password:</label> 
                                                </td>
                                                <td>
                                                    <%-- <asp:TextBox ID="txtPwd" runat="server" CssClass="login-textfield" TextMode="Password"></asp:TextBox>--%>
                                                    <input id="txtPwd" class="login-textfield" type="password" MaxLength="20"/>
                                                    <asp:HiddenField ID="hfpwd" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <cc1:CaptchaControl ID="CaptchaControl1" runat="server" RefreshImageURL="images/images.jpeg"
                                                        CaptchaWidth="170" SoundImageURL="  " />
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
                                                    <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="Sign In" OnClientClick="javascript:return getPass();"
                                                        OnClick="btnLogin_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="center">
                                                    <asp:LinkButton ID="LnkFrt" runat="server" CausesValidation="false" OnClick="LnkFrt_Click">Forgot Password</asp:LinkButton>
                                                    &nbsp;
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
