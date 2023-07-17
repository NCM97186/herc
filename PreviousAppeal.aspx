<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="PreviousAppeal.aspx.cs" Inherits="PreviousAppeal" %>

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
                        <%=Resources.HercResource.PreviousYearPetitionsAppeal%></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                            <%--<a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">--%>
                            <a href="<%=UrlPrint%>?format=Print" target="_blank" title="Print">
                                <img src="<%=ResolveUrl("~/images/print-icon.png")%>" width="18" height="16" alt="Print"
                                    title="Print" /></a>
                            <asp:ImageButton ID="imgPdf" runat="server" Width="18px" Height="16px" ImageUrl="~/images/pdf.png"
                                OnClick="imgPdf_Click" OnClientClick="aspnetForm.target ='_blank';" />
                        </div>
                        <div id="DlastUpdate" runat="server" class="last-updated">
                            <b>
                                <%=Resources.HercResource.LastUpdated %>:</b><%=lastUpdatedDate %>
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
                <div id="DRti" runat="server" class="RTI_link2">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lnkreview" runat="server" OnClick="lnkreview_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnkAppeal" runat="server" OnClick="lnkAppeal_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <br />
                <br />
                <p id="pyear" runat="server" visible="false">
                    Select for old Petition Appeal:
                    <asp:DropDownList ID="drpyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpyear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <br />
                <asp:GridView ID="grdAppeal" runat="server" AutoGenerateColumns="false" OnRowCommand="grdAppeal_RowCommand"
                    OnRowDataBound="grdAppeal_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="PRO No" ItemStyle-HorizontalAlign="Center" Visible='false'>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDetails" runat="server" Text='<%#Eval("pronumber") %>' CommandName="View"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PA_Id") %>' ToolTip="Click on Petition Number to view Petition details." summary="This table show all Pettion appeal records"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="RA No" DataField="RPNO" Visible='false' />
                        <asp:BoundField HeaderText="Date" DataField="AppealDate" />
                        <asp:BoundField HeaderText="Respondent(s)" DataField="Respondent_Name" Visible="false" />
                        <asp:TemplateField HeaderText="Petitioner(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblPetitioner" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Applicant_Name"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Respondent(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblRespondent" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Respondent"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Subject"),100) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                        <asp:TemplateField HeaderText="Where Appealed" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDetailswhere" runat="server" Text='<%#Eval("Where_Appealed") %>'
                                    CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PA_Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
          
                      
                        <asp:BoundField HeaderText="Status" DataField="PA_Status" />
                        <asp:TemplateField HeaderText="Download Judgement" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblViewDocAppeal" runat="server" CommandName="ViewDocAppeal"
                                    CommandArgument='<%#Eval("File_Name")%>'>
                                  
                                     <img src="images/pdf-icon.jpg" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFileAppeal" runat="server" Value='<%#Eval("File_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Miscelleneous_DL.FixCharacters(DataBinder.Eval(Container.DataItem,"Remarks"),50) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <div class="clear">
                </div>
              <br/>
                    <div style="float: right;" class="paging">
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
                    <li><a href="~/petition/1.aspx" id="previous" runat="server" class="current">
                        <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                    <li><a href='<%=ResolveUrl("~/PetitionSearch.aspx") %>'>
                        <%=Resources.HercResource.PetitionsSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/OnlineStatus.aspx") %>'>
                        <%=Resources.HercResource.OnlineStatus %></a></li>
                    <% }

                       else
                       { %>
                    <li><a href="~/content/Hindi/petition.aspx" id="currentHindi" runat="server">
                        <%=Resources.HercResource.CurrentYearPetitions %></a></li>
                    <li><a href="~/content/Hindi/petition/1.aspx" id="previousHindi" runat="server" class="current">
                        <%=Resources.HercResource.PreviousYearPetitions %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/PetitionSearch.aspx") %>'>
                        <%=Resources.HercResource.PetitionsSearch %></a></li>
                    <li><a href='<%=ResolveUrl("~/content/Hindi/OnlineStatus.aspx") %>'>
                        <%=Resources.HercResource.OnlineStatus %></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
