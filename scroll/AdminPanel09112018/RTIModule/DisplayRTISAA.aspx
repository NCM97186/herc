<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="DisplayRTISAA.aspx.cs" Inherits="Auth_AdminPanel_RTI_DisplayRTISAA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= fileUploadPdf.ClientID %>');
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
    <script type="text/javascript">

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdRTISAA.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdRTISAA.ClientID %>');
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
            <asp:Button ID="btnRti" runat="server" CssClass="button" Text="RTI" OnClick="btnRti_Click"
                ToolTip="View RTI" />
            <asp:Button ID="btnRtiFaa" runat="server" CssClass="button" Text="RTI FAA" OnClick="btnRtiFaa_Click"
                ToolTip="View RTI FAA" />
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
            <asp:GridView ID="grdRTISAAPdf" runat="server" Visible="false" AutoGenerateColumns="false"
                Width="100%" OnRowDataBound="grdRTISAAPdf_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No." SortExpression="RowNumber"
                        ItemStyle-CssClass="Text-Center" />
                    <asp:TemplateField HeaderText="RTI Ref No (HERC/RTI)" SortExpression="Ref_No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lnk_Reference_Number" runat="server" Text='<%#Eval("Ref_No")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RTI_SAA_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FAA Ref No (HERC/FAA)" SortExpression="FAAref" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblFAAReference1" runat="server" Text='<%#Eval("FAAref") %>'></asp:Label>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "SAA")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SAA Ref No (HERC/SAA)" SortExpression="SAA_RefNo"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAAReference1" runat="server" Text='<%#Eval("SAA_RefNo") %>'></asp:Label>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "SAA")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="Application_Date"
                        HeaderText="Application Date" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                        DataField="Application_Date" />
                    <%-- <asp:TemplateField HeaderText="Application Date" SortExpression="Application_Date" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Application_Date")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="SIC Ref No" SortExpression="SAA" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAAReference" runat="server" Text='<%#Eval("SAA") %>'></asp:Label>
                            <%-- <asp:HiddenField ID="hydyear" runat="server" Value='<%#Eval("Year") %>' />
                         <%# DataBinder.Eval(Container.DataItem, "SAA")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Name") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant Address" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantAddress" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Address") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant Mobile Number" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantMobNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Mobile_No") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant Phone Number" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantPhnNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Phone_No") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant Fax Number" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantFaxNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Fax_No") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant Email Id" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicantEmail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicant_Email") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="RTI_Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("RTI_Status") %>'></asp:Label>
                            <asp:Label ID="lblUrl" runat="server" Text='<%#Eval("PlaceholderFour") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("Filename") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PlaceholderFive") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:GridView ID="grdRTISAA" runat="server" AutoGenerateColumns="false" DataKeyNames="Temp_RTI_SAA_Id"
                OnRowCommand="grdRTISAA_RowCommand" OnRowCreated="grdRTISAA_RowCreated" OnRowDataBound="grdRTISAA_RowDataBound"
                CssClass="mGrid" OnRowDeleting="grdRTISAA_RowDeleting" AllowPaging="True" PageSize="10"
                OnPageIndexChanging="grdRTISAA_PageIndexChanging" AllowSorting="true" OnSorting="grdRTISAA_Sorting">
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
                            <asp:Label ID="lblRTISAA_ID" runat="server" Text='<%#Eval("Temp_RTI_SAA_Id")%>'></asp:Label>
                            <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RTI Ref No (HERC/RTI)" SortExpression="Ref_No" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lnk_Reference_Number" runat="server" Text='<%#Eval("Ref_No")%>' CommandName="View"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RTI_SAA_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FAA Ref No (HERC/FAA)" SortExpression="FAAref" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblFAAReference1" runat="server" Text='<%#Eval("FAAref") %>'></asp:Label>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "SAA")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SAA Ref No (HERC/SAA)" SortExpression="SAA_RefNo"
                        ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAAReference1" runat="server" Text='<%#Eval("SAA_RefNo") %>'></asp:Label>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "SAA")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" SortExpression="Application_Date"
                        HeaderText="Application Date" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                        DataField="Application_Date" />
                    <%-- <asp:TemplateField HeaderText="Application Date" SortExpression="Application_Date" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Application_Date")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="SIC Ref No" SortExpression="SAA" ItemStyle-CssClass="Text-Center"
                        HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSAAReference" runat="server" Text='<%#Eval("SAA") %>'></asp:Label>
                            <asp:HiddenField ID="hydyear" runat="server" Value='<%#Eval("Year") %>' />
                            <%-- <%# DataBinder.Eval(Container.DataItem, "SAA")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant(s)" SortExpression="Applicant_Name" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Applicant_Name")),100) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="RTI_Status" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("RTI_Status") %>'></asp:Label>
                            <asp:HiddenField ID="status" runat="server" Value='<%#Eval("RTI_SAA_Status_Id") %>' />
                            <asp:Label ID="lblUrl" runat="server" Text='<%#Eval("PlaceholderFour") %>'></asp:Label>
                            <%--   <%# DataBinder.Eval(Container.DataItem, "RTI_Status")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Change Status
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkChangeStatus" runat="server" Text='Change Status' CommandName="ChangeStatus"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RTI_SAA_Id")%>'></asp:LinkButton>
                            <asp:Label ID="lblchangestatus" runat="server" Visible='false' Text='Change Status'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center"
                        Visible="false">
                        <HeaderTemplate>
                            Appeal
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAppeal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Appeal")%>'
                                OnClientClick="return confirm('Are you sure you want to appeal rti?');" CommandName="Appeal"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Temp_RTI_SAA_Id")+ ";" +DataBinder.Eval(Container.DataItem, "Ref_No")%>'></asp:LinkButton>
                            <asp:Label ID="lblAppeal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Appeal")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"PlaceholderFive"),100) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="RTI_SAA_Edit.aspx?rti_saa_Id=<%#DataBinder.Eval(Container.DataItem,"Temp_RTI_SAA_Id")%> &ModuleID= <%=Convert.ToInt16( Module_ID_Enum.Project_Module_ID.RTI) %>&LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&DepttID=<%#DataBinder.Eval(Container.DataItem,"Deptt_Id")%>">
                                <asp:Image ID="imgedit" runat="server" ToolTip="Edit" ImageUrl="../images/pencil.png" />
                            </a>
                            <asp:Image ID="imgnotedit" runat="server" Visible="false" ToolTip="pending" Height="15"
                                Width="15" ImageUrl="../images/th_star.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="delete" CommandArgument='<%# Eval("Temp_RTI_SAA_Id")+ ";" + Eval("Status_id")%>'
                                Text="Delete" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Restore
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                                runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("Temp_RTI_SAA_Id") %>'
                                Text="Restore" ToolTip="Restore" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download">
                        <ItemTemplate>
                            <asp:Literal ID="ltrlFile" runat="server" Text='<%#Eval("FileName") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("Temp_RTI_SAA_Id") %>'
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
                ToolTip="Click To Disapprove" Style="height: 26px" />
        </p>
    </asp:Panel>
    <!---------------- MODAL POPUP CONTROLS------------->
    <cc1:ModalPopupExtender ID="mpuUpdateStatus" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlPopupChangeStatus" CancelControlID="btnCancelUpdate" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopupChangeStatus" runat="server" CssClass="ChangeStatus">
        <asp:UpdatePanel ID="upDatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpdateStatus" />
                <asp:AsyncPostBackTrigger ControlID="ddlRTIStatusUpdate" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <b>RTI SAA No:</b>
                        </td>
                        <td valign="top">
                            <asp:Label ID="lblRefNoSAA" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>Select:</b>
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="ddlRTIStatusUpdate" runat="server" OnSelectedIndexChanged="ddlRTIStatusUpdate_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>
                                <asp:Label ID="lblAuthority" Text="Authority Name:" runat="server" Visible="false"></asp:Label>
                            </b>
                        </td>
                        <td valign="top" id="tdother" runat="server">
                            <asp:TextBox ID="txtOther" runat="server" Visible="false" TextMode="MultiLine">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="txtOther" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid text. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="status" ControlToValidate="txtOther"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOther" ID="regAuthority"
                                ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="status"
                                ErrorMessage="Minimum 3 and maximum 2000 characters required.">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr id="trAnyOther" runat="server" visible="false">
                        <td valign="top">
                            <%--<b>Any Other: </b>--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnyOther" runat="server" TextMode="MultiLine">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                ControlToValidate="txtAnyOther" Display="Dynamic" ValidationGroup="status"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid text. No special characters are allowed except (space,'-_.:;#()/&)"
                                Display="Dynamic" SetFocusOnError="True" ValidationGroup="status" ControlToValidate="txtAnyOther"
                                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAnyOther"
                                ID="regAnyOther" ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="status"
                                ErrorMessage="Minimum 3 and maximum 2000 characters required.">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr id="trUpLoader" runat="server" visible="false">
                        <td valign="top">
                            <b>File:</b>
                        </td>
                        <td valign="top">
                            <asp:FileUpload ID="fileUploadPdf" runat="server" />
                            <asp:CustomValidator ID="CustvalidFileUplaod" runat="server" ClientValidationFunction="ValidateFileUpload"
                                OnServerValidate="CustvalidFileUplaod_ServerValidate" ControlToValidate="fileUploadPdf"
                                ValidationGroup="status" ErrorMessage="Please select only .pdf file." Display="Dynamic">
                            </asp:CustomValidator>
                            <b>
                                <asp:Label ID="lbluploader" runat="server"></asp:Label></b> <b>
                                    <asp:Label ID="lblOther" runat="server" Visible="false"></asp:Label></b>
                            <asp:LinkButton ID="lnkFileRemove" runat="server" OnClick="lnkFileRemove_Click" CssClass="delete_btn">Remove File</asp:LinkButton>
                        </td>
                    </tr>
                    <tr id="trUpLoaderUrl" runat="server" visible="false">
                        <td valign="top">
                            <b>Url:</b>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                            <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                Tip:-http://www.herc.gov.in </span></em>
                            <asp:RegularExpressionValidator ID="RevLinkUrl" runat="server" ControlToValidate="txtUrl"
                                ValidationGroup="status" Display="Dynamic" ErrorMessage="Please enter valid url"
                                ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr id="tr1" runat="server">
                        <td valign="top">
                            <b></b>
                        </td>
                        <td valign="top">
                            <div id="DivCommon" runat="server" visible="false">
                                <b>
                                    <asp:Label ID="lblMemo" Text="Memo No:" runat="server"> </asp:Label></b>
                                <asp:TextBox ID="txtMemo" runat="server" MaxLength="2000"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtMemo" Display="Dynamic" ValidationGroup="status"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="status"
                                    runat="server" ErrorMessage="Please enter valid text. No special characters are allowed except (space,'-_.:;#()/&)"
                                    Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtMemo" ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMemo" ID="regMemoNumber"
                                    ValidationExpression="^[\s\S]{3,2000}$" runat="server" ValidationGroup="status"
                                    ErrorMessage="Minimum 3 and maximum 2000 characters required.">
                                </asp:RegularExpressionValidator>
                                <br />
                                <br />
                                <b>
                                    <asp:Label ID="Label1" Text="Date:" runat="server"></asp:Label>
                                </b>
                                <asp:TextBox ID="txtDate" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:ImageButton ID="ibApplicationDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                                    CausesValidation="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDate" ValidationGroup="status"></asp:RequiredFieldValidator>
                                <em><span style="font-size: 8pt; color: #459300; font-weight: bold; font-family: Verdana">
                                    Tip:-dd/mm/yyyy</span></em>
                                <p>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                                        PopupButtonID="ibApplicationDate">
                                    </cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                    </cc1:CalendarExtender>
                                </p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnUpdateStatus" CommandName="ChangeStatus" CssClass="button" runat="server"
                                Text="Update" OnClick="btnUpdateStatus_Click" CausesValidation="true" ValidationGroup="status" />
                            <asp:Button ID="btnCancelUpdate" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancelUpdate_Click"
                                CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlRTIStatusUpdate" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
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
    <!---------------- END------------------------------>
</asp:Content>
