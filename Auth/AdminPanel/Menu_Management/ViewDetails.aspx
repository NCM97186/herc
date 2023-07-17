<%@ Page Title="" Language="C#" MasterPageFile="~/PreviewPage.master" AutoEventWireup="true"
    CodeFile="ViewDetails.aspx.cs" Inherits="Auth_AdminPanel_Menu_Management_ViewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphcontent" runat="Server">
    <%-- <asp:Literal ID="ltrlMainContent" runat="server">
    </asp:Literal>--%>
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="text-holder" id="content" runat="server">
                <asp:Literal ID="ltrlMainContent" runat="server">
                </asp:Literal>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <!--mid-holder-Close-->
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphfooterlink" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
