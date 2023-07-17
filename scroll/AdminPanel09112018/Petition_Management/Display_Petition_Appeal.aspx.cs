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
using System.Text;


public partial class Auth_AdminPanel_Petition_Management_Display_Petition_Appeal : System.Web.UI.Page
{
    #region Data declaration zone

    Project_Variables p_Var = new Project_Variables();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();

    #endregion

    #region page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
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

            Label lblModulename = Master.FindControl("lblModulename") as Label;
            lblModulename.Text = ": View  Appeal against Order Management";
            this.Page.Title = " Appeal against Order Management: HERC";

            ViewState["sortOrder"] = "";
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnForReview.Visible = false;
           // PLanguage.Visible = false;
            lblmsg.Visible = false;
            //ddlPageSize.Visible = false;
            //lblPageSize.Visible = false;
           
            if (Session["PetAppealLng"] != null)
            {
               // bindropDownlistLang();
               // ddlLanguage.SelectedValue = Session["PetAppealLng"].ToString();
            }
            else
            {
               // bindropDownlistLang();

            }
            if (Session["PetAppealYear"] != null)
            {
                bindRtiYearinDdl();
                ddlYear.SelectedValue = Session["PetAppealYear"].ToString();
            }
            else
            {
                bindRtiYearinDdl();
            }
            if (Session["PetAppealStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["PetAppealStatus"].ToString();
                bind_Pending_Approve_Grid("", "");
            }
            else
            {
                binddropDownlistStatus();
            }

        }
    }

    #endregion

    //Area for all dropDownlist events 

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLanguage.SelectedValue != "0")
    //    {
    //        binddropDownlistStatus();
    //        grdAppeal.Visible = false;
    //        Session["PetAppealLng"] = ddlLanguage.SelectedValue;
    //    }
    //}

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
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
            if (ddlStatus.SelectedValue.ToString() == (Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft)).ToString())
            {
                // GvAdd_Details.Columns[0].Visible = false;
                // BtnForReview.Visible = true;
            }
            else
            {
                //  GvAdd_Details.Columns[0].Visible = true;

            }

            bind_Pending_Approve_Grid("", "");
            Session["PetAppealStatus"] = ddlStatus.SelectedValue;
        }
    }

    #endregion

    //End

    //Area for all buttons, linkbuttons, imagebuttons click events

    #region button btnPetition_View click event to go to petition detail pages

    protected void btnPetition_View_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
    }

    #endregion

    #region button btnReview_View click event to go to petition review pages

    protected void btnReview_View_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/Petition_Management/Review_Petition_Display.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
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

    #region button btnApprove click event to approve records

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

        //if (Page.IsValid)
        //{
        //    ChkApprove_Disapprove();
        //}
    }

    #endregion

    #region button btnDisapprove click event to disapprove records

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
        //    foreach (GridViewRow row in grdAppeal.Rows)
        //    {
        //        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
        //        if ((selCheck.Checked == true))
        //        {
        //            Label lblid = (Label)row.FindControl("lblPA_ID");

        //            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
        //            petObject.TempPAId = p_Var.dataKeyID;
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
        //            p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
        //        }

        //    }

        //    if (p_Var.Result > 0)
        //    {
        //        Session["msg"] = "Appeal record has been disapproved successfully.";
        //        Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
        //        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        //    }


        //}
        //catch
        //{
        //    throw;
        //}
    }

    #endregion

    //End

    //Area for all gridView events



    #region gridView grdAppeal rowCommand event zone

    protected void grdAppeal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Delete")
        {
            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
            p_Var.pa_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

            petObject.PAId = p_Var.pa_id;
            petObject.StatusId = p_Var.status_id;

            p_Var.Result = petPetitionBL.Delete_Petition_Appeal(petObject);
            if (p_Var.Result > 0)
            {
                
                if (ddlStatus.SelectedValue == "8")
                {
                    Session["msg"] = "Appeal record has been deleted (purged) permanently.";
                }
                else
                {
                    Session["msg"] = "Appeal record has been deleted successfully.";
                }
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
            }
        }

        else if (e.CommandName == "Restore")
        {

            petObject.PAId = Convert.ToInt32(e.CommandArgument);

            p_Var.Result = petPetitionBL.updateAppealPetitionStatusDelete(petObject);
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Appeal record has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx",false);
            }
            else
            {
                Session["msg"] = "Appeal record has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx",false);
            }
        }

        else if (e.CommandName == "ChangeStatus")
        {
            txtOtherStatus.Visible = false;
            bindDdlPetitionStatus();
            petObject.TempPAId = Convert.ToInt32(e.CommandArgument.ToString());
            petObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.get_Temp_Appeal_Petition_RecordsEdit(petObject);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlPetitionStatusUpdate.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["PA_Status_Id"].ToString();
            }
            ViewState["id"] = e.CommandArgument.ToString();

            this.mpuUpdateStatus.Show();
        }
        else if (e.CommandName == "Audit")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);// here petition id for record id for the audittrail.
            petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            petObject.ModuleType = 2;
            dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
            Label lblprono = row.FindControl("lblProNumber") as Label;
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


    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetitionStatus()
    {

        try
        {

            petObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dSet = miscellBL.getPetitionAppealStatusAccordingtoModule(petObject);
            ddlPetitionStatusUpdate.DataSource = p_Var.dSet;
            ddlPetitionStatusUpdate.DataTextField = "Status";
            ddlPetitionStatusUpdate.DataValueField = "Status_Id";
            ddlPetitionStatusUpdate.DataBind();
            //ddlPetitionStatusUpdate.Items.Insert(0, new ListItem("Select Status", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region gridView grdAppeal rowCreated event zone

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

    #region gridView grdAppeal rowDataBound event zone

    protected void grdAppeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label judgementLink = (Label)e.Row.FindControl("lblJudgementLink");
            LinkButton lnkChangeStatus = (LinkButton)e.Row.FindControl("lnkChangeStatus");
            lnkChangeStatus.OnClientClick = "javascript:return confirm('Are you sure you want to change status of Appeal No - " + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "?');";

            if (judgementLink.Text != "" && judgementLink.Text != null)
            {
                judgementLink.Text = judgementLink.Text;
            }
            else
            {
                judgementLink.Text = "N/A";
            }

            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge Appeal No- " + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are You sure want to restore Appeal No-" + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");

            }
            else
            {

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete Appeal No- " + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are You sure want to restore Appeal No-" + DataBinder.Eval(e.Row.DataItem, "AppealNumber") + "')");
            }

            //END
        }
    }

    #endregion

    #region gridView grdAppeal rowDeleting event

    protected void grdAppeal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind language in dropDownlist according to permission

    public void bindropDownlistLang()
    {
        try
        {
            //obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            //obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            //p_Var.dSet = miscellBL.getLanguagePermission(obj_userOB);
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
            //    p_Var.dSet = miscellBL.getLanguage(usrObject);
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

                btnAppealAdd.Visible = true;
                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end

            }
            else
            {
                btnAppealAdd.Visible = false;
            }

            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                //p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover));
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

            btnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

    #region Function to bind gridView with Petitions appeal

    public void bind_Pending_Approve_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        if (ddlStatus.SelectedValue == "0")
        {
            grdAppeal.Visible = false;
            btnForReview.Visible = false;


        }
        else
        {

            grdAppeal.Visible = true;


            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
           
            petObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = petPetitionBL.get_Temp_Appeal_Petition_Records(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {


                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdAppeal.Columns[9].HeaderText = "Purge";
                    grdAppeal.Columns[9].Visible = true;
                    grdAppeal.Columns[12].Visible = false;
                }
                else
                {
                    grdAppeal.Columns[9].HeaderText = "Delete";
                    grdAppeal.Columns[9].Visible = false;
                    grdAppeal.Columns[12].Visible = false;
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
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscellBL.getLanguagePermission(obj_userOB);
                if (p_Var.dSet.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdAppeal.Columns[0].Visible = false;

                        foreach (GridViewRow row in grdAppeal.Rows)
                        {

                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblRPID = (Label)row.FindControl("lblPA_ID");
                            Label lblchangestatus = (Label)row.FindControl("lblchangestatus");
                            LinkButton lnkChangeStatus = (LinkButton)row.FindControl("lnkChangeStatus");
                            petObject.PAId = Convert.ToInt32(lblRPID.Text);

                            p_Var.dSetCompare = petPetitionBL.get_ID_For_Appeal_Comparison(petObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_PA_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblRPID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_PA_Id"]))
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


                                        //3 Oct 2013
                                        lblchangestatus.Visible = true;
                                        lnkChangeStatus.Visible = false;
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
                        //  btnDisApprove.Visible = false;
                    }

                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true)
                    {
                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdAppeal.Columns[8].Visible = false;
                            grdAppeal.Columns[10].Visible = true;//This is for restore
                            grdAppeal.Columns[11].Visible = false;//This is for change status
                            grdAppeal.Columns[12].Visible = false;

                        }
                        else
                        {
                            grdAppeal.Columns[8].Visible = true;
                            grdAppeal.Columns[10].Visible = false;//This is for restore
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdAppeal.Columns[11].Visible = true;//This is for change status
                            }
                            else
                            {
                                grdAppeal.Columns[11].Visible = false;//This is for change status
                            }
                        }
                        //grdAppeal.Columns[8].Visible = true;
                    }
                    else
                    {
                        grdAppeal.Columns[8].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                       //// grdAppeal.Columns[9].Visible = true;  // commented on date 23 sep 2013


                          // modify on date 23 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdAppeal.Columns[9].Visible = true;
                                grdAppeal.Columns[12].Visible = false;
                            }
                            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                            {
                                grdAppeal.Columns[9].Visible = true;
                                if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                {
                                    grdAppeal.Columns[12].Visible = true;
                                }
                                else
                                {
                                    grdAppeal.Columns[12].Visible = false;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdAppeal.Columns[9].Visible = true;
                                    grdAppeal.Columns[12].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdAppeal.Columns[9].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdAppeal.Columns[12].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdAppeal.Columns[12].Visible = true;
                                            }
                                            else
                                            {
                                                grdAppeal.Columns[12].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdAppeal.Columns[9].Visible = false;
                                        grdAppeal.Columns[12].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdAppeal.Columns[9].Visible = true;
                                grdAppeal.Columns[12].Visible = false;
                            }
                            

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdAppeal.Columns[9].Visible = true;
                            }

                        }

                        //End  
                    }
                    else
                    {
                        grdAppeal.Columns[9].Visible = false;
                    }

                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdAppeal.Columns[11].Visible = true;//This is for change status
                    }
                    else
                    {
                        grdAppeal.Columns[11].Visible = false;//This is for change status
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                //////////rptPager.Visible = false;
                //////////lblPageSize.Visible = false;
                //////////ddlPageSize.Visible = false;
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
        Session["Lanuage"] = Convert.ToInt16(Module_ID_Enum.Language_ID.English);

    }

    #endregion


    //End


    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind_Pending_Approve_Grid("", "");
    }
    #endregion


    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        bind_Pending_Approve_Grid("", "");
    }

    #endregion

   
 

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdAppeal.Visible = false;
        Session["PetAppealYear"] = ddlYear.SelectedValue;
    }

    #region Function to bind Rti Year

    public void bindRtiYearinDdl()
    {
        p_Var.dSet = petPetitionBL.GetYearPetitionAppeal_Admin();
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

  
    protected void grdAppeal_Sorting(object sender, GridViewSortEventArgs e)
    {
        bind_Pending_Approve_Grid(e.SortExpression, sortOrder);

    }

    #region gridView grdAppeal pageIndexChanging Event zone

    protected void grdAppeal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAppeal.PageIndex = e.NewPageIndex;
        bind_Pending_Approve_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
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


    #region button btnUpdateStatus click event to update status of petition

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        p_Var.petition_id = Convert.ToInt32(ViewState["id"]);
        petObject.PAId = p_Var.petition_id;
        if (Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue) == 25)
        {
            PetitionOB objpet = new PetitionOB();
            objpet.subject = txtOtherStatus.Text;
            objpet.StatusId = 3;
            p_Var.Result = petPetitionBL.Insert_Status(objpet, out p_Var.k);
            petObject.PetitionStatusId = p_Var.Result;

        }
        else
        {
            petObject.PetitionStatusId = Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue);
        }
        p_Var.Result = petPetitionBL.AppealpetitionStatusUpdate(petObject);
        if (p_Var.Result > 0)
        {
            Session["msg"] = "Appeal Petition status has been updated successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }

    }

    #endregion

    protected void ddlPetitionStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=3"));
    }

    protected void btnAppealAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Petition_Management/") +"Petition_Appeal_Insert_Update.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }
    protected void grdPetitionAppealPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            petObject.AppealId = Convert.ToInt32(grdPetitionAppealPdf.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getFileNameForAppealpetition(petObject);
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_Var.dsFileName.Tables[0].Rows.Count; i++)
                {

                    //p_Var.sbuilder.Append("<a href='" + p_Var.url + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" />" + "</a>");
                    p_Var.sbuilder.Append("<asp:label >" + p_Var.dsFileName.Tables[0].Rows[i]["FileName"] + "," + p_Var.dsFileName.Tables[0].Rows[i]["Comments"] + "</Label>");

                    p_Var.sbuilder.Append("<br/><hr/>");

                }
                ltrlConnectedFile1.Text = p_Var.sbuilder.ToString();

            }

            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
            Label lblPetitionerName = (Label)e.Row.FindControl("lblPetitionerName");
            Label lblPetitionerAddress = (Label)e.Row.FindControl("lblPetitionerAddress");
            Label lblRespondentName = (Label)e.Row.FindControl("lblRespondentName");
            Label lblRespondentAddress = (Label)e.Row.FindControl("lblRespondentAddress");

            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
            lblPetitionerName.Text = HttpUtility.HtmlDecode(lblPetitionerName.Text);
            lblPetitionerAddress.Text = HttpUtility.HtmlDecode(lblPetitionerAddress.Text);
            lblRespondentName.Text = HttpUtility.HtmlDecode(lblRespondentName.Text);
            lblRespondentAddress.Text = HttpUtility.HtmlDecode(lblRespondentAddress.Text);
        }
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        BindGridDetails();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AppealPetition_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdPetitionAppealPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();
    }

    #region Function to bind gridView with petitions

    public void BindGridDetails()
    {

        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdAppeal.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdPetitionAppealPdf.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);

            petObject.year = ddlYear.SelectedValue;
            p_Var.dSet = pet_TempRecordBL.get_Temp_Appeal_Petition_Records(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {



                grdPetitionAppealPdf.DataSource = p_Var.dSet;
                grdPetitionAppealPdf.DataBind();
                p_Var.dSet = null;


                p_Var.dSet = null;
                lblmsg.Visible = false;
            }


        }

    }

    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
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
                        Label lblProNumber = (Label)row.FindControl("lblProNumber");
                        ViewState["RAProNumber"] = lblProNumber.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>Appeal against Order - " + lblProNumber.Text + "<br/> </b>");
                            Label lblid = (Label)row.FindControl("lblPA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.TempPAId = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                           
                            p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);

                        }
                    }
                    if (p_Var.Result > 0)
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
                            p_Var.sbuilder.Append("Record pending for Review  : " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
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
                        string textmessage;
                        string strUrl = sbuilderSms.ToString();
                        string[] split = strUrl.Split(';');
                        ArrayList list = new ArrayList();
                        foreach (string item in split)
                        {
                            list.Add(item.Trim());
                        }

                        
                            //code to get multiple public notice in list

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
                                                textmessage = "HERC - Record pending for review -";

                                                miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                            }

                                        }
                                   
                                }
                            }

                        /* End */

                        Session["msg"] = "Appeal record has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }

                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdAppeal.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblProNumber = (Label)row.FindControl("lblProNumber");
                        ViewState["RAProNumber"] = lblProNumber.Text;
                        if ((selCheck.Checked == true))
                        {
                            Label lblid = (Label)row.FindControl("lblPA_ID");
                            sbuilder.Append("<b>Appeal against Order - " + lblProNumber.Text + "<br/> </b>");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.TempPAId = p_Var.dataKeyID;
                            petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            petObject.userID = Convert.ToInt16(Session["User_Id"]);

                            p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
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
                                int statindex = li.Text.IndexOf("(") + 1;
                                p_Var.sbuildertmp.Append(li.Text.Substring(li.Text.IndexOf("(") + 1, li.Text.LastIndexOf(")") - statindex) + ";");
                                sbuilderSms.Append(li.Value + ";");
                            }

                        }
                        if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                        {
                            p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
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
                        string textmessage;
                        string strUrl = sbuilderSms.ToString();
                        string[] split = strUrl.Split(';');
                        ArrayList list = new ArrayList();
                        foreach (string item in split)
                        {
                            list.Add(item.Trim());
                        }

                       
                            //code to get multiple public notice in list

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
                                                textmessage = "HERC - Record pending for Publish -";

                                                miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                            }

                                        }
                                   
                                }
                            }

                           
                        
                        /* End */
                        Session["msg"] = "Appeal record has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }

                else //Here code is to approve records on date 12-05-2014
                {
                    LinkOB obj_linkOB1 = new LinkOB();
                    foreach (GridViewRow row in grdAppeal.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblProNumber = (Label)row.FindControl("lblProNumber");
                        ViewState["RAProNumber"] = lblProNumber.Text;
                        if ((selCheck.Checked == true))
                        {

                            Label lblid = (Label)row.FindControl("lblPA_ID");
                            sbuilder.Append("<b>Appeal against Order - " + lblProNumber.Text + "<br/> </b>");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            petObject.TempPAId = p_Var.dataKeyID;
                            petObject.IpAddress = Miscelleneous_DL.getclientIP();
                            petObject.userID = Convert.ToInt16(Session["User_Id"]);
                            p_Var.Result = petPetitionBL.ASP_Insert_Web_Petiton_APPEAL(petObject);

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
                            p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString() + "<br/>");
                            p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
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
                                                textmessage = "HERC - Record published -";

                                                miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                            }

                                        }
                                    
                                }
                            }

                           
                       
                        /* End */

                        Session["msg"] = "Appeal record has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                        Label lblid = (Label)row.FindControl("lblPA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPAId = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
                        if (p_Var.Result > 0)
                        {
                            Session["msg"] = "Appeal record has been sent for review successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                        }

                    }
                }
            }
            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblPA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPAId = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        petObject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
                        if (p_Var.Result > 0)
                        {
                            Session["msg"] = "Appeal record has been sent for publish successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                        }

                    }
                }
            }
            else
            {
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {

                        Label lblid = (Label)row.FindControl("lblPA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPAId = p_Var.dataKeyID;
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        petObject.userID = Convert.ToInt16(Session["User_Id"]);
                        p_Var.Result = petPetitionBL.ASP_Insert_Web_Petiton_APPEAL(petObject);

                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Appeal record has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                foreach (GridViewRow row in grdAppeal.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblProNumber = (Label)row.FindControl("lblProNumber");
                    ViewState["DisAProNumber"] = lblProNumber.Text;
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblPA_ID");
                        sbuilder.Append("<b>Appeal against Order - " + lblProNumber.Text + ";<br/> </b>");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPAId = p_Var.dataKeyID;
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

                        p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
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
                        p_Var.sbuilder.Append("Record disapproved : " + sbuilder.ToString() + "<br/>");
                        p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                        p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                        //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                        string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        p_Var.sbuildertmp.Append(email);
                        miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "HERC - Record disapproved", "no-reply.herc@nic.in", p_Var.sbuilder.ToString());

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
                                            textmessage = "HERC - Record disapproved -";

                                            miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                        }

                                    }
                               
                            }
                        }
                      
                   
                    /* End */
                    Session["msg"] = "Appeal record has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                }
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
                    Label lblid = (Label)row.FindControl("lblPA_ID");

                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    petObject.TempPAId = p_Var.dataKeyID;
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
                    p_Var.Result = petPetitionBL.ASP_Temp_Appeal_Petition_Update_Status_Id(petObject);
                }

            }

            if (p_Var.Result > 0)
            {
                Session["msg"] = "Appeal record has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition_Appeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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
        pnlPopUpEmails.Visible = false;
    }

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDDiaapproves()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
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
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
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

    #region Function to bind emailid of reviewers status in checkboxlist

    public void bindCheckBoxListWithEmailIDs()
    {

        try
        {
            obj_userOB.DepttId = Convert.ToInt16(Module_ID_Enum.hercType.herc);
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
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
            obj_userOB.ModuleId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
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
}
