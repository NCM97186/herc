<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Petition_Add_Edit.aspx.cs" Inherits="Auth_AdminPanel_Petition_Management_Petition_Add_Edit" %>

<%@ Register Assembly="SyrinxCkEditor" Namespace="Syrinx.Gui.AspNet" TagPrefix="syx" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="Stylesheet" type="text/css" href="../css/uploadify.css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
    <script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>
    <script language="javascript" type="text/javascript">



        function ValidatePublicNotice(Source, args) {
            var fuData = document.getElementById('<%= fuPetition.ClientID %>');
            var FileUploadPath = fuData.value;

            if (FileUploadPath == '') {
                // There is no file selected
                args.IsValid = false;
            }
            else {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "pdf") {
                    args.IsValid = true; // Valid file type
                }
                else {
                    args.IsValid = false; // Not valid file type
                }
            }
        }
        
    </script>
    <script language="javascript" type="text/javascript">

        function ValidatePetition_Edit(Source, args) {
            var fuData = document.getElementById('<%= fuPetition_Edit.ClientID %>');
            var FileUploadPath = fuData.value;

            if (FileUploadPath == '') {
                // There is no file selected
                args.IsValid = false;
            }
            else {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "pdf") {
                    args.IsValid = true; // Valid file type
                }
                else {
                    args.IsValid = false; // Not valid file type
                }
            }
        }


        
    </script>
    <script type="text/javascript">
        var counter = 0;
        function AddFileUpload() {
            var div = document.createElement('DIV');
            div.innerHTML = '<label for="txtRemarks' + counter + '" name = "name' + counter +
                     '" >File Description :</label>' +
                     '<input name="txt' + counter + '" value = "' +
                     '" type="text" />' +

                     '<input id="file' + counter + '" name = "file' + counter +
                     '" type="file" />' +
                     '<input id="Button' + counter + '" type="button" ' +
                     'value="Remove" onclick = "RemoveFileUpload(this)" />';


            document.getElementById("FileUploadContainer").appendChild(div);
            counter++;
        }
        function RemoveFileUpload(div) {
            document.getElementById("FileUploadContainer").removeChild(div.parentNode);


        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                if (input == null) {
                    input = document.createElement("input");
                    input.setAttribute("type", "hidden");
                    input.setAttribute("id", "scrollY");
                    input.setAttribute("name", "scrollY");
                    document.forms[0].appendChild(input);
                }
                input.value = scrollY;
            }
        };
    </script>
    <asp:Panel ID="pnlPetitionAdd" runat="server">
        <p>
            <asp:Label ID="lblmsg" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtPetitionPROno.ClientID %>">
                Petition No. <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionPROno" CssClass="text-input small-input" runat="server"
                MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionPROno" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetitionPROno" ErrorMessage="Please enter petition Petition No."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusPetitionPROno" runat="server" ErrorMessage="Petition No. already exist. Please enter another Petition No."
                OnServerValidate="cusPetitionPROno_ServerValidate" ValidationGroup="Add" Display="Dynamic">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPetitionPROno"
                ErrorMessage="Petition No. must be between 1 to 9999." ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtPetitionDate.ClientID %>">
                Petition Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtPetitionDate" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibPetitionDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvStartDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtPetitionDate" ErrorMessage="Please enter petition date."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPetitionDate" runat="server" ControlToValidate="txtPetitionDate"
                ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CusomValidator1" runat="server" ControlToValidate="txtPetitionDate"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator1_ServerValidate"
                ValidationGroup="Add" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate"
                PopupButtonID="ibPetitionDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate">
            </cc1:CalendarExtender>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Petitioner's Details </span>
        </p>
        <p>
            <label for="<%=txtPetitionerName.ClientID %>">
                Petitioner's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionerName" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionerName" runat="server" SetFocusOnError="true"
                ControlToValidate="txtPetitionerName" ErrorMessage="Please enter petitioner's name."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPetitionerName"
                ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regPetitionerName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetitionerName"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtPetitionerAdd.ClientID %>">
                Petitioner's Address <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionerAdd" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionerAddress" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetitionerAdd" ErrorMessage="Please enter address."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regPetitionerAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetitionerAdd"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPetitionerAdd"
                ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtPetitionerMobileNo.ClientID %>">
                Petitioner's Mobile No :</label>
            <asp:TextBox ID="txtPetitionerMobileNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtPetitionerMobileNo"
                ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerPhoneNo.ClientID %>">
                Petitioner's Phone No :</label>
            <asp:TextBox ID="txtPetitionerPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerPhoneNo" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtPetitionerPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerFaxNo.ClientID %>">
                Petitioner's Fax No :</label>
            <asp:TextBox ID="txtPetitionerFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantFaxNo" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtPetitionerFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerEmailID.ClientID %>">
                Petitioner's Email ID :</label>
            <asp:TextBox ID="txtPetitionerEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerEmailid" runat="server" ControlToValidate="txtPetitionerEmailID"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Respondent's Details </span>
        </p>
        <p>
            <label for="<%=txtRespondentName.ClientID %>">
                Respondent's Name :</label>
            <asp:TextBox ID="txtRespondentName" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <cc1:AutoCompleteExtender ServiceMethod="GetAutoCompleteData" MinimumPrefixLength="2"
                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRespondentName"
                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
            </cc1:AutoCompleteExtender>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentName"
                ID="RegularExpressionValidator51" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRespondentName"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentAdd.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAdd" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentAdd"
                ID="regRespondentnameLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRespondentAdd"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentMobileNo.ClientID %>">
                Respondent's Mobile No :</label>
            <asp:TextBox ID="txtRespondentMobileNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentMobileNo" runat="server" ControlToValidate="txtRespondentMobileNo"
                ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentPhoneNo.ClientID %>">
                Respondent's Phone No :</label>
            <asp:TextBox ID="txtRespondentPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtRespondentPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentFaxNo.ClientID %>">
                Respondent's Fax No :</label>
            <asp:TextBox ID="txtRespondentFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtRespondentFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentEmailID.ClientID %>">
                Respondent's Email ID :</label>
            <asp:TextBox ID="txtRespondentEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="reRespondentEmailID" runat="server" ControlToValidate="txtRespondentEmailID"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionSubject.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionSubject" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionSubject" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetitionSubject" ErrorMessage="Please enter subject."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regSubjectLength" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
                ControlToValidate="txtPetitionSubject" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regSubjectValidation" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetitionSubject"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= ddlPetitionStatus.ClientID %>">
                Petition Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlPetitionStatus" runat="server" Enabled="false">
                    <asp:ListItem Value="10" Selected="True">In Process</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPetitionStatus" runat="server" ControlToValidate="ddlPetitionStatus"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select petition status."
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>
            </label>
        </p>
        <p>
            <label for="<%= ddlPetitionStatus.ClientID %>" style="color: red;">
                Is there any connected Petition? :
                <asp:LinkButton ID="lnkConnectedPetition" runat="server" Text="Yes" OnClick="lnkConnectedPetition_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedPetitionNo" runat="server" Text="No" OnClick="lnkConnectedPetitionNo_Click"
                    Visible="false">
                </asp:LinkButton>
            </label>
        </p>
        <div style="width: 487px; max-height: 350px; overflow: auto;" id="divConnectAdd"
            runat="server">
            <asp:Panel ID="pnlPetitionConnection" runat="server" Visible="false">
                <p id="PYear" runat="server">
                    <label for="<%=ddlYear.ClientID %>">
                        Year<span class="redtext">* </span>:
                        <asp:CheckBoxList ID="ddlYear" runat="server" CssClass="checkbox" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </label>
                </p>
                <asp:UpdatePanel ID="updatePnlChk" runat="server">
                    <ContentTemplate>
                        <div class="chekbox-value1" style="color: Red; font-weight: bold">
                            <asp:Literal ID="ltrlSelected" runat="server"></asp:Literal></div>
                        <asp:CheckBoxList ID="chklstPetition" CssClass="checkbox" runat="server" RepeatColumns="1"
                            RepeatDirection="Horizontal" TextAlign="Right" OnSelectedIndexChanged="chklstPetitionSelectedIndexChangd"
                            AutoPostBack="true">
                        </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <p>
            <div id="FileUploadContainer">
                <label for="<%=txtRemarks.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fuPetition" runat="server" />
                  <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="cvPublicNotice" runat="server" ClientValidationFunction="ValidatePublicNotice"
                    ControlToValidate="fuPetition" ValidationGroup="Add" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic" OnServerValidate="cvPublicNotice_ServerValidate">
                </asp:CustomValidator>
              
            </div>
            <input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
                tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
        </p>
         <p>
            <label for="<%= txtURL.ClientID %>">
                URLs :</label>
            <asp:TextBox ID="txtURL" runat="server" CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"
                ></asp:TextBox>
           <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURL"
                ID="regtxtURL" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>  
            <!--Validation for small description on date 28-10-2013 for security-->
              <asp:RegularExpressionValidator ID="RegtxtAppealLink" runat="server" ControlToValidate="txtURL"
                    Display="Dynamic" ErrorMessage="Please enter valid url" ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
            <!--End-->
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple URL separated by semicolon(;).</span></em>
        </p>
         <p>
            <label for="<%= txtURLDescription.ClientID %>">
                URLs Descriptions:</label>
            <asp:TextBox ID="txtURLDescription" runat="server" CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"
                ></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLDescription"
                ID="RegularExpressionValidator22" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ErrorMessage="Please enter valid URL description. No special characters are allowed except (space,'-_.:;#()/)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtURLDescription"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/ ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
           
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter description for multiple URLs separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%= txtRemarks.ClientID %>">
                Remarks :</label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" MaxLength="2000"
                TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRemarks"
                ID="RegularExpressionValidator12" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regRemarksValidation" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRemarks"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Remarks upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%=txtMetaKeyword.ClientID %>">
                Meta Keyword <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtMetaKeyword" runat="server" MaxLength="2000" TextMode="MultiLine"
                CssClass="text-input medium-input"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RevMetaKeyword" runat="server" ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                ControlToValidate="txtMetaKeyword" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*"
                ValidationGroup="Add" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeyword"
                ID="regMetakeywordLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMetaKeyword"
                ErrorMessage="Please enter Meta Keywords." ValidationGroup="Add"></asp:RequiredFieldValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Keywords upto 2000 characters are allowed with comma or space.</span></em>
        </p>
        <p>
            <label for="<%=txtMetaDescription.ClientID %>">
                Meta Description <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="2000" TextMode="MultiLine"
                CssClass="text-input medium-input"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaDescription"
                ID="regMetaDescriptionLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMetaDescription"
                ErrorMessage="Please enter Meta Description." ValidationGroup="Add"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regmtadescr" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaDescription"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Description upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%=ddlMetaLang.ClientID %>">
                Meta Language <span class="redtext">* </span>:</label>
            <asp:DropDownList ID="ddlMetaLang" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <label for="<%=txtMetaTitle.ClientID %>">
                Meta Title <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtMetaTitle" runat="server" MaxLength="2000" TextMode="MultiLine"
                CssClass="text-input medium-input"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaTitle"
                ID="RegularExpressionValidator18" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                ErrorMessage="Please enter Meta Title." ValidationGroup="Add"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regmetatitles" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaTitle"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Title upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <asp:Button ID="btnAddPetition" runat="server" CssClass="button" OnClick="btnAddPetition_Click"
                Text="Submit" ToolTip="Click To Save" ValidationGroup="Add" />
            <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="button"
                OnClick="btnReset_Click" Text="Reset" ToolTip="Click To Reset" />
            <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="button"
                OnClick="btnBack_Click" Text="Back" ToolTip="Go Back" />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
        <p>
            <asp:Label ID="lblMsg_Edit" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtPetitionPROno_Edit.ClientID %>">
                Petition No. <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionPROno_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionPRO_number" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetitionPROno_Edit" ErrorMessage="Please enter petition Petition No."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cus_PetitionPROno_Edit" runat="server" ErrorMessage="Petition No. already exist. Please enter another Petition No."
                OnServerValidate="cus_PetitionPROno_Edit_ServerValidate" ValidationGroup="Update"
                Display="Dynamic">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regPetitionPRO_Edit" runat="server" ErrorMessage="Petition No. must be between 1 to 9999."
                ControlToValidate="txtPetitionPROno_Edit" ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Update" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtPetitionDate_Edit.ClientID %>">
                Petition Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtPetitionDate_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibPetitionDate_Edit" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvPetitionDate_Edit" ValidationGroup="Update" SetFocusOnError="true"
                runat="server" ControlToValidate="txtPetitionDate_Edit" Display="Dynamic" EnableClientScript="false"
                ErrorMessage="Please enter petition date.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPetitionDate_Edit" runat="server" ControlToValidate="txtPetitionDate_Edit"
                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Update" CssClass="error">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtPetitionDate_Edit"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator2_ServerValidate"
                ValidationGroup="Update" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate_Edit"
                PopupButtonID="ibPetitionDate_Edit">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate_Edit">
            </cc1:CalendarExtender>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Petitioner's Details </span>
        </p>
        <p>
            <label for="<%=txtPetitionerName_Edit.ClientID %>">
                Petitioner's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionerName_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionName_Edit" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetitionerName_Edit" ErrorMessage="Please enter petitioner's name."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPetitionerNameLength" runat="server" ValidationExpression="^[\s\S]{3,2000}$"
                ControlToValidate="txtPetitionerName_Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regSmallDescription" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtPetitionerName_Edit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtPetitionerAddr_Edit.ClientID %>">
                Petitioner's Address <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionerAddr_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerAddrlength" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
                ControlToValidate="txtPetitionerAddr_Edit" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regAddressEdit" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtPetitionerAddr_Edit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtPetitionerMobileNo_Edit.ClientID %>">
                Petitioner's Mobile No :</label>
            <asp:TextBox ID="txtPetitionerMobileNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPetitionerMobileNo_Edit"
                ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerPhoneNo_Edit.ClientID %>">
                Petitioner's Phone No :</label>
            <asp:TextBox ID="txtPetitionerPhoneNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPetitionerPhoneNo_Edit"
                ErrorMessage="Please enter valid Phone number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerFaxNo_Edit.ClientID %>">
                Petitioner's Fax No :</label>
            <asp:TextBox ID="txtPetitionerFaxNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtPetitionerFaxNo_Edit"
                ErrorMessage="Please enter valid Fax number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerEmailID_Edit.ClientID %>">
                Petitioner's Email ID :</label>
            <asp:TextBox ID="txtPetitionerEmailID_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="reg_EmailID_Edit" runat="server" ControlToValidate="txtPetitionerEmailID_Edit"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Update"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Respondent's Details </span>
        </p>
        <p>
            <label for="<%=txtRespondentName_Edit.ClientID %>">
                Respondent's Name :</label>
            <asp:TextBox ID="txtRespondentName_Edit" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentName"
                ID="RegularExpressionValidatorEdit" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRespondentName_Edit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentAddr_Edit.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAddr_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentAddr" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
                ControlToValidate="txtRespondentAddr_Edit" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regRespondetAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRespondentAddr_Edit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentMobileNo_Edit.ClientID %>">
                Respondent's Mobile No :</label>
            <asp:TextBox ID="txtRespondentMobileNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtRespondentMobileNo_Edit"
                ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentPhoneNo_Edit.ClientID %>">
                Respondent's Phone No :</label>
            <asp:TextBox ID="txtRespondentPhoneNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                ControlToValidate="txtRespondentPhoneNo_Edit" ErrorMessage="Please enter valid Phone number."
                ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$" ValidationGroup="Update"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentFaxNo_Edit.ClientID %>">
                Respondent's Fax No :</label>
            <asp:TextBox ID="txtRespondentFaxNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                ControlToValidate="txtRespondentFaxNo_Edit" ErrorMessage="Please enter valid Fax number."
                ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$" ValidationGroup="Update"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentEmailID_Edit.ClientID %>">
                Respondent's Email ID :</label>
            <asp:TextBox ID="txtRespondentEmailID_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revRespondentEmailID_Edit" runat="server" ControlToValidate="txtRespondentEmailID_Edit"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Update"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionSubject_Edit.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionSubject_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionSubject_Edit" ValidationGroup="Update"
                runat="server" SetFocusOnError="true" ControlToValidate="txtPetitionSubject_Edit"
                ErrorMessage="Please enter subject." Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPetitionSubjectLength" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
                ControlToValidate="txtPetitionSubject_Edit" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regSubjectEdit" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtPetitionSubject_Edit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= ddlPetitionStatus_Edit.ClientID %>">
                Petition Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlPetitionStatus_Edit" runat="server" Enabled="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_Status_Edit" runat="server" ControlToValidate="ddlPetitionStatus_Edit"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select petition status."
                    ValidationGroup="Update">
                </asp:RequiredFieldValidator>
                <asp:Label ID="lblUploadMsg" runat="server" Visible="false"></asp:Label>
                <asp:CustomValidator ID="CompanySelectionValidation" OnServerValidate="CompanySelectionValidation_ServerValidate"
                    ControlToValidate="ddlPetitionStatus_Edit" Display="Dynamic" runat="server" ErrorMessage="You must either select the company that you represent or enter in your company information."
                    Text="*" ValidationGroup="AllValidators">
                </asp:CustomValidator>
            </label>
        </p>
        <p>
            <label for="<%= ddlPetitionStatus.ClientID %>" style="color: red;">
                Is there any connected Petition? :
                <asp:LinkButton ID="lnkConnectedPetitionEdit" runat="server" Text="Yes" OnClick="lnkConnectedPetitionEdit_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedPetitionEditNo" runat="server" Text="No" OnClick="lnkConnectedPetitionEditNo_Click">
                </asp:LinkButton>
            </label>
        </p>
        <div style="width: 487px; max-height: 350px; overflow: auto" id="divConnectEdit"
            runat="server">
            <p id="P1" runat="server">
                <label for="<%=ddlYearEdit.ClientID %>">
                    Year<span class="redtext">* </span>:
                    <asp:CheckBoxList ID="ddlYearEdit" runat="server" CssClass="checkbox" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlYearEdit_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </label>
            </p>
            <asp:UpdatePanel ID="updateChkEdit" runat="server">
                <ContentTemplate>
                    <div class="chekbox-value1" style="color: Red; font-weight: bold">
                        <asp:Literal ID="ltrlSelectedEdit" runat="server"></asp:Literal></div>
                    <asp:Panel ID="pnlEditConnectedPetition" runat="server">
                        <asp:CheckBoxList ID="chklstPetitionEdit" runat="server" CssClass="checkbox" RepeatColumns="1"
                            RepeatDirection="Horizontal" OnSelectedIndexChanged="chklstPetitionEditSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <p>
            <div id="FileUploadContainer">
                <label for="<%=txtRemarks.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fuPetition_Edit" runat="server" />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidatePetition_Edit"
                    ControlToValidate="fuPetition_Edit" ValidationGroup="Update" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic" OnServerValidate="cvPetitionEdit_ServerValidate">
                </asp:CustomValidator>
                
            </div>
            <input id="Button2" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
                tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
            <p>
            </p>
            <p id="PFileName" runat="server" visible="false">
                <asp:DataList ID="datalistFileName" runat="server" CellPadding="2" CellSpacing="1"
                    OnItemCommand="datalistFileName_ItemCommand" RepeatColumns="1" RepeatDirection="Horizontal"
                    OnItemDataBound="datalistFileName_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                        <asp:HiddenField ID="hiddenFieldPetitionID" runat="server" Value='<%#Eval("PetitionId") %>' />
                        <b>
                            <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label>
                        </b>
                        <%--<asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>--%>
                        <asp:Label ID="lblComments" runat="server" Text='<%#bind("Comments") %>'></asp:Label>
                        <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandArgument='<%# Eval("Id") %>'
                            CommandName="File" ToolTip="Remove File">Remove File</asp:LinkButton>
                        <asp:Literal ID="ltrlDownload" runat="server">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:DataList>
            </p>
            <p>
                <label for="<%= txtURLEdit.ClientID %>">
                    URLs :</label>
                <asp:TextBox ID="txtURLEdit" runat="server" CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLEdit"
                    ID="RegularExpressionValidator24" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
              <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server"
                    ControlToValidate="txtURLEdit" Display="Dynamic" ErrorMessage="Please enter valid url"
                    ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$"
                    ValidationGroup="Update"></asp:RegularExpressionValidator>--%>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server"
                    ControlToValidate="txtURLEdit" Display="Dynamic" ErrorMessage="Please enter valid url"
                  ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.%])*" 
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
                <!--End-->
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-You may enter multiple URL separated by semicolon(;) (eg:- http://www.abc.com).</span></em>
            </p>
            <p>
            <label for="<%= txtURLDescriptionEdit.ClientID %>">
                URLs Descriptions:</label>
            <asp:TextBox ID="txtURLDescriptionEdit" runat="server" CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"
                ></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLDescriptionEdit"
                ID="RegularExpressionValidator25" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ErrorMessage="Please enter valid URL description. No special characters are allowed except (space,'-_.:;#()/)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtURLDescriptionEdit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/ ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
           
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter description for multiple URLs separated by semicolon(;).</span></em>
           
            </p>
            <p>
                <label for="<%= txtRemarksEdit.ClientID %>">
                Remarks :</label>
                <asp:TextBox ID="txtRemarksEdit" runat="server" 
                    CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" 
                    runat="server" ControlToValidate="txtRemarksEdit" Display="Dynamic" 
                    ErrorMessage="Minimum 5 and maximum 2000 characters required." 
                    ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regRemarksEdit" runat="server" 
                    ControlToValidate="txtRemarksEdit" Display="Dynamic" 
                    ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&amp;)" 
                    SetFocusOnError="True" 
                    ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*" 
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
                <!--End-->
                <br />
                <em>
                <span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Remarks upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <label for="<%=txtMetaKeywordEdit.ClientID %>">
                Meta Keyword <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaKeywordEdit" runat="server" 
                    CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" 
                    runat="server" ControlToValidate="txtMetaKeywordEdit" Display="Dynamic" 
                    ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)" 
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*" 
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" 
                    runat="server" ControlToValidate="txtMetaKeywordEdit" Display="Dynamic" 
                    ErrorMessage="Minimum 3 and maximum 2000 characters required." 
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Update"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtMetaKeywordEdit" 
                    ErrorMessage="Please enter Meta Keywords." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <br />
                <em>
                <span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Keywords upto 2000 characters are allowed with comma or space.</span></em>
            </p>
            <p>
                <label for="<%=txtMetaDescriptionEdit.ClientID %>">
                Meta Description <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaDescriptionEdit" runat="server" 
                    CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" 
                    runat="server" ControlToValidate="txtMetaDescriptionEdit" Display="Dynamic" 
                    ErrorMessage="Minimum 3 and maximum 2000 characters required." 
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Update"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtMetaDescriptionEdit" 
                    ErrorMessage="Please enter Meta Description." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetadescedit" runat="server" 
                    ControlToValidate="txtMetaDescriptionEdit" Display="Dynamic" 
                    ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&amp;)" 
                    SetFocusOnError="True" 
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*" 
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
                <br />
                <em>
                <span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Description upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <label for="<%=DropDownList1.ClientID %>">
                Meta Language <span class="redtext">* </span>:</label>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                <label for="<%=txtMetaTitleEdit.ClientID %>">
                Meta Title <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaTitleEdit" runat="server" 
                    CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" 
                    runat="server" ControlToValidate="txtMetaTitleEdit" Display="Dynamic" 
                    ErrorMessage="Minimum 3 and maximum 2000 characters required." 
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Update"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtMetaTitleEdit" ErrorMessage="Please enter Meta Title." 
                    ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetaedit" runat="server" 
                    ControlToValidate="txtMetaTitleEdit" Display="Dynamic" 
                    ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&amp;)" 
                    SetFocusOnError="True" 
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*" 
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
                <br />
                <em>
                <span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Title upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <asp:Button ID="btnPetitionUpdate" runat="server" CssClass="button" 
                    OnClick="btnPetitionUpdate_Click" Text="Update" ValidationGroup="Update" />
                <asp:Button ID="btnReset_Edit" runat="server" CausesValidation="False" 
                    CssClass="button" OnClick="btnReset_Edit_Click" Text="Reset" />
                <asp:Button ID="btnBack_Edit" runat="server" CausesValidation="False" 
                    CssClass="button" OnClick="btnBack_Edit_Click" Text="Back" />
            </p>
            <p>
            </p>
            <p>
            </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            <p>
            </p>
            <p>
            </p>
            <p>
            </p>
        </p>
    </asp:Panel>
</asp:Content>
