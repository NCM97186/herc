<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="PetitionReview_Insert_Update.aspx.cs" Inherits="Auth_AdminPanel_Petition_Management_PetitionReview_Insert_Update" %>

<%@ Register Assembly="SyrinxCkEditor" Namespace="Syrinx.Gui.AspNet" TagPrefix="syx" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= fileReviewPetition.ClientID %>');
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

        function ValidatePublicNotice(Source, args) {
            var fuData = document.getElementById('<%= fileReviewPublicNotice.ClientID %>');
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
            var fuData = document.getElementById('<%= fileReviewPetition_Edit.ClientID %>');
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

        function ValidatePublicNotice_Edit(Source, args) {
            var fuData = document.getElementById('<%= fileReviewPublicNotice_Edit.ClientID %>');
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
    <p id="PLblPetitionPRO" runat="server" visible="false">
        <asp:Label ID="LblPetitionPRO" CssClass="greentext" Font-Size="Medium" Text="Petition PRO Number :"
            runat="server"></asp:Label>
    </p>
    <asp:Panel ID="pnlPetitionAdd" runat="server">
        <p>
            <asp:Label ID="lblmsg" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtPetitionRPno.ClientID %>">
                RA Number <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionRPno" CssClass="text-input small-input" runat="server"
                MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionRPno" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtPetitionRPno" ErrorMessage="Please enter petition RA number."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusPetitionRPno" runat="server" CssClass="validation" Display="Dynamic"
                ErrorMessage="RA number already exist. Please enter another RA number." OnServerValidate="cusPetitionRPno_ServerValidate"
                ValidationGroup="Add">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regPetitionRPno" runat="server" ControlToValidate="txtPetitionRPno"
                ErrorMessage="RA number must be between 1 to 9999." ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtPetitionDate.ClientID %>">
                Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtPetitionDate" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibPetitionDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvStartDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtPetitionDate" ErrorMessage="Please enter review petition date."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPetitionDate" runat="server" ControlToValidate="txtPetitionDate"
                ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtPetitionDate"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator2_ServerValidate"
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
        <p>
            <label for="<%= lnkConnectedOrder.ClientID %>" style="color: red;">
                Do you want to connect this review petition with Order? :
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
                    <asp:DropDownList ID="drpConnectedOrderId" runat="server" Width="100%">
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
            <asp:RegularExpressionValidator ID="regtxtRemarksAdd" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
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
                ID="RegularExpressionValidator10" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regPetitionerAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
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
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentName"
                ID="RegularExpressionValidator51" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regRespondentName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
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
            <asp:RegularExpressionValidator ID="regRespondentAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
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
            <asp:RegularExpressionValidator ID="regSubjectLength" runat="server" ValidationExpression="^[\s\S]{3,2000}$"
                ControlToValidate="txtPetReviewSubject" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regSubjectPetition" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetReviewSubject"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= ddlPetReviewStatus.ClientID %>" style="color: red;">
                Is there any connected Review petition ? :
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
                            RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="chklstPetition_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <p>
            <label for="<%= ddlPetReviewStatus.ClientID %>">
                Review Petition Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlPetReviewStatus" runat="server" Enabled="false">
                    <asp:ListItem Value="10" Selected="True">In Process</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPetReviewStatus" runat="server" ControlToValidate="ddlPetReviewStatus"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select petition review status."
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
            </label>
        </p>
        <p id="pPetition_Review" runat="server">
            <div id="FileUploadContainer">
                <label for="<%=txtRemarks.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fileReviewPetition" runat="server" />
                 <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidatePublicNotice"
                    ControlToValidate="fileReviewPetition" ValidationGroup="Add" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic" OnServerValidate="cvPublicNotice_ServerValidate">
                </asp:CustomValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regFileDesc" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="TextBox1"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
               
                <%-- <asp:FileUpload ID="FileUpload1" runat="server" />--%>
                <!--FileUpload Controls will be added here -->
            </div>
            <input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
                tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
            <p>
            </p>
            <p>
                <label for="<%= txtURL.ClientID %>">
                    URLs :</label>
                <asp:TextBox ID="txtURL" runat="server" CssClass="text-input medium-input" MaxLength="2000"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regtxtURL" runat="server" ControlToValidate="txtURL"
                    Display="Dynamic" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="RegtxtAppealLink" runat="server" ControlToValidate="txtURL"
                    Display="Dynamic" ErrorMessage="Please enter valid url" ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?([;]\s*(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?)*$"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
                <!--End-->
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-You may enter multiple URL separated by semicolon(;) (eg:- http://www.abc.com).</span></em>
            </p>
            <p>
                <label for="<%= txtURLDescription.ClientID %>">
                    URLs Descriptions:</label>
                <asp:TextBox ID="txtURLDescription" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLDescription"
                    ID="RegularExpressionValidator29" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                    ErrorMessage="Please enter valid URL description. No special characters are allowed except (space,'-_.:;#()/)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtURLDescription"
                    ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/ ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-You may enter description for multiple URLs separated by semicolon(;).</span></em>
            </p>
            <p id="PRemarks" runat="server">
                <label for="<%= txtRemarks.ClientID %>">
                    Remarks :</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" MaxLength="2000"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                    ControlToValidate="txtRemarks" Display="Dynamic" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regRemarksPetition" runat="server" ControlToValidate="txtRemarks"
                    Display="Dynamic" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&amp;)"
                    SetFocusOnError="True" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
                <!--End-->
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Remarks upto 2000 characters are allowed.</span></em>
            </p>
            <p id="ppublicnoiotice" runat="server" visible="false">
                <label for="<%= fileReviewPublicNotice.ClientID %>">
                    Upload Public Notice :</label>
                <asp:FileUpload ID="fileReviewPublicNotice" runat="server" />
                <asp:CustomValidator ID="cvPublicNotice" runat="server" ClientValidationFunction="ValidatePublicNotice"
                    ControlToValidate="fileReviewPublicNotice" Display="Dynamic" ErrorMessage="Please select only .pdf file."
                    OnServerValidate="cvPublicNotice_ServerValidate" ValidationGroup="Add">
                </asp:CustomValidator>
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
            </p>
            <p id="pdate" runat="server" visible="false">
                <label for="<%= txtHearingDate.ClientID %>">
                    Schedule of Hearing <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtHearingDate" runat="server" CssClass="text-input small-input"
                    MaxLength="10"></asp:TextBox>
                <asp:ImageButton ID="imgDate" runat="server" CausesValidation="false" ImageUrl="~/Auth/AdminPanel/images/cal.jpg" />
                <asp:RegularExpressionValidator ID="regHearingDate" runat="server" ControlToValidate="txtHearingDate"
                    Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                    ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-dd/mm/yyyy</span></em>
            </p>
            <p>
                <label for="<%=txtMetaKeyword.ClientID %>">
                    Meta Keyword <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaKeyword" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RevMetaKeyword" runat="server" ControlToValidate="txtMetaKeyword"
                    Display="Dynamic" ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*" ValidationGroup="Add"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="regMetakeywordLength" runat="server" ControlToValidate="txtMetaKeyword"
                    Display="Dynamic" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Add"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMetaKeyword"
                    ErrorMessage="Please enter Meta Keywords." ValidationGroup="Add"></asp:RequiredFieldValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Keywords upto 2000 characters are allowed with comma or space.</span></em>
            </p>
            <p>
                <label for="<%=txtMetaDescription.ClientID %>">
                    Meta Description <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regMetaDescriptionLength" runat="server" ControlToValidate="txtMetaDescription"
                    Display="Dynamic" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Add"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMetaDescription"
                    ErrorMessage="Please enter Meta Description." ValidationGroup="Add"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetadescrip" runat="server" ControlToValidate="txtMetaDescription"
                    Display="Dynamic" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&amp;)"
                    SetFocusOnError="True" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
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
                <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                    ControlToValidate="txtMetaTitle" Display="Dynamic" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{3,2000}$" ValidationGroup="Add"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                    ErrorMessage="Please enter Meta Title." ValidationGroup="Add"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmettitle" runat="server" ControlToValidate="txtMetaTitle"
                    Display="Dynamic" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&amp;)"
                    SetFocusOnError="True" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Title upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <cc1:CalendarExtender ID="calExHearing" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDate"
                    TargetControlID="txtHearingDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="calExHearing1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHearingDate">
                </cc1:CalendarExtender>
            </p>
            <p>
                <asp:Button ID="btnAddReviewPetition" runat="server" CssClass="button" OnClick="btnAddReviewPetition_Click"
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
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
        <p>
            <asp:Label ID="lblMsg_Edit" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtPetitionRPno_Edit.ClientID %>">
                RA Number <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtPetitionRPno_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionRPno_number" ValidationGroup="Update"
                runat="server" SetFocusOnError="true" ControlToValidate="txtPetitionRPno_Edit"
                ErrorMessage="Please enter petition RA number." Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cus_PetitionRPno_Edit" runat="server" Display="Dynamic"
                ErrorMessage="RA number already exist. Please enter another RA number." OnServerValidate="cus_PetitionPROno_Edit_ServerValidate"
                ValidationGroup="Update">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPetitionRPno_Edit"
                ErrorMessage="RA number must be between 1 to 9999." ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtPetitionDateEdit.ClientID %>">
                Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtPetitionDateEdit" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Update"
                SetFocusOnError="true" runat="server" ControlToValidate="txtPetitionDateEdit"
                ErrorMessage="Please enter review petition date." Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPetitionDateEdit"
                ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtPetitionDateEdit"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator2_ServerValidate"
                ValidationGroup="Update" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <label for="<%= lnkConnectedOrderEdit.ClientID %>" style="color: red;">
                Do you want to connect this review petition with Order? :
                <asp:LinkButton ID="lnkConnectedOrderEdit" runat="server" Text="Yes" OnClick="lnkConnectedOrderEdit_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedOrderEditNo" runat="server" Text="No" Visible="false"
                    OnClick="lnkConnectedOrderEditNo_Click">
                </asp:LinkButton>
            </label>
        </p>
        <div style="width: 487px; max-height: 350px; overflow: auto;" id="Div1" runat="server">
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
                    <asp:DropDownList ID="drpConnectedOrderIdEdit" runat="server" OnSelectedIndexChanged="drpConnectedOrderIdEdit_SelectedIndexChanged"
                        Width="100%">
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
            <asp:RegularExpressionValidator ID="regPetitionerNameEdit" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
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
            <asp:RegularExpressionValidator ID="regPetAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtPetitionerAddr_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtApplicantMobileNo_Edit.ClientID %>">
                Petitioner's Mobile No :</label>
            <asp:TextBox ID="txtApplicantMobileNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter valid Mobile number."
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
            <asp:RegularExpressionValidator ID="regRespondntName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtRespondentName_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentAddr_Edit.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAddr_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentAddr" runat="server" ValidationExpression="^[\s\S]{3,2000}$"
                ControlToValidate="txtRespondentAddr_Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regRespondentAddressEdit" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
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
                ValidationExpression="^[\s\S]{5,2000}$" ControlToValidate="txtApplicantSubject_Edit"
                ErrorMessage="Minimum 5 and maximum 2000 characters required." ValidationGroup="Update"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regSubjectEdit" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtApplicantSubject_Edit"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%= ddlPetReviewStatus_Edit.ClientID %>" style="color: red;">
                Is there any connected Review Petition ? :
                <asp:LinkButton ID="lnkConnectedPetitionEdit" runat="server" Text="Yes" OnClick="lnkConnectedPetitionEdit_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedPetitionEditNo" runat="server" Text="No" OnClick="lnkConnectedPetitionEditNo_Click">
                </asp:LinkButton>
            </label>
        </p>
        <div style="width: 487px; max-height: 350px; overflow: auto" id="divConnectEdit"
            runat="server">
            <asp:Panel ID="UpdatePanel1" runat="server">
                <p id="P2" runat="server">
                    <label for="<%=ddlYearEdit.ClientID %>">
                        Year<span class="redtext">* </span>:
                        <asp:CheckBoxList ID="ddlYearEdit" runat="server" CssClass="checkbox" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlYearEdit_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </label>
                </p>
                <asp:UpdatePanel ID="pnlEditConnectedPetition" runat="server">
                    <ContentTemplate>
                        <div class="chekbox-value1" style="color: Red; font-weight: bold">
                            <asp:Literal ID="ltrlSelectedEdit" runat="server"></asp:Literal></div>
                        <asp:CheckBoxList ID="chklstPetitionEdit" runat="server" CssClass="checkbox" RepeatColumns="1"
                            RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chklstPetitionEdit_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <p>
            <label for="<%= ddlPetReviewStatus_Edit.ClientID %>">
                Review Petition Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlPetReviewStatus_Edit" runat="server" OnSelectedIndexChanged="ddlPetReviewStatus_Edit_SelectedIndexChanged"
                    AutoPostBack="true" Enabled="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_Status_Edit" runat="server" ControlToValidate="ddlPetReviewStatus_Edit"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select petition review status."
                    ValidationGroup="Update"></asp:RequiredFieldValidator>
            </label>
        </p>
        <p id="pPetition_Review_Edit" runat="server">
            <div id="FileUploadContainer">
                <label for="<%=txtRemarks.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fileReviewPetition_Edit" runat="server" />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="cu_ReviewPetition_Edit" runat="server" ClientValidationFunction="ValidateFileUpload_Edit"
                    ControlToValidate="fileReviewPetition_Edit" ValidationGroup="Update" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic" OnServerValidate="cu_ReviewPetition_Edit_ServerValidate">
                </asp:CustomValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regFileDescriptionEdit" runat="server" ErrorMessage="Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="TextBox2"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <%-- <asp:FileUpload ID="FileUpload1" runat="server" />--%>
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
                        <asp:HiddenField ID="hiddenFieldRpID" runat="server" Value='<%#Eval("RP_Id") %>' />
                        <b>
                            <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label>
                        </b>
                        <%--  <asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>--%>
                        <asp:Label ID="lblComments" runat="server" Text='<%#bind("Comment") %>'></asp:Label>
                        <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandArgument='<%# Eval("Id") %>'
                            CommandName="File">Remove File</asp:LinkButton>
                        <asp:Literal ID="ltrlDownload" runat="server">
                        </asp:Literal>
                    </ItemTemplate>
                </asp:DataList>
            </p>
            <p>
                <label for="<%= txtURLEdit.ClientID %>">
                    URLs :</label>
                <asp:TextBox ID="txtURLEdit" runat="server" CssClass="text-input medium-input" MaxLength="2000"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLEdit"
                    ID="RegularExpressionValidator24" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
                </asp:RegularExpressionValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server"
                    ControlToValidate="txtURLEdit" Display="Dynamic" ErrorMessage="Please enter valid url"
                    ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?([;]\s*(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)*$"
                    ValidationGroup="Update"></asp:RegularExpressionValidator>
                <!--End-->
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-You may enter multiple URL separated by semicolon(;) (eg:- http://www.abc.com).</span></em>
            </p>
            <p>
                <label for="<%= txtURLDescriptionEdit.ClientID %>">
                    URLs Descriptions:</label>
                <asp:TextBox ID="txtURLDescriptionEdit" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLDescriptionEdit"
                    ID="RegularExpressionValidator27" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server"
                    ErrorMessage="Please enter valid URL description. No special characters are allowed except (space,'-_.:;#()/)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtURLDescriptionEdit"
                    ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/ ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-You may enter description for multiple URLs separated by semicolon(;).</span></em>
            </p>
            <p id="P1" runat="server">
                <label for="<%= txtRemarksEdit.ClientID %>">
                    Remarks :</label>
                <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="text-input medium-input"
                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRemarksEdit"
                    ID="RegularExpressionValidator18" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
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
            <p id="PUploadEdit" runat="server" visible="false">
                <label for="<%= fileReviewPublicNotice_Edit.ClientID %>">
                    Upload Public Notice :</label>
                <asp:FileUpload ID="fileReviewPublicNotice_Edit" runat="server" />
                <asp:Label ID="lblOldPublicNotice" runat="server" Text="Old File :"></asp:Label>
                <asp:Label ID="lblPublicNotice" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                &nbsp;<asp:LinkButton ID="lnkPublicNoticeRemove" runat="server" OnClick="lnkPublicNoticeRemove_Click">Remove File</asp:LinkButton><br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
            </p>
            <p id="pSOHEdit" runat="server" visible="false">
                <label for="<%= txtHearingDate_Edit.ClientID %>">
                    Schedule of Hearing <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtHearingDate_Edit" runat="server" CssClass="text-input small-input"
                    MaxLength="10"></asp:TextBox>
                <asp:ImageButton ID="img_CalImage" runat="server" CausesValidation="false" CssClass="error"
                    EnableClientScript="false" ForeColor="" ImageUrl="~/Auth/AdminPanel/images/cal.jpg" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtHearingDate_Edit"
                    Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                    ValidationGroup="Update">
                </asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-dd/mm/yyyy</span></em>
            </p>
            <p>
                <label for="<%=txtMetaKeywordEdit.ClientID %>">
                    Meta Keyword <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtMetaKeywordEdit" runat="server" MaxLength="2000" TextMode="MultiLine"
                    CssClass="text-input medium-input"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                    ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                    ControlToValidate="txtMetaKeywordEdit" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*"
                    ValidationGroup="Update" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeywordEdit"
                    ID="RegularExpressionValidator21" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
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
                    ID="RegularExpressionValidator22" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMetaDescriptionEdit"
                    ErrorMessage="Please enter Meta Description." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetadescedit" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
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
                    ID="RegularExpressionValidator23" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                    ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMetaTitleEdit"
                    ErrorMessage="Please enter Meta Title." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server"
                    ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtMetaTitleEdit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Title upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="img_CalImage"
                    TargetControlID="txtHearingDate_Edit">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHearingDate_Edit">
                </cc1:CalendarExtender>
            </p>
            <p>
                <asp:Button ID="btnUpdateReviewPetition" runat="server" CssClass="button" OnClick="btnUpdateReviewPetition_Click"
                    Text="Update" ValidationGroup="Update" />
                <asp:Button ID="btnReset_Edit" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnReset_Edit_Click" Text="Reset" />
                <asp:Button ID="btnBack_Edit" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnBack_Edit_Click" Text="Back" />
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
