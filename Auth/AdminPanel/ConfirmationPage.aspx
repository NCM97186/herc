<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ConfirmationPage.aspx.cs" Inherits="AdminPanel_ConfirmationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p align="center">
        <em><span style="font-size: 8pt; color: #459300; font-weight: bolder; font-family: Verdana">
            <asp:Label ID="lblmsg" Font-Size="Medium" runat="server"></asp:Label>
        </span></em>
    </p>
    <p align="center">
        <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click"
            OnClientClick="return getPass();" />
    </p>
</asp:Content>
