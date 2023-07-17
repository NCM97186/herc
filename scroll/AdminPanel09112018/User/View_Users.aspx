<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="View_Users.aspx.cs" Inherits="Auth_AdminPanel_User_View_Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p class="buttonAlign">
        <asp:LinkButton ID="lnkAddUser" runat="server" CssClass="button" OnClick="lnkAddUser_Click"
            ToolTip="Add New User">Add New User</asp:LinkButton>
    </p>
    <p id="P2" runat="server">
        <label for="<%=ddlDeptname.ClientID %>">
            Select Department<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlDeptname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptname_SelectedIndexChanged">
            </asp:DropDownList>
        </label>
    </p>
    <p id="P3" runat="server">
        <label for="<%=ddlStatus.ClientID %>">
            Select Status<span class="redtext">* </span>:
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            </asp:DropDownList>
        </label>
    </p>
    <p>
        <asp:Label ID="LblMsg" runat="server" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GrdViewUsers" runat="server" DataKeyNames="User_Id" AutoGenerateColumns="false"
            OnRowCommand="GrdViewUsers_RowCommand" OnRowDataBound="GrdViewUsers_RowDataBound"
            OnSorting="GrdViewUsers_Sorting" AllowSorting="true" CssClass="mGrid" AllowPaging="True"
            PageSize="10" OnPageIndexChanging="GrdViewUsers_PageIndexChanging">
            <EmptyDataTemplate>
                Records not available.
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="S.No." HeaderStyle-CssClass="Text-Center"
                    ItemStyle-CssClass="Text-Center" />
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>
                        User ID
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblUser_Id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "User_Id")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" SortExpression="Username" HeaderText="User Name">
                    <ItemTemplate>
                        <asp:LinkButton ID="LnkDetails" runat="server" Text='<%#Eval("Username")%>' CommandName="View"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "User_Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center"
                    HeaderText="Created On" SortExpression="dateSorting">
                    <ItemTemplate>
                        <asp:Label ID="LblCreatedOn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Rec_Insert_Date")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Change Status
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LnkStatus" runat="server" CommandName="Status" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "User_Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Edit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <a href="Add_User.aspx?User_Id=<%#DataBinder.Eval(Container.DataItem,"User_Id")%>&ModuleId=15">
                            <img src="../images/pencil.png" alt="Edit" title="Edit" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="delete" CommandArgument='<%# Eval("User_Id")%>'
                                Text="Delete" ToolTip="Delete" /><%--OnClientClick="return confirm('Are You sure want to delete this petition?');"--%>
                        </ItemTemplate>
                    </asp:TemplateField>
               <%-- <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Delete
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                            CommandName="delete" CommandArgument='<%# Eval("User_Id") %>' Text="Delete" ToolTip="Delete" /><%-- OnClientClick="return confirm('Are You sure want to Delete?');"
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField Visible="false" ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Restore
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnRestore" ImageUrl="~/Auth/AdminPanel/images/restoreIconLarge.jpg"
                            runat="server" Height="15px" Width="15px" CommandName="Restore" CommandArgument='<%# Eval("User_Id") %>'
                            Text="Restore" ToolTip="Restore" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                    <HeaderTemplate>
                        Audit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                            Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("User_Id") %>'
                            Text="Audit" ToolTip="Audit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" FirstPageText="First"
                LastPageText="Last" />
            <PagerStyle CssClass="pgr" />
        </asp:GridView>
        <span style="float: right;"></span>
    </p>
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
