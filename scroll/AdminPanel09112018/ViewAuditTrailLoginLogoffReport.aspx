<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ViewAuditTrailLoginLogoffReport.aspx.cs" Inherits="Auth_AdminPanel_ViewAuditTrailLoginLogoffReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <p>
            <label for="<%= txtPetitionDate.ClientID %>">
                Select Date to Delete Records<span class="redtext">*</span>:
            <asp:TextBox ID="txtPetitionDate" CssClass="text-input small-input" runat="server"
                MaxLength="10"></asp:TextBox>
            <asp:ImageButton ID="ibPetitionDate" runat="server" ImageUrl="~/Auth/AdminPanel/images/cal.jpg"
                CausesValidation="false" />
                <asp:Button ID="btnDeleteDetails" runat="server" CssClass="button" OnClick="btnDeleteDetails_Click"
                    Text="Delete" ToolTip="Click To Delete" ValidationGroup="Add" />
                </label>
            <asp:RequiredFieldValidator ID="rfvStartDate" ValidationGroup="Add" SetFocusOnError="true"
                runat="server" ControlToValidate="txtPetitionDate" ErrorMessage="Please select date to delete records upto selected date."
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPetitionDate" runat="server" ControlToValidate="txtPetitionDate"
                ErrorMessage="Date format should be in dd/mm/yyyy." SetFocusOnError="True" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d"
                ValidationGroup="Add" Display="Dynamic">
            </asp:RegularExpressionValidator>
            
            <br />
            <em><span style="font-size: 8pt; font-weight: bold; color: #459300; font-family: Verdana">
                Tip:-dd/mm/yyyy</span></em>

                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate"
                PopupButtonID="ibPetitionDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPetitionDate">
            </cc1:CalendarExtender>
        </p>
    <asp:GridView ID="grdAuditReport" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
        AllowPaging="True" PageSize="10" OnPageIndexChanging="grdAuditReport_PageIndexChanging"
        AllowSorting="true" OnSorting="grdAuditReport_Sorting">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username " />
            <asp:BoundField DataField="IpAddress" HeaderText="IP Address" SortExpression="IpAddress " />
            <asp:BoundField DataField="action" HeaderText="Action" SortExpression="action" />
            <asp:BoundField DataField="login_time" HeaderText="Date/Time" SortExpression="login_time" />
        </Columns>
        <PagerStyle CssClass="pgr" />
        <RowStyle CssClass="drow" Wrap="True" />
    </asp:GridView>
    <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
</asp:Content>
