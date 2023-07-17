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
using System.IO;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Collections.Generic;
using System.Net;
using System.Text;

public partial class Auth_AdminPanel_PublicNotice_PublicNoticeDisplay : System.Web.UI.Page
{
    //Area for all the variables declaration

    #region Data declaration zone

    Project_Variables p_Var = new Project_Variables();
    PetitionOB petObject = new PetitionOB();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();
    PetitionBL petPetitionBL = new PetitionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    LinkOB obj_inkOB = new LinkOB();
    LinkBL obj_linkBL = new LinkBL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    PaginationBL pagingBL = new PaginationBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for the page load event 

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("WhatsNewStatus"); // What's New sessions
        Session.Remove("Lng");  // RTI sessions
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

        Session.Remove("MLng"); //  module sessions
        Session.Remove("Mdeptt");
        Session.Remove("MStatus");

        Session.Remove("RoleDeptt");//Role Sessions

        Session.Remove("UsrDeptt");//User Sessions
        Session.Remove("UsrStatus");

        Session.Remove("ProfileLng"); // Profile module sessions
        Session.Remove("ProfileNvg");
        Session.Remove("ProfileDeptt");
        Session.Remove("profileStatus");

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
        lblModulename.Text = ": View  Public Notice";
        this.Page.Title = " Public Notice: HERC";

        if (!IsPostBack)
        {
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            ViewState["sortOrder"] = "";
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            ViewState["sortOrder"] = "";
            //lblPageSize.Visible   = false;
            //ddlPageSize.Visible   = false;
            //rptPager.Visible      = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnForReview.Visible = false;
            PLanguage.Visible = false;
            lblmsg.Visible = false;
            //Get_Deptt_Name();
            if (Session["PLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["PLng"].ToString();

            }
            else
            {
                bindropDownlistLang();

            }

            if (Session["PYear"] != null)
            {
                bindPublicNoticeYearinDdl();
                ddlYear.SelectedValue = Session["PYear"].ToString();
            }
            else
            {
                bindPublicNoticeYearinDdl();
            }
            if (Session["connectionType"] != null)
            {
                ddlConnectionType.SelectedValue = Session["connectionType"].ToString();
            }
            else
            {

            }
            if (Session["PStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["PStatus"].ToString();
                Bind_Grid("", "");
            }
            else
            {
                binddropDownlistStatus();
            }


        }
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddPublicNotice click event to add new public notice

    protected void btnAddPublicNotice_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/PublicNotice/") + "PublicNoticeAdd_Edit.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    #region button btnForReview click event to send record for review

    protected void btnForReview_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

    }

    #endregion

    #region button btnForApprove click event to send record for approval

    protected void btnForApprove_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();


    }

    #endregion

    #region button btnApprove click event to approve records

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
       //ChkApprove();
    }

    #endregion

    #region button btnDisApprove click event to disapprove

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithEmailIDDiaapproves();
            //this.mpeSendEmailDis.Show();
        }
        else
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithDataEntryEmailIDs();
            // this.mpeSendEmailDis.Show();
        }
        //try
        //{
        //    foreach (GridViewRow row in grdPublicNotice.Rows)
        //    {
        //        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
        //        if ((selCheck.Checked == true))
        //        {
        //            //Label lblid = (Label)row.FindControl("lblRTI_ID");
        //            p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
        //            petObject.TmpPublicNoticeID = p_Var.dataKeyID;
        //            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
        //            {
        //                petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
        //            }
        //            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
        //            {
        //                petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
        //            }
        //            else
        //            {
        //                petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
        //            }
        //            //petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
        //            p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);
        //        }
        //    }
        //    if (p_Var.Result > 0)
        //    {
        //        Session["msg"] = "Public notice has been disapproved successfully.";
        //        Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
        //        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        //    }
        //    else
        //    {
        //        Session["msg"] = "Public notice has not been disapproved successfully.";
        //        Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
        //        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
    }

    #endregion

    #region button btnDelete click event to delete petition

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        

    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Bind_Grid("", "");
    }

    #endregion

    //End

    //Area for all the dropDownlists, listviews events

    #region dropDownlist ddlLanguage selectedIndexChanged event

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdPublicNotice.Visible = false;
            Session["PLng"] = ddlLanguage.SelectedValue;
        }
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlConnectionType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdPublicNotice.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            Session["PStatus"] = ddlStatus.SelectedValue;
            Bind_Grid("", "");

        }
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    #region gridView grdPublicNotice rowCommand event

    protected void grdPublicNotice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
            ViewState["commandArgs"] = p_Var.commandArgs;
            p_Var.petition_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        }


        else if (e.CommandName == "Restore")
        {


            petObject.PublicNoticeID = Int32.Parse(e.CommandArgument.ToString());


            p_Var.Result = pubNoticeBL.updatePublicStatusDelete(petObject);
            if (p_Var.Result > 0)
            {

                obj_audit.ActionType = "R";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.Title = "Public Notice";
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "Public notice has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Public notice has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        else if (e.CommandName == "Audit")
        {

            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Public_Notice);
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            petObject.ModuleType = null;
            dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
            Label lblprono = row.FindControl("lblTitle") as Label;
            if (dSetAuditTrail.Tables[0].Rows.Count > 0)
            {
                ltrlPetitionNo.Text = "<strong>" + lblprono.Text + "</strong>";
                if (dSetAuditTrail.Tables[0].Rows[0]["createdBy"] != DBNull.Value)
                {
                    ltrlCreation.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["createdBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedIPAddress"].ToString();
                }
                else
                {
                    ltrlCreation.Text = "";
                }
                if (dSetAuditTrail.Tables[0].Rows[0]["editedBy"] != DBNull.Value)
                {
                    ltrlLastEdited.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["editedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["EditedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["UpdatedIPAddress"].ToString();
                }
                else
                {
                    ltrlLastEdited.Text = "";
                }
                if (dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"] != DBNull.Value)
                {
                    ltrlLastReviewed.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedDate"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedIPAddress"].ToString();
                }
                else
                {
                    ltrlLastReviewed.Text = "";
                }
                if (dSetAuditTrail.Tables[0].Rows[0]["publishedBy"] != DBNull.Value)
                {
                    ltrlLastPublished.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["publishedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedIPAddress"].ToString();
                }
                else
                {
                    ltrlLastPublished.Text = "";
                }
            }
            else
            {
                ltrlCreation.Text = "";
                ltrlLastEdited.Text = "";
                ltrlLastPublished.Text = "";
                ltrlLastReviewed.Text = "";
                ltrlPetitionNo.Text = "";
            }
            this.mdpAuditTrail.Show();
        }
    }

    #endregion

    #region gridView grdPublicNotice rowCreated event

    protected void grdPublicNotice_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdPublicNotice.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));
        }
    }

    #endregion

    #region gridView grdPublicNotice rowDataBound event

    protected void grdPublicNotice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("imgnotedit") as System.Web.UI.WebControls.Image;
            HiddenField hydelete = (HiddenField)e.Row.FindControl("hydelete");
            petObject.PublicNoticeID = Convert.ToInt32(grdPublicNotice.DataKeys[e.Row.RowIndex].Value);
            p_Var.dSet = pubNoticeBL.PublicNoticeId(petObject);

            for (int i = 0; i < p_Var.dSet.Tables[0].Rows.Count; i++)
            {
                if (p_Var.dSet.Tables[0].Rows[i]["PublicNoticeID"] != DBNull.Value)
                {

                    if (Convert.ToInt32(grdPublicNotice.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSet.Tables[0].Rows[i]["PublicNoticeID"]))
                    {

                        img.Visible = false;
                        img1.Visible = true;
                        img1.ImageUrl = "~/Auth/AdminPanel/images/th_star.png";
                        img1.Height = 10;
                        img1.Width = 10;
                    }
                    else
                    {
                        img1.Visible = false;
                        img.Visible = true;
                        img.ImageUrl = "~/Auth/AdminPanel/images/edit.gif";
                    }
                }
            }
            p_Var.dSet = null;


            //This is for delete/permanently delete 3 june 2013 

            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                if (hydelete.Value != null && hydelete.Value != "")
                {
                    if (ddlConnectionType.SelectedValue == "1")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure want to Purge (delete permanently) Public Notice against Petition No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");

                        BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure want to Restore Public Notice against Petition No-" + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                    else if (ddlConnectionType.SelectedValue == "2")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure want to Purge (delete permanently) Public Notice against Review Petition No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");

                        BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure want to Restore Public Notice against Review Petition No-" + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure want to Purge (delete permanently) this Public Notice ?" + "')");

                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are you sure want to Restore this Public Notice ?" + "')");

                }
            }
            else
            {
                if (hydelete.Value != null && hydelete.Value != "")
                {
                    if (ddlConnectionType.SelectedValue == "1")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure want to Delete Public Notice against Petition No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");

                        BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure want to Restore Public Notice against Petition No-" + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                    else if (ddlConnectionType.SelectedValue == "2")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                       "confirm('Are you sure want to Delete Public Notice against Review Petition No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");

                        BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('AAre you sure want to Restore Public Notice against Review Petition No-" + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure you want to Delete this Public Notice ?" + "')");

                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
               "confirm('Are you sure you want to Restore this Public Notice ?" + "')");
                }

            }

            //END
        }
    }

    #endregion

    #region gridView grdPublicNotice rowDeleting event

    protected void grdPublicNotice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        //petObject.RPId = p_Var.rp_id;
        petObject.TmpPublicNoticeID = p_Var.rp_id;
        petObject.StatusId = p_Var.status_id;

        p_Var.Result = pubNoticeBL.Delete_PublicNotices(petObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "Public notice has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "Public notice has been deleted successfully.";
            }

            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.Title = "Public Notice";
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

            Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
        else
        {
            Session["msg"] = "Public notice has not been deleted successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }

    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscellBL.getLanguagePermission(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                UserOB usrObject = new UserOB();
                if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.english).Append(",");
                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);

                    p_Var.sbuilder.Append(usrObject.english);
                }
                usrObject.LangId = p_Var.sbuilder.ToString().Trim();
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.dSet = null;
                p_Var.dSet = miscellBL.getLanguage(usrObject);
                PLanguage.Visible = true;
                ddlLanguage.DataSource = p_Var.dSet;
                ddlLanguage.DataTextField = "Language";
                ddlLanguage.DataValueField = "Lang_Id";
                ddlLanguage.DataBind();

            }
            p_Var.dSet = null;
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind status in dropDownlist according to permission

    public void binddropDownlistStatus()
    {
        Miscelleneous_BL miscDdlStatus = new Miscelleneous_BL();
        Miscelleneous_BL miscdlStatus = new Miscelleneous_BL();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_Var.dSet = miscDdlStatus.getLanguagePermission(obj_userOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                btnAddPublicNotice.Visible = true;
                //code written on date 20 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end

            }
            else
            {
                btnAddPublicNotice.Visible = false;
            }

            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(","); ;
                // 18 feb
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete));
            }

            usrObject.ModulestatusID = p_Var.sbuilder.ToString();
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            p_Var.dSet = null;
            p_Var.dSet = miscdlStatus.getStatusPermissionwise(usrObject);
            ddlStatus.DataSource = p_Var.dSet;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "Status_Id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Status", "0"));

            btnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

    #region Function To bind the public notice in gridView

    public void Bind_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        if (ddlStatus.SelectedValue == "0")
        {
            grdPublicNotice.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdPublicNotice.Visible = true;
            //petObject.PageIndex = pageIndex;
            //petObject.DepttId = departmentid;
            // petObject.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            petObject.PetitionType = Convert.ToInt32(ddlConnectionType.SelectedValue);
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            petObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            petObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = pubNoticeBL.DisplayPublicNoticesWithPaging(petObject, out p_Var.k);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdPublicNotice.Columns[9].HeaderText = "Purge";
                }
                else
                {
                    grdPublicNotice.Columns[9].HeaderText = "Delete";
                }

                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End

                grdPublicNotice.DataSource = myDataView;
                if (ddlConnectionType.SelectedValue == "1")
                {
                    grdPublicNotice.Columns[4].Visible = true;
                    grdPublicNotice.Columns[4].HeaderText = "PRO No";
                }
                else if (ddlConnectionType.SelectedValue == "2")
                {
                    grdPublicNotice.Columns[4].Visible = true;
                    grdPublicNotice.Columns[4].HeaderText = "RA No";
                }
                else
                {
                    grdPublicNotice.Columns[4].Visible = false;
                }
                grdPublicNotice.DataBind();
                p_Var.dSet = null;

                Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
                Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
                if (p_Var.dSet.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdPublicNotice.Columns[0].Visible = false;
                    }
                    else
                    {
                        grdPublicNotice.Columns[0].Visible = true;
                    }
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                    {
                        btnForReview.Visible = true;
                    }
                    else
                    {
                        btnForReview.Visible = false;
                    }

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        btnForApprove.Visible = true;
                        btnDisApprove.Visible = true;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        btnApprove.Visible = true;
                        btnDisApprove.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnForApprove.Visible = false;
                        btnDisApprove.Visible = false;
                    }

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        btnApprove.Visible = true;
                        btnDisApprove.Visible = true;
                        btnForApprove.Visible = false;
                    }
                    else
                    {

                        btnApprove.Visible = false;
                        //  btnDisApprove.Visible = false;
                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdPublicNotice.Columns[8].Visible = false;//This is for Edit
                            grdPublicNotice.Columns[10].Visible = true;//This is for restore
                            grdPublicNotice.Columns[11].Visible = false;
                        }
                        else
                        {
                            grdPublicNotice.Columns[8].Visible = true;
                            grdPublicNotice.Columns[10].Visible = false;
                        }
                        //grdPublicNotice.Columns[6].Visible = true;
                    }
                    else
                    {
                        grdPublicNotice.Columns[8].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        //grdPublicNotice.Columns[9].Visible = true;

                        // modify on date 21 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdPublicNotice.Columns[9].Visible = true; 
                                grdPublicNotice.Columns[11].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdPublicNotice.Columns[9].Visible = true;
                                    grdPublicNotice.Columns[11].Visible = false;
                                }
                                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdPublicNotice.Columns[11].Visible = true;
                                    }
                                    else
                                    {
                                        grdPublicNotice.Columns[11].Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdPublicNotice.Columns[9].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdPublicNotice.Columns[11].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdPublicNotice.Columns[11].Visible = true;
                                            }
                                            else
                                            {
                                                grdPublicNotice.Columns[11].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdPublicNotice.Columns[11].Visible = false;
                                        grdPublicNotice.Columns[9].Visible = false;
                                    }


                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdPublicNotice.Columns[9].Visible = true;
                                grdPublicNotice.Columns[11].Visible = false;
                            }
                            else
                            {
                                //grdCMSMenu.Columns[7].Visible = false;
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdPublicNotice.Columns[9].Visible = true;
                            }
                            else
                            {
                                //grdCMSMenu.Columns[7].Visible = false;
                            }
                        }


                        //End    
                    }
                    else
                    {
                        grdPublicNotice.Columns[9].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                
                grdPublicNotice.Visible = false;
                btnForReview.Visible = false;
                btnForApprove.Visible = false;
                btnApprove.Visible = false;
                btnDisApprove.Visible = false;
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "No record found.";
            }
        }
        p_Var.Result = p_Var.k;

        Session["Status_Id"] = ddlStatus.SelectedValue.ToString();
        Session["Lanuage"] = ddlLanguage.SelectedValue;

    }

    #endregion



    //End

    //New codes for sorting

    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }

    //End

    protected void grdPublicNotice_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, sortOrder);
    }


    #region dropDownlist ddlConnectionType selectedIndexChanged events

    protected void ddlConnectionType_SelectedIndexChanged(object sender, EventArgs e)
    {

        binddropDownlistStatus();
        grdPublicNotice.Visible = false;
        Session["connectionType"] = ddlConnectionType.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlConnectionType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdPublicNotice.Visible = false;
        Session["PYear"] = ddlYear.SelectedValue;
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlConnectionType.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }


    #region Function to bind Orders Year

    public void bindPublicNoticeYearinDdl()
    {
        p_Var.dSet = pubNoticeBL.GetYearPublicNotice_Admin();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
        }
    }

    #endregion

    public void BindGridDetails()
    {


        if (ddlStatus.SelectedValue == "0")
        {
            grdPublicNoticePdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdPublicNoticePdf.Visible = true;

            petObject.PetitionType = Convert.ToInt32(ddlConnectionType.SelectedValue);
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            petObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            petObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = pubNoticeBL.DisplayPublicNoticesWithPaging(petObject, out p_Var.k);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                grdPublicNoticePdf.DataSource = p_Var.dSet;
                grdPublicNoticePdf.DataBind();
                p_Var.dSet = null;
            }

        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        BindGridDetails();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PublicNotice_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdPublicNoticePdf.AllowPaging = false;
        grdPublicNoticePdf.DataBind();
        grdPublicNoticePdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    protected void grdPublicNoticePdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTitle = (Label)e.Row.FindControl("lblTitle");
            Label lblRemarksPublic = (Label)e.Row.FindControl("lblRemarksPublic");

            if (lblTitle.Text != "" && lblTitle.Text != null)
            {
                lblTitle.Text = HttpUtility.HtmlDecode(lblTitle.Text);
            }
            if (lblRemarksPublic.Text != "" && lblRemarksPublic.Text != null)
            {
                lblRemarksPublic.Text = HttpUtility.HtmlDecode(lblRemarksPublic.Text);
            }

            // connectedFile
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            petObject.PublicNoticeID = Convert.ToInt32(grdPublicNoticePdf.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = pubNoticeBL.getPublicNoticeFileNames(petObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {
                    p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + "</Label>");
                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                ltrlConnectedFile1.Text = p_Var.sbuilder.ToString();

            }
        }
    }

    protected void grdPublicNotice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPublicNotice.PageIndex = e.NewPageIndex;
        Bind_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
        //Bind_Grid("", "");
    }


  

    //All events are for send emails to related officers

    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdPublicNotice.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblTitle = (Label)row.FindControl("lblTitle");
                    HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                    ViewState["Title"] = lblTitle.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Public Notice -</b> " + hidden1.Value + "<br/> ");
                        p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                        petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        petObject.userID = Convert.ToInt32(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);

                    }
                }
                if (p_Var.Result > 0)
                {

                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
                            //p_Var.sbuildertmp.Append(li.Value + ";");
                            int statindex = li.Text.IndexOf("(") + 1;
                            p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_Var.sbuilder.Append("Record pending for Review: " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                       miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }

                   
                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                        string[] stringSeparators = new string[] { "<br/>" };
                        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        ArrayList listPublicNotice = new ArrayList();
                        foreach (string itemPublicNotice in splitPublicNotice)
                        {
                            listPublicNotice.Add(itemPublicNotice.Trim());
                        }

                        foreach (string strPublicNotice in listPublicNotice)
                        {
                            if (strPublicNotice != string.Empty && strPublicNotice != "")
                            {
                                //loop through cells in that row
                                 foreach (string str in list)
                                    {
                                        if (str != string.Empty)
                                        {

                                            //string message = ViewState["Title"].ToString();
                                            string message = strPublicNotice;

                                            if (message.Length > 150)
                                            {
                                                message =strPublicNotice.ToString().Substring(0, 150) + "...";
                                            }
                                            else
                                            {
                                                message = strPublicNotice.ToString();
                                            }
                                            string textmessage = "HERC - Record pending for review - ";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                        }

                                    }
                               
                            }
                        }
                       
                    /* End */

                    Session["msg"] = "Public notice has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }

            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {

                sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                foreach (GridViewRow row in grdPublicNotice.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblTitle = (Label)row.FindControl("lblTitle");
                    HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                    ViewState["TitlePublish"] = lblTitle.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Public Notice -</b> " + hidden1.Value + "<br/> ");
                        p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                        petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        petObject.userID = Convert.ToInt32(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);

                    }
                }
                if (p_Var.Result > 0)
                {

                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    sbuilderSms.Remove(0, sbuilderSms.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
                           // p_Var.sbuildertmp.Append(li.Value + ";");
                            int statindex = li.Text.IndexOf("(") + 1;
                            p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_Var.sbuilder.Append("Record pending for Publish: " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }

                    
                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                        string[] stringSeparators = new string[] { "<br/>" };
                        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        ArrayList listPublicNotice = new ArrayList();
                        foreach (string itemPublicNotice in splitPublicNotice)
                        {
                            listPublicNotice.Add(itemPublicNotice.Trim());
                        }

                        foreach (string strPublicNotice in listPublicNotice)
                        {
                            if (strPublicNotice != string.Empty && strPublicNotice != "")
                            {
                                //loop through cells in that row
                                foreach (string str in list)
                                    {
                                        if (str != string.Empty)
                                        {

                                            //string message = ViewState["Title"].ToString();
                                            string message = strPublicNotice;

                                            if (message.Length > 150)
                                            {
                                                message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                            }
                                            else
                                            {
                                                message = strPublicNotice.ToString();
                                            }
                                            string textmessage = "HERC - Record pending for Publish -";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str,textmessage);

                                        }

                                    }
                                
                            }
                        }
                      
                    /* End */
                    Session["msg"] = "Public notice has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else //Here code is to approve records on date 12-05-2014
            {

                foreach (GridViewRow row in grdPublicNotice.Rows)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblTitle = (Label)row.FindControl("lblTitle");
                    HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                    ViewState["TitlePublish"] = lblTitle.Text;
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>Public Notice -</b> " + hidden1.Value + "<br/> ");
                        p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                        petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                        petObject.userID = Convert.ToInt32(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        //Codes for the sending emails

                        char[] splitter = { ';' };
                        DataSet dsMail = new DataSet();
                        DataSet dsSms = new DataSet();
                        dsSms = pubNoticeBL.getEmailIdForSendingSms(petObject);

                        dsMail = pubNoticeBL.getEmailIdForSendingMail(petObject);

                        //End
                        p_Var.Result = pubNoticeBL.PublicNoticeApprove(petObject);
                        /* Function to get email id of petitioners/respondents to send email*/
                        p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                        p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'><strong><strong>Subject:</strong></th><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                        p_Var.sbuildertmp.Append(dsMail.Tables[0].Rows[0]["title"].ToString());



                        p_Var.sbuildertmp.Append("</th></tr>");
                        p_Var.sbuildertmp.Append("</table>");

                        p_Var.sbuildertmp.Append("<br />");
                        p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                        p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                        p_Var.sbuildertmp.Append("query, you may contact Law Officer at 0172-2569602<br /><br />");
                        p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.<br />");

                        p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");
                        if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                        {
                            foreach (DataRow drRow in dsMail.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                if (strEmailHerc.StartsWith(";"))
                                {
                                    strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                }

                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim().Trim(), "HERC-Public Notice", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

                            }
                        }

                        /* End */

                        /* Code to send sms */

                        if (dsSms.Tables[0].Rows.Count > 0 && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != DBNull.Value && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != "")
                        {
                            foreach (DataRow drRow in dsSms.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                string strUrl = drRow["Petitioner_Mobile"].ToString();
                                string[] split = strUrl.Split(';');
                                ArrayList list = new ArrayList();
                                foreach (string item in split)
                                {
                                    list.Add(item.Trim());
                                }

                                foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {


                                        string message = dsSms.Tables[0].Rows[0]["Title"].ToString();
                                        if (message.Length > 150)
                                        {
                                            message = dsSms.Tables[0].Rows[0]["Title"].ToString().Substring(0, 150) + "..." + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                        }
                                        else
                                        {
                                            message = dsSms.Tables[0].Rows[0]["Title"].ToString() + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                        }

                                        string textmessage = "HERC Public Notice - ";

                                        miscellBL.SendsmsApprove(message, str, textmessage);


                                    }

                                }

                            }
                        }
                        /* End */
                    }
                }

                if (p_Var.Result > 0)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    sbuilderSms.Remove(0, sbuilderSms.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
                            //p_Var.sbuildertmp.Append(li.Value + ";");

                            int statindex = li.Text.IndexOf("(") + 1;
                            p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                            sbuilderSms.Append(li.Value + ";");
                        }

                    }
                    if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                    {
                        p_Var.sbuilder.Append("Record Published: " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                       miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string strUrl = sbuilderSms.ToString();
                    string[] split = strUrl.Split(';');
                    ArrayList list = new ArrayList();
                    foreach (string item in split)
                    {
                        list.Add(item.Trim());
                    }

                    
                        string strpublicnotice = sbuilder.ToString().Replace("<b>", "").Replace("</b>", "");
                        string[] stringSeparators = new string[] { "<br/>" };
                        string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        ArrayList listPublicNotice = new ArrayList();
                        foreach (string itemPublicNotice in splitPublicNotice)
                        {
                            listPublicNotice.Add(itemPublicNotice.Trim());
                        }

                        foreach (string strPublicNotice in listPublicNotice)
                        {
                            if (strPublicNotice != string.Empty && strPublicNotice != "")
                            {
                                //loop through cells in that row
                                foreach (string str in list)
                                    {
                                        if (str != string.Empty)
                                        {

                                            //string message = ViewState["Title"].ToString();
                                            string message = strPublicNotice;

                                            if (message.Length > 150)
                                            {
                                                message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                            }
                                            else
                                            {
                                                message = strPublicNotice.ToString();
                                            }
                                            string textmessage = "HERC - Record Published - ";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                        }

                                    }
                               
                            }
                        }
                       
                    /* End */

                    Session["msg"] = " Public notice has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSendEmailsWithoutEmails_Click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdPublicNotice.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");

                    if ((selCheck.Checked == true))
                    {

                        p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                        petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        petObject.userID = Convert.ToInt32(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);

                    }
                }
                if (p_Var.Result > 0)
                {

                    Session["msg"] = "Public notice has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }
            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
            {


                foreach (GridViewRow row in grdPublicNotice.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");

                    if ((selCheck.Checked == true))
                    {

                        p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                        petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        petObject.userID = Convert.ToInt32(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Public notice has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                foreach (GridViewRow row in grdPublicNotice.Rows)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {

                        p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                        petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                        petObject.userID = Convert.ToInt32(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        //Codes for the sending emails

                        char[] splitter = { ';' };
                        DataSet dsMail = new DataSet();
                        DataSet dsSms = new DataSet();
                        dsSms = pubNoticeBL.getEmailIdForSendingSms(petObject);

                        dsMail = pubNoticeBL.getEmailIdForSendingMail(petObject);

                        //End
                        p_Var.Result = pubNoticeBL.PublicNoticeApprove(petObject);
                        /* Function to get email id of petitioners/respondents to send email*/
                        p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                        p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'><strong><strong>Subject:</strong></th><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                        p_Var.sbuildertmp.Append(dsMail.Tables[0].Rows[0]["title"].ToString());



                        p_Var.sbuildertmp.Append("</th></tr>");
                        p_Var.sbuildertmp.Append("</table>");

                        p_Var.sbuildertmp.Append("<br />");
                        p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                        p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                        p_Var.sbuildertmp.Append("query, you may contact Law Officer at 0172-2569602<br /><br />");
                        p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.<br />");

                        p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");
                        if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                        {
                            foreach (DataRow drRow in dsMail.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                if (strEmailHerc.StartsWith(";"))
                                {
                                    strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                }

                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim().Trim(), "HERC-Public Notice", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

                            }
                        }

                        /* End */

                        /* Code to send sms */

                        if (dsSms.Tables[0].Rows.Count > 0 && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != DBNull.Value && dsSms.Tables[0].Rows[0]["Petitioner_Mobile"] != "")
                        {
                            foreach (DataRow drRow in dsSms.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                string strUrl = drRow["Petitioner_Mobile"].ToString();
                                string[] split = strUrl.Split(';');
                                ArrayList list = new ArrayList();
                                foreach (string item in split)
                                {
                                    list.Add(item.Trim());
                                }

                                foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {


                                        string message = dsSms.Tables[0].Rows[0]["Title"].ToString();
                                        if (message.Length > 150)
                                        {
                                            message = dsSms.Tables[0].Rows[0]["Title"].ToString().Substring(0, 150) + "..." + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                        }
                                        else
                                        {
                                            message = dsSms.Tables[0].Rows[0]["Title"].ToString() + Environment.NewLine + "For details and disclaimer visit  : http://herc.gov.in";
                                        }

                                        string textmessage = "HERC Public Notice - ";

                                        miscellBL.SendsmsApprove(message, str, textmessage);


                                    }

                                }

                            }
                        }
                        /* End */
                    }
                }

                if (p_Var.Result > 0)
                {
                    Session["msg"] = " Public notice has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnCancelEmail_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmails.Visible = false;
    }
    protected void btnSendEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            foreach (GridViewRow row in grdPublicNotice.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                Label lblTitle = (Label)row.FindControl("lblTitle");
                HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                ViewState["Titledis"] = lblTitle.Text;
                if ((selCheck.Checked == true))
                {
                    sbuilder.Append("<b>Public Notice -</b> " + hidden1.Value + "<br/> ");
                    p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                    petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);
                }
            }
            if (p_Var.Result > 0)
            {
                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
                {

                    if (li.Selected == true)
                    {
                       // p_Var.sbuildertmp.Append(li.Value + ";");
                        int statindex = li.Text.IndexOf("(") + 1;
                        p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                        sbuilderSms.Append(li.Value + ";");
                    }

                }
                if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                {
                    p_Var.sbuilder.Append("Record disapproved: " + sbuilder.ToString());
                    p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                    p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                    //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                    string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    p_Var.sbuildertmp.Append(email);
                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record disapproved", "no-reply.herc@nic.in" , p_Var.sbuilder.ToString());

                }
                ///* Code to send sms */
                char[] splitter = { ';' };
                PetitionOB petObjectNew = new PetitionOB();
                DataSet dsSms = new DataSet();
                string strUrl = sbuilderSms.ToString();
                string[] split = strUrl.Split(';');
                ArrayList list = new ArrayList();
                foreach (string item in split)
                {
                    list.Add(item.Trim());
                }

                
                    string strpublicnotice = sbuilder.ToString().Replace("<b>","").Replace("</b>","");
                    string[] stringSeparators = new string[] { "<br/>" };
                    string[] splitPublicNotice = strpublicnotice.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    ArrayList listPublicNotice = new ArrayList();
                    foreach (string itemPublicNotice in splitPublicNotice)
                    {
                        listPublicNotice.Add(itemPublicNotice.Trim());
                    }

                    foreach (string strPublicNotice in listPublicNotice)
                    {
                        if (strPublicNotice != string.Empty && strPublicNotice != "")
                        {
                            //loop through cells in that row
                            foreach (string str in list)
                                {
                                    if (str != string.Empty)
                                    {

                                        //string message = ViewState["Title"].ToString();
                                        string message = strPublicNotice;

                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        string textmessage = "HERC - Record disapproved - ";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }
                           
                        }
                    }
                    /* End */
                Session["msg"] = "Public notice has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }

        }
        catch
        {
            throw;
        }
    }
    protected void btnSendEmailsWithoutEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdPublicNotice.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    //Label lblid = (Label)row.FindControl("lblRTI_ID");
                    p_Var.dataKeyID = Convert.ToInt32(grdPublicNotice.DataKeys[row.RowIndex].Value);
                    petObject.TmpPublicNoticeID = p_Var.dataKeyID;
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_Var.Result = pubNoticeBL.PublicNoticeUpdateStatus(petObject);
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Public notice has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Public notice has not been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/PublicNotice/PublicNoticeDisplay.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnCancelEmailDis_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmailsDis.Visible = false;
    }

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDs()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Public_Notice);
            p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectors.Text = "Select the Reviewer(s)";
            lblSelectors.Font.Bold = true;
            chkSendEmails.DataSource = p_Var.dSetCompare;
            chkSendEmails.DataTextField = "newEmail";
            chkSendEmails.DataValueField = "Mobile_No";
            chkSendEmails.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithApproverEmailIDs()
    {

        try
        {
            lblSelectors.Text = "Select the Publisher(s)";
            lblSelectors.Font.Bold = true;
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Public_Notice);
            p_Var.dSetCompare = obj_UserBL.getPublisherEmailIds(obj_userOB);

            chkSendEmails.DataSource = p_Var.dSetCompare;
            chkSendEmails.DataTextField = "newEmail";
            chkSendEmails.DataValueField = "Mobile_No";
            chkSendEmails.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDDiaapproves()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Public_Notice);
            p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
            lblSelectorsDis.Text = "Select the Reviewer(s)";
            lblSelectorsDis.Font.Bold = true;
            chkSendEmailsDis.DataSource = p_Var.dSetCompare;
            chkSendEmailsDis.DataTextField = "newEmail";
            chkSendEmailsDis.DataValueField = "Mobile_No";
            chkSendEmailsDis.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind emailid of Data entry operator status in checkboxlist

    public void bindCheckBoxListWithDataEntryEmailIDs()
    {

        try
        {
            lblSelectorsDis.Text = "Select the Data Entry Personnel";
            lblSelectorsDis.Font.Bold = true;
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Public_Notice);
            p_Var.dSetCompare = obj_UserBL.getDataEntryEmailIds(obj_userOB);

            chkSendEmailsDis.DataSource = p_Var.dSetCompare;
            chkSendEmailsDis.DataTextField = "newEmail";
            chkSendEmailsDis.DataValueField = "Mobile_No";
            chkSendEmailsDis.DataBind();

        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    public static string GetResponse(string sURL)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
        request.MaximumAutomaticRedirections = 4;
        request.Credentials = CredentialCache.DefaultCredentials;
        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string sResponse = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            return sResponse;
        }
        catch
        {
            return "";
        }
    } 




    

}
