<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="SOH_Display.aspx.cs" Inherits="Auth_AdminPanel_SOH_SOH_Display" %>

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

        var TotalChkBxAppeal;
        var CounterAppeal;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdScheduleOfHearing.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;

            TotalChkBxAppeal = parseInt('<%= this.grdAppealSoh.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            CounterAppeal = 0;
        }

       
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdScheduleOfHearing.ClientID %>');
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
        function validatecheckboxAppeal() {
        
            if (CounterAppeal == 0) {
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

        function HeaderClickAppeal(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdAppealSoh.ClientID %>');
            var TargetChildControl = "chkSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            CounterAppeal = CheckBox.checked ? TotalChkBxAppeal : 0;
        }


        function ChildClickAppeal(CheckBox, HCheckBox) {
            //get target base & child control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter;
            if (CheckBox.checked && CounterAppeal < TotalChkBxAppeal)
                CounterAppeal++;
            else if (CounterAppeal > 0)
                CounterAppeal--;

            //Change state of the header CheckBox.
            if (CounterAppeal < TotalChkBxAppeal)
                HeaderCheckBox.checked = false;
            else if (CounterAppeal == TotalChkBxAppeal)
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
            <asp:Button ID="btnAddScheduleOfHearing" runat="server" CssClass="button" Text="New Schedule of Hearing"
                OnClick="btnAddScheduleOfHearing_Click" ToolTip="Add New Schedule of Hearing" />
        </p>
        <p id="PDepartment" runat="server" visible="false">
            <label for="<%= ddlDepartment.ClientID %>">
                Select Department<span class="redtext">* </span>:
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <asp:Panel id="Ppetitionappeal" runat="server" visible="True">
            <p>
                <label for="<%= ddlpetitionappeal.ClientID %>">
                    Select <span class="redtext">*</span>:
                    <asp:DropDownList ID="ddlpetitionappeal" runat="server" OnSelectedIndexChanged="ddlpetitionappeal_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="1">Petition</asp:ListItem>
                        <asp:ListItem Value="2">Review Petition</asp:ListItem>
                        <asp:ListItem Value="0">Others</asp:ListItem>
                    </asp:DropDownList>
                </label>
            </p>
            <p>
                <label for="<%=ddlYear.ClientID %>">
                    Year<span class="redtext">* </span>:
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
            </p>
        </asp:Panel>
        <asp:Panel id="PApeal" runat="server" visible="false">
            <p>
                <label for="<%= ddlappeal.ClientID %>">
                    Select <span class="redtext">* </span>:
                    <asp:DropDownList ID="ddlappeal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlappeal_SelectedIndexChanged">
                        <asp:ListItem Value="2"> Appeal</asp:ListItem>
                        <asp:ListItem Value="0">Others</asp:ListItem>
                    </asp:DropDownList>
                </label>
            </p>
            <p>
                <label for="<%=ddlappealYear.ClientID %>">
                    Year<span class="redtext">* </span>:
                    <asp:DropDownList ID="ddlappealYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlappealYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </label>
            </p>
        </asp:Panel>
        <p id="P3" runat="server">
            <label for="<%=ddlStatus.ClientID %>">
                Status <span class="redtext">* </span>:
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
        </p>
        <br />
        <br />
        <asp:GridView ID="grdScheduleOfHearingPdf" runat="server" AutoGenerateColumns="false"
            DataKeyNames="soh_id_Temp" OnRowDataBound="grdScheduleOfHearingPdf_RowDataBound"
            Visible="false" Width="100%">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderStyle-CssClass="Text-Center" HeaderText="S.No."
                    ItemStyle-CssClass="Text-Center" ItemStyle-HorizontalAlign="Center" SortExpression="RowNumber" />
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Date &amp; Time"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "NEWDATE")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="PRO/RP No">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PRO_No")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Subject" SortExpression="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectSoh" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Subject")%>'></asp:Label>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "soh_Subject")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Venue" SortExpression="Venue">
                    <ItemTemplate>
                        <%-- <asp:Label ID="lblVenue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Venue")%>'></asp:Label>--%>
                        <%# DataBinder.Eval(Container.DataItem, "Venue")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="HERC Authority Email ID">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "EmailId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="HERC Authority Mobile No">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MoblieNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Schedule Of Hearing Files">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFileSOHDetails" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="grdAppealPfd" runat="server" AutoGenerateColumns="false" DataKeyNames="soh_id_Temp"
            OnRowDataBound="grdAppealPfd_RowDataBound" Visible="false" Width="100%">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderStyle-CssClass="Text-Center" HeaderText="S.No."
                    ItemStyle-CssClass="Text-Center" ItemStyle-HorizontalAlign="Center" SortExpression="RowNumber" />
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Date &amp; Time"
                    ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "NEWDATE")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Appeal No">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Appeal_Number")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Subject" SortExpression="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectSoh" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Subject")%>'></asp:Label>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "soh_Subject")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Venue" SortExpression="Venue">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Venue")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Remakrs">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="HERC Authority Email ID">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "EmailId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="HERC Authority Mobile No">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MoblieNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Schedule Of Hearing Files">
                    <ItemTemplate>
                        <asp:Literal ID="ltrlConnectedFileSOHDetails" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
        <asp:GridView ID="grdScheduleOfHearing" runat="server" AllowPaging="True" AllowSorting="true"
            AutoGenerateColumns="false" CssClass="mGrid" DataKeyNames="soh_id_Temp" OnPageIndexChanging="grdScheduleOfHearing_PageIndexChanging"
            OnRowCommand="grdScheduleOfHearing_RowCommand" OnRowCreated="grdScheduleOfHearing_RowCreated"
            OnRowDataBound="grdScheduleOfHearing_RowDataBound" OnRowDeleting="grdScheduleOfHearing_RowDeleting"
            OnSorting="grdScheduleOfHearing_Sorting" PageSize="10">
            <PagerSettings FirstPageText="« First" LastPageText="Last »" Mode="NumericFirstLast"
                PageButtonCount="2" />
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
                <asp:BoundField DataField="RowNumber" HeaderStyle-CssClass="Text-Center" HeaderText="S.No."
                    ItemStyle-CssClass="Text-Center" ItemStyle-HorizontalAlign="Center" SortExpression="RowNumber" />
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>
                        ID
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblScheduleOfHearingID" runat="server" Text='<%#Eval("soh_id_Temp")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Date &amp; Time"
                    ItemStyle-CssClass="Text-Center" SortExpression="newDate1">
                    <ItemTemplate>
                    <asp:Label ID="lbldated" runat="server" Text='<%#Eval("NEWDATE")%>'></asp:Label>
                        <%--<%# DataBinder.Eval(Container.DataItem, "NEWDATE")%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" SortExpression="PRONo">
                    <ItemTemplate>
                    <asp:Label ID="lblpronumber" runat="server" Text='<%#Eval("PRO_No")%>'></asp:Label>
                        <%--<%# DataBinder.Eval(Container.DataItem, "PRO_No")%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Year" ItemStyle-CssClass="Text-Center"
                    SortExpression="Year" Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Year")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Petitioner(s)"
                    SortExpression="Petitioner_Name" Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Petitioner_Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Respondent(s)"
                    SortExpression="Respondent_Name" Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Respondent_Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Subject" SortExpression="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectSoh" runat="server" Text='<%#Server.HtmlDecode((string)Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Subject"),100))%>'></asp:Label>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#Server.HtmlDecode((string)Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "soh_Subject"),100))%>'></asp:Label>
                        <asp:HiddenField ID="hidden" runat="server" Value='<%#Eval("Subject") %>' />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("soh_Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Venue" SortExpression="Venue">
                    <ItemTemplate>
                        <asp:Label ID="lblVenue" runat="server" Text='<%#Server.HtmlDecode(Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Venue"),100))%>'></asp:Label>
                        <%--<%# DataBinder.Eval(Container.DataItem, "Venue")%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Edit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <a href='SOH_Add_Edit.aspx?sohid=<%#DataBinder.Eval(Container.DataItem,"soh_id_Temp")%> &amp;ModuleID= <%=Convert.ToInt16( Module_ID_Enum.Project_Module_ID.SOH) %>&amp;LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&amp;depttID=<%#DataBinder.Eval(Container.DataItem,"Deptt_Id")%>&amp;ID=<%#DataBinder.Eval(Container.DataItem,"Appeal_PetitionID")%> &amp;Status= <%#DataBinder.Eval(Container.DataItem,"Status_id")%>'>
                            <asp:Image ID="imgedit" runat="server" ImageUrl="../images/pencil.png" ToolTip="Edit" />
                        </a>
                        <asp:Image ID="imgnotedit" runat="server" Height="15" ImageUrl="../images/th_star.png"
                            ToolTip="pending" Visible="false" Width="15" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hydelete" runat="server" Value='<%#Eval("PRONo") %>' />
                        <asp:ImageButton ID="BtnDelete" runat="server" CommandArgument='<%# Eval("soh_id_Temp")+ ";" + Eval("Status_id")%>'
                            CommandName="delete" ImageUrl="~/Auth/AdminPanel/images/cross.png" Text="Delete"
                            ToolTip="Delete" />
                        <%--OnClientClick="return confirm('Are You sure want to delete this schedule of hearing?');"--%>
                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status_Id")%>'
                            Visible="false">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center"
                    Visible="false">
                    <HeaderTemplate>
                        Restore
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnRestore" runat="server" CommandArgument='<%# Eval("soh_id_Temp") %>'
                            CommandName="Restore" Height="15px" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                            Text="Restore" ToolTip="Restore" Width="15px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Audit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnAudit" runat="server" CommandArgument='<%# Eval("soh_id_Temp") %>'
                            CommandName="Audit" Height="15px" ImageUrl="~/Auth/AdminPanel/images/Audit.png"
                            Text="Audit" ToolTip="Audit" Width="15px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="« First" LastPageText="Last »" Mode="NumericFirstLast"
                PageButtonCount="2" />
            <PagerStyle CssClass="pgr" />
        </asp:GridView>
        <asp:GridView ID="grdAppealSoh" runat="server" AllowPaging="true" AllowSorting="true"
            AutoGenerateColumns="false" CssClass="mGrid" DataKeyNames="soh_id_Temp" OnPageIndexChanging="grdAppealSoh_PageIndexChanging"
            OnRowCommand="grdAppealSoh_RowCommand" OnRowCreated="grdAppealSoh_RowCreated"
            OnRowDataBound="grdAppealSoh_RowDataBound" OnRowDeleting="grdAppealSoh_RowDeleting"
            OnSorting="grdAppealSoh_Sorting" PageSize="10">
            <Columns>
                <asp:TemplateField HeaderText="CheckAll">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:HeaderClickAppeal(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RowNumber" HeaderStyle-CssClass="Text-Center" HeaderText="S.No."
                    ItemStyle-CssClass="Text-Center" SortExpression="RowNumber" />
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>
                        ID
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblScheduleOfHearingID" runat="server" Text='<%#Eval("soh_id_Temp")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Date &amp; Time"
                    ItemStyle-CssClass="Text-Center" SortExpression="newDate1">
                    <ItemTemplate>
                    <asp:Label ID="lbldated1" runat="server" Text='<%#Eval("NEWDATE")%>'></asp:Label>
                        <%--<%# DataBinder.Eval(Container.DataItem, "NEWDATE")%>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Appeal No" ItemStyle-CssClass="Text-Center"
                    SortExpression="AppealNumber">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Appeal_Number")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Year" ItemStyle-CssClass="Text-Center"
                    SortExpression="Year" Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Year")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Applicant(s)" SortExpression="Applicant_Name"
                    Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Applicant_Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Respondent(s)"
                    SortExpression="Respondent_Name" Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Respondent_Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Subject" SortExpression="soh_Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectSoh" runat="server" Text='<%#Server.HtmlDecode(Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Subject"),100))%>'></asp:Label>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#Server.HtmlDecode(Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "soh_Subject"),100))%>'></asp:Label>
                        <asp:HiddenField ID="hidden" runat="server" Value='<%#Eval("Subject") %>' />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("soh_Subject") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText="Venue" SortExpression="Venue">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Venue")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Edit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <a href='SOH_Add_Edit.aspx?sohid=<%#DataBinder.Eval(Container.DataItem,"soh_id_Temp")%> &amp;ModuleID= <%=Convert.ToInt16( Module_ID_Enum.Project_Module_ID.SOH) %>&amp;LangID=<%#DataBinder.Eval(Container.DataItem,"Lang_Id")%>&amp;DepttID=<%#DataBinder.Eval(Container.DataItem,"Deptt_ID")%>&amp;ID=<%#DataBinder.Eval(Container.DataItem,"Appeal_PetitionID")%>&amp;Status= <%#DataBinder.Eval(Container.DataItem,"Status_id")%>'>
                            <asp:Image ID="imgedit" runat="server" ImageUrl="../images/pencil.png" ToolTip="Edit" />
                        </a>
                        <asp:Image ID="imgnotedit" runat="server" Height="15" ImageUrl="../images/th_star.png"
                            ToolTip="pending" Visible="false" Width="15" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hydeleteAppeal" runat="server" Value='<%#Eval("AppealNumber") %>' />
                        <asp:ImageButton ID="BtnDelete" runat="server" CommandArgument='<%# Eval("soh_id_Temp")+ ";" + Eval("Status_id")%>'
                            CommandName="delete" ImageUrl="~/Auth/AdminPanel/images/cross.png" Text="Delete"
                            ToolTip="Delete" />
                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status_Id")%>'
                            Visible="false">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center"
                    Visible="false">
                    <HeaderTemplate>
                        Restore
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnRestore" runat="server" CommandArgument='<%# Eval("soh_id_Temp") %>'
                            CommandName="Restore" Height="15px" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                            Text="Restore" ToolTip="Restore" Width="15px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Audit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnAudit" runat="server" CommandArgument='<%# Eval("soh_id_Temp") %>'
                            CommandName="Audit" Height="15px" ImageUrl="~/Auth/AdminPanel/images/Audit.png"
                            Text="Audit" ToolTip="Audit" Width="15px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                PageButtonCount="2" />
            <PagerStyle CssClass="pgr" />
        </asp:GridView>
        <!---------------- MODAL POPUP CONTROLS------------->
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Style="display: none"
            Width="400px">
            <table cellpadding="0" cellspacing="0" style="border: Solid 3px #D55500; width: 100%;
                height: 100%" width="100%">
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblDeleteMsg" runat="server" Text="birendra kumar"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CssClass="button"
                            OnClick="btnDelete_Click" Text="Delete" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="btnCancel" PopupControlID="pnlpopup" TargetControlID="btnShowPopup">
        </cc1:ModalPopupExtender>
        <!---------------- END------------------------------>
        <asp:Button ID="btnForReview" runat="server" CssClass="button" OnClick="btnForReview_Click"
             Text="For review" ToolTip="Click To Send For Review" />
        &nbsp;<asp:Button ID="btnForApprove" runat="server" CssClass="button" OnClick="btnForApprove_Click"
             Text="For Publish" ToolTip="Click To Send For Publish" />
        &nbsp;<asp:Button ID="btnApprove" runat="server" CssClass="button" OnClick="btnApprove_Click"
             Text="Publish" ToolTip="Click To Publish" />
        &nbsp;<asp:Button ID="btnDisApprove" runat="server" CssClass="button" OnClick="btnDisApprove_Click"
             Text="Disapprove" ToolTip="Click To Disapprove" />
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
    <cc1:ModalPopupExtender ID="mdpAuditTrail" runat="server" TargetControlID="btnShowAuditPopup"
        PopupControlID="pnlAudit" CancelControlID="btnCan" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
</asp:Content>
