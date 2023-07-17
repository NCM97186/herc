<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="Standards.aspx.cs" Inherits="Standards" Title="Untitled Page" %>

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
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="cphleftholder">
    <div class="left-holder">
        <div class="left-nav-holder">
            <div class="left-nav-had">
                <div class="left-nav-mid">
                    <h2>
                        <%=Resources.HercResource.Standards%>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="~/Standards/1.aspx" id="herc" runat="server">
                        <%=Resources.HercResource.herc %></a></li>
                    <li><a href="~/content/198_3_Others.aspx" id="other" runat="server">
                        <%=Resources.HercResource.Others %></a></li>
                    <li><a href="~/Standards/8.aspx" id="repealed" runat="server">
                        <%=Resources.HercResource.RepealedStandard %></a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/Standards/1.aspx" id="hercHindi" runat="server">
                        <%=Resources.HercResource.herc %></a></li>
                    <li><a href="~/content/Hindi/211_3_Others.aspx" id="otherHindi" runat="server">
                        <%=Resources.HercResource.Others %></a></li>
                    <li><a href="~/content/Hindi/Standards/8.aspx" id="repealedHindi" runat="server">
                        <%=Resources.HercResource.RepealedStandard%></a></li>
                    <%} %>
                </ul>
            </div>
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
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                     
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <strong>
                                <%=Resources.HercResource.LastUpdated %>:</strong>
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
			   <asp:Label ID="lblmsg" runat="server" Visible="false" CssClass="redtext"></asp:Label>
                <div>
                </div>
                <br />
                <div>
                    <asp:GridView ID="gvRegulations" OnRowDataBound="gvRegulations_RowDataBound" Width="100%"
                        runat="server" OnRowCommand="gvRegulations_RowCommand" AutoGenerateColumns="false"
                        UseAccessibleHeader="true" DataKeyNames="Link_Id" CssClass="more_details" ToolTip="View Standards" summary="This table shows all records of Standards" >
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbllinkId" runat="server" Text='<%#Eval("link_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="Text-Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypFile" runat="server" Text='<%# Server.HtmlDecode((string)Eval("Name"))%>'
                                        NavigateUrl='<%# "~/"+"WriteReadData/Pdf/" + Eval("File_Name") %>' Target="_blank" ToolTip='<%#Resources.HercResource.ViewDocument %>'></asp:HyperLink>
                                    <asp:HiddenField ID="hidFile" runat="server" Value='<%# Eval("File_Name") %>' />
                                    <asp:Label ID="lblname" runat="server" Text='<%#Server.HtmlDecode((string)Eval("Name")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Regulation No." HeaderStyle-CssClass="Text-Center"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegNumber" runat="server" Text='<%#Eval("RegulationNo") %>'></asp:Label>
                                    <asp:HiddenField ID="hydRegNumber" runat="server" Value='<%#Eval("test") %>' />
                                    <asp:Label ID="lblambandment" runat="server" Text='<%#Eval("ambandment") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="Text-Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAmendmentid" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="Text-Center" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CommandName="view" CommandArgument='<%#Eval("File_Name")%>'
                                        runat="server"> <img src='<%=ResolveUrl("~/images/pdf-icon.jpg")%>' title="View Document" width="20" alt="View Document"
                                    height="20" />  </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Record is not available.
                        </EmptyDataTemplate>
                    </asp:GridView>
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
                           <label for="<%=ddlPageSize.ClientID %>">&nbsp;</label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="30" Value="30" />
                        </asp:DropDownList>
                
                </p>
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
