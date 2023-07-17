<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Add_New_Tariff.aspx.cs" Inherits="Auth_AdminPanel_Tariff_Add_New_Tariff"
    Title="" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateFileUpload(Source, args) {
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
    <div class="">
        <div style="display: block;" class="tab-content default-tab" id="tab1">
        </div>
        <div>
            <div align="center">
                <asp:Label ID="lblMsg" CssClass="errmsgBold" runat="server"></asp:Label></div>
            <div class="table">
                <table cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <th class="full" colspan="2">
                        </th>
                    </tr>
                    <tr class="bg" id="trDepartment" runat="server" visible="true">
                        <td class="text_padding" align="right" valign="top" width="185px">
                            Select Department<span class="redtext">* </span>:
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr class="bg" id="tr2" runat="server" visible="true">
                        <td class="text_padding" align="right" valign="top" width="185px">
                            Select Category<span class="redtext">* </span>:
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlcategory" runat="server" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr class="bg" id="trLanguage" runat="server" visible="true">
                        <td class="text_padding" align="right" valign="top" width="185px">
                            Select Language<span class="redtext">* </span>:
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trMenu" runat="server">
                        <td class="text_padding" align="right">
                            Tariff Type<span class="redtext">* </span>:
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlTariffType" CssClass="dropdown" runat="server" AutoPostBack="True"
                                CausesValidation="false" OnSelectedIndexChanged="ddlTariffType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="Please select Tariff Type"
                                ControlToValidate="ddlTariffType" InitialValue="0" ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="tr1" runat="server">
                        <td class="text_padding" align="right">
                            Tariff Heading<span class="redtext">* </span>:
                        </td>
                        <td class="last">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="text-input medium-input" MaxLength="1000"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTariffHeading" runat="server" ControlToValidate="txtYear"
                                Display="Dynamic" ErrorMessage="Please enter tariff heading" ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <!--Validation for small description on date 28-10-2013 for security-->
                            <asp:RegularExpressionValidator ID="regTariffHeading" runat="server" ErrorMessage="Please enter valid tariff heading. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Menu" ControlToValidate="txtYear"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtYear"
                                ID="regTariffHeadingd" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 2000 characters required.">
                            </asp:RegularExpressionValidator>
                            <!--End-->
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trFileupload" runat="server">
                        <td class="text_padding" align="right">
                            File Upload :
                        </td>
                        <td class="last">
                            <asp:FileUpload ID="fileUpload_Menu" runat="server" />
                            <asp:CustomValidator ID="CustvalidFileUplaod" runat="server" ClientValidationFunction="ValidateFileUpload"
                                OnServerValidate="CustvalidFileUplaod_ServerValidate" ControlToValidate="fileUpload_Menu"
                                ValidationGroup="Menu" ErrorMessage="Please select only .pdf file." Display="Dynamic">
                            </asp:CustomValidator>
                            <asp:Label ID="lblmenuMsg" runat="server" Text="Current File:" Visible="false"></asp:Label>

                            <asp:Literal ID="lblFileName" runat="server" Visible="false"></asp:Literal>
                             &nbsp;<asp:LinkButton ID="lnkFileRemove" runat="server" OnClick="lnkFileRemove_Click">Remove File</asp:LinkButton>
                            <br />
                            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                Tip:-Pdf file only</span></em>
                            <br />
                            <br />
                        </td>
                    </tr>
                   <%-- <tr align="right">
                        <td id="trdataList" runat="server" visible="false">
                            <asp:DataList ID="datalistFileName" runat="server" CellPadding="2" CellSpacing="1"
                                OnItemCommand="datalistFileName_ItemCommand" RepeatColumns="1" RepeatDirection="Horizontal"
                                OnItemDataBound="datalistFileName_ItemDataBound">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HypId" runat="server" Value='<%#Eval("Id") %>' />
                                    <asp:HiddenField ID="hiddenFieldLinkId" runat="server" Value='<%#Eval("Id") %>' />
                                    <asp:HiddenField ID="HyStatus" runat="server" Value='<%#Eval("Status_Id") %>' />
                                    <b>
                                        <asp:Label ID="lblFile" runat="server" Text='<%#bind("FILE_NAME") %>'></asp:Label>
                                    </b>
                                    <asp:LinkButton ID="lnkFileConnectedRemove" runat="server" CommandArgument='<%# Eval("Id") %>'
                                        CommandName="File" ToolTip="Remove File">Remove File</asp:LinkButton>
                                    <asp:Literal ID="ltrlDownload" runat="server">
                                    </asp:Literal>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>--%>
                    <tr id="trstartdate" runat="server">
                        <td>
                            Start Date:<span class="redtext">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="text-input medium-input"
                                MaxLength="10"></asp:TextBox>
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                                CausesValidation="false" />
                            <asp:RequiredFieldValidator ID="rfvOrderDate" ValidationGroup="Menu" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="Please enter start date.">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regStartDate" runat="server" ControlToValidate="txtStartDate"
                                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy" SetFocusOnError="True"
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                                ValidationGroup="Menu">
                            </asp:RegularExpressionValidator>
                            <br />
                            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                Tip:-dd/mm/yyyy </span></em>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                                PopupButtonID="Image1">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                                PopupButtonID="txtStartDate">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr id="trEnddate" runat="server" visible="false">
                        <td>
                            Expiry Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="text-input medium-input" MaxLength="10">
                            </asp:TextBox>
                            <asp:ImageButton ID="ibCal1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                                CausesValidation="false" />
                            <cc1:CalendarExtender ID="calextenderExpDate" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                PopupButtonID="ibCal1" runat="server">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                PopupButtonID="txtEndDate" runat="server">
                            </cc1:CalendarExtender>
                            <asp:RegularExpressionValidator ID="regEndDate" runat="server" ControlToValidate="txtEndDate"
                                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy" SetFocusOnError="True"
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                                ValidationGroup="Menu">
                            </asp:RegularExpressionValidator>
                            <br />
                        </td>
                    </tr>
                    <tr id="trFckEditor" runat="server" visible="true">
                        <td class="text_padding">
                            Page Description :
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="BasicModule" BasePath="~/fckeditor/"
                                Height="730" Width="675">
                            </FCKeditorV2:FCKeditor>
                        </td>
                    </tr>
                    <tr>
                        <td class="text_padding" align="right">
                            <label for="<%=txtMetaKeyword.ClientID %>">
                                Meta Keyword<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:TextBox ID="txtMetaKeyword" runat="server" MaxLength="2000" TextMode="MultiLine"
                                CssClass="text-input medium-input"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RevMetaKeyword" runat="server" ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                                ControlToValidate="txtMetaKeyword" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,' ])*"
                                ValidationGroup="Menu" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeyword"
                                ID="regMetakeywordLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMetaKeyword"
                                ErrorMessage="Please enter Meta keyword." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <br />
                            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                                Tip:-Meta Keywords upto 2000 characters are allowed with comma or space.</span></em>
                            <!--End-->
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="text_padding" align="right">
                            <label for="<%=txtMetaDescription.ClientID %>">
                                Meta Description<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="2000" TextMode="MultiLine"
                                CssClass="text-input medium-input"></asp:TextBox>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaDescription"
                                ID="regMetaDescriptionLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMetaDescription"
                                ErrorMessage="Please enter Meta description." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regmetadesription" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Menu" ControlToValidate="txtMetaDescription"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <br />
                            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                                Tip:-Meta Description upto 2000 characters are allowed.</span></em>
                            <!--End-->
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="text_padding" align="right">
                            <label for="<%=ddlMetaLang.ClientID %>">
                                Meta Language<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlMetaLang" runat="server">
                            </asp:DropDownList>
                            <!--End-->
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="text_padding" align="right">
                            <label for="<%=txtMetaTitle.ClientID %>">
                                Meta Title<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:TextBox ID="txtMetaTitle" runat="server" MaxLength="2000" TextMode="MultiLine"
                                CssClass="text-input medium-input"></asp:TextBox>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaTitle"
                                ID="RegularExpressionValidator18" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                                ErrorMessage="Please enter Meta Title." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regmetatitel" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Menu" ControlToValidate="txtMetaTitle"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <br />
                            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                                Tip:-Meta Title upto 2000 characters are allowed.</span></em>
                            <!--End-->
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="first">
                        </td>
                        <td class="last">
                            &nbsp;
                            <asp:Button ID="btnMenu" runat="server" OnClick="btnMenu_Click" ValidationGroup="Menu"
                                CssClass="button" Height="24px" ToolTip="Click To Save" CausesValidation="True" />
                            <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Reset" CausesValidation="False"
                                ToolTip="Click To Reset" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnBack" CssClass="button" OnClick="btnBack_Click" runat="server"
                                Text="Back" CausesValidation="False" ToolTip="Go Back" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
