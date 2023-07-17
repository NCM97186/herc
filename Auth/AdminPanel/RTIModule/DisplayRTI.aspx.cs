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
using System.Collections.Generic;
using System.Text;

public partial class Auth_AdminPanel_RTI_DisplayRTI : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration 

    #region variable declaration zone

    Project_Variables p_Var = new Project_Variables();
    LinkBL obj_linkBL = new LinkBL();
    LinkOB obj_inkOB = new LinkOB();
    UserBL obj_UserBL = new UserBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    PetitionOB rtiObject = new PetitionOB();
    RtiBL rtiBL = new RtiBL();
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

        Session.Remove("WhatsNewStatus"); // What's New sessions
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
        lblModulename.Text = ": View  RTI";
        this.Page.Title = " RTI: HERC";

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

            //rptPager.Visible      = false;
            //lblPageSize.Visible   = false;
            //ddlPageSize.Visible   = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddRTI.Visible = false;

            if (Session["year"] != null)
            {
                bindRtiYearinDdl();
                ddlYear.SelectedValue = Session["year"].ToString();
            }
            else
            {
                bindRtiYearinDdl();
            }
            if (Session["Lng"] != null)
            {
                bindropDownlistLang();
                ddlLanguage.SelectedValue = Session["Lng"].ToString();
            }
            else
            {
                bindropDownlistLang();
            }
            if (Session["deptt"] != null)
            {
                Get_Deptt_Name();
                ddlDepartment.SelectedValue = Session["deptt"].ToString();
            }
            else
            {
                Get_Deptt_Name();
            }
            if (Session["Status"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["Status"].ToString();
                Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            }
            else
            {
                binddropDownlistStatus();
            }


        }
    }

    #endregion

    //Area for all the dropDownlist, listBox events

    #region dropDownlist ddlDepartment selectedIndexChanged events

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        // grdCMSMenu.Visible = false;
        //ddlPageSize.Visible = false;
        //lblPageSize.Visible = false;
        //rptPager.Visible    = false;
        //Session["mlang"] = ddlLanguage.SelectedValue;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
        Session["deptt"] = ddlDepartment.SelectedValue;

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

    #region ddlLanguage selectedIndexChanged events

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue != "0")
        {
            binddropDownlistStatus();
            grdRTI.Visible = false;
            Session["Lng"] = ddlLanguage.SelectedValue;
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

    #region ddlStatus selectedIndexChanged events

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlStatus.SelectedValue == "0")
        {
            grdRTI.Visible = false;
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
                grdRTI.Columns[9].Visible = true;
            }
            else
            {
                grdRTI.Columns[9].Visible = false;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                grdRTI.Columns[14].Visible = true;//This is for restore
            }
            else
            {
                grdRTI.Columns[14].Visible = false;
            }
            Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), "", "");
            Session["Status"] = ddlStatus.SelectedValue;
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

    #region ddlStatus selectedIndexChanged events

    protected void ddlRTIStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
        {
            lblAuthority.Visible = false;
            trAuthority.Visible = false;
            DivUpLoader.Visible = true;
            DivCommon.Visible = true;
            lblTransferUploader.Visible = false;
            lnkFileRemove.Visible = false;
            lblOther.Visible = false;
        }
        else
        {
            trAuthority.Visible = false;
            DivUpLoader.Visible = false;
            DivCommon.Visible = false;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
        {
            trAuthority.Visible = true;
            DivUpLoader.Visible = true;//today added
            lblTransferUploader.Visible = false;//today added
            lnkFileRemove.Visible = false;//today added
            lblOther.Visible = false;
        }
        else
        {

            //trAuthority.Visible = false;
            //txtOther.Visible = false;
        }
        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
        {

            txtOther.Text = "";
            trAuthority.Visible = true;
            lblAuthority.Visible = true;
            DivCommon.Visible = true;
            DivUpLoader.Visible = true;
            lbluploader.Visible = false;
            //lblTransferUploader.Visible = true;
            lnkFileRemove.Visible = false;
        }
        else
        {
            lblAuthority.Visible = false;

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

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnAddRTI click event to add RTI

    protected void btnAddRTI_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTI_Add.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
    }

    #endregion

    #region button btnForeReview click event to send rti for review

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

    #region button btnApprove click event to approve rti records

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;

        //ChkApprove();
    }

    #endregion

    #region button btnDisApprove click event to disApprove the records

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

        //try
        //{
        //    foreach (GridViewRow row in grdRTI.Rows)
        //    {
        //        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
        //        if ((selCheck.Checked == true))
        //        {
        //            Label lblid = (Label)row.FindControl("lblRTI_ID");
        //            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
        //            rtiObject.TempRTIId = p_Var.dataKeyID;
        //            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
        //            {
        //                rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
        //            }
        //            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
        //            {
        //                rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
        //            }
        //            else
        //            {
        //                rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
        //            }

        //            p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
        //        }
        //    }
        //    if (p_Var.Result > 0)
        //    {
        //        Session["msg"] = "RTI Application has been disapproved successfully.";
        //        Session["Redirect"] = "~/Auth/AdminPanel/RTI/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
        //        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
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

    #region button btnUpdateStatus click event to update status of rti

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            p_Var.rtiid = Convert.ToInt32(ViewState["id"]);
            rtiObject.RTIId = p_Var.rtiid;
            rtiObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();

            //26 Nov 
            if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
            {
                rtiObject.subject = txtOther.Text;
                p_Var.dSetChildData = rtiBL.get_MstStatus(rtiObject);


                if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
                {
                    lblPopupmsg.Text = "Already Exist.Please enter other status";
                    lblPopupmsg.ForeColor = System.Drawing.Color.Red;
                    this.mpuUpdateStatus.Show();
                }
            }

            //else
            //  {
            if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther))
            {

                rtiObject.subject = txtOther.Text;
                rtiObject.StatusId = Convert.ToInt32(Module_ID_Enum.rti_Status.Rti_StatusTypeId);
                p_Var.Result = rtiBL.Insert_Status(rtiObject, out p_Var.k);
                rtiObject.RTIStatusId = p_Var.Result;

            }
            else
            {
                rtiObject.RTIStatusId = Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue);
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

            p_Var.Result = rtiBL.modifyRtiStatus(rtiObject);
            if (p_Var.Result > 0)
            {

                obj_audit.ActionType = "U";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.status = ddlStatus.SelectedItem.ToString();

                obj_audit.Title = Session["RTI_Title"].ToString();
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "RTI Application's status has been updated successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
            }
            // }
            //}
        }


    }

    #endregion

    #region button btnRtiFaa click event to redirect page to display rti first appellate authority

    protected void btnRtiFaa_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    #region button btnDelete click event to delete petition

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rtiid = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        rtiObject.RTIId = p_Var.rtiid;
        rtiObject.TempRTIId = p_Var.rtiid;
        rtiObject.StatusId = p_Var.status_id;

        p_Var.Result = rtiBL.Delete_RTI(rtiObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                Session["msg"] = "RTI Application has been deleted (purged) permanently";
            }
            else
            {
                Session["msg"] = "RTI Application has been deleted successfully.";
            }

            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }
    }

    #endregion

    //End

    //Area for all the gridView events

    #region gridView grdRTI rowCommand event

    protected void grdRTI_RowCommand(object sender, GridViewCommandEventArgs e)
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


                    Response.Redirect("~/Auth/AdminPanel/RTIModule/Rti_FAA_Add.aspx?rti_id=" + p_Var.petition_id + "&Ref_Number=" + p_Var.pro_number + "&ModuleID=" + Request.QueryString["ModuleID"] + "&DepttID=" + p_Var.id);
                }
                else if (e.CommandName == "ChangeStatus")
                {
                    bindRtiStatus();
                    rtiObject.TempRTIId = Convert.ToInt32(e.CommandArgument.ToString());
                    rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    p_Var.dSetChildData = rtiBL.getTempRTIRecordsBYID(rtiObject);
                    lblRefNo.Text = p_Var.dSetChildData.Tables[0].Rows[0]["Ref_No"].ToString();

                    Session["RTI_Title"] = "RTI/Ref No. " + lblRefNo.Text + " of " + ddlYear.SelectedItem.ToString();


                    if (Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.replysent) && Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority) && Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                    {
                        ddlRTIStatusUpdate.SelectedValue = Convert.ToInt32(Module_ID_Enum.rti_Status.AnyOther).ToString();

                        trAuthority.Visible = true;
                        txtOther.Text = p_Var.dSetChildData.Tables[0].Rows[0]["anyother"].ToString();
                        DivUpLoader.Visible = true;

                        DivCommon.Visible = false;
                        lbluploader.Visible = false;
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
                            lblOther.Visible = false;
                        }
                    }
                    else if (Convert.ToInt32(p_Var.dSetChildData.Tables[0].Rows[0]["RTI_Status_Id"]) == Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                    {
                        DivUpLoader.Visible = false;
                        DivCommon.Visible = false;
                        //txtOther.Visible = false;
                        trAuthority.Visible = false;
                    }
                    else
                    {
                        ddlRTIStatusUpdate.SelectedValue = p_Var.dSetChildData.Tables[0].Rows[0]["RTI_Status_Id"].ToString();
                        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
                        {
                            DivUpLoader.Visible = true;
                            lblOther.Visible = false;
                            lblTransferUploader.Visible = false;
                            DivCommon.Visible = true;
                            txtMemo.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString();
                            txtDate.Text = p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo"].ToString();
                            if (p_Var.dSetChildData.Tables[0].Rows[0]["FileName"] != DBNull.Value && p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString() != "")
                            {
                                lbluploader.Visible = true;
                                lbluploader.Text = p_Var.dSetChildData.Tables[0].Rows[0]["FileName"].ToString();
                                lnkFileRemove.Visible = true;
                            }
                            else
                            {
                                lnkFileRemove.Visible = false;
                                lbluploader.Visible = false;
                            }
                            //txtOther.Visible = false;
                            trAuthority.Visible = false;
                        }
                        else
                        {
                            DivUpLoader.Visible = false;
                            DivCommon.Visible = false;
                        }
                        if (Convert.ToInt32(ddlRTIStatusUpdate.SelectedValue) == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                        {
                            lblOther.Visible = false;
                            lbluploader.Visible = false;
                            DivUpLoader.Visible = true;
                            lblAuthority.Visible = true;
                            trAuthority.Visible = true;
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
                    //ClientScript.RegisterStartupScript(typeof(Page), "Confirm", "<script type='text/javascript'>Confirm('Are you sure you want to delete.');</script>");

                }
                else if (e.CommandName == "Restore")
                {

                    rtiObject.RTIId = Convert.ToInt32(e.CommandArgument);


                    p_Var.Result = rtiBL.updateRtiStatusDelete(rtiObject);
                    if (p_Var.Result > 0)
                    {
                        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                        obj_audit.ActionType = "R";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.status = ddlStatus.SelectedItem.ToString();
                        Label lblRef = row.FindControl("lblRef") as Label;
                        // if (lblRef == null) { return; }

                        obj_audit.Title = lblRef.Text + " of " + ddlYear.SelectedItem.ToString();
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                        Session["msg"] = "RTI Application has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                    else
                    {
                        Session["msg"] = "RTI Application has not been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                    }
                }
                else if (e.CommandName == "Audit")
                {

                    DataSet dSetAuditTrail = new DataSet();
                    petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
                    petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.RTI);
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    petObject.ModuleType = null;
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

    #region gridView grdRTI rowCreated events to select one or all rows

    protected void grdRTI_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdRTI.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));
        }
    }

    #endregion

    #region gridView grdRTI rowDataBound event

    protected void grdRTI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hyd")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("status")).Value);
            LinkButton lnkAppeal1 = (LinkButton)e.Row.FindControl("lnkAppeal");
            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            lblApplicant.Text = lblApplicant.Text.Replace("&lt;br /&gt;", Environment.NewLine);
            //This is for change status
            LinkButton lnkChangeStatus = (LinkButton)e.Row.FindControl("lnkChangeStatus");
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
            {

                lnkChangeStatus.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to change status of this RTI Application No- " +
                DataBinder.Eval(e.Row.DataItem, "Ref_No") + "')");



                lnkAppeal1.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure that appeal has been received with FAA against this RTI Application No- " +
                DataBinder.Eval(e.Row.DataItem, "Ref_No") + "')");

            }

            //END

            //This is for delete/permanently delete 30 may 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure want to restore RTI Application No-" + DataBinder.Eval(e.Row.DataItem, "Ref_No") + "')");

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
"confirm('Are you sure you want to purge this RTI Application No-" + DataBinder.Eval(e.Row.DataItem, "Ref_No") + " permanently? " + "')");
            }
            else
            {



                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure want to delete this RTI Application No- " + DataBinder.Eval(e.Row.DataItem, "Ref_No") + "')");
            }

            //END


            //use for format of Reply sent status
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.TempRTIId = RTID;
            p_Var.dSetChildData = rtiBL.getTempRTIRecordsBYID(rtiObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = ((Label)e.Row.FindControl("lblStatus")).Text + " " + " vide " + "<br/>Memo No:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
                }
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                {
                    //lblAuthority.Visible = true;
                    ((Label)e.Row.FindControl("lblStatus")).Text = ((Label)e.Row.FindControl("lblStatus")).Text + ": " + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/>vide " + "Memo No:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
                }

            }

            //((LinkButton)e.Row.FindControl("lnk_Reference_Number")).Text = "HERC/RTI-" + ((LinkButton)e.Row.FindControl("lnk_Reference_Number")).Text;
            if (Convert.ToInt32(ddlStatus.SelectedValue) == (int)Module_ID_Enum.Module_Permission_ID.Approved)
            {
                LinkButton lnkAppeal = (LinkButton)e.Row.FindControl("lnkAppeal");
                Label lblAppeal = (Label)e.Row.FindControl("lblAppeal");


                Label lblRTI_ID = (Label)e.Row.FindControl("lblRTI_ID");

                rtiObject.RTIId = Convert.ToInt32(lblRTI_ID.Text);

                p_Var.dSetCompare = rtiBL.getRtiIDFromTempFinalRti(rtiObject);
                if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToInt32(lblRTI_ID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[0]["rti_id"]))
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


            // 31 Aug 2013 by Ruchi


            Literal orderFile = (Literal)e.Row.FindControl("ltrlFile");
            if (orderFile.Text != null && orderFile.Text != "")
            {
                p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
                p_Var.sbuilder.Append("<a href='" + p_Var.url + orderFile.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='View Document' width='15' alt='View Document' height=\"15\" /> " + "</a>");
                orderFile.Text = p_Var.sbuilder.ToString();
            }

            //End

            // Division of characters start from here

            //e.Row.Cells[6].Text = miscellBL.Division_characters(e.Row.Cells[6].Text, 10);
            //e.Row.Cells[7].Text = miscellBL.Division_characters(e.Row.Cells[7].Text, 15);
            //e.Row.Cells[11].Text = miscellBL.Division_characters(e.Row.Cells[11].Text, 10);
            // End
        }
    }

    #endregion

    #region grdRTI rowDeleted event to delete records

    protected void grdRTI_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdRTI.Rows[e.RowIndex];
        Label lblAppeal = grdRTI.Rows[e.RowIndex].Cells[8].FindControl("lblAppeal") as Label;
        p_Var.rtiid = Convert.ToInt32(grdRTI.DataKeys[e.RowIndex].Value);
        if (lblAppeal.Text == "Yes")
        {
            rtiObject.RTIId = Convert.ToInt32(p_Var.rtiid);
            p_Var.dSet = rtiBL.getRtiIDForDelete(rtiObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (p_Var.dSet.Tables[0].Rows[0]["RTI_Id"] != DBNull.Value)
                {
                    //lblDeleteMsg.Text = "This record is already used in RTI FAA application. Are you sure to delete this RTI FAA application.";
                    lblDeleteMsg.Text = "This RTI Application has corresponding application with FAA . Linked FAA records will also be deleted.  Are you sure you want to delete this record?";
                }
                else
                {
                    lblDeleteMsg.Text = "This RTI Application has corresponding application with FAA. Linked FAA records will also be deleted.  Are you sure you want to delete this record?";
                }
                this.ModalPopupExtender1.Show();
            }

        }
        else
        {

            p_Var.commandArgs = (string[])ViewState["commandArgs"];
            p_Var.rtiid = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

            rtiObject.RTIId = p_Var.rtiid;
            rtiObject.TempRTIId = p_Var.rtiid;
            rtiObject.StatusId = p_Var.status_id;

            p_Var.Result = rtiBL.Delete_RTI(rtiObject);
            if (p_Var.Result > 0)
            {
                if (ddlStatus.SelectedValue == "8")
                {
                    Session["msg"] = "RTI Application has been deleted (purged) permanently.";
                }
                else
                {
                    Session["msg"] = "RTI Application has been deleted successfully.";
                }

                obj_audit.ActionType = "D";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.status = ddlStatus.SelectedItem.ToString();
                Label lblRef = row.FindControl("lblRef") as Label;
                obj_audit.Title = lblRef.Text + " of " + ddlYear.SelectedItem.ToString();
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                btnAddRTI.Visible = true;


                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddRTI.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
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

    #region Function to bind gridView with rti records

    public void Bind_Grid(int departmentid, string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;

        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdRTI.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdRTI.Visible = true;
            rtiObject.DepttId = departmentid;
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.year = ddlYear.SelectedValue.ToString();
            //////////rtiObject.PageIndex = pageIndex;
            //////////rtiObject.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            p_Var.dSet = rtiBL.getTempRTIRecords(rtiObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdRTI.Columns[13].HeaderText = "Purge";
                    grdRTI.Columns[14].Visible = true;
                }
                else
                {
                    grdRTI.Columns[13].HeaderText = "Delete";
                    grdRTI.Columns[14].Visible = false;
                }

                if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                {
                    grdRTI.Columns[3].HeaderText = "Ref No (HERC/RTI)";
                }
                else
                {
                    grdRTI.Columns[3].HeaderText = "Ref No (EO/RTI)";
                }
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                //End
                grdRTI.DataSource = myDataView;
                grdRTI.DataBind();
                //ViewState["RTI_Status_Id"] = p_Var.dSet.Tables[0].Rows[0]["RTI_Status_Id"].ToString();
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
                        grdRTI.Columns[0].Visible = false;
                        grdRTI.Columns[7].Visible = true;
                        //p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare();

                        foreach (GridViewRow row in grdRTI.Rows)
                        {
                            Image imgedit = (Image)row.FindControl("imgEdit");
                            Image imgnotedit = (Image)row.FindControl("imgnotedit");
                            Label lblRTI_ID = (Label)row.FindControl("lblRTI_ID");
                            Label lblchangestatus = (Label)row.FindControl("lblchangestatus");
                            LinkButton lnkChangeStatus = (LinkButton)row.FindControl("lnkChangeStatus");
                            rtiObject.RTIId = Convert.ToInt32(lblRTI_ID.Text);

                            p_Var.dSetCompare = rtiBL.get_ID_For_Compare(rtiObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_Rti_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblRTI_ID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_Rti_Id"]))
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
                        grdRTI.Columns[0].Visible = true;
                        //  grdRTI.Columns[7].Visible = false;
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
                        grdRTI.Columns[12].Visible = true; //This is for Edit
                    }
                    else
                    {
                        grdRTI.Columns[12].Visible = false; //This is for Edit
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                        {
                            grdRTI.Columns[12].Visible = false; // This is for Edit  
                            grdRTI.Columns[9].Visible = false; //This is for change status
                            grdRTI.Columns[16].Visible = false;
                        }

                        // modify on date 23 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdRTI.Columns[13].Visible = true;  // This is for delete
                                grdRTI.Columns[16].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdRTI.Columns[13].Visible = true;
                                    grdRTI.Columns[16].Visible = false;
                                }
                                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdRTI.Columns[16].Visible = true;
                                    }
                                    else
                                    {
                                        grdRTI.Columns[16].Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdRTI.Columns[13].Visible = true;
                                        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdRTI.Columns[16].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdRTI.Columns[16].Visible = true;
                                            }
                                            else
                                            {
                                                grdRTI.Columns[16].Visible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        grdRTI.Columns[13].Visible = false;
                                        grdRTI.Columns[16].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdRTI.Columns[13].Visible = true;
                                grdRTI.Columns[16].Visible = false;
                            }


                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdRTI.Columns[13].Visible = true;
                            }

                        }

                        //End  



                    }
                    else
                    {
                        grdRTI.Columns[13].Visible = false; //This is for Delete
                    }


                    if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdRTI.Columns[10].Visible = true; //This is for Appeal
                        grdRTI.Columns[9].Visible = true; //This is for change status
                    }
                    else
                    {
                        grdRTI.Columns[10].Visible = false; //This is for Appeal
                        grdRTI.Columns[9].Visible = false; //This is for change status
                    }


                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                grdRTI.Visible = false;
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

    #region Function to bind Rti Year

    public void bindRtiYearinDdl()
    {
        p_Var.dSet = rtiBL.GetYearRTI_Admin();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
    }

    #endregion


    //End
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdRTI.Visible = false;
        Session["year"] = ddlYear.SelectedValue;
    }

    #region button btnRtiSaa click event to redirect page to another

    protected void btnRtiSaa_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/RTIModule/DisplayRTISAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion




    protected void grdRTI_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), e.SortExpression, sortOrder);

    }

    #region gridView grdRTI pageIndexChanging Event zone

    protected void grdRTI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRTI.PageIndex = e.NewPageIndex;
        Bind_Grid(Convert.ToInt32(ddlDepartment.SelectedValue), ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    //End

    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=+5"));
    }

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {
        rtiObject.ActionType = 1;
        rtiObject.RTIId = Convert.ToInt32(ViewState["id"]);

        // p_Var.Result1 = rtiBL.updateRTIweb(rtiObject);
        lbluploader.Visible = false;
        lnkFileRemove.Visible = false;
        lblTransferUploader.Visible = false;
        lblOther.Visible = false;


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
            grdRTIPdf.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            grdRTIPdf.Visible = true;
            rtiObject.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
            rtiObject.year = ddlYear.SelectedValue.ToString();
            p_Var.dSet = rtiBL.getTempRTIRecords(rtiObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                grdRTIPdf.DataSource = p_Var.dSet;
                grdRTIPdf.DataBind();

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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RTI_" + System.DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdRTIPdf.AllowPaging = false;
        grdRTIPdf.DataBind();
        grdRTIPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }

    protected void grdRTIPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int RTID = Convert.ToInt32(((HiddenField)e.Row.FindControl("hydpdf")).Value);
            int RtistatusId = Convert.ToInt32(((HiddenField)e.Row.FindControl("statuspdf")).Value);

            //use for format of Reply sent status
            rtiObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            rtiObject.TempRTIId = RTID;
            p_Var.dSetChildData = rtiBL.getTempRTIRecordsBYID(rtiObject);
            if (p_Var.dSetChildData.Tables[0].Rows.Count > 0)
            {
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.replysent))
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = ((Label)e.Row.FindControl("lblStatus")).Text + " " + " vide " + "<br/>Memo No:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + "<br/>Dated:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
                }
                if (RtistatusId == Convert.ToInt32(Module_ID_Enum.rti_Status.TransferedAuthority))
                {
                    //lblAuthority.Visible = true;
                    ((Label)e.Row.FindControl("lblStatus")).Text = ((Label)e.Row.FindControl("lblStatus")).Text + ": " + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderThree"].ToString() + "<br/>vide " + "Memo No:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderOne"].ToString() + " <br/>Dated:" + " " + p_Var.dSetChildData.Tables[0].Rows[0]["PlaceholderTwo1"].ToString();
                }

            }

            Label lblApplicant = (Label)e.Row.FindControl("lblApplicant");
            lblApplicant.Text = lblApplicant.Text.Replace("&lt;br /&gt;", Environment.NewLine);

            Label lblApplicantAddress = (Label)e.Row.FindControl("lblApplicantAddress");
            if (lblApplicantAddress.Text != "" && lblApplicantAddress.Text != null)
            {
                lblApplicantAddress.Text = HttpUtility.HtmlDecode(lblApplicantAddress.Text);
            }
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
                    foreach (GridViewRow row in grdRTI.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lnk_Reference_Number = (Label)row.FindControl("lnk_Reference_Number");
                        ViewState["RTI"] = lnk_Reference_Number.Text;
                        Label lblYear = (Label)row.FindControl("lblYear");
                        ViewState["RtiYear"] = lblYear.Text;
                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI -</b> " + lnk_Reference_Number.Text + " of " + lblYear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTI_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTIId = p_Var.dataKeyID;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                            p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
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
                            p_Var.sbuilder.Append("Record pending for Review:  " + sbuilder.ToString());
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

                        Session["msg"] = "RTI Application has been sent for review successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }
                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                {
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdRTI.Rows)
                    {
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lnk_Reference_Number = (Label)row.FindControl("lnk_Reference_Number");
                        Label lblYear = (Label)row.FindControl("lblYear");

                        ViewState["RTI"] = lnk_Reference_Number.Text;
                        ViewState["RtiYear"] = lblYear.Text;

                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI -</b> " + lnk_Reference_Number.Text + " of " + lblYear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTI_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTIId = p_Var.dataKeyID;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                            p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
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


                        Session["msg"] = "RTI Application has been sent for publish successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                    }

                }

                else
                {

                    LinkOB obj_linkOB1 = new LinkOB();
                    sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (GridViewRow row in grdRTI.Rows)
                    {
                        p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                        CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                        Label lnk_Reference_Number = (Label)row.FindControl("lnk_Reference_Number");
                        Label lblYear = (Label)row.FindControl("lblYear");

                        ViewState["RTI"] = lnk_Reference_Number.Text;
                        ViewState["RtiYear"] = lblYear.Text;

                        if ((selCheck.Checked == true))
                        {
                            sbuilder.Append("<b>RTI -</b> " + lnk_Reference_Number.Text + " of " + lblYear.Text + "<br/> ");
                            Label lblid = (Label)row.FindControl("lblRTI_ID");
                            p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                            rtiObject.TempRTIId = p_Var.dataKeyID;
                            rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                            rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                            char[] splitter = { ';' };
                            DataSet dsMail = new DataSet();
                            DataSet dsSms = new DataSet();
                            dsSms = rtiBL.getMobileToSendRTIEmail(rtiObject);
                            dsMail = rtiBL.getEmailIdToSendRTIEmail(rtiObject);
                            p_Var.Result = rtiBL.InsertRtiIntoWeb(rtiObject);

                            /* Function to get email id of petitioners/respondents to send email*/
                            p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                            p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                            p_Var.sbuildertmp.Append("Status of RTI Application No : " + dsMail.Tables[0].Rows[0]["Ref_No"].ToString() + "," + " Dated: " + dsMail.Tables[0].Rows[0]["Application_Date"].ToString());


                            p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                            p_Var.sbuildertmp.Append("Status : " + HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["RTI_Status"].ToString()));
                            p_Var.sbuildertmp.Append("</td></tr>");
                            p_Var.sbuildertmp.Append("</table>");

                            p_Var.sbuildertmp.Append("<br />");
                            p_Var.sbuildertmp.Append("For details see attached file.You may also visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                            p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                            p_Var.sbuildertmp.Append("query, you may contact PIO at 0172-2572993 or APIO at 0172-2569602<br/><br/>");

                            p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.<br />");

                            p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");


                            /* Function to get email id of petitioners/respondents to send email*/

                            //end

                            /* Function to get email id of petitioners/respondents to send email*/


                            if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Applicant_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Applicant_Email"] != "")
                            {
                                foreach (DataRow drRow in dsMail.Tables[0].Rows)
                                {
                                    //loop through cells in that row

                                    if (Convert.ToInt32(drRow["RTI_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                                    {
                                        if (drRow["filename"] != null)
                                        {
                                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                            {
                                                miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                            }
                                            else
                                            {
                                                miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                            }
                                        }

                                        else
                                        {
                                            if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                            {
                                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                            }
                                            else
                                            {
                                                miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                            }
                                        }
                                    }
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


                                            string textmessage = "HERC Status of RTI Application No. - ";

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

                        Session["msg"] = "RTI Application has been published successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                foreach (GridViewRow row in grdRTI.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTI_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIId = p_Var.dataKeyID;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI Application has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdRTI.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTI_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIId = p_Var.dataKeyID;
                        rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "RTI Application has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in grdRTI.Rows)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblRTI_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIId = p_Var.dataKeyID;
                        rtiObject.userID = Convert.ToInt32(Session["User_Id"]);
                        rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                        char[] splitter = { ';' };
                        DataSet dsMail = new DataSet();
                        DataSet dsSms = new DataSet();
                        dsMail = rtiBL.getEmailIdToSendRTIEmail(rtiObject);
                        dsSms = rtiBL.getMobileToSendRTIEmail(rtiObject);
                        p_Var.Result = rtiBL.InsertRtiIntoWeb(rtiObject);



                        /* Function to get email id of petitioners/respondents to send email*/
                        p_Var.sbuildertmp.Append(" <table width='90%' cellpadding='0' cellspacing='0' style='margin:0px auto; border:1px solid #cccccc; border-bottom:none;  border-right:none' align='center'>");
                        p_Var.sbuildertmp.Append("<tr><th style='border-bottom:1px solid #dddddd; padding:5px;  border-right:1px solid #dddddd; text-align:left;background:#e5e5e5;'>");
                        p_Var.sbuildertmp.Append("Status of RTI Application No : " + dsMail.Tables[0].Rows[0]["Ref_No"].ToString() + "," + " Dated: " + dsMail.Tables[0].Rows[0]["Application_Date"].ToString());


                        p_Var.sbuildertmp.Append("<tr><td style='border-bottom:1px solid #dddddd;  border-right:1px solid #dddddd; padding:5px;'>");
                        p_Var.sbuildertmp.Append("Status : " + HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["RTI_Status"].ToString()));
                        p_Var.sbuildertmp.Append("</td></tr>");
                        p_Var.sbuildertmp.Append("</table>");

                        p_Var.sbuildertmp.Append("<br />");
                        p_Var.sbuildertmp.Append("For details see attached file.You may also visit HERC website : <a href='http://herc.gov.in'>http://herc.gov.in</a>");
                        p_Var.sbuildertmp.Append("<br />This is a system generated email. Please do not reply to this email id. For any <br />");
                        p_Var.sbuildertmp.Append("query, you may contact PIO at 0172-2572993 or APIO at 0172-2569602<br/><br/>");

                        p_Var.sbuildertmp.Append("Disclaimer: This e-mail (including any attachments) may contain information that is privileged, confidential, and/or otherwise protected from disclosure to anyone other than its intended recipient(s). Any dissemination or use of this e-mail or its contents (including any attachments) by persons other than the intended recipient(s) is strictly prohibited. If you have received this e-mail by mistake, please notify immediately to sm.herc@nic.in and delete this e-mail (including any attachments) in its entirety.<br />");

                        p_Var.sbuildertmp.Append("<p style='Color:Green;'>Please consider the environment - do not print this e-mail unless absolutely necessary!</p>");


                        /* Function to get email id of petitioners/respondents to send email*/

                        //end

                        /* Function to get email id of petitioners/respondents to send email*/


                        if (dsMail.Tables[0].Rows.Count > 0 && dsMail.Tables[0].Rows[0]["Applicant_Email"] != DBNull.Value && dsMail.Tables[0].Rows[0]["Applicant_Email"] != "")
                        {
                            foreach (DataRow drRow in dsMail.Tables[0].Rows)
                            {
                                //loop through cells in that row

                                if (Convert.ToInt32(drRow["RTI_Status_Id"]) != Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess))
                                {
                                    if (drRow["filename"] != null)
                                    {
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                        }
                                        else
                                        {
                                            miscellBL.SendEmailWithAttachments("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString(), drRow["filename"].ToString());
                                        }
                                    }

                                    else
                                    {
                                        if (Convert.ToInt32(ddlDepartment.SelectedValue) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
                                        {
                                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "HERC–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                        }
                                        else
                                        {
                                            miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + drRow["Applicant_Email"].ToString().Trim(), "EO–RTI Application Status", "no-reply.herc@nic.in", p_Var.sbuildertmp.ToString());
                                        }
                                    }
                                }
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


                                        string textmessage = "HERC Status of RTI Application No. -";

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
                    Session["msg"] = "RTI Application has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
                foreach (GridViewRow row in grdRTI.Rows)
                {
                    Label lnk_Reference_Number = (Label)row.FindControl("lnk_Reference_Number");
                    Label lblYear = (Label)row.FindControl("lblYear");

                    ViewState["DisRTI"] = lnk_Reference_Number.Text;
                    ViewState["DisRtiYear"] = lblYear.Text;
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        sbuilder.Append("<b>RTI -</b> " + lnk_Reference_Number.Text + " of " + lblYear.Text + "<br/> ");
                        Label lblid = (Label)row.FindControl("lblRTI_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        rtiObject.TempRTIId = p_Var.dataKeyID;
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

                        p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
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


                    Session["msg"] = "RTI Application has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            foreach (GridViewRow row in grdRTI.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblRTI_ID");
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    rtiObject.TempRTIId = p_Var.dataKeyID;
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

                    p_Var.Result = rtiBL.updateRtiStatus(rtiObject);
                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "RTI Application has been disapproved successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
