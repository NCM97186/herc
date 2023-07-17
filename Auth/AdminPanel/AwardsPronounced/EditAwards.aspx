<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="EditAwards.aspx.cs" Inherits="Auth_AdminPanel_AwardsPronounced_EditAwards" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
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
    <asp:Panel ID="pnlRTIAdd" runat="server">
        <%--<p id="PLanguage" runat="server">
            <label for="<%= ddlLanguage.ClientID %>">
                Select Language <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlLanguage" runat="server">
                </asp:DropDownList>
            </label>
        </p>--%>
        <p>
            <label for="<%=ddlYear.ClientID %>">
                Select Year <span class="redtext">* </span>:</label>
            <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add"
                ControlToValidate="ddlYear" ErrorMessage="Please select year." InitialValue="0">
            </asp:RequiredFieldValidator>
        </p>
        <p>
            <label for="<%=ddlPetitionNo.ClientID %>">
                Select Appeal No <span class="redtext">* </span>:</label>
            <asp:DropDownList ID="ddlPetitionNo" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPetitionNo" runat="server" ValidationGroup="Add"
                ControlToValidate="ddlPetitionNo" ErrorMessage="Please select appeal number."
                InitialValue="0">
            </asp:RequiredFieldValidator>
        </p>
        <p id="pAward" runat="server" visible="false">
            <label for="<%=txtOrderTitle.ClientID %>">
                Award Title <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtOrderTitle" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPetitionPROno" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtOrderTitle" ErrorMessage="Please enter Award title."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regOrderTitle" runat="server" ErrorMessage="Please enter valid title. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtOrderTitle" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOrderTitle"
                ID="regOrderTitleLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p>
            <label for="<%=txtOrderDescription.ClientID %>">
                Subject <span class="redtext">* </span>:</label>
            <asp:TextBox ID="txtOrderDescription" CssClass="text-input medium-input" runat="server"
                TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvOrderDescription" ValidationGroup="Add" runat="server"
                SetFocusOnError="true" ControlToValidate="txtOrderDescription" ErrorMessage="Please enter description."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regOrderDescription" runat="server" ErrorMessage="Please enter valid subject. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtOrderDescription" SetFocusOnError="true"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOrderDescription"
                ID="regsubjectValidation" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
        </p>
        <p id="PFile" runat="server" visible="false">
            <label for="<%= fileUploadPdf.ClientID %>">
                Upload File :</label>
            <asp:FileUpload ID="fileUploadPdf" runat="server" />
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            <asp:CustomValidator ID="CustvalidFileUplaod" runat="server" ClientValidationFunction="ValidateFileUpload"
                OnServerValidate="CustvalidFileUplaod_ServerValidate" ControlToValidate="fileUploadPdf"
                ValidationGroup="Add" ErrorMessage="Please select only .pdf file." Display="Dynamic">
            </asp:CustomValidator>

            <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Add" Display="Dynamic" runat="server" ClientValidationFunction="ValidateFileUpload" OnServerValidate="CustvalidFileUplaod_ServerValidate" ControlToValidate="fileUploadPdf" ErrorMessage="Please select only .pdf file."></asp:CustomValidator>

            <asp:Label ID="LblOldFile" runat="server" Text="Old File :"></asp:Label>
            <asp:Label ID="lblFileName" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
            
            &nbsp;<asp:LinkButton ID="lnkFileRemove" runat="server" OnClick="lnkFileRemove_Click">Remove File</asp:LinkButton><br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-Pdf file only</span></em>
        </p>
        <p>
            <label for="<%= txtOrderDate.ClientID %>">
                Award Date<span class="redtext">*</span>:</label>
            <asp:TextBox ID="txtOrderDate" CssClass="text-input small-input" runat="server" MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibOrderDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
            <asp:RequiredFieldValidator ID="rfvOrderDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" ErrorMessage="Please enter Award date.">
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
            <div id="FileUploadContainer">
                <label for="<%=FileUploader.ClientID %>">
                    File :
                
                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                <asp:FileUpload ID="FileUploader" runat="server" /></label>
                <%-- <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>--%>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateFileUpload"
                    ControlToValidate="FileUploader" ValidationGroup="Add" ErrorMessage="Please select only .pdf file."
                    Display="Dynamic">
                </asp:CustomValidator>
                <asp:RegularExpressionValidator ID="regFileDesc" runat="server" ErrorMessage="Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)"
                    ValidationGroup="Add" ControlToValidate="TextBox1" SetFocusOnError="true" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
                <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                    Tip:-Pdf file only</span></em>
            </div>
            <%--<input id="Button1" onclick="AddFileUpload()" style="height: 27px; width: 74px;"
                tabindex="25" type="button" value="add" validationgroup="upload" causesvalidation="true" />--%>
        </p>
        <p id="PFileName" runat="server" visible="false">
            <asp:DataList ID="datalistFileName" runat="server" CellPadding="2" CellSpacing="1"
                RepeatColumns="1" RepeatDirection="Horizontal" OnItemCommand="datalistFileName_ItemCommand"
                OnItemDataBound="datalistFileName_ItemDataBound">
                <ItemTemplate>
                    <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                    <asp:HiddenField ID="hiddenFieldAwardPronounced" runat="server" Value='<%#Eval("TmpID") %>' />
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
            <label for="<%=txtremarks.ClientID %>">
                Remarks</label>
            <asp:TextBox ID="txtremarks" CssClass="text-input medium-input" runat="server" MaxLength="2000"
                TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtremarks"
                ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{5,2000}$" runat="server"
                ValidationGroup="Add" ErrorMessage="Minimum 5 and maximum 2000 characters required.">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regAwardRemarks" runat="server" ErrorMessage="Please enter valid remarks. No special characters are allowed except (space,'-_.:;#()/&)"
                ValidationGroup="Add" ControlToValidate="txtremarks" SetFocusOnError="true" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"
                Display="Dynamic">
            </asp:RegularExpressionValidator>
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
            <asp:RequiredFieldValidator ID="reqMetaDescription" runat="server" ControlToValidate="txtMetaDescription"
                ErrorMessage="Please enter Meta Description." ValidationGroup="Add"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regmetadescrip" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaDescription"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
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
            <asp:RegularExpressionValidator ID="regmetatitel" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Add" ControlToValidate="txtMetaTitle"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
        </p>
        <p>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOrderDate"
                PopupButtonID="ibOrderDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOrderDate">
            </cc1:CalendarExtender>
        </p>
        <p>
            <asp:Button ID="btnUpdateReviewAward" runat="server" CssClass="button" Text="Update"
                ValidationGroup="Add" OnClick="btnUpdateReviewAward_Click" CausesValidation="true"
                ToolTip="Click To Save" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" CssClass="button"
                OnClick="btnReset_Click" ToolTip="Click To Reset" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="button"
                OnClick="btnBack_Click" ToolTip="Go Back" />
        </p>
    </asp:Panel>
</asp:Content>
