<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmEmail.aspx.cs" Inherits="Auth_AdminPanel_ConfirmEmail" %>
<%@ Register Assembly="ncmcaptcha" Namespace="ncmcaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="css/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/menustyle.css" rel="stylesheet" type="text/css" />

    <script src="js/sha512.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnEmail" autocomplete="on">
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
                                    <td class="login-heading">
                                        Confirm Your Email
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlEmail" runat="server" DefaultButton="btnEmail">
                                            <table width="98%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td colspan="2" align="center" style="text-align: center">
                                                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="38%" align="right" valign="top" class="login-text">
                                                        Email address:
                                                    </td>
                                                    <td width="62%">
                                                        <label for="textfield">
                                                        </label>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="login-textfield"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Email address"
                                                            ControlToValidate="txtEmail" ValidationGroup="Email"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left">
                                                        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                                            Tip:-abc@gmail.com</span></em>
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
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="login-textfield" Width="179px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnEmail" runat="server" CssClass="button" Text="Submit" ValidationGroup="Email"
                                                            OnClick="btnEmail_Click" />
                                                        <asp:Button ID="btnMyMail" runat="server" Text="confirm mail" OnClick="btnMyMail_Click" Visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="center" onclick="btnMyMail_Click">
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
                                                        You may now proceed to <a href="<%=ResolveUrl("~/Auth/AdminPanel/ForgotPassword.aspx")%>">
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
