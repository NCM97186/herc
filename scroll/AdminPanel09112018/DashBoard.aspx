<%@ Page Language="C#" MasterPageFile="~/Auth/AdminPanel/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="AdminPanel_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="right" style="font-weight: bold;">
      
    </div>
    <div>
        <ul class="shortcut-buttons-set">
           
            <% if (Convert.ToInt32(Session["User_Id"]) == 6)
               { %>
            <li><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/Role/DisplayRole.aspx?ModuleID=")%><%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Role) %>'
                title="Role Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/AdminPanel/images/ser_prov_1.png")%>" alt="icon" /><br />
                    Role</span></a></li>
            <%} %>
            <% if (Convert.ToInt32(Session["User_Id"]) == 6)
               { %>
            <li><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/User/View_Users.aspx?ModuleID=")%><%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.User) %>'
                title="User Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/User-Management.png")%>" alt="icon" /><br />
                    User </span></a></li>
			 <%} %>
            <li id="Bannerli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=")%><%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Banner) %>'
                title="Banner Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/banner.png")%>" alt="icon" /><br />
                    Banner </span></a>
                Draft :<asp:Label ID="lblDraftBanner" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewBanner" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalBanner" runat="server" Text=""></asp:Label>
                    </li>
           
          
            <li id="Menuli" runat="server">
            <a class="shortcut-button" href="<%=ResolveUrl("~/Auth/AdminPanel/Menu_Management/View_CMS_Menu.aspx?ModuleID=1")%>"
                title="Content Management System"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/CMS.png")%>" alt="icon" /><br />
                    CMS </span></a>
                Draft :<asp:Label ID="lblDraftContent" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewContent" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalContent" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkCMSDetails" runat="server" Text="More" OnClick="lnkCMSDetails_Click"></asp:LinkButton>
            </li>
            <li id="Profilesli" runat="server">
            <a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/ProfileManagement/ProfileDisplay.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Profiles) %>'
                title="Profiles Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/HercProfile.png")%>" alt="icon" height="50px" width="50px"/><br />
                    Profiles</span></a> 
                Draft :<asp:Label ID="lblDraftProfile" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewProfile" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalProfile" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkProfileDetails" runat="server" Text="More" OnClick="lnkProfileDetails_Click"></asp:LinkButton>
            </li>
            <li id="moduleli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/MixModules/Module_Display.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Regulations) %>'
                title="Regulations,Codes,Standards,Policies & Guidelines Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Modules.png")%>" alt="icon" /><br />
                    Modules</span></a> Draft :<asp:Label ID="lblDraftModule" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewModule" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalModule" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkModulesDetails" runat="server" Text="More" OnClick="lnkModulesDetails_Click"></asp:LinkButton>
            </li>
            <li id="Notificationli" runat="server"><a class="shortcut-button" href="<%=ResolveUrl("~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=10")%>"
                title="Notification Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/notification_icon.png")%>"
                        alt="icon" /><br />
                    Notification</span></a> Draft :<asp:Label ID="lblDraftNotification" runat="server"
                        Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewNotification" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalNotification" runat="server" Text=""></asp:Label>
            </li>
            <li id="Petitionli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Petition) %>'
                title="Petition Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/clock_48.png")%>" alt="icon" /><br />
                    Petition</span></a> Draft :<asp:Label ID="lblPendingPetition" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewPetition" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalPetition" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkPetition" runat="server" Text="More" OnClick="lnkPetition_Click"></asp:LinkButton>
            </li>
            <li id="publicli" runat="server"><a class="shortcut-button" href="<%=ResolveUrl("~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=9")%>"
                title="Public Notice Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Conferences.png")%>" alt="icon" /><br />
                    Public Notice </span></a>Draft :<asp:Label ID="lbldraftPublicNotice" runat="server"
                        Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewPublicNotice" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalPublicNotice" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkPublicNoticDetails" runat="server" Text="More" OnClick="lnkPublicNoticDetails_Click"></asp:LinkButton>
            </li>
            <li id="sohli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=")%><%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.SOH) %>'
                title="SOH Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/SOH.png")%>" alt="icon" /><br />
                    SOH</span></a> Draft :<asp:Label ID="lblDraftSoh" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewSoh" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalSoh" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkSohMore" runat="server" Text="More" OnClick="lnkSohMore_Click"></asp:LinkButton>
            </li>
            <li id="Ordersli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/OrderManagement/Order_Display.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Orders) %>'
                title="Order Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/order.png")%>" alt="icon" /><br />
                    Orders</span></a> Draft :<asp:Label ID="lblDraftOrders" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewOrders" runat="server" Text=""></asp:Label><br />
                For Publish:<asp:Label ID="lblApprovalOrders" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkOrderDetails" runat="server" Text="More" OnClick="lnkOrderDetails_Click"></asp:LinkButton>
            </li>
            <li id="tariffli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/TariffModule/View_Tariff.aspx?ModuleID=")%><%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Tariff) %>'
                title="Tariff Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Whats-New.png")%>" alt="icon" /><br />
                    Tariff </span></a>Draft :<asp:Label ID="lblDraftTariff" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewTariff" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalTariff" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkTariff" runat="server" Text="More" OnClick="lnkTariff_Click"></asp:LinkButton>
            </li>
            <li id="AnnualReportli" runat="server"><a class="shortcut-button" href="<%=ResolveUrl("~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=6")%>"
                title="Annual Repot Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/annual_perf_report.png")%>"
                        alt="icon" /><br />
                    Annual Report </span></a>Draft :<asp:Label ID="lblDraftAnnaulReport" runat="server"
                        Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewAnnaulReport" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblAppraovalAnnaulReport" runat="server" Text=""></asp:Label>
            </li>
            <li id="Discussionli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Discussion_Paper) %>'
                title="Discussion Paper Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Discussion.png")%>" alt="icon" /><br />
                    Discussion Paper</span></a> Draft :<asp:Label ID="lblDraftDisscussionPaper" runat="server"
                        Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewDisscussionPaper" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblAppraovalDisscussionPaper" runat="server" Text=""></asp:Label>
            </li>
            <li id="Vacancyli" runat="server"><a class="shortcut-button" href="<%=ResolveUrl("~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=13")%>"
                title="Vacancy Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/publication.png")%>" alt="icon" /><br />
                    Vacancy</span></a> Draft :<asp:Label ID="lblDraftVacancy" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewVacancy" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalVacancy" runat="server" Text=""></asp:Label>
            </li>
            <li id="RTIli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.RTI) %>'
                title="RTI Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/RTI.png")%>" alt="icon" /><br />
                    RTI</span></a> 
                    Draft :<asp:Label ID="lblDraftRTI" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewRTI" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalRTI" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkRtiDetails" runat="server" Text="More" OnClick="lnkRtiDetails_Click"></asp:LinkButton>
            </li>
            <li id="Appealli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Appeal) %>'
                title="E/O Appeal Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Appeal.png")%>" alt="icon" /><br />
                    E/O Appeal</span></a>
                    Draft :<asp:Label ID="lblDraftAppeal" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewAppeal" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalAppeal" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkAppeal" runat="server" Text="More" OnClick="lnkAppeal_Click"></asp:LinkButton>
                    </li>
            <li id="Awardli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/AwardsPronounced/DisplayAwards.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.AwardPronounced) %>'
                title="E/O AwardPronounced Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Award.png")%>" alt="icon" /><br />
                    E/O AwardPronounced</span></a>
                Draft :<asp:Label ID="lblDraftAwardPronounced" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewAwardPronounced" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalAwardPronounced" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="lnkAwardPronounced" runat="server" Text="More" OnClick="lnkAwardPronounced_Click"></asp:LinkButton>
                    </li>
            <li id="Reportsli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/Reports/View_Reports.aspx?ModuleID=")%> <%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Reports) %>'
                title="E/O Reports Management"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Profile.png")%>" alt="icon" /><br />
                    E/O Reports</span></a> Draft :<asp:Label ID="lblDraftReport" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewReport" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblApprovalReport" runat="server" Text=""></asp:Label>
            </li>
						   <li id="whatsNewli" runat="server"><a class="shortcut-button" href='<%=ResolveUrl("~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=")%><%= Convert.ToInt16( Module_ID_Enum.Project_Module_ID.Whats_New) %>'
                title="What'sNew"><span>
                    <img src="<%=ResolveUrl("~/Auth/adminpanel/images/icons/Photo Gallery.png")%>" alt="icon" /><br />
                   What's New </span></a>
               Draft :<asp:Label ID="lblDraftWhatsNew" runat="server" Text=""></asp:Label><br />
                Review :<asp:Label ID="lblReviewWhatsNew" runat="server" Text=""></asp:Label><br />
                For Publish :<asp:Label ID="lblForPublishWhatsNew" runat="server" Text=""></asp:Label>
                    </li>
    </div>
    <div class="clear">
    </div>
    <div>
    </div>
</asp:Content>
