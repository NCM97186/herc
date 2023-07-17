<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="RTI_FAA_Edit.aspx.cs" Inherits="Auth_AdminPanel_RTI_RTI_FAA_Edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SyrinxCkEditor" Namespace="Syrinx.Gui.AspNet" TagPrefix="syx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlRTIAdd" runat="server">
        <p>
            <asp:Label ID="lblReferenceNo" CssClass="greentext" Font-Size="Medium" Text="RTI Reference Number :"
                runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%=txtFAppletAuthority.ClientID %>">
                FAA Reference Number<span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtFAppletAuthority" CssClass="text-input small-input" runat="server"
                MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtFAppletAuthority" ErrorMessage="Please enter FAA Reference Number."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regReferenceNoEdit" runat="server" ControlToValidate="txtFAppletAuthority"
                ErrorMessage="Reference number must be between 1 to 9999." ValidationExpression="^[1-9][0-9]{0,3}$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cusReferenceNo" runat="server" Display="Dynamic" ErrorMessage="FAA reference number already exist. Please enter another reference number."
                OnServerValidate="cusReferenceNo_ServerValidate" ValidationGroup="Add">
            </asp:CustomValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-1 to 9999</span></em>
        </p>
        <p>
            <label for="<%= txtApplicationDate.ClientID %>">
                Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtApplicationDate" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibApplicationDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvApplicationDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtApplicationDate" Display="Dynamic" ErrorMessage="Please enter application date.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rxvApplicationDate" runat="server" ControlToValidate="txtApplicationDate"
                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add">
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
            <span class="">Applicant's Details </span>
        </p>
        <p>
            <label for="<%=txtApplicantName.ClientID %>">
                Applicant's Name <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantName" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApplicantName" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantName" ErrorMessage="Please enter applicant's name."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantName"
                ID="regAppliNameLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regApplicantName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtApplicantName" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtApplicantAdd.ClientID %>">
                Applicant's Address <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtApplicantAdd" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApplicantAddress" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtApplicantAdd" ErrorMessage="Please enter address."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regApplicantAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtApplicantAdd" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtApplicantAdd"
                ID="regApplicAddressLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtApplicantMobileNo.ClientID %>">
                Applicant's Mobile No :</label>
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
                Applicant's Phone No :</label>
            <asp:TextBox ID="txtApplicantPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantPhoneNo" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtApplicantPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantFaxNo.ClientID %>">
                Applicant's Fax No :</label>
            <asp:TextBox ID="txtApplicantFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantFaxNo" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtApplicantFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtApplicantEmailID.ClientID %>">
                Applicant's Email ID :</label>
            <asp:TextBox ID="txtApplicantEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantEmailid" runat="server" ControlToValidate="txtApplicantEmailID"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtRtiSubject.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtRtiSubject" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvRtiSubject" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtRtiSubject" ErrorMessage="Please enter subject."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRtiSubject"
                ID="regrtSubject" ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="Add"
                ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regSubject" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtRtiSubject" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtremarks_Edit.ClientID %>">
                Remarks</label>
            <asp:TextBox ID="txtremarks_Edit" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtremarks_Edit"
                ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regRtiRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtremarks_Edit" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Remarks upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%= ddlRTIFAAStatus_Edit.ClientID %>">
                RTI FAA Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlRTIFAAStatus_Edit" runat="server" Enabled="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_Status_Edit" runat="server" ControlToValidate="ddlRTIFAAStatus_Edit"
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
                ValidationGroup="Add" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeyword"
                ID="regMetakeywordLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="metakeys" runat="server" ControlToValidate="txtMetaKeyword"
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
            <asp:RequiredFieldValidator ID="metades" runat="server" ControlToValidate="txtMetaDescription"
                ErrorMessage="Please enter Meta Description." ValidationGroup="Add" Display="Dynamic"></asp:RequiredFieldValidator>
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
                ID="RegularExpressionValidator18" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                ErrorMessage="Please enter Meta Title." ValidationGroup="Add" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regmetatitel" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaTitle"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                  <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Title upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <asp:Button ID="btnUpdateRtiFaa" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
                OnClick="btnUpdateRtiFaa_Click" ToolTip="Click To Update" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
                OnClick="btnReset_Click" Style="height: 26px" ToolTip="Click To Reset" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
                OnClick="btnBack_Click" ToolTip="Go Back" />
        </p>
    </asp:Panel>
</asp:Content>
