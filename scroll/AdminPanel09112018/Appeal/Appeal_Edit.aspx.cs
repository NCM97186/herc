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

public partial class Auth_AdminPanel_Appeal_Appeal_Edit : CrsfBase //System.Web.UI.Page
{
    //Area for all the variables declaration zone

    PetitionOB appealObject = new PetitionOB();
    AppealBL appealBL = new AppealBL();
    Project_Variables p_Var = new Project_Variables();
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    UserOB obj_userOB = new UserOB();
    LinkBL obj_LinkBL = new LinkBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();


    //End

    #region Page load event zone

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblModulename = Master.FindControl("lblModulename") as Label;
        lblModulename.Text = ": Edit Appeal";
        this.Page.Title = " Edit Appeal: HERC";

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

            bindAppealStatus();
            if (Request.QueryString["appeal_Id"] != null)    // for Edit
            {

                bindAppealInEditMode(Convert.ToInt32(Request.QueryString["appeal_Id"]));
            }

            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString(); // reference of previous page URL
            }
        }
    }

    #endregion

    //Area for all the buttons, imageButtons, linkButtons click events

    #region button btnUpdateAppeal click event to update appeal

    protected void btnUpdateAppeal_Click(object sender, EventArgs e)
    {
        string hdnblank = ((HiddenField)this.Master.FindControl("hdnblank")).Value;
        string CurrentSessionId = Convert.ToString(Session["AntiForgeryToken"]);

        if (Session["CurrentRequestUrl"] != null && Convert.ToString(Session["CurrentRequestUrl"]).Equals(Convert.ToString(Request.ServerVariables["HTTP_REFERER"])))
        {
            if (CurrentSessionId == hdnblank)
            {
                if (Page.IsValid)
                {
                    appealObject.ActionType = Convert.ToInt16(Module_ID_Enum.Project_Action_Type.update);
                    appealObject.TempAppealId = Convert.ToInt32(Request.QueryString["appeal_id"]);
                    appealObject.OldAppealId = Convert.ToInt32(Request.QueryString["appeal_id"]);
                    appealObject.AppealNo = HttpUtility.HtmlEncode(txtAppealNo.Text);
                    DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    appealObject.year = date.Year.ToString();
                    appealObject.AppealDate = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
                    appealObject.ApplicantName = HttpUtility.HtmlEncode(txtApplicantName.Text.Replace(Environment.NewLine, "<br />"));

                    appealObject.ApplicantAddress = HttpUtility.HtmlEncode(txtApplicantAdd.Text.Replace(Environment.NewLine, "<br />"));

                    appealObject.MetaDescription = txtMetaDescription.Text;
                    appealObject.MetaKeyWords = txtMetaKeyword.Text;
                    appealObject.MetaTitle = txtMetaTitle.Text;
                    appealObject.MetaKeyLanguage = ddlMetaLang.SelectedValue;

                    appealObject.ApplicantMobileNo = HttpUtility.HtmlEncode(txtApplicantMobileNo.Text);
                    appealObject.ApplicantPhoneNo = HttpUtility.HtmlEncode(txtApplicantPhoneNo.Text);
                    appealObject.ApplicantFaxNo = HttpUtility.HtmlEncode(txtApplicantFaxNo.Text);
                    appealObject.ApplicantEmail = HttpUtility.HtmlEncode(txtApplicantEmailID.Text);

                    appealObject.RespondentName = HttpUtility.HtmlEncode(txtRespondentName.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.RespondentMobileNo = HttpUtility.HtmlEncode(txtRespondentMobileNo.Text);
                    appealObject.RespondentPhone_No = HttpUtility.HtmlEncode(txtRespondentPhoneNo.Text);
                    appealObject.RespondentFaxNo = HttpUtility.HtmlEncode(txtRespondentFaxNo.Text);
                    appealObject.Respondentmail = HttpUtility.HtmlEncode(txtRespondentEmailID.Text);
                    appealObject.RespondentAddress = HttpUtility.HtmlEncode(txtRespondentAddress.Text.Replace(Environment.NewLine, "<br />"));

                    appealObject.subject = HttpUtility.HtmlEncode(txtAppealSubject.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.AppealStatusId = Convert.ToInt16(ddlAppealStatus.SelectedValue);
                    appealObject.Remarks = HttpUtility.HtmlEncode(txtremarks.Text.Replace(Environment.NewLine, "<br />"));
                    appealObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
                    //appealObject.LangId = Convert.ToInt16(ddlLanguage.SelectedValue);
                    appealObject.InsertedBy = Convert.ToInt16(Session["User_Id"]);
                    appealObject.LastUpdatedBy = Convert.ToInt16(Session["User_Id"]);
                    appealObject.userID = Convert.ToInt16(Session["User_Id"]);
                    appealObject.IpAddress = Miscelleneous_DL.getclientIP();
                    appealObject.LangId = Convert.ToInt32(Session["Lanuage"]);
                    if (ViewState["DeptID"] != DBNull.Value)
                    {
                        appealObject.DepttId = Convert.ToInt16(ViewState["DeptID"]);
                    }
                    else
                    {
                        appealObject.DepttId = null;
                    }
                    p_Var.Result = appealBL.insertUpdateTempAppeal(appealObject);
                    if (p_Var.Result > 0)
                    {
                        obj_audit.ActionType = "U";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.Title = txtAppealSubject.Text;
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "Appeal has been updated successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/Appeal/DisplayAppeal.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx", false);
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

    #region button btnReset click event  to reset contents

    protected void btnReset_Click(object sender, EventArgs e)
    {
        bindAppealInEditMode(Convert.ToInt32(Request.QueryString["appeal_id"]));
    }

    #endregion

    #region button btnBack click event to go back

    protected void btnBack_Click(object sender, EventArgs e)
    {
       Response.Redirect(ResolveUrl("~/Auth/AdminPanel/Appeal/") + "DisplayAppeal.aspx?ModuleID=" + Convert.ToInt16(Module_ID_Enum.Project_Module_ID.Appeal));
    }

    #endregion

    //End

    //Area for all the custom validators server events

    protected void cusAppealNo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            p_Var.dSet = null;
            appealObject.AppealNo = HttpUtility.HtmlEncode(txtAppealNo.Text);
            appealObject.TempAppealId = Convert.ToInt32(Request.QueryString["appeal_Id"]);
            DateTime date = Convert.ToDateTime(miscellBL.getDateFormat(txtApplicationDate.Text.ToString()));
            appealObject.year = date.Year.ToString();
            //appealObject.year = txtYear.Text.Trim();
            p_Var.dSet = appealBL.getAppealNumberInEditMode(appealObject);
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

    //End


    //Area for all the user defined functions

    #region Function to bind appeal in edit mode

    public void bindAppealInEditMode(int appealID)
    {
        try
        {

            appealObject.TempAppealId = appealID;
            appealObject.StatusId = Convert.ToInt32(Session["Status_Id"]);
            p_Var.dSet = appealBL.getAppealRecordForEdit(appealObject);
            if (p_Var.dSet.Tables[0].Rows.Count > 0)
            {
                txtAppealNo.Text = p_Var.dSet.Tables[0].Rows[0]["Appeal_Number"].ToString();

                txtMetaDescription.Text = p_Var.dSet.Tables[0].Rows[0]["MetaDescriptions"].ToString();
                txtMetaTitle.Text = p_Var.dSet.Tables[0].Rows[0]["MetaTitle"].ToString();
                txtMetaKeyword.Text = p_Var.dSet.Tables[0].Rows[0]["MetaKeywords"].ToString();
                ddlMetaLang.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["MetaLanguage"].ToString().Trim();

                txtApplicantName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtApplicantMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Mobile_No"].ToString();
                txtApplicantPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Phone_No"].ToString();
                txtApplicantFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Fax_No"].ToString();
                txtApplicantEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Applicant_Email"].ToString();
                txtApplicantAdd.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Applicant_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));

                txtRespondentName.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Name"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtRespondentMobileNo.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Mobile_No"].ToString();
                txtRespondentPhoneNo.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Phone_No"].ToString();
                txtRespondentFaxNo.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Fax_No"].ToString();
                txtRespondentEmailID.Text = p_Var.dSet.Tables[0].Rows[0]["Respondent_Email"].ToString();
                txtRespondentAddress.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Respondent_Address"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
               
                txtApplicationDate.Text = p_Var.dSet.Tables[0].Rows[0]["Appeal_Date"].ToString();
                txtAppealSubject.Text = HttpUtility.HtmlDecode(p_Var.dSet.Tables[0].Rows[0]["Subject"].ToString().Replace("&lt;br /&gt;", Environment.NewLine));
                txtremarks.Text = p_Var.dSet.Tables[0].Rows[0]["Remarks"].ToString().Replace("&lt;br /&gt;", Environment.NewLine);
                ddlAppealStatus.SelectedValue = p_Var.dSet.Tables[0].Rows[0]["Appeal_Status_Id"].ToString();

                if (p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"] != DBNull.Value)
                {
                    ViewState["DeptID"] = p_Var.dSet.Tables[0].Rows[0]["Deptt_Id"].ToString();
                }
                else
                {
                    ViewState["DeptID"] = DBNull.Value;
                }

            }

        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to bind status of appeal in dropDownlist

    public void bindAppealStatus()
    {
        p_Var.dSet = appealBL.getAppealStatus();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            ddlAppealStatus.DataSource = p_Var.dSet;
            ddlAppealStatus.DataTextField = "STATUS";
            ddlAppealStatus.DataValueField = "STATUS_ID";
            ddlAppealStatus.DataBind();
        }
    }

    #endregion

    protected void CusomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (Convert.ToInt16(args.Value.ToString().Substring(6, 4)) > DateTime.Today.Year)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

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
