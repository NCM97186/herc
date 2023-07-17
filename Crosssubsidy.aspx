<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="Crosssubsidy.aspx.cs" Inherits="Crosssubsidy_" Title="Untitled Page" %>

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
                        <%=Resources.HercResource.Cross %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                           
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <strong>
                                <%=Resources.HercResource.LastUpdated %>:</strong>
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
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <p id="pyear" runat="server" visible="false">
                    <label for="<%=drpyear.ClientID %>">
                        Select for Cross subsidy and Additional Surcharge:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <asp:Literal ID="lrtTariff" runat="server" Text='<%# Eval("Details") %>'>
                
                </asp:Literal>
                <div class="clear">
                </div>
             <br />
                    <p style="float: right;" class="paging">
                        <asp:Repeater ID="rptPager" runat="server">
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
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="~/GenerationTariff/362_1_GenerationTariff.aspx" title="Generation Tariff" id="A1" runat="server">
                        <%=Resources.HercResource.GenerationTariff %></a></li>
                    <li><a href="~/TransmissionCharges/361_1_TransmissionCharges.aspx" title="Transmission Charges" id="previous"
                        runat="server">
                        <%=Resources.HercResource.TransmissionCharges %>
                    </a></li>
                    <li><a href="~/WheelingCharges/363_1_WheelingCharges.aspx" title="Wheeling Charges" id="A2" runat="server">
                        <%=Resources.HercResource.WheelingCharges %>
                    </a></li>
                    <li><a href="~/Tariff/360_1_DistributionCharges.aspx" title=" Distribution Charges" id="current" runat="server">
                        <%=Resources.HercResource.DistributionCharges %></a></li>
                    <li><a class="current" href="~/Crosssubsidy/530_1_CrosssubsidyandAdditionalSurcharge.aspx" title="Cross subsidy" id="A4" runat="server">
                        <%=Resources.HercResource.Cross %>
                    </a></li>
                    <li><a href="~/RenewalEnergy/532_1_CrosssubsidyandAdditionalSurcharge.aspx" title="Renewal Energy " id="A3" runat="server">
                        <%=Resources.HercResource.Renewal %>
                    </a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/GenerationTariff/371_1_GenerationTariff.aspx" title="Generation Tariff"
                        id="A5" runat="server">
                        <%=Resources.HercResource.GenerationTariff %></a></li>
                    <li><a href="~/content/Hindi/TransmissionCharges/370_1_TransmissionCharges.aspx"
                        title="Transmission Charges" id="A6" runat="server">
                        <%=Resources.HercResource.TransmissionCharges %>
                    </a></li>
                    <li><a href="~/content/Hindi/WheelingCharges/372_1_WheelingCharges.aspx" title="Wheeling Charges"
                        id="A7" runat="server">
                        <%=Resources.HercResource.WheelingCharges %>
                    </a></li>
                    <li><a href="~/content/Hindi/Tariff/369_1_DistributionCharges.aspx" title=" Distribution Charges"
                        id="A8" runat="server">
                        <%=Resources.HercResource.DistributionCharges %></a></li>
                    <li><a class="current" href="~/content/Hindi/Crosssubsidy/806_1_Crosssubsidy.aspx"
                        title="Cross subsidy" id="A9" runat="server">
                        <%=Resources.HercResource.Cross %>
                    </a></li>
                    <li><a href="~/content/Hindi/RenewalEnergy/807_1_RenewalEnergy.aspx" title="Renewal Energy "
                        id="A10" runat="server">
                        <%=Resources.HercResource.Renewal %>
                    </a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
