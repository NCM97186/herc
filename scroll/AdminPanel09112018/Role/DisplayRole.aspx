<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    CodeFile="DisplayRole.aspx.cs" Inherits="Auth_AdminPanel_Role_DisplayRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="LblMsg" runat="server" Font-Bold="True"></asp:Label>
        <p class="buttonAlign">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click" CssClass="button"
                ToolTip="Add New Role">Create Role</asp:LinkButton>
        </p>
        <p id="pnlDearptment" runat="server">
            <asp:Label ID="lblDepartmentName" runat="server">Select Department<span class="redtext">*</span>:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                InitialValue="0" ErrorMessage="Please Please select department." ValidationGroup="Role"></asp:RequiredFieldValidator>
        </p>
        <br />
        <br />
        <p>
            <asp:GridView ID="GrdDisplayRole" runat="server" DataKeyNames="Role_Id" AutoGenerateColumns="false"
                OnRowCommand="GrdDisplayRole_RowCommand" OnRowDeleting="GrdDisplayRole_RowDeleting"
                CssClass="mGrid" OnRowDataBound="GrdDisplayRole_RowDataBound" OnSorting="GrdDisplayRole_Sorting"
                AllowSorting="true">
                <EmptyDataTemplate>
                    Records not available.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            S.No.
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <HeaderTemplate>
                            Role ID
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LblRoleID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Role_Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center" SortExpression="Role_Name"
                        HeaderText="Role">
                        <ItemTemplate>
                        <asp:Label ID="lblRole" runat="server" Text=' <%#Eval("Role_Name")%>'></asp:Label>
                          <%--  <%#DataBinder.Eval(Container.DataItem, "Role_Name")%>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center" HeaderText=" Created On" ItemStyle-CssClass="Text-Center"
                        SortExpression="recordSortingDate">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Rec_Insert_Date")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Edit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="Add_Role.aspx?Role_id=<%#DataBinder.Eval(Container.DataItem,"Role_Id")%>&ModuleId=14">
                                <img src="../images/pencil.png" alt="Edit" title="Edit" />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="Text-Center" ItemStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Delete
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" ImageUrl="~/Auth/AdminPanel/images/cross.png" runat="server"
                                CommandName="delete" CommandArgument='<%# Eval("Role_Id") %>' Text="Delete" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="Text-Center" HeaderStyle-CssClass="Text-Center">
                        <HeaderTemplate>
                            Audit
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAudit" ImageUrl="~/Auth/AdminPanel/images/Audit.png" runat="server"
                                Height="15px" Width="15px" CommandName="Audit" CommandArgument='<%# Eval("Role_Id") %>'
                                Text="Audit" ToolTip="Audit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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
    </div>
</asp:Content>
