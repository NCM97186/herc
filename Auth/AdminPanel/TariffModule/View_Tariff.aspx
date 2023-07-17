<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="View_Tariff.aspx.cs" Inherits="Auth_AdminPanel_Tariff_View_Taritt"
    Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdCMSMenu.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }


        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdCMSMenu.ClientID %>');
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
                alert('Please select at least one check box !');
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


        /****************************************************
    
        ****************************************************/
        var win = null;
        function NewWindow(mypage, myname, w, h, scroll, pos) {
            if (pos == "random") { LeftPosition = (screen.availWidth) ? Math.floor(Math.random() * (screen.availWidth - w)) : 50; TopPosition = (screen.availHeight) ? Math.floor(Math.random() * ((screen.availHeight - h) - 75)) : 50; }
            if (pos == "center") { LeftPosition = (screen.availWidth) ? (screen.availWidth - w) / 2 : 50; TopPosition = (screen.availHeight) ? (screen.availHeight - h) / 2 : 50; }
            if (pos == "default") { LeftPosition = 50; TopPosition = 50 }
            else if ((pos != "center" && pos != "random" && pos != "default") || pos == null) { LeftPosition = 0; TopPosition = 20 }
            settings = 'width=' + w + ',height=' + h + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=yes';
            win = window.open(mypage, myname, settings);
            if (win.focus) { win.focus(); }
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
            <asp:Button ID="btnAddNewPage" runat="server" CausesValidation="False" Text="Add New Tariff"
                CssClass="button" OnClick="btnAddNewPage_Click" ToolTip="Add New Tariff"></asp:Button>
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
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <p>
            <label for="<%=ddlcategory.ClientID %>">
                Category<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlcategory" runat="server" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </label>
        </p>
        <p id="Pcategory" runat="server">
            <label for="<%=ddlTariffType.ClientID %>">
                Tariff Type<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlTariffType" CssClass="dropdown" runat="server" AutoPostBack="True"
                    CausesValidation="false" OnSelectedIndexChanged="ddlTariffType_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <p id="pddlStatus" runat="server" visible="false">
            <label for="<%=ddlStatus.ClientID %>">
                Select Module Status: <span class="redtext">*
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                        ValidationGroup="Menu">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvModuleStatus" runat="server" SetFocusOnError="true"
                        ControlToValidate="ddlStatus" ErrorMessage="Please Select Status!" InitialValue="0">
               
                    </asp:RequiredFieldValidator></label>
        </p>
        <asp:Panel ID="pnlCMSMenu" runat="server">
            <p>
                <asp:GridView ID="grdCMSMenu" runat="server" AutoGenerateColumns="False" DataKeyNames="Temp_Link_Id"
                    GridLines="None" Width="100%" CssClass="mGrid" OnRowCommand="grdCMSMenu_RowCommand"
                    OnRowCreated="grdCMSMenu_RowCreated" OnRowDataBound="grdCMSMenu_RowDataBound"
                    AllowPaging="True" PageSize="10" OnPageIndexChanging="grdCMSMenu_PageIndexChanging"
                    AllowSorting="true" OnSorting="grdCMSMenu_Sorting">
                    <AlternatingRowStyle CssClass="alt" />
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
                            HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center" />
                        <asp:TemplateField HeaderText="Title" SortExpression="Name " Visible="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tariff Heading" SortExpression="Year" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTariffHeading" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Year")%>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created By" SortExpression="InsertedBY" Visible="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "InsertedBY")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date" SortExpression="Start_Date" HeaderStyle-CssClass="Text-Center"
                            ItemStyle-CssClass="Text-Center" Visible="false">
                            <ItemTemplate>
                            <asp:Label ID="lblstartdate" visible="false" runat="server" Text='<%#Eval("Start_Date") %>'></asp:Label>
                                <%# DataBinder.Eval(Container.DataItem, "Rec_Insert_Date")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                            <HeaderTemplate>
                                Edit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href="Add_New_Tariff.aspx?TempLink_Id=<%#DataBinder.Eval(Container.DataItem,"Temp_Link_Id")%>&ModuleID=<%#DataBinder.Eval(Container.DataItem,"Module_Id")%>&statusID=<%#DataBinder.Eval(Container.DataItem,"Status_Id")%>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&Catid=<%#DataBinder.Eval(Container.DataItem,"Cat_Id")%>">
                                    <asp:Image ID="imgedit" runat="server" ImageUrl="../images/edit.gif" ToolTip="Edit" />
                                </a>
                                <asp:Image ID="emgnotedit" runat="server" Height="10" ImageUrl="../images/th_star.png"
                                    Visible="false" ToolTip="Record present in pending list" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" runat="server" border="0" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Link_Id") %>'
                                    CommandName="Delete" ToolTip="Delete" Height="10" ImageUrl="../images/cross.png"
                                    Width="11" />
                                <%--OnClientClick="return confirm('Are You sure want to Delete?');"--%>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status_Id")%>'
                                    Visible="false">
                                </asp:Label>
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
                    <RowStyle CssClass="drow" Wrap="True" />
                </asp:GridView>
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
            </p>
            <asp:Button ID="BtnForReview" runat="server" Text="For review" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="BtnForReview_Click"
                ToolTip="Click To Send For Review" />
            &nbsp;<asp:Button ID="btnForApprove" runat="server" Text="For Publish" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnForApprove_Click"
                ToolTip="Click To Send For Publish" />
            &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Publish" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnApprove_Click"
                ToolTip="Click To Publish" />
            <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" CssClass="button"
                OnClientClick="javascript:return validatecheckbox();" OnClick="btnDisApprove_Click"
                ToolTip="Click To Disapprove" />
        </asp:Panel>
        </span>
    </asp:Panel>
    </span>
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
                        runat="server" Text="Submit Without Email" OnClick="btnSendEmailsWithoutEmails_Click"
                        Style="height: 26px" />
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
                        OnClick="btnCancelEmailDis_Click" Style="height: 26px" />
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
</asp:Content>
