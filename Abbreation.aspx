<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true" CodeFile="Abbreation.aspx.cs" Inherits="Abbreation"%>

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
                        <%=Resources.HercResource.Abbreviations %></h2>
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
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <asp:GridView ID="gvAnnualReport" runat="server" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCommand="gvAnnualReport_RowCommand" OnRowDataBound="gvAnnualReport_RowDataBound"
                    >
                    <Columns>
                        <asp:BoundField HeaderText="SoNo" DataField="RowNumber" />
                        
                       <%-- <asp:BoundField HeaderText="Title" DataField="Name" />--%>
                        <asp:TemplateField  HeaderText="Title">
                        <ItemTemplate>
                       <asp:LinkButton ID="lnkTitle" runat="server" Text='<%#Eval("Name") %>' CommandName="View" CommandArgument='<%#Eval("Link_Id") %>'></asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Description" DataField="SmallDesc" />
                       <asp:BoundField HeaderText="Start_Date" DataField="Start_Date" />
                        <asp:TemplateField HeaderText="Download File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_ViewDoc" runat="server" CommandName="ViewDoc" CommandArgument='<%#Eval("File_Name")%>'>
                                  
                                     <img src="images/pdf_button.png" title="View Document" width="20" alt="View Document"
                                    height="20" />       
                                </asp:LinkButton>
                                <asp:HiddenField ID="hdfFile" runat="server" Value='<%#Eval("File_Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                     <%--   <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDetails" runat="server" Text="View Details" CommandName="View"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Petition_id") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                <br />
                          
                <p id="pyear" runat="server" visible="false">
                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tbody>
                            <tr>
                                <th colspan="12" class="align">
                                    <span>Year</span>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    
                    <asp:DataList ID="datalistYear" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="6"
                        RepeatDirection="Horizontal" Width="730px" OnItemCommand="datalistYear_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkYear" runat="server" Text='<%#bind("year") %>' CommandArgument='<%#bind("year") %>'
                                CommandName="View"></asp:LinkButton>
                            
                        </ItemTemplate>
                    </asp:DataList>
                </p>
                <div class="clear">
                </div>
                <p>
                    <span style="float: right;">
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
                    </span>
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
                        <%=Resources.HercResource.Abbreviations%>
                    </h2>
                </div>
            </div>
            <div class="clear">
            </div>
             <div class="left-nav-btn">
                <ul>
                    <%--<li><a href="Abbreation.aspx" title="Current Year" id="current" runat="server" ><%=Resources.HercResource.CurrentYear %></a></li>--%>
                   <%-- <li><a href="AnnualReport.aspx?prev=1" title="Previous Year" id="previous" runat="server"><%=Resources.HercResource.PreviousYears %>
                       </a></li>--%>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

