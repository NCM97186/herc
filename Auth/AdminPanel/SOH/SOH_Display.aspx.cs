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
using System.Net;
using System.Text;


public partial class Auth_AdminPanel_SOH_SOH_Display : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration

    #region Data declaration zone

    Project_Variables p_Var = new Project_Variables();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    PaginationBL pagingBL = new PaginationBL();
    LinkBL obj_LinkBL = new LinkBL();
    UserBL obj_UserBL = new UserBL();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    //Area for the page load event 

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("WhatsNewStatus"); // What's New sessions
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

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  Schedule Of Hearing";
        this.Page.Title = " Schedule Of Hearing: HERC";

        if (!IsPostBack)
        {

            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            ViewState["sortOrder"] = "";
            ViewState["sortOrder1"] = "";
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddScheduleOfHearing.Visible = false;
            btnForReview.Visible = false;
            //PLanguage.Visible = false;
            //rptPager.Visible = false;
            if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH))
            {
                Get_Deptt_Name();
            }
            if (Session["Sohdeptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["Sohdeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }


            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {
                btnForReview.Attributes.Add("onclick", "javascript:return validatecheckbox();");
                btnForApprove.Attributes.Add("onclick", "javascript:return validatecheckbox();");
                btnApprove.Attributes.Add("onclick", "javascript:return validatecheckbox();");
                btnDisApprove.Attributes.Add("onclick", "javascript:return validatecheckbox();");
            }
            else
            {
                btnForReview.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
                btnForApprove.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
                btnApprove.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
                btnDisApprove.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
            }
            // This is commented on date 26 DEp 2013 by ruchi

            if (ddlDepartment.SelectedValue == "1")
            {
                Ppetitionappeal.Visible = true;
                PApeal.Visible = false;


            }
            else
            {
                PApeal.Visible = true;

                Ppetitionappeal.Visible = false;
            }

            //End

            if (Session["SohLng"] != null)
            {
                bindropDownlistLang();
                // ddlLanguage.SelectedValue = Session["SohLng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }

            if (Session["SohYear"] != null)
            {
                bindScheduleOfHearingYearinDdl(1);
                ddlYear.SelectedValue = Session["SohYear"].ToString();
            }
            else
            {
                bindScheduleOfHearingYearinDdl(1);

            }
            if (Session["SohAppeal"] != null)
            {
                ddlappeal.SelectedValue = Session["SohAppeal"].ToString();
                Ppetitionappeal.Visible = false;
                PApeal.Visible = true;
            }
            if (Session["appealYear1"] != null)
            {
                bindScheduleOfHearingYearinDdlForAppeal();
                ddlappealYear.SelectedValue = Session["appealYear1"].ToString();
            }
            else
            {
                bindScheduleOfHearingYearinDdlForAppeal();
            }
            if (Session["SohPetitionAppeal"] != null)
            {
                ddlpetitionappeal.SelectedValue = Session["SohPetitionAppeal"].ToString();
                Ppetitionappeal.Visible = true;
                PApeal.Visible = false;
            }
            if (Session["SohStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["SohStatus"].ToString();
                if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                {
                    Bind_Grid("", "");
                }
                else
                {
                    BindAppeal_Grid("", "");
                }
            }
            else
            {
                binddropDownlistStatus();
            }

            lblmsg.Visible = false;
        }
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddScheduleOfHearing click event to add new schedule of hearing

    protected void btnAddScheduleOfHearing_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/SOH/SOH_Add_Edit.aspx?ModuleID=" + Convert.ToInt16(Request.QueryString["ModuleID"])));
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

    #region button btnForApprove click event to send record for approve

    protected void btnForApprove_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();

    }

    #endregion

    #region button btnApprove click event to approve  the record

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
        //ApproveRecords();
    }

    #endregion

    #region button btnDisapprove click event to disapprove record

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

    }

    #endregion

    #region button btnDelete click event to delete petition

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt16(p_Var.commandArgs[1]);

        petObject.RPId = p_Var.rp_id;
        petObject.StatusId = p_Var.status_id;

        p_Var.Result = petPetitionBL.Delete_Petition(petObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "Schedule Of Hearing has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "Schedule Of Hearing has been deleted successfully.";
            }

            Session["msg"] = "Schedule Of Hearing has been deleted successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }

    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), pageIndex);
        Bind_Grid("", "");
    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLanguage.SelectedValue != "0")
    //    {
    //        binddropDownlistStatus();
    //        grdScheduleOfHearing.Visible = false;
    //        grdAppealSoh.Visible = false;
    //        Session["SohLng"] = ddlLanguage.SelectedValue;
    //    }
    //}

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event zone

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdScheduleOfHearing.Visible = false;
            grdAppealSoh.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else if (ddlStatus.SelectedValue == "100")
        {

            //  Bind_Grid("", "");



            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {
                Bind_Grid("", "");
            }
            else
            {
                BindAppeal_Grid("", "");
            }
        }
        else
        {
            if (ddlStatus.SelectedValue.ToString() == (Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft)).ToString())
            {
                // GvAdd_Details.Columns[0].Visible = false;
                // BtnForReview.Visible = true;
            }
            else
            {
                //  GvAdd_Details.Columns[0].Visible = true;

            }
            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {
                Bind_Grid("", "");
            }
            else
            {
                BindAppeal_Grid("", "");
            }
        }
        Session["SohStatus"] = ddlStatus.SelectedValue;
        if (ddlDepartment.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event zone

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid("", "");
    }

    #endregion

    //End

    //Area for all the gridView events

    #region gridView grdScheduleOfHearing rowCommand event zone

    protected void grdScheduleOfHearing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        string current = Convert.ToString(Session["CurrentRequestUrl"]);
        current = current.Replace("%20", "+");
        string httpref = Convert.ToString(Request.ServerVariables["HTTP_REFERER"]);
        httpref = httpref.Replace("%20", "+");

        if (current != null && current.Equals(httpref))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (e.CommandName == "delete")
                {

                    p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
                    ViewState["commandArgs"] = p_Var.commandArgs;
                    p_Var.petition_id = Convert.ToInt32(p_Var.commandArgs[0]);
                    p_Var.status_id = Convert.ToInt16(p_Var.commandArgs[1]);


                }

                else if (e.CommandName == "Restore")
                {
                    GridViewRow row1 = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    petObject.soh_ID = Convert.ToInt32(e.CommandArgument);


                    p_Var.Result = petPetitionBL.SOH_Changestatus_Delete(petObject);
                    if (p_Var.Result > 0)
                    {
                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        Label lbldated = row1.FindControl("lbldated") as Label;
                        string department = "Department: " + ddlDepartment.SelectedItem.ToString();
                        HiddenField HiddenField1 = row1.FindControl("HiddenField1") as HiddenField;
                        string subject = HiddenField1.Value;
                        obj_audit.Title = department + ", " + "Date & Time: " + lbldated.Text + ", " + "Subject: " + subject;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Schedule Of Hearing has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);

                    }
                    else
                    {
                        Session["msg"] = "Schedule Of Hearing has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);

                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    petObject.ModuleType = null;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblSubject") as Label;
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
        }
    }
    #endregion

    #region gridView grdScheduleOfHearing rowCreated event

    protected void grdScheduleOfHearing_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdScheduleOfHearing.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));
        }
    }

    #endregion

    #region gridView grdScheduleOfHearing rowDataBound event

    protected void grdScheduleOfHearing_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsubject = (Label)e.Row.FindControl("lblSubject");
            Label lblsubjectsoh = (Label)e.Row.FindControl("lblSubjectSoh");
            if (lblsubject.Text == "" && lblsubject.Text == null)
            {
                lblsubject.Visible = false;
            }
            else if (lblsubjectsoh.Text == "" && lblsubjectsoh.Text == null)
            {
                lblsubjectsoh.Visible = false;
            }


            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            HiddenField hydelete = (HiddenField)e.Row.FindControl("hydelete");
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                if (hydelete.Value != null && hydelete.Value != "")
                {
                    if (ddlpetitionappeal.SelectedValue == "1")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure you want to purge (delete permanently) Schedule of Hearing against PRO No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");

                        BtnRestore.Attributes.Add("onclick", "javascript:return " +
                      "confirm('Are you sure you want to Restore Schedule of Hearing against PRO No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                    else if (ddlpetitionappeal.SelectedValue == "2")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                          "confirm('Are you sure you want to purge (delete permanently) Schedule of Hearing against RA No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");

                        BtnRestore.Attributes.Add("onclick", "javascript:return " +
                      "confirm('Are you sure you want to Restore Schedule of Hearing against RA No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }

                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to purge this Schedule of Hearing ? " + "')");

                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure want to Restore this Schedule of Hearing ? " + "')");
                }

            }
            else
            {
                if (hydelete.Value != null && hydelete.Value != "")
                {
                    if (ddlpetitionappeal.SelectedValue == "1")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure you want to delete Schedule of Hearing against PRO No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                    else if (ddlpetitionappeal.SelectedValue == "2")
                    {
                        BtnDelete.Attributes.Add("onclick", "javascript:return " +
                        "confirm('Are you sure you want to delete Schedule of Hearing against RA No- " + DataBinder.Eval(e.Row.DataItem, "PRONo") + " ?')");
                    }
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                                        "confirm('Are you sure you want to delete this Schedule Of Hearing - " + lblsubject.Text + " ?')");
                }

            }

            //END
        }
    }

    #endregion

    #region gridView grdScheduleOfHearing rowDeleting event

    protected void grdScheduleOfHearing_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row1 = (GridViewRow)grdScheduleOfHearing.Rows[e.RowIndex];
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt16(p_Var.commandArgs[1]);

        ////petObject.RPId = p_Var.rp_id;
        petObject.SohTempID = p_Var.rp_id;
        petObject.StatusId = p_Var.status_id;

        p_Var.Result = petPetitionBL.Delete_ScheduleOfHearing(petObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "Schedule of Hearing has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "Schedule of Hearing has been deleted successfully.";
            }

            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.status = ddlStatus.SelectedItem.ToString();
            Label lbldated = row1.FindControl("lbldated") as Label;
            string department = "Department: " + ddlDepartment.SelectedItem.ToString();
            HiddenField HiddenField1 = row1.FindControl("HiddenField1") as HiddenField;
            string subject = HiddenField1.Value;
            obj_audit.Title = department + ", " + "Date & Time: " + lbldated.Text + ", " + "Subject: " + subject;
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


            Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
        else
        {
            Session["msg"] = "Schedule of Hearing has not been deleted successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
    }

    #endregion

    //End

    //Area for all the user-defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {
            //Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            //Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            //obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            //obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            //p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
            //if (p_Var.dSet.Tables[0].Rows.Count > 0)
            //{
            //    UserOB usrObject = new UserOB();
            //    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true && Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
            //    {
            //        usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);
            //        usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

            //        p_Var.sbuilder.Append(usrObject.english).Append(",");
            //        p_Var.sbuilder.Append(usrObject.hindi);
            //    }
            //    else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
            //    {
            //        usrObject.hindi = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Hindi);

            //        p_Var.sbuilder.Append(usrObject.hindi);
            //    }
            //    else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
            //    {
            //        usrObject.english = Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.English);

            //        p_Var.sbuilder.Append(usrObject.english);
            //    }
            //    usrObject.LangId = p_Var.sbuilder.ToString().Trim();
            //    p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            //    p_Var.dSet = null;
            //    p_Var.dSet = miscdlLanguage.getLanguage(usrObject);
            //    PLanguage.Visible = true;
            //    ddlLanguage.DataSource = p_Var.dSet;
            //    ddlLanguage.DataTextField = "Language";
            //    ddlLanguage.DataValueField = "Lang_Id";
            //    ddlLanguage.DataBind();
            //}
            //p_Var.dSet = null;
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
                btnAddScheduleOfHearing.Visible = true;

                //code written on date 21 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddScheduleOfHearing.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
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
            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
            //  ddlStatus.Items.Add(new ListItem("Expiry Data", "100"));
            btnForReview.Visible = false;
        }
        p_Var.dSet = null;
    }

    #endregion

    #region Function to bind gridView with schdule of hearing

    public void Bind_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        grdAppealSoh.Visible = false;
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdScheduleOfHearing.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdScheduleOfHearing.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
            petObject.year = ddlYear.SelectedValue.ToString();
            petObject.AppealStatusId = Convert.ToInt16(ddlpetitionappeal.SelectedValue);
            if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH))
            {
                petObject.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            else
            {
                petObject.DepttId = null;
            }
            if (ddlDepartment.SelectedValue == "1")
            {
                petObject.keyword = "1";
            }
            else if (ddlDepartment.SelectedValue == "2")
            {
                petObject.keyword = "2";
            }
            p_Var.dSet = pet_TempRecordBL.getPetition_ScheduleOfHearing(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdScheduleOfHearing.Columns[11].HeaderText = "Purge";
                }
                else
                {
                    grdScheduleOfHearing.Columns[11].HeaderText = "Delete";
                }
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End
                grdScheduleOfHearing.DataSource = myDataView;
                if (ddlpetitionappeal.SelectedValue == "1")
                {
                    grdScheduleOfHearing.Columns[4].HeaderText = "PRO No";
                    grdScheduleOfHearing.Columns[4].Visible = true;
                }
                else if (ddlpetitionappeal.SelectedValue == "2")
                {
                    grdScheduleOfHearing.Columns[4].HeaderText = "RA No";
                    grdScheduleOfHearing.Columns[4].Visible = true;
                }
                else
                {
                    grdScheduleOfHearing.Columns[4].Visible = false;
                }
                grdScheduleOfHearing.DataBind();
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
                        grdScheduleOfHearing.Columns[0].Visible = false;
                        //p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare();

                        foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                        {

                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblScheduleOfHearingID = (Label)row.FindControl("lblScheduleOfHearingID");

                            petObject.soh_ID = Convert.ToInt32(lblScheduleOfHearingID.Text);

                            p_Var.dSetCompare = pet_TempRecordBL.scheduleOfHearingIDForComparison(petObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Soh_ID"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblScheduleOfHearingID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Soh_ID"]))
                                    {
                                        imgnotedit.Visible = true;
                                        imgedit.Visible = false;

                                    }
                                    else
                                    {
                                        imgnotedit.Visible = false;
                                        imgedit.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        grdScheduleOfHearing.Columns[0].Visible = true;
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
                            grdScheduleOfHearing.Columns[10].Visible = false;
                            grdScheduleOfHearing.Columns[12].Visible = true;
                            grdScheduleOfHearing.Columns[13].Visible = false;

                        }
                        else
                        {
                            grdScheduleOfHearing.Columns[10].Visible = true;
                            grdScheduleOfHearing.Columns[12].Visible = false;
                        }


                    }

                    else
                    {
                        grdScheduleOfHearing.Columns[10].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == 100)
                        {
                            grdScheduleOfHearing.Columns[11].Visible = false;
                        }
                        else
                        {
                            grdScheduleOfHearing.Columns[11].Visible = true;
                        }

                        // modify on date 21 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdScheduleOfHearing.Columns[11].Visible = true;
                                grdScheduleOfHearing.Columns[13].Visible = false;
                            }
                            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                            {
                                if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdScheduleOfHearing.Columns[13].Visible = true;
                                }
                                else
                                {
                                    grdScheduleOfHearing.Columns[13].Visible = false;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdScheduleOfHearing.Columns[11].Visible = true;

                                    grdScheduleOfHearing.Columns[13].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdScheduleOfHearing.Columns[11].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdScheduleOfHearing.Columns[13].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdScheduleOfHearing.Columns[13].Visible = true;
                                            }
                                            else
                                            {
                                                grdScheduleOfHearing.Columns[13].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdScheduleOfHearing.Columns[13].Visible = false;
                                        grdScheduleOfHearing.Columns[11].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdScheduleOfHearing.Columns[11].Visible = true;
                                grdScheduleOfHearing.Columns[13].Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdScheduleOfHearing.Columns[11].Visible = true;
                            }

                        }

                        //End    
                    }
                    else
                    {
                        grdScheduleOfHearing.Columns[11].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                grdScheduleOfHearing.Visible = false;
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
        Session["Lanuage"] = Convert.ToInt16(Module_ID_Enum.Language_ID.English);
        Session["DepttId"] = ddlDepartment.SelectedValue;
        // Session["priv"] = p_Var.dSet;     //session hold the dsprv values  
    }

    #endregion



    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            PDepartment.Visible = true;
            obj_userOB.ModuleId = Convert.ToInt16(Request.QueryString["ModuleID"]);
            obj_userOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            p_Var.dSet = obj_LinkBL.ASP_Get_Deptt_Name(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = p_Var.dSet;
                ddlDepartment.DataValueField = "Deptt_Id";
                ddlDepartment.DataTextField = "Deptt_Name";
                ddlDepartment.DataBind();
            }
            //ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindropDownlistLang();
        binddropDownlistStatus();
        grdAppealSoh.Visible = false;
        grdScheduleOfHearing.Visible = false;
        lblmsg.Visible = false;
        btnForReview.Visible = false;
        btnForApprove.Visible = false;
        btnDisApprove.Visible = false;
        btnApprove.Visible = false;
        //rptPager.Visible = false;
        if (ddlDepartment.SelectedValue == "1")
        {


            btnForReview.Attributes.Add("onclick", "javascript:return validatecheckbox();");
            btnForApprove.Attributes.Add("onclick", "javascript:return validatecheckbox();");
            btnApprove.Attributes.Add("onclick", "javascript:return validatecheckbox();");
            btnDisApprove.Attributes.Add("onclick", "javascript:return validatecheckbox();");



            Ppetitionappeal.Visible = true;
            PApeal.Visible = false;
            Session["SohPetitionAppeal"] = ddlpetitionappeal.SelectedValue;
            Session.Remove("SohAppeal");

        }
        else
        {
            btnForReview.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
            btnForApprove.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
            btnApprove.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
            btnDisApprove.Attributes.Add("onclick", "javascript:return validatecheckboxAppeal();");
            PApeal.Visible = true;
            bindScheduleOfHearingYearinDdlForAppeal();
            Ppetitionappeal.Visible = false;
            Session.Remove("Sohdeptt");
            Session.Remove("SohLng");
            Session.Remove("SohYear");
            Session.Remove("SohStatus");
            Session["SohAppeal"] = ddlappeal.SelectedValue;
            Session.Remove("SohPetitionAppeal");
        }
        if (ddlDepartment.SelectedValue == "0" || ddlpetitionappeal.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
        Session["Sohdeptt"] = ddlDepartment.SelectedValue;
    }
    protected void grdAppealSoh_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "delete")
        {
            GridViewRow row1 = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
            ViewState["commandArgs"] = p_Var.commandArgs;
            p_Var.petition_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt16(p_Var.commandArgs[1]);


        }

        else if (e.CommandName == "Restore")
        {
            GridViewRow row1 = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            petObject.soh_ID = Convert.ToInt32(e.CommandArgument);
            p_Var.Result = petPetitionBL.SOH_Changestatus_Delete(petObject);
            if (p_Var.Result > 0)
            {
                obj_audit.ActionType = "R";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.status = ddlStatus.SelectedItem.ToString();
                obj_audit.IpAddress = miscellBL.IpAddress();
                Label lbldated1 = row1.FindControl("lbldated1") as Label;
                string department = "Department: " + ddlDepartment.SelectedItem.ToString();
                HiddenField HiddenField1 = row1.FindControl("HiddenField1") as HiddenField;
                string subject = HiddenField1.Value;
                obj_audit.Title = department + ", " + "Date & Time: " + lbldated1.Text + ", " + "Subject: " + subject;
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                Session["msg"] = "Schedule Of Hearing has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);

            }
            else
            {
                Session["msg"] = "Schedule Of Hearing has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);

            }
        }

        else if (e.CommandName == "Audit")
        {

            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH);
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            petObject.ModuleType = null;
            dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
            Label lblprono = row.FindControl("lblSubject") as Label;
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
            }
            this.mdpAuditTrail.Show();
        }

    }
    protected void grdAppealSoh_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdAppealSoh.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClickAppeal(this,'{0}');", chkBxHeader.ClientID));
        }
    }
    protected void grdAppealSoh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsubject = (Label)e.Row.FindControl("lblSubject");
            Label lblsubjectsoh = (Label)e.Row.FindControl("lblSubjectSoh");
            if (lblsubject.Text == "" && lblsubject.Text == null)
            {
                lblsubject.Visible = false;
            }
            else if (lblsubjectsoh.Text == "" && lblsubjectsoh.Text == null)
            {
                lblsubjectsoh.Visible = false;
            }


            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            HiddenField hydeleteAppeal = (HiddenField)e.Row.FindControl("hydeleteAppeal");
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                if (hydeleteAppeal.Value != null && hydeleteAppeal.Value != "")
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to purge Schedule Of Hearing against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure want to restore Schedule Of Hearing against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to purge this Schedule Of Hearing ? " + "')");
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are you sure want to restore this Schedule Of Hearing ?')");
                }

            }
            else
            {
                if (hydeleteAppeal.Value != null && hydeleteAppeal.Value != "")
                {

                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete Schedule Of Hearing against Appeal No- " + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                                        "confirm('Are you sure you want to delete this Schedule Of Hearing ? " + "')");
                }

            }

            //END
        }
    }
    protected void grdAppealSoh_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row1 = (GridViewRow)grdAppealSoh.Rows[e.RowIndex];
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt16(p_Var.commandArgs[1]);

        //petObject.RPId = p_Var.rp_id;
        petObject.SohTempID = p_Var.rp_id;
        petObject.StatusId = p_Var.status_id;

        p_Var.Result = petPetitionBL.Delete_ScheduleOfHearing(petObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "Schedule Of Hearing has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "Schedule Of Hearing has been deleted successfully.";
            }

            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.status = ddlStatus.SelectedItem.ToString();
            Label lbldated = row1.FindControl("lbldated1") as Label;
            string department = "Department: " + ddlDepartment.SelectedItem.ToString();
            HiddenField HiddenField1 = row1.FindControl("HiddenField1") as HiddenField;
            string subject = HiddenField1.Value;
            obj_audit.Title = department + ", " + "Date & Time: " + lbldated.Text + ", " + "Subject: " + subject;
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

            Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
        else
        {
            Session["msg"] = "Schedule Of Hearing has not been deleted successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/SOH/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
    }

    #region Function to bind appeal in grid

    public void BindAppeal_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        grdScheduleOfHearing.Visible = false;
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppealSoh.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdAppealSoh.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
            petObject.year = ddlappealYear.SelectedValue.ToString();
            // petObject.PageIndex = pageIndex;
            // petObject.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            petObject.keyword = ddlappeal.SelectedValue.ToString();
            if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH))
            {
                petObject.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            else
            {
                petObject.DepttId = null;
            }
            p_Var.dSet = pet_TempRecordBL.getAppeal_ScheduleOfHearing(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdAppealSoh.Columns[11].HeaderText = "Purge";
                }
                else
                {
                    grdAppealSoh.Columns[11].HeaderText = "Delete";
                }
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End
                grdAppealSoh.DataSource = myDataView;
                grdAppealSoh.DataBind();
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
                        grdAppealSoh.Columns[0].Visible = false;
                        //p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare();

                        foreach (GridViewRow row in grdAppealSoh.Rows)
                        {

                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblScheduleOfHearingID = (Label)row.FindControl("lblScheduleOfHearingID");

                            petObject.soh_ID = Convert.ToInt32(lblScheduleOfHearingID.Text);

                            p_Var.dSetCompare = pet_TempRecordBL.scheduleOfHearingIDForComparison(petObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Soh_ID"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblScheduleOfHearingID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Soh_ID"]))
                                    {
                                        imgnotedit.Visible = true;
                                        imgedit.Visible = false;

                                    }
                                    else
                                    {
                                        imgnotedit.Visible = false;
                                        imgedit.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        grdAppealSoh.Columns[0].Visible = true;
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
                            grdAppealSoh.Columns[10].Visible = false;
                            grdAppealSoh.Columns[12].Visible = true;
                            grdAppealSoh.Columns[13].Visible = false;
                        }
                        else
                        {
                            grdAppealSoh.Columns[10].Visible = true;
                            grdAppealSoh.Columns[12].Visible = false;
                        }



                    }
                    else
                    {
                        grdAppealSoh.Columns[10].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == 100)
                        {
                            grdAppealSoh.Columns[11].Visible = false;
                        }
                        else
                        {
                            // modify on date 21 Sep 2013 by ruchi

                            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                                {
                                    grdAppealSoh.Columns[11].Visible = true;
                                    grdAppealSoh.Columns[13].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                    {
                                        grdAppealSoh.Columns[11].Visible = true;
                                        grdAppealSoh.Columns[13].Visible = false;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                        {
                                            grdAppealSoh.Columns[11].Visible = true;
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                            {
                                                grdAppealSoh.Columns[13].Visible = true;
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                                {
                                                    grdAppealSoh.Columns[13].Visible = true;
                                                }
                                                else
                                                {
                                                    grdAppealSoh.Columns[13].Visible = false;
                                                }
                                                // grdAppealSoh.Columns[13].Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            grdAppealSoh.Columns[11].Visible = false;
                                        }
                                    }
                                }
                            }
                            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                                {
                                    grdAppealSoh.Columns[11].Visible = true;
                                    grdAppealSoh.Columns[13].Visible = false;
                                }

                            }
                            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                {
                                    grdAppealSoh.Columns[11].Visible = true;
                                }

                            }

                            //End   
                            ////grdAppealSoh.Columns[11].Visible = true;
                        }
                    }
                    else
                    {
                        grdAppealSoh.Columns[11].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                grdAppealSoh.Visible = false;
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
        Session["Lanuage"] = Convert.ToInt16(Module_ID_Enum.Language_ID.English);
        // Session["priv"] = p_Var.dSet;     //session hold the dsprv values  
    }

    #endregion

    //Codes for sorting of the grid

    protected void grdScheduleOfHearing_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, sortOrder);
    }

    #region gridView grdScheduleOfHearing pageIndexChanging Event zone

    protected void grdScheduleOfHearing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdScheduleOfHearing.PageIndex = e.NewPageIndex;
        Bind_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    //End


    #region gridView grdAppeal pageIndexChanging Event zone

    protected void grdAppealSoh_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAppealSoh.PageIndex = e.NewPageIndex;
        BindAppeal_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    protected void ddlpetitionappeal_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        bindScheduleOfHearingYearinDdl(Convert.ToInt16(ddlpetitionappeal.SelectedValue));
        grdScheduleOfHearing.Visible = false;
        grdAppealSoh.Visible = false;

        btnForReview.Visible = false;
        btnForApprove.Visible = false;
        btnDisApprove.Visible = false;
        btnApprove.Visible = false;


        Session["SohPetitionAppeal"] = ddlpetitionappeal.SelectedValue;

        if (ddlDepartment.SelectedValue == "0" || ddlpetitionappeal.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }
    protected void ddlappeal_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();

        grdScheduleOfHearing.Visible = false;
        grdAppealSoh.Visible = false;

        btnForReview.Visible = false;
        btnForApprove.Visible = false;
        btnDisApprove.Visible = false;
        btnApprove.Visible = false;

        Session["SohAppeal"] = ddlappeal.SelectedValue;
        if (ddlDepartment.SelectedValue == "0" || ddlpetitionappeal.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

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

    public string sortOrder1
    {
        get
        {
            if (ViewState["sortOrder1"].ToString() == "desc")
            {
                ViewState["sortOrder1"] = "asc";
            }
            else
            {
                ViewState["sortOrder1"] = "desc";
            }

            return ViewState["sortOrder1"].ToString();
        }
        set
        {
            ViewState["sortOrder1"] = value;
        }
    }

    //End

    protected void grdAppealSoh_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindAppeal_Grid(e.SortExpression, sortOrder1);
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdScheduleOfHearing.Visible = false;
        grdAppealSoh.Visible = false;

        btnForReview.Visible = false;
        btnForApprove.Visible = false;
        btnDisApprove.Visible = false;
        btnApprove.Visible = false;

        Session["SohYear"] = ddlYear.SelectedValue;
        if (ddlDepartment.SelectedValue == "0" || ddlpetitionappeal.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #region Function to bind schedule of Hearing Year

    public void bindScheduleOfHearingYearinDdl(int statusid)
    {
        petObject.AppealStatusId = statusid;
        p_Var.dSet = petPetitionBL.GetYearScheduleOfHearing_Admin(petObject);
        ddlYear.Items.Clear();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
        else
        {
            ddlYear.Items.Add(new ListItem("Select", "0"));
        }
    }

    #endregion

    #region Function to bind schedule of Hearing Year for appeal section

    public void bindScheduleOfHearingYearinDdlForAppeal()
    {

        p_Var.dSet = petPetitionBL.GetYearScheduleOfHearingforAppeal_Admin();

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlappealYear.DataSource = p_Var.dSet;
            ddlappealYear.DataTextField = "year";
            ddlappealYear.DataValueField = "year";
            ddlappealYear.DataBind();
        }
        else
        {
            ddlappealYear.Items.Add(new ListItem("Select", "0"));
        }
    }

    #endregion

    protected void ddlappealYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdScheduleOfHearing.Visible = false;
        grdAppealSoh.Visible = false;

        btnForReview.Visible = false;
        btnForApprove.Visible = false;
        btnDisApprove.Visible = false;
        btnApprove.Visible = false;

        Session["appealYear1"] = ddlappealYear.SelectedValue;
        if (ddlDepartment.SelectedValue == "0" || ddlpetitionappeal.SelectedValue == "0" || ddlappealYear.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue == "1")
        {
            BindGridDetails();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ScheduleOfHearingHERC_" + System.DateTime.Now.ToShortDateString() + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter ht = new HtmlTextWriter(sw);
            grdScheduleOfHearingPdf.AllowPaging = false;
            grdScheduleOfHearingPdf.DataBind();
            grdScheduleOfHearingPdf.RenderControl(ht);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            grdAppealBindGrid();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ScheduleOfHearingOmbudsman_" + System.DateTime.Now.ToShortDateString() + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter ht = new HtmlTextWriter(sw);
            grdAppealPfd.AllowPaging = false;
            grdAppealPfd.DataBind();
            grdAppealPfd.RenderControl(ht);
            Response.Write(sw.ToString());
            Response.End();
        }

    }

    public void BindGridDetails()
    {
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdScheduleOfHearingPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdScheduleOfHearingPdf.Visible = true;

            grdScheduleOfHearing.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
            petObject.year = ddlYear.SelectedValue.ToString();
            petObject.AppealStatusId = Convert.ToInt32(ddlpetitionappeal.SelectedValue);
            if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH))
            {
                petObject.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            else
            {
                petObject.DepttId = null;
            }
            if (ddlDepartment.SelectedValue == "1")
            {
                petObject.keyword = "1";
            }
            else if (ddlDepartment.SelectedValue == "2")
            {
                petObject.keyword = "2";
            }
            p_Var.dSet = pet_TempRecordBL.getPetition_ScheduleOfHearing(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {


                grdScheduleOfHearingPdf.DataSource = p_Var.dSet;

                grdScheduleOfHearingPdf.DataBind();
                p_Var.dSet = null;

            }

        }
    }


    public void grdAppealBindGrid()
    {


        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppealPfd.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdAppealPfd.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
            petObject.year = ddlappealYear.SelectedValue.ToString();

            petObject.keyword = ddlappeal.SelectedValue.ToString();
            if (Convert.ToInt16(Request.QueryString["ModuleID"]) == Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH))
            {
                petObject.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            }
            else
            {
                petObject.DepttId = null;
            }
            p_Var.dSet = pet_TempRecordBL.getAppeal_ScheduleOfHearing(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                grdAppealPfd.DataSource = p_Var.dSet;
                grdAppealPfd.DataBind();
                p_Var.dSet = null;

                Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
                Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);


            }

        }

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void grdAppealPfd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            petObject.soh_ID = Convert.ToInt32(grdAppealPfd.DataKeys[e.Row.RowIndex].Value.ToString());
            Literal ltrlConnectedFileSOHDetails = (Literal)e.Row.FindControl("ltrlConnectedFileSOHDetails");
            p_Var.dSetCompare = petPetitionBL.getSohFileNames(petObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {


                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] + ", ");
                    }

                    p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]);

                    p_Var.sbuilder.Append("<br/><hr/>");

                }

                ltrlConnectedFileSOHDetails.Text = p_Var.sbuilder.ToString();

            }
            Label lblSubjectSoh = (Label)e.Row.FindControl("lblSubjectSoh");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");

            lblSubjectSoh.Text = HttpUtility.HtmlDecode(lblSubjectSoh.Text);
            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

        }
    }

    protected void grdScheduleOfHearingPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            petObject.soh_ID = Convert.ToInt32(grdScheduleOfHearingPdf.DataKeys[e.Row.RowIndex].Value.ToString());
            Literal ltrlConnectedFileSOHDetails = (Literal)e.Row.FindControl("ltrlConnectedFileSOHDetails");
            p_Var.dSetCompare = petPetitionBL.getSohFileNames(petObject);
            if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
            {


                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                for (int i = 0; i < p_Var.dSetCompare.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] != DBNull.Value && p_Var.dSetCompare.Tables[0].Rows[i]["Comments"].ToString() != "")
                    {
                        p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["Comments"] + ", ");
                    }

                    p_Var.sbuilder.Append(p_Var.dSetCompare.Tables[0].Rows[i]["FileName"]);

                    p_Var.sbuilder.Append("<br/><hr/>");

                }

                ltrlConnectedFileSOHDetails.Text = p_Var.sbuilder.ToString();

            }
            Label lblSubjectSoh = (Label)e.Row.FindControl("lblSubjectSoh");
            Label lblSubject = (Label)e.Row.FindControl("lblSubject");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");

            lblSubjectSoh.Text = HttpUtility.HtmlDecode(lblSubjectSoh.Text);
            lblSubject.Text = HttpUtility.HtmlDecode(lblSubject.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

        }
    }

    public static void BypassCertificateError()
    {

        ServicePointManager.ServerCertificateValidationCallback +=



          delegate(object sender,

               System.Security.Cryptography.X509Certificates.X509Certificate certificate,

               System.Security.Cryptography.X509Certificates.X509Chain chain,

                System.Net.Security.SslPolicyErrors sslPolicyErrors)
          {

              return true;

          };

    }
    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sbuilder = new StringBuilder();
            StringBuilder sbuilderSms = new StringBuilder();
            //End Of HERC code for sending mail to reviewers and publishers

            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSubject = (Label)row.FindControl("lblSubject");//
                        Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                        HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                        HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                        if (lblSubject.Text != null && lblSubject.Text != "")
                        {
                            ViewState["SOHTitle"] = hid.Value;
                        }
                        else
                        {
                            ViewState["SOHTitle"] = hidden1.Value;
                        }

                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SOHTitle"].ToString() + "<br/> ");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);

                        }
                    }
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                    {
                        Label lblSubject = (Label)row.FindControl("lblSubject");//
                        Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                        HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                        HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                        if (lblSubject.Text != null && lblSubject.Text != "")
                        {
                            ViewState["SOHTitle"] = hid.Value;
                        }
                        else
                        {
                            ViewState["SOHTitle"] = hidden1.Value;
                        }
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            //sbuilder.Append(lblSubjectSoh.Text + "; ");
                            sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SOHTitle"].ToString() + "<br/> ");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                        }
                    }

                }
                else //Here code is to approve records on date 12-05-2014
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);

                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSubject = (Label)row.FindControl("lblSubject");//
                        Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                        HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                        HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                        if (lblSubject.Text != null && lblSubject.Text != "")
                        {
                            ViewState["SOHTitle"] = hid.Value;
                        }
                        else
                        {
                            ViewState["SOHTitle"] = hidden1.Value;
                        }
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SOHTitle"].ToString() + "<br/> ");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            //Parts of code to send email to user and HERC authority

                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = petPetitionBL.getMobileNumberForSendingSohSms(petObject);
                            dsMail = petPetitionBL.getEmailIdForSendingSohMail(petObject);

                            //End

                            p_Var.Result = petPetitionBL.ApproveScheduleOfHearing(petObject);

                            /* Function to get email id of petitioners/respondents to send email*/
                            p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                            p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'><strong><strong>Subject:</strong></th><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                            p_Var.sbuildertmp.Append(dsMail.Tables[0].Rows[0]["Subject"].ToString());
                            p_Var.sbuildertmp.Append("</th></tr><tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>Venue:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            p_Var.sbuildertmp.Append(HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Venue"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");

                            p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>Date & Time:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            p_Var.sbuildertmp.Append(HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Date"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");
                            p_Var.sbuildertmp.Append("</table>");

                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact Law Officer at 0172-2569602<br/><br/>");
                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");

                            /* Function to get email id of petitioners/respondents to send email*/

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {

                                    string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                    if (strEmailHerc.StartsWith(";"))
                                    {
                                        strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                    }

                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim(), "HERC-Schedule of Hearing", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

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
                                            int index = dsSms.Tables[0].Rows[0]["Date"].ToString().LastIndexOf(":");
                                            //string message = dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index,1).Insert(index," ")

                                            string message = dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index, 1).Insert(index, " ") + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";


                                            string textmessage = "HERC Schedule of Hearing - ";

                                            miscellBL.SendsmsApprove(message, str, textmessage);

                                        }

                                    }

                                }
                            }

                            /* End */

                        }
                    }
                }
            }

            //End Of HERC code for sending mail to reviewers and publishers

            else
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppealSoh.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSubject = (Label)row.FindControl("lblSubject");//
                        Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                        HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                        HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                        if (lblSubject.Text != null && lblSubject.Text != "")
                        {
                            ViewState["SOHTitle"] = hid.Value;
                        }
                        else
                        {
                            ViewState["SOHTitle"] = hidden1.Value;
                        }
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SOHTitle"].ToString() + "<br/> ");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);

                        }
                    }
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppealSoh.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSubject = (Label)row.FindControl("lblSubject");//
                        Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                        HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                        HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                        if (lblSubject.Text != null && lblSubject.Text != "")
                        {
                            ViewState["SOHTitle"] = hid.Value;
                        }
                        else
                        {
                            ViewState["SOHTitle"] = hidden1.Value;
                        }
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            // sbuilder.Append(lblSubjectSoh.Text + "; ");
                            sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SOHTitle"].ToString() + "<br/> ");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                        }
                    }
                }

                else //Here code is to approve records on date 12-05-2014
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppealSoh.Rows)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSubject = (Label)row.FindControl("lblSubject");//
                        Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                        HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                        HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                        if (lblSubject.Text != null && lblSubject.Text != "")
                        {
                            ViewState["SOHTitle"] = hid.Value;
                        }
                        else
                        {
                            ViewState["SOHTitle"] = hidden1.Value;
                        }
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SOHTitle"].ToString() + "<br/> ");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            //Parts of code to send email to user and HERC authority

                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = petPetitionBL.getMobileNumberForSendingSohOmbudsmanSMS(petObject);
                            dsMail = petPetitionBL.getEmailIdForSendingSohOmbudsmanMail(petObject);

                            //End

                            p_Var.Result = petPetitionBL.ApproveScheduleOfHearing(petObject);

                            /* Function to get email id of petitioners/respondents to send email*/

                            p_Var.sbuildertmp.Append("In Appeal " + dsMail.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + (dsMail.Tables[0].Rows[0]["Applicant_Name"].ToString() + ", Next hearing fixed for " + dsMail.Tables[0].Rows[0]["Date"].ToString() + " at " + HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Venue"].ToString()) + "."));
                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact at 0172-2572299<br/><br/>");

                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");


                            /* Function to get email id of petitioners/respondents to send email*/

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {
                                    string strEmail = drRow["Petitioner_Email"].ToString();
                                    if (strEmail.StartsWith(";"))
                                    {
                                        strEmail = strEmail.Substring(1, strEmail.Length - 1);
                                    }
                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmail.Trim(), "The Electricity Ombudsman - Schedule of Hearing", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

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
                                            int index = dsSms.Tables[0].Rows[0]["Date"].ToString().LastIndexOf(":");
                                            //string message = dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index,1).Insert(index," ")

                                            string message = dsSms.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + dsSms.Tables[0].Rows[0]["Applicant_Name"] + ", Next hearing fixed for " + dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index, 1).Insert(index, " ") + " at " + dsSms.Tables[0].Rows[0]["Venue"] + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";

                                            string textmessage = "EO - Schedule of Hearing : In Appeal";

                                            miscellBL.SendsmsApprove(message, str, textmessage);

                                        }

                                    }

                                }
                            }
                            /* End */
                        }
                    }
                }

            }
            if (p_Var.Result > 0)
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
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
                        p_Var.sbuilder.Append("Record pending for Review: " + sbuilder.ToString());
                        p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                        if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                        {
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                        else
                        {
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    DataSet dsSms = new DataSet();
                    string textmessage;
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
                                    if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                                    {
                                        textmessage = "HERC - Record pending for review - ";
                                    }
                                    else
                                    {

                                        textmessage = "EO - Record pending for review - ";
                                    }

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }


                    /* End */

                    Session["msg"] = "Schedule of hearing has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
                //}
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    sbuilderSms.Remove(0, sbuilderSms.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
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
                        if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                        {
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                        else
                        {
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    string textmessage;
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
                                    if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                                    {
                                        textmessage = "HERC - Record pending for publish - ";
                                    }
                                    else
                                    {

                                        textmessage = "EO - Record pending for publish - ";
                                    }

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }


                    /* End */
                    Session["msg"] = "Schedule of hearing has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

                else
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    sbuilderSms.Remove(0, sbuilderSms.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                    {

                        if (li.Selected == true)
                        {
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
                        if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                        {
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                        else
                        {
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                        }
                    }
                    ///* Code to send sms */
                    char[] splitter = { ';' };
                    PetitionOB petObjectNew = new PetitionOB();
                    string textmessage;
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
                                    if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                                    {
                                        textmessage = "HERC - Record published - ";
                                    }
                                    else
                                    {

                                        textmessage = "EO - Record published - ";
                                    }

                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                }

                            }

                        }
                    }

                    /* End */

                    Session["msg"] = "Schedule Of Hearing has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            StringBuilder sbuilder = new StringBuilder();

            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {

                    foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");

                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");

                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);

                        }
                    }
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {

                    foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                    {

                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");

                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                        }
                    }

                }
                else
                {
                    foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            //Parts of code to send email to user and HERC authority

                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = petPetitionBL.getMobileNumberForSendingSohSms(petObject);
                            dsMail = petPetitionBL.getEmailIdForSendingSohMail(petObject);

                            //End

                            p_Var.Result = petPetitionBL.ApproveScheduleOfHearing(petObject);

                            /* Function to get email id of petitioners/respondents to send email*/
                            p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                            p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'><strong><strong>Subject:</strong></th><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                            p_Var.sbuildertmp.Append(dsMail.Tables[0].Rows[0]["Subject"].ToString());
                            p_Var.sbuildertmp.Append("</th></tr><tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>Venue:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            p_Var.sbuildertmp.Append(HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Venue"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");

                            p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'><strong>Date & Time:</strong></td><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            p_Var.sbuildertmp.Append(HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Date"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");
                            p_Var.sbuildertmp.Append("</table>");

                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact Law Officer at 0172-2569602<br/><br/>");
                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");

                            /* Function to get email id of petitioners/respondents to send email*/

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {

                                    string strEmailHerc = drRow["Petitioner_Email"].ToString();
                                    if (strEmailHerc.StartsWith(";"))
                                    {
                                        strEmailHerc = strEmailHerc.Substring(1, strEmailHerc.Length - 1);
                                    }

                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmailHerc.Trim(), "HERC - Schedule of Hearing", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

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
                                            int index = dsSms.Tables[0].Rows[0]["Date"].ToString().LastIndexOf(":");
                                            string message = dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index, 1).Insert(index, " ") + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";


                                            string textmessage = "HERC - Schedule of Hearing - ";

                                            miscellBL.SendsmsApprove(message, str, textmessage);

                                        }

                                    }

                                }
                            }

                            /* End */

                        }
                    }
                }
            }
            else
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {

                    foreach (GridViewRow row in grdAppealSoh.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");

                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");

                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);

                        }
                    }
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppealSoh.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");


                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");

                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                        }
                    }
                }
                else
                {
                    foreach (GridViewRow row in grdAppealSoh.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.SohTempID = p_Var.dataKeyID;
                            petObject.userID = Convert.ToInt32(Session["User_Id"]);
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            //Parts of code to send email to user and HERC authority

                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = petPetitionBL.getMobileNumberForSendingSohOmbudsmanSMS(petObject);
                            dsMail = petPetitionBL.getEmailIdForSendingSohOmbudsmanMail(petObject);

                            //End

                            p_Var.Result = petPetitionBL.ApproveScheduleOfHearing(petObject);

                            /* Function to get email id of petitioners/respondents to send email*/

                            p_Var.sbuildertmp.Append("In Appeal " + dsMail.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + (dsMail.Tables[0].Rows[0]["Applicant_Name"].ToString() + ", Next hearing fixed for " + dsMail.Tables[0].Rows[0]["Date"].ToString() + " at " + HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["Venue"].ToString()) + "."));
                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact at 0172-2572299<br/><br/>");

                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.");
                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");


                            /* Function to get email id of petitioners/respondents to send email*/

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Petitioner_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {
                                    string strEmail = drRow["Petitioner_Email"].ToString();
                                    if (strEmail.StartsWith(";"))
                                    {
                                        strEmail = strEmail.Substring(1, strEmail.Length - 1);
                                    }
                                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + strEmail.Trim(), "EO - Schedule of Hearing", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());

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
                                            int index = dsSms.Tables[0].Rows[0]["Date"].ToString().LastIndexOf(":");
                                            //string message = dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index, 1).Insert(index, " ") + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";

                                            string message = dsSms.Tables[0].Rows[0]["Appeal_Number"].ToString() + " of " + dsSms.Tables[0].Rows[0]["Applicant_Name"] + ", Next hearing fixed for " + dsSms.Tables[0].Rows[0]["Date"].ToString().Remove(index, 1).Insert(index, " ") + " at " + dsSms.Tables[0].Rows[0]["Venue"] + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";

                                            string textmessage = "EO - Schedule of Hearing : In Appeal";

                                            miscellBL.SendsmsApprove(message, str, textmessage);

                                        }

                                    }

                                }
                            }
                            /* End */
                        }
                    }
                }


            }
            if (p_Var.Result > 0)
            {
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    Session["msg"] = "Schedule of hearing has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                {
                    Session["msg"] = "Schedule of hearing has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
                else
                {
                    Session["msg"] = "Schedule Of Hearing has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {
                foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                {
                    Label lblSubject = (Label)row.FindControl("lblSubject");//
                    Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                    HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                    HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                    if (lblSubject.Text != null && lblSubject.Text != "")
                    {
                        ViewState["SohDisTitle"] = hid.Value;
                    }
                    else
                    {
                        ViewState["SohDisTitle"] = hidden1.Value;
                    }
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                        sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SohDisTitle"].ToString() + "<br/> ");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.SohTempID = p_Var.dataKeyID;
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

                        p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                    }
                }
            }
            else
            {
                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                foreach (GridViewRow row in grdAppealSoh.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblSubject = (Label)row.FindControl("lblSubject");//
                    Label lblSubjectSoh = (Label)row.FindControl("lblSubjectSoh");
                    HiddenField hid = (HiddenField)row.FindControl("HiddenField1");//
                    HiddenField hidden1 = (HiddenField)row.FindControl("hidden");
                    if (lblSubject.Text != null && lblSubject.Text != "")
                    {
                        ViewState["SohDisTitle"] = hid.Value;
                    }
                    else
                    {
                        ViewState["SohDisTitle"] = hidden1.Value;
                    }
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                        sbuilder.Append("<b>Schedule Of Hearing - </b>" + ViewState["SohDisTitle"].ToString() + "<br/> ");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.SohTempID = p_Var.dataKeyID;
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

                        p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                    }
                }
            }
            if (p_Var.Result > 0)
            {
                p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
                {

                    if (li.Selected == true)
                    {
                        int statindex = li.Text.IndexOf("(") + 1;
                        p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                        sbuilderSms.Append(li.Value + ";");
                    }

                }
                if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                {
                    p_Var.sbuilder.Append("Record disapproved : " + sbuilder.ToString());
                    p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                    p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                    //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                    string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    p_Var.sbuildertmp.Append(email);
                    if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                    {
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                    }
                    else
                    {
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "EO - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());
                    }
                }
                ///* Code to send sms */
                char[] splitter = { ';' };
                PetitionOB petObjectNew = new PetitionOB();
                string textmessage;
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
                                if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
                                {
                                    textmessage = "HERC - Record disapproved - ";
                                }
                                else
                                {

                                    textmessage = "EO - Record disapproved - ";
                                }

                                miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                            }

                        }

                    }
                }


                /* End */
                Session["msg"] = "Schedule Of Hearing has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            if (Convert.ToInt16(ddlDepartment.SelectedValue) == Convert.ToInt16(Module_ID_Enum.hercType.herc))
            {
                foreach (GridViewRow row in grdScheduleOfHearing.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                        // p_Var.dataKeyID = Convert.ToInt32(grdPetition.DataKeys[row.RowIndex].Value);
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.SohTempID = p_Var.dataKeyID;
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
                        p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                    }
                }
            }
            else
            {
                foreach (GridViewRow row in grdAppealSoh.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblScheduleOfHearingID");
                        // p_Var.dataKeyID = Convert.ToInt32(grdPetition.DataKeys[row.RowIndex].Value);
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.SohTempID = p_Var.dataKeyID;
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
                        p_Var.Result = petPetitionBL.updateScheduleOfHearingStatus(petObject);


                    }
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Schedule Of Hearing has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/soh/SOH_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            obj_userOB.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH);
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
            obj_userOB.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH);
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
            obj_userOB.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH);
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
            obj_userOB.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.SOH);
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
}
