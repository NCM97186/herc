<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="View_News.aspx.cs" Inherits="Auth_AdminPanel_WhatsNew_View_News"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <asp:Panel ID="pnlGrid" runat="server">
        <div class="heading_center">
            <h4>
                View What's New</h4>
        </div>
        <p align="right">
            <asp:LinkButton ID="btnAddNew" runat="server" CssClass="button" OnClick="btnAddNew_Click"> Add What's New</asp:LinkButton>
        </p>
        <p id="P3" runat="server">
            <label for="<%=ddlStatus.ClientID %>">
                Status<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="text-input Verysmall-drop"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <p>
            
                <asp:GridView ID="GvAdd_Details" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                    DataKeyNames="News_ID" OnRowCommand="GvAdd_Details_RowCommand" OnRowDataBound="GvAdd_Details_RowDataBound">
                   
                    <PagerSettings FirstPageText="« First" LastPageText="Last »" Mode="NumericFirstLast"
                        PageButtonCount="2" />
                    <Columns>
                        <asp:TemplateField HeaderText="CheckAll" >
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:HeaderClick(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" onclick="javascript:ChildClick(this,this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container .DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="News_TITLE"  HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Title
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LnkTitle" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "News_ID") %>'
                                    CommandName="View" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "News_TITLE"), 30)%>'></asp:Label>
                                <asp:HiddenField ID="hidTitle" runat="server" Value='<%# Eval("News_TITLE") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="NewsDecription" HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Description
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblDetails" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(  DataBinder.Eval(Container.DataItem, "NewsDecription"), 40)%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Start Date
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblSD" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(  DataBinder.Eval(Container.DataItem, "OPENING_DT" ,"{0:dd MMM yyyy}"), 40)%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                End Date
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblED" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(  DataBinder.Eval(Container.DataItem, "LAST_DT","{0:dd MMM yyyy}"), 40)%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Edit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href='AddNews.aspx?ID=<%#DataBinder.Eval(Container.DataItem,"News_ID")%>&amp;ModuleID=<%=  Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Whats_New) %>&amp;LangID=<%#DataBinder.Eval(Container.DataItem,"FK_LANG_ID")%>&amp;StatusId=<%#DataBinder.Eval(Container.DataItem,"APPRV_STATUS")%>'>
                                    <asp:Image ID="imgedit" runat="server" ImageUrl="../images/pencil.png" ToolTip="Edit" />
                                </a>
                                <asp:Image ID="emgnotedit" runat="server" ImageUrl="../images/th_star.png" ToolTip="pending"
                                    Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnDelete" runat="server" CommandArgument='<%# Eval("News_ID") %>'
                                    CommandName="delete" ImageUrl="~/Auth/AdminPanel/images/cross.png" Text="Delete"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Restore
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnRestore" runat="server" CommandArgument='<%# Eval("News_ID") %>'
                                    CommandName="Restore" Height="15px" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                    Text="Restore" ToolTip="Restore" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Audit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                    Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("News_ID") %>'
                                    Text="Audit" ToolTip="Audit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            <p>
            </p>
            <p>
                <asp:Button ID="BtnForReview" runat="server" CssClass="button" OnClick="BtnForReview_Click"
                    OnClientClick="javascript:return validatecheckbox();" Text="For Review" ToolTip="Click To Send For Review" />
                &nbsp;<asp:Button ID="btnForApprove" runat="server" CssClass="button" OnClick="btnForApprove_Click"
                    OnClientClick="javascript:return validatecheckbox();" Text="For Publish" />
                &nbsp;<asp:Button ID="btnApprove" runat="server" CssClass="button" OnClick="btnApprove_Click"
                    OnClientClick="javascript:return validatecheckbox();" Text="Publish" />
                &nbsp;<asp:Button ID="btnDisApprove" runat="server" CssClass="button" OnClick="btnDisApprove_Click"
                    OnClientClick="javascript:return validatecheckbox();" Text="Disapprove" />
                <span style="float: right;">
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:Repeater>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPageSize" runat="server" Text="Page Size :"></asp:Label>
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="20" Value="20" />
                        <asp:ListItem Text="30" Value="30" />
                    </asp:DropDownList>
                </span>
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
            <p>
            </p>
                <p>
                </p>
        </p>
    </asp:Panel>
    <!---------------- Popup for checkboxlist to send emails------------------->
    <asp:Panel ID="pnlPopUpEmails" runat="server" CssClass="ChangeStatus" Visible="false">
        <table width="100%">
            <tr>
                <td valign="top">
                    <asp:Label ID="lblSelectors" runat="server"></asp:Label>
                    <asp:CustomValidator runat="server" ForeColor="Red" ID="cvmodulelist" ClientValidationFunction="ValidateColorList"
                        ErrorMessage="Please select atleast one." ValidationGroup="email"></asp:CustomValidator>
                    <asp:CheckBoxList ID="chkSendEmails" runat="server">
                    </asp:CheckBoxList>
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
                    <asp:Label ID="lblSelectorsDis" runat="server"></asp:Label>
                    <asp:CustomValidator runat="server" ForeColor="Red" ID="CustomValidator1" ClientValidationFunction="ValidateColorListDis"
                        ErrorMessage="Please select atleast one." ValidationGroup="emailDis"></asp:CustomValidator>
                    <asp:CheckBoxList ID="chkSendEmailsDis" runat="server">
                    </asp:CheckBoxList>
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
    <cc1:ModalPopupExtender ID="mdpAuditTrail" runat="server" TargetControlID="btnShowAuditPopup"
        PopupControlID="pnlAudit" CancelControlID="btnCan" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    </p>
</asp:Content>
