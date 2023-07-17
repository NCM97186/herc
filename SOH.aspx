<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="SOH.aspx.cs" Inherits="SOH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbreadcrumholder" runat="Server">
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                     
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <%=Resources.HercResource.LastUpdated %>:
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
                    Select for Previous Years Schedule of Hearings:</label>
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <br />
                <asp:GridView ID="gvSOH" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    DataKeyNames="Soh_ID" OnRowCommand="gvSOH_RowCommand" OnRowDataBound="gvSOH_RowDataBound"
                    Width="100%" ToolTip="Schedule Of Hearing" summary="This table shows all the records of Schedule of Hearing." >
                    <Columns>
                        <asp:BoundField HeaderText="Date & Time" DataField="Date" />
                        <asp:TemplateField HeaderText="PRO/RA Number" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblnumber" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField HeaderText=" " DataField="PRO_No"  />--%>
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Petitioner_Name"),100) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                        <asp:BoundField HeaderText="Petitioner(s)" DataField="Petitioner_Name" />
                        <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" />--%>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem, "Subject"),100)%>'
                                    CommandName="ViewDetails" CommandArgument='<%#Eval("Soh_ID")%>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarksSoh" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Venue" DataField="Venue" Visible="false" />
                        <asp:TemplateField HeaderText="Download" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("soh_file")%>'>
                                  
                                     <img src="<%=ResolveUrl("~/images/pdf-icon.jpg")%>" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <%-- <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("soh_file") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="clear">
                </div>
                <p>
                    <div style="float: right;" class="paging">
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                    Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPageSize" runat="server"></asp:Label>
                           <label for="<%=ddlPageSize.ClientID %>">
                            &nbsp;</label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
                    </div>
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
                        <%=Resources.HercResource.CLSH %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="SOH.aspx" title="Current Year" id="current" runat="server">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href="~/SOHPrevYear/1.aspx" title="Previous Year" id="previous" runat="server">
                        <%=Resources.HercResource.PreviousYears %>
                    </a></li>
                     <li><a href="~/SOHNextYear/2.aspx" title="Next Year" id="next" runat="server">
                        <%=Resources.HercResource.Nextyear%>
                    </a></li>
                    <li><a href='<%=ResolveUrl("~/SearchSOH.aspx")%>' title="Search">
                        <%=Resources.HercResource.Search%>
                    </a></li>
                    <% }
                       else
                       { %>
                    <li><a href="~/Content/Hindi/SOH.aspx" title="Current Year" id="currentHindi" runat="server">
                        <%=Resources.HercResource.CurrentYear %></a></li>
                    <li><a href="~/Content/Hindi/SOHPrevYear/1.aspx" title="Previous Year" id="previousHindi"
                        runat="server">
                        <%=Resources.HercResource.PreviousYears %>
                    </a></li>
                    <li><a href="~/Content/Hindi/SOHNextYear/2.aspx" title="Next Year" id="nextyearHindi" runat="server">
                        <%=Resources.HercResource.Nextyear%>
                    </a></li>
                    <li><a href='<%=ResolveUrl("~/Content/Hindi/SearchSOH.aspx")%>' title="Search">
                        <%=Resources.HercResource.Search%>
                    </a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
