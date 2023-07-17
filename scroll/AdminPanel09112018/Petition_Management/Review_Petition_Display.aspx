<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Review_Petition_Display.aspx.cs" Inherits="Auth_AdminPanel_Petition_Management_Review_Petition_Display" %>

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
    </style>
    <script type="text/javascript">

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdReview.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdReview.ClientID %>');
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
            <asp:Button ID="btnAddreviewPetition" runat="server" CssClass="button" Text="Add New Review Petition"
                ToolTip="Add New Review Petition" OnClick="btnAddreviewPetition_Click" />
            <asp:Button ID="btnPetition_View" runat="server" CssClass="button" Text="Petition"
                OnClick="btnPetition_View_Click" ToolTip="View Petition" />
            <%-- <asp:Button ID="btnAppealAdd" runat="server" CssClass="button" Text="Add Appeal"
            ToolTip="Add Appeal" OnClick="btnAppealAdd_Click" />--%>
            <asp:Button ID="btnPetition_Appeal" runat="server" CssClass="button" Text="Appeal against Order"
                OnClick="btnPetition_Appeal_Click" ToolTip="View Appeal" />
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
                Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <br />
        <br />
        <p>
            <asp:GridView ID="grdOrderPdf" runat="server" Visible="false" AutoGenerateColumns="false"
                OnRowDataBound="grdOrderPdf_RowDataBound" DataKeyNames="Temp_RP_Id">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="RP No" SortExpression="rpno1" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lnk_PRO_Number" runat="server" Text='<%#Eval("PRO_No")%>' CommandName="View"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_Petition_Id") %>'></asp:LinkButton>--%>
                            <asp:Label ID="lblPro" runat="server" Text='<%#Eval("RP_No")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Review Petition Date" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ReviewpetitionDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Name" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem, "Applicant_Name"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Address" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem, "Petitioner_Address"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Mobile No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Applicant_Mobile_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Phone No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Applicant_Phone_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Fax No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Applicant_Fax_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petitioner Email" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Applicant_Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Name" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem, "Respondent"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Address" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem, "Respondent_Address"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Mobile No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Respondent_Mobile_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Phone No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Respondent_Phone_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Fax No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Respondent_Fax_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respondent Email" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Respondent_Email")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Review Petition File" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlrEVIEW" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="Status"  HeaderStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Petition_Status")%>
                        <asp:HiddenField ID="hidPetitionStatusId" runat="server" Value='<%#Eval("Petition_Status_ID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlaceholderTwo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="grdReview" runat="server" AutoGenerateColumns="false" DataKeyNames="Temp_RP_Id"
                OnRowCommand="grdReview_RowCommand" OnPageIndexChanging="grdReview_PageIndexChanging"
                OnRowCreated="grdReview_RowCreated" OnRowDataBound="grdReview_RowDataBound" CssClass="mGrid"
                OnRowDeleting="grdReview_RowDeleting" AllowPaging="True" PageSize="10" AllowSorting="true"
                OnSorting="grdReview_Sorting">
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" FirstPageText="First"
                    LastPageText="Last" />
                <PagerStyle CssClass="pgr" />
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
                            Id
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRP_ID" runat="server" Text='<%#Eval("Temp_RP_Id")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RA No" SortExpression="rpno1" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lnk_RP_Number" runat="server" Text='<%#Eval("RP_No")%>' CommandName="View"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RP_Id") %>'></asp:LinkButton>--%>
                            <asp:Label ID="lblProNumber" runat="server" Text='<%#Eval("RP_No")%>'></asp:Label>
                            <%--<%# DataBinder.Eval(Container.DataItem, "RP_No")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRO No" SortExpression="PRO_No" HeaderStyle-CssClass="Text-Center"
                        Visible="false">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PRO_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" SortExpression="ReviewpetitionDateSort" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Reviewpetition_Date")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="Text-Center" SortExpression="Subject">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")),100) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="RP_Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "RP_Status")%>
                            <asp:HiddenField ID="hidRpStatus" runat="server" Value='<%#Eval("RP_Status_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                        Visible="false">
                        <HeaderTemplate>
                            Appeal
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--  OnClientClick="return confirm('Are you sure you want to appeal petition?');"--%>
                            <asp:LinkButton ID="lnkAppeal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Appeal")%>'
                                CommandName="Appeal" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RP_Id")+ ";" +DataBinder.Eval(Container.DataItem, "Temp_RP_Id")%>'></asp:LinkButton>
                            <asp:Label ID="lblAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Appeal")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="PetitionReview_Insert_Update.aspx?rp_id=<%#DataBinder.Eval(Container.DataItem,"Temp_RP_Id")%> &ModuleID= <%=Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Petition) %>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>">
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="../images/pencil.png" />
                            </a>
                            <asp:Image ID="imgnotedit" runat="server" Visible="false" ToolTip="pending" Height="15"
                                Width="15" ImageUrl="../images/th_star.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="Delete" CommandArgument='<%# Eval("Temp_RP_Id")+" ; "+  Eval("Status_id") %>'
                                Text="Delete" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Restore
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("Temp_RP_Id") %>'
                                Text="Restore" ToolTip="Restore" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Change Status
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkChangeStatus" runat="server" Text='Change Status' OnClientClick="return confirm('Are you sure you want to change review petition status?');"
                                CommandName="ChangeStatus" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RP_Id")%>'></asp:LinkButton>
                            <asp:Label ID="lblchangestatus" runat="server" Visible='false' Text='Change Status'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("Temp_RP_Id") %>'
                                Text="Audit" ToolTip="Audit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
                    <asp:Label ID="lblDeleteMsg" Text="birendra kumar" runat="server"></asp:Label>
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
    <asp:Panel ID="pnlPopupChangeStatus" runat="server" CssClass="ChangeStatus">
        <asp:UpdatePanel ID="pnlPopupChangeStatus1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpdateStatus" />
                <asp:AsyncPostBackTrigger ControlID="ddlPetitionStatusUpdate" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <asp:DropDownList ID="ddlPetitionStatusUpdate" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPetitionStatusUpdate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherStatus" runat="server" Visible="false"></asp:TextBox>
                        </td>
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
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnShowPopup1" runat="server" Style="display: none" />
    <cc1:ModalPopupExtender ID="mpuUpdateStatus" runat="server" TargetControlID="btnShowPopup1"
        PopupControlID="pnlPopupChangeStatus" CancelControlID="btnCancelUpdate" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <!---------------- END------------------------------>
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
