<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true" CodeFile="DistributionOmbudsman.aspx.cs" 
Inherits="Ombudsman_DistributionOmbudsman"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" Runat="Server">
<div id="BreadcrumDiv" runat="server" class="breadcrum">
        <div class="breadcrumb-left-holder">
            <ul>
                <asp:Literal ID="ltrlBreadcrum" runat="server"> </asp:Literal>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="contentrightholder" runat="server" ContentPlaceHolderID="cphrightholder">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.DistributionCharges %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="javascript:void(0);" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div class="last-updated">
                            <b>Last Update:</b> <%=lastUpdatedDate %>
                        </div>
                        <%--<asp:ImageButton ID="imgpdf" runat="server" ImageUrl="~/images/pdf-icon.jpg" OnClick="imgpdf_Click" />--%>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <%-- <p id="pyear" runat="server" visible="false">
                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tbody>
                            <tr>
                                <th colspan="12" class="align">
                                    <span>Year</span>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <asp:DataList ID="datalistYear" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="6"
                        RepeatDirection="Horizontal" Width="730px" OnItemCommand="datalistYear_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkYear" runat="server" Text='<%#bind("year") %>' CommandArgument='<%#bind("year") %>'
                                CommandName="View"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </p>--%>
                <asp:Literal ID="lrtTariff" runat="server" Text='<%# Eval("Details") %>'>              
                </asp:Literal>
                <%--<asp:GridView ID="gvTariff" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lnkDes" runat="server" Text='<%#Eval("Details") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
                <div class="clear">
                </div>
                <p>
                    <span style="float: right;">
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
                    </span>
                </p>
            </div>
        </div>
        <div class="clear">
        </div>
        <!--mid-holder-Close-->
    </div>
    <div class="clear">
    </div>
</asp:Content>

<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.Tariff %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                     <li><a  href="~/Ombudsman/GenerationOmbudsman.aspx" title="Generation Tariff" id="A1"
                        runat="server">
                        <%=Resources.HercResource.GenerationTariff %></a></li>
                    <li><a  href="~/Ombudsman/TransmissionOmbudsman.aspx" title="Transmission Charges" id="previous"
                        runat="server">
                        <%=Resources.HercResource.TransmissionCharges %>
                    </a></li>
                    <li><a href="~/Ombudsman/WheelingOmbudsman.aspx" title="Wheeling Charges" id="A2" runat="server">
                        <%=Resources.HercResource.WheelingCharges %>
                    </a></li>
                    <li><a  class="current" href="~/Ombudsman/DistributionOmbudsman.aspx" title=" Distribution Charges" id="current" runat="server">
                        <%=Resources.HercResource.DistributionCharges %></a></li>
                    <li><a  href="~/Ombudsman/CrosssubsidyOmbudsman.aspx" title="Cross subsidy" id="A4" runat="server">
                        <%=Resources.HercResource.Cross %>
                    </a></li>
                    <li><a href="~/Ombudsman/RenewalEnergyOmbudsman.aspx" title="Renewal Energy " id="A3" runat="server">
                        <%=Resources.HercResource.Renewal %>
                    </a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
