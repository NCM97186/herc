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

public partial class Auth_AdminPanel_RTI_DisplayRTISAA : CrsfBase //ystem.Web.UI.Page
{
    //Area for all the variables declaration zone

    #region Data declaration zone

    Project_Variables p_Var = new Project_Variables();
    PetitionOB rtiObject = new PetitionOB();
    RtiBL rtiBL = new RtiBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    PaginationBL pagingBL = new PaginationBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    #endregion

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Rti"].ToString() + "/";
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View RTI SAA";
        this.Page.Title = " RTI SAA: HERC";
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
            this.Page.Form.Enctype = "multipart/form-data";
            //rptPager.Visible     = false;
            //lblPageSize.Visible  = false;
            //ddlPageSize.Visible  = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            //btnAddRTI.Visible = false;

            if (Session["SAAyear"] != null)
            {
                bindRtiSAAYearinDdl();
                ddlYear.SelectedValue = Session["SAAyear"].ToString();
            }
            else
            {
                bindRtiSAAYearinDdl();
            }
            if (Session["SAALng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["SAALng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["SAAdeptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["SAAdeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();

            }
            if (Session["SAAStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["SAAStatus"].ToString();
                Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            }
            else
            {
                binddropDownlistStatus();

            }


        }
    }

    #endregion

    //Area for all the dropDownlist events

    #region dropDownlist ddlDepartment selectedIndexChanged event

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        // grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
        Session["SAAdeptt"] = ddlDepartment.SelectedValue;

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

    #region dropDownlist ddlLanguage selectedIndexChanged event

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdRTISAA.Visible = false;
            Session["SAALng"] = ddlLanguage.SelectedValue;
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

    #region dropDownlist ddlStatus selectedIndexChanged events

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdRTISAA.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                grdRTISAA.Columns[15].Visible = true;
                grdRTISAA.Columns[13].Visible = false;
            }
            else
            {
                grdRTISAA.Columns[15].Visible = false;
                grdRTISAA.Columns[13].Visible = true;
            }
            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            Session["SAAStatus"] = ddlStatus.SelectedValue;
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

    #region dropDownlist ddlRTIStatusUpdate selectedIndexChanged event

    protected void ddlRTIStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.judgement))
        {
            trUpLoader.Visible = true;
            trUpLoaderUrl.Visible = true;
            lbluploader.Visible = false;
            lnkFileRemove.Visible = false;
            lblOther.Visible = false;
        }
        else
        {
            trUpLoader.Visible = false;
            trUpLoaderUrl.Visible = false;
        }
        //19 jan 2013
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
        {
            txtOther.Visible = true;
            DivCommon.Visible = true;
            lblAuthority.Visible = true;
            lblOther.Visible = false;
        }
        else
        {
            txtOther.Visible = false;
            DivCommon.Visible = false;
            lblAuthority.Visible = false;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
        {
            trAnyOther.Visible = true;
            trUpLoader.Visible = true;// 20 may 2013 by ruchi 
            lbluploader.Visible = false;
            lnkFileRemove.Visible = false;
        }
        else
        {
            trAnyOther.Visible = false;
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

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion

    //End

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnUpdateStatus click event to change status

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        //{
        p_Var.rtiid = Convert.ToInt32(ViewState["id"]);
        rtiObject.RTISaaId = p_Var.rtiid;
        rtiObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
        if (Upload_File(ref p_Var.Filename))
        {
            if (p_Var.Filename != null)
            {
                // rtiObject.FileName = fileUploadPdf.FileName.ToString();
                rtiObject.FileName = p_Var.Filename.ToString();
            }


        }
        else
        {
            if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
            {
                lbluploader.Visible = false;
                if (lblOther.Text != null && lblOther.Text != "")
                {
                    rtiObject.FileName = lblOther.Text;
                }
                else
                {
                    rtiObject.FileName = null;
                }

            }
            else
            {
                lblOther.Visible = false;
                if (lbluploader.Text != null && lbluploader.Text != "")
                {
                    rtiObject.FileName = lbluploader.Text;
                }
                else
                {
                    rtiObject.FileName = null;
                }

            }
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
        {
            rtiObject.subject = txtAnyOther.Text;
            rtiObject.StatusId = Convert.ToInt32(Module_ID_Enum.rti_Status.Rti_StatusTypeId);
            p_Var.Result = rtiBL.Insert_Status(rtiObject, out p_Var.k);
            rtiObject.RTISAAStatusId = p_Var.Result;
            // rtiObject.subject = txtAnyOther.Text;
        }
        else
        {
            rtiObject.RTISAAStatusId = Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue);
            rtiObject.subject = null;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.judgement))
        {
            rtiObject.JudgementLink = txtUrl.Text;
        }
        else
        {
            rtiObject.JudgementLink = null;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
        {
            rtiObject.MemoNo = txtMemo.Text.ToString();
            rtiObject.Date = miscellBL.getDateFormat(Request.Form[txtDate.UniqueID]);
            rtiObject.TransferAuthority = txtOther.Text;

        }
        else
        {
            rtiObject.MemoNo = null;
            rtiObject.Date = null;
            rtiObject.TransferAuthority = null;
            //rtiObject.JudgementLink = null;
        }


        p_Var.Result = rtiBL.modifyRtiSAAStatus(rtiObject);
        if (p_Var.Result > 0)
        {

            obj_audit.ActionType = "U";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.status = ddlStatus.SelectedItem.ToString();

            obj_audit.Title = Session["RTISAATitle"].ToString();
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

            Session["msg"] = "RTI-SAA Application's status has been updated successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }
        //}
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

    #region button btnForApprove click event to send records for approve

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

        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //this.Bind_Grid(Convert.ToInt32(lstCMSMenu.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlLanguage.SelectedValue), Convert.ToInt32(ddlMenuPosition.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), pageIndex);
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion

    #region button btnRtiFaa click event to redirect page

    protected void btnRtiFaa_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    //End

    //Area for all the gridView events

    #region gridView grdRTISAA rowCommand event

    protected void grdRTISAA_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    bindRtiStatus();
                    rtiObject.RTISaaId = Convert.ToInt32(e.CommandArgument.ToString());
                    rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    p_Var.dSetChildData = rtiBL.getTempRTISAARecordsBYID(rtiObject);

                    Label lblSAAReference1 = row.FindControl("lblSAAReference1") as Label;
                    Session["RTISAATitle"] = "RTI-SAA/Ref No. " + lblSAAReference1.Text + " of " + ddlYear.SelectedItem.ToString();


                    if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
                    {
                        lblRefNoSAA.Text = "RTI Ref No: " + p_Var.dSetChildData.Tables[0].Rows[0]["ref_no"].ToString() + ", RTI FAA No: " + p_Var.dSetChildData.Tables[0].Rows[0]["FAAref"].ToString() + ", RTI SAA No: " + p_Var.dSetChildData.Tables[0].Rows[0]["SAA_RefNo"].ToString();
                        if (Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_SAA_Status_Id1"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.replysent) && Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_SAA_Status_Id1"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.judgement) && Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_SAA_Status_Id1"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                        {

                            ddlRTIStatusUpdate.SelectedValue = Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther).ToString();
                            txtAnyOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["anyother"].ToString();
                            trAnyOther.Visible = true;
                            DivCommon.Visible = false;
                            // on date 5 Oct 2013
                            tdother.Visible = false;
                            trUpLoaderUrl.Visible = false;
                            trUpLoader.Visible = true;
                            lbluploader.Visible = false;
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                            {

                                lnkFileRemove.Visible = true;
                                lblOther.Visible = true;
                                lblOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Filename"].ToString();

                            }
                            else
                            {
                                lblOther.Visible = false;
                                lnkFileRemove.Visible = false;
                                //lbluploader.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Filename"].ToString();
                            }

                        }

                        else if (Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_SAA_Status_Id1"]) == Convert.ToInt32(Module_ID_Enum.rti_Status.judgement))
                        {
                            ddlRTIStatusUpdate.SelectedValue = p_Var.dSetChildData.Tables[0].Rows[0]["RTI_SAA_Status_Id1"].ToString();
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                            {
                                trUpLoader.Visible = true;
                                lnkFileRemove.Visible = true;
                                lbluploader.Visible = true;
                                lbluploader.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Filename"].ToString();
                                txtUrl.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderFour"].ToString();
                                trAnyOther.Visible = false;
                                trUpLoaderUrl.Visible = true;
                                lblOther.Visible = false;
                                RequiredFieldValidator3.Visible = false;
                            }
                            else
                            {
                                trUpLoader.Visible = true;
                                txtUrl.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderFour"].ToString();
                                lnkFileRemove.Visible = false;
                                trAnyOther.Visible = false;
                                trUpLoaderUrl.Visible = true;
                                lblOther.Visible = false;
                                lbluploader.Visible = false;
                                RequiredFieldValidator3.Visible = false;
                            }
                        }
                        else if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
                        {
                            //7 feb 
                            trAnyOther.Visible = true;
                            txtAnyOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderFive"].ToString();
                            //trUpLoader.Visible = false; //today 20 may 2013
                            trUpLoaderUrl.Visible = false;
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                            {
                                trUpLoader.Visible = true;
                                lnkFileRemove.Visible = true;
                                lblOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Filename"].ToString();

                            }
                            else
                            {

                                lnkFileRemove.Visible = false;
                                //lbluploader.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Filename"].ToString();
                            }


                        }
                        else
                        {
                            txtOther.Visible = false;
                            tdother.Visible = false;
                            DivCommon.Visible = false;
                            lnkFileRemove.Visible = false;
                            trUpLoader.Visible = false;
                            trAnyOther.Visible = false;
                            trUpLoaderUrl.Visible = false;

                        }
                    }
                    ViewState["id"] = e.CommandArgument.ToString();
                    this.mpuUpdateStatus.Show();
                }
                else if (e.CommandName == "delete")
                {
                    p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
                    ViewState["commandArgs"] = p_Var.commandArgs;
                    p_Var.rtiid = Convert.ToInt32(p_Var.commandArgs[0]);
                    p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

                }
                else if (e.CommandName == "Restore")
                {

                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    rtiObject.RTISaaId = Convert.ToInt32(e.CommandArgument);
                    p_Var.Result = rtiBL.updateRtiSAAStatusDelete(rtiObject);
                    if (p_Var.Result > 0)
                    {

                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblSAAReference1 = row.FindControl("lblSAAReference1") as Label;
                        if (lblSAAReference1 == null) { return; }
                        obj_audit.Title = "RTI-SAA/Ref No. " + lblSAAReference1.Text + " of " + ddlYear.SelectedItem.ToString();
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "RTI-SAA Application has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                    else
                    {
                        Session["msg"] = "RTI-SAA Application has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    petObject.ModuleType = 2;
                    dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
                    Label lblprono = row.FindControl("lnk_Reference_Number") as Label;
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

    #region gridView grdRTISAA rowCreated event to select checkBoxes

    protected void grdRTISAA_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdRTISAA.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));
        }
    }

    #endregion

    #region gridView grdRTISAA rowDataBound events

    protected void grdRTISAA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //19 jan 2013
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);
            int RTID = Convert.ToInt32(((Label)e.Row.FindControl("lblRTISAA_ID")).Text);

            Label lblSAAReference = (Label)e.Row.FindControl("lblSAAReference");
            if (lblSAAReference.Text != null && lblSAAReference.Text != "")
            {
                lblSAAReference.Text = HttpUtility.HtmlDecode(lblSAAReference.Text);
            }
            //This is for change status

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
            {
                LinkButton lnkChangeStatus = (LinkButton)e.Row.FindControl("lnkChangeStatus");
                lnkChangeStatus.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to change status of this RTI-SAA Application NO- " +
                DataBinder.Eval(e.Row.DataItem, "SAA_RefNo") + "')");

            }

            //END

            //This is for delete/permanently delete 30 may 2013 


            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
                ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge this RTI-SAA Application No- " + DataBinder.Eval(e.Row.DataItem, "SAA_RefNo") + " permanently? " + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
              "confirm('Are you sure want to restore RTI-SAA Application No- " + DataBinder.Eval(e.Row.DataItem, "SAA_RefNo") + "')");
            }
            else
            {
                ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
                ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete RTI-SAA Application No- " + DataBinder.Eval(e.Row.DataItem, "SAA_RefNo") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
              "confirm('Are you sure want to restore RTI-SAA Application No- " + DataBinder.Eval(e.Row.DataItem, "SAA_RefNo") + "')");
            }

            //END


            //use for format of TransferedAuthority  status
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.RTISaaId = RTID;
            p_Var.dSetChildData = rtiBL.getTempRTISAARecordsBYID(rtiObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                {
                    ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/>vide " + "Memo No:" + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
                }
            }

            //Download file 
            Literal orderFile = (Literal)e.Row.FindControl("ltrlFile");
            if (orderFile.Text != null && orderFile.Text != "")
            {
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.sbuilder.Append("<a href='" + p_Var.url + orderFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                orderFile.Text = p_Var.sbuilder.ToString();
            }


        }
    }

    #endregion

    #region gridView grdRTISAA rowDeleting events

    protected void grdRTISAA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdRTISAA.Rows[e.RowIndex];
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rtiid = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        rtiObject.RTISaaId = p_Var.rtiid;
        rtiObject.TempRTISAAId = p_Var.rtiid;
        rtiObject.StatusId = p_Var.status_id;

        p_Var.Result = rtiBL.Delete_RTISAA(rtiObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "RTI-SAA Application has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "RTI-SAA Application has been deleted successfully.";
            }
            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.status = ddlStatus.SelectedItem.ToString();
            Label lblSAAReference1 = row.FindControl("lblSAAReference1") as Label;
            if (lblSAAReference1 == null) { return; }
            obj_audit.Title = "RTI-SAA/Ref No. " + lblSAAReference1.Text + " of " + ddlYear.SelectedItem.ToString();
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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
                    usrObject.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);
                    usrObject.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.english).Append(",");
                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][9]) == true)
                {
                    usrObject.hindi = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Hindi);

                    p_Var.sbuilder.Append(usrObject.hindi);
                }
                else if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][10]) == true)
                {
                    usrObject.english = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.English);

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
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                // btnAddRTI.Visible = true;

                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                // btnAddRTI.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                // p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover));
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete));
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
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
            p_Var.dSet = obj_UserBL.ASP_Get_Deptt_Name(obj_userOB);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = p_Var.dSet;
                ddlDepartment.DataValueField = "Deptt_Id";
                ddlDepartment.DataTextField = "Deptt_Name";
                ddlDepartment.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind gridView with rti second appellate authority

    public void Bind_Grid(int departmentid, string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdRTISAA.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdRTISAA.Visible = true;
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.DepttId = departmentid;
            rtiObject.year = ddlYear.SelectedValue.ToString();
            // rtiObject.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            p_Var.dSet = rtiBL.getRtiSaaTempRecords(rtiObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdRTISAA.Columns[14].HeaderText = "Purge";
                    grdRTISAA.Columns[13].Visible = false;

                }
                else
                {
                    grdRTISAA.Columns[14].HeaderText = "Delete";
                    grdRTISAA.Columns[15].Visible = false;
                }

                if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                {
                    grdRTISAA.Columns[3].HeaderText = "Ref No (HERC/RTI)";
                    grdRTISAA.Columns[4].HeaderText = "FAA Ref No (HERC/FAA)";
                    grdRTISAA.Columns[5].HeaderText = "SAA Ref No (HERC/SAA)";

                }
                else
                {
                    grdRTISAA.Columns[3].HeaderText = "Ref No (EO/RTI)";
                    grdRTISAA.Columns[4].HeaderText = "FAA Ref No (EO/FAA)";
                    grdRTISAA.Columns[5].HeaderText = "SAA Ref No (EO/SAA)";

                }

                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End
                grdRTISAA.DataSource = myDataView;
                grdRTISAA.DataBind();
                p_Var.dSet = null;

                Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
                Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
                obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
                obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);
                if (p_Var.dSet.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdRTISAA.Columns[0].Visible = false;
                        grdRTISAA.Columns[7].Visible = true;
                        //p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare();

                        foreach (GridViewRow row in grdRTISAA.Rows)
                        {

                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblPetitionID = (Label)row.FindControl("lblRTISAA_ID");
                            Label lblchangestatus = (Label)row.FindControl("lblchangestatus");
                            LinkButton lnkChangeStatus = (LinkButton)row.FindControl("lnkChangeStatus");
                            rtiObject.RTISaaId = Convert.ToInt32(lblPetitionID.Text);

                            p_Var.dSetCompare = rtiBL.getIdForrtiSAA_Comparison(rtiObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_RTI_SAA_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblPetitionID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_RTI_SAA_Id"]))
                                    {
                                        imgnotedit.Visible = true;
                                        imgedit.Visible = false;
                                        // 27 Aug 2013
                                        lblchangestatus.Visible = true;
                                        lnkChangeStatus.Visible = false;
                                        //End
                                    }
                                    else
                                    {
                                        imgnotedit.Visible = false;
                                        imgedit.Visible = true;
                                        lblchangestatus.Visible = false;
                                        lnkChangeStatus.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        grdRTISAA.Columns[0].Visible = true;
                        // grdRTISAA.Columns[7].Visible = false;
                    }
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                    {
                        btnForReview.Visible = true;
                    }
                    else
                    {
                        btnForReview.Visible = false;
                    }

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        btnForApprove.Visible = true;
                        btnDisApprove.Visible = true;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
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
                        grdRTISAA.Columns[13].Visible = true; //This is for Edit
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                        {
                            grdRTISAA.Columns[13].Visible = true; //10 previous value
                            grdRTISAA.Columns[17].Visible = false;
                        }
                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                            {
                                grdRTISAA.Columns[17].Visible = true;
                            }
                            else
                            {
                                grdRTISAA.Columns[17].Visible = false;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                {
                                    grdRTISAA.Columns[17].Visible = true;
                                }
                                else
                                {
                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                    {
                                        grdRTISAA.Columns[17].Visible = true;
                                    }
                                    else
                                    {
                                        grdRTISAA.Columns[17].Visible = false;
                                    }
                                }

                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdRTISAA.Columns[13].Visible = false; //10 previous value
                                    grdRTISAA.Columns[17].Visible = false;
                                }
                                else
                                {
                                    grdRTISAA.Columns[13].Visible = true; //10 previous value
                                }
                            }
                            else
                            {
                                //grdRTISAA.Columns[13].Visible = false; //10 previous value
                                grdRTISAA.Columns[17].Visible = false;
                            }
                        }
                    }
                    else
                    {
                        grdRTISAA.Columns[13].Visible = false; //10 previous value
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdRTISAA.Columns[11].Visible = false;  //This is for Appeal
                            //grdRTISAA.Columns[14].Visible = false; // This is for delete 
                            grdRTISAA.Columns[15].Visible = true;
                            grdRTISAA.Columns[17].Visible = false;

                        }
                        else
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdRTISAA.Columns[11].Visible = false;//This is for Appeal
                            }
                            else
                            {
                                grdRTISAA.Columns[11].Visible = false; //This is for Appeal
                            }
                            ////grdRTISAA.Columns[13].Visible = true;  //Commented on date 14 Oct 2013
                            grdRTISAA.Columns[15].Visible = false;



                        }
                        // modify on date 23 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdRTISAA.Columns[14].Visible = true;  // This is for delete
                                grdRTISAA.Columns[17].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdRTISAA.Columns[14].Visible = true;
                                    grdRTISAA.Columns[17].Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdRTISAA.Columns[17].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdRTISAA.Columns[17].Visible = true;
                                            }
                                            else
                                            {
                                                grdRTISAA.Columns[17].Visible = false;
                                            }
                                        }

                                        grdRTISAA.Columns[14].Visible = true;

                                    }
                                    else
                                    {
                                        grdRTISAA.Columns[14].Visible = false;
                                        grdRTISAA.Columns[17].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdRTISAA.Columns[14].Visible = true;
                                grdRTISAA.Columns[17].Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdRTISAA.Columns[14].Visible = true;
                            }

                        }

                        //End  

                    }
                    else
                    {
                        grdRTISAA.Columns[14].Visible = false;
                    }

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {

                        grdRTISAA.Columns[10].Visible = true; //This is for change status
                    }
                    else
                    {
                        grdRTISAA.Columns[10].Visible = false; //This is for change status
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {

                grdRTISAA.Visible = false;
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
        // Session["priv"] = p_Var.dSet;     //session hold the dsprv values  
    }

    #endregion


    #region Function to bind status of rti

    public void bindRtiStatus()
    {
        p_Var.dSet = rtiBL.getRtiStatus_SAA();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlRTIStatusUpdate.DataSource = p_Var.dSet;
            ddlRTIStatusUpdate.DataTextField = "STATUS";
            ddlRTIStatusUpdate.DataValueField = "STATUS_ID";
            ddlRTIStatusUpdate.DataBind();
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdRTISAA.Visible = false;
        Session["SAAyear"] = ddlYear.SelectedValue;
    }

    #region Function to bind Rti Year

    public void bindRtiSAAYearinDdl()
    {
        p_Var.dSet = rtiBL.GetYearRTISAAAdmin();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
    }

    #endregion


    #region button btnRti click event to redirect page

    protected void btnRti_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    //Codes for sorting of the grid



    protected void grdRTISAA_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);
    }

    #region gridView grdRTISAA pageIndexChanging Event zone

    protected void grdRTISAA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRTISAA.PageIndex = e.NewPageIndex;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    //End
    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=+5"));
    }

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {
        rtiObject.ActionType = 3;
        rtiObject.RTIId = Convert.ToInt32(ViewState["id"]);

        //p_Var.Result1 = rtiBL.updateRTIweb(rtiObject);
        lbluploader.Visible = false;
        lnkFileRemove.Visible = false;
        lblOther.Visible = false;

        lbluploader.Text = null;

        lblOther.Text = null;
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

    public void BindGridDetails()
    {
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdRTISAAPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdRTISAAPdf.Visible = true;

            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            rtiObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = rtiBL.getRtiSaaTempRecords(rtiObject, out p_Var.k);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                grdRTISAAPdf.DataSource = p_Var.dSet;
                grdRTISAAPdf.DataBind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RTISAA_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdRTISAAPdf.AllowPaging = false;
        grdRTISAAPdf.DataBind();
        grdRTISAAPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    protected void grdRTISAAPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            Label lblApplicantAddress = (Label)e.Row.FindControl("lblApplicantAddress");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");


            lblApplicant.Text = HttpUtility.HtmlDecode(lblApplicant.Text);
            lblApplicantAddress.Text = HttpUtility.HtmlDecode(lblApplicantAddress.Text);
            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);

        }
    }
    protected void btnSendEmails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilderSms = new StringBuilder();
                StringBuilder sbuilder = new StringBuilder();
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdRTISAA.Rows)
                    {
                        Label lblSAAReference = (Label)row.FindControl("lblSAAReference");
                        ViewState["RTISAA"] = lblSAAReference.Text;
                        Label lblyear = (Label)row.FindControl("lblyear");
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI-SAA -</b>" + lblSAAReference.Text + " of " + lblyear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTISAAId = p_Var.dataKeyID;
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = rtiBL.updateRtiSAAStatus(rtiObject);
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
                            p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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
                        string textmessage;
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
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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

                        Session["msg"] = "RTI-SAA Application has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }

                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdRTISAA.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSAAReference = (Label)row.FindControl("lblSAAReference");
                        ViewState["RTISAA"] = lblSAAReference.Text;
                        Label lblyear = (Label)row.FindControl("lblyear");
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI-SAA -</b>" + lblSAAReference.Text + " of " + lblyear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTISAAId = p_Var.dataKeyID;
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = rtiBL.updateRtiSAAStatus(rtiObject);
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
                            p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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
                        string textmessage;
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
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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

                        Session["msg"] = "RTI-SAA Application has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    LinkOB obj_linkOB1 = new LinkOB();
                    foreach (GridViewRow row in grdRTISAA.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblSAAReference = (Label)row.FindControl("lblSAAReference");
                        Label lblyear = (Label)row.FindControl("lblyear");
                        ViewState["RTISAA"] = lblSAAReference.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI-SAA -</b>" + lblSAAReference.Text + " of " + lblyear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTISAAId = p_Var.dataKeyID;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = rtiBL.InsertRtiSAAIntoWeb(rtiObject);
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
                            p_Var.sbuilder.Append("Record Published : " + sbuilder.ToString());
                            p_Var.sbuilder.Append("<br/>from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                            p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
                            //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                            string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                            p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                            p_Var.sbuildertmp.Append(email);
                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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
                        string textmessage;
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
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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
                        Session["msg"] = "RTI-SAA Application has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                foreach (GridViewRow row in grdRTISAA.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTISAAId = p_Var.dataKeyID;
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = rtiBL.updateRtiSAAStatus(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI-SAA Application has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdRTISAA.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTISAAId = p_Var.dataKeyID;
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = rtiBL.updateRtiSAAStatus(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI-SAA Application has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }

            else
            {

                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in grdRTISAA.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTISAAId = p_Var.dataKeyID;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = rtiBL.InsertRtiSAAIntoWeb(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI-SAA Application has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                foreach (GridViewRow row in grdRTISAA.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblSAAReference = (Label)row.FindControl("lblSAAReference");
                    Label lblyear = (Label)row.FindControl("lblyear");
                    ViewState["DisRTISAA"] = lblSAAReference.Text;

                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>RTI-SAA -</b>" + lblSAAReference.Text + " of " + lblyear.Text + "<br/> ");
                        Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTISAAId = p_Var.dataKeyID;
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                        {
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                        }
                        else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                        {
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        }
                        else
                        {
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        }

                        p_Var.Result = rtiBL.updateRtiSAAStatus(rtiObject);
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
                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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
                    string textmessage;
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
                                    if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
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

                    Session["msg"] = "RTI-SAA Application has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
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
            foreach (GridViewRow row in grdRTISAA.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblRTISAA_ID");
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    rtiObject.TempRTISAAId = p_Var.dataKeyID;
                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                    {
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    }
                    else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                    {
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                    }
                    else
                    {
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                    }
                    //rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    p_Var.Result = rtiBL.updateRtiSAAStatus(rtiObject);
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "RTI-SAA Application has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
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
            obj_userOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
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

    #region Custom validator to validate extension of upload pdf files

    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        p_Var.ext = System.IO.Path.GetExtension(fileUploadPdf.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        if (p_Var.ext == ".pdf")
        {
            p_Var.flag = miscellBL.GetActualFileType_pdf(fileUploadPdf.PostedFile.InputStream);
        }
        else
        {
            p_Var.flag = miscellBL.GetActualFileType(fileUploadPdf.PostedFile.InputStream);
        }

        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }

        /*        string fileMultiple = string.Empty;
                HttpFileCollection hfc = Request.Files;
                bool strem = true; ;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                    strem = miscellBL.GetActualFileType(hfc[i].InputStream);
                    if (strem == true)
                    {

                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = false;
                        break;
                    }

                }   */

    }

    #endregion
}
