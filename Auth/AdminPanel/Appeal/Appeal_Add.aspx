<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Appeal_Add.aspx.cs" Inherits="Auth_AdminPanel_Appeal_Appeal_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlAppealAdd" runat="server">
        <p id="PDepartment" runat="server">
            <label for="<%= ddlDepartment.ClientID %>">
                Select Department<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>
            </label>
        </p>
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
            <label for="<%=txtAppealNo.ClientID %>">
                Appeal Number <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtAppealNo" CssClass="text-input small-input" runat="server" MaxLength="4"> </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAppealno" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtAppealNo" ErrorMessage="Please enter appeal number."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusReferenceNo" runat="server" ErrorMessage="Appeal number already exist. Please enter another appeal number."
                OnServerValidate="cusAppealNo_ServerValidate" ValidationGroup="Add" Display="Dynamic">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regYear_Edit" runat="server" ErrorMessage="Appeal number must be between 1 to 9999 characters and only numbers are applicable."
                ControlToValidate="txtAppealNo" ValidationExpression="^[1-9][0-9]{0,3}$" ValidationGroup="Add"
                Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtApplicationDate.ClientID %>">
                Appeal Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtApplicationDate" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibApplicationDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvApplicationDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtApplicationDate" ErrorMessage="Please enter application date."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rxvApplicationDate" runat="server" ControlToValidate="txtApplicationDate"
                ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CusomValidator1" runat="server" ControlToValidate="txtApplicationDate"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator1_ServerValidate"
                ValidationGroup="Add" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplicationDate"
                PopupButtonID="ibApplicationDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplicationDate">
            </cc1:CalendarExtender>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Appellant's Details </span>
        </p>
        <p>
            <label for="<%=txtApplicantName.ClientID %>">
                Appellant's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantName" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApplicantName" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantName" ErrorMessage="Please enter applicant's name."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regApplicantName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtApplicantName" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantName"
                ID="regApplicantNam" ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="Add"
                ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtApplicantAdd.ClientID %>">
                Appellant's Address <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantAdd" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApplicantAddress" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantAdd" ErrorMessage="Please enter address."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantAdd"
                ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regApplicantAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtApplicantAdd" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtApplicantMobileNo.ClientID %>">
                Appellant's Mobile No :</label>
            <asp:TextBox ID="txtApplicantMobileNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid Mobile number."
                ControlToValidate="txtApplicantMobileNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don’t start with ‘0’ (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantPhoneNo.ClientID %>">
                Appellant's Phone No :</label>
            <asp:TextBox ID="txtApplicantPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantPhoneNo" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtApplicantPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without ‘0’). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantFaxNo.ClientID %>">
                Appellant's Fax No :</label>
            <asp:TextBox ID="txtApplicantFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantFaxNo" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtApplicantFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without ‘0’). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantEmailID.ClientID %>">
                Appellant's Email ID :</label>
            <asp:TextBox ID="txtApplicantEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantEmailid" runat="server" ControlToValidate="txtApplicantEmailID"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Add">
            </asp:RegularExpressionValidator>
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
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentName"
                ID="regRespondentNam" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regRespondent" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtRespondentName" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtRespondentAddress.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAddress" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentAddress"
                ID="regRespondentAddres" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regRespondentadd" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtRespondentAddress" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtRespondentMobileNo.ClientID %>">
                Respondent's Mobile No :</label>
            <asp:TextBox ID="txtRespondentMobileNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentMobileNo" runat="server" ErrorMessage="Please enter valid Mobile number."
                ControlToValidate="txtRespondentMobileNo" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don’t start with ‘0’ (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentPhoneNo.ClientID %>">
                Respondent's Phone No :</label>
            <asp:TextBox ID="txtRespondentPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentPhoneNo" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtRespondentPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without ‘0’). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentFaxNo.ClientID %>">
                Respondent's Fax No :</label>
            <asp:TextBox ID="txtRespondentFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentFaxNo" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtRespondentFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without ‘0’). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRespondentEmailID.ClientID %>">
                Respondent's Email ID :</label>
            <asp:TextBox ID="txtRespondentEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regRespondentEmailId" runat="server" ControlToValidate="txtRespondentEmailID"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtAppealSubject.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtAppealSubject" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAppealSubject" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtAppealSubject" ErrorMessage="Please enter subject."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAppealSubject"
                ID="regRespondentAddr" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regRespondntsubject" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtAppealSubject" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Subject upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%=txtremarks.ClientID %>">
                Remarks</label>
            <asp:TextBox ID="txtremarks" CssClass="text-input medium-input" runat="server" MaxLength="2000"
                TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtremarks"
                ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regAppealRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtremarks" SetFocusOnError="true" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Remarks upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%= lblAppealStatus.ClientID %>">
                Appeal Status <span class="redtext"></span>:
                <asp:Label ID="lblAppealStatus" runat="server" Text="In Process"></asp:Label>
            </label>
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
            <asp:RegularExpressionValidator ID="regMetadescr" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
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
            <asp:Button ID="btnAddAppeal" runat="server" CssClass="button" Text="Submit" ValidationGroup="Add"
                OnClick="btnAddAppeal_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
                OnClick="btnReset_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
                OnClick="btnBack_Click" />
        </p>
    </asp:Panel>
</asp:Content>
