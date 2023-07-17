<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Order_Edit.aspx.cs" Inherits="Auth_AdminPanel_OrderManagement_Order_Edit" %>

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
    <script type="text/javascript">
        function ValidateModuleList(source, args) {
            var chklist = document.getElementById('<%=ChkCategory.ClientID %>');
            var chkListinputs = chklist.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
 

    </script>
    <p>
        <asp:Label ID="lblmsg" CssClass="redtext" Font-Size="Medium" runat="server"></asp:Label>
    </p>
    <p>
        <label for="<%=ddlConnectionTypeEdit.ClientID %>">
            <asp:Label ID="lblPetitionreview" runat="server" Text="Petition/Review Petition:"
                ForeColor="Red"></asp:Label></label>
        <asp:DropDownList ID="ddlConnectionTypeEdit" runat="server" AutoPostBack="true" Enabled="false"
            OnSelectedIndexChanged="ddlConnectionTypeEdit_SelectedIndexChanged">
            <asp:ListItem Value="0" Selected="True">Select One</asp:ListItem>
            <asp:ListItem Value="1">Petition</asp:ListItem>
            <asp:ListItem Value="2"> Review Petition</asp:ListItem>
            <%--<asp:ListItem Value="3"> Appeal Petition</asp:ListItem>--%>
        </asp:DropDownList>
    </p>
    <asp:Panel ID="pnlPetitionSection" runat="server" Visible="false">
        <p>
            <label for="<%=ddlyear.ClientID %>">
                Select year <span class="redtext">* </span>:</label>
            <asp:CheckBoxList ID="ddlyear" runat="server" CssClass="checkbox" AutoPostBack="true"
                OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
            </asp:CheckBoxList>
        </p>
        <p>
            <div style="width: 487px; max-height: 350px; overflow: auto;" id="divConnectAdd1"
                runat="server">
                <div class="chekbox-value" style="color: Red; font-weight: bold;">
                    <asp:Literal ID="ltrlSelected" runat="server"></asp:Literal></div>
                <label for="<%=chklstPetitionEdit.ClientID %>">
                    Petition/Review Petition :</label>
                <asp:CheckBoxList ID="chklstPetitionEdit" CssClass="checkbox" runat="server" RepeatColumns="1"
                    RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="chklstPetitionEdit_SelectedIndexChanged">
                </asp:CheckBoxList>
            </div>
        </p>
    </asp:Panel>
    <p id="petitionDetails" runat="server">
        <p style="background-color: #E7E7E7; font-size: large;">
            <span class="">Petitioner's Details </span>
        </p>
        <p>
            <label for="<%=txtPetitionerName.ClientID %>">
                Petitioner's Name :</label>
            <asp:TextBox ID="txtPetitionerName" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <!--Validation for title on date 28-10-2013-->
            <asp:RegularExpressionValidator ID="regPetitionerName" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetitionerName"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPetitionerName"
                ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtPetitionerAdd.ClientID %>">
                Petitioner's Address :</label>
            <asp:TextBox ID="txtPetitionerAdd" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <!--Validation for title on date 28-10-2013-->
            <asp:RegularExpressionValidator ID="regPetitionerAddress" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtPetitionerAdd"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPetitionerAdd"
                ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtPetitionerMobileNo.ClientID %>">
                Petitioner's Mobile No :</label>
            <asp:TextBox ID="txtPetitionerMobileNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtPetitionerMobileNo"
                ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
                by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerPhoneNo.ClientID %>">
                Petitioner's Phone No :</label>
            <asp:TextBox ID="txtPetitionerPhoneNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerPhoneNo" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtPetitionerPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Phone No. with STD code (without '0'). You may enter multiple phone numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerFaxNo.ClientID %>">
                Petitioner's Fax No :</label>
            <asp:TextBox ID="txtPetitionerFaxNo" MaxLength="2000" CssClass="text-input medium-input"
                runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regApplicantFaxNo" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtPetitionerFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
                ValidationGroup="Add" />
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Enter Fax No. with STD code (without '0'). You may enter multiple Fax numbers
                separated by semicolon(;).</span></em>
        </p>
        <p>
            <label for="<%=txtPetitionerEmailID.ClientID %>">
                Petitioner's Email ID :</label>
            <asp:TextBox ID="txtPetitionerEmailID" CssClass="text-input medium-input" runat="server"
                MaxLength="2000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regPetitionerEmailid" runat="server" ControlToValidate="txtPetitionerEmailID"
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
                ID="regRespondentName" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for title on date 28-10-2013-->
            <asp:RegularExpressionValidator ID="regRespondentNam" runat="server" ErrorMessage="Please enter valid name. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRespondentName"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtRespondentAdd.ClientID %>">
                Respondent's Address :</label>
            <asp:TextBox ID="txtRespondentAdd" MaxLength="2000" CssClass="text-input medium-input"
                runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRespondentAdd"
                ID="regRespondentnameLength" ValidationExpression="^[\s\S]{2,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for title on date 28-10-2013-->
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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid Phone number."
                ControlToValidate="txtRespondentPhoneNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
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
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter valid Fax number."
                ControlToValidate="txtRespondentFaxNo" Display="Dynamic" ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
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
    </p>
    <p>
        <label for="<%=txtOrderTitle.ClientID %>">
            Order Description <span class="redtext">* </span>:</label>
        <asp:TextBox ID="txtOrderTitle" CssClass="text-input medium-input" runat="server"
            MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPetitionPROno" ValidationGroup="Add" runat="server"
            SetFocusOnError="true" ControlToValidate="txtOrderTitle" ErrorMessage="Please enter order title."
            Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOrderTitle"
            ID="regOrderTitleLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
        </asp:RegularExpressionValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regOrderDesc" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtOrderTitle"
            ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
    </p>
    <p id="pTitle" runat="server" visible="false">
        <label for="<%=txtOrderDescription.ClientID %>">
            Order Description <span class="redtext">* </span>:</label>
        <asp:TextBox ID="txtOrderDescription" CssClass="text-input medium-input" runat="server"
            TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOrderDescription"
            ID="regOrderDescLength" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
        </asp:RegularExpressionValidator>
        <!--Validation for title on date 28-10-2013-->
        <asp:RegularExpressionValidator ID="regOrderDescription" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtOrderDescription"
            ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <!--End-->
    </p>
    <p>
        <label for="<%=ddlOrderType.ClientID %>">
            Order Type <span class="redtext">* </span>:</label>
        <asp:DropDownList ID="ddlOrderType" runat="server" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
            AutoPostBack="true">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvOrderType" ValidationGroup="Add" runat="server"
            InitialValue="0" SetFocusOnError="true" ControlToValidate="ddlOrderType" ErrorMessage="Please select order type."
            Display="Dynamic">
        </asp:RequiredFieldValidator>
    </p>
    <p id="pCategory" runat="server" visible="false">
        <label for="<%=ChkCategory.ClientID %>">
            Order Category <span class="redtext">* </span>:</label>
        <asp:CheckBoxList ID="ChkCategory" CssClass="checkbox" runat="server" RepeatColumns="1"
            RepeatDirection="Horizontal" TextAlign="Right">
        </asp:CheckBoxList>
        <asp:CustomValidator runat="server" ID="cvmodulelist" ForeColor="Red" ClientValidationFunction="ValidateModuleList"
            ErrorMessage="Please select atleast one checkbox" OnServerValidate="valInquiry_ServerValidation"
            ValidationGroup="Add"></asp:CustomValidator>
    </p>
    <asp:Panel ID="PConnectedOrder" runat="server" Visible="false">
        <p>
            <label for="<%= lnkConnectedOrders.ClientID %>" style="color: Red">
                Is there any connected Amendment/Clarification/Corrigendum for this order ? :
                <asp:LinkButton ID="lnkConnectedOrders" runat="server" Text="Yes" OnClick="lnkConnectedOrders_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConnectedOrdersNo" runat="server" Text="No" OnClick="lnkConnectedOrdersNo_Click"
                    Visible="false">
                </asp:LinkButton>
            </label>
        </p>
        <p id="psubcategory" runat="server" visible="false">
            <label for="<%=ddlOrderSubCategory.ClientID %>">
                Order SubCategory <span class="redtext">* </span>:</label>
            <asp:DropDownList ID="ddlOrderSubCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderSubCategory_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Add" runat="server"
                InitialValue="0" SetFocusOnError="true" ControlToValidate="ddlOrderSubCategory"
                ErrorMessage="Please select order Subcategory." Display="Dynamic">
            </asp:RequiredFieldValidator>
        </p>
    </asp:Panel>
    <p id="PuploadFile" runat="server">
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
            <asp:RequiredFieldValidator ID="reqFileUploader" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="fileUploadPdf" Enabled="false" Display="Dynamic">
            </asp:RequiredFieldValidator>
           
            <%-- <asp:FileUpload ID="FileUpload1" runat="server" />--%>
            <!--FileUpload Controls will be added here -->
        </div>
        <input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
            tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Pdf file only</span></em>
    </p>
    <p id="PFileName" runat="server" visible="false">
        <asp:DataList ID="datalistFileName" runat="server" CellPadding="2" CellSpacing="1"
            RepeatColumns="1" RepeatDirection="Horizontal" OnItemCommand="datalistFileName_ItemCommand"
            OnItemDataBound="datalistFileName_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                <asp:HiddenField ID="hiddenFieldOrderId" runat="server" Value='<%#Eval("OrderID") %>' />
                <asp:HiddenField ID="hidCat" runat="server" Value='<%#Eval("CategoryName") %>' />
                <asp:HiddenField ID="hidSubCat" runat="server" Value='<%#Eval("SubCategoryName") %>' />
                <asp:HiddenField ID="hidComments" runat="server" Value='<%#Eval("Comments") %>' />
                <b>
                    <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label></b>
                <%--   <asp:LinkButton ID="lnkFileName" runat="server" Text='<%#bind("FileName") %>'></asp:LinkButton>--%>
                <asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>
                <asp:Label ID="lblOrderName" runat="server" Text='<%#bind("OrdertypeName") %>'></asp:Label>
                <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandName="File" CommandArgument='<%# Eval("Id") %>'>Remove File</asp:LinkButton>
                <asp:Literal ID="ltrlDownload" runat="server">
                </asp:Literal>
            </ItemTemplate>
        </asp:DataList>
    </p>
    <p>
        <label for="<%= txtOrderDate.ClientID %>">
            Order Date<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtOrderDate" CssClass="text-input small-input" runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ibOrderDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RequiredFieldValidator ID="rfvOrderDate" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" ErrorMessage="Please enter order date.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="rxvOrderDate" runat="server" ControlToValidate="txtOrderDate"
            Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
            ValidationGroup="Add">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-dd/mm/yyyy</span></em>
    </p>
    <p>
        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOrderDate"
            PopupButtonID="ibOrderDate">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOrderDate">
        </cc1:CalendarExtender>
    </p>
    <p id="pstartdate" runat="server" visible="false">
        <label for="<%= txtstartdate.ClientID %>">
            Start Date<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtstartdate" CssClass="text-input small-input" runat="server"></asp:TextBox>
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
    <p id="pEnddate" runat="server" visible="false">
        <label for="<%= txtendate.ClientID %>">
            End Date:</label>
        <asp:TextBox ID="txtendate" CssClass="text-input small-input" CausesValidation="true"
            runat="server"></asp:TextBox>
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
    </p>
    <p>
        <label for="<%= txtRemarks.ClientID %>">
            Remarks :</label>
        <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" MaxLength="2000"
            TextMode="MultiLine"></asp:TextBox>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRemarks"
            ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
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
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMetaDescription"
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
    </p>
    <p>
        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="txtendate"
            PopupButtonID="ImageButton2">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="txtendate">
        </cc1:CalendarExtender>
    </p>
    <p>
        <asp:Button ID="btnUpdateOrder" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
            OnClick="btnUpdateOrder_Click" CausesValidation="true" ToolTip="Click To Update" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
            OnClick="btnReset_Click" ToolTip="Click To Reset" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
            OnClick="btnBack_Click" ToolTip="Go Back" />
    </p>
</asp:Content>
