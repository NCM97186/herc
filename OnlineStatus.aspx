<%@ Page Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true"
    CodeFile="OnlineStatus.aspx.cs" Inherits="OnlineStatus" Title="Untitled Page" %>

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
                  
                       <% if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
                       { %>
                    <li><a href="petition.aspx" id="current" runat="server" >
                        <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                    <li><a href="~/petition/1.aspx" id="previous" runat="server">
                        <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                    <li><a href='<%=ResolveUrl("~/PetitionSearch.aspx") %>' title=' <%=Resources.HercResource.PetitionsSearch %>'>
                        <%=Resources.HercResource.PetitionsSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/OnlineStatus.aspx") %>' class="current" title=' <%=Resources.HercResource.OnlineStatus %>'>
                        <%=Resources.HercResource.OnlineStatus %></a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/petition.aspx" id="currentHindi" runat="server">
                        <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                    <li><a href="~/content/Hindi/petition/1.aspx" id="previousHindi" runat="server">
                        <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/PetitionSearch.aspx") %>' title=' <%=Resources.HercResource.PetitionsSearch %>'>
                        <%=Resources.HercResource.PetitionsSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/OnlineStatus.aspx") %>' class="current" title=' <%=Resources.HercResource.OnlineStatus %>'>
                        <%=Resources.HercResource.OnlineStatus %></a></li>
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
                        <%=Resources.HercResource.OnlineStatus %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">

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
            
                <div class="clear">
                </div>
                <br />
                <div>
                    <asp:GridView ID="gvOnlineStatus" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                        DataKeyNames="Petition_id" OnRowCommand="gvPetition_RowCommand" OnRowDataBound="gvPetition_RowDataBound" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Petition/RA Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetails" runat="server" Text='<%#Eval("PRO_No") %>' CommandName="View"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>

                                         <asp:LinkButton ID="lnkDetailsRP" runat="server" Text='<%#Eval("PRO_No") %>' CommandName="ViewRP"
                                    Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id") %>' ToolTip='<%#Resources.HercResource.ClickHereToViewDetails %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:BoundField HeaderText="Date" DataField="Petition_Date" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Petitioner(s)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPetitioner" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Petitioner_Name")),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Respondent(s)">
                                <ItemTemplate>
                                    <asp:Label ID="lblRespondent" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Respondent_Name")),100) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Miscelleneous_DL.FixCharacters(Server.HtmlDecode((string)DataBinder.Eval(Container.DataItem,"Subject")),100) %>' />
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
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Remarks"),50) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                       
                        </Columns>
                        <EmptyDataTemplate>
                           <%=Resources.HercResource.NoDataAvailable %>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
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
