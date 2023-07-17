<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="petition.aspx.cs" Inherits="petition" Title="" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="cphbanner" runat="Server">
</asp:Content>--%>
<asp:Content ID="contentbreadhum" runat="server" ContentPlaceHolderID="cphbreadcrumholder">
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
            <div class="page-had" id="mid" runat="server">
                <div class="page-had-left">
                </div>
                <div class="page-had-mid-side">
                    <h2 id="hPetition" runat="server">
                    </h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                          
                            <a href="<%=UrlPrint%>?format=Print" title="Print" target="_blank">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                                        
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
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
            <div class="text-holder" id="content" runat="server">
                <div id="DRti" runat="server" class="RTI_link2">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lnkreview" runat="server" OnClick="lnkreview_Click"></asp:LinkButton>
                        </li>
              
                    </ul>
                </div>
                <br />
                <br />
                <div class="overFlow">
                    <p id="pyear" runat="server" visible="false">
                      <label for="<%=drpyear.ClientID %>">
                        Select for Previous Years Petitions:</label>
                        <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </p>
                    <br />
                    <asp:GridView ID="gvPetition" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                        OnRowCommand="gvPetition_RowCommand" OnRowDataBound="gvPetition_RowDataBound"
                        DataKeyNames="Petition_id" Width="100%" ToolTip="Petitions" summary="This table show all petitions">
                        <Columns>
                            <asp:TemplateField HeaderText=" Number" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetails" runat="server" Text='<%#Eval("PRO_No") %>' CommandName="View"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id") %>'  ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:BoundField HeaderText="Date" DataField="Petition_Date" ItemStyle-HorizontalAlign="Center" />
                            
                            <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" Visible="false" />
                            <asp:TemplateField HeaderText="Petitioner(s)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Petitioner_Name"),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Respondent(s)">
                                <ItemTemplate>
                                    <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent_Name"),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>' />
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
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Remarks"),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                </div>
                <br />
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
                          <label for="<%=ddlPageSize.ClientID %>">
                            &nbsp;</label>
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
                        <%=Resources.HercResource.Petitions %>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="left-nav-btn">
                <ul>
                    <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="petition.aspx" id="current" runat="server">
                        <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                    <li><a href="~/petition/1.aspx" id="previous" runat="server" >
                        <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                    <li><a href='<%=ResolveUrl("~/PetitionSearch.aspx") %>' title='<%=Resources.HercResource.PetitionsSearch %>'>
                        <%=Resources.HercResource.PetitionsSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/OnlineStatus.aspx") %>' title='<%=Resources.HercResource.OnlineStatus %>'>
                        <%=Resources.HercResource.OnlineStatus %></a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/petition.aspx" id="currentHindi" runat="server">
                        <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                    <li><a href="~/content/Hindi/petition/1.aspx" id="previousHindi" runat="server">
                        <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/PetitionSearch.aspx") %>' title='<%=Resources.HercResource.PetitionsSearch %>'>
                        <%=Resources.HercResource.PetitionsSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/OnlineStatus.aspx") %>' title='<%=Resources.HercResource.OnlineStatus %>'>
                        <%=Resources.HercResource.OnlineStatus %></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
