using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AdminPanel_PendingReviewApproval : System.Web.UI.Page
{
    PetitionBL petitionBL = new PetitionBL();
    Project_Variables p_var = new Project_Variables();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 3)
        {

            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlTariff.Visible = false;
            pnlPetition.Visible = true;
            pnlProfile.Visible = false;
            PetitionTotalPending();
            PetitionTotalReview();
            PetitionTotalApproval();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 8)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlProfile.Visible = false;
            pnlTariff.Visible = false;
            pnlScheduleOfhearing.Visible = true;
            ScheduleOfHearingTotalPending();
            ScheduleOfHearingTotalReviewAll();
            ScheduleOfHearingTotalApprovalAll();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 5)
        {
            pnlPetition.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlTariff.Visible = false;
            pnlProfile.Visible = false;
            pnlRti.Visible = true;
            RTITotalPendingAll();
            RTITotalReviewAll();
            RTITotalApprovalAll();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 1)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlTariff.Visible = false;
            pnlProfile.Visible = false;
            pnlContent.Visible = true;
            countContentPendingall();
            countContentReviewall();
            countContentApproveall();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 17)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlProfile.Visible = false;
            pnlTariff.Visible = false;
            pnlOrder.Visible = true;
            countOrderPendingall();
            countOrderReviewall();
            countOrderApproveall();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 9)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlProfile.Visible = false;
            pnlTariff.Visible = false;
            pnlPublicNotice.Visible = true;
            countPublicNoticePendingall();
            countPublicNoticeReviewall();
            countPublicNoticeApproveall();
        }

        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 19)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAppeal.Visible = false;
            pnlModules.Visible = false;
            pnlProfile.Visible = false;
            pnlTariff.Visible = false;
            pnlAwardProunced.Visible = true;
            
            countAwardProuncedPendingall();
            countAwardProuncedReviewall();
            countAwardProuncedApproveall();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 4)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlModules.Visible = false;
            pnlProfile.Visible = false;
            pnlTariff.Visible = false;
            pnlAppeal.Visible = true;
            countAppealPendingall();
            countAppealReviewall();
            countAppealApproveall();
        }

        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 20)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlProfile.Visible = false;
            pnlTariff.Visible = false;
            pnlModules.Visible = true;
            countModulesPendingall();
            countModulesReviewall();
            countModulesApproveall();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 28)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlTariff.Visible = false;
            pnlProfile.Visible = true;
            pnlModules.Visible = false;
            countProfilePendingall();
            countProfileReviewall();
            countProfileApproveall();
        }
        else if (Convert.ToInt16(Request.QueryString["ModuleId"]) == 27)
        {
            pnlPetition.Visible = false;
            pnlRti.Visible = false;
            pnlScheduleOfhearing.Visible = false;
            pnlContent.Visible = false;
            pnlOrder.Visible = false;
            pnlPublicNotice.Visible = false;
            pnlAwardProunced.Visible = false;
            pnlAppeal.Visible = false;
            pnlProfile.Visible = false;
            pnlModules.Visible = false;
            pnlTariff.Visible = true;
            TariffTotalPendingAll();
            TariffTotalReviewAll();
            TariffTotalApprovalAll();
        }
    }

    #region function to get no of records for pending petitions

    public void PetitionTotalPending()
    {
        p_var.dSet = petitionBL.countPetitionPendingAll();
        lblPendingPetition.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblPendingReviewPetition.Text = p_var.dSet.Tables[1].Rows[0]["totalReviewPending"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        lblPendingAppealPetition.Text = p_var.dSet.Tables[2].Rows[0]["totalApprovePending"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
    }

    #endregion

    #region function to get no of records for review petitions

    public void PetitionTotalReview()
    {
        p_var.dSet = petitionBL.countPetitionReviewall();
        lblReviewPetition.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblReviewReviewPetition.Text = p_var.dSet.Tables[1].Rows[0]["totalReviewPending"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        lblReviewAppealPetition.Text = p_var.dSet.Tables[2].Rows[0]["totalApprovePending"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
    }

    #endregion

    #region function to get no of records for review petitions

    public void PetitionTotalApproval()
    {
        p_var.dSet = petitionBL.countPetitionApprovalAll();
        lblApprovalPetition.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblApprovalReviewPetition.Text = p_var.dSet.Tables[1].Rows[0]["totalReviewPending"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        lblApprovalAppealPetition.Text = p_var.dSet.Tables[2].Rows[0]["totalApprovePending"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
    }

    #endregion

    #region function to get no of records for draft schedule of hearing

    public void ScheduleOfHearingTotalPending()
    {
        p_var.dSet = petitionBL.countScheduleOfHearingPendingall();
        lblPendingHercDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblPendingOmbudsmanDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();

    }

    #endregion

    #region function to get no of records for review schedule of hearing

    public void countScheduleOfHearingPendingall()
    {
        p_var.dSet = petitionBL.countScheduleOfHearingPendingall();
        lblPendingHercDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString();
        lblPendingOmbudsmanDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString();

    }

    #endregion

    #region function to get no of records for draft content

    public void countContentPendingall()
    {
        p_var.dSet = petitionBL.countContentPendingAll();
        lblHercDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + " Menu Name: " + p_var.dSet.Tables[0].Rows[0]["name"].ToString();
        lblOmbudsmanDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + " Menu Name: " + p_var.dSet.Tables[1].Rows[0]["name"].ToString();

    }

    #endregion

    #region function to get no of records for review content

    public void countContentReviewall()
    {
        p_var.dSet = petitionBL.countContentReviewAll();
        lblHercReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + " Menu Name: " + p_var.dSet.Tables[0].Rows[0]["name"].ToString();
        lblOmbudsmanReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + " Menu Name: " + p_var.dSet.Tables[1].Rows[0]["name"].ToString();

    }

    #endregion

    #region function to get no of records for approval content

    public void countContentApproveall()
    {
        p_var.dSet = petitionBL.countContentApprovalAll();
        lblHercApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + " Menu Name: " + p_var.dSet.Tables[0].Rows[0]["name"].ToString();
        lblOmbudsmanApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + " Menu Name: " + p_var.dSet.Tables[1].Rows[0]["name"].ToString();

    }

    #endregion

    #region function to get no of records for review schedule of hearing

    public void ScheduleOfHearingTotalReviewAll()
    {
        p_var.dSet = petitionBL.countScheduleOfHearingReviewAll();
        lblReviewHercReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblPendingOmbudsmanReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();

    }

    #endregion



    #region function to get no of records for approval schedule of hearing

    public void ScheduleOfHearingTotalApprovalAll()
    {
        p_var.dSet = petitionBL.countScheduleOfHearingApprovalAll();
        lblApprovalHercApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblPendingOmbudsmanApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();

    }

    #endregion

    #region function to get no of records for draft RTI

    public void RTITotalPendingAll()
    {
        p_var.dSet = petitionBL.countRTIPendingAll();
        lblRtiHercPending.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblRtiOmbudsmanPending.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        lblRtifaaHercPending.Text = p_var.dSet.Tables[2].Rows[0]["totalPendingrtifaa"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
        lblRtifaaOmbudsmanPending.Text = p_var.dSet.Tables[3].Rows[0]["totalPendingrtifaaOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[3].Rows[0]["year"].ToString();
        lblRtisaaHercPending.Text = p_var.dSet.Tables[4].Rows[0]["totalPendingsaa"].ToString() + "  of years: " + p_var.dSet.Tables[4].Rows[0]["year"].ToString();
        lblRtisaaOmbudsmanPending.Text = p_var.dSet.Tables[5].Rows[0]["totalPendingsaaOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[5].Rows[0]["year"].ToString();

    }

    #endregion



    #region function to get no of records for review RTI

    public void RTITotalReviewAll()
    {
        p_var.dSet = petitionBL.countRTIReviewAll();
        lblRtiHercReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblRtiOmbudsmanReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        lblRtifaaHercReview.Text = p_var.dSet.Tables[2].Rows[0]["totalPendingrtifaa"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
        lblRtifaaOmbudsmanReview.Text = p_var.dSet.Tables[3].Rows[0]["totalPendingrtifaaOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[3].Rows[0]["year"].ToString();
        lblRtisaaHercReview.Text = p_var.dSet.Tables[4].Rows[0]["totalPendingsaa"].ToString() + "  of years: " + p_var.dSet.Tables[4].Rows[0]["year"].ToString();
        lblRtisaaOmbudsmanReview.Text = p_var.dSet.Tables[5].Rows[0]["totalPendingsaaOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[5].Rows[0]["year"].ToString();

    }

    #endregion

    #region function to get no of records for approval RTI

    public void RTITotalApprovalAll()
    {
        p_var.dSet = petitionBL.countRTIApprovalAll();
        lblRtiHercApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblRtiOmbudsmanApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        lblRtifaaHercApproval.Text = p_var.dSet.Tables[2].Rows[0]["totalPendingrtifaa"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
        lblRtifaaOmbudsmanApproval.Text = p_var.dSet.Tables[3].Rows[0]["totalPendingrtifaaOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[3].Rows[0]["year"].ToString();
        lblRtisaaHercApproval.Text = p_var.dSet.Tables[4].Rows[0]["totalPendingsaa"].ToString() + "  of years: " + p_var.dSet.Tables[4].Rows[0]["year"].ToString();
        lblRtisaaOmbudsmanApproval.Text = p_var.dSet.Tables[5].Rows[0]["totalPendingsaaOmbudsman"].ToString() + "  of years: " + p_var.dSet.Tables[5].Rows[0]["year"].ToString();

    }

    #endregion

    #region function to get no of records for draft Orders

    public void countOrderPendingall()
    {
        p_var.dSet = petitionBL.countOrdersPendingall();
        lblInterimOrderDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPendingInterimOrder"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["PlaceHolderFour"].ToString();
        lblFinalOrderDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingFinalOrder"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["PlaceHolderFour"].ToString();

    }

    #endregion

    #region function to get no of records for draft Orders

    public void countOrderReviewall()
    {
        p_var.dSet = petitionBL.countOrdersReviewall();
        lblInterimOrderReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPendingInterimOrder"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["PlaceHolderFour"].ToString();
        lblFinalOrderReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingFinalOrder"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["PlaceHolderFour"].ToString();

    }

    #endregion

    #region function to get no of records for draft Orders

    public void countOrderApproveall()
    {
        p_var.dSet = petitionBL.countOrdersApprovalall();
        lblInterimOrderApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPendingInterimOrder"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["PlaceHolderFour"].ToString();
        lblFinalOrderApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingFinalOrder"].ToString() + " of years: " + p_var.dSet.Tables[1].Rows[0]["PlaceHolderFour"].ToString();

    }

    #endregion

    #region function to get no of records for draft Orders

    public void countPublicNoticePendingall()
    {
        p_var.dSet = petitionBL.countPublicNoticePendingall();
        lblPublicNoticeDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["End_Date"].ToString();


    }

    #endregion


    #region function to get no of records for draft Orders

    public void countPublicNoticeReviewall()
    {
        p_var.dSet = petitionBL.countPublicNoticeReviewall();
        lblPublicNoticeReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["End_Date"].ToString();


    }

    #endregion

    #region function to get no of records for approval Orders

    public void countPublicNoticeApproveall()
    {
        p_var.dSet = petitionBL.countPublicNoticeApprovalall();
        lblPublicNoticeApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["End_Date"].ToString();


    }

    #endregion



    #region function to get no of records for approval Orders

    public void countAwardProuncedPendingall()
    {
        p_var.dSet = petitionBL.countAwardPronouncedPendingAll();
        lblAwardProuncedDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["AppealYear"].ToString();


    }

    #endregion

    #region function to get no of records for approval Orders

    public void countAwardProuncedReviewall()
    {
        p_var.dSet = petitionBL.countAwardPronouncedReviewAll();
        lblAwardProuncedReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["AppealYear"].ToString();


    }

    #endregion

    #region function to get no of records for approval Orders

    public void countAwardProuncedApproveall()
    {
        p_var.dSet = petitionBL.countAwardPronouncedApproveAll();
        lblAwardProuncedApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["AppealYear"].ToString();


    }

    #endregion

    #region function to get no of records for approval Orders

    public void countAppealPendingall()
    {
        p_var.dSet = petitionBL.countAppealPendingAll();
        lblAppealDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["Year"].ToString();
        lblAppealagainstApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["appeal_date"].ToString();

    }

    #endregion

    #region function to get no of records for approval Orders

    public void countAppealReviewall()
    {
        p_var.dSet = petitionBL.countAppealReviewAll();
        lblAppealReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["Year"].ToString();
        lblAppealagainstReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["appeal_date"].ToString();


    }

    #endregion

    #region function to get no of records for approval Orders

    public void countAppealApproveall()
    {
        p_var.dSet = petitionBL.countAppealApprovalAll();
        lblAppealApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["Year"].ToString();
        lblAppealagainstDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPending"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["appeal_date"].ToString();


    }

    #endregion

    #region function to get no of records for approval Orders

    public void countModulesPendingall()
    {
        p_var.dSet = petitionBL.countModulesPendingAll();
        lblRegulationDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString() ;
        lblCodesDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPending"].ToString();
        lblStandardsDraft.Text=p_var.dSet.Tables[2].Rows[0]["totalPending"].ToString();
        lblPolicyDraft.Text = p_var.dSet.Tables[3].Rows[0]["totalPending"].ToString();
        lblGuidelinesDraft.Text = p_var.dSet.Tables[4].Rows[0]["totalPending"].ToString();
    }

    #endregion

    #region function to get no of records for approval Orders

    public void countModulesReviewall()
    {
        p_var.dSet = petitionBL.countModulesReviewAll();
        lblRegulationReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString();
        lblCodesReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPending"].ToString();
        lblStandardsReview.Text = p_var.dSet.Tables[2].Rows[0]["totalPending"].ToString();
        lblPolicyReview.Text = p_var.dSet.Tables[3].Rows[0]["totalPending"].ToString();
        lblGuidelinesReview.Text = p_var.dSet.Tables[4].Rows[0]["totalPending"].ToString();
    }

    #endregion

    #region function to get no of records for approval Orders

    public void countModulesApproveall()
    {
        p_var.dSet = petitionBL.countModulesApprovalAll();
        lblRegulationApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString();
        lblCodesApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPending"].ToString();
        lblStandardsApproval.Text = p_var.dSet.Tables[2].Rows[0]["totalPending"].ToString();
        lblPolicyApproval.Text = p_var.dSet.Tables[3].Rows[0]["totalPending"].ToString();
        lblGuidelinesApproval.Text = p_var.dSet.Tables[4].Rows[0]["totalPending"].ToString();
    }

    #endregion



    #region function to get no of records for draft profile

    public void countProfilePendingall()
    {
        p_var.dSet = petitionBL.countProfilesPendingAll();
        lblProfileDraft.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString();
        lblOmbudsmanProfileDraft.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString();

    }

    #endregion

    #region function to get no of records for review content

    public void countProfileReviewall()
    {
        p_var.dSet = petitionBL.countProfilesReviewAll();
        lblProfileReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString();
        lblOmbudsmanProfileReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString();

    }

    #endregion

    #region function to get no of records for approval content

    public void countProfileApproveall()
    {
        p_var.dSet = petitionBL.countProfilesApprovalAll();
        lblProfileApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPending"].ToString();
        lblOmbudsmanProfileApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingOmbudsman"].ToString();

    }

    #endregion

    #region function to get no of records for draft tariff

    public void TariffTotalPendingAll()
    {
        p_var.dSet = petitionBL.countTariffPendingAll();
        lblTariffPending.Text = p_var.dSet.Tables[0].Rows[0]["totalPendingTariff"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblFSAPending.Text = p_var.dSet.Tables[2].Rows[0]["totalPendingFSA"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
        lblRenewalPending.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingGeneMisc"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();

    }

    #endregion

    #region function to get no of records for review Tariff

    public void TariffTotalReviewAll()
    {
        p_var.dSet = petitionBL.countTariffReviewAll();
        lblTariffReview.Text = p_var.dSet.Tables[0].Rows[0]["totalPendingTariff"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblFSAReview.Text = p_var.dSet.Tables[2].Rows[0]["totalPendingFSA"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
        lblRenewalReview.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingGeneMisc"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();
        
    }

    #endregion

    #region function to get no of records for approval Tariff

    public void TariffTotalApprovalAll()
    {
        p_var.dSet = petitionBL.countTariffApprovalAll();
        lblTariffApproval.Text = p_var.dSet.Tables[0].Rows[0]["totalPendingTariff"].ToString() + "  of years: " + p_var.dSet.Tables[0].Rows[0]["year"].ToString();
        lblFSAApproval.Text = p_var.dSet.Tables[2].Rows[0]["totalPendingFSA"].ToString() + "  of years: " + p_var.dSet.Tables[2].Rows[0]["year"].ToString();
        lblRenewalApproval.Text = p_var.dSet.Tables[1].Rows[0]["totalPendingGeneMisc"].ToString() + "  of years: " + p_var.dSet.Tables[1].Rows[0]["year"].ToString();

    }

    #endregion
}