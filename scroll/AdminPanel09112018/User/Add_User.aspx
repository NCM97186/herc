<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Add_User.aspx.cs" Inherits="Auth_AdminPanel_User_Add_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../js/sha512.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


        function getPass() {

            var phoneexp = /^\d{10,15}$/;
            var exp = /((?=.*\d)(?=.*[a-z])(?=.*[@#$&]).{8,15})/;
            var expEmail = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            var value1 = document.getElementById('txtPwd').value;
            var value2 = document.getElementById('<%=ddlDeptname.ClientID%>').value;
            var value3 = document.getElementById('<%=txtUserName.ClientID%>').value;
            var value4 = document.getElementById('txtConfirmPassword').value;
            var value5 = document.getElementById('<%=txtName.ClientID%>').value;
            var value6 = document.getElementById('<%=txtAddress.ClientID%>').value;


            var value9 = document.getElementById('<%=txtEmail.ClientID%>').value;
            var value10 = document.getElementById('<%=txtContactno.ClientID%>').value;
            var value11 = document.getElementById('<%=ddlDeptname.ClientID%>').value;
            var value12 = document.getElementById('<%=ddlRole.ClientID%>').value;

            if (value3 == '') {
                alert("Please enter username.");
                return false;
            }
            if (value1 == '') {
                alert("Please enter password.");
                return false;
            }
            if (value1.search(exp) == -1) {

                alert("The password should be between 8 to 15 Characters. The password must have at least one alphabet, one digit and one special character (@#$&).");

                return false;
            }
            if (value4 == '') {
                alert("Please enter confirm password.");
                return false;
            }
            var hash = hex_sha512(value1);
            var hash1 = hex_sha512(value4);

            if (value5 == '') {
                alert("Please enter name.");
                return false;
            }



            if (value9 == '') {
                alert("Please enter email id.");
                return false;
            }
            else if (value9.search(expEmail) == -1) {
                alert("Please enter email id in correct format.");
                return false
            }


            if (value11 == '0') {
                alert("Please select department.");
                return false;
            }
            if (value12 == '0') {
                alert("Please select role.");
                return false;
            }


            if (hash != hash1) {
                document.getElementById('txtConfirmPassword').value = '';
                document.getElementById('txtPwd').value = '';
                document.getElementById('<%=txtUserName.ClientID%>').value = '';
                document.getElementById('<%=txtName.ClientID%>').value = '';
                document.getElementById('<%=txtEmail.ClientID%>').value = '';
                document.getElementById('<%=txtContactno.ClientID%>').value = '';
                document.getElementById('<%=txtAddress.ClientID%>').value = '';
                alert("Password is not matched.");

                return false;
            }
            else {

                document.getElementById('<%=hfpwd.ClientID%>').value = hash;

                return true;
            }
        }
              
    </script>

    <p>
        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
    </p>
    <p>
        <label for="<%=txtUserName.ClientID %>">
            User Name<span class="redtext">* </span>:</label>
        <asp:TextBox ID="txtUserName" runat="server" CssClass="text-input small-input" MaxLength="25">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtUserName"
            Display="Dynamic" ErrorMessage="Please enter user name" SetFocusOnError="True"
            ValidationGroup="Add">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RevName" runat="server" ErrorMessage="User name should start with alphabets and no other symbols are used except . and _"
            ControlToValidate="txtUserName" ValidationExpression="[A-Za-z][A-Za-z0-9._]{1,25}"  Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtUserName"
            ID="RegularExpressionValidator26" ValidationExpression="^[\s\S]{6,25}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 6 and Maximum 25 characters required."></asp:RegularExpressionValidator>
        <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="User name already exist. Please enter other user login id."
            ValidationGroup="Add">
        </asp:CustomValidator>
    </p>
    <p id="password" runat="server">
        <label for="txtPwd">
            Password<span class="redtext">* </span>:</label>
        <input id="txtPwd" class="text-input medium-input" type="password" maxlength="15"/>
        <asp:HiddenField ID="hfpwd" runat="server" />
		<br/>
		<em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-The password should be between 8 to 15 Characters. The password must have at least one alphabet, one digit and one special character (@#$&).</span></em>
    </p>
    <p id="confirmpass" runat="server">
        <label for="txtConfirmPassword">
            Confirm Password<span class="redtext">* </span>:</label>
        <input id="txtConfirmPassword" class="text-input medium-input" type="password" />
        <asp:HiddenField ID="hfvConfirmPassword" runat="server" />
    </p>
    <p>
        <label for="<%=txtName.ClientID %>">
            Name<span class="redtext">* </span>:</label>
        <asp:DropDownList ID="ddlInitial" runat="server">
        </asp:DropDownList>
        <asp:TextBox ID="txtName" runat="server" CssClass="text-input small-input" MaxLength="30">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            Display="Dynamic" ErrorMessage="Please enter name." SetFocusOnError="True" ValidationGroup="Add">
        </asp:RequiredFieldValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtName"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/ ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtName" ID="regNameLength"
            ValidationExpression="^[\s\S]{5,30}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 30 characters required."></asp:RegularExpressionValidator>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlInitial"
            InitialValue="0" Display="Dynamic" ErrorMessage="Please select Initial." SetFocusOnError="True"
            ValidationGroup="Add">
        </asp:RequiredFieldValidator>
    </p>
    <p>
        <label for="<%= txtAddress.ClientID %>">
            Address:</label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="text-input medium-input" TextMode="MultiLine"
            MaxLength="200" onkeypress="if(this.value.length>=200) this.value = this.value.substring(0, 199);">
        </asp:TextBox>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regtxtAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtAddress"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <%--      <asp:RegularExpressionValidator ID="regAddress" runat="server" ErrorMessage="Enter only alphanumeric values and ( * - , / () : _ ) special characters."
            ControlToValidate="txtAddress" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Add"
            ValidationExpression="^([A-Za-z0-9-* - _'.,:()/\s]*)$"></asp:RegularExpressionValidator>--%>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAddress"
            ID="regAddressLength" ValidationExpression="^[\s\S]{5,200}$" runat="server" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 200 characters required."></asp:RegularExpressionValidator>
    </p>
    <p>
        <label for="<%= txtCity.ClientID %>">
            City:</label>
        <asp:TextBox ID="txtCity" runat="server" CssClass="text-input small-input" MaxLength="50">
        </asp:TextBox>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regCity" runat="server" ErrorMessage="Please enter valid city. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtCity"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCity" ID="RegularExpressionValidator2"
            ValidationExpression="^[\s\S]{3,50}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="P1" runat="server">
        <label for="<%=txtCountry.ClientID %>">
            Country:</label>
        <asp:TextBox ID="txtCountry" runat="server" CssClass="text-input small-input" MaxLength="50"></asp:TextBox>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regCountry" runat="server" ErrorMessage="Please enter valid country name. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtCountry"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>

			 <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCountry" ID="RegularExpressionValidator69"
            ValidationExpression="^[\s\S]{3,50}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
        <!--End-->
    </p>
    <p>
        <label for="<%= txtEmail.ClientID %>">
            Email<span class="redtext">* </span>:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="text-input small-input" MaxLength="50">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
            Display="Dynamic" ErrorMessage="Please enter email id." SetFocusOnError="True"
            ValidationGroup="Add">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
            Display="Dynamic" ErrorMessage="Please enter valid email id." SetFocusOnError="True"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-abc@gmail.com</span></em>
    </p>
    <p>
        <label for="<%= txtContactno.ClientID %>">
            Mobile No.:
        </label>
        <asp:TextBox ID="txtContactno" runat="server" CssClass="text-input small-input" MaxLength="11">
        </asp:TextBox>
        <%--     <asp:RequiredFieldValidator ID="rfvContactno" runat="server" ControlToValidate="txtContactno"
            Display="Dynamic" ErrorMessage="Please enter contact number." SetFocusOnError="True"
            ValidationGroup="Add">
        </asp:RequiredFieldValidator>--%>
        <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtContactno"
            ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}$"
            ValidationGroup="Add" Display="Dynamic">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Don't start with '0' (zero).</span></em>
    </p>
    <asp:UpdatePanel ID="updatePanelUser" runat="server">
        <ContentTemplate>
            <p id="P2" runat="server">
                <label for="<%=ddlDeptname.ClientID %>">
                    Deptt Name<span class="redtext">* </span>:</label>
                <asp:DropDownList ID="ddlDeptname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptname_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDeptname"
                    Display="Dynamic" ErrorMessage="Please select department name." InitialValue="0"
                    SetFocusOnError="True" ValidationGroup="Add">
                </asp:RequiredFieldValidator>
            </p>
            <p id="P3" runat="server">
                <label for="<%=ddlRole.ClientID %>">
                    Role<span class="redtext">* </span>:</label>
                <asp:DropDownList ID="ddlRole" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlRole"
                    Display="Dynamic" ErrorMessage="Please select role" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" CssClass="button"
            Text="Submit" OnClientClick="return getPass();" ValidationGroup="Add" OnClick="btnSubmit_Click"
            ToolTip="Click To Save" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CausesValidation="True" ValidationGroup="Add"
            CssClass="button" OnClick="btnUpdate_Click" ToolTip="Click To Update" />
        <asp:Button ID="btnCancel" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
            OnClick="btnCancel_Click" ToolTip="Click To Reset" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
            OnClick="btnBack_Click" ToolTip="Go Back" />
    </p>
</asp:Content>
