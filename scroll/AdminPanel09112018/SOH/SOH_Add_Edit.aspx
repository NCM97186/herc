<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="SOH_Add_Edit.aspx.cs" Inherits="Auth_AdminPanel_SOH_SOH_Add_Edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="Ajaxified" Assembly="Ajaxified" Namespace="Ajaxified" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SiteStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function clientShowing(sender) {

        }
        function clientShown(sender) {

        }
        function clientHiding(sender) {

        }
        function clientHidden(sender) {

        }
        function selectionChanged(sender) {
            //alert(sender._selectedTime);
        }
    </script>
    <script language="javascript" type="text/javascript">



        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= fileUploadPdf.ClientID %>');
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
    <script language="javascript" type="text/javascript">

        function ValidateFileUploadEdit(Source, args) {
            var fuData = document.getElementById('<%= fileUploadPdfEdit.ClientID %>');
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
        function AddFileUploadEdit() {
            var div = document.createElement('DIV');
            div.innerHTML = '<label for="txtRemarks' + counter + '" name = "name' + counter +
                     '" >File Description :</label>' +
                     '<input name="txt' + counter + '" value = "' +
                     '" type="text" />' +

                     '<input id="file' + counter + '" name = "file' + counter +
                     '" type="file" />' +
                     '<input id="Button' + counter + '" type="button" ' +
                     'value="Remove" onclick = "RemoveFileUpload(this)" />';


            document.getElementById("FileUploadContainerEdit").appendChild(div);
            counter++;
        }
        function RemoveFileUpload(div) {
            document.getElementById("FileUploadContainerEdit").removeChild(div.parentNode);


        }
    </script>
    <asp:Panel ID="pnlPetitionAdd" runat="server">
        <p id="PDepartment" runat="server" visible="true">
            <label for="<%= ddlDepartment.ClientID %>">
                Select Department<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </label>
        </p>
        <p id="Ppetitionappeal" runat="server" visible="True">
            <label for="<%= ddlpetitionappeal.ClientID %>">
                <asp:Label ID="lblselect" runat="server" ForeColor="Red" Text="Select"></asp:Label>
                <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlpetitionappeal" runat="server" OnSelectedIndexChanged="ddlpetitionappeal_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="1">Petition</asp:ListItem>
                </asp:DropDownList>
            </label>
        </p>
        <p id="PApeal" runat="server" visible="false">
            <label for="<%= ddlappeal.ClientID %>">
                <asp:Label ID="lblselectOmbudsman" runat="server" ForeColor="Red" Text="Select"></asp:Label>
                <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlappeal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlappeal_SelectedIndexChanged">
                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="2"> Appeal</asp:ListItem>
                </asp:DropDownList>
            </label>
        </p>
        <p>
            <asp:Label ID="lblmsg" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
        </p>
        <asp:UpdatePanel ID="updatepanel5" runat="server">
            <ContentTemplate>
                <div style="width: 487px; max-height: 350px; overflow: auto;" id="divConnectAdd1"
                    runat="server">
                    <asp:Panel ID="pnlDropdownlist" runat="server" Visible="false">
                        <%--<p class="floting">--%>
                        <p>
                            <label for="<%=ddlConnectionType.ClientID %>">
                                Select One <span class="redtext">* </span>:</label>
                            <asp:DropDownList ID="ddlConnectionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlConnectionType_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                                <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <asp:UpdatePanel ID="updatepanel4" runat="server">
                            <ContentTemplate>
                                <div class="chekbox-value" style="color: Red; font-weight: bold">
                                    <asp:Literal ID="ltrlSelected" runat="server"></asp:Literal></div>
                                <div class="chekbox-left">
                                    <p>
                                        <label for="<%=ddlyear.ClientID %>">
                                            Select year <span class="redtext">* </span>:</label>
                                        <asp:CheckBoxList ID="ddlyear" runat="server" CssClass="checkbox" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </p>
                                    <asp:CheckBoxList ID="chklstPetition" CssClass="checkbox" runat="server" RepeatColumns="1"
                                        RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="chklstPetition_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatepanel2" runat="server">
            <ContentTemplate>
                <div style="width: 487px; max-height: 350px; overflow: auto;" id="divConnectAdd"
                    runat="server">
                    <asp:Panel ID="PAppeal" runat="server" Visible="false">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="chekbox-value" style="color: Red; font-weight: bold">
                                    <asp:Literal ID="ltrlSelectedAppeal" runat="server"></asp:Literal></div>
                                <div class="chekbox-left">
                                    <label for="<%=ddlyearAppeal.ClientID %>">
                                        Select year <span class="redtext">* </span>:</label>
                                    <asp:CheckBoxList ID="ddlyearAppeal" runat="server" CssClass="checkbox" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlyearAppeal_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                    <br />
                                    <asp:CheckBoxList ID="CheckBoxAppeal" CssClass="checkbox" runat="server" RepeatColumns="1"
                                        AutoPostBack="true" RepeatDirection="Horizontal" TextAlign="Right" OnSelectedIndexChanged="CheckBoxAppeal_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlSubject" runat="server">
            <p>
                <label for="<%=txtSubject.ClientID %>">
                    Subject <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtSubject" MaxLength="2000" CssClass="text-input medium-input"
                    runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSubject" ValidationGroup="Add" runat="server"
                    SetFocusOnError="true" ControlToValidate="txtSubject" ErrorMessage="Please enter subject."
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <!--Validation for title on date 28-10-2013-->
                <asp:RegularExpressionValidator ID="regSubject" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtSubject"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSubject"
                    ID="regSubjectLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            </p>
        </asp:Panel>
        <p>
            <label for="<%= txtDate.ClientID %>">
                Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtDate" CssClass="text-input small-input" runat="server" MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Please enter date.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RevDate" runat="server" ControlToValidate="txtDate"
                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                PopupButtonID="ibDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
            </cc1:CalendarExtender>
        </p>
        <p>
            <label for="<%= ddlhours.ClientID %>">
                Time<span class="redtext">*</span>:</label>
            <asp:DropDownList ID="ddlhours" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlmins" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlampm" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <label for="<%=txtVenue.ClientID %>">
                Venue <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtVenue" MaxLength="2000" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" Text="Court Room, HERC, Panchkula"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvVenue" ValidationGroup="Add" runat="server" SetFocusOnError="true"
                ControlToValidate="txtVenue" ErrorMessage="Please enter venue." Display="Dynamic"></asp:RequiredFieldValidator>
            <!--Validation for title on date 28-10-2013-->
            <asp:RegularExpressionValidator ID="regVenue" runat="server" ErrorMessage="Please enter valid venue. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtVenue"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtVenue" ID="RegularExpressionValidator3"
                ValidationExpression="^[\s\S]{5,2000}$" runat="server" ValidationGroup="Add"
                ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
        </p>
        <p>
            <div id="FileUploadContainer">
                <label for="<%=fileUploadPdf.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:FileUpload ID="fileUploadPdf" runat="server" />
                 <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="CustvalidFileUplaod" runat="server" ClientValidationFunction="ValidateFileUpload"
                    OnServerValidate="CustvalidFileUplaod_ServerValidate" ControlToValidate="fileUploadPdf"
                    ValidationGroup="Add" ErrorMessage="Please select only .pdf file." Display="Dynamic">
                </asp:CustomValidator>
               
            </div>
            <input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
                tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
            <p>
            </p>
            <p>
                <label for="<%=txtEmailID.ClientID %>">
                    HERC Authority Email ID :</label>
                <asp:TextBox ID="txtEmailID" runat="server" CssClass="text-input medium-input" MaxLength="2000"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regPetitionerEmailid" runat="server" ControlToValidate="txtEmailID"
                    ErrorMessage="Please enter valid email id." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                    ValidationGroup="Add"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
            </p>
            <p>
                <label for="<%=txtMobileNo.ClientID %>">
                    HERC Authority Mobile No :</label>
                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="text-input medium-input" MaxLength="2000"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtMobileNo"
                    Display="Dynamic" ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                    ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Don&#39;t start with &#39;0&#39; (zero). You may enter multiple mobile numbers
                    separated by semicolon(;).</span></em>
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
            <p>
                <label for="<%=txtRemarks.ClientID %>">
                    Remarks:</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" MaxLength="2000"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regRespondentAddr" runat="server" ControlToValidate="txtRemarks"
                    Display="Dynamic" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                    ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add">
                </asp:RegularExpressionValidator>
                <!--Validation for title on date 28-10-2013-->
                <asp:RegularExpressionValidator ID="regRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
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
                <asp:RequiredFieldValidator ID="reqMetadesc" runat="server" ControlToValidate="txtMetaDescription"
                    ErrorMessage="Please enter Meta Description." ValidationGroup="Add"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regmetadescrip" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)."
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
                <asp:RegularExpressionValidator ID="regmetatitel" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)."
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaTitle"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Meta Title upto 2000 characters are allowed.</span></em>
            </p>
            <p>
                <asp:Button ID="btnAddScheduleOfHearing" runat="server" CssClass="button" OnClick="btnAddScheduleOfHearing_Click"
                    Text="Submit" ToolTip="Click To Save" ValidationGroup="Add" />
                <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnReset_Click" Text="Reset" ToolTip="Click To Reset" />
                <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="button"
                    OnClick="btnBack_Click" Text="Back" ToolTip="Go Back" />
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
    <asp:Panel ID="pnlPetitionEdit" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="width: 487px; max-height: 350px; overflow: auto;" id="div1" runat="server">
                    <asp:Panel ID="pnlDropdownlistEdit" runat="server" Visible="false">
                        <p id="P1" runat="server" visible="True">
                            <label for="<%= ddlpetitionappealEdit.ClientID %>">
                                <asp:Label ID="lblpetitionappealEdit" runat="server" ForeColor="Red" Text="Select"></asp:Label>
                                <span class="redtext">* </span>:
                                <asp:DropDownList ID="ddlpetitionappealEdit" runat="server" OnSelectedIndexChanged="ddlpetitionappealEdit_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Petition</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </p>
                        <asp:Panel ID="pnlEditpetition" runat="server" Visible="false">
                            <p>
                                <label for="<%=ddlConnectionTypeEdit.ClientID %>">
                                    Select One <span class="redtext">* </span>:</label>
                                <asp:DropDownList ID="ddlConnectionTypeEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlConnectionTypeEdit_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                                    <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                            <asp:UpdatePanel ID="updatepanel7" runat="server">
                                <ContentTemplate>
                                    <div class="chekbox-value" style="color: Red; font-weight: bold">
                                        <asp:Literal ID="ltrlPetitionEdit" runat="server"></asp:Literal></div>
                                    <div class="chekbox-left">
                                        <p>
                                            <label for="<%=ddlyearEdit.ClientID %>">
                                                Select year <span class="redtext">* </span>:</label>
                                            <asp:CheckBoxList ID="ddlyearEdit" runat="server" CssClass="checkbox" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlyearEdit_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                            <br />
                                            <asp:CheckBoxList ID="chklstPetitionEdit" CssClass="checkbox" runat="server" RepeatColumns="1"
                                                RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="chklstPetitionEdit_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        </p>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 487px; max-height: 350px; overflow: auto;" id="divConnectAdd">
                    <p id="p2" runat="server" visible="false">
                        <label for="<%= ddlappealEdit.ClientID %>">
                            <asp:Label ID="lblAppealEdit" runat="server" ForeColor="Red" Text="Select"></asp:Label>
                            <span class="redtext">* </span>:
                            <asp:DropDownList ID="ddlappealEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlappealEdit_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="2"> Appeal</asp:ListItem>
                            </asp:DropDownList>
                        </label>
                    </p>
                    <asp:Panel ID="PAppealedit" runat="server" Visible="false">
                        <asp:UpdatePanel ID="updatepanel8" runat="server">
                            <ContentTemplate>
                                <div class="chekbox-value" style="color: Red; font-weight: bold">
                                    <asp:Literal ID="ltrlAppealEdit" runat="server"></asp:Literal></div>
                                <div class="chekbox-left">
                                    <p>
                                        <label for="<%=ddlyearAppealedit.ClientID %>">
                                            Select year <span class="redtext">* </span>:</label>
                                        <asp:CheckBoxList ID="ddlyearAppealedit" CssClass="checkbox" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlyearAppealedit_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                        <br />
                                        <asp:CheckBoxList ID="CheckBoxAppealEdit" CssClass="checkbox" runat="server" RepeatColumns="1"
                                            RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="CheckBoxAppealEdit_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlSubjectEdit" runat="server">
            <p>
                <label for="<%=txtSubjectEdit.ClientID %>">
                    Subject <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtSubjectEdit" MaxLength="2000" CssClass="text-input medium-input"
                    runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Edit" runat="server"
                    SetFocusOnError="true" ControlToValidate="txtSubjectEdit" ErrorMessage="Please enter subject."
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <!--Validation for title on date 28-10-2013-->
                <asp:RegularExpressionValidator ID="regSubjectEdit" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)."
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Edit" ControlToValidate="txtSubjectEdit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSubjectEdit"
                    ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                    ValidationGroup="Edit" ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            </p>
        </asp:Panel>
        <p>
            <label for="<%= txtDate_Edit.ClientID %>">
                Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtDate_Edit" CssClass="text-input small-input" runat="server" MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibCalender_Edit" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Edit" SetFocusOnError="true"
                runat="server" ControlToValidate="txtDate_Edit" Display="Dynamic" ErrorMessage="Please enter date.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate_Edit"
                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Edit">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate_Edit"
                PopupButtonID="ibCalender_Edit">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate_Edit">
            </cc1:CalendarExtender>
        </p>
        <p>
            <label for="<%= ddlhours.ClientID %>">
                Time<span class="redtext">*</span>:</label>
            <asp:DropDownList ID="ddlhoursEdit" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlminsEdit" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlampmEdit" runat="server">
            </asp:DropDownList>
            <p>
                <label for="<%=txtVenue_Edit.ClientID %>">
                    Venue <span class="redtext">* </span>:</label>
                <asp:TextBox ID="txtVenue_Edit" MaxLength="2000" CssClass="text-input medium-input"
                    runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Edit" runat="server"
                    SetFocusOnError="true" ControlToValidate="txtVenue_Edit" ErrorMessage="Please enter venue."
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <!--Validation for title on date 28-10-2013-->
                <asp:RegularExpressionValidator ID="regVenueEdit" runat="server" ErrorMessage="Please enter valid venue name. No special characters are allowed except (space,'-_.:;#()/&)."
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Edit" ControlToValidate="txtVenue_Edit"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtVenue_Edit"
                    ID="regTitleLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server" ValidationGroup="Edit"
                    ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            </p>
            <p>
                <div id="FileUploadContainerEdit">
                    <label for="<%=fileUploadPdfEdit.ClientID %>">
                        File Description :
                    </label>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:FileUpload ID="fileUploadPdfEdit" runat="server" />
                     <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                        Tip:-Pdf file only</span></em>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateFileUploadEdit"
                        OnServerValidate="CustvalidFileUplaod_ServerValidateEdit" ControlToValidate="fileUploadPdfEdit"
                        ValidationGroup="Edit" ErrorMessage="Please select only .pdf file." Display="Dynamic">
                    </asp:CustomValidator>
                   
                </div>
                <input id="Button2" onclick="AddFileUploadEdit()" style="height: 27px; width: 74px;"
                    tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
                <p>
                </p>
                <p id="PFileName" runat="server" visible="false">
                    <asp:DataList ID="datalistFileName" runat="server" CellPadding="2" CellSpacing="1"
                        OnItemCommand="datalistFileName_ItemCommand" OnItemDataBound="datalistFileName_ItemDataBound"
                        RepeatColumns="1" RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                            <asp:HiddenField ID="hiddenFieldSoh" runat="server" Value='<%#Eval("SohId") %>' />
                            <b>
                                <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label>
                            </b>
                            <%-- <asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>--%>
                            <asp:Label ID="lblComments" runat="server" Text='<%#bind("Comments") %>'></asp:Label>
                            <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandArgument='<%# Eval("Id") %>'
                                CommandName="File">Remove File</asp:LinkButton>
                            <asp:Literal ID="ltrlDownload" runat="server">
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:DataList>
                </p>
                <p>
                    <label for="<%=txtEmailIDEdit.ClientID %>">
                        HERC Authority Email ID :</label>
                    <asp:TextBox ID="txtEmailIDEdit" runat="server" CssClass="text-input medium-input"
                        MaxLength="2000"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEmailIDEdit"
                        ErrorMessage="Please enter valid email id." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    <br />
                    <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                        Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
                </p>
                <p>
                    <label for="<%=txtMobileNoEdit.ClientID %>">
                        HERC Authority Mobile No :</label>
                    <asp:TextBox ID="txtMobileNoEdit" runat="server" CssClass="text-input medium-input"
                        MaxLength="2000"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMobileNoEdit"
                        Display="Dynamic" ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                        ValidationGroup="Edit">
                    </asp:RegularExpressionValidator>
                    <br />
                    <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                        Tip:-Don&#39;t start with &#39;0&#39; (zero). You may enter multiple mobile numbers
                        separated by semicolon(;).</span></em>
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
                <p>
                    <label for="<%=txtRemarksEdit.ClientID %>">
                        Remarks:</label>
                    <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="text-input medium-input"
                        MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtRemarksEdit"
                        Display="Dynamic" ErrorMessage="Minimum 5 and maximum 2000 characters required."
                        ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add">
                    </asp:RegularExpressionValidator>
                    <!--Validation for title on date 28-10-2013-->
                    <asp:RegularExpressionValidator ID="regRemarksEdit" runat="server" ControlToValidate="txtRemarksEdit"
                        Display="Dynamic" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&amp;)."
                        SetFocusOnError="True" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/&amp; ]|[#]|[-]|[\r\n]|[_\.])*"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
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
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                        ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                        ControlToValidate="txtMetaKeywordEdit" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*"
                        ValidationGroup="Edit" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeywordEdit"
                        ID="RegularExpressionValidator19" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                        ValidationGroup="Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMetaKeywordEdit"
                        ErrorMessage="Please enter Meta Keywords." ValidationGroup="Edit"></asp:RequiredFieldValidator>
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
                        ID="RegularExpressionValidator20" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                        ValidationGroup="Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMetaDescriptionEdit"
                        ErrorMessage="Please enter Meta Description." ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regmetadesceditt" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)."
                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="Edit" ControlToValidate="txtMetaDescriptionEdit"
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
                        ID="RegularExpressionValidator21" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                        ValidationGroup="Edit" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMetaTitleEdit"
                        ErrorMessage="Please enter Meta Title." ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regmetattiteledit" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)."
                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="Edit" ControlToValidate="txtMetaTitleEdit"
                        ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                    <br />
                    <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                        Tip:-Meta Title upto 2000 characters are allowed.</span></em>
                </p>
                <p>
                    <asp:Button ID="btnUpdate" runat="server" CssClass="button" OnClick="btnUpdate_Click"
                        Text="Update" ToolTip="Click To Update" ValidationGroup="Edit" />
                    <asp:Button ID="btnResetEdit" runat="server" CausesValidation="False" CssClass="button"
                        OnClick="btnResetEdit_Click" Text="Reset" ToolTip="Click To Reset" />
                    <asp:Button ID="btnBackEdit" runat="server" CausesValidation="False" CssClass="button"
                        OnClick="btnBackEdit_Click" Text="Back" ToolTip="Go Back" />
                </p>
    </asp:Panel>
</asp:Content>
