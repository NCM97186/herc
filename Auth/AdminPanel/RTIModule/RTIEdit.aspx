<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="RTIEdit.aspx.cs" Inherits="Auth_AdminPanel_RTI_RTIEdit" %>

<%@ Register Assembly="SyrinxCkEditor" Namespace="Syrinx.Gui.AspNet" TagPrefix="syx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlRTIEdit" runat="server">
        <p>
            <asp:Label ID="lblMsg_Edit" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtReferenceNo_Edit.ClientID %>">
                Reference Number <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtReferenceNo_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvReferenceNo_number" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtReferenceNo_Edit" ErrorMessage="Please enter reference number."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cus_ReferenceNo_Edit" runat="server" Display="Dynamic" ErrorMessage="Reference number already exists. Please enter another reference number."
                OnServerValidate="cus_ReferenceNo_Edit_ServerValidate" ValidationGroup="Update">
            </asp:CustomValidator>
            <asp:RegularExpressionValidator ID="regReferenceNoEdit" runat="server" ControlToValidate="txtReferenceNo_Edit"
                ErrorMessage="Reference number must be between 1 to 9999." ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Update" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtApplicationDate_Edit.ClientID %>">
                Application Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtApplicationDate_Edit" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibApplicationDate_Edit" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvApplicationDate_Edit" ValidationGroup="Update"
                SetFocusOnError="true" runat="server" ControlToValidate="txtApplicationDate_Edit"
                Display="Dynamic" ErrorMessage="Please enter Application date.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revApplicationDate_Edit" runat="server" ControlToValidate="txtApplicationDate_Edit"
                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Update">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CusomValidator1" runat="server" ControlToValidate="txtApplicationDate_Edit"
                ErrorMessage="Year of Date should be earlier or current" OnServerValidate="CusomValidator1_ServerValidate"
                ValidationGroup="Update" Display="Dynamic" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplicationDate_Edit"
                PopupButtonID="ibApplicationDate_Edit">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplicationDate_Edit">
            </cc1:CalendarExtender>
        </p>
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Applicant's Details </span>
        </p>
        <p>
            <label for="<%=txtApplicantName_Edit.ClientID %>">
                Applicant's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantName_Edit" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApplicantName_Edit" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantName_Edit" ErrorMessage="Please enter applicant's name."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regApplicantName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Update" ControlToValidate="txtApplicantName_Edit" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantName_Edit"
                ID="regApplicantNameLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtApplicantAddr_Edit.ClientID %>">
                Applicant's Address <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantAddr_Edit" CssClass="text-input medium-input" TextMode="MultiLine"
                runat="server" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApplicantAddr_Edit" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantAddr_Edit" ErrorMessage="Please enter address."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantAddr_Edit"
                ID="regApplicantAddrLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regApplicantAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Update" ControlToValidate="txtApplicantAddr_Edit" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtApplicantMobileNo_Edit.ClientID %>">
                Applicant's Mobile No :</label>
            <asp:TextBox ID="txtApplicantMobileNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revApplicantMobileNo_Edit" runat="server" ErrorMessage="Please enter valid Mobile number."
                ControlToValidate="txtApplicantMobileNo_Edit" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" />
            <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtApplicantMobileNo_Edit"
                ValidChars="0123456789">
            </cc1:FilteredTextBoxExtender>--%>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantPhoneNo_Edit.ClientID %>">
                Applicant's Phone No :</label>
            <asp:TextBox ID="txtApplicantPhoneNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revApplicantPhoneNo_Edit" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtApplicantPhoneNo_Edit" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantFaxNo_Edit.ClientID %>">
                Applicant's Fax No :</label>
            <asp:TextBox ID="txtApplicantFaxNo_Edit" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revApplicantFaxNo_Edit" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtApplicantFaxNo_Edit" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Update" />
            <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtApplicantFaxNo_Edit"
                ValidChars="0123456789">
            </cc1:FilteredTextBoxExtender>--%>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantEmailID_Edit.ClientID %>">
                Applicant's Email ID :</label>
            <asp:TextBox ID="txtApplicantEmailID_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="reg_EmailID_Edit" runat="server" ControlToValidate="txtApplicantEmailID_Edit"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                ErrorMessage="Please enter valid email id." ValidationGroup="Update"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRtiSubject_Edit.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtRtiSubject_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvRtiSubject_Edit" ValidationGroup="Update" runat="server"
                SetFocusOnError="true" ControlToValidate="txtRtiSubject_Edit" ErrorMessage="Please enter subject."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regRtiSubject" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Update" ControlToValidate="txtRtiSubject_Edit" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRtiSubject_Edit"
                ID="regRtiEditLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtremarks_Edit.ClientID %>">
                Remarks</label>
            <asp:TextBox ID="txtremarks_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtremarks_Edit"
                ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required.">

            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Update" ControlToValidate="txtremarks_Edit" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
          
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Remarks upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%= ddlRTIStatus_Edit.ClientID %>">
                RTI Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlRTIStatus_Edit" runat="server" Enabled="false" Width="250px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_Status_Edit" runat="server" ControlToValidate="ddlRTIStatus_Edit"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select rti status." ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:Label ID="lblUploadMsg" runat="server" Visible="false"></asp:Label>
            </label>
        </p>
        <p>
            <label for="<%=txtMetaKeyword.ClientID %>">
                Meta Keyword <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtMetaKeyword" runat="server" MaxLength="2000" TextMode="MultiLine"
                CssClass="text-input medium-input"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RevMetaKeyword" runat="server" ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                ControlToValidate="txtMetaKeyword" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*"
                ValidationGroup="Update" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeyword"
                ID="regMetakeywordLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="reqkeywords" runat="server" ControlToValidate="txtMetaKeyword"
                ErrorMessage="Please enter Meta Keywords." ValidationGroup="Update"></asp:RequiredFieldValidator>
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
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvMetaDesc" runat="server" ControlToValidate="txtMetaDescription"
                ErrorMessage="Please enter Meta Description." ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regmetadescrip" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtMetaDescription"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Description upto 2000 characters are allowed with comma or space.</span></em>
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
                ValidationGroup="Update" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                ErrorMessage="Please enter Meta Title." ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regmetatitel" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtMetaTitle"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Title upto 2000 characters are allowed with comma or space.</span></em>
        </p>
        <p>
            <asp:Button ID="btnRTIUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Update"
                OnClick="btnRTIUpdate_Click" ToolTip="Click To Update" />
            <asp:Button ID="btnReset_Edit" runat="server" Text="Reset" CausesValidation="False"
                CssClass="button" OnClick="btnReset_Edit_Click" ToolTip="Click To Reset" />
            <asp:Button ID="btnBack_Edit" runat="server" Text="Back" CausesValidation="False"
                CssClass="button" OnClick="btnBack_Edit_Click" ToolTip="Go Back" />
        </p>
    </asp:Panel>
</asp:Content>
