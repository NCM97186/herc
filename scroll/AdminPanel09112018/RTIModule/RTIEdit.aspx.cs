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

public partial class Auth_AdminPanel_RTI_RTIEdit : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

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
        lblModulename.Text = ": Edit  RTI";
        this.Page.Title = " Edit RTI: HERC";

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

            if (Request.QueryString["rti_id"] != null)    // for Edit
            {
                bindRtiStatus();
                bindRtiInEditMode(Convert.ToInt32(Request.QueryString["rti_id"]));
            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons, linkButtons, imageButtons click event

    #region button btnRTIUpdate click event to update rti

    protected void btnRTIUpdate_Click(object sender, EventArgs e)
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
                    rtiObject.TempRTIId = Convert.ToInt32(Request.QueryString["rti_id"]);
                    rtiObject.OldRTIId = Convert.ToInt32(Request.QueryString["rti_id"]);
                    rtiObject.RefNo = HttpUtility.HtmlEncode(txtReferenceNo_Edit.Text);
                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate_Edit.Text.ToString()));
                    rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
                    rtiObject.ApplicationDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate_Edit.Text.ToString()));
                    rtiObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName_Edit.Text.Replace(Environment.NewLine, "<br />"));
                    //petitionObject.PetitionerContactNo =Convert.ToDouble(HttpUtility.HtmlEncode(txtPetitionerContactNumber.Text));

                    rtiObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAddr_Edit.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo_Edit.Text);
                    rtiObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo_Edit.Text);
                    rtiObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo_Edit.Text);
                    rtiObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID_Edit.Text);

                    rtiObject.MetaDescription = txtMetaDescription.Text;
                    rtiObject.MetaKeyWords = txtMetaKeyword.Text;
                    rtiObject.MetaTitle = txtMetaTitle.Text;
                    rtiObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    rtiObject.subject = HttpUtility.HtmlEncode(txtRtiSubject_Edit.Text.Replace(Environment.NewLine, "<br />"));
                    rtiObject.RTIStatusId = Convert.ToInt32(ddlRTIStatus_Edit.SelectedValue);
                    rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);

                    rtiObject.LastUpdatedBy = Convert.ToInt32(Session["User_Id"]);
                    rtiObject.IpAddress = Miscelleneous_DL.getclientIP();
                    rtiObject.appeal = 0;
                    rtiObject.PlaceHolderFive = HttpUtility.HtmlEncode(txtremarks_Edit.Text); //Remarks in Rti
                    rtiObject.LangId = Convert.ToInt32(Session["Lanuage"]);
                    if (ViewState["DeptID"] != DBNull.Value)
                    {
                        rtiObject.DepttId = Convert.ToInt32(ViewState["DeptID"]);
                    }
                    else
                    {
                        rtiObject.DepttId = null;
                    }
                    if (!String.IsNullOrEmpty(txtApplicantName_Edit.Text) && !String.IsNullOrEmpty(txtApplicantAddr_Edit.Text) && !String.IsNullOrEmpty(txtRtiSubject_Edit.Text))
                    {
                        p_Var.Result = rtiBL.insertUpdateTempRTI(rtiObject);
                        if (p_Var.Result > 0)
                        {


                            obj_audit.ActionType = "U";
                            obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                            obj_audit.UserName = Session["UserName"].ToString();
                            obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                            obj_audit.IpAddress = miscellBL.IpAddress();
                            obj_audit.Title = txtMetaTitle.Text;
                            obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                            Session["msg"] = "RTI Application has been updated successfully.";
                            Session["Redirect"] = "~/Auth/AdminPanel/RTIModule/DisplayRTI.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                            Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtApplicantName_Edit.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter applicant name');", true);
                        }
                        else if (String.IsNullOrEmpty(txtApplicantAddr_Edit.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter applicant address');", true);
                        }

                        else if (String.IsNullOrEmpty(txtRtiSubject_Edit.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Please enter subject');", true);
                        }
                    }
                }
            }
        }
		else{
			Session.Abandon();
		    Response.Redirect("~/Auth/AdminPanel/Login.aspx");
			}
    }

    #endregion

    #region button btnReset_Edit click event to reset previous content

    protected void btnReset_Edit_Click(object sender, EventArgs e)
    {
        bindRtiInEditMode(Convert.ToInt32(Request.QueryString["rti_id"]));
    }

    #endregion

    #region button btnBack_Edit click event to go back

    protected void btnBack_Edit_Click(object sender, EventArgs e)
    {
		 Response.Redirect(ResolveUrl("~/auth/adminpanel/RTIModule/") + "DisplayRTI.aspx?ModuleID=" + Convert.ToInt32(Request.QueryString["ModuleID"]));
        // hold the previous page reference
        //object refUrl = ViewState["RefUrl"];
        //if (refUrl != null)
        //{
           // Response.Redirect((string)refUrl);
        //}
    }

    #endregion

    //End

    //Area for all the user defined functions

    #region Function to bind rti in edit mode

    public void bindRtiInEditMode(int rtiID)
    {
        try
        {

            rtiObject.TempRTIId = rtiID;
            rtiObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = rtiBL.getRtiRecordForEdit(rtiObject);
            txtReferenceNo_Edit.Text =p_Var.dSet.Tables[0].Rows[0]["Ref_No"].ToString();

            txtApplicantName_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicantMobileNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
            txtApplicantPhoneNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
            txtApplicantFaxNo_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
            txtApplicantEmailID_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
            txtRtiSubject_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine)); 
            txtApplicantAddr_Edit.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
            txtApplicationDate_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["Application_Date"].ToString();
            txtremarks_Edit.Text = p_Var.dSet.Tables[0].Rows[0]["PlaceholderFive"].ToString(); //use for Remarks 
            ddlRTIStatus_Edit.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["RTI_Status_Id"].ToString();

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
        //p_Var.dSet = rtiBL.getRtiStatus();
        p_Var.dSet = rtiBL.getRtiStatusDuringEdit();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlRTIStatus_Edit.DataSource = p_Var.dSet;
            ddlRTIStatus_Edit.DataTextField = "STATUS";
            ddlRTIStatus_Edit.DataValueField = "STATUS_ID";
            ddlRTIStatus_Edit.DataBind();
        }
    }

    #endregion

    //End
    protected void cus_ReferenceNo_Edit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            rtiObject.RefNo = HttpUtility.HtmlEncode(txtReferenceNo_Edit.Text);
            
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate_Edit.Text.ToString()));
            rtiObject.year = HttpUtility.HtmlEncode(date.Year.ToString());
            rtiObject.RTIId = Convert.ToInt32(Request.QueryString["rti_id"]);
            rtiObject.DepttId = Convert.ToInt32(Request.QueryString["DepttID"]);
          
            p_Var.dSet = rtiBL.getReferenceNumberEdit(rtiObject);
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

    protected void ddlRTIStatus_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

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
