<%@ Control Language="C#" AutoEventWireup="true" CodeFile="accessibility.ascx.cs"
    Inherits="UserControl_accessibility" %>
<div class="accessibility-btn">
    <ul>
        <li><a href="#main_Navigation" title="<%=Resources.HercResource.SkiptoNavigation %>">
            <%=Resources.HercResource.SkiptoNavigation %></a></li>
        <li>
            <asp:HyperLink ID="hlScreeanReader" Text='<%=Resources.HercResource.ScreenReaderAccess %>'
                runat="server"><%=Resources.HercResource.ScreenReaderAccess%></asp:HyperLink></li>
        <li class="border"><a href="#MainDiv" title='<%=Resources.HercResource.SkipToMainContent %>' tabindex="1">
            <%=Resources.HercResource.SkipToMainContent%></a> </li>
    </ul>
</div>
<div class="accessibility-right-side">
    <div class="accessibility-icons">
        <ul>
            <li>
                <%--<a href="#">--%>
                <asp:ImageButton ID="ibDecreaseFont" runat="server" Width="16px" Height="12px" ImageUrl=""
                    OnClick="ibDecreaseFont_Click" />
            </li>
            <li>
                <asp:ImageButton ID="ibNormalFont" runat="server" Width="16px" Height="12px" ImageUrl=""
                    OnClick="ibNormalFont_Click" />
            </li>
            <li>
                <asp:ImageButton ID="ibIncreaseFont" runat="server" Width="16px" Height="12px" ImageUrl=""
                    OnClick="ibIncreaseFont_Click" />
            </li>
        </ul>
    </div>
    <div class="color-theme-icons">
        <ul>
            <li>
                <asp:ImageButton ID="ibHighContrast" runat="server" ImageUrl="~/images/high_contrast.png"
                    Width="16px" Height="12px" ToolTip="High Contrast" OnClick="ibHighContrast_Click" />
            </li>
            <li>
                <asp:ImageButton ID="ibStandardContrast" runat="server" Width="16px" Height="12px"
                    ToolTip="Normal Contrast" ImageUrl="" OnClick="ibStandardContrast_Click"/>
            </li>
            <li>
                <asp:ImageButton ID="ibBlue" OnClick="ibBlue_Click" runat="server" Width="16px" Height="12px"
                    ImageUrl="~/images/blue-theme.png" />
            </li>
            <li>
                <asp:ImageButton ID="ibOrange" OnClick="ibOrange_Click" runat="server" Width="16px"
                    Height="12px" ImageUrl="~/images/orange-theme.png" />
            </li>
            <li>
                <asp:ImageButton ID="ibGreen" OnClick="ibGreen_Click" runat="server" Width="16px"
                    Height="12px" ImageUrl="~/images/green-theme.png" />
            </li>
            <li>
                <%if (Convert.ToInt16(Resources.HercResource.Lang_Id) == (int)Module_ID_Enum.Language_ID.English)
                  { %>
                <a class="hindi" title='क्या आप इस वेबसाइट को  हिंदी में देखना चाहेंगे ?' href="<%=ResolveUrl("~/content/Hindi/index.aspx") %>" target="_blank" onclick="javascript:return confirm( 'क्या आप इस वेबसाइट को  हिंदी में देखना चाहेंगे ?');">
                   हिंदी में </a>
                <%} %>
                <%else
                    { %>
                <a class="hindi" title="Would you like to open this website in English in new window?" href="<%=ResolveUrl("~/index.aspx") %>" target="_blank" onclick="javascript:return confirm( 'Would you like to open this website in English in new window?');">
                    In English</a>
                <%} %>
            </li>
        </ul>
    </div>
    <div class="clear">
    </div>
</div>
