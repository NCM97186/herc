<%@ Page Title="" Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true" CodeFile="popup.aspx.cs" Inherits="popup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" Runat="Server">
</asp:Content>
<asp:Content ID="contentrightholder" ContentPlaceHolderID="cphrightholder" Runat="Server">

 <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                
                 
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
            <asp:Button ID="btnpop" runat="server" Text="pop" onclick="btnpop_Click" />
            
            </div>
        </div>
        <div class="clear">
        </div>
        <!--mid-holder-Close-->
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphfooterlink" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

