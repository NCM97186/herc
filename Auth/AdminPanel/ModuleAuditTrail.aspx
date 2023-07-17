<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ModuleAuditTrail.aspx.cs" Inherits="Auth_AdminPanel_ModuleAuditTrail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
 
    <asp:GridView ID="grdAuditReport" runat="server"  CssClass="mGrid" OnPageIndexChanging="grdAuditReport_PageIndexChanging"
        AllowPaging="True" PageSize="10" >
        <AlternatingRowStyle CssClass="alt" />
        
        <PagerStyle CssClass="pgr" />
        <RowStyle CssClass="drow" Wrap="True" />
    </asp:GridView>
    <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
</asp:Content>