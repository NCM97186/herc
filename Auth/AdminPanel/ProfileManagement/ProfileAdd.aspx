<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ProfileAdd.aspx.cs" Inherits="Auth_AdminPanel_ProfileManagement_ProfileAdd" %>

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

        function ValidateFileUploadImage(Source, args) {
            var fuData = document.getElementById('<%=FileUploadImage.ClientID %>');
            var FileUploadPath = fuData.value;

            if (FileUploadPath == '') {
                // There is no file selected
                args.IsValid = false;
            }
            else {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "png" || Extension == "jpeg" || Extension == "gif" || Extension == "jpg") {
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
    <p id="PLanguage" runat="server" visible="false">
        <label for="<%= ddlLanguage.ClientID %>">
            Select Language<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlLanguage" runat="server">
            </asp:DropDownList>
        </label>
    </p>
    <p id="P1" runat="server">
        <label for="<%=ddlNevigation.ClientID %>">
            Select Profile Type<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlNevigation" runat="server">
            </asp:DropDownList>
        </label>
        <asp:RequiredFieldValidator ID="rfvProfileType" ValidationGroup="Add" runat="server"
            SetFocusOnError="true" ControlToValidate="ddlNevigation" InitialValue="0" ErrorMessage="Please select profile type."
            Display="Dynamic"></asp:RequiredFieldValidator>
    </p>
    <p>
        <label for="<%=txtname.ClientID %>">
            Name<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtname" CssClass="text-input small-input" runat="server" MaxLength="200"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RfvName" ValidationGroup="Add" runat="server" SetFocusOnError="true"
            ControlToValidate="txtname" ErrorMessage="Please enter name." Display="Dynamic"></asp:RequiredFieldValidator>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regSmallDescription" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtname"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtname" ID="RegularExpressionValidator2"
            ValidationExpression="^[\s\S]{3,200}$" runat="server" ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 200 characters required."></asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Name upto 200 characters are allowed.</span></em>
        <!--End-->
    </p>
    <p>
        <label for="<%=txtDesignation.ClientID %>">
            Designation:</label>
        <asp:TextBox ID="txtDesignation" CssClass="text-input small-input" runat="server"
            MaxLength="150"></asp:TextBox>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regDesignation" runat="server" ErrorMessage="Please enter valid designation. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtDesignation"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtDesignation"
            ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,150}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 150 characters required."></asp:RegularExpressionValidator>
            <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Designation upto 150 characters are allowed.</span></em>
        <!--End-->
    </p>
    <p id="psub" runat="server" visible="false">
        <label for="<%=txtSubject.ClientID %>">
            Subject:</label>
        <asp:TextBox ID="txtSubject" CssClass="text-input medium-input" runat="server" MaxLength="2000"
            TextMode="MultiLine"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regPetReviewSubjectLenght" runat="server" ValidationExpression="^[\s\S]{5,2000}$"
            ControlToValidate="txtPetReviewSubject" ErrorMessage="Minimum 5 and maximum 2000 characters required."
            ValidationGroup="Add" Display="Dynamic">
        </asp:RegularExpressionValidator>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regSubject" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtSubject"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
    </p>
    <p>
        <label for="<%=txtPhone.ClientID %>">
            Phone No.:</label>
        <asp:TextBox ID="txtPhone" CssClass="text-input medium-input" runat="server" MaxLength="11"></asp:TextBox>
       
        <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtPhone"
            ErrorMessage="Please enter valid Phone number." ValidationExpression="^[1-9][0-9]{6,10}$"
            ValidationGroup="Add" Display="Dynamic">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Don't start with '0' (zero). Please enter 7 to 11 numerals only.</span></em>
    </p>
    <p>
        <label for="<%=txtEpabx_Ext.ClientID %>">
            Epabx Ext:</label>
        <asp:TextBox ID="txtEpabx_Ext" CssClass="text-input small-input" runat="server" MaxLength="5"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid EPABX No in numerals between 1 and 99999 only."
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtEpabx_Ext"
            ValidationExpression="([0-9])*"></asp:RegularExpressionValidator>
             <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Please enter valid EPABX No between 1 and 99999 only.</span></em>
    </p>
    <p>
        <label for="<%=txtEmail.ClientID %>">
            Email :</label>
        <asp:TextBox ID="txtEmail" CssClass="text-input medium-input" runat="server" MaxLength="150"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regPetitionerEmailid" runat="server" ControlToValidate="txtEmail"
            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
            ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
    </p>
    <p>
        <label for="<%=FileUploadImage.ClientID %>">
            Upload Image :</label>
        <asp:FileUpload ID="FileUploadImage" runat="server" />
        <asp:CustomValidator ID="CustvalidUplaodImage" runat="server" ClientValidationFunction="ValidateFileUploadImage"
            OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="FileUploadImage"
            ValidationGroup="Add" ErrorMessage="Please select only .jpeg, .png or .gif image."
            Display="Dynamic">
        </asp:CustomValidator>
        <asp:Label ID="LblOldImage" runat="server" Text="Old Image:" Visible="false"></asp:Label>
        <asp:Label ID="LblImageName" runat="server" ForeColor="Green" Font-Bold="true" Visible="false"></asp:Label>
        &nbsp;<asp:LinkButton ID="lnkImageRemove" runat="server" OnClick="lnkImageRemove_Click"
            Visible="false">Remove Image</asp:LinkButton><br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip: .jpeg .png .gif image only.</span></em>
    </p>
    <p id="pDescEditor" runat="server">
        <label for="<%= FCKeditor1.ClientID %>">
            Description<span class="redtext">*</span>:</label>
        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" Height="400px"
            Width="650px" ToolbarSet="BasicModule">
        </FCKeditorV2:FCKeditor>
    </p>
    <p>
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
        <%-- <asp:CustomValidator ID="CustomValidator1" runat="server"  ClientValidationFunction="CompareDates" ControlToValidate="txtstartdate"
      Display="Dynamic" ErrorMessage="Start Date Must Be Greater Than OR Equal To Current Date..!"  ValidationGroup="Add" ></asp:CustomValidator>--%>
    </p>
    <p>
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
            Tip:-dd/mm/yyyy<%--If u will not enter the end date it will enter the end date after
            3 months from start date--%></span></em>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Add" SetFocusOnError="true"
            Display="Dynamic" runat="server" ControlToValidate="txtendate" ErrorMessage="Please enter upcoming event end date."></asp:RequiredFieldValidator>--%>
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
        <asp:RequiredFieldValidator ID="reqMetaDesc" runat="server" ControlToValidate="txtMetaDescription"
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
            ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMetaTitle"
            ErrorMessage="Please enter Meta Title." ValidationGroup="Add"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regmetattile" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
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
        <asp:Button ID="BtnSubmit" runat="server" CssClass="button" ValidationGroup="Add"
            OnClick="BtnSubmit_Click" ToolTip="Click To Save" />
        <%--<asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
            OnClick="BtnUpdate_Click" ToolTip="Click To Update" />--%>
        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
            OnClick="btnReset_Click" ToolTip="Click To Reset" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
            OnClick="btnBack_Click" ToolTip="Go Back" />
    </p>
</asp:Content>
