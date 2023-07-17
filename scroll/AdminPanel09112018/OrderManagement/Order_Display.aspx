<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Order_Display.aspx.cs" Inherits="Auth_AdminPanel_OrderManagement_Order_Display" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdOrders.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdOrders.ClientID %>');
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
            <asp:Button ID="btnAddOrder" runat="server" CssClass="button" Text="New Order" OnClick="btnAddOrder_Click"
                ToolTip="Add New Order" />
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
        <p>
            <label for="<%=ddlOrderType.ClientID %>">
                Order Type <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlOrderType" runat="server" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </label>
        </p>
        <p id="P3" runat="server">
            <label for="<%=ddlStatus.ClientID %>">
                Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <br />
        <br />
        <p>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="grdOrderPdf" runat="server" Visible="false" AutoGenerateColumns="false"
                DataKeyNames="TempOrderID" OnRowDataBound="grdOrderPdf_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="Petition/RA No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPetition" runat="server" Text='<%#Eval("PRONO")%>'>
                            </asp:Label>
                            <asp:Label ID="lblReview" runat="server" Text='<%#Eval("Review")%>' Visible="false">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" SortExpression="OrderTitle" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderTitle")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPetitionerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Petitioner_Name")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Address" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPetitionerAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Petitioner_Address")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Mobile No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Petitioner_Mobile_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Phone No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Petitioner_Phone_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Fax No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Petitioner_Fax_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Email" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Petitioner_Email")%>'
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondentName" runat="server" Text='  <%# DataBinder.Eval(Container.DataItem, "Respondent_Name")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Address" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRespondentAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Respondent_Address")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Mobile No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Respondent_Mobile_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Phone No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Respondent_Phone_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Fax No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Respondent_Fax_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Email" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Respondent_Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Order Date" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center" DataField="OrderDate" />
                    <asp:TemplateField HeaderText="Order File" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlConnectedFile1" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Category" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "OrderCatname")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlaceHolderFive")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="grdOrders" runat="server" AutoGenerateColumns="false" DataKeyNames="TempOrderID"
                OnRowCommand="grdOrders_RowCommand" OnRowCreated="grdOrders_RowCreated" OnRowDataBound="grdOrders_RowDataBound"
                CssClass="mGrid" OnRowDeleting="grdOrders_RowDeleting" AllowPaging="True" PageSize="10"
                OnPageIndexChanging="grdCMSMenu_PageIndexChanging" AllowSorting="true" OnSorting="grdCMSMenu_Sorting">
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
                        ItemStyle-CssClass="Text-Center" />
                    <asp:TemplateField Visible="false">
                        <HeaderTemplate>
                            ID
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrderID" runat="server" Text='<%#Eval("TempOrderID")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" SortExpression="OrderTitle" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderTitle")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Description" SortExpression="OrderDescription"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "OrderDescription")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="OrderDate" HeaderText="Order Date"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" DataField="OrderDate" />
                    <%--  <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" SortExpression="OrderDate" />--%>
                    <asp:TemplateField SortExpression="PRONO" HeaderText="Petition/RA No" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPetition" runat="server" Text='<%#Eval("PRONO")%>'>
                            </asp:Label>
                            <asp:Label ID="lblReview" runat="server" Text='<%#Eval("Review")%>' Visible="false">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--            <asp:BoundField HeaderText="Review No" DataField="Review" SortExpression="Review" />--%>
                    <asp:TemplateField HeaderText="Order Category" SortExpression="OrderCatName" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderCategoryName" runat="server" Text='<%#Eval("OrderCategoryName")%>'>
                            </asp:Label>
                            <%--<%# DataBinder.Eval(Container.DataItem, "OrderCategoryName")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="Order_Edit.aspx?orderID=<%#DataBinder.Eval(Container.DataItem,"TempOrderID")%> &ModuleID= <%=Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Orders) %>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&Connection=<%#DataBinder.Eval(Container.DataItem,"ConnectionID")%>">
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="../images/pencil.png" />
                            </a>
                            <asp:Image ID="imgnotedit" runat="server" Visible="false" ToolTip="pending" Height="15"
                                Width="15" ImageUrl="../images/th_star.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="delete" CommandArgument='<%# Eval("TempOrderID")+ ";" + Eval("Status_id")%>'
                                Text="Delete" ToolTip="Delete" />
                            <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status_Id")%>'
                                Visible="false">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Repeal
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkrpld" runat="server" Text="Repealed" CommandName="Repealed"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TempOrderID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Type
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblconnection" runat="server" Text='<%#Eval("ConnectionName") %>'
                                Visible="false"></asp:Label>
                            <asp:Label ID="lblconnectionName" runat="server"></asp:Label>
                            <%--       <%# DataBinder.Eval(Container.DataItem, "ConnectionName")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Restore
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("TempOrderID") %>'
                                Text="Restore" ToolTip="Restore" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Remarks" DataField="PlaceHolderFive" />--%>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlaceHolderFive")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("TempOrderID") %>'
                                Text="Audit" ToolTip="Audit" />
                            <%--  <asp:Button ID="btnAudit" runat="server" CommandName="Audit" CommandArgument='<%# Eval("Temp_Petition_Id") %>'
                                Text="View" ToolTip="Audit View" />--%>
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
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <!---------------- END------------------------------>
    </p>
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
</asp:Content>
