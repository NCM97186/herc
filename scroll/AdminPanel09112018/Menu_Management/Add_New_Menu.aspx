<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Add_New_Menu.aspx.cs" Inherits="Auth_AdminPanel_Menu_Management_Add_New_Menu" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <%--<div class="content-box-header">
        <h3 style="cursor: s-resize;">
            <asp:Label ID="lblnotification" runat="server"></asp:Label>
        </h3>
    </div>--%>
    <div class="content-box-content">
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
                            <label for="<%=ddlDepartment.ClientID %>">
                                Select Department<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr class="bg" id="trLanguage" runat="server" visible="true">
                        <td class="text_padding" align="right" valign="top" width="185px">
                            <label for="<%=ddlLanguage.ClientID %>">
                                Select Language<span class="redtext">* </span>:</label>
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
                            <label for="<%=ddlMenuPosition.ClientID %>">
                                Menu Position<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlMenuPosition" CssClass="dropdown" runat="server" AutoPostBack="True"
                                CausesValidation="false" OnSelectedIndexChanged="ddlMenuPosition_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr class="bg" id="trListBox" runat="server" visible="true">
                        <td class="text_padding" align="right" valign="top" width="185px">
                            <label for="<%=ListBox1.ClientID %>">
                                Select Main Page Name<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:ListBox ID="ListBox1" runat="server" CssClass="dropdown" Width="250px" Height="167px">
                            </asp:ListBox>
                            <asp:RequiredFieldValidator ID="RfvPageName" ControlToValidate="ListBox1" ValidationGroup="Menu"
                                runat="server" ErrorMessage="Please select page name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="text_padding" align="right">
                            <label for="<%=txtMenuName.ClientID %>">
                                Menu Name<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:TextBox ID="txtMenuName" runat="server" CssClass="text-input medium-input" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ReqMainMenu" runat="server" ControlToValidate="txtMenuName"
                                Display="Dynamic" ErrorMessage="Please enter menu name" ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMenuName"
                                ID="RegularExpressionValidator21" ValidationExpression="^[\s\S]{3,50}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter valid menu name. No special characters are allowed except (space,& -_.)"
                                ControlToValidate="txtMenuName" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Menu"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[ ,&']|[-]|[_\./])*"></asp:RegularExpressionValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="text_padding" align="right">
                            <label for="<%=ddlMenuType.ClientID %>">
                                Menu Type<span class="redtext">* </span>:</label>
                        </td>
                        <td class="last">
                            <asp:DropDownList ID="ddlMenuType" CssClass="dropdown" runat="server" AutoPostBack="True"
                                CausesValidation="false" OnSelectedIndexChanged="ddlMenuType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trFileupload" runat="server" visible="false">
                        <td class="text_padding" align="right">
                            <label for="<%=fileUpload_Menu.ClientID %>">
                                File Upload :
                            </label>
                        </td>
                        <td class="last">
                            <asp:FileUpload ID="fileUpload_Menu" runat="server" />
                            <asp:Label ID="lblmenuMsg" runat="server" Text="Current File:" Visible="false"></asp:Label>
                            <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateFileUpload1"
                                ControlToValidate="fileUpload_Menu" OnServerValidate="CustomValidator3_ServerValidate"
                                ValidationGroup="Menu" ErrorMessage="Please select only .pdf file." Display="Dynamic"> </asp:CustomValidator>
                            <br />
                            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                Tip:-Pdf file only</span></em>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trLinkUrl" runat="server" visible="false">
                        <td class="text_padding" align="right">
                            <label for="<%=txtLinkUrl.ClientID %>">
                                Link Url<span class="redtext">* </span>:
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="text-input medium-input"></asp:TextBox>
                            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                Tip:-http://www.abc.com </span></em>
                            <asp:RegularExpressionValidator ID="RevLinkUrl" runat="server" ControlToValidate="txtLinkUrl"
                                Display="Dynamic" ErrorMessage="Please enter valid url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                ValidationGroup="Menu"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RfvUrlName" runat="server" ControlToValidate="txtLinkUrl"
                                ErrorMessage="Please enter link url" ValidationGroup="Menu" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:Label ID="Label2" runat="server" ForeColor="#CC3300" Text="Invalid Url" Visible="False"></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trPageTitle" runat="server">
                        <td class="text_padding" align="right">
                            <label for="<%=txtPageTitle.ClientID %>">
                                Page Title<span class="redtext">* </span>:
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPageTitle" runat="server" CssClass="text-input medium-input"
                                MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RfvPageTitle" runat="server" ControlToValidate="txtPageTitle"
                                Display="Dynamic" ErrorMessage="Please enter page title" ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPageTitle"
                                ID="regPageTitleLength" ValidationExpression="^[\s\S]{3,50}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="regPageTitle" runat="server" ErrorMessage="Enter only alphanumeric values,don't use any special character even space."
                                ControlToValidate="txtPageTitle" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Menu"
                                ValidationExpression="^([\u0900-\u097FA-Za-z0-9]*)$"></asp:RegularExpressionValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trBrowserTitle" runat="server">
                        <td class="text_padding" align="right" valign="top">
                            <label for="<%=txtBrowserTitle.ClientID %>">
                                Browser Title :
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBrowserTitle" runat="server" CssClass="text-input medium-input"
                                MaxLength="50"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RevBrowserTitle" runat="server" ErrorMessage="Please enter only alphabets in  browser title with one of the following special characters _ and -"
                                ControlToValidate="txtBrowserTitle" ValidationExpression="[\u0900-\u097Fa-zA-Z_-]+"
                                ValidationGroup="Menu" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtBrowserTitle"
                                ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{3,50}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 50 characters required."></asp:RegularExpressionValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trUrlName" runat="server">
                        <td class="text_padding" align="right" valign="top">
                            <label for="<%=txtUrlName.ClientID %>">
                                Url Name <span class="redtext">*: </span>
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUrlName" runat="server" CssClass="text-input medium-input" MaxLength="50"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regUrlName" runat="server" ErrorMessage="Please enter url name in english with following special characters:(_-)"
                                ControlToValidate="txtUrlName" ValidationExpression="[0-9a-zA-Z_-]+" ValidationGroup="Menu"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ControlToValidate="txtUrlName" ID="regUrlNameLength"
                                ValidationExpression="^[\s\S]{3,50}$" runat="server" ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 50 characters required."
                                Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfUrlname" runat="server" ControlToValidate="txtUrlName"
                                Display="Dynamic" ErrorMessage="Please enter url name." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trmetakeyword" runat="server">
                        <td class="text_padding" align="right" valign="top">
                            <label for="<%=txtMetaKeyword.ClientID %>">
                                Meta Keyword<span class="redtext">*: </span>
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMetaKeyword" runat="server" MaxLength="2000" TextMode="MultiLine"
                                CssClass="text-input medium-input"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RevMetaKeyword" runat="server" ErrorMessage="Please enter valid data. No special characters are allowed except (space and ,)"
                                ControlToValidate="txtMetaKeyword" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[ ]|[,]|['])*"
                                ValidationGroup="Menu" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaKeyword"
                                ID="regMetakeywordLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMetaKeyword"
                                ErrorMessage="Please enter Meta Keywords." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <br />
                            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                                Tip:-Meta Keywords upto 2000 characters are allowed with comma or space.</span></em>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trMetadescription" runat="server">
                        <td class="text_padding" align="right">
                            <label for="<%=txtMetaDescription.ClientID %>">
                                Meta Description<span class="redtext">*: </span>
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="2000" TextMode="MultiLine"
                                CssClass="text-input medium-input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMetaDescription"
                                ErrorMessage="Please enter Meta Description." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaDescription"
                                ID="regMetaDescriptionLength" ValidationExpression="^[\s\S]{3,2000}$" runat="server"
                                ValidationGroup="Menu" ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="regmtadescr" runat="server" ErrorMessage="Please enter valid meta description. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Menu" ControlToValidate="txtMetaDescription"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <br />
                            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                                Tip:-Meta Description upto 2000 characters are allowed.</span></em>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trMetaTitle" runat="server">
                        <td class="text_padding" align="right">
                            <label for="<%=txtMetaTitle.ClientID %>">
                                Meta Title<span class="redtext">*: </span>
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMetaTitle" runat="server" MaxLength="2000" TextMode="MultiLine"
                                CssClass="text-input medium-input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMetaTitle"
                                ErrorMessage="Please enter Meta Title." ValidationGroup="Menu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMetaTitle"
                                ID="regMetatitle" ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="Menu"
                                ErrorMessage="Minimum 3 and maximum 2000 characters required."></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="regmetatitlespecialchar" runat="server" ErrorMessage="Please enter valid meta title. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Menu" ControlToValidate="txtMetaTitle"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <br />
                            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                                Tip:-Meta Title upto 2000 characters are allowed.</span></em>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="pStart" runat="server" visible="false">
                        <td>
                            <label for="<%=txtStartDate.ClientID %>">
                                Start Date:
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="text-input medium-input"
                                MaxLength="10"></asp:TextBox>
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                                CausesValidation="false" />
                            <asp:RegularExpressionValidator ID="regStartDate" runat="server" ControlToValidate="txtStartDate"
                                Display="Dynamic" ErrorMessage="Date format should be in dd/mm/yyyy" SetFocusOnError="True"
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                                ValidationGroup="Menu"> </asp:RegularExpressionValidator>
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
                    <tr id="pEnd" runat="server" visible="false">
                        <td>
                            <label for="<%=txtEndDate.ClientID %>">
                                Expiry Date:
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="text-input medium-input" MaxLength="10"> </asp:TextBox>
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
                                ValidationGroup="Menu"> </asp:RegularExpressionValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr id="trFckEditor" runat="server" visible="true">
                        <td class="text_padding">
                            <label for="<%=FCKeditor1.ClientID %>">
                                Page Description :
                            </label>
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="Basic" BasePath="~/fckeditor/"
                                Height="730" Width="675">
                            </FCKeditorV2:FCKeditor>
                        </td>
                    </tr>
                    <tr>
                        <td class="first">
                        </td>
                        <td class="last">
                            &nbsp;
                            <asp:Button ID="btnMenu" runat="server" ValidationGroup="Menu" CssClass="button"
                                Height="24px" OnClick="btnMenu_Click" ToolTip="Click To Save" />
                            <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Reset" CausesValidation="False"
                                OnClick="btnCancel_Click" ToolTip="Click To Reset" />
                            <asp:Button ID="btnBack" CssClass="button" runat="server" Text="Back" CausesValidation="False"
                                OnClick="btnBack_Click" ToolTip="Go Back" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
