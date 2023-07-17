<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="PublicNotice_Edit.aspx.cs" Inherits="Auth_AdminPanel_PublicNotice_PublicNotice_Edit" %>

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
            if (daysDifference > 10000000 || daysDifference <= 0)
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
        <label for="<%=txtTitle.ClientID %>">
            Title<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtTitle" CssClass="text-input medium-input" runat="server" MaxLength="2000"
            TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RfvName" ValidationGroup="Add" runat="server" SetFocusOnError="true"
            ControlToValidate="txtTitle" ErrorMessage="Please enter title." Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regTitle" runat="server" ErrorMessage="Please enter valid title. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtTitle"
            ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTitle" ID="regTitleLength"
            ValidationExpression="^[\s\S]{5,2000}$" runat="server" ValidationGroup="Add"
            ErrorMessage="Minimum 5 and maximum 2000 characters required."></asp:RegularExpressionValidator>
    </p>
    <p>
        <label for="<%= lnkConnectedPetition.ClientID %>" style="color: Red">
            Are you sure to connect petition/Review Petition with this public notice? :
            <asp:LinkButton ID="lnkConnectedPetition" runat="server" Text="Yes" OnClick="lnkConnectedPetition_Click">
            </asp:LinkButton>
            <asp:LinkButton ID="lnkConnectedPetitionNo" runat="server" Text="No" OnClick="lnkConnectedPetitionNo_Click"
                Visible="false">
            </asp:LinkButton>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPetitionStatus"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select petition status."
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>--%>
        </label>
    </p>
    <div style="width: 487px; max-height: 350px; overflow: auto;" id="divConnectAdd"
        runat="server">
        <asp:Panel ID="pnlPetitionConnection" runat="server" Visible="false">
            <%--<p class="floating">--%>
            <p>
                <label for="<%=ddlConnectionType.ClientID %>">
                    Select One <span class="redtext">* </span>:</label>
                <asp:DropDownList ID="ddlConnectionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlConnectionType_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                    <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                </asp:DropDownList>
            </p>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <div class="chekbox-value" style="color: Red; font-weight: bold">
                        <asp:Literal ID="ltrlSelected" runat="server"></asp:Literal></div>
                    <div class="chekbox-left">
                        <p id="PYear" runat="server">
                            <label for="<%=ddlYear.ClientID %>">
                                Year<span class="redtext">* </span>:
                                <asp:CheckBoxList ID="ddlYear" runat="server" CssClass="checkbox" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </label>
                        </p>
                        <asp:CheckBoxList ID="chklstPetition" CssClass="checkbox" runat="server" RepeatColumns="1"
                            RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="chklstPetition_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <p>
        <div id="FileUploadContainer">
            <label for="<%=fileUploadPdf.ClientID %>">
                File Description :
            </label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:FileUpload ID="fileUploadPdf" runat="server" />
            
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Pdf file only</span></em>
                <asp:CustomValidator ID="CustvalidFileUplaod" ForeColor="Red" runat="server" ClientValidationFunction="ValidateFileUpload"
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
                <asp:HiddenField ID="hiddenFieldPublicNoticeID" runat="server" Value='<%#Eval("PublicNoticeId") %>' />
                <b>
                    <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label></b>
                <%--<asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>--%>
                <asp:Label ID="lblComments" runat="server" Text='<%#bind("Comments") %>'></asp:Label>
                <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandName="File" CommandArgument='<%# Eval("Id") %>'>Remove File</asp:LinkButton>
                <asp:Literal ID="ltrlDownload" runat="server">
                </asp:Literal>
            </ItemTemplate>
        </asp:DataList>
    </p>
    <p>
                <label for="<%= txtURLEdit.ClientID %>">
                    URLs :</label>
                <asp:TextBox ID="txtURLEdit" runat="server" CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
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
            <asp:TextBox ID="txtURLDescriptionEdit" runat="server" CssClass="text-input medium-input" MaxLength="2000" TextMode="MultiLine"
                ></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtURLDescriptionEdit"
                ID="RegularExpressionValidator25" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Update" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ErrorMessage="Please enter valid URL description. No special characters are allowed except (space,'-_.:;#()/)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Update" ControlToValidate="txtURLDescriptionEdit"
                ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/ ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
           
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-You may enter description for multiple URLs separated by semicolon(;).</span></em>
           
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
    </p>
    <p>
        <label for="<%= txtendate.ClientID %>">
            End Date<span class="redtext">*</span>:</label>
        <asp:TextBox ID="txtendate" CssClass="text-input small-input" CausesValidation="true"
            runat="server" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
            CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Add" SetFocusOnError="true"
            runat="server" ControlToValidate="txtendate" Display="Dynamic" ErrorMessage="Please enter end date.">
        </asp:RequiredFieldValidator>
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
        <label for="<%=txtEmailID.ClientID %>">
            HERC Authority Email ID :</label>
        <asp:TextBox ID="txtEmailID" CssClass="text-input medium-input" runat="server" MaxLength="2000"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regPetitionerEmailid" runat="server" ControlToValidate="txtEmailID"
            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$"
            ErrorMessage="Please enter valid email id." ValidationGroup="Add"></asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-You may enter multiple Email Id separated by semicolon(;).</span></em>
    </p>
    <p>
        <label for="<%=txtMobileNo.ClientID %>">
            HERC Authority Mobile No :</label>
        <asp:TextBox ID="txtMobileNo" MaxLength="2000" CssClass="text-input medium-input"
            runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regPetitionerMobileNo" runat="server" ControlToValidate="txtMobileNo"
            ErrorMessage="Please enter valid Mobile number." ValidationExpression="^[1-9][0-9]{9,10}(([;][1-9][0-9]{9,10})*)?$"
            ValidationGroup="Add" Display="Dynamic">
        </asp:RegularExpressionValidator>
        <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Don't start with '0' (zero). You may enter multiple mobile numbers separated
            by semicolon(;).</span></em>
    </p>
    <p>
        <label for="<%= FCKeditor1.ClientID %>">
            Description<span class="redtext">*</span>:</label>
        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" Height="400px"
            Width="650px" ToolbarSet="BasicModule">
        </FCKeditorV2:FCKeditor>
    </p>
    <p>
        <label for="<%= txtRemarks.ClientID %>">
            Remarks :</label>
        <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" MaxLength="2000"
            TextMode="MultiLine" onkeypress="if(this.value.length>=2000) this.value = this.value.substring(0, 1999);"></asp:TextBox>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRemarks"
            ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
            ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
        </asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="regRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRemarks"
            ValidationExpression="([\u0900-\u097F]|[\u2013]|[\u2019]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
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
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMetaKeyword"
            ErrorMessage="Please enter Meta keyword." ValidationGroup="Add"></asp:RequiredFieldValidator>
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
            ErrorMessage="Please enter Meta description." ValidationGroup="Add"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regmtadescr" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaDescription"
            ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                    <br />
        <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
            Tip:-Meta Description upto 2000 characters are allowed.</span></em>
    </p>
    <p>
        <label for="<%=ddlMetaLang.ClientID %>">
            Meta Language :</label>
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
        <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
            OnClick="BtnUpdate_Click" ToolTip="Click To Update" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
            OnClick="btnReset_Click" ToolTip="Click To Reset" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
            OnClick="btnBack_Click" ToolTip="Go Back" />
    </p>
</asp:Content>
