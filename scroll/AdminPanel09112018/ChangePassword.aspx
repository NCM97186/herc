<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Change_Password"
    Title="Welcome To Admin Panel" %>

<%@ Register Assembly="ncmcaptcha" Namespace="ncmcaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="js/sha512.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function getPass() {
            
            var phoneexp = /^\d{10,15}$/;
            var exp = /((?=.*\d)(?=.*[a-z])(?=.*[@#$&]).{8,15})/;
            var expEmail = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            var value1 = document.getElementById('txtPwd').value;
            var valueOldPwd = document.getElementById('OldPassword').value;
            var value4 = document.getElementById('txtConfirmPassword').value;
           
            if (valueOldPwd == "") {
                alert("please enter old password");
                return false;
            }
          
            if (value1 == '') {
                alert("Please enter password.");
                return false;
            }
            if (value1.search(exp) == -1) {

                alert("Use min 8-15 Characters.The password must have at least one alphabets, one digit and one special character(@#$&).");

                return false;
            }
            if (value4 == '') {
                alert("Please enter confirm password.");
                return false;
            }
            var hash = hex_sha512(value1);
            var hash1 = hex_sha512(value4);
            var hash3 = hex_sha512(valueOldPwd);
           

            
            document.getElementById('OldPassword').value = hash3;
            if (hash != hash1) {
                document.getElementById('txtConfirmPassword').value = '';
                document.getElementById('txtPwd').value = '';
               document.getElementById('OldPassword.ClientID').value = '';
                alert("Password is not matched.");

                return false;
            }
            else {

                document.getElementById('<%=hfpwd.ClientID%>').value = hash;
                document.getElementById('<%=hidOldPassword.ClientID%>').value = hash3;
                return true;
            }
        }
              
    </script>

    Change Password
    <div class="content-box-content">
        <div id="tab1" class="tab-content default-tab" style="display: block;">
            <div class="table">
                <asp:Panel ID="pnlChangePassword" runat="server" DefaultButton="update">
                    <table border="0" align="center" cellpadding="0" cellspacing="0" class="listing form"
                        id="border_table2">
                        <tr>
                            <td class="heading_bg" colspan="2">
                                <span class="text"></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="text_padding" width="30%">
                            </td>
                            <td class="last" style="width: 491px">
                            </td>
                        </tr>
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        <tr>
                            <td class="text_padding" align="right">
                                <label for="OldPassword">
                                    Old Password <span class="redtext">* </span>:</label>
                            </td>
                            <td class="last">
                                <input id="OldPassword" class="text-input medium-input" type="password" MaxLength="20"/>
                                 <asp:HiddenField ID="hidOldPassword" runat="server" />
                               <%-- <asp:TextBox ID="OldPassword" runat="server" CssClass="text-input medium-input" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="Please enter old password" ControlToValidate="OldPassword" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                              --%>  <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="text_padding" align="right">
                                <label for="txtPwd">
                                    New Password <span class="redtext">* </span>:</label>
                            </td>
                            <td class="last">
                                <input id="txtPwd" class="text-input medium-input" type="password" MaxLength="20"/>
                                 <asp:HiddenField ID="hfpwd" runat="server" />
                                <%--<asp:TextBox ID="NewPassword" runat="server" CssClass="text-input medium-input" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="Please enter new password" ControlToValidate="NewPassword" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                               --%> <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="text_padding" align="right">
                                <label for="txtConfirmPassword">
                                    Confirm Password <span class="redtext">* </span>:</label>
                            </td>
                            <td class="last">
                                <input id="txtConfirmPassword" class="text-input medium-input" type="password" MaxLength="20"/>
                                <asp:HiddenField ID="hfvConfirmPassword" runat="server" />
                               <%-- <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="text-input medium-input"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                    ErrorMessage="Please enter confirm password" ControlToValidate="ConfirmPassword"
                                    ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword"
                                    Display="Dynamic" ControlToValidate="ConfirmPassword" ErrorMessage="Password does not match"
                                    ValidationGroup="ChangePassword"></asp:CompareValidator><br />--%>
                                <br />
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
                            <td class="login-text">
                                <label for="<%=txtCaptcha.ClientID %>">
                                    Please enter the code shown above<span class="redtext">* </span>:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCaptcha" runat="server"   CssClass="text-input medium-input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="update" runat="server" Text="Update" CssClass="button" ValidationGroup="ChangePassword"
                                    OnClick="update_Click" OnClientClick="javascript:getPass();" />
                                <asp:Button ID="reset" CssClass="button" runat="server" Text="Reset" CausesValidation="false"
                                    OnClick="reset_Click1" /><br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
