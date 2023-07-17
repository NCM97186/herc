<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRtiDetails.aspx.cs" Inherits="Ombudsman_ViewRtiDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../css/layout.css">
    <%--
    <script type="text/javascript" language="javascript">

        function closeIt() {
            close();
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="pfaa" runat="server">
            <asp:GridView ID="grdrtifAA" runat="server" Width="100%" AutoGenerateColumns="false"
                OnRowDataBound="grdrtifAA_RowDataBound" OnRowCommand="grdrtifAA_RowCommand" 
                OnSelectedIndexChanged="grdrtifAA_SelectedIndexChanged" GridLines="None" CssClass="more_details" Caption="RTI-FAA Details" ToolTip="RTI-FAA" summary="This table show all RTI-FAA records">
                <Columns>
                    <asp:BoundField HeaderText="Ref No" DataField="RTI_Id" />
                    <asp:BoundField HeaderText="Year" DataField="Year" Visible="false" />
                    <asp:BoundField HeaderText="Application Date" DataField="Application_Date" />
                    <asp:BoundField HeaderText="FAA(First Applet Authority)" DataField="FAA" />
                    <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                    <asp:BoundField HeaderText="Subject" DataField="Subject" />
                    <asp:BoundField HeaderText="Status" DataField="RTI_FAA_Status_Id" />
                    <asp:TemplateField HeaderText="Appeal To SAA">
                        <ItemTemplate>
                            <asp:HiddenField ID="hyd" runat="server" Value='<%#Eval("RTI_FAA_Id") %>' />
                            <asp:LinkButton ID="lnlbtn" runat="server" CommandArgument='<%#Eval("RTI_FAA_Id") %>'
                                CommandName="vdetail" Font-Bold="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <br />
        <div id="PSAA" runat="server">
            <asp:GridView ID="grdFAA_SAA_RTI" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None" CssClass="more_details" Caption="RTI-SAA Details" ToolTip="RTI-SAA" summary="This table show all RTI-SAA records">
                <Columns>
                    <asp:BoundField HeaderText="Ref No" DataField="RTI_FAA_Id" />
                    <asp:BoundField HeaderText="Application Date" DataField="Application_Date" />
                    <asp:BoundField HeaderText="SAA(Second Applet Authority)" DataField="SAA" />
                    <asp:BoundField HeaderText="Applicant(s)" DataField="Applicant_Name" />
                    <asp:BoundField HeaderText="Subject" DataField="Subject" />
                    <asp:BoundField HeaderText="Status" DataField="RTI_SAA_Status_Id" />
                </Columns>
            </asp:GridView>
            <%-- <asp:Button ID="btnback" CssClass="btn" runat="server" Text="Back" OnClick="btnback_Click" />--%>
        </div>
    </div>
    </form>
</body>
</html>
