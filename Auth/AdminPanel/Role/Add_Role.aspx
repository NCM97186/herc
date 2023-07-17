<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Add_Role.aspx.cs" Inherits="Auth_AdminPanel_Role_Add_Role" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
 
    </script>

    <div>
        <asp:Label ID="LblMsg" runat="server" Font-Bold="True"></asp:Label>
        <p>
            <asp:Label ID="LblName" runat="server">Role Name<span class="redtext">*</span>:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtName" runat="server" ControlToValidate="TxtName"
                ErrorMessage="Please enter role name." ValidationGroup="Role" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Please enter valid data. No special characters are allowed except (space,'-_.:;#()/&)"
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="Role" ControlToValidate="TxtName"
                ValidationExpression="([\u0900-\u097F]|[a-z]|[A-Z]|[0-9]|[,'():;/& ]|[#]|[-]|[\r\n]|[_\.])*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="TxtName" ID="RegularExpressionValidator4"
                ValidationExpression="^[\s\S]{5,20}$" runat="server" ValidationGroup="Role" ErrorMessage="Minimum 5 and maximum 20 characters required."></asp:RegularExpressionValidator>
        </p>
        <p id="pnlDearptment" runat="server">
            <asp:Label ID="lblDepartmentName" runat="server">Select Department<span class="redtext">*</span>:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlDepartment" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDepartmentName" runat="server" ControlToValidate="ddlDepartment"
                InitialValue="0" ErrorMessage="Please select department." ValidationGroup="Role"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:UpdatePanel ID="Updategrid" runat="server">
                <ContentTemplate>
                    <%--<asp:CheckBox ID="Chkall" runat="server" AutoPostBack="true" OnCheckedChanged="Chkall_CheckedChanged"
                        Text="CheckAll" TextAlign="Left" />--%>
                    <asp:GridView ID="GrdPermission" runat="server" DataKeyNames="Module_Id" AutoGenerateColumns="false"
                        CssClass="mGrid">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    Module ID
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblModuleID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Module_Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ChkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkheader_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkChild" runat="server" AutoPostBack="true" OnCheckedChanged="ChkChild_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    S.No
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Modules
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Module_Name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Draft
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDrft" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Review
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRvw" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Publish
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkpblsh" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Edit
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEdit" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Delete
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    Repeal
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRepealed" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Hindi
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkHindi" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    English
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEnglish" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </p>
        <p>
            <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" CssClass="button"
                ValidationGroup="Role" ToolTip="Click To Save" />
            &nbsp;
            <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click"
                CssClass="button" ValidationGroup="Role" ToolTip="Click To Update" />
            &nbsp;
            <asp:Button ID="BtnBack" runat="server" Text="Back" OnClick="BtnBack_Click" CssClass="button" ToolTip="Go Back" />
        </p>
    </div>
</asp:Content>
