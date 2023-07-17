<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Petition_Appeal_Insert_Update.aspx.cs" Inherits="Auth_AdminPanel_Petition_Management_Petition_Appeal_Insert_Update" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= fileAppeal.ClientID %>');
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

        function ValidateFileUpload_Edit(Source, args) {
            var fuData = document.getElementById('<%= fileAppeal_Edit.ClientID %>');
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
            div.innerHTML = '<label for="txtRemarksAppeal' + counter + '" name = "name' + counter +
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
    <p id="pAppealNumber" runat="server" visible="false">
        <asp:Label ID="LblPetitionPA" CssClass="greentext" Font-Size="Medium" Text="Petition Appeal Number :"
            runat="server"></asp:Label>
    </p>
    <asp:Panel ID="pnlAppealAdd" runat="server">
        <p id="PLanguage" runat="server">
            <label for="<%= ddlLanguage.ClientID %>">
                Select Language <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlLanguage" runat="server">
                </asp:DropDownList>
            </label>
        </p>
        <p>
            <asp:Label ID="lblmsg" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtAppeal_no.ClientID %>">
                Appeal Number (HERC Reference No.) <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtAppeal_no" CssClass="text-input small-input" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAppealNumber" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtAppeal_no" ErrorMessage="Please enter appeal number (HERC Internal Number)."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusAppealNumber" runat="server" CssClass="validation" Display="Dynamic"
                ErrorMessage="Appeal number already exist. Please enter another appeal number."
                OnServerValidate="cusAppealNumber_ServerValidate" ValidationGroup="Add">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regAppealNumber" runat="server" ControlToValidate="txtAppeal_no"
                ErrorMessage="Appeal number must be between 1 to 9999." ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtPetitionDate.ClientID %>">
                Appeal Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtPetitionDate" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibPetitionDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvStartDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtPetitionDate" ErrorMessage="Please enter appeal date."
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
            <label for="<%=txtAppeal_Address.ClientID %>">
                Where Appealed <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtAppeal_Address" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetReviewSubject" ValidationGroup="Add" runat="server"
                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtAppeal_Address"
                ErrorMessage="Please enter where appealed."></asp:RequiredFieldValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regAppealAddressAdd" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtAppeal_Address"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <asp:RegularExpressionValidator ID="regAppealAddressLength" runat="server" ValidationExpression="^[\s\S]{3,2000}$"
                ControlToValidate="txtAppeal_Address" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%= txtRefNo.ClientID %>">
                Ref No(Where Appealed):
            </label>
            <asp:TextBox ID="txtRefNo" runat="server" CssClass="text-input small-input" MaxLength="200"></asp:TextBox>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regtxtRefNumber" runat="server" ErrorMessage="Please enter valid reference number. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRefNo"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regrefnumber" runat="server" ValidationExpression="^[\s\S]{3,200}$"
                ControlToValidate="txtRefNo" ErrorMessage="Minimum 3 and maximum 200 characters required."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Ref No(Where Appealed) upto 200 characters are allowed.</span></em>
            <!--End-->
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate"
                PopupButtonID="ibPetitionDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate">
            </cc1:CalendarExtender>
        </p>
        <p id="pOrderlink" runat="server">
            <label for="<%= lnkConnectedOrder.ClientID %>" style="color: red;">
                Do you want to connect this Appeal petition with Order? :
                <asp:LinkButton ID="lnkConnectedOrder" runat="server" Text="Yes" OnClick="lnkConnectedOrder_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedOrderNo" runat="server" Text="No" OnClick="lnkConnectedOrderNo_Click"
                    Visible="false">
                </asp:LinkButton>
            </label>
        </p>
        <div style="width: 300px; max-height: 350px; overflow: auto;" id="DivConnectedOrder"
            runat="server">
            <asp:Panel ID="pnlConnectedOrder" runat="server" Visible="false">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rdioConnectedOrder" AutoPostBack="true" RepeatDirection="Horizontal"
                            CssClass="mrg_radio" runat="server" OnSelectedIndexChanged="rdioConnectedOrder_SelectedIndexChanged">
                            <asp:ListItem Value="8" Text="Interim Order" />
                            <asp:ListItem Value="9" Text="Final Order" />
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="rdioConnectedOrder" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <p id="PConnectedYear" runat="server" visible="false">
                <label for="<%=drpConnectedOrderYear.ClientID %>">
                    Year<span class="redtext">* </span>:
                    <asp:DropDownList ID="drpConnectedOrderYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpConnectedOrderYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
            </p>
            <p id="PConnectedOrderId" runat="server" visible="false">
                <label for="<%=drpConnectedOrderId.ClientID %>">
                    Order<span class="redtext">* </span>:
                    <asp:DropDownList ID="drpConnectedOrderId" runat="server" OnPreRender="drpConnectedOrderId_PreRender"
                        Width="100%">
                    </asp:DropDownList>
                </label>
            </p>
        </div>
        <p>
            <label for="<%=txtApplicantName.ClientID %>">
                Petitioner's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantName" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionerName" runat="server" SetFocusOnError="true"
                ControlToValidate="txtApplicantName" ErrorMessage="Please enter petitioner's name."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantName"
                ID="RegularExpressionValidator9" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regApplicantNameAdd" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtApplicantName"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
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
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPetitionerAdd"
                ID="regPetitionerAdderss" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regPetitionerAddressAdd" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetitionerAdd"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtApplicantMobileNo.ClientID %>">
                Petitioner's Mobile No :</label>
            <asp:TextBox ID="txtApplicantMobileNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantMobileNo" runat="server" ErrorMessage="Please enter valid Mobile number."
                ControlToValidate="txtApplicantMobileNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantPhoneNo.ClientID %>">
                Petitioner's Phone No :</label>
            <asp:TextBox ID="txtApplicantPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtApplicantPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantFaxNo.ClientID %>">
                Petitioner's Fax No :</label>
            <asp:TextBox ID="txtApplicantFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtApplicantFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantEmailID.ClientID %>">
                Petitioner's Email ID :</label>
            <asp:TextBox ID="txtApplicantEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantEmailID" runat="server" ControlToValidate="txtApplicantEmailID"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated with semicolon(;).</span></em>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Respondent's Details </span>
        </p>
        <p>
            <label for="<%=txtRespondentName.ClientID %>">
                Respondent's Name :</label>
            <asp:TextBox ID="txtRespondentName" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentName"
                ID="RegularExpressionValidator51" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regRespondentNameAdd" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRespondentName"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentAdd.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAdd" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentAdd"
                ID="regRespondentnameLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regRespondentAddressAdd" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRespondentAdd"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                ErrorMessage="Please enter valid Phone number." ControlToValidate="txtRespondentPhoneNo"
                Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                ErrorMessage="Please enter valid Fax number." ControlToValidate="txtRespondentFaxNo"
                Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
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
            <label for="<%=txtPetReviewSubject.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetReviewSubject" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionSubject" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetReviewSubject" ErrorMessage="Please enter subject."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regSubjectLength" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
                ControlToValidate="txtPetReviewSubject" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetReviewSubject"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= ddlAppealStatus.ClientID %>">
                Appeal Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlAppealStatus" runat="server" Enabled="false">
                    <asp:ListItem Value="10" Selected="True">In Process</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPetReviewStatus" runat="server" ControlToValidate="ddlAppealStatus"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select appeal status."
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
            </label>
        </p>
        <p id="pPetition_Appeal" runat="server">
            <label for="<%= fileAppeal.ClientID %>">
                Upload File :</label>
            <asp:FileUpload ID="fileAppeal" runat="server" />
            <asp:CustomValidator ID="cvFileReviewPetition" runat="server" ClientValidationFunction="ValidateFileUpload"
                ControlToValidate="fileAppeal" ValidationGroup="Add" ErrorMessage="Please select only .pdf file."
                Display="Dynamic" OnServerValidate="cvFileReviewPetition_ServerValidate">
            </asp:CustomValidator>
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Pdf file only</span></em>
            <br />
        </p>
        <div style="width: 200px; max-height: 350px; overflow: auto;" id="divConnectAdd1"
            runat="server">
            <asp:Panel ID="pnlPetitionConnection1" runat="server" Visible="false">
                <p id="P3" runat="server">
                    <label for="<%=ddlYear1.ClientID %>">
                        Year<span class="redtext">* </span>:
                        <asp:CheckBoxList ID="ddlYear1" runat="server" CssClass="checkbox" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlYear1_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </label>
                </p>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:CheckBoxList ID="chklstPetition1" CssClass="checkbox" runat="server" RepeatColumns="1"
                            RepeatDirection="Horizontal" TextAlign="Right" OnSelectedIndexChanged="chklstPetition1SelectedIndexChangd"
                            AutoPostBack="true">
                        </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <p>
            <div id="FileUploadContainer">
                <label for="<%=txtRemarksAppeal.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="txtRemarksAppeal" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fuPetition" runat="server" />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="cvPublicNotice" runat="server" ClientValidationFunction="ValidatePublicNotice"
                    ControlToValidate="fuPetition" ValidationGroup="Add" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic" OnServerValidate="cvAppeal_ServerValidate">
                </asp:CustomValidator>
                <%-- <asp:FileUpload ID="FileUpload1" runat="server" />--%>
                <!--FileUpload Controls will be added here -->
            </div>
            <input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
                tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
            <p>
            </p>
            <p id="PJudgementDescription" runat="server">
                <label for="<%=txtAppealSubject.ClientID %>">
                    Judgement Description:</label>
                <asp:TextBox ID="txtAppealSubject" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                    ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtAppealSubject"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <asp:RegularExpressionValidator ID="regRespondentAddr" runat="server" ControlToValidate="txtAppealSubject"
                    Display="Dynamic" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Description upto 2000 characters are allowed.</span></em>
            </p>
            <p id="pJudgementLink" runat="server">
                <label for="<%= txtAppealLink.ClientID %>">
                    Judgement Link :</label>
                <asp:TextBox ID="txtAppealLink" runat="server" CssClass="text-input medium-input"
                    MaxLength="200"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regJudgementLink" runat="server" ControlToValidate="txtAppealLink"
                    Display="Dynamic" ErrorMessage="Minimum 5 and maximum 200 characters required."
                    ValidationExpression="^[\s\S]{5,200}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegtxtAppealLink" runat="server" ControlToValidate="txtAppealLink"
                    Display="Dynamic" ErrorMessage="Please enter valid url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label for="<%= txtRemarks.ClientID %>">
                    Remarks :</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" MaxLength="2000"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRemarks"
                    ID="RegularExpressionValidator17" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regtxtRemarksAdd" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRemarks"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMetaKeyword"
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMetaDescription"
                    ErrorMessage="Please enter Meta Description." ValidationGroup="Add"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetadescrip" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
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
                    ID="regmetatitlees" ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="Add"
                    ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                    ErrorMessage="Please enter Meta Title." ValidationGroup="Add"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmettitle" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaTitle"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Title upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <asp:Button ID="btnAddAppeal" runat="server" CssClass="button" OnClick="btnAddAppeal_Click"
                    Text="Submit" ValidationGroup="Add" />
                <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnReset_Click" Text="Reset" />
                <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnBack_Click" Text="Back" />
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
    <asp:Panel ID="pnlAppealEdit" runat="server">
        <p>
            <asp:Label ID="lblmsg_Edit" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtAppeal_no_Edit.ClientID %>">
                Appeal Number (HERC Reference No.)<span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtAppeal_no_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="4"> </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAppeal_No" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtAppeal_no_Edit" ErrorMessage="Please enter appeal number (HERC Internal Number)."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusAppealNumber_Edit" runat="server" CssClass="validation"
                Display="Dynamic" ErrorMessage="Appeal number already exist. Please enter another appeal number."
                OnServerValidate="cusAppealNumber_Edit_ServerValidate" ValidationGroup="Update">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regAppealNumEdit" runat="server" ControlToValidate="txtAppeal_no_Edit"
                ErrorMessage="Appeal number must be between 1 to 9999 characters and only numbers are applicable."
                ValidationExpression="^[1-9][0-9]{0,3}$" ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtPetitionDate_Edit.ClientID %>">
                Appeal Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtPetitionDate_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Update"
                SetFocusOnError="true" runat="server" ControlToValidate="txtPetitionDate_Edit"
                ErrorMessage="Please enter appeal date." Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPetitionDate_Edit"
                ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtPetitionDate_Edit"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator2_ServerValidate"
                ValidationGroup="Update" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <label for="<%=txtAppeal_Address_Edit.ClientID %>">
                Where Appealed <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtAppeal_Address_Edit" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAppealAddress" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtAppeal_Address_Edit"
                ErrorMessage="Please enter where appealed."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regAppealAddress" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
                ControlToValidate="txtAppeal_Address_Edit" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regWhereAppealed" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtAppeal_Address_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= txtRefEdit.ClientID %>">
                Ref No(Where Appealed):
            </label>
            <asp:TextBox ID="txtRefEdit" runat="server" CssClass="text-input small-input" MaxLength="200"></asp:TextBox>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRefEdit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regrefEdit" runat="server" ValidationExpression="^[\s\S]{3,200}$"
                ControlToValidate="txtRefEdit" ErrorMessage="Minimum 3 and maximum 200 characters required."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Ref No(Where Appealed) upto 200 characters are allowed.</span></em>
            <!--End-->
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate_Edit"
                PopupButtonID="ibPetitionDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate_Edit">
            </cc1:CalendarExtender>
        </p>
        <p>
            <label for="<%= lnkConnectedOrderEdit.ClientID %>" style="color: red;">
                Do you want to connect this Appeal petition with Order? :
                <asp:LinkButton ID="lnkConnectedOrderEdit" runat="server" Text="Yes" OnClick="lnkConnectedOrderEdit_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedOrderEditNo" runat="server" Text="No" Visible="false"
                    OnClick="lnkConnectedOrderEditNo_Click">
                </asp:LinkButton>
            </label>
        </p>
        <div style="width: 300px; max-height: 350px; overflow: auto;" id="Div1" runat="server">
            <asp:Panel ID="pnlConnectedOrderEdit" runat="server" Visible="false">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rdioConnectedOrderEdit" AutoPostBack="true" RepeatDirection="Horizontal"
                            CssClass="mrg_radio" runat="server" OnSelectedIndexChanged="rdioConnectedOrderEdit_SelectedIndexChanged">
                            <asp:ListItem Value="8" Text="Interim Order" />
                            <asp:ListItem Value="9" Text="Final Order" />
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="rdioConnectedOrderEdit" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <p id="PConnectedYearEdit" runat="server" visible="false">
                <label for="<%=drpConnectedOrderYearEdit.ClientID %>">
                    Year<span class="redtext">* </span>:
                    <asp:DropDownList ID="drpConnectedOrderYearEdit" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="drpConnectedOrderYearEdit_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
            </p>
            <p id="PConnectedOrderIdEdit" runat="server" visible="false">
                <label for="<%=drpConnectedOrderIdEdit.ClientID %>">
                    Order<span class="redtext">* </span>:
                    <asp:DropDownList ID="drpConnectedOrderIdEdit" runat="server" Width="100%" OnPreRender="drpConnectedOrderIdEdit_PreRender">
                    </asp:DropDownList>
                </label>
            </p>
        </div>
        <p>
            <label for="<%=txtApplicantName_Edit.ClientID %>">
                Petitioner's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantName_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionName_Edit" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantName_Edit" ErrorMessage="Please enter petitioner's name."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPetitionerNameLength" runat="server" ValidationExpression="^[\s\S]{3,2000}$"
                ControlToValidate="txtApplicantName_Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regNameEdit" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtApplicantName_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtPetitionerAddr_Edit.ClientID %>">
                Petitioner's Address:</label>
            <asp:TextBox ID="txtPetitionerAddr_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerAddrlength" runat="server" ValidationExpression="^[\s\S]{3,2000}$"
                ControlToValidate="txtPetitionerAddr_Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regAdderssEdit" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtPetitionerAddr_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtApplicantMobileNo_Edit.ClientID %>">
                Petitioner's Mobile No :</label>
            <asp:TextBox ID="txtApplicantMobileNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter valid Mobile number."
                ControlToValidate="txtApplicantMobileNo_Edit" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantPhoneNo_Edit.ClientID %>">
                Petitioner's Phone No :</label>
            <asp:TextBox ID="txtApplicantPhoneNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtApplicantPhoneNo_Edit" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantFaxNo_Edit.ClientID %>">
                Petitioner's Fax No :</label>
            <asp:TextBox ID="txtApplicantFaxNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtApplicantFaxNo_Edit" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantEmailID_Edit.ClientID %>">
                Petitioner's Email ID :</label>
            <asp:TextBox ID="txtApplicantEmailID_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="reg_EmailID_Edit" runat="server" ControlToValidate="txtApplicantEmailID_Edit"
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
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentName_Edit"
                ID="RegularExpressionValidatorEdit" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regnameEdittext" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRespondentName_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentAddr_Edit.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAddr_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator150" runat="server"
                ValidationExpression="^[\s\S]{3,2000}$" ControlToValidate="txtRespondentAddr_Edit"
                ErrorMessage="Minimum 3 and maximum 2000 characters required." ValidationGroup="Update"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regAddressEdit" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRespondentAddr_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentMobileNo_Edit.ClientID %>">
                Respondent's Mobile No :</label>
            <asp:TextBox ID="txtRespondentMobileNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                ControlToValidate="txtRespondentMobileNo_Edit" ErrorMessage="Please enter valid Mobile number."
                ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$" ValidationGroup="Update"
                Display="Dynamic">
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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
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
            <label for="<%=txtApplicantSubject_Edit.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantSubject_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Update"
                runat="server" SetFocusOnError="true" ControlToValidate="txtApplicantSubject_Edit"
                ErrorMessage="Please enter subject." Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                ValidationExpression="^[\s\S]{3,2000}$" ControlToValidate="txtApplicantSubject_Edit"
                ErrorMessage="Minimum 3 and maximum 2000 characters required." ValidationGroup="Update"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regSubjectEdit" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtApplicantSubject_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= ddlAppealStatus_Edit.ClientID %>">
                Select Appeal Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlAppealStatus_Edit" runat="server" OnSelectedIndexChanged="ddlAppealStatus_Edit_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAppealStatus" runat="server" ControlToValidate="ddlAppealStatus_Edit"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select appeal status."
                    ValidationGroup="Update"></asp:RequiredFieldValidator>
            </label>
        </p>
        <div style="width: 200px; max-height: 350px; overflow: auto;" id="divConnectEdit1"
            runat="server">
            <asp:Panel ID="Panel1" runat="server" Visible="false">
                <p id="P4" runat="server">
                    <label for="<%=ddlYearEdit1.ClientID %>">
                        Year<span class="redtext">* </span>:
                        <asp:CheckBoxList ID="ddlYearEdit1" runat="server" CssClass="checkbox" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlYearEdit1_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </label>
                </p>
                <asp:UpdatePanel ID="pnlEditConnectedPetition1" runat="server">
                    <ContentTemplate>
                        <asp:CheckBoxList ID="chklstPetitionEdit1" CssClass="checkbox" runat="server" RepeatColumns="1"
                            RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="chklstPetitionEdit1_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <p>
            <div id="FileUploadContainer">
                <label for="<%=TextBox2.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fuPetition_Edit" runat="server" />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidatePetition_Edit"
                    ControlToValidate="fuPetition_Edit" ValidationGroup="Update" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic" OnServerValidate="cvAppealEdit_ServerValidate">
                </asp:CustomValidator>
                <!--FileUpload Controls will be added here -->
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
                        <asp:HiddenField ID="hiddenFieldAppealId" runat="server" Value='<%#Eval("AppealId") %>' />
                        <b>
                            <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label>
                        </b>
                        <asp:Label ID="lblComments" runat="server" Text='<%#bind("Comments") %>'></asp:Label>
                        <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandArgument='<%# Eval("Id") %>'
                            CommandName="File">Remove File</asp:LinkButton>
                        <asp:Literal ID="ltrlDownload" runat="server">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:DataList>
            </p>
            <p id="pPetitionAppeal_Edit" runat="server" visible="false">
                <label for="<%= fileAppeal_Edit.ClientID %>">
                    Upload File :</label>
                <asp:FileUpload ID="fileAppeal_Edit" runat="server" />
                <asp:CustomValidator ID="cus_Appeal_Edit" runat="server" ClientValidationFunction="ValidateFileUpload"
                    ControlToValidate="fileAppeal_Edit" Display="Dynamic" ErrorMessage="Please select only .pdf file."
                    OnServerValidate="cus_Appeal_Edit_ServerValidate" ValidationGroup="Update">
                </asp:CustomValidator>
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <br />
            </p>
            <p id="pJudgementDesc_Edit" runat="server">
                <label for="<%=txtJudgementDesc_Edit.ClientID %>">
                    Judgement Description:</label>
                <asp:TextBox ID="txtJudgementDesc_Edit" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regJudgementEdit" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtJudgementDesc_Edit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtJudgementDesc_Edit"
                    Display="Dynamic" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Description upto 2000 characters are allowed.</span></em>
            </p>
            <p id="pJudgement_Edit" runat="server">
                <label for="<%= txtjudgementLink_Edit.ClientID %>">
                    Judgement Link :</label>
                <asp:TextBox ID="txtjudgementLink_Edit" runat="server" CssClass="text-input medium-input"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtjudgementLink_Edit"
                    Display="Dynamic" ErrorMessage="Minimum 5 and maximum 300 characters required."
                    ValidationExpression="^[\s\S]{5,300}$" ValidationGroup="Update">
                </asp:RegularExpressionValidator>
                <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                    Tip:-http://www.herc.gov.in </span></em>
                <asp:RegularExpressionValidator ID="RevLinkUrl" runat="server" ControlToValidate="txtjudgementLink_Edit"
                    Display="Dynamic" ErrorMessage="Please enter valid url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
            </p>
            <p>
                <label for="<%= txtRemarksEdit.ClientID %>">
                    Remarks :</label>
                <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRemarksEdit"
                    ID="RegularExpressionValidator18" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regRemarksEdit" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRemarksEdit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Remarks upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <label for="<%=txtMetaKeywordEdit.ClientID %>">
                    Meta Keyword <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaKeywordEdit" runat="server" MaxLength="2000" TextMode="MultiLine"
                    CssClass="text-input medium-input"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                    ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                    ControlToValidate="txtMetaKeywordEdit" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*"
                    ValidationGroup="Update" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeywordEdit"
                    ID="RegularExpressionValidator23" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMetaKeywordEdit"
                    ErrorMessage="Please enter Meta Keywords." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Keywords upto 2000 characters are allowed with comma or space.</span></em>
            </p>
            <p>
                <label for="<%=txtMetaDescriptionEdit.ClientID %>">
                    Meta Description <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaDescriptionEdit" runat="server" MaxLength="2000" TextMode="MultiLine"
                    CssClass="text-input medium-input"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaDescriptionEdit"
                    ID="RegularExpressionValidator24" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMetaDescriptionEdit"
                    ErrorMessage="Please enter Meta Description." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetadescri" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtMetaDescriptionEdit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
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
                <asp:TextBox ID="txtMetaTitleEdit" runat="server" MaxLength="2000" TextMode="MultiLine"
                    CssClass="text-input medium-input"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaTitleEdit"
                    ID="RegularExpressionValidator25" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMetaTitleEdit"
                    ErrorMessage="Please enter Meta Title." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetatitles" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtMetaTitleEdit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Title upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <asp:Button ID="btnUpdateAppeal" runat="server" CssClass="button" OnClick="btnUpdateAppeal_Click"
                    Text="Update" ValidationGroup="Update" />
                <asp:Button ID="btnReset_Edit" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnReset_Edit_Click" Text="Reset" />
                <asp:Button ID="btnBack_Edit" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnBack_Edit_Click" Text="Back" />
            </p>
    </asp:Panel>
</asp:Content>
