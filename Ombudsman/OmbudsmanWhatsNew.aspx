<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="OmbudsmanWhatsNew.aspx.cs" Inherits="Ombudsman_OmbudsmanWhatsNew" Title="Untitled Page" %>

<%@ Register Src="~/usercontrol/OmbudsmanLeftMenuFor_InternalPagesUserControl.ascx"
    TagName="OmbudsmanLeftMenuFor_InternalPagesUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" runat="Server">
    <div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"> </asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphrightholder" runat="Server">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.WhatNew %>
                    </h2>
                    <div class="page-had-right-side">
                        <%-- <div id="PrintDiv" runat="server" class="print-icon">
                           
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                     <label for="<%=imgPdf.ClientID %>">
                                &nbsp;
                            </label>
                                    <asp:ImageButton ID="imgPdf" runat="server" Width="18px" Height="16px" ImageUrl="~/images/pdf.png"
                                OnClick="imgPdf_Click" OnClientClick="aspnetForm.target ='_blank';" AlternateText="pdf" />
                        </div>--%>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <%=Resources.HercResource.LastUpdated %>:
                            <%=lastUpdatedDate%>
                           </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
            <h3 id="hAward" runat="server"></h3>
                <%--<asp:Literal ID="ltrWhatsNew" runat="server"></asp:Literal>--%>
                <asp:Repeater ID="rptWhatsNew" runat="server" OnItemCommand="rptWhatsNew_ItemCommand"
                    OnItemDataBound="rptWhatsNew_ItemDataBound">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem,"AwardDate") %>
                                - Award pronounced against appeal number:
                                <asp:LinkButton ID="lnkLatestUpdate" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Appeal_Number")%>'
                                    CommandName="view" Text='<%# DataBinder.Eval(Container.DataItem, "AppealNumber")%>'>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                <p>
                </p>
                 <h3 id="hAppeal" runat="server"></h3>
                <asp:Repeater ID="rptAppealWhatsNew" runat="server" OnItemCommand="rptAppealWhatsNew_ItemCommand">
                    <ItemTemplate>
                        <ul><%--AppealNo--%>
                            <li>
                                <%#DataBinder.Eval(Container.DataItem, "AwardDate")%>
                                - Appeal number:
                                <asp:LinkButton ID="lnkLatestUpdate" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Appeal_Id")%>'
                                    CommandName="viewAppeal"><%# DataBinder.Eval(Container.DataItem, "Appeal_Number") + " of " + DataBinder.Eval(Container.DataItem, "Applicant_Name")%></asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clear">
                </div>
                <p id="pPaging" runat="server" Visible="false">
                    <div style="float: right;" class="paging">
                        <asp:Repeater ID="rptPager" runat="server" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                           <label for="<%=ddlPageSize.ClientID %>">&nbsp;
                        </label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
                    </div>
                </p>
                <br />
                <br />
                <br />
            </div>
        </div>
        <div class="clear">
        </div>
        <!--mid-holder-Close-->
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphleftholder" runat="Server">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2 id="hparentId" runat="server">
                        <%=Resources.HercResource.WhatNew %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <%--   <uc1:OmbudsmanLeftMenuFor_InternalPagesUserControl ID="LeftMenuFor_InternalPagesUserControl1"
                runat="server" />--%>
        </div>
    </div>
</asp:Content>
