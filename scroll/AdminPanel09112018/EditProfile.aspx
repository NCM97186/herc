<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="Auth_AdminPanel_EditProfile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
  <script language="javascript" type="text/javascript">
      function myKeyPress(txt, e) {

          var keynum;


          if (window.event) { // IE					
              keynum = e.keyCode;


          }
          else {
              if (e.which) { // Netscape/Firefox/Opera					
                  keynum = e.which;
              }
          }

          if (keynum == 8) {

              return true;

          }

          else {
              var ffff = document.getElementById('<%= txtAddress.ClientID %>').value;
              var input = document.getElementById('<%= txtAddress.ClientID %>');
              // var ddd = e.clipboardData.getData('text/plain');
              input.onpaste = function () { document.getElementById('txtAddress').value = txt.substring(0, 200); return false; };
              //substring(1,4)   document.getElementById('txtPwd').value 
              var len = ffff.length + 1;
              if (len > 200) {
                  document.getElementById('txtAddress').value = ffff.substring(0, 200);
                  return false;
              }
              else {

                  return true;
              }
          }

      }


    </script>
        <div id="tab1" class="tab-content default-tab" style="display: block;">
            <div class="table">
                <p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    <label for="<%= TxtUsername.ClientID %>">
                        Username <span class="redtext">* </span>:</label>
                    <asp:TextBox ID="TxtUsername" runat="server" CssClass="text-input medium-input" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtUsername"
                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ErrorMessage="Please Enter UserName">
                    </asp:RequiredFieldValidator>
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
                    <asp:RegularExpressionValidator ID="regName" runat="server" ErrorMessage="Enter only alphanumeric values and ( * , / () ; _ ) special characters."
                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtName"
                        ValidationExpression="^([A-Za-z0-9-*_'.,()/\s]*)$"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtName" ID="regNameLength"
                        ValidationExpression="^[\s\S]{5,30}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 30 characters required."></asp:RegularExpressionValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInitial"
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
                    <asp:RegularExpressionValidator ID="regAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                        ControlToValidate="txtAddress" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Add"
                        ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAddress"
                        ID="regAddressLength" ValidationExpression="^[\s\S]{5,200}$" runat="server" ValidationGroup="Add"
                        ErrorMessage="Minimum 5 and maximum 200 characters required."></asp:RegularExpressionValidator>
                </p>
                <p>
                    <label for="<%= txtCity.ClientID %>">
                        City:</label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="text-input small-input" MaxLength="50">
                    </asp:TextBox>
                    <asp:RegularExpressionValidator ID="regCity" runat="server" ErrorMessage="Please enter valid city. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtCity"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCity" ID="RegularExpressionValidator2"
                        ValidationExpression="^[\s\S]{3,50}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
                </p>
                <p id="P1" runat="server">
                    <label for="<%=txtCountry.ClientID %>">
                        Country:</label>
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="text-input small-input" MaxLength="50"></asp:TextBox>
					 <asp:RegularExpressionValidator ID="regCountry" runat="server" ErrorMessage="Please enter valid country name. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtCountry"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
			  <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCountry" ID="RegularExpressionValidator59"
                        ValidationExpression="^[\s\S]{3,50}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
                </p>
                <p>
                    <label for="<%= txtEmail.ClientID %>">
                        Email<span class="redtext">* </span>:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="text-input small-input" MaxLength="50">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ErrorMessage="Please enter email id." SetFocusOnError="True"
                        ValidationGroup="Add">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ErrorMessage="Please enter valid email id. eg: abc@xyz.com."
                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="Add">
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
                    <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtContactno"
                        ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}$"
                        ValidationGroup="Add" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    <br />
                    <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                        Tip:-Don't start with '0' (zero).</span></em>
                </p>
                
                <p>
                    <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click"
                        CssClass="button" Width="52px" ValidationGroup="Add" />
                    <asp:Button ID="Btnreset" runat="server" Text="Reset" CssClass="button" OnClick="Btnreset_Click" />
                    <br />
                </p>
            </div>
        </div>
 
</asp:Content>
