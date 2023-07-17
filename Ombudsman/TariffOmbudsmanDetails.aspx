<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="TariffOmbudsmanDetails.aspx.cs" Inherits="Ombudsman_TariffOmbudsmanDetails" %>

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
<asp:Content ID="contentrightholder" runat="server" ContentPlaceHolderID="cphrightholder">
    <div class="right-holder">
        <div class="working-content-holder">
            <div class="page-had">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2>
                        <%=Resources.HercResource.Details %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="javascript:void(0);" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div class="last-updated">
                            <b>Last Update:</b> <a href="javascript:void(0);">07-07-2010</a>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <asp:Label ID="lblyear" runat="server"></asp:Label>
                <asp:Literal ID="lrtTariff" runat="server" Text='<%# Eval("Details") %>'>             
                </asp:Literal>
                <p>
                    <asp:LinkButton ID="Lnkback" runat="server" Text="Back" OnClick="Lnkback_Click" CssClass="back"></asp:LinkButton></p>
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
                    <li><a href="~/Ombudsman/GenerationOmbudsman.aspx" title="Generation Tariff" id="Gen"
                        runat="server">
                        <%=Resources.HercResource.GenerationTariff %></a></li>
                    <li><a href="~/Ombudsman/TransmissionOmbudsman.aspx" title="Transmission Charges"
                        id="Trans" runat="server">
                        <%=Resources.HercResource.TransmissionCharges %>
                    </a></li>
                    <li><a href="~/Ombudsman/WheelingOmbudsman.aspx" title="Wheeling Charges" id="Wheeling"
                        runat="server">
                        <%=Resources.HercResource.WheelingCharges %>
                    </a></li>
                    <li><a href="~/Ombudsman/DistributionOmbudsman.aspx" title="Distribution Charges"
                        id="Dist" runat="server">
                        <%=Resources.HercResource.DistributionCharges %></a></li>
                    <li><a href="~/Ombudsman/CrosssubsidyOmbudsman.aspx" title="Cross subsidy" id="cross"
                        runat="server">
                        <%=Resources.HercResource.Cross %>
                    </a></li>
                    <li><a href="~/Ombudsman/RenewalEnergyOmbudsman.aspx" title="Renewal Energy " id="renewal"
                        runat="server">
                        <%=Resources.HercResource.Renewal %>
                    </a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
