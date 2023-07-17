<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Module_Display.aspx.cs" Inherits="Auth_AdminPanel_MixModules_Module_Display" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.GvAdd_Details.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.GvAdd_Details.ClientID %>');
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
        function ConfirmationBox(username) {

            var result = confirm('Are you sure you want to delete this record/ amendment number : ' + username + '');
            if (result) {

                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ConfirmationBoxRepeal(username) {

            var result = confirm('Are you sure you want to repeal/unrepeal this record/ amendment number: ' + username + '');
            if (result) {

                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ConfirmationBoxRestore(username) {

            var result = confirm('Are you sure you want to restore this record/ amendment number: ' + username + '');
            if (result) {

                return true;
            }
            else {
                return false;
            }
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
        <p class="buttonAlign">
            <asp:LinkButton ID="btnPdf" runat="server" CssClass="button" Text="Export to Excel"
                OnClick="btnPdf_Click" Visible="false" ToolTip="Export to Excel"></asp:LinkButton>
            <asp:LinkButton ID="btnAddNew" runat="server" CssClass="button" OnClick="btnAddNew_Click">Add Module</asp:LinkButton>
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
        <p id="P1" runat="server">
            <label for="<%= ddlModules.ClientID %>">
                Select Module<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged">
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
            <asp:GridView ID="grdMixPdf" runat="server" Width="100%" Visible="false" DataKeyNames="Temp_Link_Id"
                AutoGenerateColumns="false" OnRowDataBound="grdMixPdf_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField SortExpression="Name" HeaderText="Title" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="LnkTitle" runat="server" Text='<%#Server.HtmlDecode((string)Eval("Name"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="RegulationNo" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" HeaderStyle-ForeColor="Red">
                        <ItemTemplate>
                            <asp:Label ID="lblRegulation" runat="server" Text='<%#Eval("RegulationNo") %>'></asp:Label>
                            <asp:Label ID="lblregulationAmbendment" Visible="false" runat="server" Text='<%#Eval("RegulationNoAmbendment") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <HeaderTemplate>
                            Module_Id
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LblModuleID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Module_Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reference No." SortExpression="ReferenceNo" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblReferece" runat="server" Text='<%#Eval("ReferenceNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="GvAdd_Details" runat="server" DataKeyNames="Temp_Link_Id" AutoGenerateColumns="false"
                OnRowCommand="GvAdd_Details_RowCommand" OnRowCreated="GvAdd_Details_RowCreated"
                AllowPaging="True" PageSize="10" OnRowDataBound="GvAdd_Details_RowDataBound"
                CssClass="mGrid" AllowSorting="true" OnSorting="GvAdd_Details_Sorting" OnPageIndexChanging="GvAdd_Details_PageIndexChanging"
                Width="100%" OnRowDeleting="GvAdd_Details_RowDeleting">
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" FirstPageText="First"
                    LastPageText="Last" />
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
                            Module_Id
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LblModuleID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Module_Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Name" HeaderText="Title" HeaderStyle-CssClass="Text-Center">
                        <%--<HeaderTemplate>
                        Title
                    </HeaderTemplate>--%>
                        <ItemTemplate>
                            <%-- <asp:LinkButton ID="LnkTitle" runat="server" Text='<%#Server.HtmlDecode((string)Eval("Name"))%>'
                                CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Link_Id") %>'></asp:LinkButton>--%>
                            <asp:Label ID="LnkTitle" runat="server" Text='<%#Server.HtmlDecode((string)Eval("Name"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("File_Name")%>'>
                                  
                                     <img src="../images/pdf-icon.jpg" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                            </asp:LinkButton>
                            <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="RegulationNo" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" HeaderStyle-ForeColor="Red">
                        <ItemTemplate>
                            <asp:Label ID="lblRegulation" runat="server" Text='<%#Eval("RegulationNo") %>'></asp:Label>
                            <asp:Label ID="lblregulationAmbendment" Visible="false" runat="server" Text='<%#Eval("RegulationNoAmbendment") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%-- <a href="Add_Modules.aspx?Temp_Link_Id=<% str DataBinder.Eval(Container.DataItem,"Temp_Link_Id")%>&ModuleID=<%#DataBinder.Eval(Container.DataItem,"Module_Id")%>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&amb=<%#DataBinder.Eval(Container.DataItem,"RegulationNoAmbendment") %>&Status=<%#ddlStatus.SelectedValue %>">--%>
                            <a href='<%# string.Format("Add_Modules.aspx?Temp_Link_Id={0}&ModuleID={1}&LangID={2}&amb={3}&Status={4}&DepId={5}",Eval("Temp_Link_Id"),Eval("Module_Id"),Eval("Lang_Id"),Eval("RegulationNoAmbendment"),ddlStatus.SelectedValue,ddlDepartment.SelectedValue)%>'>
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="../images/pencil.png" /></a>
                            <asp:Image ID="emgnotedit" Visible="false" Height="10" runat="server" ToolTip="pending"
                                ImageUrl="../images/th_star.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="delete" CommandArgument='<%# Eval("Temp_Link_Id") %>' Text="Delete"
                                ToolTip="Delete" />
                            <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status_Id")%>'
                                Visible="false">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Repealed
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkrpld" runat="server" Text="Repealed" CommandName="Repealed"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Link_Id") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reference No." SortExpression="ReferenceNo" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblReferece" runat="server" Text='<%#Eval("ReferenceNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Restore
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("Temp_Link_Id") %>'
                                Text="Restore" ToolTip="Restore" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rec_Insert_Date" HeaderText="Insert Date" SortExpression="recInsertSort"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("Temp_Link_Id") %>'
                                Text="Audit" ToolTip="Audit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
            </asp:GridView>
        </p>
        <p>
            <asp:Button ID="BtnForReview" runat="server" Text="For review" CssClass="button"
                OnClick="BtnForReview_Click" OnClientClick="javascript:return validatecheckbox();"
                ToolTip="Click To Send For Review" />
            &nbsp;<asp:Button ID="btnForApprove" runat="server" Text="For Publish" CssClass="button"
                OnClick="btnForApprove_Click" OnClientClick="javascript:return validatecheckbox();"
                ToolTip="Click To Send For Publish" Height="26px" />
            &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Publish" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnApprove_Click"
                ToolTip="Click To Publish" Style="height: 26px" />
            &nbsp;<asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnDisApprove_Click"
                ToolTip="Click To Disapprove" />
        </p>
    </asp:Panel>
    <!---------------- Popup for checkboxlist to send emails------------------->
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
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="400px"
        Style="display: none">
        <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
            cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    Remarks:
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="text-input medium-input" TextMode="MultiLine"
                        Width="300px" Height="100%">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                    <asp:Button ID="btnReply" CommandName="Delete" CssClass="button" runat="server" Text="Submit"
                        OnClick="btnReply_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Popup to display audit records -->
    <asp:Panel ID="pnlAudit" runat="server" CssClass="ChangeStatus">
        <asp:UpdatePanel ID="upDatePanel1" runat="server">
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
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
</asp:Content>
