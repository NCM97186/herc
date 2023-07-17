using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.SessionState;
using System.Reflection;


public partial class AdminPanel_DashBoard : System.Web.UI.Page
{
   
    #region variable declaration 

    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    PetitionBL petitionBL = new PetitionBL();
    Project_Variables p_var = new Project_Variables();
    UserOB obj_UserOB = new UserOB();
    
    
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("Lng"); // RTI sessions
        Session.Remove("deptt");
        Session.Remove("Status");
        Session.Remove("year");
        Session.Remove("FAALng"); // FAARTI sessions
        Session.Remove("FAAdeptt");
        Session.Remove("FAAStatus");
        Session.Remove("FAAyear");
        Session.Remove("SAALng"); // SAARTI sessions
        Session.Remove("SAAdeptt");
        Session.Remove("SAAStatus");
        Session.Remove("SAAyear");

        Session.Remove("MixLng"); // Mix module sessions
        Session.Remove("Mixdeptt");
        Session.Remove("MixStatus");
        Session.Remove("MixModule");

        Session.Remove("Appealyear"); //Appeal module sessions
        Session.Remove("AppealLng");
        Session.Remove("Appealdeptt");
        Session.Remove("AppealStatus");

        Session.Remove("PStatus");// Public Notice module sessions
        Session.Remove("PYear");
        Session.Remove("PLng");

        Session.Remove("MLng"); //  module sessions
        Session.Remove("Mdeptt");
        Session.Remove("MStatus");

        Session.Remove("ProfileLng"); // Profile module sessions
        Session.Remove("ProfileNvg");
        Session.Remove("ProfileDeptt");
        Session.Remove("profileStatus");

        Session.Remove("RoleDeptt");

        Session.Remove("menulang"); //  menu sessions
        Session.Remove("menuposition");
        Session.Remove("menulst");
        Session.Remove("menustatus");

        Session.Remove("TariffCategory"); //  Tariff sessions
        Session.Remove("TariffLng");
        Session.Remove("TariffDeptt");
        Session.Remove("TariffType");
        Session.Remove("TariffStatus");
        Session.Remove("OrderYear"); //  Order sessions
        Session.Remove("OrderLng");
        Session.Remove("OrderType");
        Session.Remove("OrderStatus");

        Session.Remove("AwardYear"); //  Award sessions
        Session.Remove("AwardLng");
        Session.Remove("AwardStatus");

        Session.Remove("PetAppealLng"); //  Appeal petition sessions
        Session.Remove("PetAppealYear");
        Session.Remove("PetAppealStatus");

        Session.Remove("PetRvYear");    // review  petition sessions
        Session.Remove("PetRvLng");
        Session.Remove("PetRvStatus");

        Session.Remove("PetLng"); //  Petition sessions
        Session.Remove("PetYear");
        Session.Remove("PetStatus");

        Session.Remove("Sohdeptt"); // SOH sessions
        Session.Remove("SohLng");
        Session.Remove("SohYear");
        Session.Remove("SohStatus");

        Session.Remove("SohPetitionAppeal");
        Session.Remove("SohAppeal");
        Session.Remove("appealYear1");

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": DashBoard";
        this.Page.Title = "DashBoard: HERC";

        if (!IsPostBack)
        {
           
            False_all();
            chk_privilages();
        }
        PetitionTotal();
        PublicNoticeTotal();
        ScheduleOfHearingTotal();
        OrdersTotal();
        NotificationsTotal();
        TariffTotal();
        AnnualReportTotal();
        VacancyTotal();
        DisscussionPaperTotal();
        ReportTotal();
        modulesTotal();
        ProfileTotal();
        ContentTotal();
        RTITotal();
        AppealTotal();
        AwardPronouncedTotal();
        BannerTotal();
		WhatsNewTotal();
        //if (Session["UserName"] == null)
        //{
        //    Response.Redirect("~/AdminPanel/Login.aspx");
        //}
        //Label lbl = (Label)Page.Master.FindControl("lblModulename");
        //lbl.Text = "Welcome To Sulabh Envis Admin Panel";
        //if (Session["AdminID"] == null)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        Session.Abandon();
        //        Response.Redirect("~/AdminPanel/Login.aspx");
        //    }
        //    else
        //    {
        //        return;
               
        //    }
        //    Response.Redirect("~/AdminPanel/Login.aspx");
        //}
        ////else if (Session["PTypeID"] == null)
        ////{
        ////    Response.Redirect("~/Home.aspx");
        ////}

        
    }


    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("EditPswd.aspx");
    //}




//User defind functions  area----------


    #region Function  false to the li's

    public void False_all()
    {
        AnnualReportli.Visible = false;

        Notificationli.Visible = false;
        publicli.Visible = false;

        Vacancyli.Visible = false;
        Menuli.Visible = false;
        Discussionli.Visible = false;
        //Userli.Visible = false;
        //Roleli.Visible = false;
        sohli.Visible = false;
        Bannerli.Visible = false;
        tariffli.Visible = false;
        Ordersli.Visible = false;
        Petitionli.Visible = false;
        Appealli.Visible = false;
        Awardli.Visible = false;
        RTIli.Visible = false;
        moduleli.Visible = false;
        Profilesli.Visible = false;
        Reportsli.Visible = false;
		 whatsNewli.Visible = false;
    }

    #endregion 

    #region Function to check the permisions

    public void chk_privilages()
    {
        DataSet dsprv = new DataSet();
        obj_UserOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        dsprv = obj_RoleBL.ASP_CheckPrivilagesALL_For_Master(obj_UserOB);
        if (dsprv.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsprv.Tables[0].Rows.Count; i++)
            {
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("1"))
                {
                    Menuli.Visible = true;
                }
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("3"))
                {
                    Petitionli.Visible = true;

                }
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("4"))
                {
                    Appealli.Visible = true;
                }
                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("5"))
                {
                    RTIli.Visible = true;
                }


                if (dsprv.Tables[0].Rows[i][1].ToString().Equals("6"))
                {
                    AnnualReportli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("7"))
                {
                    whatsNewli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("8"))
                {
                    sohli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("9"))
                {
                    publicli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("10"))
                {
                    Notificationli.Visible = true;
                }
                //else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("11"))
                //{
                //    Licensesli.Visible = true;
                //}
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("12"))
                {
                    Discussionli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("13"))
                {
                    Vacancyli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("14"))
                {
                    //Roleli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("15"))
                {
                   // Userli.Visible = true;
                }

                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("17"))
                {
                    Ordersli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("18"))
                {
                    Bannerli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("19"))
                {
                    Awardli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("20"))
                {
                    moduleli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("27"))
                {
                    tariffli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("28"))
                {
                    Profilesli.Visible = true;
                }
                else if (dsprv.Tables[0].Rows[i][1].ToString().Equals("29"))
                {
                    Reportsli.Visible = true;
                }
            }
        }
    }

    #endregion 

 //End

    #region function to get no of records for pending petitions

    public void PetitionTotal()
    {
       lblPendingPetition.Text= petitionBL.countPetitionPending();
       lblReviewPetition.Text = petitionBL.countPetitionReview();
       lblApprovalPetition.Text = petitionBL.countPetitionApproval();
    }

    #endregion

    #region function to get no of records for Pending PublicNotice

    public void PublicNoticeTotal()
    {
        lbldraftPublicNotice.Text = petitionBL.countPublicNoticePending();
        lblReviewPublicNotice.Text = petitionBL.countPublicNoticeReview();
        lblApprovalPublicNotice.Text = petitionBL.countPublicNoticeApproval();
    }

    #endregion

    #region function to get no of records for pending Schedule Of Hearing

    public void ScheduleOfHearingTotal()
    {
        lblDraftSoh.Text = petitionBL.countScheduleOfHearingPending();
        lblReviewSoh.Text = petitionBL.countScheduleOfHearingReview();
        lblApprovalSoh.Text = petitionBL.countScheduleOfHearingApproval();
    }

    #endregion

    #region function to get no of records for pending orders

    public void OrdersTotal()
    {
       lblDraftOrders.Text = petitionBL.countOrdersPending();
       lblReviewOrders.Text = petitionBL.countOrdersReview();
       lblApprovalOrders.Text = petitionBL.countOrdersApproval();
    }

    #endregion

    #region function to get no of records for pending notifications

    public void NotificationsTotal()
    {
        lblDraftNotification.Text = petitionBL.countNotificationPending();
        lblReviewNotification.Text = petitionBL.countNotificationReview();
        lblApprovalNotification.Text = petitionBL.countNotificationApproval();
    }

    #endregion

    #region function to get no of records for pending tariff

    public void TariffTotal()
    {
        lblDraftTariff.Text = petitionBL.countTariffPending();
        lblReviewTariff.Text = petitionBL.countTariffReview();
        lblApprovalTariff.Text = petitionBL.countTariffApproval();
    }

    #endregion

    #region function to get no of records for pending annaul report

    public void AnnualReportTotal()
    {
        lblDraftAnnaulReport.Text = petitionBL.countAnnualReportPending();
        lblAppraovalAnnaulReport.Text = petitionBL.countAnnualReportApproval();
        lblReviewAnnaulReport.Text = petitionBL.countAnnualReportReview();
    }

    #endregion

    #region function to get no of records for pending vacancy

    public void VacancyTotal()
    {
        lblDraftVacancy.Text = petitionBL.countVacancyPending();
        lblReviewVacancy.Text = petitionBL.countVacancyReview();
        lblApprovalVacancy.Text = petitionBL.countVacancyApproval();
    }

    #endregion

    public void DisscussionPaperTotal()
    {
        lblDraftDisscussionPaper.Text = petitionBL.countDisscussionPaperPending();
        lblReviewDisscussionPaper.Text = petitionBL.countDisscussionPaperReview();
        lblAppraovalDisscussionPaper.Text = petitionBL.countDisscussionPaperApproval();
    }

    public void ReportTotal()
    {

        lblDraftReport.Text = petitionBL.countReportPending();
        lblReviewReport.Text = petitionBL.countReportReview();
        lblApprovalReport.Text = petitionBL.countReportApproval();
    }

    public void modulesTotal()
    {

        lblDraftModule.Text = petitionBL.countModulesPending();
        lblReviewModule.Text = petitionBL.countModulesReview();
        lblApprovalModule.Text = petitionBL.countModulesApproval();
    }

    public void ProfileTotal()
    {

        lblDraftProfile.Text = petitionBL.countProfilePending();
        lblReviewProfile.Text = petitionBL.countProfileReview();
        lblApprovalProfile.Text = petitionBL.countProfileApproval();
    }

    public void ContentTotal()
    {

        lblDraftContent.Text = petitionBL.countContentPending();
        lblReviewContent.Text = petitionBL.countContentReview();
        lblApprovalContent.Text = petitionBL.countContentApproval();
    }

    public void RTITotal()
    {

        lblDraftRTI.Text = petitionBL.countRTIPending();
        lblReviewRTI.Text = petitionBL.countRTIReview();
        lblApprovalRTI.Text = petitionBL.countRTIApproval();
    }
    public void AppealTotal()
    {

        lblDraftAppeal.Text = petitionBL.countAppealPending();
        lblReviewAppeal.Text = petitionBL.countAppealReview();
        lblApprovalAppeal.Text = petitionBL.countAppealApproval();
    }

    public void AwardPronouncedTotal()
    {

        lblDraftAwardPronounced.Text = petitionBL.countAwardPronouncedPending();
        lblReviewAwardPronounced.Text = petitionBL.countAwardPronouncedReview();
        lblApprovalAwardPronounced.Text = petitionBL.countAwardPronouncedApproval();
    }

    public void BannerTotal()
    {

        lblDraftBanner.Text = petitionBL.countBannerPending();
        lblReviewBanner.Text = petitionBL.countBannerReview();
        lblApprovalBanner.Text = petitionBL.countBannerApproval();
    }
	    public void WhatsNewTotal()
    {

        lblDraftWhatsNew.Text = petitionBL.countwhatsnewPending();
        lblReviewWhatsNew.Text = petitionBL.countwhatsnewReview();
        lblForPublishWhatsNew.Text = petitionBL.countwhatsnewApproval();
    }
    protected void lnkPetition_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 3) + "', 'mywindow', " +
                                       "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                                       "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkSohMore_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 8) + "', 'mywindow', " +
                               "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                               "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkRtiDetails_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 5) + "', 'mywindow', " +
                                      "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                                      "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkCMSDetails_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 1) + "', 'mywindow', " +
                              "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                              "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkOrderDetails_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 17) + "', 'mywindow', " +
                      "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                      "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkPublicNoticDetails_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 9) + "', 'mywindow', " +
                    "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                    "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkAwardPronounced_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 19) + "', 'mywindow', " +
            "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
            "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkAppeal_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 4) + "', 'mywindow', " +
    "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
    "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkModulesDetails_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 20) + "', 'mywindow', " +
    "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
    "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }

    void regenerateId()
    {
         System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
         string oldId = manager.GetSessionID(Context);
         string newId = manager.CreateSessionID(Context);
         bool isAdd = false, isRedir = false;
         manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
         HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
         HttpModuleCollection mods = ctx.Modules;
         System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
         System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
         SessionStateStoreProviderBase store = null;
         System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
         foreach (System.Reflection.FieldInfo field in fields)
         {
             if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
             if (field.Name.Equals("_rqId")) rqIdField = field;
             if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
             if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
         }
         object lockId = rqLockIdField.GetValue(ssm);
         if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
         rqStateNotFoundField.SetValue(ssm, true);
         rqIdField.SetValue(ssm, newId);
    }
    protected void lnkProfileDetails_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 28) + "', 'mywindow', " +
                      "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                      "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
    protected void lnkTariff_Click(object sender, EventArgs e)
    {
        p_var.strPopupID = "<script language='javascript'>" + "window.open('" + ResolveUrl("~/Auth/AdminPanel/" + "PendingReviewApproval.aspx?ModuleId=" + 27) + "', 'mywindow', " +
                                    "'menubar=no, resizable=yes, scrollbars=yes,width=700,Height=530,top=0,left=0 ')" +
                                    "</script>";
        this.Page.RegisterStartupScript("PopupScript", p_var.strPopupID);
    }
}
