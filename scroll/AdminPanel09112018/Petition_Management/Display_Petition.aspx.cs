using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Auth_AdminPanel_Petition_Management_Display_Petition : System.Web.UI.Page
{
    //Area for all the variables declaration 

    #region Variables declaration zone

    Project_Variables p_Var = new Project_Variables();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    UserOB obj_userOB = new UserOB();
    UserBL obj_UserBL = new UserBL();
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

        Session.Remove("Sohdeptt"); // SOH sessions
        Session.Remove("SohLng");
        Session.Remove("SohYear");
        Session.Remove("SohStatus");

        Session.Remove("SohPetitionAppeal");
        Session.Remove("SohAppeal");
        Session.Remove("appealYear1");

        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": View  Petitions";
        this.Page.Title = " Petition: HERC";
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
            rptPager.Visible = false;
            lblPageSize.Visible = false;
            ddlPageSize.Visible = false;
            btnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            btnAddPetition.Visible = false;
            btnForReview.Visible = false;
           // PLanguage.Visible = false;
            //Get_Deptt_Name();
            if (Session["PetLng"] != null)
            {
               // bindropDownlistLang();
               // ddlLanguage.SelectedValue = Session["PetLng"].ToString();
            }
            else
            {
               // bindropDownlistLang();
            }
          
            if (Session["PetYear"] != null)
            {
                bindRtiYearinDdl();
                ddlYear.SelectedValue = Session["PetYear"].ToString();
            }
            else
            {
                bindRtiYearinDdl();
            }

            if (Session["PetStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["PetStatus"].ToString();
                Bind_Grid("", "");
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

    //Area for all buttons, link buttons, and image buttons click events

    #region button btnAddPetition click event to add new petition

    protected void btnAddPetition_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Petition_Management/") +"Petition_Add_Edit.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
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

    #region button btnForApprove click event to send data for approve 

    protected void btnForApprove_Click(object sender, EventArgs e)
    {

        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();
        //this.mpeSendEmail.Show();
    }

    #endregion

    #region button btnApprove click event to approve pending records

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        bindCheckBoxListWithEmailIDs();
        pnlGrid.Visible = false;
       // ChkApprove_Disapprove();
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
       
    }

    #endregion

    #region button btnReviewPetition click event to see review petition

    protected void btnReviewPetition_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/AdminPanel/Petition_Management/Review_Petition_Display.aspx?ModuleID="+Request.QueryString["ModuleID"]);
    }

    #endregion

    #region button btnDelete click event to delete petition 

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        p_Var.commandArgs = (string[])ViewState["commandArgs"];
        p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
        p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        petObject.RPId = p_Var.rp_id;
        petObject.StatusId = p_Var.status_id;

        p_Var.Result = petPetitionBL.Delete_Petition(petObject);
        if (p_Var.Result > 0)
        {
            if (ddlStatus.SelectedValue == "8")
            {
                
                Session["msg"] = "Petition has been deleted (purged) permanently.";
            }
            else
            {
                Session["msg"] = "Petition has been deleted successfully.";
            }

            obj_audit.ActionType = "D";
            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
            obj_audit.UserName = Session["UserName"].ToString();
            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            obj_audit.IpAddress = miscellBL.IpAddress();
            obj_audit.Title = "Petition";
            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }

    }

    #endregion

    #region linkButton lnkPage click event to change page number

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Bind_Grid("", "");
    }

    #endregion

    #region button btnUpdateStatus click event to update status of petition

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        p_Var.petition_id = Convert.ToInt32(ViewState["id"]);
        petObject.PetitionId = p_Var.petition_id;
        if (Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue) == 25)
        {
            PetitionOB objpet = new PetitionOB();
            objpet.subject = txtOtherStatus.Text;
            objpet.StatusId = 3;
            p_Var.Result = petPetitionBL.Insert_Status(objpet,out p_Var.k);
            petObject.PetitionStatusId = p_Var.Result;

        }
        else
        {
            petObject.PetitionStatusId = Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue);
        }
        p_Var.Result = petPetitionBL.petitionStatusUpdate(petObject);
        if (p_Var.Result > 0)
        {
            Session["msg"] = "Petition's status has been updated successfully.";
            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        }

    }

    #endregion

    //End

    //Area for all dropDownlist events

    #region dropDownlist ddlLanguage selectedIndexChanged event zone

    //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLanguage.SelectedValue != "0")
    //    {
    //        binddropDownlistStatus();
    //        grdPetition.Visible = false;
    //        Session["PetLng"] = ddlLanguage.SelectedValue;
    //    }
    //}

    #endregion

    #region dropDownlist ddlStatus selectedIndexChanged event zone

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            grdPetition.Visible = false;
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

            Bind_Grid("", "");
            //BindGridDetails();
            Session["PetStatus"] = ddlStatus.SelectedValue;
        }
    }

    #endregion

    #region dropDownlist ddlPageSize selectedIndexChanged event

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid("", "");
    }

    #endregion

    //End

    //Area for all user defined functions

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
                btnAddPetition.Visible = true;

                //code written on date 23sep 2013
                p_Var.sbuilder.Append(Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddPetition.Visible = false;
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
            ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Status", "0"));

            btnForReview.Visible = false;
        }

        p_Var.dSet = null;
    }

    #endregion

    #region Function to bind gridView with petitions

    public void Bind_Grid(string sortExp, string sortDir)
    {
        ViewState["o"] = sortDir;
        ViewState["e"] = sortExp;
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdPetition.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {
            
            grdPetition.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
           // petObject.PageIndex = pageIndex;
           // petObject.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            petObject.year = ddlYear.SelectedValue;
            p_Var.dSet = pet_TempRecordBL.get_Temp_Petition_Records(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    grdPetition.Columns[10].HeaderText = "Purge";
                    grdPetition.Columns[11].Visible = true;
                }
                else
                {
                    grdPetition.Columns[10].HeaderText = "Delete";
                    grdPetition.Columns[11].Visible = false;
                }
                //rptPager.Visible = true;
                //lblPageSize.Visible = true;
                //ddlPageSize.Visible = true;
               
                //Codes for the sorting of records

                DataView myDataView = new DataView();
                myDataView = p_Var.dSet.Tables[0].DefaultView;

                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                grdPetition.DataSource = myDataView;
                grdPetition.DataBind();
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
                        grdPetition.Columns[0].Visible = false;
                        //grdPetition.Columns[7].Visible = true;
                        //p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare();

                        foreach (GridViewRow row in grdPetition.Rows)
                        {

                            System.Web.UI.WebControls.Image imgedit = (System.Web.UI.WebControls.Image)row.FindControl("imgEdit");
                            System.Web.UI.WebControls.Image imgnotedit = (System.Web.UI.WebControls.Image)row.FindControl("imgnotedit");
                            Label lblPetitionID = (Label)row.FindControl("lblPetition_ID");
                            Label lblchangestatus = (Label)row.FindControl("lblchangestatus");
                            LinkButton lnkChangeStatus = (LinkButton)row.FindControl("lnkChangeStatus");
                            petObject.PetitionId = Convert.ToInt32(lblPetitionID.Text);
                            
                            p_Var.dSetCompare = pet_TempRecordBL.get_ID_For_Compare(petObject);
                            for (p_Var.i = 0; p_Var.i < p_Var.dSetCompare.Tables[0].Rows.Count; p_Var.i++)
                            {
                                if (p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_Petition_Id"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(lblPetitionID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[p_Var.i]["Old_Petition_Id"]))
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
                        grdPetition.Columns[0].Visible = true;
                        //grdPetition.Columns[7].Visible = false;
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
                            grdPetition.Columns[7].Visible = false;//change status
                            grdPetition.Columns[9].Visible = false;
                            grdPetition.Columns[11].Visible = true;//This is for restore
                            grdPetition.Columns[12].Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdPetition.Columns[7].Visible = true;
                                grdPetition.Columns[12].Visible = true;
                            }
                            else
                            {
                                grdPetition.Columns[7].Visible = false;
                                grdPetition.Columns[12].Visible = false;
                            }
                           
                            grdPetition.Columns[9].Visible = true;
                            grdPetition.Columns[11].Visible = false;//This is for restore
                        }
                    }
                    else
                    {
                        grdPetition.Columns[9].Visible = false;
                        grdPetition.Columns[12].Visible = false;
                    }
                    if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)
                    {
                        ////grdPetition.Columns[10].Visible = true; // commented on date 23 sep 2013

                        // modify on date 23 Sep 2013 by ruchi

                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                            {
                                grdPetition.Columns[10].Visible = true;
                                grdPetition.Columns[12].Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
                                {
                                    grdPetition.Columns[10].Visible = true;
                                    grdPetition.Columns[12].Visible = false;
                                }
                                else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdPetition.Columns[12].Visible = true;
                                    }
                                    else
                                    {
                                        grdPetition.Columns[12].Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt16(Session["Role_Id"]) == Convert.ToInt16(Module_ID_Enum.hercType.superadmin))
                                    {
                                        grdPetition.Columns[10].Visible = true;
                                        if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                                        {
                                            grdPetition.Columns[12].Visible = true;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.ForApprover))
                                            {
                                                grdPetition.Columns[12].Visible = true;
                                            }
                                            else
                                            {
                                                grdPetition.Columns[12].Visible = false;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        grdPetition.Columns[10].Visible = false;
                                        grdPetition.Columns[12].Visible = false;
                                    }
                                }
                            }
                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review))
                            {
                                grdPetition.Columns[10].Visible = true;
                                grdPetition.Columns[12].Visible = false;
                            }

                        }
                        if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
                        {
                            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                            {
                                grdPetition.Columns[10].Visible = true;

                            }

                        }

                        //End  
                    }
                    else
                    {
                        grdPetition.Columns[10].Visible = false;
                    }
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        grdPetition.Columns[7].Visible = true; // this is for change status
                    }
                    else
                    {
                        grdPetition.Columns[7].Visible = false;
                    }
                }
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
            else
            {
                //rptPager.Visible = false;
                //lblPageSize.Visible = false;
                //ddlPageSize.Visible = false;
                grdPetition.Visible = false;
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

    #region Function to approve petitions

    public void ChkApprove_Disapprove()
    {
       
    }

    #endregion

    #region Function to bind Petition status in dropDownlist

    public void bindDdlPetitionStatus()
    {
        //Miscelleneous_BL miscellBL=new Miscelleneous_BL();
        try
        {

            petObject.PetitionStatusId = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            p_Var.dSet = miscellBL.getPetitionStatusAccordingtoModule(petObject);
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

    #region gridView grdPetition rowCommand event zone

    protected void grdPetition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Review")
        {
            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
            p_Var.petition_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.pro_number  = p_Var.commandArgs[1].ToString();


            Response.Redirect("~/Auth/AdminPanel/Petition_Management/PetitionReview_Insert_Update.aspx?Petion_id=" + p_Var.petition_id + "&P_Number=" + p_Var.pro_number + "&ModuleID=" + Request.QueryString["ModuleID"]);            
        }
        else if (e.CommandName == "delete")
        {
            p_Var.commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });
            ViewState["commandArgs"] = p_Var.commandArgs;
            p_Var.petition_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

        }
        else if (e.CommandName == "ChangeStatus")
        {
            txtOtherStatus.Visible = false;
            bindDdlPetitionStatus();
            petObject.TempPetitionId = Convert.ToInt32(e.CommandArgument.ToString());
            petObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = petPetitionBL.get_Temp_Petition_Records_Edit(petObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.InProcess) && Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.PublicNotice) && Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.ScheduleForHearing) && Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.InterimOrder) && Convert.ToInt16(p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"]) != Convert.ToInt16(Module_ID_Enum.Petition_Status.FinalOrder))
                {
                    ddlPetitionStatusUpdate.SelectedValue = Convert.ToInt16(Module_ID_Enum.Petition_Status.anyOther).ToString();
                    txtOtherStatus.Visible = true;
                    txtOtherStatus.Text = p_Var.dSet.Tables[0].Rows[0]["Petition_Status"].ToString();
                }
                else
                {
                 
                     ddlPetitionStatusUpdate.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Petition_Status_Id"].ToString();
                     txtOtherStatus.Visible = false;

                }
            }
            ViewState["id"] = e.CommandArgument.ToString();
           
            this.mpuUpdateStatus.Show();
        }

        else if (e.CommandName == "Restore")
        {

            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);


            p_Var.Result = petPetitionBL.updatePetitionStatusDelete(petObject);
            if (p_Var.Result > 0)
            {

                obj_audit.ActionType = "R";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.Title = "Petition";
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "Petition has been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
            }
            else
            {
                Session["msg"] = "Petition has not been restored successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
            }
        }
        else if (e.CommandName == "Audit")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition);
            petObject.ModuleType = null;
            dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
            Label lblprono=row.FindControl("lblProNumber") as Label;
            if (dSetAuditTrail.Tables[0].Rows.Count > 0)
            {
                ltrlPetitionNo.Text = "<strong>"+lblprono.Text+"</strong>";
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

    #region gridView grdPetition rowCreated event zone

    protected void grdPetition_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow & (e.Row.RowState == DataControlRowState.Normal | e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
            CheckBox chkBxHeader = (CheckBox)this.grdPetition.HeaderRow.FindControl("chkSelectAll");
            //Add client side function childclick on check boxes
            chkBxSelect.Attributes.Add("onclick", string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID));


        }
    }

    #endregion

    #region gridView grdPetition rowDatabound event zone

    protected void grdPetition_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // ((LinkButton)e.Row.FindControl("lnk_PRO_Number")).Text = "HERC/PRO-" + ((LinkButton)e.Row.FindControl("lnk_PRO_Number")).Text;
            if (Convert.ToInt16(ddlStatus.SelectedValue) == (int)Module_ID_Enum.Module_Permission_ID.Approved)
            {
                LinkButton lnkReview = (LinkButton)e.Row.FindControl("lnkReview");
                Label lblReview = (Label)e.Row.FindControl("lblReview");
                HiddenField hid = (HiddenField)e.Row.FindControl("hidPetitionStatusId");
                
                Label lblPetitionID = (Label)e.Row.FindControl("lblPetition_ID");
                Label lblPro = (Label)e.Row.FindControl("lblPro");
                LinkButton lnkChangeStatus = (LinkButton)e.Row.FindControl("lnkChangeStatus");
                lnkChangeStatus.OnClientClick = "javascript:return confirm('Are you sure you want to change status of petition No- " + lblPro.Text + "?');";
                lnkReview.OnClientClick = "javascript:return confirm('Are you sure Review Petition has been received against this Petition No- " + lblPro.Text + "?');";
                petObject.PetitionId = Convert.ToInt32(lblPetitionID.Text);

                p_Var.dSetCompare = petPetitionBL.get_PetitionID_From_Temp_PetReview(petObject);
                if (p_Var.dSetCompare.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(lblPetitionID.Text) == Convert.ToInt32(p_Var.dSetCompare.Tables[0].Rows[0]["Petition_id"]))
                    {
                        lnkReview.Visible = false;
                        lblReview.Visible = true;
                        lblReview.Text = "Yes";
                    }
                    else
                    {
                        lnkReview.Visible = true;
                        lblReview.Visible = false;

                        if (lnkReview.Text == "False")
                        {
                            lnkReview.Text = "Review";
                        }
                        else
                        {
                            lnkReview.Text = "Reviewed";
                        }
                    }
                }
                else
                {
                    ////////lnkReview.Visible = true;
                    ////////lblReview.Visible = false;

                    if (lnkReview.Text == "False")
                    {
                        if (hid.Value == "13")
                        {
                            lnkReview.Visible = true;
                            lblReview.Visible = false;
                            lnkReview.Text = "Review";
                        }
                        else
                        {
                            lnkReview.Visible = false;

                            //Change from true to false on date 05-03-2013
                            lblReview.Visible = false;
                            //End
                        }
                    }
                    else
                    {
                        lnkReview.Text = "Reviewed";
                    }
                }

                
            }
            else
            {
                LinkButton lnkReview = (LinkButton)e.Row.FindControl("lnkReview");
                Label lblReview = (Label)e.Row.FindControl("lblReview");

                lnkReview.Visible = false;
                lblReview.Visible = true;

                if (lblReview.Text == "False")
                {
                    lblReview.Text = "Review";
                }
                else
                {
                    lblReview.Text = "Reviewed";
                }
            }


            //This is for delete/permanently delete 3 june 2013 
            ImageButton BtnDelete = (ImageButton)e.Row.FindControl("BtnDelete");
            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");
            if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.Delete))
            {

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to purge Petition No- " + DataBinder.Eval(e.Row.DataItem, "pro1") + "?')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are You sure want to Restore Petition No-" + DataBinder.Eval(e.Row.DataItem, "pro1") + "?')");

            }
            else
            {

                BtnDelete.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to delete Petition No- " + DataBinder.Eval(e.Row.DataItem, "pro1") + "?')");

                BtnRestore.Attributes.Add("onclick", "javascript:return " +
                  "confirm('Are You sure want to Restore Petition No-" + DataBinder.Eval(e.Row.DataItem, "pro1") + "?')");

            }

            //END
        }
    }

    #endregion

    #region gridView grdPetition rowDeleting event zone

    protected void grdPetition_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblReview = grdPetition.Rows[e.RowIndex].Cells[8].FindControl("lblReview") as Label;
        p_Var.petition_id = Convert.ToInt32(grdPetition.DataKeys[e.RowIndex].Value);
        if (lblReview.Text == "Yes")
        {
            petObject.PetitionId = Convert.ToInt32(p_Var.petition_id);
            p_Var.dSet = petPetitionBL.get_Review_Appeal_Number_for_Delete(petObject);

        }
        else
        {

            p_Var.commandArgs = (string[])ViewState["commandArgs"];
            p_Var.rp_id = Convert.ToInt32(p_Var.commandArgs[0]);
            p_Var.status_id = Convert.ToInt32(p_Var.commandArgs[1]);

            //petObject.RPId = p_Var.rp_id;
            petObject.RPId = p_Var.rp_id;
            petObject.StatusId = p_Var.status_id;

            p_Var.Result = petPetitionBL.Delete_Petition(petObject);
            if (p_Var.Result > 0)
            {
                if (ddlStatus.SelectedValue == "8")
                {
                    Session["msg"] = "Petition has been deleted (purged) permanently.";
                }
                else
                {
                    Session["msg"] = "Petition has been deleted successfully.";
                }

                obj_audit.ActionType = "D";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.Title = "Petition";
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
            }

        }
       
    }

    #endregion

    //End

    protected void ddlPetitionStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlPetitionStatusUpdate.SelectedValue) == 25)
        {
            txtOtherStatus.Visible = true;
            
        }
        else
        {
            txtOtherStatus.Visible = false;
        }

        this.mpuUpdateStatus.Show();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddropDownlistStatus();
        grdPetition.Visible = false;
        Session["PetYear"] = ddlYear.SelectedValue;
        btnPdf.Visible = false;
    }

    #region Function to bind Petition Year

    public void bindRtiYearinDdl()
    {
        
        p_Var.dSet = petPetitionBL.GetYearPetition_Admin();

        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = p_Var.dSet;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
        else
        {
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
    }

    #endregion

    protected void grdPetition_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_Grid(e.SortExpression, sortOrder);
       
    }

    #region gridView grdPetition pageIndexChanging Event zone

    protected void grdPetition_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPetition.PageIndex = e.NewPageIndex;
        Bind_Grid(ViewState["e"].ToString(), ViewState["o"].ToString());
    }

    #endregion

    //End

    //New codes for paginations


    #region button btnPetition_Appeal click event to go to petition appeal page

    protected void btnPetition_Appeal_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Petition_Management/") +"Display_Petition_Appeal.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Petition));
    }

    #endregion

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

    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=3"));
    }



    #region Function to bind gridView with petitions

    public void BindGridDetails()
    {
        
        PetitionBL pet_TempRecordBL = new PetitionBL();
        if (ddlStatus.SelectedValue == "0")
        {
            grdPetition.Visible = false;
            btnForReview.Visible = false;
        }
        else
        {

            grdOrderPdf.Visible = true;
            petObject.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            petObject.LangId = Convert.ToInt32(Module_ID_Enum.Language_ID.English);
         
            petObject.year = ddlYear.SelectedValue;
            p_Var.dSet = pet_TempRecordBL.get_Temp_Petition_Records(petObject, out p_Var.k);


            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {

               

                grdOrderPdf.DataSource = p_Var.dSet;
                grdOrderPdf.DataBind();
                p_Var.dSet = null;

               
                p_Var.dSet = null;
                lblmsg.Visible = false;
            }
           

        }
      
    }

    #endregion

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        BindGridDetails();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Petition_"+System.DateTime.Now.ToShortDateString()+".xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdOrderPdf.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    #region Gridview grdOrderPdf event RowDataBound

    protected void grdOrderPdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltrlConnectedFile1 = (Literal)e.Row.FindControl("ltrlConnectedFile1");
            petObject.PetitionId = Convert.ToInt32(grdOrderPdf.DataKeys[e.Row.RowIndex].Value.ToString());
            p_Var.dsFileName = petPetitionBL.getPetitionFileNames(petObject);
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

            lblRemarks.Text = HttpUtility.HtmlDecode(lblRemarks.Text);
        }
    }

    #endregion

    protected void btnSendEmails_Click(object sender, EventArgs e)
    {

        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                if (Page.IsValid)
                {
                    if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.draft))
                    {
                        sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                        foreach (GridViewRow row in grdPetition.Rows)
                        {
                            CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                            Label lblProNumber = (Label)row.FindControl("lblProNumber");
                            ViewState["ProNumber"]=lblProNumber.Text;
                            if ((selCheck.Checked == true))
                            {
                                sbuilder.Append("<b>Petition - " + lblProNumber.Text + "<br/></b>");
                                Label lblid = (Label)row.FindControl("lblPetition_ID");
                                p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                                petObject.TempPetitionId = p_Var.dataKeyID;
                                petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                                petObject.IpAddress = Miscelleneous_DL.getclientIP();
                                p_Var.Result = petPetitionBL.ASP_TempPetition_Update_Status_Id(petObject);


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
                                p_Var.sbuilder.Append("Record pending for Review : " + sbuilder.ToString() + "<br/>");
                                p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
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
                            Session["msg"] = "Petition has been sent for review successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                        }
                    }
                    else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
                    {
                        sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                        foreach (GridViewRow row in grdPetition.Rows)
                        {
                            CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                            Label lblProNumber = (Label)row.FindControl("lblProNumber");
                            ViewState["ProNumber"] = lblProNumber.Text;
                            if ((selCheck.Checked == true))
                            {
                                Label lblid = (Label)row.FindControl("lblPetition_ID");
                                sbuilder.Append("<b>Petition - " + lblProNumber.Text + "<br/> </b>");
                                p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                                petObject.TempPetitionId = p_Var.dataKeyID;
                                petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                                petObject.IpAddress = Miscelleneous_DL.getclientIP();
                                petObject.userID = Convert.ToInt16(Session["User_Id"]);
                                p_Var.Result = petPetitionBL.ASP_TempPetition_Update_Status_Id(petObject);


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
                                p_Var.sbuilder.Append("Record pending for Publish : " + sbuilder.ToString()+ "<br/>");
                                p_Var.sbuilder.Append("from <b>" + Session["UserName"] + "</b>(" + Session["Email"] + ")");
                                p_Var.sbuilder.Append("<br/>This is a system generated email. Please do not reply to this email id.");
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
                                                    textmessage = "HERC - Record pending for publish -";

                                                    miscellBL.Sendsms(message, Session["UserName"].ToString(), str, textmessage);

                                                }

                                            }
                                       
                                    }
                                }
                               
                            
                            /* End */

                            Session["msg"] = "Petition has been sent for publish successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                        }
                    }
                    else //Here code is to approve records on date 12-05-2014
                    {
                        sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                        LinkOB obj_linkOB1 = new LinkOB();
                        foreach (GridViewRow row in grdPetition.Rows)
                        {
                            CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                            Label lblProNumber = (Label)row.FindControl("lblProNumber");
                            ViewState["ProNumber"] = lblProNumber.Text;
                            if ((selCheck.Checked == true))
                            {

                                sbuilder.Append("<b>Petition - " + lblProNumber.Text + "<br/> </b>");
                               
                                Label lblid = (Label)row.FindControl("lblPetition_ID");
                                p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                                petObject.TempPetitionId = p_Var.dataKeyID;
                                petObject.userID = Convert.ToInt16(Session["User_Id"]);
                                petObject.IpAddress = Miscelleneous_DL.getclientIP();
                                p_Var.Result = petPetitionBL.ASP_Insert_Web_Petiton(petObject);
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

                            Session["msg"] = " Petition has been published successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                        }
                    }//End
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
                foreach (GridViewRow row in grdPetition.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblPetition_ID");
                        
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPetitionId = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.review;
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = petPetitionBL.ASP_TempPetition_Update_Status_Id(petObject);


                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = "Petition has been sent for review successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else if (Convert.ToInt16(ddlStatus.SelectedValue) == Convert.ToInt16(Module_ID_Enum.Module_Permission_ID.review)) //Review Case
            {
                foreach (GridViewRow row in grdPetition.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblPetition_ID");

                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPetitionId = p_Var.dataKeyID;
                        petObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                        petObject.userID = Convert.ToInt16(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = petPetitionBL.ASP_TempPetition_Update_Status_Id(petObject);


                    }
                }
                if (p_Var.Result > 0)
                {

                    Session["msg"] = "Petition has been sent for publish successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
            else
            {
                LinkOB obj_linkOB1 = new LinkOB();
                foreach (GridViewRow row in grdPetition.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblPetition_ID");
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPetitionId = p_Var.dataKeyID;
                        petObject.userID = Convert.ToInt16(Session["User_Id"]);
                        petObject.IpAddress = Miscelleneous_DL.getclientIP();
                        p_Var.Result = petPetitionBL.ASP_Insert_Web_Petiton(petObject);
                    }
                }
                if (p_Var.Result > 0)
                {
                    Session["msg"] = " Petition has been published successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                    Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                }
            }
        }
        catch
        {
            throw;
        }
    }

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
    protected void btnSendEmailsDis_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                StringBuilder sbuilder = new StringBuilder();
                StringBuilder sbuilderSms = new StringBuilder();
                foreach (GridViewRow row in grdPetition.Rows)
                {
                    CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                    Label lblProNumber = (Label)row.FindControl("lblProNumber");
                    ViewState["DisProNumber"] = lblProNumber.Text;
                    if ((selCheck.Checked == true))
                    {
                        Label lblid = (Label)row.FindControl("lblPetition_ID");
                        sbuilder.Append("<b>Petition - " + lblProNumber.Text + "<br/> </b>");
                        // p_Var.dataKeyID = Convert.ToInt32(grdPetition.DataKeys[row.RowIndex].Value);
                        p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                        petObject.TempPetitionId = p_Var.dataKeyID;

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
                        p_Var.Result = petPetitionBL.ASP_TempPetition_Update_Status_Id(petObject);


                    }
                }
                if (p_Var.Result > 0)
                {
                    p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                    foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
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
                        p_Var.sbuilder.Append("Record disapproved : " + sbuilder.ToString()+"<br/>");
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


                    Session["msg"] = "Petition has been disapproved successfully.";
                    Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
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
            
            foreach (GridViewRow row in grdPetition.Rows)
            {
                
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                    Label lblid = (Label)row.FindControl("lblPetition_ID");
                    
                    p_Var.dataKeyID = Convert.ToInt32(lblid.Text);
                    petObject.TempPetitionId = p_Var.dataKeyID;

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
                    p_Var.Result = petPetitionBL.ASP_TempPetition_Update_Status_Id(petObject);


                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "Petition has been disapproved ";
                Session["Redirect"] = "~/Auth/AdminPanel/Petition_Management/Display_Petition.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        catch
        {
            throw;
        }
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

    protected void btnCancelEmail_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmails.Visible = false;
    }
    protected void btnCancelEmailDis_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlPopUpEmailsDis.Visible = false;
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
}
