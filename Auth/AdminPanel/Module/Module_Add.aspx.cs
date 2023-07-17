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
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

public partial class Auth_AdminPanel_Module_Module_Add : CrsfBase //System.Web.UI.Page
{

    #region Variable declaration zone

    LinkOB obj_LinkOB = new LinkOB();
    LinkBL obj_LinkBL = new LinkBL();
    UserOB obj_userOB = new UserOB();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Project_Variables p_Var = new Project_Variables();
    HtmlSanitizer removerBL = new HtmlSanitizer();
    PetitionBL petPetitionBL = new PetitionBL();
    //ModuleAuditTrailDL _moduleauditTraildl = new ModuleAuditTrailDL();
    //ModuleAuditTrail _moduleAuditTrail = new ModuleAuditTrail();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    #endregion

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        p_Var.imageUrl = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Image"].ToString() + "/";
        p_Var.url = "~/" + ConfigurationManager.AppSettings["WriteReadData"].ToString() + "/" + ConfigurationManager.AppSettings["Pdf"].ToString() + "/";

        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_RoleBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }


            BtnUpdate.Visible = false;
            LblOldFile.Visible = false;
            LblOldImage.Visible = false;
            lnkFileRemove.Visible = false;
            lnkImageRemove.Visible = false;
            //26 Nov
            PSmalldesc.Visible = true;

            bindropDownlistLang(); // Get the Language privilage
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
            {
                //26 Nov
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add Banner";
                this.Page.Title = "Add Banner: HERC";
                PSmalldesc.Visible = false;

                pStart.Visible = false;
                pEnd.Visible = false;
            }

            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
            {
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add Disscussion Paper";
                this.Page.Title = "Add Banner: Disscussion Paper";

                PSmalldesc.Visible = false;
                pFileUpload.Visible = false;
                pMultipleFile.Visible = true;
                pImage.Visible = false;
                pVenu.Visible = true;
                pLastDateRcv.Visible = true;
                pPublicHearing.Visible = true;
                pDescEditor.Visible = false;
                pEnd.Visible = false;
                pEndMendatory.Visible = true;
                lbldiscussionMsg.Visible = true;
                lblLastDateRcv.Text = "Last Date of Receiving Comments (If any):";
                PTime.Visible = true;
                bindTimeinDropdownlist();
                bindHoursinDropdownlist();
                bindampminDropdownlist();
                pStart.Visible = true;
                //pEnd.Visible = true;
            }
            else
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                {
                    //26 Nov
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Add Notification";
                    this.Page.Title = "Add Notification: HERC";
                    PSmalldesc.Visible = false;
                    pImage.Visible = false;

                    pStart.Visible = false;
                    pEnd.Visible = false;
                }

                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
                {
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Add Annual Report";
                    this.Page.Title = "Add Annual Report: HERC";

                    PSmalldesc.Visible = false;
                    pImage.Visible = false;
                    pDescEditor.Visible = false;
                    //// pEnd.Visible = true;

                    pStart.Visible = true;
                    pEnd.Visible = true;
                }
                else
                {
                    if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                    {
                        rfvFileUpload.Enabled = true;
                        PSmalldesc.Visible = false;
                        Label lblModulename = Master.FindControl("lblModulename") as Label;
                        lblModulename.Text = ": Add Banner";
                        this.Page.Title = "Add Banner: HERC";
                    }
                    else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
                    {
                        PSmalldesc.Visible = true;
                        Label lblModulename = Master.FindControl("lblModulename") as Label;
                        lblModulename.Text = ": Add Disscussion Paper";
                        this.Page.Title = "Add Disscussion Paper: HERC";
                    }
                    else
                    {
                        PSmalldesc.Visible = true;
                    }
                    pImage.Visible = true;

                    pFileUpload.Visible = true;
                    pMultipleFile.Visible = false;

                    pVenu.Visible = false;
                    pLastDateRcv.Visible = false;
                    pPublicHearing.Visible = false;
                    pDescEditor.Visible = true;
                    pEnd.Visible = true;
                    pEndMendatory.Visible = false;
                    lbldiscussionMsg.Visible = false;
                    PTime.Visible = false;
                }


            }
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                this.Page.Title = "Add Vacancy: HERC";
                Label lblModulename = Master.FindControl("lblModulename") as Label;
                lblModulename.Text = ": Add Vacancy";
                pDescEditor.Visible = false;
                //PCirculationPublicNotice.Visible = true;
                pImage.Visible = false;
                pLastDateRcv.Visible = true;
                PSmalldesc.Visible = false;
                pFileUpload.Visible = false;
                pMultipleFile.Visible = true;
                pEnd.Visible = false;
                pEndMendatory.Visible = true;
                lblLastDateRcv.Text = "Last Date of Receiving Applications (If any):";

                pStart.Visible = true;
                //pEnd.Visible = true;
            }

            if (Request.QueryString["Temp_Link_Id"] != null)    // for Edit
            {
                LblOldFile.Visible = true;
                LblOldImage.Visible = true;
                lnkFileRemove.Visible = true;
                lnkImageRemove.Visible = true;
                PDepartment.Visible = false;
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                {
                    rfvFileUpload.Enabled = false;
                    this.Page.Title = "Edit Banner: HERC";
                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Banner";

                    pAltTag.Visible = true;
                    pAltTag_Reg.Visible = true;
                    pDesc.Visible = true;
                    pDesc_Reg.Visible = true;
                    pDescEditor.Visible = false;
                    pFileUpload.Visible = false;
                    PLanguage.Visible = false;
                    pTitle_Reg.Visible = true;
                    //26 Nov
                    PSmalldesc.Visible = false;

                    pStart.Visible = false;
                    pEnd.Visible = false;
                }
                Display(Request.QueryString["Temp_Link_Id"].ToString());
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                {
                    this.Page.Title = "Edit Notification: HERC";

                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Notification";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                {
                    this.Page.Title = "Edit Banner: HERC";

                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Banner";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
                {
                    this.Page.Title = "Edit Annual Report: HERC";

                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Annual Report";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
                {
                    this.Page.Title = "Edit Disscussion Paper: HERC";

                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Disscussion Paper";
                }
                else if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                {
                    this.Page.Title = "Edit Vacancy: HERC";

                    Label lblModulename = Master.FindControl("lblModulename") as Label;
                    lblModulename.Text = ": Edit Vacancy";
                }
            }
            else
            {
                PDepartment.Visible = true;
                Get_Deptt_Name();
            }
            if (Request.QueryString["Temp_Link_Id"] == null)    // for Edit
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                {
                    rfvFileUpload.Enabled = true;
                    pAltTag.Visible = true;
                    pAltTag_Reg.Visible = true;
                    pDesc.Visible = true;
                    pDesc_Reg.Visible = true;
                    pDescEditor.Visible = false;
                    pFileUpload.Visible = false;
                    PLanguage.Visible = false;
                    pTitle_Reg.Visible = true;

                    pStart.Visible = false;
                    pEnd.Visible = false;
                }
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    // Area for all user defind functions

    #region Function to display data in edit mode

    public void Display(string Temp_Link_Id)
    {
        PLanguage.Visible = false;
        BtnSubmit.Visible = false;
        BtnUpdate.Visible = true;

        obj_LinkOB.TempLinkId = Convert.ToInt32(Temp_Link_Id);
        obj_LinkOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
        p_Var.dSet = obj_LinkBL.ASP_Links_DisplayBYID(obj_LinkOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["Mate_Description"].ToString();
            txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

            if (p_Var.dSet.Tables[0].Rows[0]["Name"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Name"].ToString() != "")
            {
                txtname.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Name"].ToString());
            }
            if (p_Var.dSet.Tables[0].Rows[0]["Start_Date"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["Start_Date"].ToString() != "")
            {
                txtstartdate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["Start_Date"].ToString());
            }
            else
            {
                txtstartdate.Text = "";
            }
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                if (p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString() != "")
                {
                    txtendate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString());
                }
                else
                {
                    txtendate.Text = "";
                }
            }
            else
            {
                if (p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString() != "")
                {
                    txtenddate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["End_Date"].ToString());
                }
                else
                {
                    txtenddate.Text = "";
                }
            }

            if (Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
            {
                txtDesc_Reg.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Details_Reg"].ToString());
                txtDesc.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Details"].ToString());
                txtAltTag_Reg.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["AltTag_Reg"].ToString());
                txtAltTag.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Alt_Tag"].ToString());
                txtTitle_Reg.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Name_Reg"].ToString());
            }
            else
            {
                FCKeditor1.Value = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Details"].ToString());
                txtSmalldesc.Text = p_Var.dSet.Tables[0].Rows[0]["SmallDetails"].ToString();
            }
            if (Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
            {

                ddlhours.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["time"].ToString().Substring(0, 2);
                ddlmins.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["time"].ToString().Substring(3, 2);

                ddlampm.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["time"].ToString().Substring(6, 4).Trim();
                if (p_Var.dSet.Tables[0].Rows[0]["LastDateOfReceivingComment"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString() != "")
                {
                    txtLastDate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString());
                }
                else
                {
                    txtLastDate.Text = null;
                }
                if (p_Var.dSet.Tables[0].Rows[0]["PublicHearingDate"] != DBNull.Value)
                {
                    TxtPublicHearing.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["PublicHearingDate"].ToString());
                }
                else
                {
                    TxtPublicHearing.Text = null;
                }

                if (p_Var.dSet.Tables[0].Rows[0]["Venu"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["Venu"].ToString() != "")
                {
                    txtVenue.Text = p_Var.dSet.Tables[0].Rows[0]["Venu"].ToString();
                }
                else
                {
                    txtVenue.Text = null;
                }
            }
            if (Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                //if (p_Var.dSet.Tables[0].Rows[0]["CirculationPublicNotice"] != DBNull.Value)
                //{
                //    TxtCirculation.Text = p_Var.dSet.Tables[0].Rows[0]["CirculationPublicNotice"].ToString();
                //}
                if (p_Var.dSet.Tables[0].Rows[0]["LastDateOfReceivingComment"] != DBNull.Value && p_Var.dSet.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString() != "")
                {
                    txtLastDate.Text = miscellBL.getDateFormatddMMYYYY(p_Var.dSet.Tables[0].Rows[0]["LastDateOfReceivingComment"].ToString());
                }
            }
            if (Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                BindData(Convert.ToInt32(Temp_Link_Id));
            }
            if (p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString() != "")
            {
                lblFileName.Text = p_Var.dSet.Tables[0].Rows[0]["File_Name"].ToString();
            }
            else
            {
                LblOldFile.Visible = false;
                lnkFileRemove.Visible = false;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["Image_Name"].ToString() != null && p_Var.dSet.Tables[0].Rows[0]["Image_Name"].ToString() != "")
            {
                LblImageName.Text = p_Var.dSet.Tables[0].Rows[0]["Image_Name"].ToString();
            }
            else
            {
                LblOldImage.Visible = false;
                lnkImageRemove.Visible = false;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
            {
                ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
            }
            else
            {
                ViewState["DeptID"] = DBNull.Value;
            }
            if (Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) || Convert.ToInt32(Request.QueryString["Moduleid"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
            {
                BindDataFiles(Convert.ToInt32(Temp_Link_Id));
            }

        }



        p_Var.dSet = null;
    }

    #endregion

    #region Function To Add Details

    public void Add_Module()
    {
        //if (Page.IsValid)
        //{
        //    try
        //    {
        //        obj_LinkOB.ActionType = 1;
        //        obj_LinkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
        //        obj_LinkOB.MateDescription = txtMetaDescription.Text;
        //        obj_LinkOB.MetaKeywords = txtMetaKeyword.Text;
        //        obj_LinkOB.MetaTitle = txtMetaTitle.Text;
        //        obj_LinkOB.MetaLanguage = ddlMetaLang.SelectedValue;

        //        if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
        //        {
        //            obj_LinkOB.Details_Regional = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtDesc_Reg.Text, 250));
        //            obj_LinkOB.details = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtDesc.Text, 250));
        //            obj_LinkOB.AltTag = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtAltTag.Text, 100));
        //            obj_LinkOB.AltTagReg = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtAltTag_Reg.Text, 100));
        //            obj_LinkOB.Name_Regional = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtTitle_Reg.Text, 150));
        //            obj_LinkOB.NAME = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtname.Text, 150));
        //        }
        //        else
        //        {
        //            obj_LinkOB.Smalldetails = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtSmalldesc.Text, 50));
        //            obj_LinkOB.details = miscellBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
        //            obj_LinkOB.NAME = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtname.Text, 2000));

        //        }
        //        // 10 apr 2013
        //        if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
        //        {
        //            PTime.Visible = true;
        //            obj_LinkOB.Time = ddlhours.SelectedValue + ":" + ddlmins.SelectedValue + ":" + ddlampm.SelectedItem.ToString();
        //            if (txtVenue.Text != null && txtVenue.Text != "")
        //            {
        //                obj_LinkOB.venu = txtVenue.Text;
        //            }
        //            else
        //            {
        //                obj_LinkOB.venu = null;

        //            }
        //            if (txtLastDate.Text != null && txtLastDate.Text != "")
        //            {
        //                obj_LinkOB.LastDateReceiving = Convert.ToDateTime(miscellBL.getDateFormat(txtLastDate.Text));
        //            }

        //            if (TxtPublicHearing.Text != null && TxtPublicHearing.Text != "")
        //            {
        //                obj_LinkOB.PublicHearingDate = Convert.ToDateTime(miscellBL.getDateFormat(TxtPublicHearing.Text));
        //            }
        //            else
        //            {
        //                obj_LinkOB.PublicHearingDate = null;
        //            }
        //        }
        //        else
        //        {
        //            PTime.Visible = false;
        //            obj_LinkOB.venu = null;
        //            obj_LinkOB.PublicHearingDate = null;
        //            //obj_LinkOB.LastDateReceiving = null;
        //        }
        //        if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
        //        {
        //            if (txtLastDate.Text != null && txtLastDate.Text != "")
        //            {
        //                obj_LinkOB.LastDateReceiving = Convert.ToDateTime(miscellBL.getDateFormat(txtLastDate.Text));
        //            }

        //        }
        //        obj_LinkOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);

        //        if (txtstartdate.Text != null && txtstartdate.Text != "")
        //        {
        //            obj_LinkOB.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtstartdate.Text));
        //        }
        //        else
        //        {
        //            obj_LinkOB.StartDate = null;
        //        }
        //        if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
        //        {
        //            if (txtendate.Text != null && txtendate.Text != "")
        //            {
        //                obj_LinkOB.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtendate.Text));
        //            }
        //            else
        //            {
        //                obj_LinkOB.EndDate = null;
        //            }
        //        }
        //        else
        //        {

        //            if (txtenddate.Text != null && txtenddate.Text != "")
        //            {
        //                obj_LinkOB.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtenddate.Text));
        //            }
        //            else
        //            {
        //                obj_LinkOB.EndDate = null;
        //            }
        //        }
        //        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module
        //        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;  //draft
        //        obj_LinkOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
        //        if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper)
        //            && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
        //        {
        //            if (FileUploadImage.PostedFile.ContentLength != 0 && FileUploadImage.PostedFile.ContentLength != null)
        //            {
        //                if (miscellBL.CheckImageFileExtension(System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName).ToLower()) && Upload_Image(ref p_Var.ImageName))
        //                {

        //                    obj_LinkOB.ImageName = FileUploadImage.FileName.ToString();

        //                }
        //            }
        //            else
        //            {
        //                obj_LinkOB.ImageName = null;
        //            }
        //        }

        //        if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
        //        {
        //            if (Upload_File(ref p_Var.Filename))
        //            {
        //                if (p_Var.Filename != null)
        //                {
        //                    obj_LinkOB.FileName = FileUpload2.FileName.ToString();
        //                }
        //                else
        //                {
        //                    obj_LinkOB.FileName = null;
        //                }

        //            }
        //            else
        //            {
        //                obj_LinkOB.FileName = null;
        //            }

        //        }
        //        obj_LinkOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
        //        obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
        //        obj_LinkOB.IpAddress = Miscelleneous_DL.getclientIP();
        //        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        //        p_Var.Result = obj_LinkBL.ASP_Links_Insert(obj_LinkOB);
        //        if (p_Var.Result > 0)
        //        {
        //            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
        //            {
        //                //This is for multiple files
        //                if (FileUpload2.PostedFile != null && FileUpload2.PostedFile.InputStream.Length != 0)
        //                {
        //                    obj_LinkOB.TempLinkId = p_Var.Result;
        //                    obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
        //                    obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        //                    p_Var.ext = System.IO.Path.GetExtension(this.FileUpload2.PostedFile.FileName);
        //                    p_Var.ext = p_Var.ext.ToUpper();
        //                    if (p_Var.ext.Equals(".PDF"))
        //                    {
        //                        p_Var.Path = p_Var.url;
        //                        p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
        //                        p_Var.Filename = miscellBL.getUniqueFileName(FileUpload2.FileName, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

        //                        //p_Var.Filename = FileUpload2.FileName;
        //                        FileUpload2.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));
        //                        obj_LinkOB.FileName = p_Var.Filename;
        //                    }

        //                    obj_LinkBL.InsertFiles(obj_LinkOB);
        //                }
        //            }

        //            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
        //            {
        //                string fileMultiple = string.Empty;
        //                HttpFileCollection hfc = Request.Files;

        //                for (int i = 0; i < hfc.Count; i++)
        //                {
        //                    HttpPostedFile hpf = hfc[i];

        //                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
        //                    if (fileMultiple != null && fileMultiple != "")
        //                    {
        //                        LinkOB newObj = new LinkOB();
        //                        newObj.linkID = p_Var.Result;
        //                        newObj.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        //                        newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);

        //                        if (i == 0)
        //                        {
        //                            newObj.Remarks = TextBox1.Text;
        //                        }
        //                        else
        //                        {
        //                            int j = i - 1;


        //                            string strId = "txt" + j.ToString();
        //                            newObj.Remarks = Request.Form[strId].ToString();
        //                        }
        //                        p_Var.ext = System.IO.Path.GetExtension(fileMultiple);
        //                        p_Var.ext = p_Var.ext.ToUpper();
        //                        if (p_Var.ext.Equals(".PDF"))
        //                        {
        //                            lblmsg.Visible = false;
        //                            p_Var.Filename = hpf.FileName;
        //                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
        //                            p_Var.ext = Path.GetExtension(hpf.FileName);
        //                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
        //                            hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
        //                            //newObj.PetitionFile = "P_" + p_Var.Filename;
        //                            newObj.FileName = p_Var.Filename;
        //                        }

        //                        newObj.StartDate = System.DateTime.Now;

        //                        int Result2 = obj_LinkBL.insertConnectedDiscussionFiles(newObj);
        //                    }
        //                }
        //            }
        //            Session["msg"] = "Record has been submitted successfully.";
        //            Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
        //            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
        //        }
        //        else
        //        {
        //            Session["msg"] = "Record has not been submitted successfully.";
        //            Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
        //            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

    }

    #endregion

    #region Function to update the data

    public void Update_Data()
    {
        LinkOB obj_LinkOB2 = new LinkOB();
        lnkFileRemove.Visible = true;
        lnkImageRemove.Visible = true;
        Miscelleneous_BL obj_Miscel1 = new Miscelleneous_BL();
        try
        {
            obj_LinkOB2.ActionType = 2;
            obj_LinkOB2.TempLinkId = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);
            obj_LinkOB2.StatusId = Convert.ToInt32(Session["Status_Id"]);
            obj_LinkOB2.OldLinkId = Convert.ToInt32(Request.QueryString["Temp_Link_Id"]);

            obj_LinkOB2.MateDescription = txtMetaDescription.Text;
            obj_LinkOB2.MetaKeywords = txtMetaKeyword.Text;
            obj_LinkOB2.MetaTitle = txtMetaTitle.Text;
            obj_LinkOB2.MetaLanguage = ddlMetaLang.SelectedValue;
            obj_LinkOB2.UserID = Convert.ToInt32(Session["User_Id"]);
            obj_LinkOB2.IpAddress = Miscelleneous_DL.getclientIP();
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
            {
                obj_LinkOB2.Details_Regional = HttpUtility.HtmlEncode(txtDesc_Reg.Text);
                obj_LinkOB2.details = HttpUtility.HtmlEncode(txtDesc.Text);
                obj_LinkOB2.AltTag = HttpUtility.HtmlEncode(txtAltTag.Text);
                obj_LinkOB2.AltTagReg = HttpUtility.HtmlEncode(txtAltTag_Reg.Text);
                obj_LinkOB2.Name_Regional = HttpUtility.HtmlEncode(txtTitle_Reg.Text);
                obj_LinkOB2.NAME = HttpUtility.HtmlEncode(txtname.Text);
            }
            else
            {
                obj_LinkOB2.Smalldetails = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtSmalldesc.Text, 50));
                obj_LinkOB2.details = miscellBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
                obj_LinkOB2.NAME = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtname.Text, 2000));
            }
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
            {
                PTime.Visible = true;
                obj_LinkOB2.Time = ddlhours.SelectedValue + ":" + ddlmins.SelectedValue + ":" + ddlampm.SelectedItem.ToString();
                if (txtVenue.Text != null && txtVenue.Text != "")
                {
                    obj_LinkOB2.venu = txtVenue.Text;
                }
                else
                {
                    obj_LinkOB2.venu = null;

                }
                if (txtLastDate.Text != null && txtLastDate.Text != "")
                {
                    obj_LinkOB2.LastDateReceiving = Convert.ToDateTime(miscellBL.getDateFormat(txtLastDate.Text));
                }
                else
                {
                    obj_LinkOB2.LastDateReceiving = null;
                }
                if (TxtPublicHearing.Text != null && TxtPublicHearing.Text != "")
                {
                    obj_LinkOB2.PublicHearingDate = Convert.ToDateTime(miscellBL.getDateFormat(TxtPublicHearing.Text));
                }
                else
                {
                    obj_LinkOB2.PublicHearingDate = null;
                }
            }
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
            {
                if (txtLastDate.Text != null && txtLastDate.Text != "")
                {
                    obj_LinkOB2.LastDateReceiving = Convert.ToDateTime(miscellBL.getDateFormat(txtLastDate.Text));
                }

            }
            if (txtstartdate.Text != null && txtstartdate.Text != "")
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                {
                    obj_LinkOB2.StartDate = null;
                }
                else
                {
                    obj_LinkOB2.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtstartdate.Text));
                }
            }
            else
            {
                obj_LinkOB2.StartDate = null;
            }
            //if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
            {
                if (txtendate.Text != null && txtendate.Text != "")
                {
                    if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                    {
                        obj_LinkOB2.EndDate = null;
                    }
                    else
                    {
                        obj_LinkOB2.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtendate.Text));
                    }
                }
                else
                {
                    obj_LinkOB2.EndDate = null;
                }
            }
            else
            {

                if (txtenddate.Text != null && txtenddate.Text != "")
                {
                    obj_LinkOB2.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtenddate.Text));
                }
                else
                {
                    obj_LinkOB2.EndDate = null;
                }
            }
            obj_LinkOB2.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);


            if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
            {
                if (FileUploadImage.PostedFile.ContentLength != 0)
                {
                    if (obj_Miscel1.CheckImageFileExtension(System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName).ToLower()) && Upload_Image(ref p_Var.ImageName))
                    {

                        obj_LinkOB2.ImageName = p_Var.ImageName;
                    }

                }
                else if (LblImageName.Visible == false && FileUploadImage.PostedFile.InputStream.Length == 0)
                {
                    obj_LinkOB2.ImageName = null;
                }
                else if (LblImageName.Visible == true && FileUploadImage.PostedFile.InputStream.Length == 0)
                {
                    obj_LinkOB2.ImageName = LblImageName.Text;
                }

                else
                {
                    obj_LinkOB2.ImageName = FileUploadImage.FileName.ToString();
                }
            }
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                {
                    if (FileUpload2.PostedFile.ContentLength != 0 && FileUpload2.PostedFile.ContentLength != null)
                    {
                        if (Upload_File(ref p_Var.Filename))
                        {
                            if (FileUpload2.PostedFile.InputStream.Length != 0 && lblFileName.Visible == false)
                            {
                                //obj_LinkOB2.FileName = FileUpload2.FileName.ToString();
                                if (p_Var.Filename != null && p_Var.Filename != "")
                                {
                                    obj_LinkOB2.FileName = p_Var.Filename.ToString();
                                }
                                else
                                {
                                    obj_LinkOB2.FileName = FileUpload2.FileName.ToString();
                                }
                            }
                            else if (lblFileName.Visible == true && FileUpload2.PostedFile.InputStream.Length == 0)
                            {
                                obj_LinkOB2.FileName = lblFileName.Text;
                            }
                            else if (lblFileName.Visible == false && FileUpload2.PostedFile.InputStream.Length == 0)
                            {
                                obj_LinkOB2.FileName = null;
                            }
                            else
                            {
                                obj_LinkOB2.FileName = FileUpload2.FileName.ToString();
                            }
                        }
                        else
                        {
                            obj_LinkOB2.FileName = lblFileName.Text;
                        }
                    }
                    else if (lblFileName.Visible == false && FileUpload2.PostedFile.InputStream.Length == 0)
                    {
                        if (lblFileName.Text != null && lblFileName.Text != "")
                        {
                            obj_LinkOB2.FileName = lblFileName.Text;
                        }
                        else
                        {
                            obj_LinkOB2.FileName = null;
                        }
                    }
                    else if (lblFileName.Visible == true && FileUpload2.PostedFile.InputStream.Length == 0)
                    {
                        obj_LinkOB2.FileName = lblFileName.Text;
                    }

                    else
                    {
                        obj_LinkOB2.FileName = FileUpload2.FileName.ToString();
                    }

                }
            }
            obj_LinkOB2.LangId = Convert.ToInt32(Session["Lanuage"]);
            if (ViewState["DeptID"] != DBNull.Value)
            {
                obj_LinkOB2.DepttId = Convert.ToInt32(ViewState["DeptID"]);
            }
            else
            {
                obj_LinkOB2.DepttId = null;
            }
            obj_LinkOB2.UserID = Convert.ToInt32(Session["User_Id"]);
            p_Var.Result = obj_LinkBL.ASP_Links_Update(obj_LinkOB2);
            if (p_Var.Result > 0)
            {
                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                {
                    //This is for multiple files
                    if (FileUpload2.PostedFile != null && FileUpload2.PostedFile.InputStream.Length != 0)
                    {
                        obj_LinkOB.TempLinkId = p_Var.Result;
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
                        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        p_Var.ext = System.IO.Path.GetExtension(this.FileUpload2.PostedFile.FileName);
                        p_Var.ext = p_Var.ext.ToUpper();
                        if (p_Var.ext.Equals(".PDF"))
                        {
                            p_Var.Path = p_Var.url;
                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
                            p_Var.Filename = miscellBL.getUniqueFileName(FileUpload2.FileName, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                            // p_Var.Filename = FileUpload2.FileName;
                            FileUpload2.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));
                            obj_LinkOB.FileName = p_Var.Filename;
                        }

                        obj_LinkBL.InsertFiles(obj_LinkOB);
                    }
                }


                if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                {
                    string fileMultiple = string.Empty;
                    HttpFileCollection hfc = Request.Files;

                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];

                        fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                        if (fileMultiple != null && fileMultiple != "")
                        {
                            LinkOB newObj = new LinkOB();
                            newObj.linkID = p_Var.Result;
                            newObj.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);

                            if (i == 0)
                            {
                                newObj.Remarks = TextBox1.Text;
                            }
                            else
                            {
                                int j = i - 1;


                                string strId = "txt" + j.ToString();
                                newObj.Remarks = Request.Form[strId].ToString();
                            }
                            p_Var.ext = System.IO.Path.GetExtension(fileMultiple);
                            p_Var.ext = p_Var.ext.ToUpper();
                            if (p_Var.ext.Equals(".PDF"))
                            {
                                lblmsg.Visible = false;
                                p_Var.Filename = hpf.FileName;
                                p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                p_Var.ext = Path.GetExtension(hpf.FileName);
                                p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                //newObj.PetitionFile = "P_" + p_Var.Filename;
                                newObj.FileName = p_Var.Filename;
                            }

                            newObj.StartDate = System.DateTime.Now;
                            p_Var.ext = p_Var.ext.ToUpper();
                            if (p_Var.ext.Equals(".PDF"))
                            {
                                int Result2 = obj_LinkBL.insertConnectedDiscussionFiles(newObj);
                            }
                        }
                    }
                }
                /*Audit Trail Entry*/
                //_moduleAuditTrail.UserId = Convert.ToInt32(Session["User_Id"]);
                //_moduleAuditTrail.IpAddress = Miscelleneous_DL.getclientIP();
                //_moduleAuditTrail.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                //_moduleAuditTrail.Title = HttpUtility.HtmlEncode(txtTitle_Reg.Text);
                //_moduleAuditTrail.ActionType = "U";
                //_moduleauditTraildl.InsertModuleAuditTrailDetails(_moduleAuditTrail);

                /*end*/


                obj_audit.ActionType = "U";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                string st = Request.QueryString["Status"].Trim();
                if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                if (obj_audit.ModuleID == 13 | obj_audit.ModuleID == 12 | obj_audit.ModuleID == 6)
                {

                    obj_audit.Title = txtstartdate.Text + ", " + txtname.Text;

                }
                else
                {
                    obj_audit.Title = txtname.Text;

                }

                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);


                Session["msg"] = "Record has been updated successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
            else
            {
                Session["msg"] = "Record has not been updated successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to upload files

    private bool Upload_File(ref string filename)
    {

        Miscelleneous_BL miscellFileBL = new Miscelleneous_BL();
        try
        {
            if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
            {
                p_Var.Filename = FileUpload2.FileName;
                p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
                p_Var.ext = Path.GetExtension(FileUpload2.FileName);
                //For Unique File Name
                filename = miscellFileBL.getUniqueFileName(p_Var.Filename, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

                FileUpload2.PostedFile.SaveAs(Server.MapPath(p_Var.url) + filename);
            }
        }
        catch
        {
            p_Var.uploadStatus = false;
        }
        return p_Var.uploadStatus;
    }

    #endregion

    #region Function to upload images

    private bool Upload_Image(ref string imagename)
    {

        bool uploadStatus = true;
        try
        {

            p_Var.ImageName = FileUploadImage.FileName;
            p_Var.Imagenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUploadImage.FileName);
            p_Var.ext = Path.GetExtension(FileUploadImage.FileName);
            //For Unique File Name
            imagename = miscellBL.getUniqueFileName(p_Var.ImageName, Server.MapPath(p_Var.imageUrl), p_Var.Imagenamewithout_Ext, p_Var.ext);
            FileUploadImage.PostedFile.SaveAs(Server.MapPath(p_Var.imageUrl) + imagename);

        }
        catch
        {
            uploadStatus = false;
        }
        return uploadStatus;
    }

    #endregion

    #region Custom validator to validate extension of upload pdf files

    protected void CustvalidFileUplaod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string UploadFileName = fileUploadPdf.PostedFile.FileName;

        if (UploadFileName == "")
        {
            // There is no file selected
            args.IsValid = false;
        }
        else
        {
            string Extension = UploadFileName.Substring(UploadFileName.LastIndexOf('.') + 1).ToLower();
            int count = fileUploadPdf.PostedFile.FileName.Split('.').Length - 1;
            if (Extension == "pdf" && count == 1)
            {
                args.IsValid = true; // Valid file type
            }
            else
            {
                args.IsValid = false; // Not valid file type
                return;
            }
        }
        string fileMultiple = string.Empty;
        HttpFileCollection hfc = Request.Files;
        string str = string.Empty;
        bool strem = true;
        Regex regex = new Regex(@"^[a-z | 0-9 | /,|,:;()#&]*$", RegexOptions.IgnoreCase);
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];

            fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
            strem = miscellBL.GetActualFileType_pdf(hfc[i].InputStream);
            if (strem == true)
            {

                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                break;
            }


            if (i == 0)
            {
                Match match = regex.Match(TextBox1.Text);
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    CustomValidator1.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";

                }
            }
            else
            {
                int j = i - 1;
                str = "txt" + j.ToString();
                Match match = regex.Match(Request.Form[str].ToString());
                if (match.Success == true)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;
                    CustomValidator1.Text = "Please enter valid file description. No special characters are allowed except (space,'-_.:;#()/&)";
                    TextBox1.Text = "";
                }
            }
        }

    }

    #endregion

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

    #region Custom validator to validate extension of upload pdf files

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        p_Var.ext = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        int count = FileUpload2.PostedFile.FileName.Split('.').Length - 1;
        if (p_Var.ext == ".pdf" && count == 1)
        {
            p_Var.flag = miscellBL.GetActualFileType_pdf(FileUpload2.PostedFile.InputStream);
        }
        else
        {
            args.IsValid = false;
            //p_Var.flag = miscellBL.GetActualFileType(FileUpload2.PostedFile.InputStream);
        }

        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    #endregion

    #region Custom validator to validate extension of upload Images

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Get file name
        p_Var.ext = System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName);
        p_Var.ext = p_Var.ext.ToLower();
        int count = FileUploadImage.PostedFile.FileName.Split('.').Length - 1;
        if (p_Var.ext == ".jpg" || p_Var.ext == ".png" || p_Var.ext == ".jpeg" || p_Var.ext == ".gif")
        {
            p_Var.flag = miscellBL.GetActualFileType(FileUploadImage.PostedFile.InputStream);
        }
        if (p_Var.flag == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    #endregion

    #region Function bind department name in dropDownlist

    public void Get_Deptt_Name()
    {
        try
        {
            obj_userOB.DepttId = Convert.ToInt32(Session["Dept_ID"]);
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


    //Area for all button, link button and image button click events

    #region Button btnSubmit click event to add new records

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {

                if (Page.IsValid)
                {
                    try
                    {
                        obj_LinkOB.ActionType = 1;
                        obj_LinkOB.LangId = Convert.ToInt32(ddlLanguage.SelectedValue);
                        obj_LinkOB.MateDescription = txtMetaDescription.Text;
                        obj_LinkOB.MetaKeywords = txtMetaKeyword.Text;
                        obj_LinkOB.MetaTitle = txtMetaTitle.Text;
                        obj_LinkOB.MetaLanguage = ddlMetaLang.SelectedValue;

                        if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Banner))
                        {
                            obj_LinkOB.Details_Regional = HttpUtility.HtmlEncode(txtDesc_Reg.Text);
                            obj_LinkOB.details = HttpUtility.HtmlEncode(txtDesc.Text);
                            obj_LinkOB.AltTag = HttpUtility.HtmlEncode(txtAltTag.Text);
                            obj_LinkOB.AltTagReg = HttpUtility.HtmlEncode(txtAltTag_Reg.Text);
                            obj_LinkOB.Name_Regional = HttpUtility.HtmlEncode(txtTitle_Reg.Text);
                            obj_LinkOB.NAME = HttpUtility.HtmlEncode(txtname.Text);
                        }
                        else
                        {
                            obj_LinkOB.Smalldetails = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtSmalldesc.Text, 50));
                            obj_LinkOB.details = miscellBL.RemoveUnnecessaryHtmlTagHtml(FCKeditor1.Value);
                            obj_LinkOB.NAME = HttpUtility.HtmlEncode(miscellBL.FixGivenCharacters(txtname.Text, 2000));

                        }
                        // 10 apr 2013
                        if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper))
                        {
                            PTime.Visible = true;
                            obj_LinkOB.Time = ddlhours.SelectedValue + ":" + ddlmins.SelectedValue + ":" + ddlampm.SelectedItem.ToString();
                            if (txtVenue.Text != null && txtVenue.Text != "")
                            {
                                obj_LinkOB.venu = txtVenue.Text;
                            }
                            else
                            {
                                obj_LinkOB.venu = null;

                            }
                            if (txtLastDate.Text != null && txtLastDate.Text != "")
                            {
                                obj_LinkOB.LastDateReceiving = Convert.ToDateTime(miscellBL.getDateFormat(txtLastDate.Text));
                            }

                            if (TxtPublicHearing.Text != null && TxtPublicHearing.Text != "")
                            {
                                obj_LinkOB.PublicHearingDate = Convert.ToDateTime(miscellBL.getDateFormat(TxtPublicHearing.Text));
                            }
                            else
                            {
                                obj_LinkOB.PublicHearingDate = null;
                            }
                        }
                        else
                        {
                            PTime.Visible = false;
                            obj_LinkOB.venu = null;
                            obj_LinkOB.PublicHearingDate = null;

                        }
                        if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                        {
                            if (txtLastDate.Text != null && txtLastDate.Text != "")
                            {
                                obj_LinkOB.LastDateReceiving = Convert.ToDateTime(miscellBL.getDateFormat(txtLastDate.Text));
                            }

                        }
                        obj_LinkOB.DepttId = Convert.ToInt32(ddlDepartment.SelectedValue);

                        if (txtstartdate.Text != null && txtstartdate.Text != "")
                        {
                            obj_LinkOB.StartDate = Convert.ToDateTime(miscellBL.getDateFormat(txtstartdate.Text));
                        }
                        else
                        {
                            obj_LinkOB.StartDate = null;
                        }
                        if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                        {
                            if (txtendate.Text != null && txtendate.Text != "")
                            {
                                obj_LinkOB.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtendate.Text));
                            }
                            else
                            {
                                obj_LinkOB.EndDate = null;
                            }
                        }
                        else
                        {

                            if (txtenddate.Text != null && txtenddate.Text != "")
                            {
                                obj_LinkOB.EndDate = Convert.ToDateTime(miscellBL.getDateFormat(txtenddate.Text));
                            }
                            else
                            {
                                obj_LinkOB.EndDate = null;
                            }
                        }
                        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);   //should be change according to the module
                        obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;  //draft
                        obj_LinkOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                        if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper)
                            && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification) && Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report))
                        {
                            if (FileUploadImage.PostedFile.ContentLength != 0 && FileUploadImage.PostedFile.ContentLength != null)
                            {
                                if (miscellBL.CheckImageFileExtension(System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName).ToLower()) && Upload_Image(ref p_Var.ImageName))
                                {

                                    obj_LinkOB.ImageName = FileUploadImage.FileName.ToString();

                                }
                            }
                            else
                            {
                                obj_LinkOB.ImageName = null;
                            }
                        }

                        if (Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || Convert.ToInt32(Request.QueryString["ModuleID"]) != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                        {
                            if (Upload_File(ref p_Var.Filename))
                            {
                                if (p_Var.Filename != null)
                                {
                                    obj_LinkOB.FileName = FileUpload2.FileName.ToString();
                                }
                                else
                                {
                                    obj_LinkOB.FileName = null;
                                }

                            }
                            else
                            {
                                obj_LinkOB.FileName = null;
                            }

                        }
                        obj_LinkOB.InsertedBy = Convert.ToInt32(Session["User_Id"]);
                        obj_LinkOB.UserID = Convert.ToInt32(Session["User_Id"]);
                        obj_LinkOB.IpAddress = Miscelleneous_DL.getclientIP();
                        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        p_Var.Result = obj_LinkBL.ASP_Links_Insert(obj_LinkOB);

                        /*Audit Trail Entry*/
                        //_moduleAuditTrail.UserId= Convert.ToInt32(Session["User_Id"]);
                        //_moduleAuditTrail.IpAddress = Miscelleneous_DL.getclientIP();
                        //_moduleAuditTrail.ModuleID= Convert.ToInt32(Request.QueryString["ModuleID"]);
                        //_moduleAuditTrail.Title= HttpUtility.HtmlEncode(txtTitle_Reg.Text);
                        //_moduleAuditTrail.ActionType = "I";
                        //_moduleauditTraildl.InsertModuleAuditTrailDetails(_moduleAuditTrail);

                        /*end*/
                        if (p_Var.Result > 0)
                        {
                            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Annual_Report) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Notification))
                            {
                                //This is for multiple files
                                if (FileUpload2.PostedFile != null && FileUpload2.PostedFile.InputStream.Length != 0)
                                {
                                    obj_LinkOB.TempLinkId = p_Var.Result;
                                    obj_LinkOB.StatusId = (int)Module_ID_Enum.Module_Permission_ID.Approved;
                                    obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                    p_Var.ext = System.IO.Path.GetExtension(this.FileUpload2.PostedFile.FileName);
                                    p_Var.ext = p_Var.ext.ToUpper();
                                    if (p_Var.ext.Equals(".PDF"))
                                    {
                                        p_Var.Path = p_Var.url;
                                        p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
                                        p_Var.Filename = miscellBL.getUniqueFileName(FileUpload2.FileName, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);

                                        //p_Var.Filename = FileUpload2.FileName;
                                        FileUpload2.SaveAs(Server.MapPath(p_Var.Path + p_Var.Filename));
                                        obj_LinkOB.FileName = p_Var.Filename;
                                    }

                                    obj_LinkBL.InsertFiles(obj_LinkOB);
                                }
                            }

                            if (Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Discussion_Paper) || Convert.ToInt32(Request.QueryString["ModuleID"]) == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Vacancy))
                            {
                                string fileMultiple = string.Empty;
                                HttpFileCollection hfc = Request.Files;

                                for (int i = 0; i < hfc.Count; i++)
                                {
                                    HttpPostedFile hpf = hfc[i];

                                    fileMultiple = System.IO.Path.GetFileName(hpf.FileName);
                                    if (fileMultiple != null && fileMultiple != "")
                                    {
                                        LinkOB newObj = new LinkOB();
                                        newObj.linkID = p_Var.Result;
                                        newObj.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                                        newObj.StatusId = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);

                                        if (i == 0)
                                        {
                                            newObj.Remarks = TextBox1.Text;
                                        }
                                        else
                                        {
                                            int j = i - 1;


                                            string strId = "txt" + j.ToString();
                                            newObj.Remarks = Request.Form[strId].ToString();
                                        }
                                        p_Var.ext = System.IO.Path.GetExtension(fileMultiple);
                                        p_Var.ext = p_Var.ext.ToUpper();
                                        if (p_Var.ext.Equals(".PDF"))
                                        {
                                            lblmsg.Visible = false;
                                            p_Var.Filename = hpf.FileName;
                                            p_Var.filenamewithout_Ext = Path.GetFileNameWithoutExtension(hpf.FileName);
                                            p_Var.ext = Path.GetExtension(hpf.FileName);
                                            p_Var.Filename = miscellBL.getUniqueFileName(fileMultiple, Server.MapPath(p_Var.url), p_Var.filenamewithout_Ext, p_Var.ext);
                                            hpf.SaveAs(Server.MapPath(p_Var.url + p_Var.Filename));
                                            //newObj.PetitionFile = "P_" + p_Var.Filename;
                                            newObj.FileName = p_Var.Filename;
                                        }

                                        newObj.StartDate = System.DateTime.Now;
                                        p_Var.ext = p_Var.ext.ToUpper();
                                        if (p_Var.ext.Equals(".PDF"))
                                        {
                                            int Result2 = obj_LinkBL.insertConnectedDiscussionFiles(newObj);
                                        }
                                    }
                                }
                            }

                            obj_audit.ActionType = "I";
                            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                            obj_audit.UserName = Session["UserName"].ToString();
                            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            obj_audit.IpAddress = miscellBL.IpAddress();
                            obj_audit.status = "New Record";
                            if (obj_audit.ModuleID == 13 | obj_audit.ModuleID == 12 | obj_audit.ModuleID == 6)
                            {

                                obj_audit.Title = txtstartdate.Text + ", " + txtname.Text;

                            }
                            else
                            {
                                obj_audit.Title = txtname.Text;

                            }
                            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                            Session["msg"] = "Record has been submitted successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                        }
                        else
                        {
                            Session["msg"] = "Record has not been submitted successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/Module/Module_Display.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        else
        {
            Session.Abandon();
            Response.Redirect("~/Auth/AdminPanel/Login.aspx");
        }

    }

    #endregion

    #region Button btnUpdate click event to update records

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {

                try
                {
                    if (Page.IsValid)
                    {
                        Update_Data();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }

    #endregion

    #region Button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/Module/") + "Module_Display.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));

    }

    #endregion

    #region Button btnReset to reset all fields

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Temp_Link_Id"] != null)
        {
            Display(Request.QueryString["Temp_Link_Id"].ToString());

        }
        else
        {
            txtAltTag.Text = "";
            txtAltTag_Reg.Text = "";
            TxtCirculation.Text = "";
            txtDesc.Text = "";
            txtDesc_Reg.Text = "";
            txtenddate.Text = "";
            TxtPublicHearing.Text = "";
            txtSmalldesc.Text = "";
            txtTitle_Reg.Text = "";
            txtVenue.Text = "";
            TextBox1.Text = "";
            txtLastDate.Text = "";
            txtendate.Text = "";
            txtname.Text = "";
            txtstartdate.Text = "";
            FCKeditor1.Value = "";

        }

    }

    #endregion

    #region Link button lnkImageRemove click event to remove image

    protected void lnkImageRemove_Click(object sender, EventArgs e)
    {
        LblImageName.Visible = false;
        LblOldImage.Visible = false;
        lnkImageRemove.Visible = false;
    }

    #endregion

    #region Link button lnkFileRemove click event to remove file

    protected void lnkFileRemove_Click(object sender, EventArgs e)
    {
        lblFileName.Visible = false;
        LblOldFile.Visible = false;
        lnkFileRemove.Visible = false;
    }

    #endregion

    #region function  bind with Repeator
    public void BindData(int linkId)
    {
        obj_LinkOB.linkID = linkId;
        //obj_LinkOB.StatusId = Convert.ToInt32(Session["Status_Id"]);
        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_Var.dsFileName = obj_LinkBL.getFileNameForDiscussion(obj_LinkOB);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            PFileName.Visible = true;
            datalistFileName.DataSource = p_Var.dsFileName;
            datalistFileName.DataBind();
        }
        else
        {
            PFileName.Visible = false;
        }
    }


    #endregion


    protected void datalistFileName_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("File"))
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            obj_LinkOB.linkID = id;
            p_Var.Result1 = obj_LinkBL.UpdateFileStatusForDiscussion(obj_LinkOB);
            if (p_Var.Result1 > 0)
            {
                Label filename = (Label)e.Item.FindControl("lblFile");
                Label date = (Label)e.Item.FindControl("lblDate");

                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");

                filename.Visible = false;
                date.Visible = false;

                RemoveFileLink.Visible = false;
            }
            Display(Request.QueryString["Temp_Link_Id"].ToString());


        }
    }

    protected void datalistFileName_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

            Literal ltrlDownload = (Literal)e.Item.FindControl("ltrlDownload");
            Label filename = (Label)e.Item.FindControl("lblFile");

            var link = e.Item.FindControl("lnkFileConnectedRemove") as LinkButton;
            var hiddenField = e.Item.FindControl("hiddenFieldLinkId") as HiddenField;


            if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
            {
                if (link != null)
                {
                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = true;//if user has right then enabled else disabled
                    }
                    else
                    {
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["Temp_Link_Id"]))
                        {
                            link.Visible = true;
                        }
                        else
                        {
                            link.Visible = false;

                        }
                    }
                }
            }
            else
            {
                if (link != null)
                {

                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        link.Visible = false;//if user has right then enabled else disabled
                    }
                    else
                    {
                        // link.Visible = true;//if user has right then enabled else disabled
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["Temp_Link_Id"]))
                        {
                            link.Visible = true;
                        }
                        else
                        {
                            link.Visible = false;

                        }
                    }
                }
            }

            LinkButton lnkFileConnectedRemove = (LinkButton)e.Item.FindControl("lnkFileConnectedRemove");
            lnkFileConnectedRemove.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + filename.Text + " ?')";

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuilder.Append("<hr />");
            ltrlDownload.Text = p_Var.sbuilder.ToString();
        }
    }


    #region Function to bind hours in all dropdownlist of hours

    public void bindHoursinDropdownlist()
    {
        p_Var.dSet = petPetitionBL.getHours();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlhours.DataSource = p_Var.dSet;

            ddlhours.DataTextField = "hours";
            ddlhours.DataValueField = "hours";
            ddlhours.DataBind();
            ddlhours.SelectedValue = "11";


        }
    }

    #endregion

    #region Funtion to bind time in all dropdownlist of times

    public void bindTimeinDropdownlist()
    {
        p_Var.dSet = petPetitionBL.getTime();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {

            ddlmins.DataSource = p_Var.dSet;
            //ddlsecs.DataSource = p_Var.dSet;
            ddlmins.DataTextField = "mins";
            ddlmins.DataValueField = "mins";
            ddlmins.DataBind();
            ddlmins.SelectedValue = "30";
            //ddlsecs.DataTextField = "secs";
            //ddlsecs.DataValueField = "secs";
            //ddlsecs.DataBind();

        }
    }

    #endregion


    #region Function to bind ampm in all dropdownlist of ampm

    public void bindampminDropdownlist()
    {
        p_Var.dSet = petPetitionBL.getampm();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlampm.DataSource = p_Var.dSet;

            ddlampm.DataTextField = "am_pm";
            ddlampm.DataValueField = "am_pm";
            ddlampm.DataBind();


        }
    }

    #endregion
    //End 



    #region function  bind with Repeator
    public void BindDataFiles(int LinkId)
    {
        lblFileName.Visible = false;
        LblOldFile.Visible = false;
        lnkFileRemove.Visible = false;
        obj_LinkOB.TempLinkId = LinkId;
        obj_LinkOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_Var.dsFileName = obj_LinkBL.getFileNameForModules(obj_LinkOB);
        if (p_Var.dsFileName.Tables[0].Rows.Count > 0)
        {
            PFilesAnnualNotification.Visible = true;
            dtlistFiles.DataSource = p_Var.dsFileName;
            dtlistFiles.DataBind();
        }
        else
        {
            PFilesAnnualNotification.Visible = false;
        }
    }


    #endregion



    protected void dtlistFiles_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("FileModules"))
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            obj_LinkOB.linkID = id;
            p_Var.Result1 = obj_LinkBL.UpdateFileStatusdelete(obj_LinkOB);
            if (p_Var.Result1 > 0)
            {
                Label filename = (Label)e.Item.FindControl("lblFiles");

                LinkButton RemoveFileLink = (LinkButton)e.Item.FindControl("lnkFileConnectedRemoves");

                filename.Visible = false;
                lblFileName.Text = string.Empty;
                RemoveFileLink.Visible = false;
            }
            Display(Request.QueryString["Temp_Link_Id"].ToString());


        }
    }



    protected void dtlistFiles_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);

            Literal ltrlDownloadModule = (Literal)e.Item.FindControl("ltrlDownloadModule");
            Label filename = (Label)e.Item.FindControl("lblFiles");

            var linkModule = e.Item.FindControl("lnkFileConnectedRemoves") as LinkButton;
            var hiddenField = e.Item.FindControl("hydLinkId") as HiddenField;


            if (Convert.ToInt32(Session["Role_Id"]) == Convert.ToInt32(Module_ID_Enum.hercType.superadmin))
            {
                if (linkModule != null)
                {
                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        linkModule.Visible = true;//if user has right then enabled else disabled
                    }
                    else
                    {
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["Temp_Link_Id"]))
                        {
                            linkModule.Visible = true;
                        }
                        else
                        {
                            linkModule.Visible = false;

                        }
                    }
                }
            }
            else
            {
                if (linkModule != null)
                {

                    if (Convert.ToInt32(Session["Status_Id"]) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                    {
                        linkModule.Visible = false;//if user has right then enabled else disabled
                    }
                    else
                    {
                        // link.Visible = true;//if user has right then enabled else disabled
                        if (Convert.ToInt32(hiddenField.Value) == Convert.ToInt32(Request.QueryString["Temp_Link_Id"]))
                        {
                            linkModule.Visible = true;
                        }
                        else
                        {
                            linkModule.Visible = false;

                        }
                    }
                }
            }

            LinkButton lnkFileConnectedRemoves = (LinkButton)e.Item.FindControl("lnkFileConnectedRemoves");
            lnkFileConnectedRemoves.OnClientClick = "javascript:return confirm('Are you sure you want to delete this file :- " + filename.Text + " ?')";

            p_Var.sbuilder.Append("<a href='" + ResolveUrl(p_Var.url) + filename.Text + "'  target='_blank'>" + "<img src='" + ResolveUrl("~/images/pdf-icon.jpg") + "' title='Download File' width='15' alt='Download File' height=\"15\" /> " + "</a>");
            p_Var.sbuilder.Append("<hr />");
            ltrlDownloadModule.Text = p_Var.sbuilder.ToString();
        }
    }

    #region function to bind metalanguage

    public void displayMetaLang()
    {
        p_Var.dSet = miscellBL.getMetaLanguage();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlMetaLang.DataSource = p_Var.dSet;
            ddlMetaLang.DataTextField = "languagename";
            ddlMetaLang.DataValueField = "LanguageKey";
            ddlMetaLang.DataBind();
        }
    }

    #endregion
}
