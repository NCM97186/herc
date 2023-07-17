<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="AddEditAppealAward.aspx.cs" Inherits="Auth_AdminPanel_Appeal_AddEditAppealAward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">



        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= FileUploader.ClientID %>');
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
    <div>
        <p>
            <asp:Label ID="lblAppealNo" CssClass="greentext" Font-Size="Medium" Text="Appeal Number :"
                runat="server"></asp:Label>
        </p>
        <p>
            <label for="<%= txtWhere_Appealed.ClientID %>">
                Where Appealed<span class="redtext">* </span>:
            </label>
            <asp:TextBox ID="txtWhere_Appealed" runat="server" CssClass="text-input small-input" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvWhere_Appealed" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtWhere_Appealed" ErrorMessage="Please enter where appealed."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid where appealed. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtWhere_Appealed"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>

                 <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtWhere_Appealed"
                ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{3,200}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 200 characters required.">
            </asp:RegularExpressionValidator>
            <br />
             <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Where Appealed upto 200 characters are allowed.</span></em>
        </p>
        <p>
            <label for="<%= txtRefNo.ClientID %>">
                Ref No(Where Appealed):
            </label>
            <asp:TextBox ID="txtRefNo" runat="server" CssClass="text-input small-input" MaxLength="200"></asp:TextBox>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regtxtRemarksAdd" runat="server" ErrorMessage="Please enter valid address. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtRefNo"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRefNo"
                ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{3,200}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 200 characters required.">
            </asp:RegularExpressionValidator>
            <br />
             <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Ref No(Where Appealed) upto 200 characters are allowed.</span></em>
            <!--End-->
        </p>
        <p>
            <label for="<%=txtdate.ClientID %>">
                Date(Where Appealed)<span class="redtext">* </span>:
            </label>
            <asp:TextBox ID="txtdate" CssClass="text-input small-input" runat="server" MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg" />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>
            <asp:RequiredFieldValidator ID="rfvApplicationDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtdate" Display="Dynamic" ErrorMessage="Please enter date.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rxvApplicationDate" runat="server" ControlToValidate="txtdate"
                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtAppealremarks.ClientID %>">
                Remarks</label>
            <asp:TextBox ID="txtAppealremarks" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAppealremarks"
                ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regAppealRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtAppealremarks"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Remarks upto 2000 characters are allowed.</span></em>
        </p>
        <p id="PSTatus" runat="server">
            <label for="<%= ddlAppealStatus.ClientID %>">
                Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlAppealStatus" runat="server" Enabled="false">
                    <asp:ListItem Value="10" Selected="True">In Process</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPetReviewStatus" runat="server" ControlToValidate="ddlAppealStatus"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select appeal status."
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
            </label>
        </p>
        <p id="PSTatusEdit" runat="server" visible="false">
            <label for="<%= ddlAppealStatus_Edit.ClientID %>">
                Select Appeal Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlAppealStatus_Edit" runat="server" OnSelectedIndexChanged="ddlAppealStatus_Edit_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvAppealStatus" runat="server" ControlToValidate="ddlAppealStatus_Edit"
                    InitialValue="0" Display="Dynamic" ErrorMessage="Please select appeal status."
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
            </label>
        </p>
        <p id="PJudgementDescription" runat="server" visible="false">
            <label for="<%=txtAppealSubject.ClientID %>">
                Judgement Description:</label>
            <asp:TextBox ID="txtAppealSubject" CssClass="text-input medium-input" runat="server"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAppealSubject"
                ID="regRespondentAddr" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regJudgementDescription" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtAppealSubject"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Description upto 2000 characters are allowed.</span></em>
        </p>
        <p id="pJudgement_Edit" runat="server" visible="false">
            <label for="<%= txtJudgement_Link.ClientID %>">
                Judgement_Link:
            </label>
            <asp:TextBox ID="txtJudgement_Link" runat="server" CssClass="text-input small-input"></asp:TextBox>
            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                Tip:-http://www.herc.gov.in </span></em>
            <asp:RegularExpressionValidator ID="RevLinkUrl" runat="server" ControlToValidate="txtJudgement_Link"
                Display="Dynamic" ValidationGroup="Add" ErrorMessage="Please enter valid url"
                ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtJudgement_Link" ErrorMessage="Please enter judgement link."
                Display="Dynamic"></asp:RequiredFieldValidator>--%>
        </p>
        <p id="pOtherDesc" runat="server" visible="false">
            <label for="<%= txtOtherDescription.ClientID %>">
                Other Description:
            </label>
            <asp:TextBox ID="txtOtherDescription" runat="server" CssClass="text-input medium-input"
                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOtherDescription"
                ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <!--Validation for small description on date 28-10-2013 for security-->
            <asp:RegularExpressionValidator ID="regOtherDesc" runat="server" ErrorMessage="Please enter valid description. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtOtherDescription"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <!--End-->
            <br />
            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                Tip:-Description upto 2000 characters are allowed.</span></em>
        </p>
        <p id="PFile" runat="server">
            <div id="FileUploadContainer">
                <label for="<%=FileUploader.ClientID %>">
                    File Description :
                </label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:FileUpload ID="FileUploader" runat="server" />
                <%-- <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>--%>
                <asp:CustomValidator ID="CustvalidFileUplaod" runat="server" ClientValidationFunction="ValidateFileUpload"
                    ControlToValidate="FileUploader" ValidationGroup="Add" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic">
                </asp:CustomValidator>
                <!--Validation for small description on date 28-10-2013 for security-->
                <asp:RegularExpressionValidator ID="regFileDesc" runat="server" ErrorMessage="Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)"
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="TextBox1"
                    ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                <!--End-->
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
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
                    <asp:HiddenField ID="hiddenFieldAppealId" runat="server" Value='<%#Eval("Appeal_Id") %>' />
                    <asp:HiddenField ID="hiddenFieldStatusId" runat="server" Value='<%#Eval("StatusId") %>' />
                    <b>
                        <asp:Label ID="lblFile" runat="server" Text='<%#bind("FileName") %>'></asp:Label></b>
                    <asp:Label ID="lblDate" runat="server" Text='<%#bind("Date") %>'></asp:Label>
                    <asp:Label ID="lblComments" runat="server" Text='<%#bind("Comments") %>'></asp:Label>
                    <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandName="File" CommandArgument='<%# Eval("Id") %>'>Remove File</asp:LinkButton>
                    <asp:Literal ID="ltrlDownload" runat="server">
                    </asp:Literal>
                </ItemTemplate>
            </asp:DataList>
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
            <asp:RegularExpressionValidator ID="regmetadesc" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaDescription"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaDescription"
                ID="regMetaDescriptionLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMetaDescription"
                ErrorMessage="Please enter Meta Description." ValidationGroup="Add"></asp:RequiredFieldValidator>
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
            <asp:RegularExpressionValidator ID="regMetatitile" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaTitle"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Meta Title upto 2000 characters are allowed.</span></em>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdate"
                PopupButtonID="ImageButton1">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdate">
            </cc1:CalendarExtender>
        </p>
        <p>
            <asp:Button ID="BtnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="Add"
                OnClick="BtnSubmit_Click" ToolTip="Click To Save" />
            <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
                OnClick="BtnUpdate_Click" ToolTip="Click To Update" Visible="false" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
                OnClick="btnReset_Click" ToolTip="Click To Reset" Visible="false" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
                OnClick="btnBack_Click" ToolTip="Go Back" />
        </p>
    </div>
</asp:Content>
