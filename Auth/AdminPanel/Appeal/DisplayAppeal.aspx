<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="DisplayAppeal.aspx.cs" Inherits="Auth_AdminPanel_Appeal_DisplayAppeal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .button
        {
            height: 26px;
        }
    </style>
    <script type="text/javascript">

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdAppeal.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdAppeal.ClientID %>');
            var TargetChildControl = "chkSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function validatecheckbox() {
            if (Counter == 0) {
                alert('Please select at least one check box.');
                return false;
            }
            else
                return true;

        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target base & child control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter;            
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
        function IMG1_onclick() {

        }
    </script>
    <script type="text/javascript">
        function ValidateColorList(source, args) {
            var chkListModules = document.getElementById('<%= chkSendEmails.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <script type="text/javascript">
        function ValidateColorListDis(source, args) {
            var chkListModules = document.getElementById('<%= chkSendEmailsDis.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <asp:Panel ID="pnlGrid" runat="server">
        <p style="float: right">
            <asp:Button ID="btnPdf" runat="server" CssClass="button" Text="Export to Excel" OnClick="btnPdf_Click"
                Visible="false" ToolTip="Export to Excel" />
            <asp:Button ID="btnAddAppeal" runat="server" CssClass="button" Text="Add New Appeal"
                OnClick="btnAddAppeal_Click" ToolTip="Add New Appeal" />
            <asp:Button ID="btnAppealAward" runat="server" CssClass="button" Text="Appeal Against Award"
                ToolTip="View Award" OnClick="btnAppealAward_Click" />
        </p>
        <p id="pnlDearptment" runat="server">
            <label for="<%=ddlDepartment.ClientID %>">
                Select Department<span class="redtext">*</span>:
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <p id="PLanguage" runat="server">
            <label for="<%=ddlLanguage.ClientID %>">
                Language<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <p id="PYear" runat="server">
            <label for="<%=ddlYear.ClientID %>">
                Year<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <p id="P3" runat="server">
            <label for="<%=ddlStatus.ClientID %>">
                Status<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <br />
        <br />
        <p>
            <asp:GridView ID="grdAppealPdf" runat="server" Visible="false" AutoGenerateColumns="false"
                Width="100%" OnRowDataBound="grdAppealPdf_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Appeal No" SortExpression="apealnum" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Appeal_Number" runat="server" Text='<%#Eval("Appeal_Number")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" SortExpression="Year" Visible="false" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Year")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="Appeal_Date" HeaderText="Appeal Date"
                        HeaderStyle-CssClass="Text-Center" DataField="Appeal_Date" />
                    <asp:TemplateField HeaderText="Appellant(s)" SortExpression="Applicant_Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantName" runat="server" Text='<%#Eval("Applicant_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appellant Address" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Applicant_Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appellant Mobile No" SortExpression="Applicant_Mobile_No"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Applicant_Mobile_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appellant Phone No" SortExpression="Applicant_Phone_No"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Applicant_Phone_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appellant Fax No" SortExpression="Applicant_Fax_No"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Applicant_Fax_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appellant Email" SortExpression="Applicant_Email"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Applicant_Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent(s)" SortExpression="Respondent_Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondent" runat="server" Text='<%#Eval("Respondent_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Address" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondentAddress" runat="server" Text='<%#Eval("Respondent_Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Mobile No" SortExpression="Respondent_Mobile_No"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Respondent_Mobile_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Phone No" SortExpression="Respondent_Phone_No"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Respondent_Phone_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Fax No" SortExpression="Respondent_Fax_No"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Respondent_Fax_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Email" SortExpression="Respondent_Email"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Respondent_Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lbSubject" runat="server" Text='<%#Eval("Subject") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Appeal_Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Appeal_Status")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="grdAppeal" runat="server" AutoGenerateColumns="false" DataKeyNames="Temp_Appeal_Id"
                OnRowCommand="grdAppeal_RowCommand" OnRowCreated="grdAppeal_RowCreated" OnRowDataBound="grdAppeal_RowDataBound"
                CssClass="mGrid" OnRowDeleting="grdAppeal_RowDeleting" AllowPaging="True" PageSize="10"
                OnPageIndexChanging="grdAppeal_PageIndexChanging" AllowSorting="true" OnSorting="grdAppeal_Sorting">
                <Columns>
                    <asp:TemplateField HeaderText="CheckAll">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField Visible="false">
                        <HeaderTemplate>
                            ID
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAppeal_ID" runat="server" Text='<%#Eval("Temp_Appeal_Id")%>'>
                       
                            </asp:Label>
                            <asp:HiddenField ID="StatusId" runat="server" Value='<%#Eval("Status_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appeal No" SortExpression="apealnum" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%-- <asp:LinkButton ID="lnk_Appeal_Number" runat="server" Text='<%#Eval("Appeal_Number")%>'
                            CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Appeal_Id") %>'></asp:LinkButton>--%>
                            <asp:Label ID="lblAppealNumber" runat="server" Text='<%#Eval("Appeal_Number") %>'></asp:Label>
                            <%--<%# DataBinder.Eval(Container.DataItem, "Appeal_Number")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" SortExpression="Year" Visible="false" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Year")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="Appeal_Date" HeaderText="Appeal Date"
                        HeaderStyle-CssClass="Text-Center" DataField="Appeal_Date" />
                    <asp:TemplateField HeaderText="Appellant(s)" SortExpression="Applicant_Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string)  Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Applicant_Name"),100))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent(s)" SortExpression="Respondent_Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string) Miscelleneous_DL.FixCharacters( DataBinder.Eval(Container.DataItem, "Respondent_Name"),100))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" SortExpression="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string) Miscelleneous_DL.FixCharacters( DataBinder.Eval(Container.DataItem, "Subject"),100))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Appeal_Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Appeal_Status")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Change Status
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkChangeStatus" runat="server" Text='Change Status' CommandName="ChangeStatus"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Appeal_Id")%>'></asp:LinkButton>
                            <asp:Label ID="lblchangestatus" runat="server" Visible='false' Text='Change Status'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Appeal(Y/N)
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAppealId" runat="server" Visible="false" />
                            <asp:LinkButton ID="lnkAppealId" runat="server" CommandName="Appeal" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Temp_Appeal_Id")%>'></asp:LinkButton>
                            <asp:HiddenField ID="HypStatus" runat="server" Value='<%#Eval("Appeal_Status_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="Appeal_Edit.aspx?appeal_Id=<%#DataBinder.Eval(Container.DataItem,"Temp_Appeal_Id")%> &ModuleID= <%=Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Appeal) %>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&Status=<%#ddlStatus.SelectedValue %>">
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="../images/pencil.png" />
                            </a>
                            <asp:Image ID="imgnotedit" runat="server" Visible="false" ToolTip="pending" Height="15"
                                Width="15" ImageUrl="../images/th_star.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="delete" CommandArgument='<%# Eval("Temp_Appeal_Id")+ ";" + Eval("Status_id")%>'
                                Text="Delete" ToolTip="Delete" /><%--OnClientClick="return confirm('Are You sure want to delete this appeal ?');"--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Restore
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("Temp_Appeal_Id") %>'
                                Text="Restore" ToolTip="Restore" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("Temp_Appeal_Id") %>'
                                Text="Audit" ToolTip="Audit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" FirstPageText="First"
                    LastPageText="Last" />
                <PagerStyle CssClass="pgr" />
            </asp:GridView>
            <p>
                <asp:Button ID="btnForReview" runat="server" Text="For review" CssClass="button"
                    OnClick="btnForReview_Click" OnClientClick="javascript:return validatecheckbox();"
                    ToolTip="Click To Send For Review" />
                &nbsp;<asp:Button ID="btnForApprove" runat="server" Text="For Publish" CssClass="button"
                    OnClick="btnForApprove_Click" OnClientClick="javascript:return validatecheckbox();"
                    ToolTip="Click To Send For Publish" />
                &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Publish" CssClass="button"
                    OnClientClick="javascript:return validatecheckbox();" OnClick="btnApprove_Click"
                    ToolTip="Click To Publish" />
                &nbsp;<asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" CssClass="button"
                    OnClientClick="javascript:return validatecheckbox();" OnClick="btnDisApprove_Click"
                    ToolTip="Click To Disapprove" />
            </p>
            <p>
            </p>
            <p>
            </p>
            <p>
            </p>
            <p>
            </p>
            <p>
            </p>
        </p>
    </asp:Panel>
    <!---------------- MODAL POPUP CONTROLS------------->
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="400px"
        Style="display: none">
        <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
            cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <asp:Label ID="lblDeleteMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                    <asp:Button ID="btnDelete" CommandName="Delete" CssClass="button" runat="server"
                        Text="Delete" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="Button1" runat="server" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender ID="mpuUpdateStatus" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlPopupChangeStatus" CancelControlID="btnCancelUpdate" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopupChangeStatus" runat="server" CssClass="ChangeStatus">
        <asp:UpdatePanel ID="upDatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpdateStatus" />
                <asp:AsyncPostBackTrigger ControlID="ddlAppealStatusUpdate" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <asp:DropDownList ID="ddlAppealStatusUpdate" runat="server" OnSelectedIndexChanged="ddlAppealStatusUpdate_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherStatus" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trUpLoader" runat="server" visible="false">
                        <td colspan="2">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Upload File:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fileUploadPdf" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Where Appealed:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWhereAppealed" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td valign="top">
                    <asp:FileUpload ID="fileUploadPdf" runat="server" />
                </td>--%>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnUpdateStatus" CommandName="ChangeStatus" CssClass="button" runat="server"
                                Text="Update" OnClick="btnUpdateStatus_Click" />
                            <asp:Button ID="btnCancelUpdate" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancelUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlAppealStatusUpdate" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <!---------------- END------------------------------>
    <asp:Panel ID="pnlPopUpEmails" runat="server" CssClass="ChangeStatus" Visible="false">
        <table width="100%">
            <tr>
                <td valign="top">
                    <asp:Label ID="lblSelectors" runat="server"></asp:Label><br />
                    <asp:CustomValidator runat="server" ForeColor="Red" ID="cvmodulelist" ClientValidationFunction="ValidateColorList"
                        ErrorMessage="Please select atleast one." ValidationGroup="email"></asp:CustomValidator>
                    <asp:CheckBoxList ID="chkSendEmails" runat="server" CssClass="checkbox1">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSendEmails" CommandName="SendEmails" CssClass="button" runat="server"
                        Text="Submit" OnClick="btnSendEmails_Click" ValidationGroup="email" />
                    <asp:Button ID="btnSendEmailsWithoutEmails" CommandName="SendEmails" CssClass="button"
                        runat="server" Text="Submit Without Email" OnClick="btnSendEmailsWithoutEmails_Click" />
                    <asp:Button ID="btnCancelEmail" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancelEmail_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPopUpEmailsDis" runat="server" CssClass="ChangeStatus" Visible="false">
        <table width="100%">
            <tr>
                <td valign="top">
                    <asp:Label ID="lblSelectorsDis" runat="server"></asp:Label><br />
                    <asp:CustomValidator runat="server" ForeColor="Red" ID="CustomValidator1" ClientValidationFunction="ValidateColorListDis"
                        ErrorMessage="Please select atleast one." ValidationGroup="emailDis"></asp:CustomValidator>
                    <asp:CheckBoxList ID="chkSendEmailsDis" runat="server" CssClass="checkbox1">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSendEmailsDis" CommandName="send" CssClass="button" runat="server"
                        Text="Submit" OnClick="btnSendEmailsDis_Click" ValidationGroup="emailDis" />
                    <asp:Button ID="btnSendEmailsWithoutEmailsDis" CommandName="SendEmailsDis" CssClass="button"
                        runat="server" Text="Submit Without Email" OnClick="btnSendEmailsWithoutEmailsDis_Click" />
                    <asp:Button ID="btnCancelEmailDis" runat="server" CssClass="button" Text="Cancel"
                        OnClick="btnCancelEmailDis_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
     <!-- Popup to display audit records -->
    <asp:Panel ID="pnlAudit" runat="server" CssClass="ChangeStatus">
        <asp:UpdatePanel ID="upDatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" colspan="2">
                            <asp:Literal ID="ltrlPetitionNo" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>Created</b>
                        </td>
                        <td valign="top">
                            <asp:Literal ID="ltrlCreation" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>Edited</b>
                        </td>
                        <td valign="top">
                            <asp:Literal ID="ltrlLastEdited" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>Reviewed</b>
                        </td>
                        <td valign="top">
                            <asp:Literal ID="ltrlLastReviewed" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>Published</b>
                        </td>
                        <td valign="top">
                            <asp:Literal ID="ltrlLastPublished" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnCan" runat="server" CssClass="button" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnShowAuditPopup" runat="server" Style="display: none" />
    <cc1:ModalPopupExtender ID="mdpAuditTrail" runat="server" TargetControlID="btnShowAuditPopup"
        PopupControlID="pnlAudit" CancelControlID="btnCan" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
</asp:Content>
