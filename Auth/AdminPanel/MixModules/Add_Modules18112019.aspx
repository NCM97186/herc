<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Add_Modules.aspx.cs" Inherits="Auth_AdminPanel_MixModules_Add_Modules"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function CompareDates(source, args) {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm }
            var str1 = dd + '/' + mm + '/' + yyyy;

            var str2 = document.getElementById('<%= txtstartdate.ClientID %>').value;
            // var str4= document.getElementById("txtpublishdate");
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10);
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10);
            var yr2 = parseInt(str2.substring(6, 10), 10);

            var today = new Date(yr1, mon1, dt1);
            var userdate = new Date(yr2, mon2, dt2);

            if (userdate < today) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function CheckExpiryDate(source, args) {

            var fromDate = new Date();

            var txtFromDate = document.getElementById('<%= txtstartdate.ClientID %>').value;
            var FromDate = txtFromDate.split("/");

            /*Start 'Date to String' conversion block, this block is required because javascript do not provide any direct function to convert 'String to Date' */
            var fdd = FromDate[0]; //get the day part
            var fmm = FromDate[1]; //get the month part
            var fyyyy = FromDate[2]; //get the year part

            fromDate.setUTCDate(fdd);
            fromDate.setUTCMonth(fmm - 1);
            fromDate.setUTCFullYear(fyyyy);

            var toDate = new Date();
            var txtToDate = document.getElementById('<%= txtendate.ClientID %>').value;
            var ToDate = txtToDate.split("/");
            var tdd = ToDate[0]; //get the day part
            var tmm = ToDate[1]; //get the month part
            var tyyyy = ToDate[2]; //get the year part

            toDate.setUTCDate(tdd);
            toDate.setUTCMonth(tmm - 1);
            toDate.setUTCFullYear(tyyyy);
            //end of 'String to Date' conversion block

            var difference = toDate.getTime() - fromDate.getTime();
            var daysDifference = Math.floor(difference / 1000 / 60 / 60 / 24);
            //     alert('df');
            difference -= daysDifference * 1000 * 60 * 60 * 24;
            //    
            //    //if diffrence is greater then 366 then invalidate, else form is valid
            // if(difference > 366 )
            if (daysDifference > 200000000 || daysDifference <= 0)
                args.IsValid = false;
            else
                args.IsValid = true;
            //else
            //  args.IsValid = true;
        }
 
    </script>
    <script language="javascript" type="text/javascript">


        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= FileUpload2.ClientID %>');
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
    <p id="PDepartment" runat="server">
        <label for="<%= ddlDepartment.ClientID %>">
            Select Department<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlDepartment" runat="server">
            </asp:DropDownList>
        </label>
    </p>
    <p id="PLanguage" runat="server">
        <label for="<%= ddlLanguage.ClientID %>">
            Select Language<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlLanguage" runat="server">
            </asp:DropDownList>
        </label>
    </p>
    <p id="P1" runat="server">
        <label for="<%= ddlModules.ClientID %>">
            Select Module<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlModules" runat="server" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </label>
    </p>
    <p id="PModuleName" runat="server" visible="false">
        <label for="<%= lblModuleName.ClientID %>">
            Module Name:
            <asp:Label ID="lblModuleName" runat="server"></asp:Label>
        </label>
    </p>
    <asp:Panel ID="pnlRegulation" runat="server" Visible="false">
        <p id="ambendantType" runat="server">
            <label for="<%=ddlamendment.ClientID %>">
                New/Amendment:</label><asp:DropDownList ID="ddlamendment" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlamendment_SelectedIndexChanged">
                </asp:DropDownList>
            <asp:CustomValidator ID="cus_ambendment" runat="server" ErrorMessage="This amendment already exist for this record."
                OnServerValidate="cus_ambendment_ServerValidate" ValidationGroup="Add" Display="Dynamic"></asp:CustomValidator>
        </p>
        <p id="regulationNumber" runat="server" visible="true">
            <label for="<%=txtRegulationNo.ClientID %>">
                <asp:Label ID="lblTxtRegulations" runat="server"></asp:Label>:</label><asp:TextBox
                    ID="txtRegulationNo" CssClass="text-input small-input" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regReferenceNo" runat="server" ControlToValidate="txtRegulationNo"
                ErrorMessage="Reference number must be between 1 to 9999 characters and only numbers are applicable."
                ValidationExpression="^[1-9][0-9]{0,3}$" ValidationGroup="Add" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cusRegulation" runat="server" ErrorMessage="Regulation number already exist. Please enter another regulation number."
                OnServerValidate="cusRegulation_ServerValidate" ValidationGroup="Add" Display="Dynamic"></asp:CustomValidator></p>
        <p id="pOther" runat="server" visible="false">
            <label for="<%=txtStandard.ClientID %>">
                <asp:Label ID="lblStandard" runat="server"></asp:Label>
                <span class="redtext">&nbsp;(For internal reference only)* </span>:</label><asp:TextBox
                    ID="txtStandard" CssClass="text-input small-input" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtStandard" ErrorMessage="Please enter number."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStandard"
                ErrorMessage="Reference number must be between 1 to 9999 characters and only numbers are applicable."
                ValidationExpression="^[1-9][0-9]{0,3}$" ValidationGroup="Add" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Record number already exist. Please enter another record ."
                OnServerValidate="cusRegulation1_ServerValidate" ValidationGroup="Add" Display="Dynamic"></asp:CustomValidator></p>
        <p id="regulationddlNumber" runat="server" visible="false">
            <label for="<%=ddlRegulationNumber.ClientID %>">
                <asp:Label ID="lblRegulationNumber" runat="server"></asp:Label>
                <span class="redtext">* </span>:</label><asp:DropDownList ID="ddlRegulationNumber"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRegulationNumber_SelectedIndexChanged">
                </asp:DropDownList>
            <asp:Label ID="lbldetail" runat="server" Visible="false"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvRegulationNumber" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="ddlRegulationNumber" ErrorMessage="Please select regulation number."
                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
        </p>
    </asp:Panel>
    <p>
        <label for="<%=txtname.ClientID %>">
            Details<span class="redtext">*</span>:</label>
             <FCKeditorV2:FCKeditor ID="txtname" runat="server" ToolbarSet="ankit" BasePath="~/fckeditor/"
                                Height="100px" Width="400">
                            </FCKeditorV2:FCKeditor>
        
    </p>
    <p id="pTitle_Reg" runat="server" visible="false">
        <label for="<%=txtTitle_Reg.ClientID %>">
            Title(In reg lang)<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtTitle_Reg" CssClass="text-input medium-input" runat="server"
            MaxLength="2000"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTitleReg" ValidationGroup="Add" runat="server"
            Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtTitle_Reg" ErrorMessage="Please enter title in regional language."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regTitleReglng" runat="server" ControlToValidate="txtTitle_Reg"
            Display="Dynamic" ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="pAltTag" runat="server" visible="false">
        <label for="<%= txtAltTag.ClientID %>">
            Alt Tag<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtAltTag" CssClass="text-input medium-input" runat="server" MaxLength="2000"
            TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtAltTag" Display="Dynamic" ErrorMessage="Please enter alt tag.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAltTag" ID="RegularExpressionValidator54"
            ValidationExpression="^[\s\S]{5,2000}$" runat="server" ValidationGroup="Add"
            SetFocusOnError="true" ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="pAltTag_Reg" runat="server" visible="false">
        <label for="<%= txtAltTag_Reg.ClientID %>">
            Alt Tag(In reg Lang)<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtAltTag_Reg" CssClass="text-input medium-input" runat="server"
            MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAlttagReg" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtAltTag_Reg" Display="Dynamic" ErrorMessage="Please enter alt tag in regional language.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regAlttagReglng" runat="server" ControlToValidate="txtAltTag_Reg"
            Display="Dynamic" ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="p2" runat="server">
        <label for="<%= ddlfiles.ClientID %>">
            Select File/ External Links</label>
        <asp:DropDownList ID="ddlfiles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfiles_SelectedIndexChanged">
            <asp:ListItem Value="0" Selected="True">Upload File</asp:ListItem>
            <asp:ListItem Value="1">External Link</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p id="pFileUpload" runat="server" visible="true">
        <label for="<%= FileUpload2.ClientID %>">
            Upload File :</label>
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:CustomValidator ID="CustvalidFileUplaod" runat="server" ClientValidationFunction="ValidateFileUpload"
            OnServerValidate="CustomValidator3_ServerValidate" ControlToValidate="FileUpload2"
            ValidationGroup="Add" ErrorMessage="Please select only .pdf file." Display="Dynamic">
        </asp:CustomValidator>
        <asp:Label ID="LblOldFile" runat="server" Text="Old File :"></asp:Label>
        <asp:Label ID="lblFileName" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
        &nbsp;<asp:LinkButton ID="lnkFileRemove" runat="server" OnClick="lnkFileRemove_Click">Remove File</asp:LinkButton><br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Pdf file only</span></em>
    </p>
    <p id="pExternalLinks" runat="server" visible="false">
        <label for="<%= txtLinks.ClientID %>">
            Website Links :</label>
        <asp:TextBox ID="txtLinks" runat="server" CssClass="text-input small-input"></asp:TextBox>
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-http://www.abc.com </span></em>
        <asp:RegularExpressionValidator ID="RevLinkUrl" runat="server" ControlToValidate="txtLinks"
            Display="Dynamic" ErrorMessage="Please enter valid url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
            ValidationGroup="Menu"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RfvUrlName" runat="server" ControlToValidate="txtLinks"
            ErrorMessage="Enter link url name" ValidationGroup="Menu" Display="Dynamic"></asp:RequiredFieldValidator>
    </p>
    <p id="pStart" runat="server" visible="false">
        <label for="<%= txtstartdate.ClientID %>">
            Start Date<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtstartdate" CssClass="text-input small-input" runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RfvStartDate" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtstartdate" Display="Dynamic" ErrorMessage="Please enter start date.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RevStartDate" runat="server" ControlToValidate="txtstartdate"
            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
            ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-dd/mm/yyyy</span></em>
    </p>
    <p id="pEnd" runat="server" visible="false">
        <label for="<%= txtendate.ClientID %>">
            End Date :</label>
        <asp:TextBox ID="txtendate" CssClass="text-input small-input" CausesValidation="true"
            runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RegularExpressionValidator ID="RevEndDate" runat="server" ControlToValidate="txtendate"
            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
            ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <asp:CustomValidator ID="CustValEndDate" runat="server" ClientValidationFunction="CheckExpiryDate"
            ControlToValidate="txtendate" Display="Dynamic" ErrorMessage="End date must be greater than start date."
            ValidationGroup="Add"></asp:CustomValidator>
        <br />
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-dd/mm/yyyy</span></em>
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
        <asp:RegularExpressionValidator ID="regmetadescr" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
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
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtstartdate"
            PopupButtonID="ImageButton1">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtstartdate">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtendate"
            PopupButtonID="ImageButton2">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtendate">
        </cc1:CalendarExtender>
    </p>
    <p>
        <asp:Button ID="BtnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="Add"
            OnClick="BtnSubmit_Click" ToolTip="Click To Save" />
        <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
            OnClick="BtnUpdate_Click" ToolTip="Click To Update" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
            OnClick="btnReset_Click" ToolTip="Click To Reset" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
            OnClick="btnBack_Click" ToolTip="Go Back" />
    </p>
</asp:Content>
