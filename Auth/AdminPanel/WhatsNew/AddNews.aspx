<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="AddNews.aspx.cs" Inherits="Auth_AdminPanel_WhatsNew_AddNews"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../jsMultipleFileUpload/jquery-1.8.2.js" type="text/javascript"></script>

    <script src="../jsMultipleFileUpload/jquery.MultiFile.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function CheckExpiryDate(source, args) {

            var fromDate = new Date();

            var txtFromDate = document.getElementById('<%= Txtstartdate.ClientID %>').value;
            var FromDate = txtFromDate.split("/");

            /*Start 'Date to String' conversion block, this block is required because javascript do not provide any direct function to convert 'String to Date' */
            var fdd = FromDate[0]; //get the day part
            var fmm = FromDate[1]; //get the month part
            var fyyyy = FromDate[2]; //get the year part

            fromDate.setUTCDate(fdd);
            fromDate.setUTCMonth(fmm - 1);
            fromDate.setUTCFullYear(fyyyy);

            var toDate = new Date();
            var txtToDate = document.getElementById('<%= TxtExpirydate.ClientID %>').value;
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

        function ValidateFileUpload1(Source, args) {
            var fuData = document.getElementById('<%= fileUpload_Menu.ClientID %>');
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

    <!-- Start Content Box -->
    <div class="heading_center">
        <h4>
            <asp:Label ID="lblHead" runat="server"></asp:Label></h4>
    </div>
    <p>
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </p>

    <p>
        <label for="<%= txtTenderName.ClientID %>">
            What’s New Title <span class="redtext">* </span>:</label>
        <asp:TextBox ID="txtTenderName" runat="server" MaxLength="2000" CssClass="text-input medium-input"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RfvTenderName" runat="server" ControlToValidate="txtTenderName"
            Display="Dynamic" ErrorMessage="Please enter title" SetFocusOnError="True" ValidationGroup="News"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regSubject" runat="server" ErrorMessage="Please enter valid title. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="News" ControlToValidate="txtTenderName"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTenderName"
            ID="regSubjectLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
            ValidationGroup="News" ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p>
        <label for="<%= txtDescription.ClientID %>">
            What’s New Description <span class="redtext">* </span>:</label>
        <asp:TextBox ID="txtDescription" runat="server" MaxLength="5000" CssClass="text-input medium-input"
            Columns="10" Rows="3" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
            Display="Dynamic" ErrorMessage="Please enter description" SetFocusOnError="True"
            ValidationGroup="News"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="News" ControlToValidate="txtDescription"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtDescription"
            ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{5,5000}$" runat="server"
            ValidationGroup="News" ErrorMessage="Minimum 5 and maximum 5000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p>
        <label for="<%= Txtstartdate.ClientID %>">
            Publish&nbsp; Date<span class="redtext">* </span>:</label>
        <asp:TextBox ID="Txtstartdate" runat="server" CssClass="text-input small-input" MaxLength="100"></asp:TextBox>
        <asp:ImageButton ImageUrl="~/Auth/AdminPanel/images/cal.jpg" ID="Image8" runat="server" />
        <asp:RequiredFieldValidator ID="RfvPublishDate" runat="server" ControlToValidate="Txtstartdate"
            Display="Dynamic" ErrorMessage="Please Enter Publish Date" ValidationGroup="News"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RevStartDate" runat="server" ControlToValidate="Txtstartdate"
            Display="Dynamic" ErrorMessage="Enter Date in Formate dd/MM/yyyy" SetFocusOnError="True"
            ValidationGroup="News" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"></asp:RegularExpressionValidator>
        <AjaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
            TargetControlID="Txtstartdate" PopupButtonID="Image8">
        </AjaxToolKit:CalendarExtender>
        <AjaxToolKit:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy"
            TargetControlID="Txtstartdate">
        </AjaxToolKit:CalendarExtender>
    </p>
    <p>
        <label for="<%= TxtExpirydate.ClientID %>">
            Expiry Date<span class="redtext">* </span>:</label>
        <asp:TextBox ID="TxtExpirydate" runat="server" CssClass="text-input small-input"
            MaxLength="100"></asp:TextBox>
        <asp:ImageButton ImageUrl="~/Auth/AdminPanel/images/cal.jpg" ID="myimage" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TxtExpirydate"
            Display="Dynamic" ErrorMessage="Please Enter Expiry Date" ValidationGroup="News"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtExpirydate"
            Display="Dynamic" ErrorMessage="Enter Date in Formate dd/MM/yyyy" SetFocusOnError="True"
            ValidationGroup="News" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"></asp:RegularExpressionValidator>
        <AjaxToolKit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
            TargetControlID="TxtExpirydate" PopupButtonID="myimage">
        </AjaxToolKit:CalendarExtender>
        <AjaxToolKit:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
            TargetControlID="TxtExpirydate">
        </AjaxToolKit:CalendarExtender>
        <asp:CustomValidator ID="CustValEndDate" runat="server" ClientValidationFunction="CheckExpiryDate"
            ControlToValidate="TxtExpirydate" Display="Dynamic" ErrorMessage="Expiry Date must be greater than start date."
            ValidationGroup="News"></asp:CustomValidator>
    </p>
    <p id="trFileupload" runat="server">
        <label for="<%=fileUpload_Menu.ClientID %>">
            Upload File:
        </label>
        <asp:FileUpload ID="fileUpload_Menu" runat="server" />
        <asp:Label ID="lblmenuMsg" runat="server" Text="Current File:" Visible="false"></asp:Label>
        <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
        <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandArgument='<%# Eval("News_ID") %>'
            ToolTip="Remove File" OnClick="lnkFileConnectedRemove_Click" Visible="false">Remove File</asp:LinkButton>
        <asp:Literal ID="ltrlDownload" runat="server" Visible="false">
        </asp:Literal>
        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateFileUpload1"
            ControlToValidate="fileUpload_Menu" OnServerValidate="CustomValidator3_ServerValidate"
            ValidationGroup="News" ErrorMessage="Please select only .pdf file." Display="Dynamic"> </asp:CustomValidator>
        <br />
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-Pdf file only</span></em>
    </p>
    <br />
    <br />
    <p align="left">
        <label>
            &nbsp;</label>
        <asp:Button ID="BtnSubmit" CssClass="button" runat="server" Text="Submit" ValidationGroup="News"
            OnClick="BtnSubmit_Click" Style="height: 26px" />
        <asp:Button ID="BtnReset" CssClass="button" runat="server" Text="Reset" CausesValidation="False"
            OnClick="BtnReset_Click" />
        <asp:Button ID="BtnBack" CssClass="button" runat="server" Text="Back" CausesValidation="False"
            OnClick="BtnBack_Click" /></p>
</asp:Content>
