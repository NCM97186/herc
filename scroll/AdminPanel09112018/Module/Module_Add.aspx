<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Module_Add.aspx.cs" Inherits="Auth_AdminPanel_Module_Module_Add" %>

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



        function CheckExpiryDateMendatory(source, args) {

            var fromDatem = new Date();

            var txtFromDatem = document.getElementById('<%= txtstartdate.ClientID %>').value;
            var FromDatem = txtFromDatem.split("/");

            /*Start 'Date to String' conversion block, this block is required because javascript do not provide any direct function to convert 'String to Date' */
            var fddm = FromDatem[0]; //get the day part
            var fmmm = FromDatem[1]; //get the month part
            var fyyyym = FromDatem[2]; //get the year part

            fromDatem.setUTCDate(fddm);
            fromDatem.setUTCMonth(fmmm - 1);
            fromDatem.setUTCFullYear(fyyyym);

            var toDatem = new Date();
            var txtToDatem = document.getElementById('<%= txtenddate.ClientID %>').value;
            var ToDatem = txtToDatem.split("/");
            var tddm = ToDatem[0]; //get the day part
            var tmmm = ToDatem[1]; //get the month part
            var tyyyym = ToDatem[2]; //get the year part

            toDatem.setUTCDate(tddm);
            toDatem.setUTCMonth(tmmm - 1);
            toDatem.setUTCFullYear(tyyyym);
            //end of 'String to Date' conversion block

            var differencem = toDatem.getTime() - fromDatem.getTime();
            var daysDifferencem = Math.floor(differencem / 1000 / 60 / 60 / 24);
            //     alert('df');
            differencem -= daysDifferencem * 1000 * 60 * 60 * 24;
            //    
            //    //if diffrence is greater then 366 then invalidate, else form is valid
            // if(difference > 366 )
            if (daysDifferencem > 200000000 || daysDifferencem <= 0)
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
        function ValidateFileUpload1(Source, args) {
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
    <p>
        <asp:Label ID="lblmsg" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
    </p>
    <p>
        <label for="<%=txtname.ClientID %>">
            Title<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtname" CssClass="text-input medium-input" runat="server" MaxLength="2000"
            TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RfvName" ValidationGroup="Add" runat="server" SetFocusOnError="true"
            ControlToValidate="txtname" ErrorMessage="Please enter title." Display="Dynamic"></asp:RequiredFieldValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regTitle" runat="server" ErrorMessage="Please enter valid title. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtname"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtname" ID="RegularExpressionValidator51"
            ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="Add"
            ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="pTitle_Reg" runat="server" visible="false">
        <label for="<%=txtTitle_Reg.ClientID %>">
            Title(In reg lang)<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtTitle_Reg" CssClass="text-input medium-input" runat="server"
            MaxLength="2000" TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTitleReg" ValidationGroup="Add" runat="server"
            Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtTitle_Reg" ErrorMessage="Please enter title in regional language."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regTitleReglng" runat="server" ControlToValidate="txtTitle_Reg"
            Display="Dynamic" ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regTitlevalidation" runat="server" ErrorMessage="Please enter valid title in regional language. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtTitle_Reg"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
    </p>
    <p id="pAltTag" runat="server" visible="false">
        <label for="<%= txtAltTag.ClientID %>">
            Alt Tag<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtAltTag" CssClass="text-input medium-input" runat="server" MaxLength="2000"
            TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtAltTag" Display="Dynamic" ErrorMessage="Please enter alt tag.">
        </asp:RequiredFieldValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regAltTag" runat="server" Display="Dynamic" ErrorMessage="Please enter valid data. No special characters are allowed except (space,-_.)"
            ValidationGroup="Add" ControlToValidate="txtAltTag" SetFocusOnError="true" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*">
        </asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAltTag" ID="RegularExpressionValidator54"
            ValidationExpression="^[\s\S]{5,2000}$" runat="server" ValidationGroup="Add"
            SetFocusOnError="true" ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="pAltTag_Reg" runat="server" visible="false">
        <label for="<%= txtAltTag_Reg.ClientID %>">
            Alt Tag(In reg Lang)<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtAltTag_Reg" CssClass="text-input medium-input" runat="server"
            MaxLength="2000" TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAlttagReg" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtAltTag_Reg" Display="Dynamic" ErrorMessage="Please enter alt tag in regional language.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regAlttagReglng" runat="server" ControlToValidate="txtAltTag_Reg"
            Display="Dynamic" ValidationExpression="^[\s\S]{5,2000}$" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regAltTagValidation" runat="server" ErrorMessage="Please enter valid alt tag in regional language. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtAltTag_Reg"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
    </p>
    <p id="pImage" runat="server">
        <label for="<%=FileUploadImage.ClientID %>">
            Upload Image :</label>
        <asp:FileUpload ID="FileUploadImage" runat="server" />
        <asp:CustomValidator ID="CustvalidUplaodImage" runat="server" ClientValidationFunction="ValidateFileUploadImage"
            OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="FileUploadImage"
            ValidationGroup="Add" ErrorMessage="Please select only .jpeg, .png or .gif image."
            Display="Dynamic">
        </asp:CustomValidator>
        <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="FileUploadImage"
            ErrorMessage="Please select image" Enabled="false" ValidationGroup="Add" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:Label ID="LblOldImage" runat="server" Text="Old Image:"></asp:Label>
        <asp:Label ID="LblImageName" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
        &nbsp;<asp:LinkButton ID="lnkImageRemove" runat="server" OnClick="lnkImageRemove_Click">Remove Image</asp:LinkButton><br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip: .jpeg .png .gif image only.</span></em>
    </p>
    <p id="pFileUpload" runat="server">
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
    <b>
        <asp:Label ID="lbldiscussionMsg" runat="server" Visible="false" Text="Upload Discussion Paper/Public Notice,etc."
            ForeColor="Red"></asp:Label></b>
    <p id="pMultipleFile" runat="server" visible="false">
        <div id="FileUploadContainer">
            <label for="<%=fileUploadPdf.ClientID %>">
                File Description :
            </label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:FileUpload ID="fileUploadPdf" runat="server" />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Pdf file only</span></em>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateFileUpload1"
                OnServerValidate="CustvalidFileUplaod_ServerValidate" ControlToValidate="fileUploadPdf"
                ValidationGroup="Add" ErrorMessage="Please select only .pdf file." Display="Dynamic">
            </asp:CustomValidator>
            
        </div>
        <input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
            tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
    </p>
    <p id="PFileName" runat="server" visible="false">
        <asp:DataList ID="datalistFileName" runat="server" CellPadding="2" CellSpacing="1"
            RepeatColumns="1" RepeatDirection="Horizontal" OnItemCommand="datalistFileName_ItemCommand"
            OnItemDataBound="datalistFileName_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                <asp:HiddenField ID="hiddenFieldLinkId" runat="server" Value='<%#Eval("Link_ID") %>' />
                <asp:HiddenField ID="hidComments" runat="server" Value='<%#Eval("Comments") %>' />
                
                <asp:Label ID="lblFileComment" runat="server" Text='<%#bind("Comments") %>'></asp:Label>
                <b>
                <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label></b>
                <asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>
                <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandName="File" CommandArgument='<%# Eval("Id") %>'>Remove File</asp:LinkButton>
                <asp:Literal ID="ltrlDownload" runat="server">
                </asp:Literal>
            </ItemTemplate>
        </asp:DataList>
    </p>
    <p id="PFilesAnnualNotification" runat="server" visible="false">
        <asp:DataList ID="dtlistFiles" runat="server" CellPadding="2" CellSpacing="1" RepeatColumns="1"
            RepeatDirection="Horizontal" OnItemCommand="dtlistFiles_ItemCommand" OnItemDataBound="dtlistFiles_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                <asp:HiddenField ID="hydLinkId" runat="server" Value='<%#Eval("LinkId") %>' />
                <b>
                    <asp:Label ID="lblFiles" runat="server" Text='<%#bind("FileName") %>'></asp:Label></b>
                <asp:LinkButton ID="lnkFileConnectedRemoves" runat="server" CommandName="FileModules"
                    CommandArgument='<%# Eval("Id") %>'>Remove File</asp:LinkButton>
                <asp:Literal ID="ltrlDownloadModule" runat="server">
                </asp:Literal>
            </ItemTemplate>
        </asp:DataList>
    </p>
    <p id="pLastDateRcv" runat="server" visible="false">
        <label for="<%= txtLastDate.ClientID %>">
            <asp:Label ID="lblLastDateRcv" runat="server"></asp:Label></label>
        <asp:TextBox ID="txtLastDate" CssClass="text-input small-input" CausesValidation="true"
            runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLastDate"
            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
            ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-dd/mm/yyyy</span></em>
    </p>
    <p id="pPublicHearing" runat="server" visible="false">
        <label for="<%= TxtPublicHearing.ClientID %>">
            Date of Public Hearing (If any):</label>
        <asp:TextBox ID="TxtPublicHearing" CssClass="text-input small-input" CausesValidation="true"
            runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtPublicHearing"
            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
            ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-dd/mm/yyyy</span></em>
    </p>
    <p id="PTime" runat="server" visible="false">
        <label for="<%= ddlhours.ClientID %>">
            Time<span class="redtext">*</span>:</label>
        <asp:DropDownList ID="ddlhours" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlmins" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlampm" runat="server">
        </asp:DropDownList>
    </p>
    <p id="pVenu" runat="server" visible="false">
        <label for="<%=txtVenue.ClientID %>">
            Venue (If any):</label>
        <asp:TextBox ID="txtVenue" MaxLength="500" CssClass="text-input medium-input" runat="server"
            TextMode="MultiLine" Text="Court Room, HERC, Panchkula" onkeypress="if(this.value.length>=500) this.value = this.value.substring(0, 499);"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid venue name. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtVenue"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtVenue" ID="RegularExpressionValidator3"
            ValidationExpression="^[\s\S]{3,500}$" runat="server" ValidationGroup="Add"
            ErrorMessage="Minimum 3 and maximum 500 characters required."></asp:RegularExpressionValidator>
             <br />
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Venue upto 500 characters are allowed.</span></em>
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
    <p id="pEndMendatory" runat="server" visible="false">
        <label for="<%= txtendate.ClientID %>">
            End Date<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtenddate" CssClass="text-input small-input" CausesValidation="true"
            runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtenddate"
            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
            ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtenddate" Display="Dynamic" ErrorMessage="Please enter end date.">
        </asp:RequiredFieldValidator>
        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="CheckExpiryDateMendatory"
            ControlToValidate="txtenddate" Display="Dynamic" ErrorMessage="End date must be greater than start date."
            ValidationGroup="Add"></asp:CustomValidator>
        <br />
        <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
            Tip:-dd/mm/yyyy</span></em>
    </p>
    <p id="PSmalldesc" runat="server" visible="false">
        <label for="<%= txtSmalldesc.ClientID %>">
            Small Description<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtSmalldesc" runat="server" TextMode="MultiLine" MaxLength="200"
            CausesValidation="true" CssClass="text-input medium-input" onkeypress="if(this.value.length>=200) this.value = this.value.substring(0, 199);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Add" runat="server"
            SetFocusOnError="true" ControlToValidate="txtSmalldesc" ErrorMessage="Please enter Description."
            Display="Dynamic"></asp:RequiredFieldValidator>
        <!--Validation for small description on date 28-10-2013 for security-->
        <asp:RegularExpressionValidator ID="regSmallDescription" runat="server" ErrorMessage="Please enter valid small description. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtSmalldesc"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSmalldesc"
            ID="regSmallDescLength" ValidationExpression="^[\s\S]{5,200}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 200 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="PCirculationPublicNotice" runat="server" visible="false">
        <label for="<%= txtSmalldesc.ClientID %>">
            Advertisement / Circulation/ public Notice:</label>
        <asp:TextBox ID="TxtCirculation" runat="server" TextMode="MultiLine" MaxLength="200"
            CausesValidation="true" CssClass="text-input medium-input" onkeypress="if(this.value.length>=200) this.value = this.value.substring(0, 199);"></asp:TextBox>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regAdvSmallDesc" runat="server" ErrorMessage="Please enter valid small description. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtSmalldesc"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="TxtCirculation"
            ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{5,200}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 200 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="pDescEditor" runat="server">
        <label for="<%= FCKeditor1.ClientID %>">
            Description<span class="redtext">*</span>:</label>
        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" Height="400px"
            Width="650px" ToolbarSet="BasicModule">
        </FCKeditorV2:FCKeditor>
    </p>
    <p id="pDesc" runat="server" visible="false">
        <label for="<%= txtDesc.ClientID %>">
            Description<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtDesc" CssClass="text-input medium-input" runat="server" Height="100px"
            MaxLength="2000" TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvDescription" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtDesc" Display="Dynamic" ErrorMessage="Please enter description.">
        </asp:RequiredFieldValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regDescription" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,-_.)"
            ControlToValidate="txtDesc" ValidationGroup="Add" Display="Dynamic" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtDesc" ID="regDescriptionLength"
            ValidationExpression="^[\s\S]{5,2000}$" runat="server" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p id="pDesc_Reg" runat="server" visible="false">
        <label for="<%= txtDesc_Reg.ClientID %>">
            Description(In reg lang)<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtDesc_Reg" CssClass="text-input medium-input" runat="server" Height="100px"
            MaxLength="2000" TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvDescriptionReg" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtDesc_Reg" Display="Dynamic" ErrorMessage="Please enter description in regional language.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtDesc_Reg"
            ID="regDescriptionReglng" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
        </asp:RegularExpressionValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regDescriptionReglngValidation" runat="server"
            ErrorMessage="Please enter valid description in regional language. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtDesc_Reg"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*">
        </asp:RegularExpressionValidator>
        <!--End-->
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
        <asp:RegularExpressionValidator ID="regmetatitles" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
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
        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
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
        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLastDate"
            PopupButtonID="ImageButton3">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLastDate">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtPublicHearing"
            PopupButtonID="ImageButton4">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtPublicHearing">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" TargetControlID="txtenddate"
            PopupButtonID="ImageButton5">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
            TargetControlID="txtenddate">
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
