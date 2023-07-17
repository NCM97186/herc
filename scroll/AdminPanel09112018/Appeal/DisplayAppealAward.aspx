<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="DisplayAppealAward.aspx.cs" Inherits="Auth_AdminPanel_Appeal_DisplayAppealAward" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <asp:Button ID="btnAppealAward" runat="server" CssClass="button" Text="Appeal" ToolTip="View Appeal"
                OnClick="btnAppealAward_Click" />
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
                Width="100%" OnRowDataBound="grdAppealPdf_RowDataBound" DataKeyNames="Appeal_Id">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Appeal No" SortExpression="Appeal_Number1" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAppalNumber" runat="server" Text='<%#Eval("Appeal_Number")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Year")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="Appeal_Date" HeaderText="Appeal Date"
                        HeaderStyle-CssClass="Text-Center" DataField="Appeal_Date" ItemStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Ref No(Where Appealed)" SortExpression="ReferenceNumber"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ReferenceNumber")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Where Appealed" SortExpression="Where_Appealed" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Where_Appealed")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Judgement Link" SortExpression="Judgement_Link" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Judgement_Link")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Judgement Description" SortExpression="Description"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Description")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="statusname" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "statusname")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Remarks")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedFile1" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="grdAppeal" runat="server" AutoGenerateColumns="false" DataKeyNames="TmpID"
                CssClass="mGrid" AllowPaging="True" PageSize="10" AllowSorting="true" OnSorting="grdAppeal_Sorting"
                OnRowCreated="grdAppeal_RowCreated" OnRowCommand="grdAppeal_RowCommand" OnRowDataBound="grdAppeal_RowDataBound">
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
                            <asp:Label ID="lblAppeal_ID" runat="server" Text='<%#Eval("TmpID")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appeal No" SortExpression="Appeal_Number1" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAppealNo" runat="server" Text='<%#Eval("Appeal_Number")%>'></asp:Label>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Appeal_Number")%>--%>
                            <asp:Label ID="lblAppalNumber" runat="server" Text='<%#Eval("Appeal_Number1")%>'
                                Visible="false">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" SortExpression="Year" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Year")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="Appeal_Date" HeaderText="Appeal Date"
                        HeaderStyle-CssClass="Text-Center" DataField="Appeal_Date" ItemStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Where Appealed" SortExpression="Where_Appealed" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Where_Appealed")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="statusname" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "statusname")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="AddEditAppealAward.aspx?appeal_Id=<%#DataBinder.Eval(Container.DataItem,"Appeal_Id")%> &ModuleID=4&ID=<%#DataBinder.Eval(Container.DataItem,"ID")%>&TID=<%#DataBinder.Eval(Container.DataItem,"tmpid")%>">
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="~/Auth/AdminPanel/images/pencil.png" />
                            </a>
                            <asp:Image ID="imgnotedit" runat="server" Visible="false" ToolTip="pending" Height="15"
                                Width="15" ImageUrl="../images/th_star.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="Delete" CommandArgument='<%# Eval("TmpID")+ ";" + Eval("Status_id")%>'
                                Text="Delete" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Restore
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("TmpID") %>'
                                Text="Restore" ToolTip="Restore" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("TmpID") %>'
                                Text="Audit" ToolTip="Audit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" FirstPageText="First"
                    LastPageText="Last" />
                <PagerStyle CssClass="pgr" />
            </asp:GridView>
        </p>
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
                        Text="Submit" OnClick="btnSendEmailsDis_Click" ValidationGroup="emailDis" Style="height: 26px" />
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
