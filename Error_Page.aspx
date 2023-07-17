<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" 
  CodeFile="Error_Page.aspx.cs" Inherits="Error_Page" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphleftholder" Runat="Server">
<div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.Error %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphrightholder" Runat="Server">
<div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        Error</h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            
                            <a href="#" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder" style="height:300px; text-align:center;">
           
                <asp:Literal ID="ltrlErrorMsg" runat="server">
                </asp:Literal>
            
            <div class="clear">
            </div>
        </div>
        </div>
        <div class="clear">
        </div>
       
        <!--mid-holder-Close-->
    </div>
    <div class="clear">
    </div>
</asp:Content>


