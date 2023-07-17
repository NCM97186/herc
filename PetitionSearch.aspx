<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="PetitionSearch.aspx.cs" Inherits="PetitionSearch" Title="" %>

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
                        <%=Resources.HercResource.Petitions %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <ul>
                        <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                           { %>
                        <li><a href='<%=ResolveUrl("~/petition.aspx") %>' title='<%=Resources.HercResource.CurrentYearPetitions %>'>
                            <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                        <li><a href='<%=ResolveUrl("~/petition/1.aspx") %>' title='<%=Resources.HercResource.PreviousYearPetitions %>'>
                            <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                        <li><a href='<%=ResolveUrl("~/PetitionSearch.aspx") %>' class="current" title='<%=Resources.HercResource.PetitionsSearch %>'>
                            <%=Resources.HercResource.PetitionsSearch %></a></li>
                        <li><a href="OnlineStatus.aspx" title='<%=Resources.HercResource.OnlineStatus %>'>
                            <%=Resources.HercResource.OnlineStatus %></a></li>
                        <% }

                           else
                           { %>
                        <li><a href='<%=ResolveUrl("~/content/Hindi/petition.aspx") %>' title='<%=Resources.HercResource.CurrentYearPetitions %>'>
                            <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                        <li><a href='<%=ResolveUrl("~/content/Hindi/petition/1.aspx") %>' title='<%=Resources.HercResource.PreviousYearPetitions %>'>
                            <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                        <li><a href='<%=ResolveUrl("~/content/Hindi/PetitionSearch.aspx") %>' class="current"
                            title='<%=Resources.HercResource.PetitionsSearch %>'>
                            <%=Resources.HercResource.PetitionsSearch %></a></li>
                        <li><a href="OnlineStatus.aspx" title='<%=Resources.HercResource.OnlineStatus %>'>
                            <%=Resources.HercResource.OnlineStatus %></a></li>
                        <%} %>
                    </ul>
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
                        <%=Resources.HercResource.PetitionsSearch %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" title="Print" target="_blank">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                           
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <p class="search_form_Date">
                    <label for="<%=ddlconnectionYearWise.ClientID %>">
                        <span>Select :</span></label>
                    <asp:DropDownList ID="ddlconnectionYearWise" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlconnectionYearWise_SelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                        <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                    </asp:DropDownList>
                </p>
                <div class="search_form">
                    <div>
                        <label for="<%=drpyear.ClientID %>">
                            <span>Select Year:</span></label>
                        <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label for="<%=drppro.ClientID %>">
                            <span>Petition/RA Number:</span></label>
                        <asp:DropDownList ID="drppro" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="search_btn">
                        <asp:Button ID="btnsearchYearwise" runat="server" Text="GO" OnClick="btnsearchYearwise_click" />
                    </div>
                </div>
                <div class="optional_or">
                    <asp:Label ID="lbl" runat="server" Text="OR"></asp:Label>
                </div>
                <p class="search_form_Date">
                    <label for="<%=ddlConnectionType.ClientID %>">
                        <span>Select :</span></label>
                    <asp:DropDownList ID="ddlConnectionType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlConnectionType_SelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Petition</asp:ListItem>
                        <asp:ListItem Value="2"> Review Petition</asp:ListItem>
                    </asp:DropDownList>
                </p>
                <div class="search_form">
                    <div>
                        <label for="<%=txtname.ClientID %>">
                            <span>Petitioner Name: </span>
                        </label>
                        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%=txtrespodent.ClientID %>">
                            <span>Respondent Name:</span></label>
                        <asp:TextBox ID="txtrespodent" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%=ddlPetitionStatusUpdate.ClientID %>">
                            <span>Status:</span></label>
                        <asp:DropDownList ID="ddlPetitionStatusUpdate" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label for="<%=txtsubject.ClientID %>">
                            <span>Subject:</span></label>
                        <asp:TextBox ID="txtsubject" runat="server"></asp:TextBox>
                    </div>
                    <div class="search_btn">
                        <asp:Button ID="btnsearch" runat="server" Text="GO" OnClick="btnsearch_click" />
                    </div>
                </div>
                <div class="clear">
                </div>
                <br />
                <br />
                <asp:GridView ID="gvOnlineStatus" OnRowDataBound="gvOnlineStatus_RowDataBound" OnRowCommand="gvOnlineStatus_RowCommand"
                    runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true" DataKeyNames="Petition_id"
                    Width="100%">
                    <EmptyDataTemplate>
                        <%=Resources.HercResource.NoDataAvailable %>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Petition/RA Number">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblNumber" runat="server" Text='<%#Eval("PRO_No") %>'> </asp:Label>--%>
                                <asp:LinkButton ID="lnkDetails" runat="server" Text='<%#Eval("PRO_No") %>' CommandName="View"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                <asp:LinkButton ID="lnkDetailsRP" runat="server" Text='<%#Eval("PRO_No") %>' CommandName="ViewRP"
                                    Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id") %>'
                                    ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField HeaderText="Date" DataField="Petition_Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Petitioner_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" Visible="false">
                            <ItemTemplate>
                                <asp:Literal ID="ltrlConnectedFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("Petition_File") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Status" DataField="PetitionStatusName" />
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Judgement Link" Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="ImageButton1" runat="server" NavigateUrl='<%# Eval("Judgement_Link") %>'
                                    Text='<%#Eval("Judgement_Link") %>' Target="_blank" OnClientClick='<%# String.Format("javascript:return openTargetURL(\"{0}\")", Eval("Judgement_Link")) %>'
                                    AlternateText="Read online" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                    <label for="<%=ddlPageSize.ClientID %>">
                        &nbsp;</label>
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
