<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="UserControl_Search" %>
<asp:HiddenField ID="cx" Value="013280925726808751639:i85g1b47nss" runat="server" />
<asp:HiddenField ID="cof" Value="FORID:9" runat="server" />
<asp:Panel ID="pnlSearch" runat="server" DefaultButton="ImgSearch">
<asp:TextBox ID="txtSearch" runat="server" onfocus="this.value=''" value="<%$Resources:HercResource,EnterYourKeywords%>"
     class="inputbox"></asp:TextBox>
   <%-- <asp:Button ID="ImgSearch" runat="server" Text="<%$Resources:HercResource,googleSearch%>" OnClick="ImgSearch_Click" CssClass="searchbutton" CausesValidation="false"></asp:Button>--%>
    <asp:ImageButton ID="ImgSearch" runat="server" Height="20" Width="36" OnClick="ImgSearch_Click" CausesValidation="false" CssClass="ImgSearch_Click" />

</asp:Panel>