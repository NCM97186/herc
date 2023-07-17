using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;

public partial class Auth_AdminPanel_Appeal_DisplayAppeal : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    #region variable declaration zone

    Project_Variables p_Var = new Project_Variables();
    LinkBL obj_linkBL = new LinkBL();
    LinkOB obj_inkOB = new LinkOB();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionOB appealObject = new PetitionOB();
    AppealBL appealBL = new AppealBL();
    PaginationBL pagingBL = new PaginationBL();
    PetitionBL petPetitionBL = new PetitionBL();
    PetitionOB petObject = new PetitionOB();
    PublicNoticeBL pubNoticeBL = new PublicNoticeBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

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

        Session.Remove("PStatus");// Public Notice module sessions
        Session.Remove("PYear");
        Session.Remove("PLng");

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

        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Appeal"].ToString() + "/";
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

            //Label lblModulename = Master.FindControl("lblModulename") as Label;
            //lblModulename.Text = ": EO Appeal Management";

            Label lblModulename = Master.FindControl("lblModulename") as Label;
            lblModulename.Text = ": View  EO Appeal";
            this.Page.Title = " EO Appeal: HERC";

            ViewState["sortOrder"] = "";
            this.Page.Form.Enctype = "multipart/form-data";

            //rptPager.Visible      = false;
            //lblPageSize.Visible   = false;
            //ddlPageSize.Visible   = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddAppeal.Visible = false;

            if (Session["Appealyear"] != null)
            {
                bindOrdersYearinDdl();
                ddlYear.SelectedValue = Session["Appealyear"].ToString();
            }
            else
            {
                bindOrdersYearinDdl();
            }
            if (Session["AppealLng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["AppealLng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["Appealdeptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["Appealdeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
            if (Session["AppealStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["AppealStatus"].ToString();
                Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            }
            else
            {
                binddropDownlistStatus();
            }


        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddAppeal click event to redirect on other page

    protected void btnAddAppeal_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Appeal/") + "Appeal_Add.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    #region button btnForReview click event to send records for review

    protected void btnForReview_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

    }

    #endregion

    #region btnForApprove click event to send records for approval

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

    #region button btnDisApprove click event to disapprove records

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithEmailIDDiaapproves();

        }
        else
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithDataEntryEmailIDs();

        }


    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), pageIndex);
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion

    #region button btnUpdateStatus click event to update status

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        p_Var.rtiid = Convert.ToInt32(ViewState["id"]);
        appealObject.AppealId = p_Var.rtiid;
        if (Convert.ToInt16(ddlAppealStatusUpdate.SelectedValue) == 25)
        {
            PetitionOB objpet = new PetitionOB();
            objpet.subject = txtOtherStatus.Text;
            objpet.StatusId = 3;
            p_Var.Result = petPetitionBL.Insert_Status(objpet, out p_Var.k);
            appealObject.AppealStatusId = p_Var.Result;

        }
        else
        {
            appealObject.AppealStatusId = Convert.ToInt16(ddlAppealStatusUpdate.SelectedValue);
        }
        appealObject.WhereAppealed = txtWhereAppealed.Text;
        // appealObject.AppealNo = txtAppealNumber.Text;
        if (Upload_File(ref p_Var.Filename))
        {
            if (p_Var.Filename != null)
            {
                appealObject.FileName = fileUploadPdf.FileName.ToString();
            }
            else
            {
                appealObject.FileName = null;
            }

        }
        else
        {
            appealObject.FileName = null;
        }

        p_Var.Result = appealBL.modifyAppealStatus(appealObject);
        if (p_Var.Result > 0)
        {
            Session["msg"] = "Appeal's status has been updated successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
    }

    #endregion

    #region button btnDelete click event to delete petition

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        appealObject.AppealId = p_Var.rp_id;
        appealObject.StatusId = p_Var.status_id;

        p_Var.Result = appealBL.Delete_AwardwithAppealReview(appealObject);
        if (p_Var.Result > 0)
        {

            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.Title = "Appeal";
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

            Session["msg"] = "Appeal has been deleted successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        }
    }

    #endregion

    //End

    //Area for all the dropDownlist selectedIndexChanged events

    #region dropDownlist ddlDepartment selectedIndexChanged event zone

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        // grdCMSMenu.Visible = false;
        grdAppeal.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
        Session["Appealdeptt"] = ddlDepartment.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdAppeal.Visible = false;
            Session["AppealLng"] = ddlLanguage.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event zone

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppeal.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            Session["AppealStatus"] = ddlStatus.SelectedValue;
        }

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }

        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
        {
            grdAppeal.Columns[10].Visible = true;//This is for Change status
        }
        else
        {
            grdAppeal.Columns[10].Visible = false;//This is for Change status
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged events

    protected void ddlAppealStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlAppealStatusUpdate.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Appeal_Status.AwardPronounced))
        {
            //// trUpLoader.Visible = true;
        }
        else
        {
            ////trUpLoader.Visible = false;
        }


        if (Convert.ToInt16(ddlAppealStatusUpdate.SelectedValue) == 25)
        {
            txtOtherStatus.Visible = true;

        }
        else
        {
            txtOtherStatus.Visible = false;
        }
        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #endregion

    //End

    //Area for all the gridView events

    #region gridView grdAppeal rowCommand event zone

    protected void grdAppeal_RowCommand(object sender, GridViewCommandEventArgs e)
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
                if (e.CommandName == "ChangeStatus")
                {
                    bindAppealStatus();
                    appealObject.TempAppealId = Convert.ToInt32(e.CommandArgument.ToString());
                    appealObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    p_Var.dSet = appealBL.getAppealRecordForEdit(appealObject);
                    if (p_Var.dSet.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Appeal_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.InProcessOmbudsman) && Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Appeal_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.AwardPronounced) && Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Appeal_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.ScheduleForHearing))
                        {
                            ddlAppealStatusUpdate.SelectedValue = Convert.ToInt16(Module_ID_Enum.Petition_Status.anyOther).ToString();
                            txtOtherStatus.Visible = true;
                            txtOtherStatus.Text = p_Var.dSet.Tables[0].Rows[0]["AppealStatusName"].ToString();
                        }
                        else
                        {
                            ddlAppealStatusUpdate.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Appeal_Status_Id"].ToString();
                            txtOtherStatus.Visible = false;
                        }

                    }
                    ViewState["id"] = e.CommandArgument.ToString();
                    this.mpuUpdateStatus.Show();
                }
                if (e.CommandName == "Appeal")
                {

                    Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/AddEditAppealAward.aspx?ModuleID=4 &Appeal_Id=" + e.CommandArgument.ToString()));

                }
                if (e.CommandName == "delete")
                {
                    p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
                    p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
                    p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);
                    ViewState["commandArgs"] = p_Var.commandArgs;
                }
                if (e.CommandName == "Restore")
                {

                    appealObject.AppealId = Convert.ToInt32(e.CommandArgument);
                    p_Var.Result = appealBL.AppealAward_Restore(appealObject);
                    if (p_Var.Result > 0)
                    {
                        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblAppealNumber = row.FindControl("lblAppealNumber") as Label;
                        obj_audit.Title = lblAppealNumber.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Appeal has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "Appeal has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    petObject.ModuleType = null;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lblAppealNumber") as Label;
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

    #region gridView grdAppeal rowCreated event to select checkboxes

    protected void grdAppeal_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdAppeal.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));


        }
    }

    #endregion

    #region gridView grdAppeal rowDataBound event

    protected void grdAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField status = e.Row.FindControl("HypStatus") as HiddenField;
            LinkButton lnkAppealId = (LinkButton)e.Row.FindControl("lnkAppealId");
            Label lblAppealId = (Label)e.Row.FindControl("lblAppealId");

            Label lblAppeal_ID = (Label)e.Row.FindControl("lblAppeal_ID");
            HiddenField StatusId = e.Row.FindControl("StatusId") as HiddenField;

            LinkButton lnkChangeStatus = (LinkButton)e.Row.FindControl("lnkChangeStatus");
            if (status.Value == "17")
            {
                lblAppealId.Visible = false;
                lnkAppealId.Visible = true;
                lnkAppealId.Text = "Yes";
            }
            else
            {
                lblAppealId.Visible = true;
                lnkAppealId.Visible = false;
                lblAppealId.Text = "No";
            }


            appealObject.StatusId = Convert.ToInt16(StatusId.Value);
            appealObject.TempAppealId = Convert.ToInt32(lblAppeal_ID.Text);
            p_Var.dsFileName = appealBL.GETAppealID_Award(appealObject);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                lblAppealId.Visible = true;
                lnkAppealId.Visible = false;
                lblAppealId.Text = "Yes";
            }



            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");

            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            string number = DataBinder.Eval(e.Row.DataItem, "apealnum").ToString();
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                if (number != null && number != "")
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to purge Appeal No- " + DataBinder.Eval(e.Row.DataItem, "apealnum") + "')");
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure want to restore Appeal No- " + DataBinder.Eval(e.Row.DataItem, "apealnum") + "')");
                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                 "confirm('Are you sure you want to purge ?')");
                    BtnRestore.Attributes.Add("onclick", "javascript:return " +
                   "confirm('Are you sure want to restore ?')");

                }
            }
            else
            {
                if (number != null && number != "")
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete Appeal No- " + DataBinder.Eval(e.Row.DataItem, "apealnum") + "')");


                    lnkChangeStatus.Attributes.Add("onclick", "javascript:return " +
                                       "confirm('Are you sure you want to change status of Appeal No- " + DataBinder.Eval(e.Row.DataItem, "apealnum") + "')");

                }
                else
                {
                    BtnDelete.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are you sure you want to delete this record ?')");


                    lnkChangeStatus.Attributes.Add("onclick", "javascript:return " +
                 "confirm('Are you sure you want to change status?')");
                }

            }

            //END
        }

    }

    #endregion

    #region gridView grdAppeal rowDeleting event to delete records

    protected void grdAppeal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        GridViewRow row = (GridViewRow)grdAppeal.Rows[e.RowIndex];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        appealObject.AppealId = Convert.ToInt32(p_Var.rp_id);
        p_Var.dSet = appealBL.getAppeal_Number_for_DeleteReviewAppeal(appealObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            //lblDeleteMsg.Text = "This record is already present in Award Pronounced. Are you sure you want to delete this record which has appeal number : " + p_Var.dSet.Tables[0].Rows[0]["Appeal_Number"];
            lblDeleteMsg.Text = "Award has already been pronounced against this appeal. Are you sure you want to delete this Appeal No : " + p_Var.dSet.Tables[0].Rows[0]["Appeal_Number"] + "?";
            this.ModalPopupExtender1.Show();
        }
        else
        {
            p_Var.commandArgs = (string[])ViewState["commandArgs"];
            p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

            appealObject.RPId = p_Var.rp_id;
            appealObject.StatusId = p_Var.status_id;

            p_Var.Result = appealBL.Delete_AppealReview(appealObject);
            if (p_Var.Result > 0)
            {
                obj_audit.ActionType = "D";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.status = ddlStatus.SelectedItem.ToString();
                Label lblAppealNumber = row.FindControl("lblAppealNumber") as Label;
                obj_audit.Title = lblAppealNumber.Text;
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "Appeal has been deleted successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }

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
            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
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
                p_Var.dSet = miscdlLanguage.getLanguage(usrObject);
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
                btnAddAppeal.Visible = true;

                //code written on date 21 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddAppeal.Visible = false;
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

            //BtnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            //obj_userOB.DepttId = Convert.ToInt16(Session["Dept_ID"]);
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            p_Var.dSet = obj_UserBL.ASP_Get_Deptt_Name(obj_userOB);
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

    #region Function to bind gridView with appeals records

    public void Bind_Grid(int departmentid, string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppeal.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdAppeal.Visible = true;
            appealObject.DepttId = departmentid;
            appealObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            appealObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            appealObject.year = ddlYear.SelectedValue.ToString();

            p_Var.dSet = appealBL.getTempAppealRecords(appealObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdAppeal.Columns[13].HeaderText = "Purge";
                }
                else
                {
                    grdAppeal.Columns[13].HeaderText = "Delete";
                }
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End

                grdAppeal.DataSource = myDataView;
                grdAppeal.DataBind();

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
                        grdAppeal.Columns[0].Visible = false;


                        foreach (GridViewRow row in grdAppeal.Rows)
                        {
                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblRTI_ID = (Label)row.FindControl("lblAppeal_ID");
                            //Line added on date 3 Oct 2013 by ruchi sharma
                            Label lblchangestatus = (Label)row.FindControl("lblchangestatus");
                            LinkButton lnkChangeStatus = (LinkButton)row.FindControl("lnkChangeStatus");
                            //End
                            appealObject.AppealId = Convert.ToInt16(lblRTI_ID.Text);

                            p_Var.dSetCompare = appealBL.get_ID_For_Compare(appealObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_Appeal_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblRTI_ID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_Appeal_Id"]))
                                    {
                                        imgnotedit.Visible = true;
                                        imgedit.Visible = false;

                                        // 3 Oct 2013
                                        lblchangestatus.Visible = true;
                                        lnkChangeStatus.Visible = false;
                                        //End

                                    }
                                    else
                                    {
                                        imgnotedit.Visible = false;
                                        imgedit.Visible = true;

                                        // 3 Oct 2013
                                        lblchangestatus.Visible = false;
                                        lnkChangeStatus.Visible = true;
                                        //End
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        grdAppeal.Columns[0].Visible = true;


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

                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdAppeal.Columns[10].Visible = false;//This is for Change status
                            grdAppeal.Columns[11].Visible = false;//This is for Review(Y/N)
                            grdAppeal.Columns[12].Visible = false;//This is for Edit
                            grdAppeal.Columns[14].Visible = true;//This is for restore
                            grdAppeal.Columns[15].Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdAppeal.Columns[10].Visible = true;//This is for Change status
                            }
                            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                            {
                                if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdAppeal.Columns[15].Visible = true;
                                }
                                else
                                {
                                    grdAppeal.Columns[15].Visible = false;
                                }
                            }
                            else
                            {
                                grdAppeal.Columns[10].Visible = false;//This is for Change status

                            }
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdAppeal.Columns[11].Visible = true;//This is for Review(Y/N)
                            }
                            else
                            {
                                grdAppeal.Columns[11].Visible = false;//This is for Review(Y/N)
                            }
                            grdAppeal.Columns[12].Visible = true; //This is for Edit
                            grdAppeal.Columns[14].Visible = false; //This is for restore
                        }

                    }
                    else
                    {
                        grdAppeal.Columns[12].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdAppeal.Columns[13].Visible = true;
                                grdAppeal.Columns[15].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdAppeal.Columns[13].Visible = true;
                                    grdAppeal.Columns[15].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdAppeal.Columns[13].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdAppeal.Columns[15].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdAppeal.Columns[15].Visible = true;
                                            }
                                            else
                                            {
                                                grdAppeal.Columns[15].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdAppeal.Columns[13].Visible = false;
                                        grdAppeal.Columns[15].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdAppeal.Columns[13].Visible = true;
                                grdAppeal.Columns[15].Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdAppeal.Columns[13].Visible = true;
                            }

                        }

                        //End    
                    }
                    else
                    {
                        grdAppeal.Columns[13].Visible = false;
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                        {
                            grdAppeal.Columns[10].Visible = true;//This is for Change status
                        }
                        else
                        {
                            grdAppeal.Columns[10].Visible = false;//This is for Change status

                        }
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                        {
                            grdAppeal.Columns[11].Visible = true;//This is for Review(Y/N)
                        }
                        else
                        {
                            grdAppeal.Columns[11].Visible = false;//This is for Review(Y/N)
                        }
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                grdAppeal.Visible = false;
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

    #region Function to bind status of Appeal

    public void bindAppealStatus()
    {
        p_Var.dSet = appealBL.getAppealStatus();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlAppealStatusUpdate.DataSource = p_Var.dSet;
            ddlAppealStatusUpdate.DataTextField = "STATUS";
            ddlAppealStatusUpdate.DataValueField = "STATUS_ID";
            ddlAppealStatusUpdate.DataBind();
        }
    }

    #endregion

    #region Function to upload files

    private bool Upload_File(ref string filename)
    {

        Miscelleneous_BL miscellFileBL = new Miscelleneous_BL();
        try
        {
            if (fileUploadPdf.HasFile)
            {
                p_Var.Filename = fileUploadPdf.PostedFile.FileName;
            }
            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(fileUploadPdf.PostedFile.FileName);
            p_Var.ext = Path.GetExtension(fileUploadPdf.FileName);
            //For Unique File Name
            filename = miscellFileBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
            //End
            fileUploadPdf.PostedFile.SaveAs(Server.MapPath(p_Var.url) + filename);
        }
        catch
        {
            p_Var.uploadStatus = false;
        }
        return p_Var.uploadStatus;
    }

    #endregion

    //End


    protected void grdAppeal_Sorting(object sender, GridViewSortEventArgs e)
    {

        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);

    }

    #region gridView grdOrders pageIndexChanging Event zone

    protected void grdAppeal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAppeal.PageIndex = e.NewPageIndex;

        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion


    #region Function to bind Orders Year

    public void bindOrdersYearinDdl()
    {
        appealObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
        p_Var.dSet = appealBL.GetYear(appealObject);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
    }

    #endregion


    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdAppeal.Visible = false;
        Session["Appealyear"] = ddlYear.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
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

    //End
    protected void btnAppealAward_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/DisplayAppealAward.aspx?ModuleID=4"));
    }


    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=4"));
    }

    public void BindGridDetails()
    {
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppealPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdAppealPdf.Visible = true;
            appealObject.DepttId = Convert.ToInt16(ddlDepartment.SelectedValue);
            appealObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            appealObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            appealObject.year = ddlYear.SelectedValue.ToString();

            p_Var.dSet = appealBL.getTempAppealRecords(appealObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                grdAppealPdf.DataSource = p_Var.dSet;
                grdAppealPdf.DataBind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Appeal_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdAppealPdf.AllowPaging = false;
        grdAppealPdf.DataBind();
        grdAppealPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    protected void grdAppealPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblApplicantName = (Label)e.Row.FindControl("lblApplicantName");
            Label lblAddress = (Label)e.Row.FindControl("lblAddress");
            Label lblRespondent = (Label)e.Row.FindControl("lblRespondent");
            Label lblRespondentAddress = (Label)e.Row.FindControl("lblRespondentAddress");
            Label lbSubject = (Label)e.Row.FindControl("lbSubject");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            if (lblApplicantName.Text != null && lblApplicantName.Text != "")
            {
                lblApplicantName.Text = HttpUtility.HtmlDecode(lblApplicantName.Text);
            }
            if (lblAddress.Text != null && lblAddress.Text != "")
            {
                lblAddress.Text = HttpUtility.HtmlDecode(lblAddress.Text);
            }
            if (lblRespondent.Text != null && lblRespondent.Text != "")
            {
                lblRespondent.Text = HttpUtility.HtmlDecode(lblRespondent.Text);
            }
            if (lblRespondentAddress.Text != null && lblRespondentAddress.Text != "")
            {
                lblRespondentAddress.Text = HttpUtility.HtmlDecode(lblRespondentAddress.Text);
            }
            if (lbSubject.Text != null && lbSubject.Text != "")
            {
                lbSubject.Text = HttpUtility.HtmlDecode(lbSubject.Text);
            }
            if (lblRemarks.Text != null && lblRemarks.Text != "")
            {
                lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            }
        }
    }

    //Codes to send email

    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppeal.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                        ViewState["AppealNumber"] = lblAppealNumber.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>EO Appeal - " + lblAppealNumber.Text + "<br/></b>");
                            Label lblid = (Label)row.FindControl("lblAppeal_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            appealObject.TempAppealId = p_Var.dataKeyID;
                            appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                            appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                            appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            p_Var.Result = appealBL.updateAppealStatus(appealObject);
                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
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
                            p_Var.sbuilder.Append("Record pending for Review: " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");

                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record pending for Review", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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


                                        string message = strPublicNotice;

                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        textmessage = "EO - Record pending for review, Appeal -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = "Appeal has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppeal.Rows)
                    {
                        Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        ViewState["AppealNumber"] = lblAppealNumber.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>EO Appeal - " + lblAppealNumber.Text + "<br/></b>");
                            Label lblid = (Label)row.FindControl("lblAppeal_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            appealObject.TempAppealId = p_Var.dataKeyID;
                            appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                            appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = appealBL.updateAppealStatus(appealObject);
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
                            p_Var.sbuilder.Append("Record pending for Publish: " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");

                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record pending for Publish", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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


                                        string message = strPublicNotice;

                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        textmessage = "EO - Record pending for publish, Appeal -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */
                        Session["msg"] = "Appeal has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else
                {
                    foreach (GridViewRow row in grdAppeal.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                        ViewState["AppealNumber"] = lblAppealNumber.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>EO Appeal - " + lblAppealNumber.Text + "<br/></b>");
                            Label lblid = (Label)row.FindControl("lblAppeal_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            appealObject.TempAppealId = p_Var.dataKeyID;
                            appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                            appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = appealBL.InsertAppealIntoWeb(appealObject);

                        }
                    }
                    if (p_Var.Result > 0)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
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
                            p_Var.sbuilder.Append("Record Published: " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");

                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record Published", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                        string message = strPublicNotice;
                                        if (message.Length > 150)
                                        {
                                            message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                        }
                                        else
                                        {
                                            message = strPublicNotice.ToString();
                                        }
                                        textmessage = "EO - Record published, Appeal -";

                                        miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                    }

                                }

                            }
                        }

                        /* End */

                        Session["msg"] = " Appeal has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
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
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealObject.TempAppealId = p_Var.dataKeyID;
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                        appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = appealBL.updateAppealStatus(appealObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Appeal has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealObject.TempAppealId = p_Var.dataKeyID;
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                        appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = appealBL.updateAppealStatus(appealObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Appeal has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {

                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblAppeal_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        appealObject.TempAppealId = p_Var.dataKeyID;
                        appealObject.userID = Convert.ToInt32(Session["User_Id"]);
                        appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = appealBL.InsertAppealIntoWeb(appealObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = " Appeal has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            foreach (GridViewRow row in grdAppeal.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                Label lblAppealNumber = (Label)row.FindControl("lblAppealNumber");
                ViewState["DisAppealNumber"] = lblAppealNumber.Text;
                if ((selCheck.Checked == true))
                {
                    sbuilder.Append("<b>EO Appeal - " + lblAppealNumber.Text + "<br/> </b>");
                    Label lblid = (Label)row.FindControl("lblAppeal_ID");
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    appealObject.TempAppealId = p_Var.dataKeyID;

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }

                    p_Var.Result = appealBL.updateAppealStatus(appealObject);
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
                    p_Var.sbuilder.Append("Record disapproved : " + sbuilder.ToString() + "<br/>");
                    p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                    p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");

                    string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    p_Var.sbuildertmp.Append(email);
                    miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "E/O - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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


                                string message = strPublicNotice;

                                if (message.Length > 150)
                                {
                                    message = strPublicNotice.ToString().Substring(0, 150) + "...";
                                }
                                else
                                {
                                    message = strPublicNotice.ToString();
                                }
                                textmessage = "EO - Record disapproved, Appeal -";

                                miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                            }

                        }
                    }
                }


                /* End */
                Session["msg"] = "Appeal has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            foreach (GridViewRow row in grdAppeal.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblAppeal_ID");
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    appealObject.TempAppealId = p_Var.dataKeyID;

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //appealObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_Var.Result = appealBL.updateAppealStatus(appealObject);
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Appeal has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
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
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.ombudsman);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal);
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
}
