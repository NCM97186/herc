<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="TariffDetails.aspx.cs" Inherits="TariffDetailsaspx" %>

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
                        <%=headname %><%--<%=Resources.HercResource.Details %>--%></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                    
                        </div>
                        <div id="DlastUpdate" runat="server"  class="last-updated">
                            <strong><%=Resources.HercResource.LastUpdated %>:</strong>
                            <%=lastUpdatedDate %>
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
                <asp:Literal ID="ltrlPdf" runat="server" Visible="false">             
                </asp:Literal>
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
                <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a  href="GenerationTariff.aspx" title="Generation Tariff" id="Gen"
                        runat="server">
                        <%=Resources.HercResource.GenerationTariff %></a></li>
                    <li><a href="TransmissionCharges.aspx" title="Transmission Charges" id="Trans"
                        runat="server">
                        <%=Resources.HercResource.TransmissionCharges %>
                    </a></li>
                    <li><a href="WheelingCharges.aspx" title="Wheeling Charges" id="Wheeling" runat="server">
                        <%=Resources.HercResource.WheelingCharges %>
                    </a></li>
                    <li><a href="Tariff.aspx" title="Distribution Charges" id="Dist" runat="server">
                        <%=Resources.HercResource.DistributionCharges %></a></li>
                    <li><a href="Crosssubsidy.aspx" title="Cross subsidy" id="cross" runat="server">
                        <%=Resources.HercResource.Cross %>
                    </a></li>
                    <li><a href="RenewalEnergy.aspx" title="Renewal Energy " id="renewal" runat="server">
                        <%=Resources.HercResource.Renewal %>
                    </a></li>
                     <% }

                       else
                       { %>
                        <li><a href="~/content/Hindi/GenerationTariff/371_1_GenerationTariff.aspx" title="Generation Tariff"
                        id="Gen_Hin" runat="server">
                        <%=Resources.HercResource.GenerationTariff %></a></li>
                    <li><a href="~/content/Hindi/TransmissionCharges/370_1_TransmissionCharges.aspx"
                        title="Transmission Charges" id="Trans_Hin" runat="server">
                        <%=Resources.HercResource.TransmissionCharges %>
                    </a></li>
                    <li><a href="~/content/Hindi/WheelingCharges/372_1_WheelingCharges.aspx" title="Wheeling Charges"
                        id="Wheeling_Hin" runat="server">
                        <%=Resources.HercResource.WheelingCharges %>
                    </a></li>
                    <li><a  href="~/content/Hindi/Tariff/369_1_DistributionCharges.aspx"
                        title=" Distribution Charges" id="Dist_Hin" runat="server">
                        <%=Resources.HercResource.DistributionCharges %></a></li>
                   <li><a  href="~/content/Hindi/Crosssubsidy/779_1_Crosssubsidy.aspx" title="Cross subsidy" id="Cross_Hin" runat="server">
                        <%=Resources.HercResource.Cross %>
                    </a></li>
                    <li><a  href="~/content/Hindi/RenewalEnergy/780_1_RenewalEnergy.aspx" title="Renewal Energy " id="renewal_Hin" runat="server">
                        <%=Resources.HercResource.Renewal %>
                    </a></li>
                       
                       <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
