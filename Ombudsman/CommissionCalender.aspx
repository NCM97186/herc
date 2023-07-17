<%@ Page Language="C#" MasterPageFile="~/OmbudsmanMaster.master" AutoEventWireup="true"
    CodeFile="CommissionCalender.aspx.cs" Inherits="Ombudsman_CommissionCalender" %>

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
                        <%=Resources.HercResource.OmbudsmanCalendar %></h2>
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
               <%--  <asp:DropDownList ID="MonthSelect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="MonthSelect_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="YearSelect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="YearSelect_SelectedIndexChanged">
                </asp:DropDownList>--%>
                 <asp:DropDownList ID="MonthSelect" runat="server" >
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="YearSelect" runat="server" >
                </asp:DropDownList>
                <asp:Button ID="btnCalender" runat="server" Text="Submit" OnClick="btnCalender_Click" />

                <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" 
                  CellPadding="0" DayNameFormat="Short" 
                     OnVisibleMonthChanged ="MonthChange" Width="100%" 
					 SelectionMode="None">
                    <SelectedDayStyle CssClass="SelectedDayStyle" />
                    <TodayDayStyle CssClass="TodayDayStyle" />
                    <SelectorStyle CssClass="SelectorStyle" />
                    <WeekendDayStyle CssClass="WeekendDayStyle" />
                    
                    <NextPrevStyle CssClass="NextPrevStyle" />
                    <DayHeaderStyle CssClass="DayHeaderStyle" />
                    <TitleStyle CssClass="TitleStyle" />
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
                        <%=Resources.HercResource.OmbudsmanCalendar%>
                    </h2>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
