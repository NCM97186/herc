<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Module_Display.aspx.cs" Inherits="Auth_AdminPanel_Module_Module_Display" %>
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
                OnClick="btnPdf_Click" Visible="false" ToolTip="Export to Excel" />
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
            <asp:GridView ID="grdModulePdf" runat="server" DataKeyNames="Temp_Link_Id" AutoGenerateColumns="false"
                Visible="false" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="CheckAll" Visible="false">
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
                        <ItemTemplate>
                            <%-- <asp:LinkButton ID="LnkTitle" runat="server" Text='<%#Eval("Name")%>' CommandName="View"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Link_Id") %>'></asp:LinkButton>--%>
                            <asp:Label ID="lblTitle" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Name"),200)%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CirculationPublicNotice" HeaderText="Advertisement/Circulation/Public Notice"
                        Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "CirculationPublicNotice")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Details" HeaderText="Description" Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Details")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField SortExpression="LastDateOfReceivingComment"   ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                    Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LastDateOfReceivingComment")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" Visible="false" SortExpression="LastDateOfReceivingComment"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" DataField="LastDateOfReceivingComment" />
                    <asp:TemplateField SortExpression="PublicHearingDate" HeaderText="Date of Public Hearing"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PublicHearingDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venue" Visible="false" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Venu")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date" Visible="false" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Start_Date")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date" Visible="false" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "End_Date")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Name" Visible="false" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "File_Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="GvAdd_Details" runat="server" DataKeyNames="Temp_Link_Id" AutoGenerateColumns="false"
                OnRowCommand="GvAdd_Details_RowCommand" OnRowCreated="GvAdd_Details_RowCreated"
                OnRowDataBound="GvAdd_Details_RowDataBound" CssClass="mGrid" AllowPaging="True"
                PageSize="10" OnPageIndexChanging="grdCMSMenu_PageIndexChanging" AllowSorting="true"
                OnSorting="grdCMSMenu_Sorting" OnRowDeleting="GvAdd_Details_RowDeleting">
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" FirstPageText="« First"
                    LastPageText="Last »" />
                <Columns>
                    <asp:TemplateField HeaderText="CheckAll" Visible="false">
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
                        <ItemTemplate>
                        <asp:Label ID="lblstartdate" visible="false" runat="server" Text='<%#Eval("Start_Date") %>'></asp:Label>
                            <%-- <asp:LinkButton ID="LnkTitle" runat="server" Text='<%#Eval("Name")%>' CommandName="View"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Link_Id") %>'></asp:LinkButton>--%>
                            <asp:Label ID="lblTitle" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Name"),200)%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CirculationPublicNotice" HeaderText="Advertisement/Circulation/Public Notice"
                        Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "CirculationPublicNotice")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Details" HeaderText="Description" Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Details")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField SortExpression="LastDateOfReceivingComment"   ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                    Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LastDateOfReceivingComment")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" Visible="false" SortExpression="LastDateOfReceivingComment"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" DataField="LastDateOfReceivingComment" />
                    <asp:TemplateField SortExpression="PublicHearingDate" HeaderText="Date of Public Hearing"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" Visible="false">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlPublicHearingDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PublicHearingDate")%>'>
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Venu" HeaderText="Venue" Visible="false" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Venu")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="Module_Add.aspx?Temp_Link_Id=<%#DataBinder.Eval(Container.DataItem,"Temp_Link_Id")%>&ModuleID=<%#DataBinder.Eval(Container.DataItem,"Module_Id")%>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&Status=<%#ddlStatus.SelectedValue %>">
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="../images/pencil.png" /></a>
                            <asp:Image ID="emgnotedit" runat="server" ToolTip="pending" />
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
                            <asp:LinkButton ID="lnkrpld" runat="server" Text="Repeal" CommandName="Repealed"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Link_Id") %>'></asp:LinkButton>
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
                    <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlFile" runat="server" Text='<%#Eval("File_Name") %>'></asp:Literal>
                            <asp:Literal ID="ltrlImage" runat="server" Text='<%#Eval("Image_Name") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Inserted Date" DataField="Rec_Insert_Date" Visible="false" />
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
                ToolTip="Click To Send For Publish" Style="height: 26px" />
            &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Publish" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnApprove_Click"
                ToolTip="Click To Publish" />
            &nbsp;<asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnDisApprove_Click"
                ToolTip="Click To Disapprove" />
        </p>
    </asp:Panel>
    <p>
        &nbsp;<asp:Panel ID="pnlPopUpEmails" runat="server" CssClass="ChangeStatus" Visible="false">
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
                            Text="Submit" OnClick="btnSendEmails_Click" ValidationGroup="email" Style="height: 26px" />
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
    <cc1:modalpopupextender ID="mdpAuditTrail" runat="server" TargetControlID="btnShowAuditPopup"
        PopupControlID="pnlAudit" CancelControlID="btnCan" 
        BackgroundCssClass="modalBackground">
    </cc1:modalpopupextender>
    </p>
</asp:Content>
