<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="ViewAuditTrailReport.aspx.cs" Inherits="Auth_AdminPanel_ViewAuditTrailReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Audit Trail Report</h3>
    <asp:GridView ID="grdAuditReport" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
        AllowPaging="True" PageSize="10" OnPageIndexChanging="grdAuditReport_PageIndexChanging"
        AllowSorting="true" OnSorting="grdAuditReport_Sorting" 
        onrowdatabound="grdAuditReport_RowDataBound">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="modulename" HeaderText="ModuleName" />--%>
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" Visible="false"/>
            <asp:BoundField DataField="IpAddress" HeaderText="IP Address" SortExpression="IpAddress" Visible="false"/>
            <asp:BoundField DataField="ActionTaken" HeaderText="Action" SortExpression="ActionTaken" />
            <asp:BoundField DataField="UpdateDate" HeaderText="Action Date" SortExpression="UpdateDate" />
            <asp:TemplateField HeaderText="Old Value" SortExpression="OldValue" >
            <ItemTemplate>
            <asp:Label ID="lbloldval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OldValue") %>'  ></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="New Value" SortExpression="OldValue" >
            <ItemTemplate>
            <asp:Label ID="lblNewval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"NewValue") %>'  ></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
               <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
           
        </Columns>
          <PagerStyle CssClass="pgr" />
                <RowStyle CssClass="drow" Wrap="True" />
    </asp:GridView>
    <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
</asp:Content>
