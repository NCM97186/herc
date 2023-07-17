<%@ Page Language="C#" MasterPageFile="~/Usermaster.master" AutoEventWireup="true"
    CodeFile="scheduleOfHearings.aspx.cs" Inherits="scheduleOfHearings" %>

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
                        <%=Resources.HercResource.Commissioncalendar %></h2>
                    <div class="page-had-right-side">
                        <div id="PrintDiv" runat="server" class="print-icon">
                        
                        </div>
                        <div class="last-updated">
                          
                        </div>
                    </div>
                </div>
                <div class="page-had-right">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="text-holder">
            <label for="<%=MonthSelect.ClientID %>">&nbsp;</label>
                <asp:DropDownList ID="MonthSelect" runat="server" >
                </asp:DropDownList>
                &nbsp;
                   <label for="<%=YearSelect.ClientID %>">&nbsp;</label>
                <asp:DropDownList ID="YearSelect" runat="server" >
                </asp:DropDownList>
                <asp:Button ID="btnCalender" runat="server" Text="Submit" OnClick="btnCalender_Click" />
                
                <asp:Calendar ID="Calendar1" summary="Commission Calendar"  runat="server" OnDayRender="Calendar1_DayRender"  OnVisibleMonthChanged ="MonthChange"
                    CellPadding="0" DayNameFormat="Short"
                     Width="100%" CssClass="firstDate"   
					  SelectionMode="None" ToolTip="Schedule of Hearing Calender" UseAccessibleHeader="true" >
                    <SelectedDayStyle CssClass="SelectedDayStyle" />
                    <TodayDayStyle CssClass="TodayDayStyle" />
                    <SelectorStyle CssClass="SelectorStyle"/>
                    <WeekendDayStyle CssClass="WeekendDayStyle"/>
                   
                    <NextPrevStyle CssClass="NextPrevStyle" />
                    
                    <DayHeaderStyle  CssClass="DayHeaderStyle"  />
                    <TitleStyle CssClass="TitleStyle"  />
                </asp:Calendar>
                <br />
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
                        <%=Resources.HercResource.Commissioncalendar%>
                    </h2>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
