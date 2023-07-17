<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="AnnualReport.aspx.cs" Inherits="AnnualReport" %>

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
                        <%=headerName %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" title="Print" target="_blank">
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
                <p id="pyear" runat="server" visible="false">
                <label for="<%=drpyear.ClientID %>" >
                    Select for old Annual Reports:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <br />
                <asp:GridView ID="gvAnnualReport" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCommand="gvAnnualReport_RowCommand" OnRowDataBound="gvAnnualReport_RowDataBound"
                    Width="100%" ToolTip="Annual Reports" summary="This table show all the annual reports">
                    <Columns>
                        <asp:BoundField HeaderText="S.No." DataField="RowNumber" ItemStyle-CssClass="Text-Center" />
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem,"Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Start_Date" DataField="Start_Date" Visible="false" />
                        <asp:TemplateField HeaderText="Download" ItemStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedFile" runat="server" Text='<%#Eval("File_Name") %>'></asp:Literal>
                                <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module_Id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("Link_Id") %>' Visible="false"></asp:Label>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <div class="clear">
                </div>
                <br />
                    <div style="float: right;" class="paging">
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                        <label for="<%=ddlPageSize.ClientID %>"></label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
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
                        <%=Resources.HercResource.AnnualReports %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="~/AnnualReport.aspx" title="Current Year" id="current" runat="server">
                        <%=Resources.HercResource.CurrentAnnualReport %></a></li>
                    <li><a href="~/AnnualReport/1.aspx" title="Previous Year" id="previous" runat="server">
                        <%=Resources.HercResource.OldAnnualReport %>
                    </a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/Content/Hindi/AnnualReport.aspx" title="Current Year" id="currentHindi"
                        runat="server">
                        <%=Resources.HercResource.CurrentAnnualReport%></a></li>
                    <li><a href="~/Content/Hindi/AnnualReport/1.aspx" title="Previous Year" id="previousHindi"
                        runat="server">
                        <%=Resources.HercResource.OldAnnualReport%>
                    </a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
