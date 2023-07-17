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

public partial class Auth_AdminPanel_RTI_DisplayRTIFAA : CrsfBase //System.Web.UI.Page
{
    //Area for all the data declaration zone

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
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  RTI FAA";
        this.Page.Title = " RTI FAA: HERC";
        p_Var.url = ResolveUrl("~/") + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Rti"].ToString() + "/";
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
            //rptPager.Visible = false;
            //lblPageSize.Visible = false;
            //ddlPageSize.Visible = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            //btnAddRTI.Visible = false;

            if (Session["FAAyear"] != null)
            {
                bindRtiFAAYearinDdl();
                ddlYear.SelectedValue = Session["FAAyear"].ToString();
            }
            else
            {
                bindRtiFAAYearinDdl();
            }

            if (Session["FAALng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["FAALng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["FAAdeptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["FAAdeptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
            if (Session["FAAStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["FAAStatus"].ToString();
                Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            }
            else
            {
                binddropDownlistStatus();
            }


        }
    }

    #endregion

    //Area for all the buttons, imageButtons, linkButtons click events

    #region button btnUpdateStatus click event to update rti first appellate authority

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        //{
        p_Var.rtiid = Convert.ToInt32(ViewState["id"]);
        rtiObject.RTIFAAId = p_Var.rtiid;
        rtiObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
        {

            rtiObject.subject = txtOther.Text;

            rtiObject.StatusId = Convert.ToInt32(Module_ID_Enum.rti_Status.Rti_StatusTypeId);
            p_Var.Result = rtiBL.Insert_Status(rtiObject, out p_Var.k);
            rtiObject.RTIFAAStatusId = p_Var.Result;

        }
        else
        {
            rtiObject.RTIFAAStatusId = Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue);
        }
        if (Upload_File(ref p_Var.Filename))
        {
            if (p_Var.Filename != null)
            {
                rtiObject.FileName = p_Var.Filename.ToString();
                lbluploader.Text = p_Var.Filename.ToString();

            }

        }
        else
        {
            if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
            {
                lblTransferUploader.Visible = false;
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
            else if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
            {
                lblTransferUploader.Visible = false;
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
                lbluploader.Visible = false;
                if (lblTransferUploader.Text != null && lblTransferUploader.Text != "")
                {
                    rtiObject.FileName = lblTransferUploader.Text;
                }
                else
                {
                    rtiObject.FileName = null;
                }

            }
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
        {
            rtiObject.MemoNo = txtMemo.Text.ToString();
            if (txtDate.Text != null && txtDate.Text != "")
            {
                rtiObject.Date = miscellBL.getDateFormat(txtDate.Text);
            }
            else
            {
                rtiObject.Date = null;
            }

        }
        else
        {
            rtiObject.MemoNo = null;
            rtiObject.Date = null;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
        {
            rtiObject.MemoNo = txtMemo.Text.ToString();
            rtiObject.Date = miscellBL.getDateFormat(Request.Form[txtDate.UniqueID]);
            rtiObject.TransferAuthority = txtOther.Text;
        }
        else
        {
            rtiObject.TransferAuthority = null;
        }

        p_Var.Result = rtiBL.modifyRtiFAAStatus(rtiObject);
        if (p_Var.Result > 0)
        {

            obj_audit.ActionType = "U";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.status = ddlStatus.SelectedItem.ToString();

            obj_audit.Title = Session["RTIFAATitle"].ToString();
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


            Session["msg"] = "RTI-FAA Application's status has been updated successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }
        // }
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

    #region button btnForApprove click event to send records for approval

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

    #region button btnRtiSaa click event to redirect page to another

    protected void btnRtiSaa_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    #region button btnRti click event to redirect page

    protected void btnRti_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    #region button btnDelete click event to delete petition

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rtiid = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        rtiObject.RTIFAAId = p_Var.rtiid;
        rtiObject.TempRTIFAAId = p_Var.rtiid;
        rtiObject.StatusId = p_Var.status_id;

        p_Var.Result = rtiBL.Delete_RTIFAA(rtiObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "RTI-FAA Application has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "RTI-FAA Application has been deleted successfully.";
            }

            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }
    }

    #endregion

    //End

    //Area for all the dropDownlist events

    #region dropDownlist ddlDepartment selectedIndexChanged event to bind grid

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        // grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible = false;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
        Session["FAAdeptt"] = ddlDepartment.SelectedValue;

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
            grdRTIFAA.Visible = false;
            Session["FAALng"] = ddlLanguage.SelectedValue;
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
            grdRTIFAA.Visible = false;
            btnForReview.Visible = false;
            btnApprove.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
            {
                grdRTIFAA.Columns[9].Visible = true;
            }
            else
            {
                grdRTIFAA.Columns[9].Visible = false;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                grdRTIFAA.Columns[14].Visible = true;
            }
            else
            {
                grdRTIFAA.Columns[14].Visible = false;
            }
            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            Session["FAAStatus"] = ddlStatus.SelectedValue;
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

    #region dropPageSize selectedIndexChanged event to do paginf

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
    }

    #endregion

    #region dropDownlist ddlRTISTatusUpdate selectedIndexChanged event

    protected void ddlRTIStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
        {
            DivUpLoader.Visible = true;
            DivCommon.Visible = true;
            lbluploader.Visible = false;
            lblTransferUploader.Visible = false;
            lnkFileRemove.Visible = false;
        }
        else
        {
            DivUpLoader.Visible = false;
            DivCommon.Visible = false;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
        {
            trAuthority.Visible = true;
            lblAuthority.Visible = false;
            DivUpLoader.Visible = true;//today added
            lblTransferUploader.Visible = false;//today added
            lnkFileRemove.Visible = false;//today added
            lbluploader.Visible = false;
            //txtOther.Visible = true;


        }
        else
        {
            trAuthority.Visible = false; ;
            // txtOther.Visible = false;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
        {
            trAuthority.Visible = true;
            lblAuthority.Visible = true;
            //txtOther.Visible = true;
            DivCommon.Visible = true;
            DivUpLoader.Visible = true;
            lbluploader.Visible = false;
            lblTransferUploader.Visible = false;
            lnkFileRemove.Visible = false;
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

    #region gridView grdRTIFAA rowCommand event

    protected void grdRTIFAA_RowCommand(object sender, GridViewCommandEventArgs e)
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
                if (e.CommandName == "Appeal")
                {
                    p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
                    p_Var.petition_id = Convert.ToInt32(p_Var.commandArgs[0]);
                    p_Var.pro_number = p_Var.commandArgs[1].ToString();
                    p_Var.id = p_Var.commandArgs[2].ToString();


                    Response.Redirect("~/Auth/AdminPanel/RTIModule/RTI_SAA_ADD.aspx?rti_FAA_id=" + p_Var.petition_id + "&Ref_Number=" + p_Var.pro_number + "&ModuleID=" + Request.QueryString["ModuleID"] + "&DepttID=" + p_Var.id);
                }
                else if (e.CommandName == "ChangeStatus")
                {
                    bindRtiStatus();
                    rtiObject.RTIFAAId = Convert.ToInt32(e.CommandArgument.ToString());
                    rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    p_Var.dSetChildData = rtiBL.getTempRTIFAARecordsBYID(rtiObject);

                    lblRefNoFAA.Text = "RTI Ref No: " + p_Var.dSetChildData.Tables[0].Rows[0]["Ref_No"].ToString() + ", RTI FAA No: " + p_Var.dSetChildData.Tables[0].Rows[0]["FAA"].ToString();
                    string refnum = p_Var.dSetChildData.Tables[0].Rows[0]["FAA"].ToString();
                    Session["RTIFAATitle"] = "RTI-FAA/Ref No. " + refnum + " of " + ddlYear.SelectedItem.ToString();


                    if (Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_FAA_Status_Id1"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.replysent) && Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_FAA_Status_Id1"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess) && Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_FAA_Status_Id1"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                    {
                        ddlRTIStatusUpdate.SelectedValue = Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther).ToString();
                        trAuthority.Visible = true;
                        txtOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["anyother"].ToString();

                        DivUpLoader.Visible = true;
                        DivCommon.Visible = false;
                        lbluploader.Visible = false;
                        lblTransferUploader.Visible = false;
                        //Today line added by ruchi on date 20 may 2013
                        if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                        {
                            lblOther.Visible = true;
                            lblOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString();
                            lnkFileRemove.Visible = true;
                        }
                        else
                        {
                            lnkFileRemove.Visible = false;

                        }
                    }
                    else if (Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_FAA_Status_Id1"]) == Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                    {
                        DivUpLoader.Visible = false;
                        DivCommon.Visible = false;
                        trAuthority.Visible = false;
                    }
                    else
                    {
                        ddlRTIStatusUpdate.SelectedValue = p_Var.dSetChildData.Tables[0].Rows[0]["RTI_FAA_Status_Id1"].ToString();
                        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
                        {
                            lblOther.Visible = false;
                            lblTransferUploader.Visible = false;
                            DivUpLoader.Visible = true;
                            DivCommon.Visible = true;
                            txtMemo.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString();
                            txtDate.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                            {
                                lbluploader.Visible = true;
                                lbluploader.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Filename"].ToString();
                                lnkFileRemove.Visible = true;
                            }
                            else
                            {
                                lnkFileRemove.Visible = false;
                                lbluploader.Visible = false;

                            }
                            trAuthority.Visible = false;

                        }
                        else
                        {
                            DivUpLoader.Visible = false;
                            DivCommon.Visible = false;
                        }
                        //19 Jan 2013 
                        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                        {
                            lblOther.Visible = false;
                            lbluploader.Visible = false;
                            //txtOther.Visible = true;
                            DivUpLoader.Visible = true;
                            trAuthority.Visible = true;
                            lblAuthority.Visible = true;
                            DivCommon.Visible = true;
                            txtMemo.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString();
                            txtDate.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
                            txtOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString();
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                            {
                                lblTransferUploader.Visible = true;
                                lblTransferUploader.Text = p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString();
                                lnkFileRemove.Visible = true;

                            }
                            else
                            {
                                lnkFileRemove.Visible = false;
                                lbluploader.Visible = false;
                            }
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
                    rtiObject.RTIFAAId = Convert.ToInt32(e.CommandArgument);
                    p_Var.Result = rtiBL.updateRtiFAAStatusDelete(rtiObject);
                    if (p_Var.Result > 0)
                    {
                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblFAAReference = row.FindControl("lblFAAReference") as Label;
                        //if (lblFAAReference == null) { return; }
                        obj_audit.Title = "RTI-FAA/Ref No. " + lblFAAReference.Text + " of " + ddlYear.SelectedItem.ToString();
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "RTI-FAA Application has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                    else
                    {
                        Session["msg"] = "RTI-FAA Application has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    petObject.ModuleType = 1;
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

    #region gridView grdRTIFAA rowCreated event to select checkboxes

    protected void grdRTIFAA_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdRTIFAA.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));
        }
    }

    #endregion

    #region gridView grdRTIFAA rowDataBound event

    protected void grdRTIFAA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);
            LinkButton lnkAppeal1 = (LinkButton)e.Row.FindControl("lnkAppeal");
            //This is for change status

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
            {
                LinkButton lnkChangeStatus = (LinkButton)e.Row.FindControl("lnkChangeStatus");
                lnkChangeStatus.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to change status of this RTI-FAA Application No- " +
                DataBinder.Eval(e.Row.DataItem, "FAA") + "')");


                lnkAppeal1.Attributes.Add("onclick", "javascript:return " +
               "confirm('Are you sure that appeal has been received with SAA against this RTI-FAA Application No- " +
               DataBinder.Eval(e.Row.DataItem, "FAA") + "')");

            }

            //END

            //This is for delete/permanently delete 30 may 2013 


            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
                ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge this RTI-FAA Application No- " + DataBinder.Eval(e.Row.DataItem, "FAA") + " permanently? " + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure want to restore RTI-FAA Application No- " + DataBinder.Eval(e.Row.DataItem, "FAA") + "')");
            }
            else
            {
                ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
                ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete RTI-FAA Application No- " + DataBinder.Eval(e.Row.DataItem, "FAA") + "')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure want to restore RTI-FAA Application No- " + DataBinder.Eval(e.Row.DataItem, "FAA") + "')");
            }

            //END
            //use for format of Reply sent status
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.RTIFAAId = RTID;
            p_Var.dSetChildData = rtiBL.getTempRTIFAARecordsBYID(rtiObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
                {
                    ((Label)e.Row.FindControl("lblstatus")).Text = ((Label)e.Row.FindControl("lblstatus")).Text + " " + "vide " + "<br/>Memo No:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
                }
                //19 jan 2013
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = ((Label)e.Row.FindControl("lblStatus")).Text + ": " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/>vide " + "Memo No:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " " + "Dated:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
                }
            }


            //((LinkButton)e.Row.FindControl("lnk_Reference_Number")).Text = "HERC/RTI-" + ((LinkButton)e.Row.FindControl("lnk_Reference_Number")).Text + "of " + ((Label)e.Row.FindControl("lblyear")).Text;
            //((Label)e.Row.FindControl("lblFAAReference")).Text = "HERC/FAA-" + ((Label)e.Row.FindControl("lblFAAReference")).Text + "of " + ((Label)e.Row.FindControl("lblyear")).Text;
            if (Convert.ToInt32(ddlStatus.SelectedValue) == (int)Module_ID_Enum.Module_Permission_ID.Approved)
            {
                LinkButton lnkAppeal = (LinkButton)e.Row.FindControl("lnkAppeal");
                Label lblAppeal = (Label)e.Row.FindControl("lblAppeal");


                Label lblRTI_ID = (Label)e.Row.FindControl("lblRTIFAA_ID");

                rtiObject.RTIFAAId = Convert.ToInt32(lblRTI_ID.Text);

                p_Var.dSetCompare = rtiBL.getRtiFAAIDFromTempFinalRti(rtiObject);
                if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(lblRTI_ID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[0]["rti_faa_id"]))
                    {
                        lnkAppeal.Visible = false;
                        lblAppeal.Visible = true;
                        lblAppeal.Text = "Yes";
                    }
                    else
                    {
                        lnkAppeal.Visible = true;
                        lblAppeal.Visible = false;

                        if (lnkAppeal.Text == "False")
                        {
                            lnkAppeal.Text = "Appeal";
                        }
                        else
                        {
                            lnkAppeal.Text = "Appealed";
                        }
                    }
                }
                else
                {
                    lnkAppeal.Visible = true;
                    lblAppeal.Visible = false;

                    if (lnkAppeal.Text == "False")
                    {
                        lnkAppeal.Text = "Appeal";
                    }
                    else
                    {
                        lnkAppeal.Text = "Appealed";
                    }
                }


            }
            else
            {
                LinkButton lnkAppeal = (LinkButton)e.Row.FindControl("lnkAppeal");
                Label lblAppeal = (Label)e.Row.FindControl("lblAppeal");

                lnkAppeal.Visible = false;
                lblAppeal.Visible = true;

                if (lblAppeal.Text == "False")
                {
                    lblAppeal.Text = "Appeal";
                }
                else
                {
                    lblAppeal.Text = "Appealed";
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

            //End
            // Division of characters start from here

            // string item = DataBinder.Eval(e.Row.DataItem, "Subject").ToString();

            //e.Row.Cells[7].Text = miscellBL.Division_characters(e.Row.Cells[7].Text,10);
            //e.Row.Cells[11].Text = miscellBL.Division_characters(e.Row.Cells[11].Text,11);
            // End

        }
    }

    #endregion

    #region gridView grdRTIFAA rowDeleting event to delete records

    protected void grdRTIFAA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdRTIFAA.Rows[e.RowIndex];
        Label lblAppeal = grdRTIFAA.Rows[e.RowIndex].Cells[8].FindControl("lblAppeal") as Label;
        p_Var.rtiid = Convert.ToInt32(grdRTIFAA.DataKeys[e.RowIndex].Value);
        if (lblAppeal.Text == "Yes")
        {
            rtiObject.RTIFAAId = Convert.ToInt32(p_Var.rtiid);
            p_Var.dSet = rtiBL.getRtiFAAIDForDelete(rtiObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (p_Var.dSet.Tables[0].Rows[0]["RTI_FAA_Id"] != DBNull.Value)
                {
                    //lblDeleteMsg.Text = "This record is already used in RTI-SAA application. Are you sure you want to delete this record.";
                    lblDeleteMsg.Text = "This RTI-FAA Application has corresponding application with SAA .Linked SAA records will also be deleted.Are you sure you want to delete this record?";
                }
                else
                {
                    lblDeleteMsg.Text = "This RTI-FAA Application has corresponding application with SAA .Linked SAA records will also be deleted.Are you sure you want to delete this record?";
                }
                this.ModalPopupExtender1.Show();
            }

        }
        else
        {

            p_Var.commandArgs = (string[])ViewState["commandArgs"];
            p_Var.rtiid = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

            rtiObject.RTIFAAId = p_Var.rtiid;
            rtiObject.TempRTIFAAId = p_Var.rtiid;
            rtiObject.StatusId = p_Var.status_id;

            p_Var.Result = rtiBL.Delete_RTIFAA(rtiObject);
            if (p_Var.Result > 0)
            {
                if (ddlStatus.SelectedValue == "8")
                {
                    Session["msg"] = "RTI-FAA Application has been deleted (purged) permanently.";
                }
                else
                {
                    Session["msg"] = "RTI-FAA Application has been deleted successfully.";
                }
                obj_audit.ActionType = "D";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.status = ddlStatus.SelectedItem.ToString();
                Label lblFAAReference = row.FindControl("lblFAAReference") as Label;
                //if (lblFAAReference == null) { return; }
                obj_audit.Title = "RTI-FAA/Ref. No. " + lblFAAReference.Text + " of " + ddlYear.SelectedItem.ToString();
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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
            //ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind gridView with rti first appellate authority

    public void Bind_Grid(int departmentid, string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdRTIFAA.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdRTIFAA.Visible = true;
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.DepttId = departmentid;
            //////rtiObject.PageIndex = pageIndex;
            rtiObject.year = ddlYear.SelectedValue.ToString();
            //////rtiObject.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            p_Var.dSet = rtiBL.getRtiFaaTempRecords(rtiObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdRTIFAA.Columns[13].HeaderText = "Purge";
                    grdRTIFAA.Columns[14].Visible = true;
                    grdRTIFAA.Columns[9].Visible = false;
                }
                else
                {
                    grdRTIFAA.Columns[13].HeaderText = "Delete";
                    grdRTIFAA.Columns[14].Visible = false;
                    //// grdRTIFAA.Columns[9].Visible = true;
                }

                if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                {
                    grdRTIFAA.Columns[3].HeaderText = "Ref No (HERC/RTI)";
                    grdRTIFAA.Columns[4].HeaderText = "FAA Ref No (HERC/FAA)";

                }
                else
                {
                    grdRTIFAA.Columns[3].HeaderText = "Ref No (EO/RTI)";
                    grdRTIFAA.Columns[4].HeaderText = "FAA Ref No (EO/FAA)";

                }
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End
                grdRTIFAA.DataSource = myDataView;
                grdRTIFAA.DataBind();
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
                        grdRTIFAA.Columns[0].Visible = false;
                        grdRTIFAA.Columns[7].Visible = true;
                        //p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare();

                        foreach (GridViewRow row in grdRTIFAA.Rows)
                        {

                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblPetitionID = (Label)row.FindControl("lblRTIFAA_ID");
                            Label lblchangestatus = (Label)row.FindControl("lblchangestatus");
                            LinkButton lnkChangeStatus = (LinkButton)row.FindControl("lnkChangeStatus");
                            rtiObject.RTIFAAId = Convert.ToInt32(lblPetitionID.Text);

                            p_Var.dSetCompare = rtiBL.getIdForrtiFAA_Comparison(rtiObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_RTI_FAA_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblPetitionID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_RTI_FAA_Id"]))
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
                        grdRTIFAA.Columns[0].Visible = true;
                        // grdRTIFAA.Columns[7].Visible = false;
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
                        grdRTIFAA.Columns[12].Visible = true;
                    }
                    else
                    {
                        grdRTIFAA.Columns[12].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdRTIFAA.Columns[12].Visible = false;
                            grdRTIFAA.Columns[10].Visible = false; //This is for appeal
                            grdRTIFAA.Columns[16].Visible = false;
                        }

                        // modify on date 23 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdRTIFAA.Columns[13].Visible = true;  // This is for delete
                                grdRTIFAA.Columns[16].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdRTIFAA.Columns[13].Visible = true;
                                    grdRTIFAA.Columns[16].Visible = false;
                                }
                                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdRTIFAA.Columns[16].Visible = true;
                                    }
                                    else
                                    {
                                        grdRTIFAA.Columns[16].Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdRTIFAA.Columns[13].Visible = true;
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdRTIFAA.Columns[16].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdRTIFAA.Columns[16].Visible = true;
                                            }
                                            else
                                            {
                                                grdRTIFAA.Columns[16].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdRTIFAA.Columns[13].Visible = false;
                                        grdRTIFAA.Columns[16].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdRTIFAA.Columns[13].Visible = true;
                                grdRTIFAA.Columns[16].Visible = false;
                            }


                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdRTIFAA.Columns[13].Visible = true;
                            }

                        }

                        //End  
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdRTIFAA.Columns[9].Visible = false;
                            grdRTIFAA.Columns[10].Visible = false;
                        }
                        else
                        {
                            grdRTIFAA.Columns[12].Visible = false;  //Edit
                            grdRTIFAA.Columns[9].Visible = true;
                            grdRTIFAA.Columns[10].Visible = true;
                        }

                        grdRTIFAA.Columns[13].Visible = false;
                    }

                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdRTIFAA.Columns[10].Visible = true; //This is for Appeal
                        grdRTIFAA.Columns[9].Visible = true; //This is for change Status
                    }
                    else
                    {
                        grdRTIFAA.Columns[10].Visible = false; //This is for Appeal
                        grdRTIFAA.Columns[9].Visible = false; //This is for change Status
                    }


                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {

                grdRTIFAA.Visible = false;
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
        p_Var.dSet = rtiBL.getRtiStatus();
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
        grdRTIFAA.Visible = false;
        Session["FAAyear"] = ddlYear.SelectedValue;

        if (ddlLanguage.SelectedValue == "0" || ddlYear.SelectedValue == "0" || ddlDepartment.SelectedValue == "0" || ddlStatus.SelectedValue == "0")
        {
            btnPdf.Visible = false;
        }
        else
        {
            btnPdf.Visible = true;
        }
    }

    #region Function to bind Rti Year

    public void bindRtiFAAYearinDdl()
    {
        p_Var.dSet = rtiBL.GetYearRTIFAA_admin();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
    }

    #endregion

    //Codes for sorting of the grid



    protected void grdRTIFAA_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);
    }

    #region gridView grdRTIFAA pageIndexChanging Event zone

    protected void grdRTIFAA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRTIFAA.PageIndex = e.NewPageIndex;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    //End
    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=+5"));
    }

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {
        rtiObject.ActionType = 2;
        rtiObject.RTIId = Convert.ToInt32(ViewState["id"]);

        //p_Var.Result1 = rtiBL.updateRTIweb(rtiObject);

        lnkFileRemove.Visible = false;
        lblOther.Visible = false;
        lblTransferUploader.Visible = false;
        lbluploader.Visible = false;

        lbluploader.Text = null;
        lblTransferUploader.Text = null;
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
            grdRTIFAAPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdRTIFAAPdf.Visible = true;
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            rtiObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = rtiBL.getRtiFaaTempRecords(rtiObject, out p_Var.k);

            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                grdRTIFAAPdf.DataSource = p_Var.dSet;
                grdRTIFAAPdf.DataBind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RTIFAA_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdRTIFAAPdf.AllowPaging = false;
        grdRTIFAAPdf.DataBind();
        grdRTIFAAPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    protected void grdRTIFAAPdf_RowDataBound(object sender, GridViewRowEventArgs e)
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
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdRTIFAA.Rows)
                    {
                        Label lblFAAReference = (Label)row.FindControl("lblFAAReference");
                        HiddenField hydYear = (HiddenField)row.FindControl("hydYear");
                        Label lblyear = (Label)row.FindControl("lblyear");
                        ViewState["RTIFAA"] = lblFAAReference.Text;

                        ViewState["RtiYearFAA"] = lblyear.Text;
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI-FAA -</b>" + lblFAAReference.Text + " of " + lblyear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTIFAAId = p_Var.dataKeyID;
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = rtiBL.updateRtiFAAStatus(rtiObject);
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


                        Session["msg"] = "RTI-FAA Application has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdRTIFAA.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblFAAReference = (Label)row.FindControl("lblFAAReference");
                        HiddenField hydYear = (HiddenField)row.FindControl("hydYear");
                        Label lblyear = (Label)row.FindControl("lblyear");
                        ViewState["RTIFAA"] = lblFAAReference.Text;

                        ViewState["RtiYearFAA"] = lblyear.Text;

                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI-FAA -</b>" + lblFAAReference.Text + " of " + lblyear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTIFAAId = p_Var.dataKeyID;
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            p_Var.Result = rtiBL.updateRtiFAAStatus(rtiObject);
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


                        Session["msg"] = "RTI-FAA Application has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }
                else
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    LinkOB obj_linkOB1 = new LinkOB();
                    foreach (GridViewRow row in grdRTIFAA.Rows)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lblFAAReference = (Label)row.FindControl("lblFAAReference");
                        HiddenField hydYear = (HiddenField)row.FindControl("hydYear");
                        Label lblyear = (Label)row.FindControl("lblyear");
                        ViewState["RTIFAA"] = lblFAAReference.Text;

                        ViewState["RtiYearFAA"] = lblyear.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI-FAA -</b>" + lblFAAReference.Text + " of " + lblyear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTIFAAId = p_Var.dataKeyID;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsMail = rtiBL.getEmailIdToSendRTIFAAEmail(rtiObject);
                            dsSms = rtiBL.getMobileNumberToSendRTIFAASms(rtiObject);
                            p_Var.Result = rtiBL.InsertRtiFAAIntoWeb(rtiObject);


                            /* Function to get email id of petitioners/respondents to send email*/
                            p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                            p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                            p_Var.sbuildertmp.Append("Status of RTI-FAA Application No : " + dsMail.Tables[0].Rows[0]["Ref_No"].ToString() + "," + " Dated: " + dsMail.Tables[0].Rows[0]["Application_Date"].ToString());


                            p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            p_Var.sbuildertmp.Append("Status :" + HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["RTI_Status"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");
                            p_Var.sbuildertmp.Append("</table>");

                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details see attached file.You may also visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact PIO at 0172-2572993 or APIO at 0172-2569602<br/><br/>");

                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.<br />");

                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");




                            /* Function to get email id of petitioners/respondents to send email*/

                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Applicant_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Applicant_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {
                                    //loop through cells in that row
                                    if (Convert.ToInt32(drRow["RTI_FAA_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                                    {

                                        if (drRow["filename"] != null)
                                        {
                                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                            {
                                                miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                            }
                                            else
                                            {
                                                miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                            {
                                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                            }
                                            else
                                            {
                                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                            }
                                        }
                                    }
                                    //  miscellBL.SendEmail(drRow["Applicant_Email"].ToString().Trim(), "", "", "Multiple email testing from RTI FAA during approve", "birendra.kumar@netcreativemind.co.in", "Testing email for RTI FAA");

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


                                            string message = dsSms.Tables[0].Rows[0]["Ref_No"].ToString() + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";


                                            string textmessage = "HERC Status of RTI FAA Application No. - ";

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


                        Session["msg"] = "RTI-FAA Application has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                foreach (GridViewRow row in grdRTIFAA.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIFAAId = p_Var.dataKeyID;
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = rtiBL.updateRtiFAAStatus(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI-FAA Application has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdRTIFAA.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIFAAId = p_Var.dataKeyID;
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = rtiBL.updateRtiFAAStatus(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI-FAA Application has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }

            }
            else
            {
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in grdRTIFAA.Rows)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIFAAId = p_Var.dataKeyID;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        char[] splitter = { ';' };
                        DataSet dsMail = new DataSet();
                        DataSet dsSms = new DataSet();
                        dsSms = rtiBL.getMobileNumberToSendRTIFAASms(rtiObject);
                        dsMail = rtiBL.getEmailIdToSendRTIFAAEmail(rtiObject);

                        p_Var.Result = rtiBL.InsertRtiFAAIntoWeb(rtiObject);


                        /* Function to get email id of petitioners/respondents to send email*/
                        p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                        p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                        p_Var.sbuildertmp.Append("Status of RTI-FAA Application No : " + dsMail.Tables[0].Rows[0]["Ref_No"].ToString() + "," + " Dated: " + dsMail.Tables[0].Rows[0]["Application_Date"].ToString());


                        p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                        p_Var.sbuildertmp.Append("Status :" + HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["RTI_Status"].ToString()));
                        p_Var.sbuildertmp.Append("</td></tr>");
                        p_Var.sbuildertmp.Append("</table>");

                        p_Var.sbuildertmp.Append("<br />");
                        p_Var.sbuildertmp.Append("For details see attached file.You may also visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                        p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                        p_Var.sbuildertmp.Append("query, you may contact PIO at 0172-2572993 or APIO at 0172-2569602<br/><br/>");

                        p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.<br />");

                        p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");




                        /* Function to get email id of petitioners/respondents to send email*/

                        if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Applicant_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Applicant_Email"] != "")
                        {
                            foreach (DataRow drRow in dsMail.Tables[0].Rows)
                            {
                                //loop through cells in that row
                                if (Convert.ToInt32(drRow["RTI_FAA_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                                {

                                    if (drRow["filename"] != null)
                                    {
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                        }
                                        else
                                        {
                                            miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                        }
                                        else
                                        {
                                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO - RTI – FAA Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                        }
                                    }
                                }
                                //  miscellBL.SendEmail(drRow["Applicant_Email"].ToString().Trim(), "", "", "Multiple email testing from RTI FAA during approve", "birendra.kumar@netcreativemind.co.in", "Testing email for RTI FAA");

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


                                        string message = dsSms.Tables[0].Rows[0]["Ref_No"].ToString() + Environment.NewLine + "For details and disclaimer visit :  http://herc.gov.in";


                                        string textmessage = "HERC Status of RTI FAA Application No. - ";

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
                    Session["msg"] = "RTI-FAA Application has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
    protected void btnSendEmailsWithoutEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdRTIFAA.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    rtiObject.TempRTIFAAId = p_Var.dataKeyID;
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
                    p_Var.Result = rtiBL.updateRtiFAAStatus(rtiObject);
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "RTI-FAA Application has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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

    protected void btnSendEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                foreach (GridViewRow row in grdRTIFAA.Rows)
                {
                    Label lblFAAReference = (Label)row.FindControl("lblFAAReference");
                    HiddenField hydYear = (HiddenField)row.FindControl("hydYear");
                    Label lblyear = (Label)row.FindControl("lblyear");
                    ViewState["DisRTIFAA"] = lblFAAReference.Text;

                    ViewState["DisRtiYearFAA"] = hydYear.Value;

                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>RTI-FAA -</b>" + lblFAAReference.Text + " of " + lblyear.Text + "<br/> ");
                        Label lblid = (Label)row.FindControl("lblRTIFAA_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIFAAId = p_Var.dataKeyID;
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

                        p_Var.Result = rtiBL.updateRtiFAAStatus(rtiObject);
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
                        p_Var.sbuilder.Append("Record disapproved: " + sbuilder.ToString());
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


                    Session["msg"] = "RTI-FAA Application has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }

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
