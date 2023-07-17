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

public partial class Auth_AdminPanel_RTI_Rti_FAA_Add : CrsfBase //System.Web.UI.Page
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
    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();

    #endregion

    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Add RTI FAA";
        this.Page.Title = " Add RTI FAA: HERC";

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

            if (Request.QueryString["rp_id"] != null)    // for Edit
            {

                lblReferenceNo.Visible = true;
                //bindPetition_Review_IN_Edit_Mode(Convert.ToInt32(Request.QueryString["rp_id"]));
            }
            else
            {

                lblReferenceNo.Visible = true;
                lblReferenceNo.Text += " HERC/RTI-" + Request.QueryString["Ref_Number"].ToString(); //16 jan 2013

                bind_Rti_Details(Convert.ToInt32(Request.QueryString["rti_id"]));
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click events


    #region button btnAddRtiFaa cilck event to add first applet authority

    protected void btnAddRtiFaa_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    rtiObject.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert);
                    rtiObject.RefNo = HttpUtility.HtmlEncode(lblReferenceNo.Text);
                    rtiObject.RTIId = Convert.ToInt32(Request.QueryString["rti_id"]);
                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());

                    rtiObject.MetaDescription = txtMetaDescription.Text;
                    rtiObject.MetaKeyWords = txtMetaKeyword.Text;
                    rtiObject.MetaTitle = txtMetaTitle.Text;
                    rtiObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    rtiObject.ApplicationDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    rtiObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAdd.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                    rtiObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                    rtiObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                    rtiObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);
                    rtiObject.Faa = HttpUtility.HtmlEncode(txtFAppletAuthority.Text);
                    rtiObject.subject = HttpUtility.HtmlEncode(txtRtiSubject.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.RTIFAAStatusId = Convert.ToInt32(Module_ID_Enum.rti_Status.inprocess);
                    rtiObject.StatusId = (int)Module_ID_Enum.Module_Permission_ID.draft;
                    rtiObject.InsertedBy = Convert.ToInt32(Session["User_Id"]);
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

                    rtiObject.appeal = 0;
                    rtiObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtremarks.Text); //Remarks in FAARti

                    if (!String.IsNullOrEmpty(txtApplicantName.Text) && !String.IsNullOrEmpty(txtApplicantAdd.Text) && !String.IsNullOrEmpty(txtRtiSubject.Text))
                    {
                        p_Var.Result = rtiBL.insertUpdateTempRTIFAA(rtiObject);
                        if (p_Var.Result > 0)
                        {
                            obj_audit.ActionType = "I";
                            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                            obj_audit.UserName = Session["UserName"].ToString();
                            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            obj_audit.IpAddress = miscellBL.IpAddress();
                            obj_audit.status = "New Record";
                            obj_audit.Title = "RTI-FAA/Ref No. " + txtFAppletAuthority.Text + " of " + date.Year.ToString();
                            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);
                            Session["msg"] = "RTI application for FAA has been added successfully";
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

    #region button btnReset click event to reset previous contents

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFAppletAuthority.Text = "";
        txtApplicantAdd.Text = "";
        txtApplicantEmailID.Text = "";
        txtApplicantFaxNo.Text = "";
        txtApplicantMobileNo.Text = "";
        txtApplicantName.Text = "";
        txtApplicantPhoneNo.Text = "";
        txtApplicationDate.Text = "";
        txtFAppletAuthority.Text = "";
        txtremarks.Text = "";
        txtRtiSubject.Text = "";
        bind_Rti_Details(Convert.ToInt32(Request.QueryString["rti_id"]));

    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
        // hold the previous page reference
        // object refUrl = ViewState["RefUrl"];
        //if (refUrl != null)
        //{
        // Response.Redirect((string)refUrl);
        //}

        Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTIFAA.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind rti details during filling rti-faa

    public void bind_Rti_Details(int rti_id)
    {
        try
        {
            rtiObject.TempRTIId = Convert.ToInt32(rti_id);
            rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = rtiBL.getRtiRecordForEdit(rtiObject);
            //txtYear.Text = p_Var.dSet.Tables[0].Rows[0]["year"].ToString();
            //txtYear.Text = p_Var.dSet.Tables[0].Rows[0]["year"].ToString();
            txtApplicantName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine)); ;
            lblReferenceNo.Text = lblReferenceNo.Text + " Of " + p_Var.dSet.Tables[0].Rows[0]["year"].ToString();
            txtApplicantMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
            txtApplicantPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
            txtApplicantFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
            txtApplicantEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
            txtRtiSubject.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantAdd.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            //txtApplicationDate.Text = p_Var.dSet.Tables[0].Rows[0]["Application_Date"].ToString(); // 16 jan 2013
            txtremarks.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString();
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
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //End


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
            rtiObject.DepttId = Convert.ToInt32(Request.QueryString["DepttID"]);
            p_Var.dSet = rtiBL.getReferenceNumberFAA(rtiObject);
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
