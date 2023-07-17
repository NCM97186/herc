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

public partial class Auth_AdminPanel_RTI_RTI_FAA_Edit : CrsfBase //System.Web.UI.Page
{
    //Area for all the data declaration zone

    #region Data declaration zone

    PetitionOB rtiObject = new PetitionOB();
    RtiBL rtiBL = new RtiBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_permissionBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    #endregion

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Edit RTI FAA";
        this.Page.Title = " Edit RTI FAA: HERC";

        if (!IsPostBack)
        {
            displayMetaLang();
            DataSet dsprv = new DataSet();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleId"]);
            dsprv = obj_permissionBL.ASP_CheckPrivilagesALLForPermission(obj_userOB);
            if (dsprv.Tables[0].Rows.Count == 0)
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

            if (Request.QueryString["rti_faa_Id"] != null)    // for Edit
            {
                bindRtiStatus();
                bindRtiFaaInEditMode(Convert.ToInt32(Request.QueryString["rti_faa_Id"]));
            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click events

    #region button btnUpdateRtiFaa click event to update rti faa

    protected void btnUpdateRtiFaa_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    rtiObject.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.update);
                    rtiObject.RefNo = HttpUtility.HtmlEncode(lblReferenceNo.Text);
                    rtiObject.TempRTIFAAId = Convert.ToInt32(Request.QueryString["rti_faa_id"]);
                    rtiObject.OldRTIFAAId = Convert.ToInt32(Request.QueryString["rti_faa_id"]);
                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));

                    rtiObject.MetaDescription = txtMetaDescription.Text;
                    rtiObject.MetaKeyWords = txtMetaKeyword.Text;
                    rtiObject.MetaTitle = txtMetaTitle.Text;
                    rtiObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                    rtiObject.ApplicationDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAdd.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                    rtiObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                    rtiObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                    rtiObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);
                    rtiObject.Faa = HttpUtility.HtmlEncode(txtFAppletAuthority.Text);
                    rtiObject.subject = HttpUtility.HtmlEncode(txtRtiSubject.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.RTIFAAStatusId = Convert.ToInt32(ddlRTIFAAStatus_Edit.SelectedValue);
                    rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    rtiObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                    if (ViewState["DeptID"] != DBNull.Value)
                    {
                        rtiObject.DepttId = Convert.ToInt32(ViewState["DeptID"]);
                    }
                    else
                    {
                        rtiObject.DepttId = null;
                    }
                    if (ViewState["Lang_Id"] != DBNull.Value)
                    {
                        rtiObject.LangId = Convert.ToInt32(ViewState["Lang_Id"]);
                    }
                    else
                    {
                        rtiObject.LangId = null;
                    }
                    if (ViewState["RTI_Id"] != DBNull.Value)
                    {
                        rtiObject.RTIId = Convert.ToInt32(ViewState["RTI_Id"]);
                    }
                    else
                    {
                        rtiObject.RTIId = null;
                    }

                    rtiObject.appeal = 0;
                    rtiObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtremarks_Edit.Text); //Remarks for RTIFAA


                    if (!String.IsNullOrEmpty(txtApplicantName.Text) && !String.IsNullOrEmpty(txtApplicantAdd.Text) && !String.IsNullOrEmpty(txtRtiSubject.Text))
                    {
                        p_Var.Result = rtiBL.insertUpdateTempRTIFAA(rtiObject);
                        if (p_Var.Result > 0)
                        {

                            obj_audit.ActionType = "U";
                            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                            obj_audit.UserName = Session["UserName"].ToString();
                            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            obj_audit.IpAddress = miscellBL.IpAddress();
                            string st = Request.QueryString["Status"].Trim();
                            if (st == "3") { obj_audit.status = "Draft"; } else if (st == "4") { obj_audit.status = "For Publish"; } else if (st == "6") { obj_audit.status = "Published"; } else if (st == "8") { obj_audit.status = "Deleted"; } else if (st == "21") { obj_audit.status = "For Review"; }
                            obj_audit.Title = "RTI-FAA/Ref No. " + txtFAppletAuthority.Text + " of " + date.Year.ToString();
                            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                            Session["msg"] = "RTI application for this FAA has been updated successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtApplicantName.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter applicant name');", true);
                        }
                        else if (String.IsNullOrEmpty(txtApplicantAdd.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter applicant address');", true);
                        }

                        else if (String.IsNullOrEmpty(txtRtiSubject.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter subject');", true);
                        }
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

    #region button btnReset click event to reset contents

    protected void btnReset_Click(object sender, EventArgs e)
    {
        bindRtiFaaInEditMode(Convert.ToInt32(Request.QueryString["rti_faa_id"]));
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
        // hold the previous page reference
        // object refUrl = ViewState["RefUrl"];
        //if (refUrl != null)
        //{
        //  Response.Redirect((string)refUrl);
        //}
    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind rti-faa in edit mode

    public void bindRtiFaaInEditMode(int rtifaaID)
    {
        try
        {

            rtiObject.TempRTIFAAId = rtifaaID;
            rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = rtiBL.getRtiFAARecordForEdit(rtiObject);

            if (Convert.ToInt32(Request.QueryString["DepttID"]) == Convert.ToInt32(Module_ID_Enum.hercType.herc))
            {
                lblReferenceNo.Text += " HERC/RTI-" + p_Var.dSet.Tables[0].Rows[0]["ref_no"].ToString() + " Of " + p_Var.dSet.Tables[0].Rows[0]["rtiYear"].ToString();
            }
            else
            {
                lblReferenceNo.Text += " EO/RTI-" + p_Var.dSet.Tables[0].Rows[0]["ref_no"].ToString() + " Of " + p_Var.dSet.Tables[0].Rows[0]["rtiYear"].ToString();
            }
            txtFAppletAuthority.Text = p_Var.dSet.Tables[0].Rows[0]["faa"].ToString();
            txtApplicantName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
            txtApplicantPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
            txtApplicantFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
            txtApplicantEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
            txtRtiSubject.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantAdd.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicationDate.Text = p_Var.dSet.Tables[0].Rows[0]["Application_Date"].ToString();
            txtremarks_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString();
            ddlRTIFAAStatus_Edit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["RTI_FAA_Status_Id"].ToString();

            txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
            txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
            txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
            ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

            if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
            {
                ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
            }
            else
            {
                ViewState["DeptID"] = DBNull.Value;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["Lang_Id"] != DBNull.Value)
            {
                ViewState["Lang_Id"] = p_Var.dSet.Tables[0].Rows[0]["Lang_Id"].ToString();
            }
            else
            {
                ViewState["Lang_Id"] = DBNull.Value;
            }
            if (p_Var.dSet.Tables[0].Rows[0]["RTI_Id"] != DBNull.Value)
            {
                ViewState["RTI_Id"] = p_Var.dSet.Tables[0].Rows[0]["RTI_Id"].ToString();
            }
            else
            {
                ViewState["RTI_Id"] = DBNull.Value;
            }


        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind status of rti

    public void bindRtiStatus()
    {
        p_Var.dSet = rtiBL.getRtiStatus();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlRTIFAAStatus_Edit.DataSource = p_Var.dSet;
            ddlRTIFAAStatus_Edit.DataTextField = "STATUS";
            ddlRTIFAAStatus_Edit.DataValueField = "STATUS_ID";
            ddlRTIFAAStatus_Edit.DataBind();
        }
    }

    #endregion

    protected void CusomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Convert.ToInt32(args.Value.ToString().Substring(6, 4)) > DateTime.Today.Year)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }



    #region customValidator cusReferenceNo serverValidate event to validate reference no. duplicacy

    protected void cusReferenceNo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            rtiObject.RefNo = HttpUtility.HtmlEncode(txtFAppletAuthority.Text);
            //rtiObject.year = txtYear.Text.Trim();
            //2 jan 2013
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
            rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
            rtiObject.LangId = Convert.ToInt32(ViewState["Lang_Id"]);
            rtiObject.RTIFAAId = Convert.ToInt32(Request.QueryString["rti_faa_Id"]);
            rtiObject.DepttId = Convert.ToInt32(Request.QueryString["DepttID"]);
            p_Var.dSet = rtiBL.getReferenceNumberFAAEdit(rtiObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End

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
